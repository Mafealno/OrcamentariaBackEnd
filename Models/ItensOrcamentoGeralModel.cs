using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace OrcamentariaBackEnd
{
    public class ItensOrcamentoGeralModel : ItensOrcamentoModel
    {
        private string _ambienteAplicado;
        private double _valorLargura;
        private string _localAplicacao;
        private string _acaoAplicar;
        private double _valorM2;
        private MaterialModel _produto;

        public ItensOrcamentoGeralModel(string ambienteAplicado, double valorLargura, string localAplicacao, 
                                        string acaoAplicar, double valorM2, int itensOrcamentoId, int orcamentoId, 
                                        int numeroLinha, double valorComprimento, double area, 
                                        MaterialModel produto) : base(itensOrcamentoId, orcamentoId,
                                        numeroLinha, valorComprimento, area)
        {
            AMBIENTE_APLICACAO = ambienteAplicado;
            VALOR_LARGURA = valorLargura;
            LOCAL_APLICACAO = localAplicacao;
            ACAO_APLICAR = acaoAplicar;
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
        public string LOCAL_APLICACAO { get => _localAplicacao; set => _localAplicacao = value; }
        public string ACAO_APLICAR { get => _acaoAplicar; set => _acaoAplicar = value; }
        public double VALOR_LARGURA { get => _valorLargura; set => _valorLargura = value; }
        public double VALOR_M_2 { get => _valorM2; set => _valorM2 = value; }
        public MaterialModel PRODUTO { get => _produto; set => _produto = value; }

    }
}