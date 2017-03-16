using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  System.Data;
using Dominio.Entidade;


namespace CorridaKart
{
    public interface IHelper
    {
        List<Piloto> PopularTabelaCorrida(DataTable log);

        List<DadosCorrida> GerarDadosCorrida(List<Piloto> dadosPilotos);
    }
}
