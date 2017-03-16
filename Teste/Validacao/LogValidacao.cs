using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validacao
{
    public static class LogValidacao
    {
      
        public static void CaractereVazio(string campo, string mensagemErro)
        {
            if (string.IsNullOrWhiteSpace(campo))
                throw new Exception(mensagemErro);
        }

      
        public static void NumeroVazio(int numero, string mensagemErro)
        {
            if (numero < 0)
                throw new Exception(mensagemErro);
        }

        public static void VoltaVazia(int volta, string mensagemErro)
        {
            if (volta <= 0)
                throw new Exception(mensagemErro);
        }

       
        public static void HoraInvalida(DateTime data, string mensagemErro)
        {
            if (data.Equals(DateTime.MinValue))
                throw new Exception(mensagemErro);
        }

      
        public static void TempoVoltaInvalida(TimeSpan data, string mensagemErro)
        {
            if (data.Equals(TimeSpan.MinValue))
                throw new Exception(mensagemErro);
        }

      
        public static void VelocidadeInvalida(double velocidade, string mensagemErro)
        {
            if (velocidade < 0)
                throw new Exception(mensagemErro);
        }

       
        public static void ValidarCaminhoArquivo(string caminho, string mensagemErro)
        {
            if (!System.IO.File.Exists(caminho))
                throw new Exception(mensagemErro);
        }

        
    }
}
