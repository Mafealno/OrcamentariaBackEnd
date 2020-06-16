using Orcamentaria.Model.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orcamentaria.Model.Orcamento
{
    public class MaoObraOrcamentoModel
    {
        private int _maoObraOrcamentoId;
        private int _orcamentoId;
        private FuncionarioModel _funcionario;
        private List<CustoModel> _custosFuncionario;

        public MaoObraOrcamentoModel(int maoObraOrcamentoId, int orcamentoId, FuncionarioModel funcionario, List<CustoModel> custosFuncionario)
        {
            MAO_OBRA_ORCAMENTO_ID = maoObraOrcamentoId;
            ORCAMENTO_ID = orcamentoId;
            FUNCIONARIO = funcionario;
            LIST_CUSTO = custosFuncionario;
        }

        public MaoObraOrcamentoModel()
        {

        }

        public int MAO_OBRA_ORCAMENTO_ID { get => _maoObraOrcamentoId; set => _maoObraOrcamentoId = value; }
        public int ORCAMENTO_ID { get => _orcamentoId; set => _orcamentoId = value; }
        public FuncionarioModel FUNCIONARIO { get => _funcionario; set => _funcionario = value; }
        public List<CustoModel> LIST_CUSTO { get => _custosFuncionario; set => _custosFuncionario = value; }
    }
}