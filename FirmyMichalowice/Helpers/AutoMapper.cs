using AutoMapper;
using FirmyMichalowice.Dto_s;
using FirmyMichalowice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Helpers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<User, CompaniesForListDTO>();
            CreateMap<User, CompanyForDateilDTO>(); 
            CreateMap<Photo, PhotoForDetailDTO>();
            CreateMap<CompanyForDateilDTO, User>();
            CreateMap<CompaniesForEditDTO, User>();
            CreateMap<Trade, CompanyTypeDTO>();
            CreateMap<CookieConsentDTO, CookieConsent>();
            CreateMap<OfferDTO, Offer>();
            CreateMap<OfferForEditDTO, Offer>();
            CreateMap<SettingsTemplate, SettingsTemplateDTO>();
            CreateMap<SettingsTemplateDTO, SettingsTemplate>();
         
        }
    }
}
