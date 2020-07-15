using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QoolloTaskViewer.Models;

namespace QoolloTaskViewer.Db
{
    public class ServiceTypeConfiguration : IEntityTypeConfiguration<ServiceTypeModel>
    {
        public void Configure(EntityTypeBuilder<ServiceTypeModel> builder)
        {
            builder.ToTable("ServiceTypes");

            builder.Property(s => s.Type).IsRequired();
            builder.Property(s => s.Type).HasMaxLength(16);
            builder.HasAlternateKey(s => s.Type);

            builder.HasData(
                new ServiceTypeModel[]
                {
                    new ServiceTypeModel{ Id=Guid.NewGuid(), Type="GitHub" },
                    new ServiceTypeModel{ Id=Guid.NewGuid(), Type="GitLab" },
                    new ServiceTypeModel{ Id=Guid.NewGuid(), Type="Atlassian Jira" },
                }
            );
        }
    }
}
