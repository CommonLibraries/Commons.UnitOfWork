using System.Data;
using System.Data.Common;
using Commons.UnitOfWork.Context;

namespace Commons.UnitOfWork
{
    public class DefaultConnectionContext : IMutableConnectionContext
    {
        private IDbConnection? connection = null;
        public DefaultConnectionContext()
        {
            
        }

        public IDbConnection Current
        {
            get => this.connection ?? throw new InvalidOperationException("No connection is set in the current context.");
            set => this.connection =  value;
        }
    }
}
