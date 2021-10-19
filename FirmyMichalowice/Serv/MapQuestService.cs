using FirmyMichalowice.Helpers;
using FirmyMichalowice.Model;
using log4net.Core;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace FirmyMichalowice.Data
{
    public class MapQuestService: IMapQuestService
    {
        private readonly IConfiguration _configuration;
        private HttpClient _http;
        private protected string apiUrl = "http://www.mapquestapi.com/geocoding/v1/address?key=";
        private ILoggerManager _logger;
        public MapQuestService(IConfiguration configuration, ILoggerManager logger)
        {
            _configuration = configuration;
            _http = new HttpClient();
            _logger = logger;
        }

        public async Task<IList<Geolocation>> GetGeolocations(string adress)
        {
            string apiKey = _configuration.GetSection("MapQuest:ApiKey").Value;
            string apiURL = apiUrl + apiKey + "&location=" + adress;
            var response = await _http.GetAsync(apiURL, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var geolocations = ParseDataToGeolocationEntity(data);
            return geolocations;
        }

        private IList<Geolocation> ParseDataToGeolocationEntity(string data)
        {
            try
            {

                var obj = JObject.Parse(data);
                var geoString = obj.Last.Children().Last().ToString();
                IList<Geolocation> geolocations = JsonSerializer.Deserialize<IList<Geolocation>>(geoString);
                return geolocations;
               
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return null;
            }
        }
    }
}
