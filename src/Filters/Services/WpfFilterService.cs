using Filters.Domain.Enums;
using Filters.Domain.Interfaces;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Filters.Infrastructure.Services;

public class WpfFilterService : IFilterService
{
    private readonly IFilterFactory _filterFactory;

    public WpfFilterService(IFilterFactory filterFactory)
    {
        _filterFactory = filterFactory;
    }

    public double[,] GenerateFilter(int size, FilterType filterType)
    {
        IFilter filter = _filterFactory.GetFilter(filterType);
        return filter.GenerateFilter(size);
    }

    public BitmapSource ApplyFilter(BitmapImage source, double[,] filterMatrix, FilterType filterType)
    {
        IFilter filter = _filterFactory.GetFilter(filterType);
        int width = source.PixelWidth;
        int height = source.PixelHeight;
        int stride = width * 4;
        byte[] pixelData = new byte[height * stride];

        source.CopyPixels(pixelData, stride, 0);

        var resultData = filter.ApplyFilterToPixels(pixelData, width, height, stride, filterMatrix);

        return BitmapSource.Create(width, height, source.DpiX, source.DpiY, PixelFormats.Bgra32, null, resultData, stride);
    }
}
