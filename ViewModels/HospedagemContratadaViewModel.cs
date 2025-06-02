using ReactiveUI;
using System;

namespace AppHotel2.ViewModels
{
    public class HospedagemContratadaViewModel : ReactiveObject
    {
        public Quarto? QuartoSelecionado { get; set; }
        public int QntAdultos { get; set; }
        public int QntCriancas { get; set; }
        public DateTime DataCheckIn { get; set; }
        public DateTime DataCheckOut { get; set; }
        public int Estadia { get; set; }
        public double ValorTotal { get; set; }

        public ReactiveCommand<Unit, Unit> VoltarCommand { get; }

        public Action? FecharJanelaAction { get; set; }

        public HospedagemContratadaViewModel()
        {
            VoltarCommand = ReactiveCommand.Create(() =>
            {
                FecharJanelaAction?.Invoke(); // Chama a ação para fechar
            });
        }
    }
}
