using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QoolloTaskViewer.Models;

namespace QoolloTaskViewer.Db.Repositories
{
    public interface ITokensRepository
    {
        Task<List<TokenModel>> GetTokensAsync(string userId, bool enabledOnly = true);
        Task<TokenModel> FindTokenAsync(Guid id);
        Task AddTokenAsync(TokenModel token);
        Task UpdateTokenAsync(TokenModel token);
        Task RemoveTokenAsync(TokenModel token);
    }
}
