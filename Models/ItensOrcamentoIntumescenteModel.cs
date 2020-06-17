using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace OrcamentariaBackEnd
{
    public class ItensOrcamentoIntumescenteModel : ItensOrcamentoModel
    {
        private string _referencia;
        private int _numeroFaces;
        private double _valorHp;
        private double _valorHpA;
        private double _valorWD;
        private int _qtde;
        private double _valorEspessura;
        private double _qtdeLitros;
        private PerfilModel _perfilModel;

        public ItensOrcamentoIntumescenteModel(string referencia, int numeroFaces, double valorHp, double valorHpA, double valorWD, 
                                            int qtde, double valorEspessura, double qtdeLitros, PerfilModel perfilModel, int itensOrcamentoId, int orcamentoId,
                                            int numeroLinha, double valorComprimento, double area, MaterialModel produto) : base(itensOrcamentoId, 
                                            orcamentoId, numeroLinha, valorComprimento, area, produto)
        {
            REFERENCIA = referencia;
            NUMERO_FACES = numeroFaces;
            VALOR_HP = valorHp;
            VALOR_HP_A = valorHpA;
            VALOR_WD = valorWD;
            QTDE = qtde;
            VALOR_ESPESSURA = valorEspessura;
            QTDE_LITROS = qtdeLitros;
            PERFIL = perfilModel;
            ITENS_ORCAMENTO_ID = itensOrcamentoId;
            ORCAMENTO_ID = orcamentoId;
            NUMERO_LINHA = numeroLinha;
            VALOR_COMPRIMENTO = valorComprimento;
            AREA = area;
            PRODUTO = produto;
        }

        public ItensOrcamentoIntumescenteModel()
        {

        }
        public string REFERENCIA { get => _referencia; set => _referencia = value; }
        public int NUMERO_FACES { get => _numeroFaces; set => _numeroFaces = value; }
        public double VALOR_HP { get => _valorHp; set => _valorHp = value; }
        public double VALOR_HP_A { get => _valorHpA; set => _valorHpA = value; }
        public double VALOR_WD { get => _valorWD; set => _valorWD = value; }
        public int QTDE { get => _qtde; set => _qtde = value; }
        public double VALOR_ESPESSURA { get => _valorEspessura; set => _valorEspessura = value; }
        public double QTDE_LITROS { get => _qtdeLitros; set => _qtdeLitros = value; }
        public PerfilModel PERFIL { get => _perfilModel; set => _perfilModel = value; }
    }
}