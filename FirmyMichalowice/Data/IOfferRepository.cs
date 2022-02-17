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
        Task<bool> AddOffer(Offer offer);
        Task<bool> RemoveOffer(int id);

        Task<Offer> GetOfferForEdit(int id);
        Task<bool> SaveAll();
    }
}
