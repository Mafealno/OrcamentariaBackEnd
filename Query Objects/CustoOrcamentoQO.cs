﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class CustoOrcamentoQO
    {
        public int CustoOrcamentoId { get; set; }

        public int OrcamentoId { get; set; }

        public CustoOrcamentoQO(int custoOrcamentoId, int orcamentoId)
        {
            CustoOrcamentoId = custoOrcamentoId;
            OrcamentoId = orcamentoId;
        }

        public CustoOrcamentoQO()
        {
        }
    }
}
