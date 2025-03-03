using Filters.Domain.Interfaces;
using Filters.Infrastructure.Factories;
using Filters.Infrastructure.Services;
using Filters.Laplacian;
using Microsoft.Extensions.DependencyInjection;

namespace Filters.Infrastructure;

public static class InfrastucureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<LaplacianFilter>();
        services.AddSingleton<IFilterService, WpfFilterService>();

        services.AddSingleton<IFilterFactory, FilterFactory>();

        return services; 
    }
}
