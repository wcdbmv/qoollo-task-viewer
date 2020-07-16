using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QoolloTaskViewer.Models;

namespace QoolloTaskViewer.Db.Configuration
{
    public class DomainConfiguration : IEntityTypeConfiguration<DomainModel>
    {
        public void Configure(EntityTypeBuilder<DomainModel> builder)
        {
            builder.Property(d => d.Domain).IsRequired();
            builder.Property(d => d.Domain).HasMaxLength(255);
            builder.HasAlternateKey(d => d.Domain);

            builder.HasData(
                new DomainModel[]
                {
                    new DomainModel { Id=Guid.NewGuid(), Domain="github.com" },
                    new DomainModel { Id=Guid.NewGuid(), Domain="gitlab.com" },
                    new DomainModel { Id=Guid.NewGuid(), Domain="jira.atlassian.com" },
                }
            );
        }
    }
}
