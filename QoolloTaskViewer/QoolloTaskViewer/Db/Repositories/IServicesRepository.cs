using System;
using System.Threading.Tasks;
using QoolloTaskViewer.Models;

namespace QoolloTaskViewer.Db.Repositories
{
    public interface IServicesRepository
    {
        Task<ServiceModel> FindServiceByIdAsync(Guid id);
        Task<ServiceModel> FindServiceByDomainAsync(string domain);
        Task AddServiceAsync(ServiceModel service);
        Task UpdateServiceAsync(ServiceModel service);
        Task RemoveServiceAsync(ServiceModel service);
    }
}
