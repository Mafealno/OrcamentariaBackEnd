using Orcamentaria.Model.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using OrcamentariaBackEnd.Database;

namespace OrcamentariaBackEnd.Repositories
{
    public class CustoRepository : ICustoRepository
    {
        private IConexao Conexao;

        public CustoRepository(IConexao conexao, IPessoaRepository pessoa)
        {
            this.Conexao = conexao;
        }

        public CustoModel Create(CustoModel custo)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"INSERT INTO T_ORCA_CUSTO (NOME_CUSTO, DESCRICAO, TIPO_CUSTO, VALOR_CUSTO) 
                                VALUES(@NOME_CUSTO, @DESCRICAO, @TIPO_CUSTO, @VALOR_CUSTO)", custo);

                    return Find(cn.Query<int>("SELECT LAST_INSERT_ID()").ToArray()[0]);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(int custoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute("DELETE FROM T_ORCA_CUSTO WHERE CUSTO_ID = @custoId", new { custoId });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CustoModel Find(int custoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<CustoModel>("SELECT * FROM T_ORCA_CUSTO WHERE CUSTO_ID = @custoId", new { custoId });
                    return resposta.ToArray()[0];
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<CustoModel> List()
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<CustoModel>("SELECT * FROM T_ORCA_CUSTO");
                    return resposta;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<CustoModel> ListPorNomeCusto(string nomeCusto)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<CustoModel>("SELECT * FROM T_ORCA_CUSTO WHERE NOME_CUSTO LIKE @nomeCusto", new { nomeCusto = nomeCusto + "%"});
                    return resposta;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(int custoId, CustoModel custo)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"UPDATE T_ORCA_CUSTO SET NOME_CUSTO = @NOME_CUSTO, DESCRICAO = @DESCRICAO, 
                                TIPO_CUSTO = @TIPO_CUSTO, VALOR_CUSTO = @VALOR_CUSTO WHERE CUSTO_ID = @custoId", new 
                    { 
                        custo.NOME_CUSTO, 
                        custo.DESCRICAO, 
                        custo.TIPO_CUSTO, 
                        custo.VALOR_CUSTO, 
                        custoId
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
