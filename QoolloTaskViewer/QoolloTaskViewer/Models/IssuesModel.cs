using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QoolloTaskViewer.ApiServices.Dtos;

namespace QoolloTaskViewer.Models
{
    public class Issues
    {
        public List<IssueDto> UnrecognizedIssues { get; set; }
        public List<IssueDto> ToDoIssues { get; set; }
        public List<IssueDto> DoingIssues { get; set; }
        public List<IssueDto> ReviewIssues { get; set; }

        public Issues()
        {
            UnrecognizedIssues = new List<IssueDto> { };
            ToDoIssues = new List<IssueDto> { };
            DoingIssues = new List<IssueDto> { };
            ReviewIssues = new List<IssueDto> { };
        }
    }
}
