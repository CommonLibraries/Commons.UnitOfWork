using System.Data;
using Commons.UnitOfWork.Context;

namespace Commons.UnitOfWork.Context
{
    internal class DefaultTransactionScope : ITransactionContext
    {
        private readonly IUnitOfWorkContext scopedUnitOfWork;
        public DefaultTransactionScope(IUnitOfWorkContext scopedUnitOfWork)
        {
            this.scopedUnitOfWork = scopedUnitOfWork;
        }
        public IDbTransaction? Current => this.scopedUnitOfWork.Current.Transaction;
    }
}
