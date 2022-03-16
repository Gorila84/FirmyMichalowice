using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Dto_s
{
    public class UserForRegisterDTO
    {
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Hasło jest wymagane")]
        [StringLength(12, MinimumLength = 6)]
        public string Password { get; set; }
        [Required(ErrorMessage = "NIP jest wymagane")]
        [StringLength(10)]
        public string NIP { get; set; }
        [Required(ErrorMessage = "Krótki opis jest wymagany")]
        [StringLength(255)]
        public string ShortDescription { get; set; }

        public string CompanyType { get; set; }
    }
}
