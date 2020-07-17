using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class EquipamentoOrcamentoQO
    {
        public int EquipamentoOrcamentoId { get; set; }

        public int OrcamentoId { get; set; }

        public EquipamentoOrcamentoQO(int equipamentoOrcamentoId, int orcamentoId)
        {
            EquipamentoOrcamentoId = equipamentoOrcamentoId;
            OrcamentoId = orcamentoId;
        }

        public EquipamentoOrcamentoQO()
        {
        }
    }
}
