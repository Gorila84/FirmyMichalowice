using FirmyMichalowice.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WcfCoreMtomEncoder;

namespace FirmyMichalowice.Helpers
{
    public class RegonServiceHelper
    {
        private readonly IConfiguration _configuration;
        public RegonServiceHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SetupBinding(ref UslugaBIRzewnPublClient client)
        {
            var encoding = new MtomMessageEncoderBindingElement(new TextMessageEncodingBindingElement());
            var transport = new HttpsTransportBindingElement();

            var customBinding = new CustomBinding(encoding, transport);

            client.Endpoint.Binding = customBinding;
        }




        public FirmaRS ParseDataToFirmaEntity(string data)
        {
            try
            {
                var obj = JObject.Parse(data);
                var companyObj = obj["root"]["dane"];

                FirmaRS firma = JsonSerializer.Deserialize<FirmaRS>(companyObj.ToString());
                return firma;
            }
            catch (Exception ex)
            {
       
                return null;
            }
        }

    }
}
