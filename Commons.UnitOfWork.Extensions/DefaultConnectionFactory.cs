using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.UnitOfWork.Extensions
{
    internal class DefaultConnectionFactory : IConnectionFactory
    {
        private readonly string invariantName;
        private readonly string connectionString;

        private IDbConnection? connection;

        public DefaultConnectionFactory(string invariantName, string connectionString)
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

        public IDbConnection Open()
        {
            if (this.connection is null)
            {
                this.connection = this.CreateConnection();
                this.connection.Open();
            }

            return this.connection;
        }

        public async Task<IDbConnection> OpenAsync(CancellationToken cancellationToken = default)
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
    }
}
