﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentariaBackEnd
{
    public class ItensOrcamentoModel
    {
        private int _itensOrcamentoId;
        private int _orcamentoId;
        private int _numeroLinha;
        private double _valorComprimento;
        private double _area;

        public ItensOrcamentoModel(int itensOrcamentoId, int orcamentoId, int numeroLinha, double valorComprimento, double area)
        {
            ITENS_ORCAMENTO_ID = itensOrcamentoId;
            ORCAMENTO_ID = orcamentoId;
            NUMERO_LINHA = numeroLinha;
            VALOR_COMPRIMENTO = valorComprimento;
            AREA = area;
        }

        public ItensOrcamentoModel()
        {

        }

        public int ITENS_ORCAMENTO_ID { get => _itensOrcamentoId; set => _itensOrcamentoId = value; }
        public int ORCAMENTO_ID { get => _orcamentoId; set => _orcamentoId = value; }
        public int NUMERO_LINHA { get => _numeroLinha; set => _numeroLinha = value; }
        public double VALOR_COMPRIMENTO { get => _valorComprimento; set => _valorComprimento = value; }
        public double AREA { get => _area; set => _area = value; }

    }
}