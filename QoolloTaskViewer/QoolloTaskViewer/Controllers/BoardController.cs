using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QoolloTaskViewer.ApiServices.Dtos;
using QoolloTaskViewer.ApiServices.Enums;
using QoolloTaskViewer.Db.Repositories;
using QoolloTaskViewer.ViewModels;

namespace QoolloTaskViewer.Controllers
{
    [Authorize]
    public class BoardController : Controller
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ITokensRepository _tokensRepository;

        public BoardController(IUsersRepository usersRepository,
            ITokensRepository tokensRepository)
        {
            _usersRepository = usersRepository;
            _tokensRepository = tokensRepository;
        }

        public async Task<IActionResult> Index()
        {
             return View(await GetIssues());
        }

        public IActionResult Task()
        {
            return PartialView("_Task");
        }

        async Task<IssuesViewModel> GetIssues()
        {
            //List<ServiceInfoDto> services = new List<ServiceInfoDto>
            //{
            //    new ServiceInfoDto { ServiceType = ServiceType.GitLab },
            //    new ServiceInfoDto { ServiceType = ServiceType.GitHub },
            //    new ServiceInfoDto { ServiceType = ServiceType.Jira },
            //};

            //List<IssueDto> testIssues = new List<IssueDto>
            //{
            //    new IssueDto { Name = "Разработать макет веб-приложения", Description = "", Difficulty = Difficulty.Hard, ServiceInfo = services[0], DueDate = new DateTime(2020,07,12,0,0,0), Priority = Priority.High, State = State.ToDo, Url = "https://google.com/" },
            //    new IssueDto { Name = "Разработать макет приложения", Description = "", Difficulty = Difficulty.Easy, ServiceInfo = services[1], DueDate = new DateTime(2020,07,29,0,0,0), Priority = Priority.Low, State = State.ToDo, Url = "https://google.com/" },
            //    new IssueDto { Name = "Построить схему модулей приложения с потоками данных", Description = "", Difficulty = Difficulty.Medium, ServiceInfo = services[2], DueDate = new DateTime(2020,07,11,0,0,0), Priority = Priority.Medium, State = State.ToDo, Url = "https://google.com/" },
            //    new IssueDto { Name = "Создать файл с описанием основной идеи и функциональноcти приложения", Description = "", Difficulty = Difficulty.Easy, ServiceInfo = services[0], DueDate = new DateTime(2020,07,18,0,0,0), Priority = Priority.Low, State = State.ToDo, Url = "https://google.com/" },
                
            //    new IssueDto { Name = "Выбрать проект для практики", Description = "", Difficulty = Difficulty.Medium, ServiceInfo = services[2], DueDate = new DateTime(2020,07,07,0,0,0), Priority = Priority.Medium, State = State.Review, Url = "https://google.com/" },
                
            //    new IssueDto { Name = "Сделать курсовую по операционным системам", Description = "", Difficulty = Difficulty.Medium, ServiceInfo = services[2], DueDate = new DateTime(2020,07,07,0,0,0), Priority = Priority.Medium, State = State.Unrecognized, Url = "https://google.com/" },
            //    new IssueDto { Name = "Посмотреть смешные видосы", Description = "", Difficulty = Difficulty.Easy, ServiceInfo = services[2], DueDate = new DateTime(2020,07,07,0,0,0), Priority = Priority.Medium, State = State.Unrecognized, Url = "https://google.com/" },
            //};

            IssuesViewModel model = new IssuesViewModel();

            var user = await _usersRepository.FindUserByNameAsync(HttpContext.User.Identity.Name);
            var tokens = await _tokensRepository.GetTokensAsync(user.Id);
            foreach (var token in tokens)
            {
                List<IssueDto> issues;
                try
                {
                    issues = await ApiConnector.ApiConnector.GetAllIssuesAsync(token);
                }
                catch (Exception)
                {
                    continue;
                }

                foreach (var issue in issues)
                {
                    switch (issue.State)
                    {
                        case State.Unrecognized:
                            model.UnrecognizedIssues.Add(issue);
                            break;
                        case State.ToDo:
                            model.ToDoIssues.Add(issue);
                            break;
                        case State.Doing:
                            model.DoingIssues.Add(issue);
                            break;
                        case State.Review:
                            model.ReviewIssues.Add(issue);
                            break;
                        default:
                            break;
                    }
                }
            }

            return model;
        }
    }
}
