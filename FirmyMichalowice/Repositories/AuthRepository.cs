using FirmyMichalowice.Controllers;
using FirmyMichalowice.Data;
using FirmyMichalowice.Helpers;
using FirmyMichalowice.Model;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Linq;

using System.Text;
using System.Threading.Tasks;



namespace FirmyMichalowice.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly CeidgService _cEIDGmanger;
        private readonly SmtpManager _smtpManager;
        private readonly ILoggerManager _logger;

        public AuthRepository(DataContext context, CeidgService cEIDGmanger, SmtpManager smtpManager, ILoggerManager logger)
        {
            _context = context;
            _cEIDGmanger = cEIDGmanger;
            _smtpManager = smtpManager;
            _logger = logger;
        }
        #region method public

        public async Task<User> Login(string username, string pasword)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
                if (user == null)
                    return null;

                if (!VerifyPasswordHash(pasword, user.PasswordHash, user.PasswordSalt))
                    return null;

                return user;
            }
            catch( Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return null;
            }
           
        }



        public async Task<User> Register(User user, string password)
        {
            try
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHashSalt(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.IsActive = true;

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return null;
            }
        }



            public async Task<Tuple<bool, string, string>> UserValidation(string userName, string nip)
        {

            bool isError = false;
            string errorMessage = string.Empty;
            string municipalitie = string.Empty;

            try
            {


                if (await _context.Users.AnyAsync(x => x.Username == userName))
                {
                    isError = true;
                    errorMessage = "Użytkownik o podanej nazwie juz istnieje! Podaj inna nazwe użytkownika.";

                }

                if (await _context.Users.AnyAsync(x => x.NIP == nip))
                {
                    isError = true;
                    errorMessage = "Użytkownik o podanym NIPie juz istnieje! Sprawdź NIP";

                }
                if (isError == false && string.IsNullOrEmpty(errorMessage)) CheckMunicipalitie(ref isError, ref errorMessage, ref municipalitie, nip);

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return Tuple.Create(true, ex.Message, municipalitie);

            }

            return Tuple.Create(isError, errorMessage, municipalitie);
        }

        private void CheckMunicipalitie(ref bool isError, ref string errorMessage, ref string municipalitie, string nip)
        {

            var firma = _cEIDGmanger.GetData(nip).Result;
            IQueryable<string> listOfAllowedMunicipalities = _context.Municipalities.Select(x => x.Name);

            municipalitie = firma.adresDzialanosci.gmina;

            if (!listOfAllowedMunicipalities.Contains(firma.adresDzialanosci.gmina))
            {
                isError = true;
                errorMessage = "Firma zarejestrowana poza dozwolonymi gminami";
                //municipalitie = firma.adresDzialanosci.gmina;
            }

        }
        #endregion
        #region method private
        private void CreatePasswordHashSalt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string pasword, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(pasword));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
                return true;
            }
        }

        public string ResetPassword(string userName)
        {
            try
            {
                var user =  _context.Users.Where(x => x.Username == userName).FirstOrDefault() ?? throw new Exception("Nie ma takiego usera");

                if (user != null)
                {
                    Random random = new Random();
                    byte[] passwordHash, passwordSalt;
                    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    string password = new string(Enumerable.Repeat(chars, 6)
                        .Select(s => s[random.Next(s.Length)]).ToArray());

                    CreatePasswordHashSalt(password, out passwordHash, out passwordSalt);

             
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;

                    _context.SaveChanges();
                    return password;

                }
                return null;

            }
            catch (Exception e)
            {

                throw;
            }
           



        }

        public void ChangePassword(int id, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHashSalt(password, out passwordHash, out passwordSalt);

            var user = _context.Users.Where(x => x.Id == id).FirstOrDefault();


            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.SaveChanges();





            #endregion
        }
    }
}
