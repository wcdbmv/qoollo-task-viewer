using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QoolloTaskViewer.Models;

namespace QoolloTaskViewer.Db.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.Property(u => u.Username).IsRequired();
            builder.Property(u => u.Username).HasMaxLength(255);
            builder.HasAlternateKey(u => u.Username);

            builder.Property(u => u.PasswordHash).IsRequired();
        }
    }
}
