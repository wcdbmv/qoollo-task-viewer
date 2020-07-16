using System;
using System.Collections.Generic;
using System.Text;

namespace QoolloTaskViewer.ApiServices.Dtos
{
    public enum Service
    {
        Github,
        Gitlab,
        Jira
    }

    public class ServiceInfoDto
    {
        public Service Name { get; set; }
        public string Url { get; set; }
    }
}
