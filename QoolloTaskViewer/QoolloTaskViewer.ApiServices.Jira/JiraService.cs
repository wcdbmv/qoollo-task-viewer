using QoolloTaskViewer.ApiServices.Dtos;
using QoolloTaskViewer.ApiServices.Enums;
using QoolloTaskViewer.ApiServices.Jira.Dtos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using QoolloTaskViewer.Models;
using System.IO;
using QoolloTaskViewer.ApiServices.Jira.Exceptions;

namespace QoolloTaskViewer.ApiServices.Jira
{
    public class JiraService : IApiService
    {
        private readonly string baseAddress;
        private readonly string _token;
        private readonly string _username;

        private HttpClient Client { get; set; }

        public JiraService(string token, string address, string username)
        {
            _token = token;
            baseAddress = address;
            _username = username;
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
            var byteArray = Encoding.ASCII.GetBytes(_username + ':' + _token);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        public async Task<List<IssueDto>> GetAllIssuesAsync()
        {
            string query = "/rest/api/2/search?jql=assignee=currentuser()";
            Stream streamTask;
            try
            {
                streamTask = await Client.GetStreamAsync(baseAddress + query);
            }
            catch (HttpRequestException)
            {
                throw new JiraServiceException();
            }

            JiraResponseDto response;
            try
            {
                response = await JsonSerializer.DeserializeAsync<JiraResponseDto>(streamTask);
            }
            catch (JsonException)
            {
                throw new JiraServiceException();
            }

            return MapIssues(response.issues);
        }

        List<IssueDto> MapIssues(List<JiraIssueDto> rawIssues)
        {
            List<IssueDto> issues = new List<IssueDto>();
            if (rawIssues != null)
            {
                foreach (var rawIssue in rawIssues)
                {
                    DateTime? dueDateResult = null;
                    DateTime dueDate;

                    if (DateTime.TryParse(rawIssue.fields.duedate, out dueDate))
                    {
                        dueDateResult = dueDate;                            
                    }

                    LabelFinder labelFinder = new LabelFinder(rawIssue.fields.labels);

                    IssueDto issue = new IssueDto
                    {
                        Name = rawIssue.fields.summary,
                        State = MapState(rawIssue.fields.status.statusCategory.key),
                        Description = rawIssue.fields.description,
                        DueDate = dueDateResult,
                        Difficulty = labelFinder.GetDifficulty(),
                        Priority = MapPriority(rawIssue.fields.priority.name),
                        Labels = rawIssue.fields.labels,
                        ServiceInfo = new ServiceInfoDto
                        {
                            ServiceType = ServiceType.Jira
                        },
                        Url = baseAddress + "/browse/" + rawIssue.key
                    };

                    issues.Add(issue);
                }
            }

            return issues;
        }

        Priority MapPriority(string name)
        {
            Priority priority = Priority.Unrecognized;

            if (name == "High" || name == "Highest")
            {
                priority = Priority.High;
            }
            else if (name == "Low" || name == "Lowest")
            {
                priority = Priority.Low;
            }
            else if (name == "Medium")
            {
                priority = Priority.Medium;
            }

            return priority;
        }

        State MapState(string key)
        {
            State state = State.Unrecognized;

            switch (key)
            {
                case "done":
                    state = State.Closed;
                    break;
                case "indeterminate":
                    state = State.Doing;
                    break;
                case "new":
                    state = State.ToDo;
                    break;
                case "undefined":
                    state = State.Unrecognized;
                    break;
            }

            return state;
        }
    }
}
