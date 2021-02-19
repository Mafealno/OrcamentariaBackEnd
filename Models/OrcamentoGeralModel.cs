using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class OrcamentoGeralModel : OrcamentoModel
    {
        private List<ItensOrcamentoGeralModel> _itensOrcamentoGeral;

        public OrcamentoGeralModel(List<ItensOrcamentoGeralModel> itensOrcamentoGeral,
                                    int orcamentoId, string nomeObra, string referencia, string prazoEntrega, int diasTrabalhado, DateTime dataCriacaoOrcamento,
                                    string contatoObra, string tipoObra, TotaisOrcamentoModel totaisOrcamento, PessoaModel clienteOrcamento,
                                    List<MaoObraOrcamentoModel> maoObraOrcamento, List<CustoOrcamentoModel> custoOrcamento, List<EquipamentoOrcamentoModel> equipamentoOrcamento,
                                    List<MaterialOrcamentoModel> materialOrcamento) : base(orcamentoId, nomeObra, referencia, prazoEntrega, diasTrabalhado, dataCriacaoOrcamento, 
                                    contatoObra, tipoObra, totaisOrcamento, clienteOrcamento,maoObraOrcamento, custoOrcamento, equipamentoOrcamento, materialOrcamento)
        {
            LIST_ITENS_ORCAMENTO_GERAL = itensOrcamentoGeral;
            ORCAMENTO_ID = orcamentoId;
            NOME_OBRA = nomeObra;
            REFERENCIA = referencia;
            PRAZO_ENTREGA = prazoEntrega;
            DIAS_TRABALHADO = diasTrabalhado;
            DATA_CRIACAO_ORCAMENTO = dataCriacaoOrcamento;
            A_C = contatoObra;
            TIPO_OBRA = tipoObra;
            TOTAIS_ORCAMENTO = totaisOrcamento;
            CLIENTE_ORCAMENTO = clienteOrcamento;
            LIST_ITENS_ORCAMENTO_GERAL = itensOrcamentoGeral;
            LIST_MAO_OBRA_ORCAMENTO = maoObraOrcamento;
            LIST_CUSTO_ORCAMENTO = custoOrcamento;
            LIST_EQUIPAMENTO_ORCAMENTO = equipamentoOrcamento;
            LIST_MATERIAL_ORCAMENTO = materialOrcamento;
        }

        public OrcamentoGeralModel()
        {

        }


        public List<ItensOrcamentoGeralModel> LIST_ITENS_ORCAMENTO_GERAL { get => _itensOrcamentoGeral; set => _itensOrcamentoGeral = value; }
    }
}
