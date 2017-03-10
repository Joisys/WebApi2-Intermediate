using System;
using Jo2let.Data;

namespace Jo2let.Infrastructure.Factory 
{
    public interface IDatabaseFactory : IDisposable
    {
        PropertyDbContext Get();
    }
}
