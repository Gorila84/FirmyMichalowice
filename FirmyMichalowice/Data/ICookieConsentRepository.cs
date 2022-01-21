using FirmyMichalowice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Data
{
    public interface ICookieConsentRepository
    {
      void AddConsent(CookieConsent consent);
    }
}
