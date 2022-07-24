using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirmyMichalowice.Model
{
    public class CompanySetting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool LinkVisibility { get; set; }
        public bool PKDVisibility { get; set; }
        public bool OfferVisibility { get; set; }
    }
}
