using System.Data;

namespace Commons.UnitOfWork.Context
{
    public interface ITransactionContext
    {
        IDbTransaction? Current { get;  }
    }
}
