using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Model
{
    public class RecaptchaServiceResponse
    {
        public bool success { get; set; }
        public decimal score { get; set; }
        public string action { get; set; }
        public DateTime challenge_ts { get; set; }
        public string hostname { get; set; }
    }
}
