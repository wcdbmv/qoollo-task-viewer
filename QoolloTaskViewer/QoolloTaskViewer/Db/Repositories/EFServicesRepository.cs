using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QoolloTaskViewer.Models;

namespace QoolloTaskViewer.Db.Repositories
{
    public class EFServicesRepository : IServicesRepository
    {
        protected QoolloTaskViewerContext Context;

        public EFServicesRepository(QoolloTaskViewerContext context)
        {
            Context = context;
        }

        public async Task<ServiceModel> FindServiceAsync(Guid id)
        {
            return await Context.Services.FindAsync(id);
        }

        public async Task AddServiceAsync(ServiceModel service)
        {
            await Context.Services.AddAsync(service);
            await Context.SaveChangesAsync();
        }

        public Task UpdateServiceAsync(ServiceModel service)
        {
            Context.Entry(service).State = EntityState.Modified;
            return Context.SaveChangesAsync();
        }

        public Task RemoveServiceAsync(ServiceModel service)
        {
            Context.Services.Remove(service);
            return Context.SaveChangesAsync();
        }
    }
}
