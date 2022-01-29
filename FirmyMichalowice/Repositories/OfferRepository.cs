using FirmyMichalowice.Data;
using FirmyMichalowice.Dto_s;
using FirmyMichalowice.Helpers;
using FirmyMichalowice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        private readonly DataContext _context;

        public OfferRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Offer>> GetOffer(int userId)
        {
            var offers =  _context.Offers.Where(x => x.UserId == userId).ToList();

            return offers;
        }

        public void AddOffer(Offer offer)
        {
            offer.ModifyDate = DateTime.Now;
             _context.Offers.Add(offer);
            _context.SaveChanges();
        }

        public void RemoveOffer(int id)
        {
            var offerToRemove = _context.Offers.Find(id);
            _context.Offers.Remove(offerToRemove);
            _context.SaveChanges();

        }

       
    }
}
