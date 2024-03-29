﻿using FirmyMichalowice.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Dto_s
{
    public class CompaniesForListDTO
    {
       
        public int Id { get; set; }
        
        public string Username { get; set; }
       

        public string CompanyName { get; set; }
        public string ShortDescription { get; set; }
        public string PhoneNumber { get; set; }
        public string WebSite { get; set; }
        public string EmailAddress { get; set; }

        public string Municipalitie { get; set; }
        public string PhotoUrl { get; set; }
        public string CompanyType { get; set; }

        public Photo Photo { get; set; }

        [Required(ErrorMessage = "To pole jest obowiązkowe")]
        [StringLength(10)]
        public string NIP { get; set; }
        public PKD MainPKD { get; set; }
        public IList<PKD> PKDS { get; set; }
    }
}
