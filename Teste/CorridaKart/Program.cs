using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CargaLog;


namespace CorridaKart
{
    class Program
    {
        static void Main(string[] args)
        {

            ILeitorLog leitorLog =  new LeitorLog(@"\users\caio\documents\visual studio 2015\Projects\Teste\CorridaKart\Log\CorridaLog.txt");

            var logTabela = leitorLog.ConverterParaDataTable();

            IHelper helper = new Helper();

            var dadosLog = helper.PopularTabelaCorrida(logTabela);

            var resultadoCorrida = helper.GerarDadosCorrida(dadosLog);

            var vencedor = resultadoCorrida.First();


            Console.WriteLine("Segue o resultado da corrida");

            for (int i = 0; i < resultadoCorrida.Count; i++)
            {
                if (vencedor.VoltaRealizadas == resultadoCorrida[i].VoltaRealizadas)
                {
                    if (vencedor.CodigoPiloto == resultadoCorrida[i].CodigoPiloto)
                    {
                        Console.WriteLine(string.Format("Piloto vencedor: {0}. \n Detalhes da corrida:  \n Posição: {1} \n Código Piloto: {2} \n " +
                                                        "Quantidade voltas completadas: {3} \n Tempo Total: {4} \n Velocidade média: {5} \n" +
                                                        " Melhor volta: {6}", vencedor.NomePiloto, vencedor.ClassificacaoCorrida, vencedor.CodigoPiloto,
                                                        vencedor.VoltaRealizadas, vencedor.TempoTotal, vencedor.VelocidadeMediaCorrida, vencedor.TempoMelhorVolta));
                        Console.WriteLine("");
                    }
                    else
                    {
                        Console.WriteLine(string.Format("Piloto: {0}. \n Detalhes da corrida:  \n Posição: {1} \n Diferença do tempo com vencedor: {2} \n Código Piloto: {3} \n " +
                                                         "Quantidade voltas completadas: {4} \n Tempo Total: {5} \n Velocidade média: {6} \n" +
                                                         " Melhor volta: {7}", resultadoCorrida[i].NomePiloto, resultadoCorrida[i].ClassificacaoCorrida, resultadoCorrida[i].DiferencaTempoVencedor, resultadoCorrida[i].CodigoPiloto,
                                                         resultadoCorrida[i].VoltaRealizadas, resultadoCorrida[i].TempoTotal, resultadoCorrida[i].VelocidadeMediaCorrida, resultadoCorrida[i].TempoMelhorVolta));
                        Console.WriteLine("");
                    }
                }
                else
                {
                    Console.WriteLine(string.Format("O piloto: {0} não terminou a corrida. \n Detalhes da corrida:  \n Posição: {1} \n Diferença do tempo com vencedor: {2} \n Código Piloto: {3} \n " +
                                                        "Quantidade voltas completadas: {4} \n Tempo Total: {5} \n Velocidade média: {5} \n" +
                                                        " Melhor volta: {7}", resultadoCorrida[i].NomePiloto, resultadoCorrida[i].ClassificacaoCorrida, resultadoCorrida[i].DiferencaTempoVencedor, resultadoCorrida[i].CodigoPiloto,
                                                        resultadoCorrida[i].VoltaRealizadas, resultadoCorrida[i].TempoTotal, resultadoCorrida[i].VelocidadeMediaCorrida, resultadoCorrida[i].TempoMelhorVolta));
                    Console.WriteLine("");
                }
            }

            var pilotoMelhorVolta = resultadoCorrida.OrderBy(x => x.TempoMelhorVolta).First();
            Console.WriteLine(string.Format("A mellhor volta foi do piloto: {0} \n Volta nº {1} \n Tempo: {2}",
                                                pilotoMelhorVolta.NomePiloto, pilotoMelhorVolta.MelhorVolta,pilotoMelhorVolta.TempoMelhorVolta));


            Console.ReadKey();

        }
    }
}
