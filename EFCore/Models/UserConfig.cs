using EFCore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Models
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User() { Id = Guid.NewGuid(), Email = "user@gmail.com", FullName = "user", Age = 19, Role = Roles.User },
                new User() { Id = Guid.NewGuid(), Email = "admin@gmail.com", FullName = "admin", Age = 19, Role = Roles.Admin });
        }
    }
}
