using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class EquipamentoQO
    {
        public int EquipamentoId { get; set; }

        public string NomeEquipamento { get; set; }

        public EquipamentoQO(int equipamentoId, string nomeEquipamento)
        {
            EquipamentoId = equipamentoId;
            NomeEquipamento = nomeEquipamento;
        }
    }
}
