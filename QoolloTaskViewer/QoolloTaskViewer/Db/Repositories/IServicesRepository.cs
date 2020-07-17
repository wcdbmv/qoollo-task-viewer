using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QoolloTaskViewer.Models;

namespace QoolloTaskViewer.Db.Repositories
{
    public interface IServicesRepository
    {
        Task<ServiceModel> FindServiceAsync(Guid id);
        Task AddServiceAsync(ServiceModel service);
        Task UpdateServiceAsync(ServiceModel service);
        Task RemoveServiceAsync(ServiceModel service);
    }
}
