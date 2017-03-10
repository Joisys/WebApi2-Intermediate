using Jo2let.Data;
using Jo2let.Infrastructure.Factory;

namespace Jo2let.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PropertyDbContext _dbContext;
        private readonly IDatabaseFactory _dbFactory;
        protected PropertyDbContext DbContext
        {
            get
            {
                return _dbContext ?? _dbFactory.Get();
            }
        }

        public UnitOfWork(IDatabaseFactory dbFactory, PropertyDbContext dbContext)
        {
            this._dbFactory = dbFactory;
            this._dbContext = dbContext;
        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }
    }
}
