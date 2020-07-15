using QoolloTaskViewer.ApiServices;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QoolloTaskViewer.ApiServices
{
    public interface IApiService
    {
        Task<List<IssueDto>> GeatAllMyIssues();
    }
}
