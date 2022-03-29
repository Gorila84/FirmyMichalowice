using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Dto_s
{
    public class CompaniesForEditAdminDTO
    {
        public int Id { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
    }
}
