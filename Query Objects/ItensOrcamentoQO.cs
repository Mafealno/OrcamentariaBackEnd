﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class ItensOrcamentoQO
    {
        public int ItensOrcamentoId { get; set; }

        public int OrcamentoId { get; set; }

        public ItensOrcamentoQO(int itensOrcamentoId, int orcamentoId)
        {
            ItensOrcamentoId = itensOrcamentoId;
            OrcamentoId = orcamentoId;
        }

        public ItensOrcamentoQO()
        {
        }
    }
}
