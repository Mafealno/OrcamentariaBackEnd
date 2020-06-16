using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orcamentaria.Model.Cadastro
{
    public class EquipamentoModel

    {
        private int _equipamentoId;
        private string _nome;
        private string _descricao;
        private PessoaModel _fabricante;

        public EquipamentoModel(int equipamentoId, string nome, string descricao, PessoaModel fabricante)
        {
            EQUIPAMENTO_ID = equipamentoId;
            NOME_EQUIPAMENTO = nome;
            DESCRICAO = descricao;
            FABRICANTE = fabricante;
        }

        public EquipamentoModel()
        {

        }
        
        public int EQUIPAMENTO_ID { get => _equipamentoId; set => _equipamentoId = value; }
        public string NOME_EQUIPAMENTO { get => _nome; set => _nome = value; }
        public string DESCRICAO { get => _descricao; set => _descricao = value; }
        public PessoaModel FABRICANTE { get => _fabricante; set => _fabricante = value; }
    }
}