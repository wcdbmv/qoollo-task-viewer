using System;
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

        public Task<ServiceModel> FindServiceByIdAsync(Guid id)
        {
            return _context.Services
                .Include(s => s.Domain)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<ServiceModel> FindServiceByDomainAsync(string domain)
        {
            return _context.Services
                .Include(s => s.Domain)
                .FirstOrDefaultAsync(s => s.Domain.Domain == domain);
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
