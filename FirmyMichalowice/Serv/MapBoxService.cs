using FirmyMichalowice.Data;
using FirmyMichalowice.Helpers;
using FirmyMichalowice.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Serv
{
    public class MapBoxService : IMapBoxService
    {
        private readonly IConfiguration _configuration;
        private readonly MapQuestService _mapQuestService;
        private ILoggerManager _logger;
        public MapBoxService(IConfiguration configuration, MapQuestService mapQuestService, ILoggerManager logger)
        {
            _configuration = configuration;
            _mapQuestService = mapQuestService;
            _logger = logger;
        }
        public async Task<string> GetGeolocationURL(string adress, string gmina, bool isOfficeGeoloacations)
        {
            try
            {
                var geolocations = await _mapQuestService.GetGeolocations(adress);
                string mapBoxKey = _configuration.GetSection("MapBox:ApiKey").Value;

                //Geolocation geoLocCompany = geolocations.Where(x => x. == "Poland" && x.region == "Lesser Poland Voivodeship" && x.administrative_area == firma.adresDzialanosci.gmina).FirstOrDefault();

                IList<Location> geoLocCompanies = geolocations.Select(x => x.locations).FirstOrDefault();
                if(!geoLocCompanies.Any(x=>x.adminArea1 == "PL"))
                {
                    geolocations = await _mapQuestService.GetGeolocations(adress + ", PL");
                    geoLocCompanies = geolocations.Select(x => x.locations).FirstOrDefault();
                }
                Location searchingGeo;
                if (isOfficeGeoloacations)
                {
                    searchingGeo = geoLocCompanies.Where(y => y.adminArea1 == "PL" && (y.adminArea3 == "Lesser Poland Voivodeship" || y.adminArea3 == "Małopolskie") && (adress.Split(",").First().ToLower().Contains(y.street.ToLower()))).FirstOrDefault();

                }
                else
                {
                    var searchingAdress = adress.Split(",").First().Split(".").Last().ToLower();
                    searchingGeo = geoLocCompanies.Where(y => y.adminArea1 == "PL" && (y.adminArea3 == "Lesser Poland Voivodeship" || y.adminArea3 == "Małopolskie" ) && y.adminArea4 == "gmina " + gmina && (adress.Split(",").First().ToLower().Contains(y.street.ToLower()))).FirstOrDefault();
                }
               
                string geolocationUrl = string.Empty;

                if (searchingGeo != null)
                    geolocationUrl = string.Format("https://api.mapbox.com/styles/v1/mapbox/light-v10/static/pin-s-l+000({1},{0})/{1},{0},14/500x300?access_token={2}", searchingGeo.latLng.lat.ToString().Replace(",", "."), searchingGeo.latLng.lng.ToString().Replace(",", "."), mapBoxKey);
                return geolocationUrl;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return string.Empty;
            }
        }
    }
}
