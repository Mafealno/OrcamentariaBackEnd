using Orcamentaria.Model.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orcamentaria.Model.Orcamento
{
    public class EquipamentoOrcamentoModel
    {
        private int _equipamentoOrcamentoId;
        private int _orcamentoId;
        private double _valorUnitarioEquipamento;
        private int _qtdeEquipamento;
        private EquipamentoModel _equipamento;

        public EquipamentoOrcamentoModel(int equipamentoOrcamentoId, int orcamentoId, double valorUnitarioEquipamento, int qtdeEquipamento, EquipamentoModel equipamento)
        {
            EQUIPAMENTO_ORCAMENTO_ID = equipamentoOrcamentoId;
            ORCAMENTO_ID = orcamentoId;
            VALOR_UNITARIO_EQUIPAMENTO = valorUnitarioEquipamento;
            QTDE_EQUIPAMENTO = qtdeEquipamento;
            EQUIPAMENTO = equipamento;
        }

        public EquipamentoOrcamentoModel()
        {

        }

        public int EQUIPAMENTO_ORCAMENTO_ID { get => _equipamentoOrcamentoId; set => _equipamentoOrcamentoId = value; }
        public int ORCAMENTO_ID { get => _orcamentoId; set => _orcamentoId = value; }
        public double VALOR_UNITARIO_EQUIPAMENTO { get => _valorUnitarioEquipamento; set => _valorUnitarioEquipamento = value; }
        public int QTDE_EQUIPAMENTO { get => _qtdeEquipamento; set => _qtdeEquipamento = value; }
        public EquipamentoModel EQUIPAMENTO { get => _equipamento; set => _equipamento = value; }
    }
}