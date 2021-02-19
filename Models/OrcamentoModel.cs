using System;
using System.Collections.Generic;

namespace OrcamentariaBackEnd
{
    public class OrcamentoModel
    {
        private int _orcamentoId;
        private string _nomeObra;
        private string _referencia;
        private string _prazoEntrega;
        private int _diasTrabalhado;
        private DateTime _dataCriacaoOrcamento;
        private string _contatoObra;
        private string _tipoObra;
        private PessoaModel clienteOrcamento;
        private TotaisOrcamentoModel _totaisOrcamento;
        private List<MaoObraOrcamentoModel> _maoObraOrcamento;
        private List<CustoOrcamentoModel> _custoOrcamento;
        private List<EquipamentoOrcamentoModel> _equipamentoOrcamento;
        private List<MaterialOrcamentoModel> _materialOrcamento;

        public OrcamentoModel(int orcamentoId, string nomeObra, string referencia, string prazoEntrega, int diasTrabalhado, DateTime dataCriacaoOrcamento, string contatoObra, string tipoObra, 
            TotaisOrcamentoModel totaisOrcamento, PessoaModel clienteOrcamento, List<MaoObraOrcamentoModel> maoObraOrcamento, List<CustoOrcamentoModel> custoOrcamento, 
            List<EquipamentoOrcamentoModel> equipamentoOrcamento, List<MaterialOrcamentoModel> materialOrcamento)
        {
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
            LIST_MAO_OBRA_ORCAMENTO = maoObraOrcamento;
            LIST_CUSTO_ORCAMENTO = custoOrcamento;
            LIST_EQUIPAMENTO_ORCAMENTO = equipamentoOrcamento;
            LIST_MATERIAL_ORCAMENTO = materialOrcamento;
        }

        public OrcamentoModel()
        {

        }
        public int ORCAMENTO_ID { get => _orcamentoId; set => _orcamentoId = value; }
        public string NOME_OBRA { get => _nomeObra; set => _nomeObra = value; }
        public string REFERENCIA { get => _referencia; set => _referencia = value; }
        public string PRAZO_ENTREGA { get => _prazoEntrega; set => _prazoEntrega = value; }
        public int DIAS_TRABALHADO { get => _diasTrabalhado; set => _diasTrabalhado = value; }
        public DateTime DATA_CRIACAO_ORCAMENTO { get => _dataCriacaoOrcamento; set => _dataCriacaoOrcamento = value; }
        public string A_C { get => _contatoObra; set => _contatoObra = value; }
        public string TIPO_OBRA { get => _tipoObra; set => _tipoObra = value; }
        public TotaisOrcamentoModel TOTAIS_ORCAMENTO { get => _totaisOrcamento; set => _totaisOrcamento = value; }
        public PessoaModel CLIENTE_ORCAMENTO { get => clienteOrcamento; set => clienteOrcamento = value; }
        public List<MaoObraOrcamentoModel> LIST_MAO_OBRA_ORCAMENTO { get => _maoObraOrcamento; set => _maoObraOrcamento = value; }
        public List<CustoOrcamentoModel> LIST_CUSTO_ORCAMENTO { get => _custoOrcamento; set => _custoOrcamento = value; }
        public List<EquipamentoOrcamentoModel> LIST_EQUIPAMENTO_ORCAMENTO { get => _equipamentoOrcamento; set => _equipamentoOrcamento = value; }
        public List<MaterialOrcamentoModel> LIST_MATERIAL_ORCAMENTO { get => _materialOrcamento; set => _materialOrcamento = value; }
    }
}