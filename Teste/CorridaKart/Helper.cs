using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading;
using Dominio.Entidade;

namespace CorridaKart
{
    public class Helper : IHelper
    {

        #region Método Públicos

        /// <summary>
        /// Organiza as voltas de cada piloto
        /// </summary>
        /// <param name="log">Tabela do arquivo do log</param>
        /// <returns></returns>
        public List<Piloto> PopularTabelaCorrida(DataTable log)
        {
            //Populando os pilotos e realizado distinct através do número do piloto.
            List<Piloto> pilotos = PreencherPilotos(log);

            //Populando  as voltas.
            List<Volta> voltas = PreencherVoltaPilotos(log);

            // Atribuição a volta de cada piloto, através do número do piloto.
            foreach (var piloto in pilotos)
            {
                var voltaPiloto = voltas.Where(volta => volta.NumeroPiloto == piloto.Numero).ToList();

                piloto.AddVoltas(voltaPiloto);
            }


            return pilotos;
        }


        /// <summary>
        /// Tratar e padronizar a lista dos pilotos da corrida
        /// </summary>
        /// <param name="dadosPilotos"> Lista dos pilotos</param>
        /// <returns>Lista final da corrida</returns>
        public List<DadosCorrida> GerarDadosCorrida(List<Piloto> dadosPilotos)
        {
            // var dadosCorrida = new List<DadosCorrida>();
            var retorno = new List<DadosCorrida>();
            var dadosVoltas = new List<Volta>();

            //Adicionando as voltas dos pitolos na lista dos pilotos para fazer o join
            dadosPilotos.ForEach(x => dadosVoltas.AddRange(x.Voltas));

            var dados = PilotoVoltaSumarizar(dadosPilotos, dadosVoltas);

            var dadosCorrida = TratarDadosCorridaRetorno(dados, dadosVoltas);

            // Ordernação do vencedor até o último colocado através do tempo total, menor tempo, e quantidade das voltas 
            retorno = dadosCorrida.OrderBy(x => x.TempoTotal).OrderByDescending(x => x.VoltaRealizadas).ToList();


            // Calculando a diferença do tempo cada piloto chegou após o vencedor no término das 4 voltas
            ClassificarCorridaGeral(ref retorno);

            return retorno;

        }


        #endregion

        #region Método Privados

        /// <summary>
        /// Pradonizar lista do log dos pilotos
        /// </summary>
        /// <param name="log">Log dos pilotos</param>
        /// <returns>Lista pradronizada</returns>
        private List<Piloto> PreencherPilotos(DataTable log)
        {
            List<Piloto> pilotos = log.AsEnumerable()
                                      .Select(m => new Piloto(
                                                              m.Field<int>("NumeroPiloto"),
                                                              m.Field<string>("NomePiloto"))
                                                              )
                                      .GroupBy(m => m.Numero)
                                      .Select(x => x.First())
                                      .ToList();

            return pilotos;
        }

        /// <summary>
        /// Pradonizar lista do log das voltas
        /// </summary>
        /// <param name="log">Log das voltas</param>
        /// <returns>Lista pradronizada</returns>
        private List<Volta> PreencherVoltaPilotos(DataTable log)
        {
            List<Volta> voltas = log.AsEnumerable()
                                    .Select(m => new Volta(
                                                           m.Field<int>("NumeroVolta"),
                                                           m.Field<TimeSpan>("TempoVolta"),
                                                           m.Field<DateTime>("Hora"),
                                                           m.Field<double>("VelocidadeMedio"),
                                                           m.Field<int>("NumeroPiloto"))
                                                          )
                                    .ToList();

            return voltas;
        }

        /// <summary>
        /// Classificação da corrida.
        /// </summary>
        /// <param name="dadosCorrida">Lista da corrida</param>
        private void ClassificarCorridaGeral(ref List<DadosCorrida> dadosCorrida)
        {
            var vencedor = dadosCorrida.OrderBy(x => x.TempoTotal).First();

            for (int i = 0; i < dadosCorrida.Count; i++)
            {
                if (dadosCorrida[i].CodigoPiloto == vencedor.CodigoPiloto)
                {
                    dadosCorrida[i].AlterarTempoPilotoAposVencedor(vencedor.TempoTotal);
                    dadosCorrida[i].AlterarClassificacaoCorrida(1);
                }
                else
                {
                    var diferencaTempo = dadosCorrida[i].TempoTotal - vencedor.TempoTotal;
                    dadosCorrida[i].AlterarTempoPilotoAposVencedor(diferencaTempo);
                    dadosCorrida[i].AlterarClassificacaoCorrida(i + 1);
                }
            }
        }

