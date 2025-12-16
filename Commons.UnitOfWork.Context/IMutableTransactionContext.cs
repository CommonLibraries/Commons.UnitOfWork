using System.Data;

namespace Commons.UnitOfWork.Context
{
    public interface IMutableTransactionContext : ITransactionContext
    {
        new IDbTransaction? Current { get; set; }
    }
}
