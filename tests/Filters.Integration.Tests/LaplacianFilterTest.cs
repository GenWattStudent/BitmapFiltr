using Filters.Integration.Tests;
using Filters.Laplacian;

namespace Filters.Integration.Test;

public class LaplacianFilterTest : BaseFilterTest
{
    [Theory]
    [InlineData(3)]
    [InlineData(5)]
    public void ApplyFilter_PreservesImageDimensions(int filterSize)
    {
        //// Arrange
        //var filter = new LaplacianFilter(GetFixedRandom());
        //var testImage = CreateTestImage(10, 10);
        //var filterMatrix = filter.GenerateFilter(filterSize);

        //// Act
        //var result = filter.ApplyFilterToPixels(testImage, filterMatrix);

        //// Assert
        //Assert.Equal(testImage.PixelWidth, result.PixelWidth);
        //Assert.Equal(testImage.PixelHeight, result.PixelHeight);
    }
}