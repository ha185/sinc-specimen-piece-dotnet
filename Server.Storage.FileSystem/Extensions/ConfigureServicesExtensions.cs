using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Server.Core.Interfaces;
using Server.Storage.FileSystem.Configuration;

namespace Server.Storage.FileSystem.Extensions
{
    public static class ConfigureServicesExtensions
    {
        public static IServiceCollection ConfigureFilesystemUpdateStorage(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IUpdateStorage, FileSystemUpdateStorage>();

            services.Configure<FileStorageConfiguration>(configuration.GetSection("Storage:File"));

            return services;
        }
    }
}
