using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using QoolloTaskViewer.ApiServices.Dtos;
using QoolloTaskViewer.ApiServices.Github;
using QoolloTaskViewer.ApiServices.Gitlab;
using QoolloTaskViewer.ApiServices.Jira;
using QoolloTaskViewer.Models;

namespace QoolloTaskViewer.ApiConnector
{
    public class ApiConnector
    {
        public static Task<List<IssueDto>> GetAllIssuesAsync(TokenModel token)
        {
            switch (token.Service.Type)
            {
                case ApiServices.Enums.ServiceType.GitHub:
                    return (new GithubService(token.Token)).GetAllIssuesAsync();

                case ApiServices.Enums.ServiceType.GitLab:
                    return (new GitlabService(token.Token, token.Service.Domain.Domain)).GetAllIssuesAsync();

                case ApiServices.Enums.ServiceType.Jira:
                    return (new JiraService(token.Token, token.Service.Domain.Domain, token.InServiceUsername)).GetAllIssuesAsync();

                default:
                    return Task.FromResult(new List<IssueDto>());
            }
        }
    }
}
