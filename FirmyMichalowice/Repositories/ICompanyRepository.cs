using FirmyMichalowice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Repositories
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<User>> GetCompanies();
        Task<User> GetCompany(int id);
        Task<bool> SaveAll();
    }
}
