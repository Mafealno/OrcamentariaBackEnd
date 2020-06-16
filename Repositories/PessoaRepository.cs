using Orcamentaria.Model.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using OrcamentariaBackEnd.Database;
using OrcamentariaBackEnd.Repositories.Cadastro;
using System.Security.Principal;

namespace OrcamentariaBackEnd.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        private IConexao Conexao;
        private IContatoRepository Contato;
        private IEnderecoRepository Endereco;

        public PessoaRepository(IConexao conexao, IContatoRepository contato, IEnderecoRepository endereco)
        {
            this.Conexao = conexao;
            this.Contato = contato;
            this.Endereco = endereco;
        }

        public PessoaModel Create(PessoaModel pessoa)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"INSERT INTO T_ORCA_PESSOA (NOME_PESSOA, RG, CPF, CNPJ, TIPO_CADASTRO, TIPO_PESSOA) 
                                VALUES(@NOME_PESSOA, @RG, @CPF, @CNPJ, @TIPO_CADASTRO, @TIPO_PESSOA)", pessoa);

                    return Find(cn.Query<int>("SELECT LAST_INSERT_ID()").ToArray()[0]);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int pessoaId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    Contato.DeletePorPessoaId(pessoaId);
                    Endereco.DeletePorPessoaId(pessoaId);
                    cn.Execute("DELETE FROM T_ORCA_PESSOA WHERE PESSOA_ID = @PessoaId", new { pessoaId });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public PessoaModel Find(int pessoaId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<PessoaModel>("SELECT * FROM T_ORCA_PESSOA WHERE PESSOA_ID = @PessoaId", new { pessoaId });
                    resposta.ToArray()[0].LIST_CONTATO = Contato.ListPorPessoaId(pessoaId).ToList();
                    resposta.ToArray()[0].LIST_ENDERECO = Endereco.ListPorPessoaId(pessoaId).ToList();

                    return resposta.ToArray()[0];
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<PessoaModel> List()
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<PessoaModel>("SELECT * FROM T_ORCA_PESSOA");

                    List<PessoaModel> listPessoas = new List<PessoaModel>();

                    foreach (PessoaModel pessoa in resposta)
                    {
                        listPessoas.Add(Find(pessoa.PESSOA_ID));
                    }

                    return listPessoas;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<PessoaModel> ListPorNomePessoa(string nomePessoa)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<PessoaModel>("SELECT * FROM T_ORCA_PESSOA WHERE NOME_PESSOA LIKE @nomePessoa", new { nomePessoa = nomePessoa + '%' });

                    List<PessoaModel> listPessoas = new List<PessoaModel>();

                    foreach (PessoaModel pessoa in resposta)
                    {
                        listPessoas.Add(Find(pessoa.PESSOA_ID));
                    }

                    return listPessoas;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(int pessoaId, PessoaModel pessoa)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"UPDATE T_ORCA_PESSOA SET NOME_PESSOA = @NOME_PESSOA, RG = @RG,
                                CPF = @CPF, CNPJ = @CNPJ, TIPO_CADASTRO = @TIPO_CADASTRO,
                                TIPO_PESSOA = @TIPO_PESSOA WHERE PESSOA_ID = @pessoaId", new 
                    { 
                        pessoa.NOME_PESSOA, 
                        pessoa.RG, 
                        pessoa.CPF, 
                        pessoa.CNPJ, 
                        pessoa.TIPO_CADASTRO, 
                        pessoa.TIPO_PESSOA, 
                        pessoaId 
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
