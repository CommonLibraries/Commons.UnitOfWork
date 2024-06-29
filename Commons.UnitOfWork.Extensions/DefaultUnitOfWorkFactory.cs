using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.Common;

namespace Commons.UnitOfWork
{
    internal class DefaultUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IConnectionFactory connectionFactory;

        public DefaultUnitOfWorkFactory(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public IUnitOfWork Create(IsolationLevel isolationLevel)
        {
            if (this.connectionFactory.Open() is not DbConnection connection)
            {
                throw new InvalidCastException($"The connection does not inherit {nameof(DbConnection)} class.");
            }

            var unitOfWork = new DefaultUnitOfWork(connection, isolationLevel);
            return unitOfWork;
        }

        public async Task<IUnitOfWork> CreateAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default)
        {
            if (await this.connectionFactory.OpenAsync(cancellationToken)
                        is not DbConnection connection)
            {
                throw new InvalidCastException($"The connection does not inherit {nameof(DbConnection)} class.");
            }

            var unitOfWork = new DefaultUnitOfWork(connection, isolationLevel);
            return unitOfWork;
        }
    }
}
