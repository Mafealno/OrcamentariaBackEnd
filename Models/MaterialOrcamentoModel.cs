using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class MaterialOrcamentoModel
    {
        private int _materialOrcamentoId;
        private int _orcamentoId;
        private double _valorUnitarioMaterial;
        private int _qtdeMaterial;
        private MaterialModel _material;

        public MaterialOrcamentoModel(int materialOrcamentoId, int orcamentoId, 
            double valorUnitarioMaterial, int qtdeMaterial, MaterialModel material)
        {
            MATERIAL_ORCAMENTO_ID = materialOrcamentoId;
            ORCAMENTO_ID = orcamentoId;
            VALOR_UNITARIO_MATERIAL = valorUnitarioMaterial;
            QTDE_MATERIAL = qtdeMaterial;
            MATERIAL = material;
        }

        public MaterialOrcamentoModel()
        {

        }

        public int MATERIAL_ORCAMENTO_ID { get => _materialOrcamentoId; set => _materialOrcamentoId = value; }
        public int ORCAMENTO_ID { get => _orcamentoId; set => _orcamentoId = value; }
        public double VALOR_UNITARIO_MATERIAL { get => _valorUnitarioMaterial; set => _valorUnitarioMaterial = value; }
        public int QTDE_MATERIAL { get => _qtdeMaterial; set => _qtdeMaterial = value; }
        public MaterialModel MATERIAL { get => _material; set => _material = value; }
    }
}
