using Dapper;

using OrcamentariaBackEnd.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class CustoOrcamentoRepository : ICustoOrcamentoRepository
    {

        private IConexao Conexao;

        public CustoOrcamentoRepository(IConexao conexao)
        {
            this.Conexao = conexao;
        }

        public CustoOrcamentoModel Create(CustoOrcamentoModel custoOrcamento)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"INSERT INTO T_ORCA_CUSTO_ORCAMENTO (CUSTO_ID, VALOR_CUSTO, ORCAMENTO_ID) 
                                VALUES(@CUSTO_ID, @VALOR_CUSTO, @ORCAMENTO_ID)", new
                    {
                                   custoOrcamento.CUSTO_OBRA.CUSTO_ID,
                                   custoOrcamento.VALOR_CUSTO,
                                   custoOrcamento.ORCAMENTO_ID
                    });

                    return Find(cn.Query<int>("SELECT LAST_INSERT_ID()").ToArray()[0]);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int custoOrcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute("DELETE FROM T_ORCA_CUSTO_ORCAMENTO WHERE CUSTO_ORCAMENTO_ID = @custoOrcamentoId", new { custoOrcamentoId });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeletePorOrcamentoId(int orcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute("DELETE FROM T_ORCA_CUSTO_ORCAMENTO WHERE ORCAMENTO_ID = @orcamentoId", new { orcamentoId });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CustoOrcamentoModel Find(int custoOrcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<CustoOrcamentoModel>("SELECT * FROM T_ORCA_CUSTO_ORCAMENTO WHERE CUSTO_ORCAMENTO_ID = @custoOrcamentoId", new { custoOrcamentoId });

                    if (resposta.Count() == 0)
                    {
                        return new CustoOrcamentoModel();
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

        public IEnumerable<CustoOrcamentoModel> List()
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<CustoOrcamentoModel>("SELECT * FROM T_ORCA_CUSTO_ORCAMENTO");

                    return resposta;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<CustoOrcamentoModel> ListPorOrcamentoId(int orcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<CustoOrcamentoModel>("SELECT * FROM T_ORCA_CUSTO_ORCAMENTO WHERE ORCAMENTO_ID = @orcamentoId", new { orcamentoId });

                    return resposta;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(int custoOrcamentoId, CustoOrcamentoModel custoOrcamento)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"UPDATE T_ORCA_CUSTO_ORCAMENTO SET CUSTO_ID = @CUSTO_ID, VALOR_CUSTO = @VALOR_CUSTO 
                                WHERE CUSTO_ORCAMENTO_ID = @custoOrcamentoId", new 
                    { 
                        custoOrcamento.CUSTO_OBRA.CUSTO_ID, 
                        custoOrcamento.CUSTO_OBRA.VALOR_CUSTO, 
                        custoOrcamentoId 
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
