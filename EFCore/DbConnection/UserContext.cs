﻿using EFCore.Model;
using EFCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.DbConnection
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfig());
        }
    }
}
