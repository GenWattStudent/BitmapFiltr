using System.Windows.Media.Imaging;

namespace Filters.Integration.Tests;

public abstract class BaseFilterTest
{
    /// <summary>
    /// Creates a predictable random number generator with a fixed seed for testing
    /// </summary>
    protected Random GetFixedRandom() => new Random(1234);

    /// <summary>
    /// Verifies that a filter matrix has the expected dimensions
    /// </summary>
    protected void AssertFilterDimensions(double[,] filterMatrix, int expectedSize)
    {
        Assert.Equal(expectedSize, filterMatrix.GetLength(0));
        Assert.Equal(expectedSize, filterMatrix.GetLength(1));
    }

    /// <summary>
    /// Creates a test bitmap image with the specified dimensions
    /// </summary>
    protected BitmapImage CreateTestImage(int width, int height)
    {
        var bitmap = new WriteableBitmap(
            width,
            height,
            96,
            96,
            System.Windows.Media.PixelFormats.Bgra32,
            null);

        var bitmapImage = new BitmapImage();

        using (var stream = new MemoryStream())
        {
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            encoder.Save(stream);
            stream.Seek(0, SeekOrigin.Begin);
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = stream;
            bitmapImage.EndInit();
        }

        return bitmapImage;
    }
}