using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Dto_s
{
    public class SettingsTemplateDTO
    {
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int LengthService { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string CreatorUser { get; set; }
        public string UpdateUser { get; set; }
    }
}
