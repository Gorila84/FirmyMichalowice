﻿using FirmyMichalowice.Model;
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
        public DbSet<CompanyType> CompanyTypes { get; set; }
        public DbSet<Photo> Photo { get; set; }
        public DbSet<Municipalitie> Municipalities { get; set; }
        public DbSet<PKD> PKD { get; set; }
    }
}
