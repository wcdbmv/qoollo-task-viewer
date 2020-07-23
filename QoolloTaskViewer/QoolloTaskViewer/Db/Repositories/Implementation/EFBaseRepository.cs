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
