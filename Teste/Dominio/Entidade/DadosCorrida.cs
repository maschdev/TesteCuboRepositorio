using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidade
{
    public class DadosCorrida 
    {
        public DadosCorrida(
            int codigoPiloto, 
            string nomePiloto, 
            int voltaRealizadas, 
            TimeSpan tempoTotal, 
            TimeSpan tempoMelhorVolta, 
            int melhorVolta, 
            double velocidadeMediaCorrida)
        {
            this.CodigoPiloto = codigoPiloto;
            this.NomePiloto = nomePiloto;
            this.VoltaRealizadas = voltaRealizadas;
            this.TempoTotal = tempoTotal;
            this.TempoMelhorVolta = tempoMelhorVolta;
            this.MelhorVolta = melhorVolta;
            this.VelocidadeMediaCorrida = velocidadeMediaCorrida;
        }

        public int CodigoPiloto { get; private set; }
        public string NomePiloto { get; private set; }
        public int VoltaRealizadas { get; private set; }
        public TimeSpan TempoTotal { get; private set; }
        public TimeSpan TempoMelhorVolta { get; private set; }
        public int MelhorVolta { get; private set; }
        public TimeSpan DiferencaTempoVencedor { get; private set; }
        public double VelocidadeMediaCorrida { get; private set; }
        public int ClassificacaoCorrida{ get; private set; }

        public void AlterarTempoPilotoAposVencedor(TimeSpan tempo)
        {
            this.DiferencaTempoVencedor = tempo;
        }

        public void AlterarClassificacaoCorrida(int posicao)
        {
            this.ClassificacaoCorrida = posicao;
        }
    }
}
