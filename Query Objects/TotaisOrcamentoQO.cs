using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class TotaisOrcamentoQO
    {
        public int TotaisOrcamentoId { get; set; }

        public int OrcamentoId { get; set; }

        public TotaisOrcamentoQO(int totaisOrcamentoId, int orcamentoId)
        {
            TotaisOrcamentoId = totaisOrcamentoId;
            OrcamentoId = orcamentoId;
        }

        public TotaisOrcamentoQO()
        {
        }
    }
}
