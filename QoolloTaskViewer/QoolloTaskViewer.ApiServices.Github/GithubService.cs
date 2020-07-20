using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using QoolloTaskViewer.ApiServices.Github.Dtos;
using QoolloTaskViewer.ApiServices.Dtos;
using QoolloTaskViewer.ApiServices.Enums;
using QoolloTaskViewer.Models;

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

        public async Task<List<IssueDto>> GetAllIssuesAsync()
        {
            string query = "/issues?scope=assigned_to_me";
            var stringTask = await Client.GetStreamAsync(baseAddress + query);
            List<GithubIssueDto> rawIssues;
            try
            {
                rawIssues = await JsonSerializer.DeserializeAsync<List<GithubIssueDto>>(stringTask);
            }
            catch (JsonException)
            {
                return null;
            }

            return MapIssues(rawIssues);
        }

        List<IssueDto> MapIssues(List<GithubIssueDto> rawIssues)
        {
            List<IssueDto> issues = new List<IssueDto>();

            foreach (var rawIssue in rawIssues)
            {
                DateTime dueDate = default;

                List<string> labels = new List<string>();

                foreach (var label in rawIssue.labels)
                {
                    labels.Add(label.name);
                }

                LabelFinder labelFinder = new LabelFinder(labels);

                if (rawIssue.milestone != null && rawIssue.milestone.due_on != null)
                {
                    dueDate = DateTime.Parse(rawIssue.milestone.due_on);
                }

                State issueState = rawIssue.state == "closed" ? State.Closed : labelFinder.GetState();
                
                IssueDto issue = new IssueDto
                {
                    Name = rawIssue.title,
                    State = issueState,
                    Description = rawIssue.body,
                    Labels = labels,
                    Difficulty = labelFinder.GetDifficulty(),
                    Priority = labelFinder.GetPriority(),
                    DueDate = dueDate,
                    ServiceInfo = new ServiceInfoDto
                    {
                        ServiceType = ServiceType.GitHub
                    },
                    Url = rawIssue.html_url
                };

                issues.Add(issue);
            }

            return issues;
        }
    }
}
