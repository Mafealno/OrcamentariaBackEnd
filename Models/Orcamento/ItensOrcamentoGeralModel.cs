using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orcamentaria.Model.Cadastro;

namespace Orcamentaria.Model.Orcamento
{
    public class ItensOrcamentoGeralModel : ItensOrcamentoModel
    {
        private string _ambienteAplicado;
        private double _valorLargura;
        private double _valorM2;

        public ItensOrcamentoGeralModel(string ambienteAplicado, double valorLargura, double valorM2, int itensOrcamentoId,  int orcamentoId,
                                        int numeroLinha, double valorComprimento, double area, MaterialModel produto) : base(itensOrcamentoId, 
                                        orcamentoId, numeroLinha, valorComprimento, area, produto)
        {
            AMBIENTE_APLICACAO = ambienteAplicado;
            VALOR_LARGURA = valorLargura;
            VALOR_M_2 = valorM2;
            ITENS_ORCAMENTO_ID = itensOrcamentoId;
            ORCAMENTO_ID = orcamentoId;
            NUMERO_LINHA = numeroLinha;
            VALOR_COMPRIMENTO = valorComprimento;
            AREA = area;
            PRODUTO = produto;
        }

        public ItensOrcamentoGeralModel()
        {

        }

        public string AMBIENTE_APLICACAO { get => _ambienteAplicado; set => _ambienteAplicado = value; }
        public double VALOR_LARGURA { get => _valorLargura; set => _valorLargura = value; }
        public double VALOR_M_2 { get => _valorM2; set => _valorM2 = value; }
    }
}