using FirmyMichalowice.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FirmyMichalowice.Serv
{
    public class RecaptchaService
    {
        private HttpClient _http;
        private readonly IConfiguration _configuration;
        private readonly ILogger<RecaptchaService> _logger;

        public RecaptchaService(IConfiguration configuration, ILogger<RecaptchaService> logger)
        {
            _http = new HttpClient();
            _configuration = configuration;
            _logger = logger;

        }

        public async Task<RecaptchaServiceResponse> GetData(RecaptchaResponse model)
        {

            string recaUrl = _configuration.GetSection("Recaptcha:apiUrl").Value;
            model.Secret = _configuration.GetSection("Recaptcha:serverSiteKey").Value;
            var response = await _http.PostAsync(recaUrl + string.Format("?secret={0}&response={1}", model.Secret, model.Response), null);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var result = ParseDataToEntity(data);
            return result;


        }

        private RecaptchaServiceResponse ParseDataToEntity(string data)
        {
            try
            {
                var obj = JObject.Parse(data);
                var responseObj = obj.ToString();
                RecaptchaServiceResponse result = System.Text.Json.JsonSerializer.Deserialize<RecaptchaServiceResponse>(responseObj);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return null;
            }
        }
    }
}

