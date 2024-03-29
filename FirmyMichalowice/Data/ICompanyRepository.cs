﻿using FirmyMichalowice.Helpers;
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
        Task<IList<string>> GetCompanyTypes();
    }
}
