using System.Data;
using Commons.UnitOfWork.Context;

namespace Commons.UnitOfWork.Context
{
    internal class DefaultConnectionScope : IConnectionContext
    {
        private readonly IUnitOfWorkContext scopedUnitOfWork;
        public DefaultConnectionScope(IUnitOfWorkContext scopedUnitOfWork)
        {
            this.scopedUnitOfWork = scopedUnitOfWork;
        }

        public IDbConnection Current => this.scopedUnitOfWork.Current.Connection;
    }
}
