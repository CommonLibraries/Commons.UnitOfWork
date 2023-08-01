using System.Data;

namespace Commons.UnitOfWork
{
    public interface ITransactionContext {
        
        IDbTransaction GetTransaction();

        void SetTransaction(IDbTransaction transaction);
    }
}
