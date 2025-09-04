using System.Data;

namespace Commons.UnitOfWork.Context
{
    public class DefaultTransactionContext : ITransactionContext
    {
        private readonly IUnitOfWorkContext unitOfWorkContext;
        public DefaultTransactionContext(IUnitOfWorkContext unitOfWorkContext)
        {
            this.unitOfWorkContext = unitOfWorkContext;
        }
        public IDbTransaction? Current => this.unitOfWorkContext.Current.Transaction;
    }
}
