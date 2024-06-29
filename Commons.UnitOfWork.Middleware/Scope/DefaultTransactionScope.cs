using System.Data;
using Commons.UnitOfWork.Scope;

namespace Commons.UnitOfWork.Scope
{
    internal class DefaultTransactionScope : ITransactionScope
    {
        private readonly IUnitOfWorkScope scopedUnitOfWork;
        public DefaultTransactionScope(IUnitOfWorkScope scopedUnitOfWork)
        {
            this.scopedUnitOfWork = scopedUnitOfWork;
        }
        public IDbTransaction? Current => this.scopedUnitOfWork.Current.Transaction;
    }
}
