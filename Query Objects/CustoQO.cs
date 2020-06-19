using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class CustoQO
    {
        public int CustoId { get; set; }

        public string NomeCusto { get; set; }

        public CustoQO(int custoId, string nomeCusto)
        {
            CustoId = custoId;
            NomeCusto = nomeCusto;
        }
    }
}
