using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QoolloTaskViewer.ApiServices.Dtos;
using QoolloTaskViewer.ApiServices.Enums;
using QoolloTaskViewer.Models;

namespace QoolloTaskViewer.Controllers
{
    public class BoardController : Controller
    {
        public IActionResult Index()
        {
             return View(GetIssues());
        }

        public IActionResult Task()
        {
            return PartialView("_Task");
        }

        Issues GetIssues()
        {
            List<ServiceInfoDto> services = new List<ServiceInfoDto>
            {
                new ServiceInfoDto { ServiceType = ApiServices.Enums.ServiceType.Gitlab },
                new ServiceInfoDto { ServiceType = ApiServices.Enums.ServiceType.Github },
                new ServiceInfoDto { ServiceType = ApiServices.Enums.ServiceType.Jira },
            };

            List<IssueDto> testIssues = new List<IssueDto>
            {
                new IssueDto { Name = "Разработать макет веб-приложения", Description = "", Difficulty = Difficulty.Hard, ServiceInfo = services[0], DueDate = new DateTime(2020,07,12,0,0,0), Priority = Priority.High, State = State.ToDo, Url = "https://google.com/" },
                new IssueDto { Name = "Разработать макет приложения", Description = "", Difficulty = Difficulty.Easy, ServiceInfo = services[1], DueDate = new DateTime(2020,07,29,0,0,0), Priority = Priority.Low, State = State.ToDo, Url = "https://google.com/" },
                new IssueDto { Name = "Построить схему модулей приложения с потоками данных", Description = "", Difficulty = Difficulty.Medium, ServiceInfo = services[2], DueDate = new DateTime(2020,07,11,0,0,0), Priority = Priority.Medium, State = State.ToDo, Url = "https://google.com/" },
                new IssueDto { Name = "Создать файл с описанием основной идеи и функциональноcти приложения", Description = "", Difficulty = Difficulty.Easy, ServiceInfo = services[0], DueDate = new DateTime(2020,07,18,0,0,0), Priority = Priority.Low, State = State.ToDo, Url = "https://google.com/" },
                
                new IssueDto { Name = "Выбрать проект для практики", Description = "", Difficulty = Difficulty.Medium, ServiceInfo = services[2], DueDate = new DateTime(2020,07,07,0,0,0), Priority = Priority.Medium, State = State.Review, Url = "https://google.com/" },
                
                new IssueDto { Name = "Сделать курсовую по операционным системам", Description = "", Difficulty = Difficulty.Medium, ServiceInfo = services[2], DueDate = new DateTime(2020,07,07,0,0,0), Priority = Priority.Medium, State = State.Unrecognized, Url = "https://google.com/" },
                new IssueDto { Name = "Посмотреть смешные видосы", Description = "", Difficulty = Difficulty.Easy, ServiceInfo = services[2], DueDate = new DateTime(2020,07,07,0,0,0), Priority = Priority.Medium, State = State.Unrecognized, Url = "https://google.com/" },
            };

            Issues issues = new Issues();

            foreach (var issue in testIssues)
            {
                switch (issue.State)
                {
                    case State.Unrecognized:
                        issues.UnrecognizedIssues.Add(issue);
                        break;
                    case State.ToDo:
                        issues.ToDoIssues.Add(issue);
                        break;
                    case State.Doing:
                        issues.DoingIssues.Add(issue);
                        break;
                    case State.Review:
                        issues.ReviewIssues.Add(issue);
                        break;
                    default:
                        break;
                }
            }

            return issues;
        }
    }
}
