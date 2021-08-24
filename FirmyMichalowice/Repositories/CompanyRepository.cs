using FirmyMichalowice.Data;
using FirmyMichalowice.Helpers;
using FirmyMichalowice.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FirmyMichalowice.Repositories
{
    public class CompanyRepository : GenericRepository, ICompanyRepository
    {
        private readonly DataContext _context;
       

        public CompanyRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetCompany(int id)
        {
            var user = await _context.Users.Include(x=>x.Photo).FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<PageList<User>> GetCompanies(UserParams userParams)
        {
            var users = _context.Users.Include(x => x.Photo);
            return await PageList<User>.CreateListAsync(users, userParams.PageNumber, userParams.PageSize);
        }

 

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IList<string>> GetCompanyTypes()
        {
            return _context.CompanyTypes.OrderBy(x => x.Name).Select(x=>x.Name).ToList();
        }

      
    }
}
