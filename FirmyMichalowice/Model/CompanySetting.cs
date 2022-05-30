using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Model
{
    public class CompanySetting
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool LinkVisibility { get; set; }
        public bool PKDVisibility { get; set; }
        public bool OfferVisibility { get; set; }
    }
}
