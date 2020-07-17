using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QoolloTaskViewer.Db.Repositories.Implementation
{
    public class EFBaseRepository
    {
        protected QoolloTaskViewerContext _context;

        public EFBaseRepository(QoolloTaskViewerContext context)
        {
            _context = context;
        }
    }
}
