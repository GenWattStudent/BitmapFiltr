using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BitmapFiltr.Models;

public class Observable2DCollection<T> : ObservableCollection<ObservableCollection<T>>, INotifyPropertyChanged 
{
    public Observable2DCollection() { }

    public Observable2DCollection(T[,] array)
    {
        FromArray(array);
    }

    public void FromArray(T[,] array)
    {
        Clear();
        int rows = array.GetLength(0);
        int cols = array.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            var row = new ObservableCollection<T>();
            for (int j = 0; j < cols; j++)
            {
                row.Add(array[i, j]);
            }
            Add(row);
        }
    }

    public T[,] ToArray()
    {
        int rows = Count;
        int cols = this[0].Count;
        T[,] array = new T[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                array[i, j] = this[i][j];
            }
        }

        return array;
    }

    public new event PropertyChangedEventHandler? PropertyChanged;

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        PropertyChanged?.Invoke(this, e);
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
