using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentariaBackEnd
{
    public class TotaisOrcamentoModel 
    {
        private int _totaisOrcamentoId;
        private int _orcamentoId;
        private double _totaisItens;
        private double _totaisMaoObra;
        private double _totaisEquipamentos;
        private double _totaisCustos;
        private double _totalGeral;
        private double _areaTotal;

        public TotaisOrcamentoModel(int totaisOrcamentoId, int orcamentoId, double totaisItens, double totaisMaoObra, double totaisEquipamentos, double totaisCustos, double totalGeral, double areaTotal)
        {
            TOTAIS_ORCAMENTO_ID = totaisOrcamentoId;
            TOTAIS_ITENS = totaisItens;
            ORCAMENTO_ID = orcamentoId;
            TOTAIS_MAO_OBRA = totaisMaoObra;
            TOTAIS_EQUIPAMENTOS = totaisEquipamentos;
            TOTAIS_CUSTOS = totaisCustos;
            TOTAL_GERAL = totalGeral;
            AREA_TOTAL = areaTotal;
        }

        public TotaisOrcamentoModel()
        {

        }

        public int TOTAIS_ORCAMENTO_ID { get => _totaisOrcamentoId; set => _totaisOrcamentoId = value; }
        public int ORCAMENTO_ID { get => _orcamentoId; set => _orcamentoId = value; }
        public double TOTAIS_ITENS { get => _totaisItens; set => _totaisItens = value; }
        public double TOTAIS_MAO_OBRA { get => _totaisMaoObra; set => _totaisMaoObra = value; }
        public double TOTAIS_EQUIPAMENTOS { get => _totaisEquipamentos; set => _totaisEquipamentos = value; }
        public double TOTAIS_CUSTOS { get => _totaisCustos; set => _totaisCustos = value; }
        public double TOTAL_GERAL { get => _totalGeral; set => _totalGeral = value; }
        public double AREA_TOTAL { get => _areaTotal; set => _areaTotal = value; }
    }
}