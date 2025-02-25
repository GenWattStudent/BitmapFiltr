using System.Windows.Media.Imaging;

namespace Filters.Models;

public interface IFilter
{
    public double[,] GenerateFilter(int size);
    public BitmapSource ApplyFilter(BitmapImage source, double[,] filterMatrix);
}
