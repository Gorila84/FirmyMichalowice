using FirmyMichalowice.Data;
using FirmyMichalowice.Helpers;
using FirmyMichalowice.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Xml;

namespace FirmyMichalowice.Serv
{
    public class RegonService : IRegonService
    {
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;

        public RegonService(IConfiguration configuration, ILoggerManager logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<FirmaRS> GetData(string nip)
        {
            UslugaBIRzewnPublClient client = new UslugaBIRzewnPublClient();
            RegonServiceHelper serviceHelper = new RegonServiceHelper(_configuration);
            serviceHelper.SetupBinding(ref client);

            string apiKey = _configuration.GetSection("API_REGON:ApiKey").Value;
            var isLogin = await client.ZalogujAsync(apiKey);
            var sessionId = isLogin.ZalogujResult;

            OperationContextScope scope = new OperationContextScope(client.InnerChannel);

            HttpRequestMessageProperty requestProperties = new HttpRequestMessageProperty();
            requestProperties.Headers.Add("sid", sessionId);
            OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestProperties;

            var regonData = await client.DaneSzukajPodmiotyAsync(new ParametryWyszukiwania() { Nip = nip });

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(regonData.DaneSzukajPodmiotyResult);
                string jsonText = JsonConvert.SerializeXmlNode(doc);
                var result = serviceHelper.ParseDataToFirmaEntity(jsonText); // tutaj mamy nasz objekt
              //  var test1 = await client.DanePobierzPelnyRaportAsync(result.Regon, "BIR11OsFizycznaPkd");
              //  var test2 = await client.DanePobierzPelnyRaportAsync(result.Regon, "BIR11OsFizycznaDaneOgolne");
              // var test3 = await client.DanePobierzPelnyRaportAsync(result.Regon, "BIR11OsFizycznaDzialalnoscCeidg");
              //  var rgt = await client.DanePobierzPelnyRaportAsync(result.Regon, "BIR11OsPrawnaPkd");
              //  var rgt2 = await client.DanePobierzPelnyRaportAsync(result.Regon, "BIR11OsPrawna");
                await client.WylogujAsync(sessionId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }
            return null;
        }
    }
}
