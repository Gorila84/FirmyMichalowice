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

        public string Municipalitie { get; set; }

        //public DataType Created { get; set; }
        //public DataType Modify { get; set; }

        public string PhotoUrl { get; set; }
        
        public string CompanyType { get; set; }

        public Photo Photo { get; set; }

        [Required(ErrorMessage = "To pole jest obowiązkowe")]
        [StringLength(10)]
        public string NIP { get; set; }

        public PKD MainPKD { get; set; }
        public IList<PKD> PKDS { get; set; }

        public string GeolocationUrl { get; set; }

        public string ArmsUrl { get; set; }
        public bool AdditionalAddress { get; set; }
        public string OfficeCity { get; set; }
        public string OfficeStreet { get; set; }
        public string OfficePostalCode { get; set; }
        public string OfficeMunicipalitie { get; set; }
        public string StatusFromCeidg { get; set; }
        public string Geolocation2Url { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modify { get; set; }
        public IList<Offer> Offers { get; set; }
        public Geometry Geometry { get; set; }
        public Geometry Geometry2 { get; set; }

        public UserSettings UserSettings { get; set; }

    }
}
