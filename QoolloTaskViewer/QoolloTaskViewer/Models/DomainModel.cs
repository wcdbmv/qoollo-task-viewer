using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QoolloTaskViewer.Models
{
    public class DomainModel
    {
        public Guid Id { get; set; }
        public string Domain { get; set; }

        public List<ServiceModel> Services { get; set; }
    }
}
