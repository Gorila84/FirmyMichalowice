using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Model
{


    public class CookieConsent
    {
        public int Id { get; set; } 
        public DateTime Date { get; set; }
        public string UserIP { get; set; }

    }
}
