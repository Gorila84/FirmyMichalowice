using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Model
{

    [Keyless]
    public class CookieConsent
    {
       
        public DateTime Date { get; set; }
        public string UserIP { get; set; }

    }
}
