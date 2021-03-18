using FirmyMichalowice.Data;
using FirmyMichalowice.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<IEnumerable<User>> GetCompanies()
        {
            var users = await _context.Users.Include(p => p.Photos).ToListAsync();
            return users;
        }
    }
}
