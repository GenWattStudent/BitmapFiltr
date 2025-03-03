namespace Filters.Shared;

public class MatrixHelpers
{
    public static double SumMatrix(double[,] matrix)
    {
        double sum = 0;

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                sum += matrix[i, j];
            }
        }

        return sum;
    }

    public static bool HasExpectedSum(double[,] matrix, double expectedSum, double tolerance = 0.0001)
    {
        return Math.Abs(SumMatrix(matrix) - expectedSum) < tolerance;
    }
}
