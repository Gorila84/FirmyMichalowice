using FirmyMichalowice.Data;
using FirmyMichalowice.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FirmyMichalowice.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly CEIDGmanger _cEIDGmanger;

        public AuthRepository(DataContext context, CEIDGmanger cEIDGmanger)
        {
            _context = context;
            _cEIDGmanger = cEIDGmanger;
        }
        #region method public

        public async Task<User> Login(string username, string pasword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null)
                return null;

            if (!VerifyPasswordHash(pasword, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }



        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHashSalt(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }



        public async Task<Tuple<bool, string>> UserValidation(string userName, string nip)
        {
            var ceidgJson = await _cEIDGmanger.GetData(nip);
            var data = JObject.Parse(ceidgJson);
            var companYJson = data.First.First.First.ToString();


            try
            {
                Firma firma = JsonSerializer.Deserialize<Firma>(companYJson);
            }
            catch(Exception ex)
            {

            }
       

            if (await _context.Users.AnyAsync(x => x.Username == userName))
            {
                Tuple<bool, string> result = Tuple.Create(true, "Użytkownik o podanej nazwie juz istnieje! Podaj inna nazwe użytkownika.");
                return result;                       
            }

            if (await _context.Users.AnyAsync(x => x.NIP == nip))
            {
                Tuple<bool, string> result = Tuple.Create(true, "Użytkownik o podanym NIPie juz istnieje! Sprawdź NIP");
                return result;
            }

          

            return Tuple.Create(false, string.Empty);
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
        #endregion
    }
}
