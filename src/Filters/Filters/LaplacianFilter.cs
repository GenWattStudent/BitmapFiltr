using Filters.Domain.Interfaces;

namespace Filters.Laplacian;

public class LaplacianFilter : IFilter
{
    private readonly Random _random;

    public LaplacianFilter(Random random)
    {
        _random = random;
    }


    public double[,] GenerateFilter(int size)
    {
        if (size % 2 == 0 || size < 3) size = 3;
        double[,] filter = new double[size, size];

        int center = size / 2;

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                filter[y, x] = _random.Next(-2, 2); 
            }
        }

        filter[center, center] = -filter.Cast<double>().Sum() + filter[center, center];

        return filter;
    }

    public byte[] ApplyFilterToPixels(byte[] pixelData, int width, int height, int stride, double[,] filterMatrix)
    {
        byte[] resultData = new byte[pixelData.Length];
        int filterSize = filterMatrix.GetLength(0);
        int offset = filterSize / 2;

        for (int y = offset; y < height - offset; y++)
        {
            for (int x = offset; x < width - offset; x++)
            {
                double sumR = 0, sumG = 0, sumB = 0;
                for (int fy = 0; fy < filterSize; fy++)
                {
                    for (int fx = 0; fx < filterSize; fx++)
                    {
                        int imgX = x + fx - offset;
                        int imgY = y + fy - offset;
                        int pixelIndex = (imgY * stride) + (imgX * 4);
                        double filterValue = filterMatrix[fy, fx];

                        sumB += pixelData[pixelIndex] * filterValue;
                        sumG += pixelData[pixelIndex + 1] * filterValue;
                        sumR += pixelData[pixelIndex + 2] * filterValue;
                    }
                }
                int newPixelIndex = (y * stride) + (x * 4);

                resultData[newPixelIndex] = Clamp(sumB);
                resultData[newPixelIndex + 1] = Clamp(sumG);
                resultData[newPixelIndex + 2] = Clamp(sumR);
                resultData[newPixelIndex + 3] = 255;
            }
        }

        return resultData;
    }

    private byte Clamp(double value)
    {
        return (byte)Math.Max(0, Math.Min(255, value));
    }
}