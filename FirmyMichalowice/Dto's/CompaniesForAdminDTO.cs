using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Dto_s
{
    public class CompaniesForAdminDTO
    {
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string WebSite { get; set; }
        public string EmailAddress { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string Rodo { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modify { get; set; }
        public string CompanyType { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
    }
}
