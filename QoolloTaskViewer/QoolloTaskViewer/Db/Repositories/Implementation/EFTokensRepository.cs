using System;
using System.Collections.Generic;
using System.Linq;
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
            Task<List<TokenModel>> res;
            if (enabledOnly)
                res = _context.Tokens.Where(t => t.UserId == userId && t.Enabled).ToListAsync();
            else
                res = _context.Tokens.Where(t => t.UserId == userId).ToListAsync();
            return res;
        }

        public async Task<TokenModel> FindTokenAsync(Guid id)
        {
            return await _context.Tokens.FindAsync(id);
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
