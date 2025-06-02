using Avalonia.Controls;
using AppHotel2.ViewModels;


namespace AppHotel2.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }

}