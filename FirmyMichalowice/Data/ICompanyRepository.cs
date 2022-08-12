using FirmyMichalowice.Helpers;
using FirmyMichalowice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Repositories
{
    public interface ICompanyRepository
    {
        Task<PageList<User>> GetCompanies(UserParams userParams);
        Task<User> GetCompany(int id);
        Task<bool> SaveAll();
        Task<bool> UpdateUser(User user);
        Task<IList<string>> GetCompanyTypes();
        Task<IList<string>> GetMunicipalieties();
        Task<List<User>> GetCompaniesForAdmin();
        Task<List<User>> GetCompaniesForAdminForAdmin();
        Task<bool> AddCompanyConfigurations(CompanySetting companySetting);
        Task<CompanySetting> GetCompanySettingsKeys(int userId);
    }
}
