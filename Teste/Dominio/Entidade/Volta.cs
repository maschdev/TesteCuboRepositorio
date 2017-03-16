using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validacao;

namespace Dominio.Entidade
{
    public class Volta
    {

        public Volta(int numeroVolta, TimeSpan tempoVolta, DateTime hora, double velocidadeMedia, int numeroPiloto)
        {
            LogValidacao.NumeroVazio(numeroVolta, "Número da volta inválido");
            LogValidacao.VoltaVazia(numeroVolta, "A primeia volta não foi informada");
            LogValidacao.TempoVoltaInvalida(tempoVolta, "Tempo da volta inválido");
            LogValidacao.HoraInvalida(hora, "Hora inválido");
            LogValidacao.NumeroVazio(NumeroPiloto, "Número do piloto inválido");
            LogValidacao.VelocidadeInvalida(velocidadeMedia, "Piloto ainda não completou nenhuma volta");

            this.NumeroVolta = numeroVolta;
            this.Tempo = tempoVolta;
            this.Hora = hora;
            this.VelocidadeMedia = velocidadeMedia;
            this.NumeroPiloto = numeroPiloto;
        }

        public int NumeroVolta { get; set; }

        public TimeSpan Tempo { get; set; }

        public DateTime Hora { get; set; }

        public double VelocidadeMedia { get; set; }

        public int NumeroPiloto { get; set; }

    }
}
