using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using QoolloTaskViewer.Models;

namespace QoolloTaskViewer.Db.Repositories
{
    public interface IUsersRepository
    {
        Task<UserModel> FindUserByNameAsync(string username);
        Task<UserModel> FindUserByIdAsync(string id);
        Task<IdentityResult> CreateUserAsync(UserModel user, string password);
        Task UpdateUserAsync(UserModel user);
        Task RemoveUserAsync(UserModel user);
    }
}
