using System;
using System.Collections.Generic;
using System.Text;
using QoolloTaskViewer.ApiServices.Enums;

namespace QoolloTaskViewer.ApiServices.Dtos
{   
    public class IssueDto
    {
        public string Name { get; set; }
        public State State { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public Difficulty Difficulty { get; set; }
        public Priority Priority { get; set; }
        public List<string> Labels { get; set; }
        public ServiceInfoDto ServiceInfo { get; set; }
        public string Url { get; set; }
    }
}
