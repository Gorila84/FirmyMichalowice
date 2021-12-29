using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Model
{
    public class RecaptchaResponse
    {
        public string Secret { get; set; }
        public string Response { get; set; }
    }
}
