using System.Data;
using Commons.UnitOfWork.Context;

namespace Commons.UnitOfWork
{
    public class DefaultTransactionContext : IMutableTransactionContext
    {
        private IDbTransaction? transaction = null;
        public DefaultTransactionContext()
        {
            
        }

        public IDbTransaction? Current
        {
            get => this.transaction;
            set => this.transaction = value;
        }
    }
}
