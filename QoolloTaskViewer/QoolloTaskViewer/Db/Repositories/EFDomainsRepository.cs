using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QoolloTaskViewer.Models;

namespace QoolloTaskViewer.Db.Repositories
{
    public class EFDomainsRepository : IDomainsRepository
    {
        protected QoolloTaskViewerContext Context;

        public EFDomainsRepository(QoolloTaskViewerContext context)
        {
            Context = context;
        }

        public async Task<DomainModel> FindDomainAsync(Guid id)
        {
            return await Context.Domains.FindAsync(id);
        }

        public async Task AddDomainAsync(DomainModel domain)
        {
            await Context.Domains.AddAsync(domain);
            await Context.SaveChangesAsync();
        }

        public Task UpdateDomainAsync(DomainModel domain)
        {
            Context.Entry(domain).State = EntityState.Modified;
            return Context.SaveChangesAsync();
        }

        public Task RemoveDomainAsync(DomainModel domain)
        {
            Context.Domains.Remove(domain);
            return Context.SaveChangesAsync();
        }
    }
}
