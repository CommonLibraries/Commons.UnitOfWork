using System.Data;

namespace Commons.UnitOfWork.Context
{
    public interface IMutableConnectionContext : IConnectionContext
    {
        new IDbConnection Current { get; set; }
    }
}
