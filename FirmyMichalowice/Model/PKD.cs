using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Model
{
    [Keyless]
    public class PKD
    {
      
        public string Symbol { get; set; }
        public string Nazwa { get; set; }
    }
}
