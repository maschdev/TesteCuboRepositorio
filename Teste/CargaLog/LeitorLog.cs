using System;
using System.Data;
using Validacao;
using Dominio.Enum;


namespace CargaLog
{
    public class LeitorLog : ILeitorLog
    {
        public LeitorLog(string caminhoArquivo)
        {
            LogValidacao.CaractereVazio(caminhoArquivo,"Caminho do arquivo obrigatório");
            LogValidacao.ValidarCaminhoArquivo(caminhoArquivo, "informar o caminho correto");

            this._caminhoArquivo = caminhoArquivo;
        }

        private string _caminhoArquivo { get; set; }


        #region Métodos Públicos

        /// <summary>
        /// Conversão do arquivo do log da corrida
        /// </summary>
        /// <param name="caminhoArquivo"></param>
        /// <returns>Tabela do log da corrida populado</returns>
        public DataTable ConverterParaDataTable()
        {
            DataTable retorno = null;

            // leitura do log
            string[] linhas = System.IO.File.ReadAllLines(this._caminhoArquivo);

            for (int i = 1; i < linhas.Length; i++)
            {
                // Divisão da linha em array tratrado. Não terá  os seguintes caracteres abaixo
                var linhaAtual = linhas[i].Split(new char[] { ' ', '\n', '\t', '\r', '\f', '\v', '\\' }, StringSplitOptions.RemoveEmptyEntries);

                // Geração do datatable vazio 
                if (i == 1)
                    retorno = GerarTabelaArquivo(linhaAtual.Length);

                DataRow linha = retorno.NewRow();

                // Preenchendo a linha do datatable
                linha = PreecherLinha(linhaAtual, ref linha);

                retorno.Rows.Add(linha);
            }

            // Excluindo a coluna com nenhum valor  
            retorno.Columns.RemoveAt(2);

            return retorno;
        }
        
        #endregion

        #region Métodos Privados

        /// <summary>
        /// Preenchimento da linha do tabela de log
        /// </summary>
        /// <param name="linhaAtual">A linha atual do log com os valores para registrar um tabela</param>
        /// <param name="linha"> A linha a ser populada</param>
        /// <returns>Um registro do log preenchido</returns>
        private DataRow PreecherLinha(string[] colunaValor, ref DataRow linha)
        {

            for (int colunaIndice = 0; colunaIndice < colunaValor.Length; colunaIndice++)
            {
                
                // Desconsiderando a coluna com o caractere especial
                if (colunaIndice != 2)
                {
                    switch (colunaIndice)
                    {
                        // Coluna 1: Hora
                        case (int)DadosLog.Hora:

                            linha[colunaIndice] = DateTime.Parse(string.Format("{0}/{1}/{2} {3}",
                                                  DateTime.Now.Year.ToString(),
                                                  DateTime.Now.Month.ToString(),
                                                  DateTime.Now.Day.ToString(),
                                                  colunaValor[colunaIndice]));
                            break;

                        // Coluna 2: Número do piloto    
                        case (int)DadosLog.NumeroPiloto:

                            linha[colunaIndice] = Convert.ToInt32(colunaValor[colunaIndice]);
                            break;

                        // Coluna 3: Nome do piloto    
                        case (int)DadosLog.NomePiloto:

                            linha[colunaIndice] = colunaValor[colunaIndice].Trim().ToUpper();
                            break;

                        // Coluna 4: Número volta    
                        case (int)DadosLog.NumeroVolta:

                            linha[colunaIndice] = Convert.ToInt32(colunaValor[colunaIndice]);
                            break;

                        // Coluna 5: Tempo da volta    
                        case (int)DadosLog.TempoVolta:

                            var arrayColuna = colunaValor[colunaIndice].Replace(".", ":").Split(':');
                            var tempoVolta = new TimeSpan(0,0,Convert.ToInt32(arrayColuna[0]) , Convert.ToInt32(arrayColuna[1]) , Convert.ToInt32(arrayColuna[2]));

                            linha[colunaIndice] = tempoVolta;
                            break;

                        // Coluna 6: Velocidade méia da volta    
                        case (int)DadosLog.VelocidadeMedio:
                            linha[colunaIndice] = Convert.ToDouble(colunaValor[colunaIndice]);
                            break;
                    }
                }
            }

            return linha;
        }

        /// <summary>
        /// Geração da tabela do log de Corrida
        /// </summary>
        /// <param name="quantidadeColunas">Quantidade das colunas da tabela</param>
        /// <returns>A tabela com as colunas tipadas</returns>
        private DataTable GerarTabelaArquivo(int quantidadeColunas)
        {
            DataTable tabelaArquivo = new DataTable();

            for (int coluna = 0; coluna < quantidadeColunas; coluna++)
            {
                switch (coluna)
                {
                    case (int)DadosLog.Hora:
                        tabelaArquivo.Columns.Add(new DataColumn("Hora", typeof(DateTime)));
                        break;

                    case (int)DadosLog.NumeroPiloto:
                        tabelaArquivo.Columns.Add(new DataColumn("NumeroPiloto", typeof(int)));
                        break;

                    case (int)DadosLog.NomePiloto:
                        tabelaArquivo.Columns.Add(new DataColumn("NomePiloto", typeof(string)));
                        break;

                    case (int)DadosLog.NumeroVolta:
                        tabelaArquivo.Columns.Add(new DataColumn("NumeroVolta", typeof(int)));
                        break;

                    case (int)DadosLog.TempoVolta:
                        tabelaArquivo.Columns.Add(new DataColumn("TempoVolta", typeof(TimeSpan)));
                        break;

                    case (int)DadosLog.VelocidadeMedio:
                        tabelaArquivo.Columns.Add(new DataColumn("VelocidadeMedio", typeof(Double)));
                        break;

                    default:
                        tabelaArquivo.Columns.Add(new DataColumn("Coluna" + (coluna + 1).ToString()));
                        break;
                }

            }

            return tabelaArquivo;

        }

        #endregion

    }
}
