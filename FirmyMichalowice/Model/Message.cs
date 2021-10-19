using System.ComponentModel.DataAnnotations;

namespace FirmyMichalowice.Controllers
{
    public class Message
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Content { get; set; }

        public bool IsEmailValid()
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(this.Username);
                return addr.Address == this.Username;
            }
            catch
            {
                return false;
            }
        }
    }
}