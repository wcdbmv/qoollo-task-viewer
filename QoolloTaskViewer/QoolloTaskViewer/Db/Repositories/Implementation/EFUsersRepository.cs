using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QoolloTaskViewer.Models;

namespace QoolloTaskViewer.Db.Repositories.Implementation
{
    public class EFUsersRepository : EFBaseRepository, IUsersRepository
    {
        public EFUsersRepository(QoolloTaskViewerContext context)
            : base(context)
        {
        }

        public Task<UserModel> FindUserAsync(Guid id)
        {
            return _context.Users
                .Include(u => u.Tokens)
                    .ThenInclude(t => t.Service)
                        .ThenInclude(s => s.Domain)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task AddUserAsync(UserModel user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
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
