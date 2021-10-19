using FirmyMichalowice.Data;
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
        public MapBoxService(IConfiguration configuration, MapQuestService mapQuestService)
        {
            _configuration = configuration;
            _mapQuestService = mapQuestService;
        }
        public async Task<string> GetGeolocationURL(Firma firma)
        {
            string adress = string.Format("{0} {1}, {2} {3}", firma.adresDzialanosci.ulica, firma.adresDzialanosci.budynek, firma.adresDzialanosci.miasto, firma.adresDzialanosci.kod);
            var geolocations = await _mapQuestService.GetGeolocations(adress);
            string mapBoxKey = _configuration.GetSection("MapBox:ApiKey").Value;

            //Geolocation geoLocCompany = geolocations.Where(x => x. == "Poland" && x.region == "Lesser Poland Voivodeship" && x.administrative_area == firma.adresDzialanosci.gmina).FirstOrDefault();

            IList<Location> geoLocCompanies = geolocations.Select(x => x.locations).FirstOrDefault();
            Location searchingGeo = geoLocCompanies.Where(y => y.adminArea1 == "PL" && y.adminArea3 == "Lesser Poland Voivodeship" && y.adminArea4 == "gmina " + firma.adresDzialanosci.gmina && (firma.adresDzialanosci.ulica + ' ' + firma.adresDzialanosci.budynek).ToLower().Contains(y.street.ToLower())).FirstOrDefault();


            string geolocationUrl = string.Format("https://api.mapbox.com/styles/v1/mapbox/light-v10/static/pin-s-l+000({1},{0})/{1},{0},14/500x300?access_token={2}", searchingGeo.latLng.lat.ToString().Replace(",", "."), searchingGeo.latLng.lng.ToString().Replace(",", "."), mapBoxKey);
            return geolocationUrl;
        }
    }
}
