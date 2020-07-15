using System;
using System.Collections.Generic;
using System.Text;

namespace QoolloTaskViewer.ApiServices
{
    public class Label
    {
        public string name { get; set; }
        public string description { get; set; }
    }

    public class Milestone
    {
        public string due_on { get; set; }
    }

    public class IssueDto
    {
        public string title { get; set; }
        public string state { get; set; }

        public string body { get; set; }

        public List<Label> labels { get; set; }
        public Milestone milestone { get; set; }

        public string html_url { get; set; }
    }
}
