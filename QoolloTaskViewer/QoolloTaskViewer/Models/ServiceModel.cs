using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QoolloTaskViewer.Models
{
    public enum ServiceType
    {
        GitHub,
        GitLab,
        Jira,
    }

    public class ServiceModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid DomainId { get; set; }
        public ServiceType Type { get; set; }

        public DomainModel Domain { get; set; }
        public List<TokenModel> Tokens { get; set; }
    }
}
