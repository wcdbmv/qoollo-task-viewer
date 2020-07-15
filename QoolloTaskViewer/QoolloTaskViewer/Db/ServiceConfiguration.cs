using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QoolloTaskViewer.Models;

namespace QoolloTaskViewer.Db
{
    public class ServiceConfiguration : IEntityTypeConfiguration<ServiceModel>
    {
        public void Configure(EntityTypeBuilder<ServiceModel> builder)
        {
            builder.HasOne(s => s.Domain).WithMany(d => d.Services).HasForeignKey(s => s.DomainId);
            builder.HasOne(s => s.Type).WithMany(t => t.Services).HasForeignKey(s => s.ServiceTypeId);

            builder.Property(s => s.Name).IsRequired();
            builder.Property(s => s.Name).HasMaxLength(255);
            builder.HasAlternateKey(s => s.Name);
        }
    }
}
