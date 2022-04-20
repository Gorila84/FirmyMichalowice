  using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirmyMichalowice.Model
{
    public class AppConfiguration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string KeyName { get; set; }
        public bool IsActive { get; set; }
        public DateTime Create { get; set; }
        public DateTime Update { get; set; }


    }
}
