using Jo2let.Infrastructure.Factory;
using Jo2let.Infrastructure.Repository.Interface;
using Jo2let.Model;

namespace Jo2let.Infrastructure.Repository
{
    public class LocationRepository : RepositoryBase<Location>, ILocationRepository
    {
        public LocationRepository(IDatabaseFactory dbFactory): base(dbFactory)
        {

        }
    }
}
