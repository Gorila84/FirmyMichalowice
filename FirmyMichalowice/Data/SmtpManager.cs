using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Data
{
    public class SmtpManager
    {
        private IConfiguration _configuration;
        public SmtpManager( IConfiguration configuartion)
        {
            _configuration = configuartion;
        }

        public string Server { get => _configuration.GetSection("SmtpSettings:Server").Value; }
        public string User { get => _configuration.GetSection("SmtpSettings:User").Value;  }
        public string Password { get => _configuration.GetSection("SmtpSettings:Password").Value;  }

        public int Port { get => int.Parse(_configuration.GetSection("SmtpSettings:Port").Value);  }
    }
}
