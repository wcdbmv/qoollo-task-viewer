using System;
using System.Collections.Generic;
using System.Text;

namespace QoolloTaskViewer.ApiServices.Jira.Dtos
{
    class JiraResponseDto
    {
        public string expand { get; set; }
        public List<JiraIssueDto> issues { get; set; }
    }
}
