using Dapper;

using OrcamentariaBackEnd.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class ItensOrcamentoGeralRepository : IItensOrcamentoGeralRepository
    {

        private IConexao Conexao;
        private IMaterialRepository Material;

        public ItensOrcamentoGeralRepository(IConexao conexao, IMaterialRepository material)
        {
            this.Conexao = conexao;
            this.Material = material;
        }

        public ItensOrcamentoGeralModel Create(ItensOrcamentoGeralModel itensOrcamentoGeral)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"INSERT INTO T_ORCA_ITENS_ORCAMENTO_GERAL (ITENS_ORCAMENTO_ID,ORCAMENTO_ID, AMBIENTE_APLICACAO, VALOR_LARGURA, VALOR_M_2) 
                                VALUES(@ITENS_ORCAMENTO_ID, @ORCAMENTO_ID, @AMBIENTE_APLICACAO, @VALOR_LARGURA, @VALOR_M_2)", new
                                {
                                    itensOrcamentoGeral.ITENS_ORCAMENTO_ID,
                                    itensOrcamentoGeral.ORCAMENTO_ID,
                                    itensOrcamentoGeral.AMBIENTE_APLICACAO,
                                    itensOrcamentoGeral.VALOR_LARGURA,
                                    itensOrcamentoGeral.VALOR_M_2
                                });

                    return Find(cn.Query<int>("SELECT LAST_INSERT_ID()").ToArray()[0]);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int itensOrcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute("DELETE FROM T_ORCA_ITENS_ORCAMENTO_GERAL WHERE ITENS_ORCAMENTO_ID = @itensOrcamentoId", new { itensOrcamentoId });
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
                    cn.Execute("DELETE FROM T_ORCA_ITENS_ORCAMENTO_GERAL WHERE ORCAMENTO_ID = @orcamentoId", new { orcamentoId });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ItensOrcamentoGeralModel Find(int itensOrcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<ItensOrcamentoGeralModel>(@"SELECT * FROM T_ORCA_ITENS_ORCAMENTO_GERAL INNER JOIN T_ORCA_ITENS_ORCAMENTO_GERAL 
                                                                        ON T_ORCA_ITENS_ORCAMENTO.ITENS_ORCAMENTO_ID = T_ORCA_ITENS_ORCAMENTO_GERAL.ITENS_ORCAMENTO_ID 
                                                                        WHERE ITENS_ORCAMENTO_ID = @itensOrcamentoId", new { itensOrcamentoId });

                    var materialId = cn.Query<int>(@"SELECT MATERIAL_ID FROM T_ORCA_ITENS_ORCAMENTO_GERAL WHERE ITENS_ORCAMENTO_ID = @itensOrcamentoId", new { itensOrcamentoId });

                    resposta.ToArray()[0].PRODUTO = Material.Find(materialId.ToArray()[0]);

                    return resposta.ToArray()[0];
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ItensOrcamentoGeralModel> List()
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<ItensOrcamentoGeralModel>(@"SELECT * FROM T_ORCA_ITENS_ORCAMENTO_GERAL INNER JOIN T_ORCA_ITENS_ORCAMENTO_GERAL 
                                                                        ON T_ORCA_ITENS_ORCAMENTO.ITENS_ORCAMENTO_ID = T_ORCA_ITENS_ORCAMENTO_GERAL.ITENS_ORCAMENTO_ID");

                    List<ItensOrcamentoGeralModel> listItensOrcamentoGeral = new List<ItensOrcamentoGeralModel>();

                    foreach (ItensOrcamentoGeralModel ItensOrcamentoGeral in resposta)
                    {
                        listItensOrcamentoGeral.Add(Find(ItensOrcamentoGeral.ITENS_ORCAMENTO_ID));
                    }

                    return listItensOrcamentoGeral;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ItensOrcamentoGeralModel> ListPorOrcamentoId(int orcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<ItensOrcamentoGeralModel>(@"SELECT * FROM T_ORCA_ITENS_ORCAMENTO_GERAL INNER JOIN T_ORCA_ITENS_ORCAMENTO_GERAL 
                                                                    ON T_ORCA_ITENS_ORCAMENTO.ITENS_ORCAMENTO_ID = T_ORCA_ITENS_ORCAMENTO_GERAL.ITENS_ORCAMENTO_ID
                                                                    WHERE ORCAMENTO_ID = @orcamentoId", new { orcamentoId });

                    List<ItensOrcamentoGeralModel> listItensOrcamentoGeral = new List<ItensOrcamentoGeralModel>();

                    foreach (ItensOrcamentoGeralModel ItensOrcamentoGeral in resposta)
                    {
                        listItensOrcamentoGeral.Add(Find(ItensOrcamentoGeral.ITENS_ORCAMENTO_ID));
                    }

                    return listItensOrcamentoGeral;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(int itensOrcamentoId, ItensOrcamentoGeralModel itensOrcamentoGeral)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"UPDATE T_ORCA_ITENS_ORCAMENTO_GERAL SET AMBIENTE_APLICACAO = @AMBIENTE_APLICACAO, VALOR_LARGURA = @VALOR_LARGURA,
                                VALOR_M_2 = @VALOR_M_2 WHERE ITENS_ORCAMENTO_ID = @itensOrcamentoId", new
                    {
                        itensOrcamentoGeral.AMBIENTE_APLICACAO,
                        itensOrcamentoGeral.VALOR_LARGURA,
                        itensOrcamentoGeral.VALOR_M_2,
                        itensOrcamentoId
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
