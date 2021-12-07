using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Server.Core.Interfaces;
using Server.Storage.Database.Configuration;

namespace Server.Storage.Database.Extensions
{
    public static class ConfigureServicesExtensions
    {
        public static IServiceCollection ConfigureDatabaseUpdateStorage(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IUpdateStorage, DatabaseUpdateStorage>();

            services.Configure<DatabaseStorageConfiguration>(configuration.GetSection("Storage:Database"));

            return services;
        }
    }
}
