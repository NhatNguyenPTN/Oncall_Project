using CRUD_EF.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_EF.DbConnection
{
    public class UserContext : DbContext
    {
        public DbSet<User> users { get; set; }

        private const string connectionString = @"Host=localhost;Database=useroncall2;Port=5432;User ID=postgres;
                                                  Username=postgres;Password=224339";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          => optionsBuilder.UseNpgsql(connectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
