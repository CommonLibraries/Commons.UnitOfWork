using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.Common;

namespace Commons.UnitOfWork
{
    public class UnitOfWorkContext : IUnitOfWorkContext
    {
        private IUnitOfWork? unitOfWork;

        private IConnectionContext connectionContext;

        public UnitOfWorkContext(IConnectionContext connectionContext)
        {
            this.connectionContext = connectionContext;
        }

        public IUnitOfWork Create()
        {
            if (this.unitOfWork is not null && !this.unitOfWork.IsDisposed)
            {
                throw new InvalidOperationException("There is an existing Unit of Work.");
            }

            var unitOfWork = new UnitOfWork();
            return unitOfWork;
        }

        public async Task<IUnitOfWork> CreateAsync(CancellationToken cancellationToken = default)
        {
            this.connection = this.dbProviderFactory.CreateConnection();
            this.connection!.ConnectionString = this.connectionString;
            
            if (this.connection as DbConnection is null)
            {
                throw new NotSupportedException("");
            }            
            await (this.connection as DbConnection)!.OpenAsync(cancellationToken);
            
            var unitOfWork = new UnitOfWork(this.connection!);
            return unitOfWork;
        }
    }
}