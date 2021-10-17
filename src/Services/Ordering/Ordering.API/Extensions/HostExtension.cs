using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ordering.API.Extensions
{
    public static class HostExtension
    {
        /// <summary>
        ///  this extension method just for show case only ,
        /// I do not recommend to use it on production Scenario. 
        /// Currently, many developers migrate their databases at application startup time. This is easy but is not recommended because:
        /// Multiple threads/processes/servers may attempt to migrate the database concurrently
        ///Applications may try to access inconsistent state while this is happening
        /// Usually the database permissions to modify the schema should not be granted for application execution
        /// It's hard to revert back to a clean state if something goes wrong
        /// thank to dotnet team , they provide us a better way of doing migration.
        /// please refer to :https://docs.microsoft.com/en-us/ef/core/what-is-new/ef-core-5.0/plan on Migrations and deployment experience
        /// and migration bundle on :https://github.com/dotnet/efcore/issues/19693
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="host"></param>
        /// <param name="seeder"></param>
        /// <param name="retry"></param>
        /// <returns></returns>
        public static IHost MigrationDataBase<TContext>(
            this IHost host,
            Action<TContext, IServiceProvider> seeder,
            int? retry = 0) where TContext : DbContext
        {
            using var scope = host.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var logger = serviceProvider.GetRequiredService<ILogger<TContext>>();
            var context = serviceProvider.GetService<TContext>();
            try
            {
                logger.LogInformation("Migrating database ");
                InvokeSeeder(seeder,context,serviceProvider);
                logger.LogInformation("database Migrated");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex,"An error occurred while migrating the database");
                if (retry < 50)
                {
                    System.Threading.Thread.Sleep(1000);
                    retry++;
                    MigrationDataBase<TContext>(host, seeder, retry);
                }
                throw;
            }

            return host;
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder,
            TContext context, IServiceProvider serviceProvider) where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, serviceProvider);
        }
    }
}
