using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QoolloTaskViewer.ApiServices.Github
{
    class GithubService : IApiService
    {
        private readonly string baseAddress = "https://api.github.com";

        private string Token { get; set; }
        private HttpClient Client { get; set; }

        public GithubService(string token)
        {
            Token = token;
            CreateClient();
            AuthorizeClient();
        }

        void CreateClient()
        {
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Add("User-Agent", "QoolloTaskViewer");
        }

        void AuthorizeClient()
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
        }

        async Task<List<IssueDto>> IApiService.GeatAllMyIssues()
        {
            var query = "/issues?scope=assigned_to_me";
            var stringTask = Client.GetStreamAsync(baseAddress + query);
            var issues = await JsonSerializer.DeserializeAsync<List<IssueDto>>(await stringTask);

            return issues;
        }
    }
}
