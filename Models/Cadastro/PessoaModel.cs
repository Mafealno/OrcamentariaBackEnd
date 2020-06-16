using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orcamentaria.Model.Cadastro
{

    public class PessoaModel
    {
        private int _pessoaId;
        private string _nome;
        private string _rg;
        private string _cpf;
        private string _cnpj;
        private string _tipoCadastro;
        private string _tipoPessoa;
        private List<EnderecoModel> _endereco;
        private List<ContatoModel> _contato;

        public PessoaModel(int pessoaId, string nome, string rg, string cpf, string cnpj, List<EnderecoModel> listEndereco, 
                            List<ContatoModel> listContato, string tipoCadastro, string tipoPessoa)
        {
            PESSOA_ID = pessoaId;
            NOME_PESSOA = nome;
            RG = rg;
            CPF = cpf;
            CNPJ = cnpj;
            TIPO_CADASTRO = tipoCadastro;
            TIPO_PESSOA = tipoPessoa;
            LIST_ENDERECO = listEndereco;
            LIST_CONTATO = listContato;
        }

        public PessoaModel()
        {

        }


        public int PESSOA_ID { get => _pessoaId; set => _pessoaId = value; }
        public string NOME_PESSOA { get => _nome; set => _nome = value; }
        public string RG { get => _rg; set => _rg = value; }
        public string CPF { get => _cpf; set => _cpf = value; }
        public string CNPJ { get => _cnpj; set => _cnpj = value; }
        public string TIPO_CADASTRO { get => _tipoCadastro; set => _tipoCadastro = value; }
        public string TIPO_PESSOA { get => _tipoPessoa; set => _tipoPessoa = value; }
        public List<EnderecoModel> LIST_ENDERECO { get => _endereco; set => _endereco = value; }
        public List<ContatoModel> LIST_CONTATO { get => _contato; set => _contato = value; }



    }
}