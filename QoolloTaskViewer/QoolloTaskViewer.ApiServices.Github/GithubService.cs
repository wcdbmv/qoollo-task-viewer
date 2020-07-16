using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using QoolloTaskViewer.ApiServices.Dtos;

namespace QoolloTaskViewer.ApiServices.Github
{
    public class GithubService : IApiService
    {
        private readonly string baseAddress = "https://api.github.com";

        private readonly string _token;

        private HttpClient Client { get; set; }

        public GithubService(string token)
        {
            _token = token;
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

        public async Task<List<IssueDto>> GeatAllMyIssuesAsync()
        {
            var query = "/issues?scope=assigned_to_me";
            var stringTask = await Client.GetStreamAsync(baseAddress + query);
            List<GithubIssueDto> rawIssues;
            try
            {
                rawIssues = await JsonSerializer.DeserializeAsync<List<GithubIssueDto>>(stringTask);
            }
            catch (JsonException e)
            {
                throw e;
            }

            return mapIssues(rawIssues);
        }

        List<IssueDto> mapIssues(List<GithubIssueDto> rawIssues)
        {
            List<IssueDto> issues = new List<IssueDto>();

            foreach (var rawIssue in rawIssues)
            {
                LabelFinder labelFinder = new LabelFinder(rawIssue.labels);
                string dueDate = null;

                if (rawIssue.milestone != null)
                {
                    dueDate = rawIssue.milestone.due_on;
                }

                State issueState;
                if (rawIssue.state == "open")
                {
                    issueState = State.Open;
                }
                else
                {
                    issueState = State.Closed;
                }

                IssueDto issue = new IssueDto
                {
                    Name = rawIssue.title,
                    State = issueState,
                    Description = rawIssue.body,
                    Labels = rawIssue.labels,
                    Difficulty = labelFinder.GetDifficulty(),
                    Priority = labelFinder.GetPriority(),
                    DueDate = dueDate,
                    ServiceInfo = new ServiceInfoDto
                    {
                        Name = Service.Github,
                        Url = rawIssue.html_url
                    }
                };

                issues.Add(issue);
            }

            return issues;
        }
    }
}
