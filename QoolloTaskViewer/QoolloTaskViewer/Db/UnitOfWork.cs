using QoolloTaskViewer.Db.Repositories;
using QoolloTaskViewer.Db.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QoolloTaskViewer.Db
{
    public class UnitOfWork
    {
        private QoolloTaskViewerContext _context;
        public IUsersRepository UsersRepository { get; }
        public IDomainsRepository DomainsRepository { get; }
        public IServicesRepository ServicesRepository { get; }
        public ITokensRepository TokensRepository { get; }

        public UnitOfWork(QoolloTaskViewerContext context)
        {
            _context = context;
            UsersRepository = new EFUsersRepository(_context);
            DomainsRepository = new EFDomainsRepository(_context);
            ServicesRepository = new EFServicesRepository(_context);
            TokensRepository = new EFTokensRepository(_context);
        }
    }
}
