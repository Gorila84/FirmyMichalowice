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

        public CompanyRepository(DataContext context, CeidgService ceidgService,  IConfiguration configuration, MapBoxService mapBoxService) : base(context)
        {
            _context = context;
            _ceidgService = ceidgService;
            _configuration = configuration;
            _mapBoxService = mapBoxService;

        }

        public async Task<User> GetCompany(int id)
        {
            try
            {
                var user = await _context.Users.Include(x => x.Photo).FirstOrDefaultAsync(u => u.Id == id);
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
                    string adress = string.Format("{0} {1}, {2} {3}", firma.adresDzialanosci.ulica, firma.adresDzialanosci.budynek, firma.adresDzialanosci.miasto, firma.adresDzialanosci.kod);
                    user.GeolocationUrl = await _mapBoxService.GetGeolocationURL(adress, firma.adresDzialanosci.gmina);
                    if (!string.IsNullOrEmpty(user.OfficeCity))
                    {
                        string adress2 = string.Format("{0}, {1} {2}", user.OfficeStreet, user.OfficeCity, user.OfficePostalCode);
                        user.Geolocation2Url = await _mapBoxService.GetGeolocationURL(adress2, user.OfficeMunicipalitie);
                    }
                    user.StatusFromCeidg = firma.status;
                
                }

                return user;
            }
            catch(Exception ex)
            {
                return null;
            }
            
        }

       

        public async Task<PageList<User>> GetCompanies(UserParams userParams)
        {
            var users = _context.Users.Where(x=>x.IsActive == true).Include(p => p.Photo).AsQueryable();

            if (userParams.CompanyName != null || userParams.CompanyType != null || userParams.City != null)
            {
                users = users.Where(u => u.CompanyName.Contains(userParams.CompanyName) 
                                    || u.CompanyType == userParams.CompanyType 
                                    || u.City == userParams.City);
            }
   
            
            var cos = users.Count();

            return await PageList<User>.CreateListAsync(users, userParams.PageNumber, userParams.PageSize);
        }

 

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IList<string>> GetCompanyTypes()
        {
            return _context.CompanyTypes.OrderBy(x => x.Name).Select(x => x.Name).ToList();
        }

      
    }
}
