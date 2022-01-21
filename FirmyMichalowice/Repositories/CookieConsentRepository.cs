using FirmyMichalowice.Data;
using FirmyMichalowice.Helpers;
using FirmyMichalowice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Repositories
{
    public class CookieConsentRepository : ICookieConsentRepository
    {
        private readonly DataContext _context;
        private readonly ILoggerManager _logger;

        public CookieConsentRepository(DataContext context, ILoggerManager logger)
        {
            _context = context;
            _logger = logger;
            
        }
        public void AddConsent(CookieConsent consent)
        {
            try
            {
                _context.CookieConsents.Add(consent);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }
        }
    }
}
