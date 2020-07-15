using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QoolloTaskViewer.Models
{
    public enum ServiceModelType
    {
        GitHub,
        GitLab,
        Jira,
    }

    public class ServiceModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public ServiceModelType Type { get; set; }
    }
}
