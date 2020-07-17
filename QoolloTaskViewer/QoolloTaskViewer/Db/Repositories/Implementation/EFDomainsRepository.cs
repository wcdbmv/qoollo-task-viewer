using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<DomainModel> FindDomainAsync(Guid id)
        {
            return await _context.Domains.FindAsync(id);
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
