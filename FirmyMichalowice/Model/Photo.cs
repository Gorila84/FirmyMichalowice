using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Model
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }     // Opis
        public DateTime DateAdded { get; set; }     // Data dodania
        public bool IsMain { get; set; }            // Czy zdjęcie jest główne

       // public User User { get; set; }
        public int UserId { get; set; }

        public byte[] FileData { get; set; }
    }
}
