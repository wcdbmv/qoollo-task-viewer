using System;
using System.Collections.Generic;
using System.Text;

namespace QoolloTaskViewer.ApiServices.Gitlab.Exceptions
{
    public class GitlabServiceException : Exception
    {
        public GitlabServiceException()
            : base("GitlabServiceException")
        {
        }
    }
}
