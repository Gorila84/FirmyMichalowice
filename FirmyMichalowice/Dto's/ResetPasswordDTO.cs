using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Dto_s
{
    public class ResetPasswordDTO
    {
        [Required]
        public string UserName { get; set; }
    }
}
