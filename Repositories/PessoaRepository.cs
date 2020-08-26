
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using OrcamentariaBackEnd.Database;

namespace OrcamentariaBackEnd
{
    public class PessoaRepository : IPessoaRepository
    {
        private IConexao Conexao;

        public PessoaRepository(IConexao conexao)
        {
            this.Conexao = conexao;
        }

        public PessoaModel Create(PessoaModel pessoa)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"INSERT INTO T_ORCA_PESSOA (NOME_PESSOA, RG, CPF, CNPJ, TIPO_CADASTRO, TIPO_PESSOA) 
                                VALUES(@NOME_PESSOA, @RG, @CPF, @CNPJ, @TIPO_CADASTRO, @TIPO_PESSOA)", pessoa);

                    return Find(cn.Query<int>("SELECT PESSOA_ID FROM T_ORCA_PESSOA ORDER BY PESSOA_ID DESC LIMIT 1").ToArray()[0]);
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

                    if(resposta.Count() == 0)
                    {
                        return new PessoaModel();
                    }
                    else
                    {
                        return resposta.ToArray()[0];
                    }

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

                    return resposta;
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

                    return resposta;
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
