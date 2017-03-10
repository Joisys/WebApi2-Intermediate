using Jo2let.Infrastructure.Factory;
using Jo2let.Infrastructure.Repository.Interface;
using Jo2let.Model;

namespace Jo2let.Infrastructure.Repository
{
    public class PropertyRepository : RepositoryBase<Property>, IPropertyRepository
    {
        public PropertyRepository(IDatabaseFactory dbFactory): base(dbFactory)
        {

        }
    }
}
