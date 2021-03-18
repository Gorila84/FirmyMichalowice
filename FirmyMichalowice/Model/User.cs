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
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string PhoneNumber { get; set; }
        public string WebSite { get; set; }
        public string EmailAddress { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string Rodo { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modify { get; set; }

        public ICollection<Photo> Photos { get; set; }


    }
}
