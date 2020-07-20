using System;
using System.Collections.Generic;
using System.Text;

namespace QoolloTaskViewer.ApiServices.Gitlab.Dtos
{
    class GitlabIssueDto
    {
        public string title { get; set; }
        public string state { get; set; }
        public string description { get; set; }
        public List<string> labels { get; set; }
        public string web_url { get; set; }
        public string due_date { get; set; }
    }
}
