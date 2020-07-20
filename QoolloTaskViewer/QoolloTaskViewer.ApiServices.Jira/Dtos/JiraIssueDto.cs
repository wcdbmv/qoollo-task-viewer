using System;
using System.Collections.Generic;
using System.Text;

namespace QoolloTaskViewer.ApiServices.Jira.Dtos
{
    class JiraIssueDto
    {
        public string key { get; set; }
        public JiraIssueFieldsDto fields { get; set; }
    }
}
