namespace FirmyMichalowice.Model
{
    public class UserSettings
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool UsingGoggleMaps { get; set; }
        public bool UsingActiveLink { get; set; }
    }
}