using Jo2let.Data;

namespace Jo2let.Infrastructure.Factory 
{
public class DatabaseFactory : Disposable, IDatabaseFactory
{
    private PropertyDbContext _dataContext;
    public PropertyDbContext Get()
    {
        return _dataContext ?? (_dataContext = new PropertyDbContext());
    }
    protected override void DisposeCore()
    {
        _dataContext?.Dispose();
    }
}
}
