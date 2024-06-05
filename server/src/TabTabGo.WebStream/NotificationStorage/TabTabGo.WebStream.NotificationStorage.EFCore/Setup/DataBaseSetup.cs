using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabTabGo.WebStream.NotificationStorage.EFCore.Enums;

namespace TabTabGo.WebStream.NotificationStorage.EFCore
{
    public static class DataBaseSetup
    {
        public static IServiceCollection DatabaseFactory(this IServiceCollection services, IConfiguration configuration, DataBaseType dataBaseType = DataBaseType.InMemory)
        {

            services.AddScoped<DbContext>(provider => provider.GetService<NotificationDbContext>());

            if (dataBaseType == DataBaseType.InMemory)
            {
                services.AddDbContext<NotificationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("MemoryDb");
                    options.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));

                }, ServiceLifetime.Scoped, ServiceLifetime.Scoped);
            }
            else
            {
                services.AddDbContext<NotificationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("AppDbContext")));
            }

            return services;
        }
    }
}
