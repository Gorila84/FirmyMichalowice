using AutoMapper;
using FirmyMichalowice.Dto_s;
using FirmyMichalowice.Model;

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
            CreateMap<CompaniesForAdminDTO, User>();
            CreateMap<User, CompaniesForAdminDTO>();
            CreateMap<UpdateCompanySettingDTO, CompanySetting>();
            CreateMap<CompanySetting, UpdateCompanySettingDTO>();

        }
    }
}
