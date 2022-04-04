using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Model
{
    public class AppConfiguration
    {
        public int Id { get; set; }
        public string KeyName { get; set; }
        public bool IsActive { get; set; }
        public DateTime Create { get; set; }
        public DateTime Update { get; set; }


    }
}
