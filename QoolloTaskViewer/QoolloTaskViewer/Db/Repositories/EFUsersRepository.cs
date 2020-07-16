using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QoolloTaskViewer.Models;

namespace QoolloTaskViewer.Db.Repositories
{
    public class EFUsersRepository : IUsersRepository
    {
        protected QoolloTaskViewerContext Context;

        public EFUsersRepository(QoolloTaskViewerContext context)
        {
            Context = context;
        }

        public async Task<UserModel> FindUserAsync(Guid id)
        {
            return await Context.Users.FindAsync(id);
        }

        public async Task AddUserAsync(UserModel user)
        {
            await Context.Users.AddAsync(user);
            await Context.SaveChangesAsync();
        }

        public Task UpdateUserAsync(UserModel user)
        {
            Context.Entry(user).State = EntityState.Modified;
            return Context.SaveChangesAsync();
        }

        public Task RemoveUserAsync(UserModel user)
        {
            Context.Users.Remove(user);
            return Context.SaveChangesAsync();
        }
    }
}
