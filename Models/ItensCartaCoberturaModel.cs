using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentariaBackEnd
{
    public class ItensCartaCoberturaModel
    {
        private int _itensCartaCoberturaId;
        private int _cartaCoberturaId;
        private string _valorHpA;
        private string _tempoResistenciaFogo;
        private double _valorEspessura;

        public ItensCartaCoberturaModel(int itensCartaCoberturaId, int cartaCoberturaId, string valorHpA, string tempoResistenciaFogo, double valorEspessura)
        {
            ITENS_CARTA_COBERTURA_ID = itensCartaCoberturaId;
            CARTA_COBERTURA_ID = cartaCoberturaId;
            VALOR_HP_A = valorHpA;
            TEMPO_RESISTENCIA_FOGO = tempoResistenciaFogo;
            VALOR_ESPESSURA = valorEspessura;
        }

        public ItensCartaCoberturaModel()
        {

        }
        public int ITENS_CARTA_COBERTURA_ID { get => _itensCartaCoberturaId; set => _itensCartaCoberturaId = value; }
        public int CARTA_COBERTURA_ID { get => _cartaCoberturaId; set => _cartaCoberturaId = value; }
        public string VALOR_HP_A { get => _valorHpA; set => _valorHpA = value; }
        public string TEMPO_RESISTENCIA_FOGO { get => _tempoResistenciaFogo; set => _tempoResistenciaFogo = value; }
        public double VALOR_ESPESSURA { get => _valorEspessura; set => _valorEspessura = value; }
    }
}