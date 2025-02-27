using BitmapFiltr.ViewModels;
using System.Windows;

namespace BitmapFiltr;

public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel mainWindowViewModel)
    {
        InitializeComponent();
        DataContext = mainWindowViewModel;
    }
}