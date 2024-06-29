using System.Data;
using Commons.UnitOfWork.Scope;

namespace Commons.UnitOfWork.Scope
{
    internal class DefaultConnectionScope : IConnectionScope
    {
        private readonly IUnitOfWorkScope scopedUnitOfWork;
        public DefaultConnectionScope(IUnitOfWorkScope scopedUnitOfWork)
        {
            this.scopedUnitOfWork = scopedUnitOfWork;
        }

        public IDbConnection Current => this.scopedUnitOfWork.Current.Connection;
    }
}
