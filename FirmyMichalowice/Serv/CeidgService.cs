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

namespace FirmyMichalowice.Data
{
    public class CeidgService: ICeidgService
    {
        private HttpClient _http;
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;

        public CeidgService(IConfiguration configuration, ILoggerManager logger)
        {
            _http = new HttpClient();
            _configuration = configuration;
            _logger = logger;

        }

        public async Task<Firma> GetData(string nip)
        {
            addHeaders();
            string ceidgUrl = _configuration.GetSection("HurtowniaDanych:Url").Value;
            var response = await _http.GetAsync(string.Format("{0}{1}", ceidgUrl, nip), HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var firma = ParseDataToFirmaEntity(data);
            return firma;


        }

        private Firma ParseDataToFirmaEntity(string data)
        {
            try
            {
                var obj = JObject.Parse(data);
                var companyObj = obj.First.First.First.ToString();
                Firma firma = JsonSerializer.Deserialize<Firma>(companyObj);
                return firma;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return null;
            }
        }

        private void addHeaders()
        {
            string token = _configuration.GetSection("HurtowniaDanych:Token").Value;
            _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }
}
