using System;
using System.Threading.Tasks;
using QoolloTaskViewer.Models;

namespace QoolloTaskViewer.Db.Repositories
{
    public interface IDomainsRepository
    {
        Task<DomainModel> FindDomainAsync(Guid id);
        Task AddDomainAsync(DomainModel domain);
        Task UpdateDomainAsync(DomainModel domain);
        Task RemoveDomainAsync(DomainModel domain);
    }
}
