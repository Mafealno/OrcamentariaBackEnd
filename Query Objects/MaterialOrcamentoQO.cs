using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class MaterialOrcamentoQO
    {
        public int MaterialOrcamentoId { get; set; }

        public int OrcamentoId { get; set; }

        public MaterialOrcamentoQO(int materialOrcamentoId, int orcamentoId)
        {
            MaterialOrcamentoId = materialOrcamentoId;
            OrcamentoId = orcamentoId;
        }

        public MaterialOrcamentoQO()
        {
        }
    }
}
