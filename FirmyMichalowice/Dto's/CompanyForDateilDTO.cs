using FirmyMichalowice.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Dto_s
{
    public class CompanyForDateilDTO
    {
       
        public int Id { get; set; }
       
        public string Username { get; set; }
      
        public string CompanyName { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string PhoneNumber { get; set; }
        public string WebSite { get; set; }
        public string EmailAddress { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
       
        //public DataType Created { get; set; }
        //public DataType Modify { get; set; }

        public string PhotoUrl { get; set; }
        
        public string CompanyType { get; set; }

        public Photo Photo { get; set; }

    }
}