        /// <summary>
        /// Lista parcial dos resultado da corrida: melhor tempo, soma das voltas e velocidade média.
        /// </summary>
        /// <param name="dados">Lista sumarizado do merge do piloto e volta</param>
        /// <param name="dadosVoltas">Lista das voltas da corrida</param>
        /// <returns>Resultado parcial da corrida</returns>
        private List<DadosCorrida> TratarDadosCorridaRetorno(List<DadoInterno> dados, List<Volta> dadosVoltas)
        {
            var retorno = new List<DadosCorrida>();

            for (int i = 0; i < dados.Count; i++)
            {
                // Lista de todas as voltas do piloto
                var tempoVolta = dadosVoltas.FindAll(x => x.NumeroPiloto == dados[i].Numero).Select(x => x.Tempo).ToList();

                //Soma de todas as voltas do piloto
                var tempoTotal = tempoVolta.Aggregate(TimeSpan.Zero, (subtotal, t) => subtotal.Add(t));

                // Soma da velocidade média da volta de cada piloto durante a corrida
                var velocidadeMedia = dadosVoltas.FindAll(x => x.NumeroPiloto == dados[i].Numero).Select(x => x.VelocidadeMedia).ToList().Sum(media => media);

                var melhorVolta = dadosVoltas.FindAll(x => x.NumeroPiloto == dados[i].Numero).OrderBy(x => x.Tempo).First().NumeroVolta;

                retorno.Add(
                    new DadosCorrida(
                                    dados[i].Numero,
                                    dados[i].Nome,
                                    dados[i].QuantidadeVolta,
                                    tempoTotal,
                                    dados[i].MelhorTempoVolta,
                                    melhorVolta,
                                    velocidadeMedia / Convert.ToDouble(dados[i].QuantidadeVolta)
                                    ));
            }

            return retorno;
        }

        /// <summary>
        /// Realização do merge entre as listas de pilotos e voltas, smarizando a quantidade de voltas e o melhor tempo de cada piloto
        /// </summary>
        /// <param name="pilotos">Lista dos pilotos</param>
        /// <param name="voltas">Lista das voltas</param>
        /// <returns>Lista do merge entre as listas de pilotos e voltas</returns>
        private List<DadoInterno> PilotoVoltaSumarizar(List<Piloto> pilotos, List<Volta> voltas)
        {
            var retorno = new List<DadoInterno>();

            var dados = pilotos.Join(voltas, dp => dp.Numero, dv => dv.NumeroPiloto, (dp, dv) => new { dp, dv })
                                  .GroupBy(x => new { x.dp.Numero, x.dp.Nome })
                                  .Select(g => new
                                  {
                                      g.Key.Numero,
                                      g.Key.Nome,
                                      QuantidadeVolta = g.Select(x => x.dv.NumeroVolta).Count(),
                                      MelhorTempoVolta = g.Select(x => x.dv.Tempo).OrderBy(x => x).First()
                                  })
                                 .ToList();

            for (int i = 0; i < dados.Count; i++)
            {
                retorno.Add(new DadoInterno()
                {
                    Numero = dados[i].Numero,
                    Nome = dados[i].Nome,
                    QuantidadeVolta = dados[i].QuantidadeVolta,
                    MelhorTempoVolta = dados[i].MelhorTempoVolta
                });
            }


            return retorno;
        }


        #endregion

        #region Classes Internas

        private class DadoInterno
        {
            public DadoInterno()
            {
            }

            public int Numero { get; set; }
            public string Nome { get; set; }
            public int QuantidadeVolta { get; set; }
            public TimeSpan MelhorTempoVolta { get; set; }
        }
        #endregion

    }
}
