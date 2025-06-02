using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using System.Linq;  // Para usar FirstOrDefault()

namespace AppHotel2.ViewModels
{
    public class Quarto
    {
        public string? Descricao { get; set; }

        // Propriedades para valores diários por adulto e criança
        public double ValorDiariaAdulto { get; set; }
        public double ValorDiariaCrianca { get; set; }
    }

    public class ContratacaoHospedagemViewModel : ReactiveObject
    {
        private int _adultos;
        private int _criancas;
        private Quarto? _suiteSelecionada;
        private DateTime _checkin;
        private DateTime _checkout;
        private DateTime _checkinMax;
        private DateTime _checkoutMin;
        private DateTime _checkoutMax;

        public ObservableCollection<Quarto> Suites { get; } = new ObservableCollection<Quarto>();

        public int Adultos
        {
            get => _adultos;
            set => this.RaiseAndSetIfChanged(ref _adultos, value);
        }

        public int Criancas
        {
            get => _criancas;
            set => this.RaiseAndSetIfChanged(ref _criancas, value);
        }

        public Quarto? SuiteSelecionada
        {
            get => _suiteSelecionada;
            set => this.RaiseAndSetIfChanged(ref _suiteSelecionada, value);
        }

        public DateTime Hoje { get; } = DateTime.Today;

        public DateTime Checkin
        {
            get => _checkin;
            set
            {
                this.RaiseAndSetIfChanged(ref _checkin, value);

                // Atualizar limites do checkout quando Checkin mudar
                CheckoutMin = _checkin.AddDays(1);
                CheckoutMax = _checkin.AddMonths(6);

                if (Checkout < CheckoutMin)
                    Checkout = CheckoutMin;
            }
        }

        public DateTime CheckinMax
        {
            get => _checkinMax;
            set => this.RaiseAndSetIfChanged(ref _checkinMax, value);
        }

        public DateTime CheckoutMin
        {
            get => _checkoutMin;
            set => this.RaiseAndSetIfChanged(ref _checkoutMin, value);
        }

        public DateTime CheckoutMax
        {
            get => _checkoutMax;
            set => this.RaiseAndSetIfChanged(ref _checkoutMax, value);
        }

        public DateTime Checkout
        {
            get => _checkout;
            set => this.RaiseAndSetIfChanged(ref _checkout, value);
        }

        public ReactiveCommand<Unit, Unit> AvancarCommand { get; }

        public ContratacaoHospedagemViewModel()
        {
            Suites.Add(new Quarto { Descricao = "Suíte Standard", ValorDiariaAdulto = 150, ValorDiariaCrianca = 75 });
            Suites.Add(new Quarto { Descricao = "Suíte Luxo", ValorDiariaAdulto = 250, ValorDiariaCrianca = 125 });
            Suites.Add(new Quarto { Descricao = "Suíte Presidencial", ValorDiariaAdulto = 500, ValorDiariaCrianca = 250 });

            Adultos = 1;
            Criancas = 0;

            Checkin = Hoje;
            CheckinMax = Hoje.AddMonths(1);
            CheckoutMin = Checkin.AddDays(1);
            CheckoutMax = Checkin.AddMonths(6);
            Checkout = CheckoutMin;

            SuiteSelecionada = Suites.FirstOrDefault();

            // Define quando o comando Avancar estará habilitado
            var canAvancar = this.WhenAnyValue(
                x => x.SuiteSelecionada,
                x => x.Adultos,
                (suite, adultos) => suite != null && adultos > 0);

            AvancarCommand = ReactiveCommand.CreateFromTask(AvancarAsync, canAvancar);
        }

        private async Task AvancarAsync()
{
var hospedagem = new HospedagemContratadaViewModel
{
    QuartoSelecionado = SuiteSelecionada,
    QntAdultos = Adultos,
    QntCriancas = Criancas,
    DataCheckIn = Checkin,
    DataCheckOut = Checkout,
    Estadia = (Checkout - Checkin).Days,
    ValorTotal = (SuiteSelecionada?.ValorDiariaAdulto * Adultos + SuiteSelecionada.ValorDiariaCrianca * Criancas) * (Checkout - Checkin).Days
};

await Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() =>
{
    var janela = new Views.HospedagemContratadaWindow(hospedagem);
    janela.Show();
});
    }
}
