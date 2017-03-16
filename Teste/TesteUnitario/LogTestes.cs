using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dominio.Entidade;
using CargaLog;
using CorridaKart;


namespace TesteUnitario
{
    [TestClass]
    public class Dada_Leitrua_de_Log
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("Leitura do Log")]
        public void Caminho_do_Log_fornecedo_correto()
        {
            ILeitorLog leitorLog = new LeitorLog(@"\users\caio\documents\visual studio 2015\Projects\Teste\CorridaKart\Log\CorridaLog.txt");

            Assert.IsNotNull(@"\users\caio\documents\visual studio 2015\Projects\Teste\CorridaKart\Log\CorridaLog.txt");
        }


        /// <summary>
        /// /
        /// </summary>
        [TestMethod]
        [TestCategory("Leitura do Log")]
        [ExpectedException(typeof(Exception))]
        public void Nao_existencia_do_log()
        {
            ILeitorLog leitorLog = new LeitorLog(@"\users\caio\documents\visual studio 2015\Projects\Teste\CorridaKart\Log\NaoExiste.txt");
        }


        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("Leitura do Log")]
        [ExpectedException(typeof(Exception))]
        public void Caminho_do_Log_fornecedo()
        {
            ILeitorLog leitorLog = new LeitorLog("");
            Assert.IsNotNull(@"\users\caio\documents\visual studio 2015\Projects\Teste\CorridaKart\Log\CorridaLog.txt");
        }
    }

    [TestClass]
    public class Dada_um_novo_piloto
    {
        /// <summary>
        ///  Validar o piloto com os dados preenchidos.
        /// </summary>
        [TestMethod]
        [TestCategory("Novo piloto")]
        public void Os_dados_do_piloto_estao_preenchidos()
        {
            var piloto = new Piloto(99, "Caio");

            Assert.AreEqual(99,piloto.Numero);
            Assert.AreEqual("Caio", piloto.Nome);
        }

        /// <summary>
        /// Forçar a exceção com o número do piloto invalído
        /// </summary>
        [TestMethod]
        [TestCategory("Novo piloto")]
        [ExpectedException(typeof(Exception))]
        public void Os_dados_do_piloto_deve_ser_fornecido_correto()
        {
            var piloto = new Piloto(-1, "Caio");
        }

        /// <summary>
        /// Forçar a exceção com o nome do piloto vazio.
        /// </summary>
        [TestMethod]
        [TestCategory("Novo piloto")]
        [ExpectedException(typeof(Exception))]
        public void Nome_do_piloto_deve_ser_fornecido()
        {
            var piloto = new Piloto(1, "");
        }
    }

    [TestClass]
    public class Dada_uma_volta_nova
    {
        /// <summary>
        /// Validar a volta com os dados preenchidos.
        /// </summary>
        [TestMethod]
        [TestCategory("Nova Volta")]
        public void Os_dados_da_volta_estao_preenchidos()
        {
            var volta = new Volta(1, new TimeSpan(0, 0, 1, 3, 4), DateTime.Now, 22.11, 22);
        }

        /// <summary>
        /// Forçar a exceção com o número da volta como 0.
        /// </summary>
        [TestMethod]
        [TestCategory("Nova Volta")]
        [ExpectedException(typeof(Exception))]
        public void Os_dados_da_volta_estao_preenchidos_corretos()
        {
            var volta = new Volta(0, new TimeSpan(0, 0, 1, 3, 4), DateTime.Now, 22.11, 22);
        }


    }


    [TestClass]
    public class Resultado_da_corrrida
    {
        /// <summary>
        /// Validar o fluxo completo.
        /// </summary>
        [TestMethod]
        [TestCategory("Resultado Gerado")]
        public void Classificacao_da_corrida()
        {
            ILeitorLog leitorLog = new LeitorLog(@"\users\caio\documents\visual studio 2015\Projects\Teste\CorridaKart\Log\CorridaLog.txt");

            var logTabela = leitorLog.ConverterParaDataTable();

            IHelper helper = new Helper();

            var dadosLog = helper.PopularTabelaCorrida(logTabela);

            var resultadoCorrida = helper.GerarDadosCorrida(dadosLog);

            var vencedor = resultadoCorrida.First();

            Assert.IsTrue(((resultadoCorrida.Count == 6 ? true : false) && (vencedor.NomePiloto.Equals("F.MASSA") ? true : false)));
        }

    }
}
