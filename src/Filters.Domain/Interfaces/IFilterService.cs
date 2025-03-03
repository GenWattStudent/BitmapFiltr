using Filters.Domain.Enums;
using System.Windows.Media.Imaging;

namespace Filters.Domain.Interfaces;

public interface IFilterService
{
    double[,] GenerateFilter(int size, FilterType filterType);
    BitmapSource ApplyFilter(BitmapImage source, double[,] filterMatrix, FilterType filterType);
}
