using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QoolloTaskViewer.Models;

namespace QoolloTaskViewer.Db.Repositories.Implementation
{
    public class EFUsersRepository : EFBaseRepository, IUsersRepository
    {
        private readonly UserManager<UserModel> _userManager;

        public EFUsersRepository(QoolloTaskViewerContext context, UserManager<UserModel> userManager)
            : base(context)
        {
            _userManager = userManager;
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
            return await _userManager.CreateAsync(user, password);
        }

        public Task UpdateUserAsync(UserModel user)
        {
            return _userManager.UpdateAsync(user);
        }

        public Task RemoveUserAsync(UserModel user)
        {
            return _userManager.DeleteAsync(user);
        }
    }
}
