using FirmyMichalowice.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        //public DbSet<CompanyType> CompanyTypes { get; set; }
        public DbSet<Photo> Photo { get; set; }
        public DbSet<Municipalitie> Municipalities { get; set; }
        public DbSet<PKD> PKD { get; set; }
        public DbSet<CookieConsent> CookieConsents { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Trade> Trade { get; set; }
        public DbSet<AppConfiguration> AppConfigurations { get; set; }

        public DbSet<CompanySetting> CompanySettings { get; set; }
    }
}
