using FirmyMichalowice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Data
{
    public interface IAdminRepository
    {
        Task<bool> AddConfiguration(AppConfiguration appConfiguration);
        Task<AppConfiguration> GetAppConfigurationForEdit(int id);
        Task<List<AppConfiguration>> GetAppConfigurationKeys();


    }
}
