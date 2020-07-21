using System;
using System.Collections.Generic;
using System.Text;

namespace QoolloTaskViewer.ApiServices.Jira.Dtos
{
    class JiraIssueFieldsDto
    {
        public string description { get; set; }
        public string summary { get; set; }
        public string duedate { get; set; }
        public JiraIssuePriorityDtp priority { get; set; }
        public JiraIssueStatusDto status { get; set; }
        public List<string> labels { get; set; }
    }
}
