using System;

namespace FirmyMichalowice.Model
{
    public class CompanySetting
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool LinkVisibility { get; set; }
        public DateTime LinkVisibilityStart { get; set; }
        public DateTime LinkVisibilityEnd { get; set; }
        public bool PKDVisibility { get; set; }
        public DateTime PKDVisibilityStart { get; set; }
        public DateTime PKDVisibilityEnd { get; set; }
        public bool OfferVisibility { get; set; }
        public DateTime OfferVisibilityStart { get; set; }
        public DateTime OfferVisibilityEnd { get; set; }
    }
}
