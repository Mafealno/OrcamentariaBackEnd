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

        public ItensOrcamentoGeralRepository(IConexao conexao)
        {
            this.Conexao = conexao;
        }

        public ItensOrcamentoGeralModel Create(ItensOrcamentoGeralModel itensOrcamentoGeral)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"INSERT INTO T_ORCA_ITENS_ORCAMENTO_GERAL (ITENS_ORCAMENTO_ID, ORCAMENTO_ID, 
                                AMBIENTE_APLICACAO, LOCAL_APLICACAO, ACAO_APLICAR, VALOR_LARGURA, VALOR_M_2,
                                MATERIAL_ID, NOME_MATERIAL, DESCRICAO_MATERIAL) VALUES(@ITENS_ORCAMENTO_ID, 
                                @ORCAMENTO_ID, @AMBIENTE_APLICACAO, @LOCAL_APLICACAO, @ACAO_APLICAR, 
                                @VALOR_LARGURA, @VALOR_M_2, @MATERIAL_ID, @NOME_MATERIAL, @DESCRICAO_MATERIAL)", new
                                {
                                    itensOrcamentoGeral.ITENS_ORCAMENTO_ID,
                                    itensOrcamentoGeral.ORCAMENTO_ID,
                                    itensOrcamentoGeral.AMBIENTE_APLICACAO,
                                    itensOrcamentoGeral.LOCAL_APLICACAO,
                                    itensOrcamentoGeral.ACAO_APLICAR,
                                    itensOrcamentoGeral.VALOR_LARGURA,
                                    itensOrcamentoGeral.VALOR_M_2,
                                    itensOrcamentoGeral.PRODUTO.MATERIAL_ID,
                                    itensOrcamentoGeral.PRODUTO.NOME_MATERIAL,
                                    itensOrcamentoGeral.PRODUTO.DESCRICAO_MATERIAL
                    });

                    return Find(itensOrcamentoGeral.ITENS_ORCAMENTO_ID);
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
                    var resposta = cn.Query<ItensOrcamentoGeralModel>(@"SELECT * FROM T_ORCA_ITENS_ORCAMENTO_GERAL INNER JOIN T_ORCA_ITENS_ORCAMENTO 
                                                                        ON T_ORCA_ITENS_ORCAMENTO.ITENS_ORCAMENTO_ID = T_ORCA_ITENS_ORCAMENTO_GERAL.ITENS_ORCAMENTO_ID 
                                                                        WHERE T_ORCA_ITENS_ORCAMENTO_GERAL.ITENS_ORCAMENTO_ID = @itensOrcamentoId", new { itensOrcamentoId });

                    if (resposta.Count() == 0)
                    {
                        return new ItensOrcamentoGeralModel();
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

        public IEnumerable<ItensOrcamentoGeralModel> List()
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<ItensOrcamentoGeralModel>(@"SELECT * FROM T_ORCA_ITENS_ORCAMENTO_GERAL INNER JOIN T_ORCA_ITENS_ORCAMENTO_GERAL 
                                                                        ON T_ORCA_ITENS_ORCAMENTO.ITENS_ORCAMENTO_ID = T_ORCA_ITENS_ORCAMENTO_GERAL.ITENS_ORCAMENTO_ID");

                    return resposta;
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
                    var resposta = cn.Query<ItensOrcamentoGeralModel>(@"SELECT * FROM T_ORCA_ITENS_ORCAMENTO_GERAL INNER JOIN T_ORCA_ITENS_ORCAMENTO 
                                                                    ON T_ORCA_ITENS_ORCAMENTO.ITENS_ORCAMENTO_ID = T_ORCA_ITENS_ORCAMENTO_GERAL.ITENS_ORCAMENTO_ID
                                                                    WHERE T_ORCA_ITENS_ORCAMENTO.ORCAMENTO_ID = @orcamentoId", new { orcamentoId });

                    return resposta;
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
                    cn.Execute(@"UPDATE T_ORCA_ITENS_ORCAMENTO_GERAL SET AMBIENTE_APLICACAO = @AMBIENTE_APLICACAO, 
                                LOCAL_APLICACAO = @LOCAL_APLICACAO, ACAO_APLICAR = @ACAO_APLICAR, 
                                VALOR_LARGURA = @VALOR_LARGURA, VALOR_M_2 = @VALOR_M_2, MATERIAL_ID = @MATERIAL_ID,
                                NOME_MATERIAL = @NOME_MATERIAL, DESCRICAO_MATERIAL = @DESCRICAO_MATERIAL
                                WHERE ITENS_ORCAMENTO_ID = @itensOrcamentoId", new
                    {
                        itensOrcamentoGeral.AMBIENTE_APLICACAO,
                        itensOrcamentoGeral.LOCAL_APLICACAO,
                        itensOrcamentoGeral.ACAO_APLICAR,
                        itensOrcamentoGeral.VALOR_LARGURA,
                        itensOrcamentoGeral.VALOR_M_2,
                        itensOrcamentoGeral.PRODUTO.MATERIAL_ID,
                        itensOrcamentoGeral.PRODUTO.NOME_MATERIAL,
                        itensOrcamentoGeral.PRODUTO.DESCRICAO_MATERIAL,
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
