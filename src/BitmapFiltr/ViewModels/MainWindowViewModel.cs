using BitmapFiltr.Commands;
using BitmapFiltr.Models;
using Filters.Domain.Enums;
using Filters.Domain.Interfaces;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace BitmapFiltr.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private BitmapImage _imageBitmap = null!;
    private BitmapSource _newImageBitmap = null!;
    private int _filterSize = 5; 
    private Observable2DCollection<double> _filterMatrix = new ();
    private double _matrixValue;

    private ICommand _applyFilterCommand;
    private ICommand _randomMatrixCommand;

    private IFilterService _filterService;
    private string _uri = "pack://application:,,,/Filters.UI;component/Assets/DevToolsTodo.png";

    public BitmapImage ImageBitmap
    {
        get => _imageBitmap;
        set => SetProperty(ref _imageBitmap, value);
    }

    public Observable2DCollection<double> FilterMatrix
    {
        get => _filterMatrix;
        set => SetProperty(ref _filterMatrix, value);
    }

    public int FilterSize
    {
        get => _filterSize;
        set => SetProperty(ref _filterSize, value);
    }

    public BitmapSource NewImageBitmap
    {
        get => _newImageBitmap;
        set => SetProperty(ref _newImageBitmap, value);
    }

    public double MatrixValue
    {
        get => _matrixValue;
        set => SetProperty(ref _matrixValue, value);
    }

    public ICommand ApplyFilterCommand => _applyFilterCommand;
    public ICommand RandomMatrixCommand =>  _randomMatrixCommand;

    public MainWindowViewModel(IFilterService laplacianFilter)
    {
        var uri = new Uri(_uri, UriKind.Absolute);
        var image = new BitmapImage(uri);

        ImageBitmap = image;

        _applyFilterCommand = new RelayCommand(ApplyFilter);
        _randomMatrixCommand = new RelayCommand(UpdateMatrix);
        _filterService = laplacianFilter;

        UpdateMatrix();
    }

    private void UpdateMatrix()
    {
        FilterMatrix.FromArray(_filterService.GenerateFilter(_filterSize, FilterType.Laplacian));
    }

    private void ApplyFilter()
    {
        NewImageBitmap = _filterService.ApplyFilter(_imageBitmap, _filterMatrix.ToArray(), FilterType.Laplacian);
    }
}
