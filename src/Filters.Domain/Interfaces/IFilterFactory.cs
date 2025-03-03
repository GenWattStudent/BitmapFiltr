using Filters.Domain.Enums;

namespace Filters.Domain.Interfaces;

public interface IFilterFactory
{
    IFilter GetFilter(FilterType filterType);
}
