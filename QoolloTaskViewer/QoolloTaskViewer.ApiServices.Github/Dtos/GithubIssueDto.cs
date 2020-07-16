using System;
using System.Collections.Generic;
using System.Text;
using QoolloTaskViewer.ApiServices.Dtos;

namespace QoolloTaskViewer.ApiServices.Github.Dtos
{
    public class GithubIssueDto
    {
        public string title { get; set; }
        public string state { get; set; }

        public string body { get; set; }

        public List<LabelDto> labels { get; set; }
        public MilestoneDto milestone { get; set; }

        public string html_url { get; set; }
    }
}
