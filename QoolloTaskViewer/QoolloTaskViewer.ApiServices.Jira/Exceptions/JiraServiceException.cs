using System;
using System.Collections.Generic;
using System.Text;

namespace QoolloTaskViewer.ApiServices.Jira.Exceptions
{
    public class JiraServiceException : Exception
    {
        public JiraServiceException() 
            : base("JiraServiceException")
        {
        }
    }
}
