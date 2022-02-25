using FirmyMichalowice.Data;
using FirmyMichalowice.Helpers;
using FirmyMichalowice.Model;
using FirmyMichalowice.Serv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FirmyMichalowice.Repositories
{
    public class CompanyRepository : GenericRepository, ICompanyRepository
    {
        private readonly DataContext _context;
        private readonly CeidgService _ceidgService;
        private readonly MapBoxService _mapBoxService;
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;

        public CompanyRepository(DataContext context, CeidgService ceidgService,  IConfiguration configuration, MapBoxService mapBoxService, ILoggerManager logger) : base(context)
        {
            _context = context;
            _ceidgService = ceidgService;
            _configuration = configuration;
            _mapBoxService = mapBoxService;
            _logger = logger;

        }

        public async Task<User> GetCompany(int id, bool isForEdit)
        {
            try
            {
                var user = await _context.Users.Include(x => x.Photo).Include(x=>x.Offers).FirstOrDefaultAsync(u => u.Id == id);
                if (user != null)
                {
                    var firma = await _ceidgService.GetData(user.NIP);
                    user.MainPKD = _context.PKD.Where(x => x.Symbol == Regex.Replace(firma.pkdGlowny, ".{2}", "$0.")).FirstOrDefault();
                    List<string> pkds = new List<string>();
                    firma.pkd.ToList().ForEach(x =>
                    {
                        var kod = Regex.Replace(x, ".{2}", "$0.");
                        pkds.Add(kod);

                    });

                    user.PKDS = _context.PKD.Where(x => pkds.Contains(x.Symbol)).ToList();

                    if (!isForEdit)
                    {
                        string adress = string.Format("{0} {1}, {2} {3}", firma.adresDzialanosci.ulica, firma.adresDzialanosci.budynek, firma.adresDzialanosci.miasto, firma.adresDzialanosci.kod);
                        user.GeolocationUrl = await _mapBoxService.GetGeolocationURL(adress, firma.adresDzialanosci.gmina, false);
                        if (!string.IsNullOrEmpty(user.OfficeCity) && !string.IsNullOrEmpty(user.OfficeStreet) && !string.IsNullOrEmpty(user.OfficePostalCode))
                        {
                            string adress2 = ValidateOfficeAdress(user.OfficeCity, user.OfficeStreet, user.OfficePostalCode);  
                            user.Geolocation2Url = await _mapBoxService.GetGeolocationURL(adress2, user.OfficeMunicipalitie, true);
                        }
                    }
                    user.StatusFromCeidg = firma.status;
                
                }

                return user;
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return null;
            }
            
        }

        private string ValidateOfficeAdress(string officeCity, string officeStreet, string officePostalCode)
        {
            Regex regex = new Regex(@"\s");
            string[] bits = regex.Split(officeStreet.ToLower());
            string[] numbers = bits.Last().Split("/");
            string ulOrOs = bits.First() == "os." || bits.First() == "os" ? "osiedle" : bits.First();
            string number = numbers.First();
            List<string> possibleCases = new List<string>() { "ul", "ul.", "os.", "os", "osiedle", "ulica" };


            if (bits.Length == 3 && possibleCases.Contains(ulOrOs)) 
            {
                return string.Format("{0}, {1} {2}", string.Format("{0} {1} {2}", ulOrOs, bits[1], number), officeCity, officePostalCode);
            }
            if(bits.Length > 3)
            {
                return string.Format("{0}, {1} {2}", officeStreet, officeCity, officePostalCode);
            }
            else
            {
                return string.Format("{0}, {1} {2}", string.Format("{0} {1}",  bits.First(), bits.Last()), officeCity, officePostalCode);
            }
        
           
        }

        public async Task<PageList<User>> GetCompanies(UserParams userParams)
        {
            var users = _context.Users.Where(x=>x.IsActive == true).Include(p => p.Photo).AsQueryable();

            if (userParams.CompanyName != null )
            {
                users = users.Where(u => u.CompanyName.Contains(userParams.CompanyName));
            }
            if ( userParams.CompanyType != null)
            {
                users = users.Where(u => u.CompanyType == userParams.CompanyType);
            }
            if (userParams.City != null)
            {
                users = users.Where(u => u.City == userParams.City);
            }

            var cos = users.Count();

            return await PageList<User>.CreateListAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<List<User>> GetCompaniesForAdmin()
        {
            var users = _context.Users.ToList();

            //if (userParams.CompanyName != null)
            //{
            //    users = users.Where(u => u.CompanyName.Contains(userParams.CompanyName));
            //}
            //if (userParams.CompanyType != null)
            //{
            //    users = users.Where(u => u.CompanyType == userParams.CompanyType);
            //}
            //if (userParams.City != null)
            //{
            //    users = users.Where(u => u.City == userParams.City);
            //}

            var cos = users.Count();

            return users;
        }



        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IList<string>> GetCompanyTypes()
        {
            return _context.Trade.OrderBy(x => x.Name).Select(x => x.Name).ToList();
        }

      
    }
}
