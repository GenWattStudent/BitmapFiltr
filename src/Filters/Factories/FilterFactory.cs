using Filters.Domain.Enums;
using Filters.Domain.Interfaces;
using Filters.Laplacian;
using Microsoft.Extensions.DependencyInjection;

namespace Filters.Infrastructure.Factories;

public class FilterFactory : IFilterFactory
{
    private readonly IServiceProvider _serviceProvider;

    public FilterFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IFilter GetFilter(FilterType filterType)
    {
        return filterType switch
        {
            FilterType.Laplacian => _serviceProvider.GetRequiredService<LaplacianFilter>(),
            // Add other filters here
            _ => throw new ArgumentException($"Unknown filter type: {filterType}")
        };
    }
}