using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orcamentaria.Model.Cadastro
{

    public class CustoModel
    {
        private int _custoId;
        private string _nome;
        private string _descricao;
        private string _tipoCusto;
        private double _valorCusto;

        public CustoModel(int custoId, string nome, string descricao, string tipoCusto, double valorCusto)
        {
            CUSTO_ID = custoId;
            NOME_CUSTO = nome;
            DESCRICAO = descricao;
            TIPO_CUSTO = tipoCusto;
            VALOR_CUSTO = valorCusto;
        }

        public CustoModel()
        {

        }

        public int CUSTO_ID { get => _custoId; set => _custoId = value; }
        public string NOME_CUSTO { get => _nome; set => _nome = value; }
        public string DESCRICAO { get => _descricao; set => _descricao = value; }
        public string TIPO_CUSTO { get => _tipoCusto; set => _tipoCusto = value; }
        public double VALOR_CUSTO { get => _valorCusto; set => _valorCusto = value; }

        
    }
}