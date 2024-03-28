using Microsoft.Extensions.DependencyInjection;

namespace Wasm.Size;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWasmSize(this IServiceCollection services, ISizeConfig config)
    {
        services.AddSingleton<ISizeConfig>(config);
        services.AddScoped<ISizeManager, SizeManager>();
        return services;
    }

    public static IServiceCollection AddWasmSize(this IServiceCollection services)
        => services.AddWasmSize(new SizeConfig());
}