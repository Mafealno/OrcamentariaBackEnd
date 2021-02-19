using Dapper;

using OrcamentariaBackEnd.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class TotaisOrcamentoRepository : ITotaisOrcamentoRepository
    {

        private IConexao Conexao;

        public TotaisOrcamentoRepository(IConexao conexao)
        {
            this.Conexao = conexao;
        }

        public TotaisOrcamentoModel Create(TotaisOrcamentoModel totaisOrcamento)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"INSERT INTO T_ORCA_TOTAIS_ORCAMENTO (ORCAMENTO_ID, TOTAIS_ITENS, TOTAIS_MAO_OBRA, 
                                TOTAIS_EQUIPAMENTOS, TOTAIS_MATERIAL, TOTAIS_CUSTOS, TOTAL_GERAL, AREA_TOTAL) VALUES(@ORCAMENTO_ID, 
                                @TOTAIS_ITENS, @TOTAIS_MAO_OBRA, @TOTAIS_EQUIPAMENTOS, @TOTAIS_MATERIAL, @TOTAIS_CUSTOS, @TOTAL_GERAL, 
                                @AREA_TOTAL)", totaisOrcamento);

                    return Find(cn.Query<int>("SELECT LAST_INSERT_ID()").FirstOrDefault());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int totaisOrcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute("DELETE FROM T_ORCA_TOTAIS_ORCAMENTO WHERE TOTAIS_ORCAMENTO_ID = @totaisOrcamentoId", new { totaisOrcamentoId });
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
                    cn.Execute("DELETE FROM T_ORCA_TOTAIS_ORCAMENTO WHERE ORCAMENTO_ID = @orcamentoId", new { orcamentoId });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TotaisOrcamentoModel Find(int totaisOrcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<TotaisOrcamentoModel>(@"SELECT * FROM T_ORCA_TOTAIS_ORCAMENTO 
                                                                    WHERE TOTAIS_ORCAMENTO_ID = @totaisOrcamentoId", new { totaisOrcamentoId });

                    if (resposta.Count() == 0)
                    {
                        return new TotaisOrcamentoModel();
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

        public TotaisOrcamentoModel FindPorOrcamentoId(int orcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<TotaisOrcamentoModel>(@"SELECT * FROM T_ORCA_TOTAIS_ORCAMENTO 
                                                                    WHERE ORCAMENTO_ID = @orcamentoId", new { orcamentoId });

                    if (resposta.Count() == 0)
                    {
                        return new TotaisOrcamentoModel();
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

        public IEnumerable<TotaisOrcamentoModel> List()
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<TotaisOrcamentoModel>(@"SELECT * FROM T_ORCA_TOTAIS_ORCAMENTO");

                    return resposta;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(int totaisOrcamentoId, TotaisOrcamentoModel totaisOrcamento)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"UPDATE T_ORCA_TOTAIS_ORCAMENTO SET TOTAIS_ITENS = @TOTAIS_ITENS, TOTAIS_MAO_OBRA = @TOTAIS_MAO_OBRA, 
                                TOTAIS_EQUIPAMENTOS = @TOTAIS_EQUIPAMENTOS, TOTAIS_MATERIAL = @TOTAIS_MATERIAL, 
                                TOTAIS_CUSTOS = @TOTAIS_CUSTOS, TOTAL_GERAL = @TOTAL_GERAL, AREA_TOTAL =  @AREA_TOTAL 
                                WHERE TOTAIS_ORCAMENTO_ID = @totaisOrcamentoId", new
                    {
                        totaisOrcamento.TOTAIS_ITENS,
                        totaisOrcamento.TOTAIS_MAO_OBRA,
                        totaisOrcamento.TOTAIS_EQUIPAMENTOS,
                        totaisOrcamento.TOTAIS_MATERIAL,
                        totaisOrcamento.TOTAIS_CUSTOS,
                        totaisOrcamento.TOTAL_GERAL,
                        totaisOrcamento.AREA_TOTAL,
                        totaisOrcamentoId
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
