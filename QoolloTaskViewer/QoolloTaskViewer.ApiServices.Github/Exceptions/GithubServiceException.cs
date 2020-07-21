using System;
using System.Collections.Generic;
using System.Text;

namespace QoolloTaskViewer.ApiServices.Github.Exceptions
{
    public class GithubServiceException : Exception
    {
        public GithubServiceException()
            : base("GithubServiceException")
        {
        }
    }
}
