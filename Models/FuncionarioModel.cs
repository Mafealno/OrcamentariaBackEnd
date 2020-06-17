using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentariaBackEnd
{

    public class FuncionarioModel : PessoaModel
    {
        private string _cargoFuncionario;
        private DateTime _dataAdmissao;
        private double _valorDiaTrabalhado;
        private string _statusFuncionario;

        public FuncionarioModel(string cargoFuncionario, DateTime dataAdmissao, double valorDiaTrabalhado, string statusFuncionario,
                                int pessoaId, string nomePessoa, string rgPessoa, string cpfPessoa, string cnpjPessoa,  
                                List<EnderecoModel> enderecoPessoa, List<ContatoModel> contatoPessoa, string tipoCadastroPessoa,
                                string tipoPessoa) : base(pessoaId, nomePessoa, rgPessoa, cpfPessoa, cnpjPessoa,  
                                enderecoPessoa, contatoPessoa, tipoCadastroPessoa, tipoPessoa)
        {
            CARGO_FUNCIONARIO = cargoFuncionario;
            DATA_ADMISSAO = dataAdmissao;
            VALOR_DIA_TRABALHADO = valorDiaTrabalhado;
            STATUS_FUNCIONARIO = statusFuncionario;
            PESSOA_ID = pessoaId;
            NOME_PESSOA = nomePessoa;
            RG = rgPessoa;
            CPF = cpfPessoa;
            CNPJ = cnpjPessoa;
            TIPO_CADASTRO = tipoCadastroPessoa;
            TIPO_PESSOA = tipoPessoa;
            LIST_ENDERECO = enderecoPessoa;
            LIST_CONTATO = contatoPessoa;
        }

        public FuncionarioModel()
        {

        }

        public string CARGO_FUNCIONARIO { get => _cargoFuncionario; set => _cargoFuncionario = value; }
        public DateTime DATA_ADMISSAO { get => _dataAdmissao; set => _dataAdmissao = value; }
        public double VALOR_DIA_TRABALHADO { get => _valorDiaTrabalhado; set => _valorDiaTrabalhado = value; }
        public string STATUS_FUNCIONARIO { get => _statusFuncionario; set => _statusFuncionario = value; }



    }
}