using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.UnitOfWork.Extensions.MySQL
{
    public static class UnitOfWorkMySQLServiceCollectionExtensions
    {
        public static IServiceCollection AddUnitOfWorkWithMySQL(this IServiceCollection services, string connectionString)
        {
            DbProviderFactories.RegisterFactory("MySqlConnector", MySqlConnector.MySqlConnectorFactory.Instance);
            services.AddUnitOfWork()
                .SetInvariantName("MySqlConnector")
                .SetConnectionString(connectionString)
                .Done();

            return services;
        }
    }
}
