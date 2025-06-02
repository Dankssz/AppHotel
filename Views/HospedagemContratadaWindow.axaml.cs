using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AppHotel2.Views
{
    public partial class HospedagemContratadaWindow : Window
    {
        public HospedagemContratadaWindow(object dados)
        {
            InitializeComponent();
            this.DataContext = dados;

            viewModel.FecharJanelaAction = Close
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
