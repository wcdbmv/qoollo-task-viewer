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
                    case ServiceType.GitLab:
                        break;
                    case ServiceType.GitHub:
                        // DomainModel domain = await _domainsRepository.FindDomainByNameAsync("github.com");
                        // ServiceModel service = await _servicesRepository.FindServiceByDomainIdAsync(domain.Id);
                        ServiceModel service = await _servicesRepository.FindServiceByDomainAsync("github.com");
                        UserModel user = await _usersRepository.FindUserByNameAsync(model.TokenToAdd.UserName);
                        TokenModel token = new TokenModel()
                        {
                            Id = new Guid(),
                            Token = model.TokenToAdd.Token,
                            ServiceId = service.Id,
                            UserId = user.Id,
                        };
                        await _tokensRepository.AddTokenAsync(token);
                        break;
                    case ServiceType.Jira:
                        break;
                }
                return RedirectToAction("Index", "Settings");
            }
            return View(model);
        }
    }
}
