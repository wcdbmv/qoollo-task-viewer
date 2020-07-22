using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QoolloTaskViewer.Models;

namespace QoolloTaskViewer.Db.Repositories.Implementation
{
    public class EFDomainsRepository : EFBaseRepository, IDomainsRepository
    {
        public EFDomainsRepository(QoolloTaskViewerContext context)
            : base(context)
        {
        }

        public Task<DomainModel> FindDomainAsync(Guid id)
        {
            return _context.Domains.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task AddDomainAsync(DomainModel domain)
        {
            await _context.Domains.AddAsync(domain);
            await _context.SaveChangesAsync();
        }

        public Task UpdateDomainAsync(DomainModel domain)
        {
            _context.Entry(domain).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        public Task RemoveDomainAsync(DomainModel domain)
        {
            _context.Domains.Remove(domain);
            return _context.SaveChangesAsync();
        }
    }
}
