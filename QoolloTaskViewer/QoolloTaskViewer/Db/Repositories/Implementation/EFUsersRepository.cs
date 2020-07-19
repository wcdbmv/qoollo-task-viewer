using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QoolloTaskViewer.Models;

namespace QoolloTaskViewer.Db.Repositories.Implementation
{
    public class EFUsersRepository : EFBaseRepository, IUsersRepository
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;

        public EFUsersRepository(QoolloTaskViewerContext context, UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
            : base(context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public Task<UserModel> FindUserByNameAsync(string username)
        {
            return _userManager.Users
                .Include(u => u.Tokens)
                    .ThenInclude(t => t.Service)
                        .ThenInclude(s => s.Domain)
                .FirstOrDefaultAsync(u => u.UserName == username);
        }

        public Task<UserModel> FindUserByIdAsync(string id)
        {
            return _userManager.Users
                .Include(u => u.Tokens)
                    .ThenInclude(t => t.Service)
                        .ThenInclude(s => s.Domain)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IdentityResult> CreateUserAsync(UserModel user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
            }

            return result;
        }

        public async Task<SignInResult> PasswordSignInAsync(string username, string password, bool rememberMe)
        {
            return await _signInManager.PasswordSignInAsync(username, password, rememberMe, false);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public Task UpdateUserAsync(UserModel user)
        {
            _context.Entry(user).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        public Task RemoveUserAsync(UserModel user)
        {
            _context.Users.Remove(user);
            return _context.SaveChangesAsync();
        }
    }
}
