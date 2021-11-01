using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirmyMichalowice.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(255)]
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Username { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public string CompanyName { get; set; }
        [StringLength(255)]
        public string ShortDescription { get; set; }
        [StringLength(5000)]
        public string LongDescription { get; set; }
        public string PhoneNumber { get; set; }
        public string WebSite { get; set; }
        public string EmailAddress { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string Municipalitie { get; set; } 
        public string Rodo { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modify { get; set; }

        public Photo Photo { get; set; }

        public string CompanyType { get; set; }

        [Required(ErrorMessage ="To pole jest obowiązkowe")]
        [StringLength(10)]
        public string NIP { get; set; }

        [NotMapped]
        public PKD MainPKD { get; set; }
        [NotMapped]
        public IList<PKD> PKDS { get; set; }
        [NotMapped]
        public string GeolocationUrl { get; set; }
        
        public bool IsActive { get; set; }

        public string OfficeCity { get; set; }
        public string OfficeStreet { get; set; }
        public string OfficePostalCode { get; set; }

    }
}
