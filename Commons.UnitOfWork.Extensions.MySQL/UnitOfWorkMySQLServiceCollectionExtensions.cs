using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.UnitOfWork.Extensions.MySQL
{
    public static class UnitOfWorkMySQLServiceCollectionExtensions
    {
        public static IServiceCollection AddUnitOfWorkWithMySQL(this IServiceCollection services, string connectionString)
        {
            services.AddUnitOfWork().SetInvariantName("MySqlConnector").SetConnectionString(connectionString).Done();
            return services;
        }
    }
}
