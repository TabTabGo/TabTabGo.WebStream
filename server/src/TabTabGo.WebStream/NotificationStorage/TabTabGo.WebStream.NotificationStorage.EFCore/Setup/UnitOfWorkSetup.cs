using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TabTabGo.Core.Data;

namespace TabTabGo.WebStream.NotificationStorage.EFCore
{
    public static class UnitOfWorkSetup
    {
        public static IServiceCollection UseUnitOfWork(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IUnitOfWork, TabTabGo.WebStream.NotificationStorage.EFCore.UnitOfWork>();

            return services;
        }
    }
}
