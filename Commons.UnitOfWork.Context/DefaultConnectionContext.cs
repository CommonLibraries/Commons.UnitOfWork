using System.Data;

namespace Commons.UnitOfWork.Context
{
    public class DefaultConnectionContext : IConnectionContext
    {
        private readonly IUnitOfWorkContext unitOfWorkContext;
        public DefaultConnectionContext(IUnitOfWorkContext unitOfWorkContext)
        {
            this.unitOfWorkContext = unitOfWorkContext;
        }

        public IDbConnection Current => this.unitOfWorkContext.Current.Connection;
    }
}
