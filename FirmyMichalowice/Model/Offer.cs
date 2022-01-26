using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Model
{
    public class Offer
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public double Price { get; set; }
        public int UserId { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
