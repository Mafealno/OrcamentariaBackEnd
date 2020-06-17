using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentariaBackEnd
{
    public class CustoOrcamentoModel
    {
        private int _custoOrcamentoId;
        private double _valorCusto;
        private int _orcamentoId;
        private CustoModel _custoObra;

        public CustoOrcamentoModel(int custoOrcamentoId, double valorCusto, int orcamentoId, CustoModel custoObra)
        {
            CUSTO_ORCAMENTO_ID = custoOrcamentoId;
            VALOR_CUSTO = valorCusto;
            ORCAMENTO_ID = orcamentoId;
            CUSTO_OBRA = custoObra;
        }

        public CustoOrcamentoModel()
        {

        }

        public int CUSTO_ORCAMENTO_ID { get => _custoOrcamentoId; set => _custoOrcamentoId = value; }
        public double VALOR_CUSTO { get => _valorCusto; set => _valorCusto = value; }
        public int ORCAMENTO_ID { get => _orcamentoId; set => _orcamentoId = value; }
        public CustoModel CUSTO_OBRA { get => _custoObra; set => _custoObra = value; }
    }
}