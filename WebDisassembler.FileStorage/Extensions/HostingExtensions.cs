using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebDisassembler.FileStorage.Impl;
using WebDisassembler.FileStorage.Options;

namespace WebDisassembler.FileStorage.Extensions;

public static class HostingExtensions
{
    public static void AddFileStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<FileStorageOptions>(o =>
        {
            configuration.GetSection("FileStorage")
                .Bind(o);
        });
        services.AddScoped<IFileStorage, BlobStorageFileStorage>();
    }
}