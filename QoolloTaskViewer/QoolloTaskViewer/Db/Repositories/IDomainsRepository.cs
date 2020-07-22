using System;
using System.Threading.Tasks;
using QoolloTaskViewer.Models;

namespace QoolloTaskViewer.Db.Repositories
{
    public interface IDomainsRepository
    {
        Task<DomainModel> FindDomainByIdAsync(Guid id);
        Task<DomainModel> FindDomainByNameAsync(string domain);
        Task AddDomainAsync(DomainModel domain);
        Task UpdateDomainAsync(DomainModel domain);
        Task RemoveDomainAsync(DomainModel domain);
    }
}
