using FirmyMichalowice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Data
{
    public interface IOfferRepository
    {
         Task<List<Offer>> GetOffer(int userId);
        void AddOffer(Offer offer);
        void RemoveOffer(int id);
    }
}
