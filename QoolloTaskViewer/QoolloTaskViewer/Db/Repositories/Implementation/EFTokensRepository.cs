using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QoolloTaskViewer.Models;

namespace QoolloTaskViewer.Db.Repositories.Implementation
{
    public class EFTokensRepository : EFBaseRepository, ITokensRepository
    {
        public EFTokensRepository(QoolloTaskViewerContext context)
            : base(context)
        {
        }

        public Task<List<TokenModel>> GetTokensAsync(Guid userId, bool enabledOnly = true)
        {
            Expression<Func<TokenModel, bool>> predicate = t => t.UserId == userId;
            if (enabledOnly)
                predicate = t => t.UserId == userId && t.Enabled;

            return _context.Tokens.Where(predicate)
                    .Include(t => t.Service)
                        .ThenInclude(s => s.Domain)
                    .ToListAsync();
        }

        public Task<TokenModel> FindTokenAsync(Guid id)
        {
            return _context.Tokens
                .Include(t => t.Service)
                    .ThenInclude(s => s.Domain)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddTokenAsync(TokenModel token)
        {
            await _context.Tokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public Task UpdateTokenAsync(TokenModel token)
        {
            _context.Entry(token).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        public Task RemoveTokenAsync(TokenModel token)
        {
            _context.Tokens.Remove(token);
            return _context.SaveChangesAsync();
        }
    }
}
