using Domain.Contracts.Infra;
using Domain.Contracts.Infra.Repositories;
using Domain.Entities;
using Infra.Configurations;
using Infra.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra
{
    public class UnitOfWork : DbContext, IUnitOfWork
    {
        public UnitOfWork(IConfiguration configuration)
            :base(GetOptions(configuration))
        {
        }


        public IRepository<Profile> ProfileRepository => throw new NotImplementedException();

        public IRepository<Event> EventRepository => throw new NotImplementedException();

        public IRepository<Profile_Event> Profile_EventRepository => throw new NotImplementedException();

        public void Commit() => base.SaveChanges();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProfileConfiguration());
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new Profile_EventConfiguration());
        }

        private static DbContextOptions GetOptions(IConfiguration configuration)
        {
            var configFromAppSettings = configuration.Get<Configuration>();
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), configFromAppSettings.ConnectionString).Options;
        }
        
    }
}
