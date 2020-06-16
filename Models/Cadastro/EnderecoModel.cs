using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orcamentaria.Model.Cadastro
{
    public class EnderecoModel
    {
        private int _enderecoId;
        private int _pessoaId;
        private string _logradouro;
        private string _cep;
        private string _numeroEndereco;
        private string _complemento;
        private string _bairro;
        private string _cidade;
        private string _estado;
        private string _uf;
        private bool _enderecoPadrao;

        public EnderecoModel(int enderecoId, int pessoaId, string logradouro, string cep, string numeroEndereco, string complemento,
                    string cidade, string bairro, string estado, string uf, bool enderecoPadrao)
        {
            ENDERECO_ID = enderecoId;
            PESSOA_ID = pessoaId;
            LOGRADOURO = logradouro;
            CEP = cep;
            NUMERO_ENDERECO = numeroEndereco;
            COMPLEMENTO = complemento;
            CIDADE = cidade;
            BAIRRO = bairro;
            ESTADO = estado;
            ENDERECO_PADRAO = enderecoPadrao;
            UF = uf;
        }

        public EnderecoModel()
        {

        }

        public int ENDERECO_ID { get => _enderecoId; set => _enderecoId = value; }
        public int PESSOA_ID { get => _pessoaId; set => _pessoaId = value; }
        public string LOGRADOURO { get => _logradouro; set => _logradouro = value; }
        public string CEP { get => _cep; set => _cep = value; }
        public string NUMERO_ENDERECO { get => _numeroEndereco; set => _numeroEndereco = value; }
        public string COMPLEMENTO { get => _complemento; set => _complemento = value; }
        public string CIDADE { get => _cidade; set => _cidade = value; }
        public string BAIRRO { get => _bairro; set => _bairro = value; }
        public string ESTADO { get => _estado; set => _estado = value; }
        public bool ENDERECO_PADRAO { get => _enderecoPadrao; set => _enderecoPadrao = value; }
        public string UF { get => _uf; set => _uf= value; }


    }
}