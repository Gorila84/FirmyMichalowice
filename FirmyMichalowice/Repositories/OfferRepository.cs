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
        private readonly ILoggerManager _logger;

        public OfferRepository(DataContext context, ILoggerManager logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Offer>> GetOffer(int userId)
        {
            var offers =  _context.Offers.Where(x => x.UserId == userId).ToList();

            return offers;
        }

        public async Task<Offer> GetOfferForEdit(int id)
        {
            var offer = _context.Offers.Where(x => x.Id == id).FirstOrDefault();
            return offer;

        }

        public async Task<bool> AddOffer(Offer offer)
        {
            try
            {
                offer.ModifyDate = DateTime.Now;
                _context.Offers.Add(offer);
                await _context.SaveChangesAsync();
                return true;
            }
           catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return false;
            }
        }

        public async Task<bool> RemoveOffer(int id)
        {
            try
            {
                var offerToRemove = _context.Offers.Find(id);
                _context.Remove(offerToRemove);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return false;
            }
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }


    }
}
