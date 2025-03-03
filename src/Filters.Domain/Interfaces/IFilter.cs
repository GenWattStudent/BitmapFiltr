namespace Filters.Domain.Interfaces;

public interface IFilter
{
    double[,] GenerateFilter(int size);
    byte[] ApplyFilterToPixels(byte[] pixelData, int width, int height, int stride, double[,] filterMatrix);
}
