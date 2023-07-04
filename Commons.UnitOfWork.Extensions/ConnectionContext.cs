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

        public ConnectionContext(string invariantName, string connectionString)
        {
            this.invariantName = invariantName;
            this.connectionString = connectionString;
        }

        private IDbConnection CreateConnection(string invariantName, string connectionString)
        {
            var dbProviderFactory = DbProviderFactories.GetFactory(invariantName);
            var connection = dbProviderFactory.CreateConnection();
            if (connection is null)
            {
                throw new NullReferenceException("Connection is null.");
            }
            connection.ConnectionString = this.connectionString;
            return connection;
        }

        public IDbConnection GetConnection()
        {
            if (this.connection is null)
            {
                this.connection = this.CreateConnection(this.invariantName, this.connectionString);
                this.connection.Open();
            }

            return this.connection;
        }

        public async Task<IDbConnection> GetConnectionAsync(CancellationToken cancellationToken = default)
        {
            if (this.connection is null)
            {
                this.connection = this.CreateConnection(this.invariantName, this.connectionString);
                var connection = this.connection as DbConnection;
                if (connection is null)
                {
                    throw new InvalidCastException($"The created connection does not inherit { nameof(DbConnection) } class.");
                }
                await connection.OpenAsync(cancellationToken);
            }

            return this.connection;
        }
    }
}
