﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QoolloTaskViewer.Db.Configuration;
using QoolloTaskViewer.Models;

namespace QoolloTaskViewer.Db
{
    public class QoolloTaskViewerContext : IdentityDbContext<UserModel>
    {
        public DbSet<DomainModel> Domains { get; set; }
        public DbSet<ServiceModel> Services { get; set; }
        public DbSet<TokenModel> Tokens { get; set; }

        public QoolloTaskViewerContext(DbContextOptions<QoolloTaskViewerContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new DomainConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceConfiguration());
            modelBuilder.ApplyConfiguration(new TokenConfiguration());
        }
    }
}
