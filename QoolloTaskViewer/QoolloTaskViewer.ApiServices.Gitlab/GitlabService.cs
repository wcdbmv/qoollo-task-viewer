using QoolloTaskViewer.ApiServices.Dtos;
using QoolloTaskViewer.ApiServices.Enums;
using QoolloTaskViewer.ApiServices.Gitlab.Dtos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using QoolloTaskViewer.Models;

namespace QoolloTaskViewer.ApiServices.Gitlab
{
    public class GitlabService : IApiService
    {
        private readonly string baseAddress;
        private readonly string _token;

        private HttpClient Client { get; set; }

        public GitlabService(string token, string address)
        {
            _token = token;
            baseAddress = address;
            CreateClient();
            AuthorizeClient();
        }

        void CreateClient()
        {
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Add("User-Agent", CommonData.AppName);
        }

        void AuthorizeClient()
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }

        public async Task<List<IssueDto>> GetAllIssuesAsync()
        {
            string query = "/issues?scope=assigned_to_me";
            var stringTask = await Client.GetStreamAsync(baseAddress + query);
            List<GitlabIssueDto> rawIssues;
            try
            {
                rawIssues = await JsonSerializer.DeserializeAsync<List<GitlabIssueDto>>(stringTask);
            }
            catch (JsonException)
            {
                return null;
            }

            return MapIssues(rawIssues);
        }

        List<IssueDto> MapIssues(List<GitlabIssueDto> rawIssues)
        {
            List<IssueDto> issues = new List<IssueDto>();

            foreach (var rawIssue in rawIssues)
            {
                DateTime dueDate;
                
                if (!DateTime.TryParse(rawIssue.due_date, out dueDate))
                {
                    dueDate = default;
                }

                LabelFinder labelFinder = new LabelFinder(rawIssue.labels);

                State issueState = rawIssue.state == "closed" ? State.Closed : labelFinder.GetState();

                IssueDto issue = new IssueDto
                {
                    Name = rawIssue.title,
                    State = issueState,
                    Description = rawIssue.description,
                    DueDate = dueDate,
                    Difficulty = labelFinder.GetDifficulty(),
                    Priority = labelFinder.GetPriority(),
                    Labels = rawIssue.labels,
                    ServiceInfo = new ServiceInfoDto
                    {
                        ServiceType = ServiceType.GitLab
                    },
                    Url = rawIssue.web_url
                };

                issues.Add(issue);
            }

            return issues;
        }
    }
}
