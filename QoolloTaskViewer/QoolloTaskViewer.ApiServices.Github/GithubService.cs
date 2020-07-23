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
using System.IO;
using QoolloTaskViewer.ApiServices.Github.Exceptions;

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
            Stream streamTask;
            try
            {
                streamTask = await Client.GetStreamAsync(baseAddress + query);
            }
            catch (HttpRequestException)
            {
                throw new GithubServiceException();
            }

            List<GithubIssueDto> rawIssues;
            try
            {
                rawIssues = await JsonSerializer.DeserializeAsync<List<GithubIssueDto>>(streamTask);
            }
            catch (JsonException)
            {
                throw new GithubServiceException();
            }

            return MapIssues(rawIssues);
        }

        List<IssueDto> MapIssues(List<GithubIssueDto> rawIssues)
        {
            List<IssueDto> issues = new List<IssueDto>();

            foreach (var rawIssue in rawIssues)
            {
                DateTime? dueDateResult = null;
                DateTime dueDate;
                
                if (rawIssue.milestone != null)
                {
                    if (DateTime.TryParse(rawIssue.milestone.due_on, out dueDate))
                        dueDateResult = dueDate;
                }

                List<string> labels = new List<string>();

                foreach (var label in rawIssue.labels)
                {
                    labels.Add(label.name);
                }

                LabelFinder labelFinder = new LabelFinder(labels);



                State issueState = rawIssue.state == "closed" ? State.Closed : labelFinder.GetState();
                
                IssueDto issue = new IssueDto
                {
                    Name = rawIssue.title,
                    State = issueState,
                    Description = rawIssue.body,
                    DueDate = dueDateResult,
                    Difficulty = labelFinder.GetDifficulty(),
                    Priority = labelFinder.GetPriority(),
                    Labels = labels,
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
