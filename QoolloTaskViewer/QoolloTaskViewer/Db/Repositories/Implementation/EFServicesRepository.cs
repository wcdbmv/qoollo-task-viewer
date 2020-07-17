using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QoolloTaskViewer.Models;

namespace QoolloTaskViewer.Db.Repositories.Implementation
{
    public class EFServicesRepository : EFBaseRepository, IServicesRepository
    {
        public EFServicesRepository(QoolloTaskViewerContext context)
            : base(context)
        {
        }

        public async Task<ServiceModel> FindServiceAsync(Guid id)
        {
            return await _context.Services.FindAsync(id);
        }

        public async Task AddServiceAsync(ServiceModel service)
        {
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
        }

        public Task UpdateServiceAsync(ServiceModel service)
        {
            _context.Entry(service).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        public Task RemoveServiceAsync(ServiceModel service)
        {
            _context.Services.Remove(service);
            return _context.SaveChangesAsync();
        }
    }
}
