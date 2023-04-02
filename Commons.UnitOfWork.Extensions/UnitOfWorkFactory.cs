using System.Data.Common;

namespace Commons.UnitOfWork
{

    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        DbProviderFactory dbProviderFactory;
        string invariantName;
        string connectionString;

        public UnitOfWorkFactory(string invariantName, string ConnectionString)
        {
            this.invariantName = invariantName;
            this.connectionString = ConnectionString;
            this.dbProviderFactory = DbProviderFactories.GetFactory(this.invariantName);
        }

        public IUnitOfWork Create()
        {
            var connection = this.dbProviderFactory.CreateConnection();
            connection!.ConnectionString = this.connectionString;
            connection.Open();
            
            var unitOfWork = new UnitOfWork(connection!);
            return unitOfWork;
        }

        public async Task<IUnitOfWork> CreateAsync(CancellationToken cancellationToken = default)
        {
            var connection = this.dbProviderFactory.CreateConnection();
            connection!.ConnectionString = this.connectionString;
            await connection.OpenAsync(cancellationToken);
            
            var unitOfWork = new UnitOfWork(connection!);
            return unitOfWork;
        }
    }
}