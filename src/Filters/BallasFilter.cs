using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using Filters.Models;

namespace BalasFilter;

public class BallasFilter : IFilter
{
    private readonly Random _random = new Random();

    public double[,] GenerateFilter(int size)
    {
        double[,] filter = new double[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                filter[i, j] = _random.Next(-10, 10);
            }
        }

        return filter;
    }

    public BitmapSource ApplyFilter(BitmapImage source, double[,] filterMatrix)
    {
        WriteableBitmap writableSource = new WriteableBitmap(source);

        int width = writableSource.PixelWidth;
        int height = writableSource.PixelHeight;
        int stride = writableSource.BackBufferStride;
        int bytesPerPixel = (writableSource.Format.BitsPerPixel + 7) / 8;

        byte[] originalPixels = new byte[height * stride];
        writableSource.CopyPixels(originalPixels, stride, 0);

        byte[] resultPixels = new byte[height * stride];

        int kernelSize = 3;
        int radius = kernelSize / 2;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                double blue = 0, green = 0, red = 0;

                for (int i = -radius; i <= radius; i++)
                {
                    for (int j = -radius; j <= radius; j++)
                    {
                        int nx = x + i;
                        int ny = y + j;

                        if (nx < 0) nx = 0;
                        if (nx >= width) nx = width - 1;
                        if (ny < 0) ny = 0;
                        if (ny >= height) ny = height - 1;

                        int index = ny * stride + nx * bytesPerPixel;
                        double kernelValue = filterMatrix[j + radius, i + radius];

                        blue += originalPixels[index] * kernelValue;
                        green += originalPixels[index + 1] * kernelValue;
                        red += originalPixels[index + 2] * kernelValue;
                    }
                }

                byte b = (byte)Math.Clamp(blue, 0, 255);
                byte g = (byte)Math.Clamp(green, 0, 255);
                byte r = (byte)Math.Clamp(red, 0, 255);

                int resultIndex = y * stride + x * bytesPerPixel;
                resultPixels[resultIndex] = b;
                resultPixels[resultIndex + 1] = g;
                resultPixels[resultIndex + 2] = r;
                resultPixels[resultIndex + 3] = originalPixels[resultIndex + 3];
            }
        }

        WriteableBitmap resultBitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgra32, null);
        resultBitmap.WritePixels(new Int32Rect(0, 0, width, height), resultPixels, stride, 0);

        return resultBitmap;
    }
}
