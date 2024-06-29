using System.Data;

namespace Commons.UnitOfWork.Scope
{
    public interface ITransactionScope
    {
        IDbTransaction? Current { get; }
    }
}
