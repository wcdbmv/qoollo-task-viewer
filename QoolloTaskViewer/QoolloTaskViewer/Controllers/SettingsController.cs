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

namespace QoolloTaskViewer.Controllers
{
    public class SettingsController : Controller
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IDomainsRepository _domainsRepository;
        private readonly IServicesRepository _servicesRepository;
        private readonly ITokensRepository _tokensRepository;

        public SettingsController (IUsersRepository usersRepository,
            IDomainsRepository domainsRepository, 
            IServicesRepository servicesRepository, 
            ITokensRepository tokensRepository)
        {
            _usersRepository = usersRepository;
            _domainsRepository = domainsRepository;
            _servicesRepository = servicesRepository;
            _tokensRepository = tokensRepository;
        }

        public IActionResult Index()
        {
            return View(GetTokens());
        }

        SettingsViewModel GetTokens()
        {
            return new SettingsViewModel { Tokens = new List<TokenViewModel>() };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToken(SettingsViewModel model)
        {
            if (ModelState.IsValid)
            {
                switch (model.TokenToAdd.Type)
                {
                    case ServiceType.GitHub:
                        await AddGitHubToken(model.TokenToAdd);
                        break;
                    case ServiceType.GitLab:
                        await AddGitLabToken(model.TokenToAdd);
                        break;
                    case ServiceType.Jira:
                        await AddJiraToken(model.TokenToAdd);
                        break;
                }
                return RedirectToAction("Index", "Settings");
            }
            return View(model);
        }

        private async Task AddGitHubToken(TokenViewModel model)
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

        private async Task AddGitLabToken(TokenViewModel model)
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

        private async Task AddJiraToken(TokenViewModel model)
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
    }
}
