using System.Data;
using System.Data.Common;

namespace Commons.UnitOfWork.Extensions
{
    public class TransactionContext : ITransactionContext
    {
        protected readonly IConnectionContext connectionContext;
        protected IDbTransaction transaction;

        public TransactionContext(IConnectionContext connectionContext)
        {
            this.connectionContext = connectionContext;
            this.transaction = null!;
        }

        public IDbTransaction GetTransaction()
        {
            return this.transaction;
        }

        public void SetTransaction(IDbTransaction transaction) {
            this.transaction = transaction;
        }
    }
}
