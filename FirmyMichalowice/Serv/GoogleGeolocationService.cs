using FirmyMichalowice.Data;
using FirmyMichalowice.Helpers;
using FirmyMichalowice.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace FirmyMichalowice.Serv
{
    public class GoogleGeolocationService: IGoogleGeolocationService
    {
        private readonly IConfiguration _configuration;
        private HttpClient _http;
        private protected string apiUrl = "https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}";
        private ILoggerManager _logger;
        public GoogleGeolocationService(IConfiguration configuration, ILoggerManager logger)
        {
            _configuration = configuration;
            _http = new HttpClient();
            _logger = logger;
        }

        public async Task<GeolocationFromGoogle> GetGeolocations(string adress)
        {
            string apiKey = _configuration.GetSection("GoogleMaps:ApiKey").Value;
            string apiURL = string.Format(apiUrl, adress, apiKey);
            var response = await _http.GetAsync(apiURL, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var geolocations = ParseDataToGeolocationEntity(data);
            return geolocations;
        }

        private GeolocationFromGoogle ParseDataToGeolocationEntity(string data)
        {
            try
            {

                var obj = JObject.Parse(data);
                var geoString = obj.First.Children().Last().ToString();
                IList<GeolocationFromGoogle> geolocations = JsonSerializer.Deserialize<IList<GeolocationFromGoogle>>(geoString);
                return geolocations.First();

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return null;
            }
        }
    }
    
}
