using System;

namespace FirmyMichalowice.Model
{
    public class UserSettings
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public int SettingsTemplateId { get; set; }
        public SettingsTemplate SettingsTemplate { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime SubscriptionEndDate { get; set; }
    }
}