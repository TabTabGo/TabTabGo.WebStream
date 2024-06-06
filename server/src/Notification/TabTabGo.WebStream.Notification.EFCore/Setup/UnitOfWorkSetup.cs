using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TabTabGo.Core.Data;

namespace TabTabGo.WebStream.Notification.EFCore
{
    public static class UnitOfWorkSetup
    {
        public static IServiceCollection UseUnitOfWork(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IUnitOfWork, TabTabGo.WebStream.Notification.EFCore.UnitOfWork>();

            return services;
        }
    }
}
