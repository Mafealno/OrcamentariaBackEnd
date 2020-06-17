
using OrcamentariaBackEnd.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace OrcamentariaBackEnd
{
    public class EnderecoRepository : IEnderecoRepository
    {

        private IConexao Conexao;

        public EnderecoRepository(IConexao conexao)
        {
            this.Conexao = conexao;
        }

        public EnderecoModel Create(EnderecoModel endereco)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"INSERT INTO T_ORCA_ENDERECO (PESSOA_ID, LOGRADOURO, CEP, NUMERO_ENDERECO, COMPLEMENTO, 
                                CIDADE, BAIRRO, ESTADO, ENDERECO_PADRAO, UF) VALUES(@PESSOA_ID, @LOGRADOURO, @CEP, 
                                @NUMERO_ENDERECO, @COMPLEMENTO, @CIDADE, @BAIRRO, @ESTADO, @ENDERECO_PADRAO, @UF)", endereco);

                    return Find(cn.Query<int>("SELECT LAST_INSERT_ID()").ToArray()[0]);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int enderecoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute("DELETE FROM T_ORCA_ENDERECO WHERE ENDERECO_ID = @enderecoId", new { enderecoId });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeletePorPessoaId(int pessoaId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute("DELETE FROM T_ORCA_ENDERECO WHERE PESSOA_ID = @pessoaId", new { pessoaId });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public EnderecoModel Find(int enderecoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<EnderecoModel>("SELECT * FROM T_ORCA_ENDERECO WHERE ENDERECO_ID = @enderecoId", new { enderecoId });
                    return resposta.ToArray()[0];
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public EnderecoModel FindPorEnderecoPadrao(int pessoaId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<EnderecoModel>("SELECT * FROM T_ORCA_ENDERECO WHERE PESSOA_ID = @pessoaId AND ENDERECO_PADRAO = true", new { pessoaId });
                    return resposta.ToArray()[0];
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<EnderecoModel> List()
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<EnderecoModel>("SELECT * FROM T_ORCA_ENDERECO");
                    return resposta;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<EnderecoModel> ListPorPessoaId(int pessoaId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<EnderecoModel>("SELECT * FROM T_ORCA_ENDERECO WHERE PESSOA_ID = @pessoaId", new { pessoaId });
                    return resposta;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(int enderecoId, EnderecoModel endereco)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"UPDATE T_ORCA_ENDERECO SET LOGRADOURO = @LOGRADOURO, CEP = @CEP, 
                                NUMERO_ENDERECO = @NUMERO_ENDERECO, COMPLEMENTO = @COMPLEMENTO
                                CIDADE = @CIDADE, BAIRRO = @BAIRRO, ESTADO = @ESTADO, 
                                ENDERECO_PADRAO = @ENDERECO_PADRAO, UF = @UF WHERE ENDERECO_ID = @enderecoId", new 
                    { 
                        endereco.LOGRADOURO, 
                        endereco.CEP, 
                        endereco.NUMERO_ENDERECO, 
                        endereco.COMPLEMENTO, 
                        endereco.CIDADE, 
                        endereco.BAIRRO,
                        endereco.ESTADO, 
                        endereco.ENDERECO_PADRAO, 
                        endereco.UF, 
                        enderecoId
                    });
                }
            }
            catch (Exception)
            {

                throw;
            };
        }
    }
}
