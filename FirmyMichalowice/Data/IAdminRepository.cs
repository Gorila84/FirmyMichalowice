using FirmyMichalowice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Data
{
    public interface IAdminRepository
    {
        Task<bool> AddConfigurations(AppConfiguration appConfiguration);
        Task<AppConfiguration> GetAppConfigurationForEdit(int id);
        Task<List<AppConfiguration>> GetAppConfigurationKeys();
        Task<CompanySetting> GetCompanySettingsKeys(int userId);
        Task<bool> GetAppConfigurationValue(string keyName);
        Task<bool> AddCompanyConfigurations(CompanySetting companySetting);
        Task<bool> GetLinkVisibility(int userId);
        Task<bool> GetPKDisibility(int userId);
        Task<bool> GetOfferisibility(int userId);



    }
}
