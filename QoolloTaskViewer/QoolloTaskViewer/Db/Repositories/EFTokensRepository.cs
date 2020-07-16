﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QoolloTaskViewer.Models;

namespace QoolloTaskViewer.Db.Repositories
{
    public class EFTokensRepository : ITokensRepository
    {
        protected QoolloTaskViewerContext Context;

        public EFTokensRepository(QoolloTaskViewerContext context)
        {
            Context = context;
        }

        public async Task<List<TokenModel>> GetTokensAsync(Guid userId, bool enabledOnly = true)
        {
            Func<TokenModel, bool> predicate = t => t.UserId.Equals(userId);
            if (enabledOnly)
            {
                predicate = t => t.UserId.Equals(userId) && t.Enabled;
            }

            return await Context.Tokens.Where(predicate).ToListAsync(); // BUILD FAIL
        }

        public async Task<TokenModel> FindTokenAsync(Guid id)
        {
            return await Context.Tokens.FindAsync(id);
        }

        public async Task AddTokenAsync(TokenModel token)
        {
            await Context.Tokens.AddAsync(token);
            await Context.SaveChangesAsync();
        }

        public Task UpdateTokenAsync(TokenModel token)
        {
            Context.Entry(token).State = EntityState.Modified;
            return Context.SaveChangesAsync();
        }

        public Task RemoveTokenAsync(TokenModel token)
        {
            Context.Tokens.Remove(token);
            return Context.SaveChangesAsync();
        }
    }
}