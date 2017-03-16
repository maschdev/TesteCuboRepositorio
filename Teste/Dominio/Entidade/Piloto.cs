using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validacao;

namespace Dominio.Entidade
{
    public class Piloto
    {
        public Piloto(int numero, string nome)
        {
            LogValidacao.NumeroVazio(numero, "Número do piloto inválido");
            LogValidacao.CaractereVazio(nome,"Nome do piloto obrigatório");

            this.Numero = numero;
            this.Nome = nome;
            this._voltas = new List<Volta>();
        }

        private IList<Volta> _voltas;
        public int Numero { get; private set; }
        public string Nome { get; private set; }
        public int PosicaoChegada { get; set; }

        public ICollection<Volta> Voltas
        {
            get { return _voltas; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="volta"></param>
        private void AddVolta(Volta volta)
        {
            this._voltas.Add(volta);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="voltas"></param>
        public void AddVoltas(List<Volta> voltas)
        {
            voltas.ForEach(volta => AddVolta(volta));
        }

    }

}
