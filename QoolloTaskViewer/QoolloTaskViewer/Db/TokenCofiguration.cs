using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QoolloTaskViewer.Models;

namespace QoolloTaskViewer.Db
{
    public class TokenConfiguration : IEntityTypeConfiguration<TokenModel>
    {
        public void Configure(EntityTypeBuilder<TokenModel> builder)
        {
            builder.HasOne(t => t.User).WithMany(u => u.Tokens).HasForeignKey(t => t.UserId);
            builder.HasOne(t => t.Service).WithMany(s => s.Tokens).HasForeignKey(t => t.ServiceId);

            builder.Property(t => t.InServiceUsername).IsRequired();
            builder.Property(t => t.InServiceUsername).HasMaxLength(255);

            builder.Property(t => t.Token).IsRequired();
        }
    }
}
