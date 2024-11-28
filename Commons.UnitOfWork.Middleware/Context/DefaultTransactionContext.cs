using System.Data;

namespace Commons.UnitOfWork.Context
{
    internal class DefaultTransactionContext : ITransactionContext
    {
        private readonly IUnitOfWorkContext unitOfWorkContext;
        public DefaultTransactionContext(IUnitOfWorkContext unitOfWorkContext)
        {
            this.unitOfWorkContext = unitOfWorkContext;
        }
        public IDbTransaction? Current => this.unitOfWorkContext.Current.Transaction;
    }
}
