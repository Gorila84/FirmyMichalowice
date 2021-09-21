using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FirmyMichalowice.Data
{
    public class CEIDGmanger
    {
        private HttpClient _http;
        private readonly IConfiguration _configuration;

        public CEIDGmanger(IConfiguration configuration)
        {
            _http = new HttpClient();
            _configuration = configuration;
        }

        public async Task<string> GetData(string nip)
        {
            addHeaders();
            string ceidgUrl = _configuration.GetSection("HurtowniaDanych:Url").Value;
            var response = await _http.GetAsync(string.Format("{0}{1}", ceidgUrl, nip), HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return data;


        }

        private void addHeaders()
        {
            string token = _configuration.GetSection("HurtowniaDanych:Token").Value;
            _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }
}
