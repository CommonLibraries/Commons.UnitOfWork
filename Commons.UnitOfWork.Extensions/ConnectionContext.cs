using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.UnitOfWork.Extensions
{
    public class ConnectionContext : IConnectionContext
    {
        private readonly string invariantName;
        private readonly string connectionString;

        private IDbConnection? connection;
        private bool disposed;

        public ConnectionContext(string invariantName, string connectionString)
        {
            this.invariantName = invariantName;
            this.connectionString = connectionString;
        }

        private IDbConnection CreateConnection()
        {
            var dbProviderFactory = DbProviderFactories.GetFactory(this.invariantName);
            var connection = dbProviderFactory.CreateConnection() ?? 
                throw new NullReferenceException("Created connection is null.");
            connection.ConnectionString = this.connectionString;
            return connection;
        }

        public IDbConnection GetConnection()
        {
            if (this.connection is null)
            {
                this.connection = this.CreateConnection();
                this.connection.Open();
            }

            return this.connection;
        }

        public async Task<IDbConnection> GetConnectionAsync(CancellationToken cancellationToken = default)
        {
            if (this.connection is null)
            {
                if (this.CreateConnection() is not DbConnection connection)
                {
                    throw new InvalidCastException($"The created connection does not inherit {nameof(DbConnection)} class.");
                }
                this.connection = connection;
                
                await connection.OpenAsync(cancellationToken);
            }

            return this.connection;
        }

        protected virtual async ValueTask DisposeAsyncCore() {
            if (this.connection is IAsyncDisposable disposable) {
                await disposable.DisposeAsync();
            }

            this.connection = null;
        }

        public async ValueTask DisposeAsync()
        {
            await this.DisposeAsyncCore().ConfigureAwait(false);

            Dispose(disposing: false);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.connection?.Dispose();
                }

                this.connection = null;
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
