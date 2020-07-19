using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QoolloTaskViewer.Models;

namespace QoolloTaskViewer.Db.Repositories
{
    public interface IUsersRepository
    {
        Task<UserModel> FindUserAsync(string username, string password);
        Task<UserModel> FindUserAsync(Guid id);
        Task AddUserAsync(UserModel user);
        Task UpdateUserAsync(UserModel user);
        Task RemoveUserAsync(UserModel user);
    }
}
