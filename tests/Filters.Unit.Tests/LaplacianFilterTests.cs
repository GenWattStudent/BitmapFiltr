using Filters.Laplacian;
using Filters.Shared;

namespace Filters.Unit.Tests;

public class LaplacianFilterTests
{
    [Theory]
    [InlineData(3, 3)]
    [InlineData(4, 3)]
    public void GenerateFilter_Returns_CorrectSizeAndZeroSum(int inputSize, int expectedSize)
    {
        // Arrange
        var fixedRandom = new Random(1234);
        var filter = new LaplacianFilter(fixedRandom);

        // Act
        double[,] matrix = filter.GenerateFilter(inputSize);
        double sum = MatrixHelpers.SumMatrix(matrix);

        // Assert
        Assert.Equal(expectedSize, matrix.GetLength(0));
        Assert.Equal(expectedSize, matrix.GetLength(1));
        Assert.Equal(0, sum);
    }
}