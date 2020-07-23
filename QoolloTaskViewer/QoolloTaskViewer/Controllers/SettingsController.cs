using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using QoolloTaskViewer.ApiServices.Enums;
using QoolloTaskViewer.Db.Repositories;
using QoolloTaskViewer.Models;
using QoolloTaskViewer.ViewModels;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace QoolloTaskViewer.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IDomainsRepository _domainsRepository;
        private readonly IServicesRepository _servicesRepository;
        private readonly ITokensRepository _tokensRepository;

        public SettingsController(IUsersRepository usersRepository,
            IDomainsRepository domainsRepository, 
            IServicesRepository servicesRepository, 
            ITokensRepository tokensRepository)
        {
            _usersRepository = usersRepository;
            _domainsRepository = domainsRepository;
            _servicesRepository = servicesRepository;
            _tokensRepository = tokensRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetTokens());
        }

        async Task<SettingsViewModel> GetTokens()
        {
            var user = await _usersRepository.FindUserByNameAsync(HttpContext.User.Identity.Name);
            var tokens = await _tokensRepository.GetTokensAsync(user.Id);

            var models = tokens
                .Select(t => new TokenViewModel
                {
                    Id = t.Id,
                    Token = t.Token,
                    Domain = t.Service.Domain.Domain,
                    InServiceUsername = t.InServiceUsername,
                    Type = t.Service.Type,
                })
                .ToList();

            return new SettingsViewModel { Tokens = models };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToken(SettingsViewModel model)
        {
            if (ModelState.IsValid)
            {
                switch (model.Token.Type)
                {
                    case ServiceType.GitHub:
                        await AddGitHubToken(model.Token);
                        break;
                    case ServiceType.GitLab:
                        await AddGitLabToken(model.Token);
                        break;
                    case ServiceType.Jira:
                        await AddJiraToken(model.Token);
                        break;
                }
                return RedirectToAction("Index", "Settings");
            }
            return View(model);
        }

        async Task AddGitHubToken(TokenViewModel model)
        {
            ServiceModel service = await _servicesRepository.FindServiceByDomainAsync("github.com");
            UserModel user = await _usersRepository.FindUserByNameAsync(model.Username);
            TokenModel token = new TokenModel()
            {
                Id = new Guid(),
                Token = model.Token,
                ServiceId = service.Id,
                UserId = user.Id,
            };
            await _tokensRepository.AddTokenAsync(token);
        }

        async Task AddGitLabToken(TokenViewModel model)
        {
            ServiceModel service = await _servicesRepository.FindServiceByDomainAsync(model.Domain);

            if (service == null)
            {
                DomainModel domain = new DomainModel { Id = new Guid(), Domain = model.Domain };
                await _domainsRepository.AddDomainAsync(domain);
                service = new ServiceModel { Id = new Guid(), DomainId = domain.Id, Type = ServiceType.GitLab };
                await _servicesRepository.AddServiceAsync(service);
            }

            UserModel user = await _usersRepository.FindUserByNameAsync(model.Username);
            TokenModel token = new TokenModel()
            {
                Id = new Guid(),
                Token = model.Token,
                ServiceId = service.Id,
                UserId = user.Id,
            };
            await _tokensRepository.AddTokenAsync(token);
        }

        async Task AddJiraToken(TokenViewModel model)
        {
            ServiceModel service = await _servicesRepository.FindServiceByDomainAsync(model.Domain);

            if (service == null)
            {
                DomainModel domain = new DomainModel { Id = new Guid(), Domain = model.Domain };
                await _domainsRepository.AddDomainAsync(domain);
                service = new ServiceModel { Id = new Guid(), DomainId = domain.Id, Type = ServiceType.Jira };
                await _servicesRepository.AddServiceAsync(service);
            }

            UserModel user = await _usersRepository.FindUserByNameAsync(model.Username);
            TokenModel token = new TokenModel()
            {
                Id = new Guid(),
                Token = model.Token,
                ServiceId = service.Id,
                UserId = user.Id,
                InServiceUsername = model.InServiceUsername,
            };
            await _tokensRepository.AddTokenAsync(token);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteToken(SettingsViewModel model)
        {
            var token = await _tokensRepository.FindTokenAsync(model.Token.Id);
            await _tokensRepository.RemoveTokenAsync(token);

            return RedirectToAction("Index", "Settings");
        }
    }
}
