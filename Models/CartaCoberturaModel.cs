using System.Collections.Generic;

namespace OrcamentariaBackEnd
{
    public class CartaCoberturaModel
    {

        private int _cartaCoberturaId;
        private string _referencia;
        private MaterialModel _produto;
        private List<ItensCartaCoberturaModel> _itensCartaCoberturaModel;

        public CartaCoberturaModel(int cartaCoberturaId, string referencia, MaterialModel produto, List<ItensCartaCoberturaModel> itensCartaCoberturaModel)
        {
            CARTA_COBERTURA_ID = cartaCoberturaId;
            REFERENCIA = referencia;
            MATERIAL = produto;
            LIST_ITENS_CARTA_COBERTURA = itensCartaCoberturaModel;
        }

        public CartaCoberturaModel()
        {

        }

        public int CARTA_COBERTURA_ID { get => _cartaCoberturaId; set => _cartaCoberturaId = value; }
        public string REFERENCIA { get => _referencia; set => _referencia = value; }
        public MaterialModel MATERIAL { get => _produto; set => _produto = value; }
        public List<ItensCartaCoberturaModel> LIST_ITENS_CARTA_COBERTURA { get => _itensCartaCoberturaModel; set => _itensCartaCoberturaModel = value; }
        
    }
}