using System.ComponentModel;
using System.Windows.Input;

namespace AppHotel2.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private int _contador;
        private string _textoBotao = "Click me";
        public string TextoBotao
        {
        get => _textoBotao;
        set => SetProperty(ref _textoBotao, value);
     }


        public ICommand ContarCommand { get; }

        public MainWindowViewModel()
        {
            ContarCommand = new RelayCommand(Contar);
        }

        private void Contar()
        {
            _contador++;
            TextoBotao = _contador == 1 ? $"Clicked {_contador} time" : $"Clicked {_contador} times";
        }
    }
}
