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

        public async Task<List<IssueDto>> GeatAllIssuesAsync()
        {
            var query = "/issues?scope=assigned_to_me";
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

            return MappIssues(rawIssues);
        }

        List<IssueDto> MappIssues(List<GitlabIssueDto> rawIssues)
        {
            List<IssueDto> issues = new List<IssueDto>();

            foreach (var rawIssue in rawIssues)
            {
                DateTime dueDate = default;

                GitLabelFinder labelFinder = new GitLabelFinder(rawIssue.labels);

                State issueState = rawIssue.state == "closed" ? State.Closed : labelFinder.GetState();

                if (rawIssue.due_date != null)
                {
                    dueDate = DateTime.Parse(rawIssue.due_date);
                }

                IssueDto issue = new IssueDto
                {
                    Name = rawIssue.title,
                    State = issueState,
                    Description = rawIssue.description,
                    Labels = rawIssue.labels,
                    Difficulty = labelFinder.GetDifficulty(),
                    Priority = labelFinder.GetPriority(),
                    DueDate = dueDate,
                    ServiceInfo = new ServiceInfoDto
                    {
                        ServiceType = ServiceType.Gitlab
                    },
                    Url = rawIssue.web_url
                };

                issues.Add(issue);
            }

            return issues;
        }
    }
}
