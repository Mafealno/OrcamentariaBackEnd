using Dapper;


using OrcamentariaBackEnd.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class CustosMaoObraRepository : ICustosMaoObraRepository
    {

        private IConexao Conexao;

        public CustosMaoObraRepository(IConexao conexao)
        {
            this.Conexao = conexao;
        }

        public void Create(MaoObraOrcamentoModel maoObraOrcamento, CustoModel custo)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"INSERT INTO T_ORCA_CUSTOS_MAO_OBRA (MAO_OBRA_ORCAMENTO_ID, CUSTO_ID, NOME_CUSTO, 
                                TIPO_CUSTO, VALOR_CUSTO) VALUES(@MAO_OBRA_ORCAMENTO_ID, @CUSTO_ID, @NOME_CUSTO, 
                                @TIPO_CUSTO, @VALOR_CUSTO)", new
                    {
                        maoObraOrcamento.MAO_OBRA_ORCAMENTO_ID,
                        custo.CUSTO_ID,
                        custo.NOME_CUSTO,
                        custo.TIPO_CUSTO,
                        custo.VALOR_CUSTO
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int maoObraOrcamentoId, int custoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"DELETE FROM T_ORCA_CUSTOS_MAO_OBRA WHERE MAO_OBRA_ORCAMENTO_ID = @maoObraOrcamentoId 
                                AND CUSTO_ID = @custoId", new 
                    { 
                        maoObraOrcamentoId,
                        custoId
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeletePorMaoObraOrcamentoId(int maoObraOrcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"DELETE FROM T_ORCA_CUSTOS_MAO_OBRA WHERE MAO_OBRA_ORCAMENTO_ID = @maoObraOrcamentoId", 
                                new { maoObraOrcamentoId });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CustoModel Find(int maoObraOrcamentoId, int custoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<CustoModel>(@"SELECT T_ORCA_CUSTO.CUSTO_ID,T_ORCA_CUSTO.NOME_CUSTO, T_ORCA_CUSTO.DESCRICAO,
                                                        T_ORCA_CUSTO.TIPO_CUSTO, T_ORCA_CUSTOS_MAO_OBRA.VALOR_CUSTO FROM T_ORCA_CUSTOS_MAO_OBRA 
                                                        INNER JOIN T_ORCA_CUSTO ON T_ORCA_CUSTOS_MAO_OBRA.CUSTO_ID = T_ORCA_CUSTO.CUSTO_ID WHERE 
                                                        T_ORCA_CUSTOS_MAO_OBRA.MAO_OBRA_ORCAMENTO_ID = @maoObraOrcamentoId AND T_ORCA_CUSTO.CUSTO_ID = @custoId", new  
                    { 
                        maoObraOrcamentoId,
                        custoId
                    });

                    if (resposta.Count() == 0)
                    {
                        return new CustoModel();
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

        public IEnumerable<CustoModel> List(int maoObraOrcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<CustoModel>(@"SELECT T_ORCA_CUSTO.CUSTO_ID,T_ORCA_CUSTO.NOME_CUSTO, T_ORCA_CUSTO.DESCRICAO,
                                                        T_ORCA_CUSTO.TIPO_CUSTO, T_ORCA_CUSTOS_MAO_OBRA.VALOR_CUSTO FROM T_ORCA_CUSTOS_MAO_OBRA 
                                                        INNER JOIN T_ORCA_CUSTO ON T_ORCA_CUSTOS_MAO_OBRA.CUSTO_ID = T_ORCA_CUSTO.CUSTO_ID WHERE 
                                                        T_ORCA_CUSTOS_MAO_OBRA.MAO_OBRA_ORCAMENTO_ID = @maoObraOrcamentoId", new { maoObraOrcamentoId });

                    return resposta;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(int maoObraOrcamentoId, int custoId, MaoObraOrcamentoModel maoObraOrcamento, CustoModel custo)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"UPDATE T_ORCA_CUSTOS_MAO_OBRA SET CUSTO_ID = @CUSTO_ID, NOME_CUSTO = @NOME_CUSTO, 
                                TIPO_CUSTO = @TIPO_CUSTO, VALOR_CUSTO = @VALOR_CUSTO WHERE MAO_OBRA_ORCAMENTO_ID = @maoObraOrcamentoId 
                                AND CUSTO_ID = @custoId", new
                    {
                        custo.CUSTO_ID,
                        custo.NOME_CUSTO,
                        custo.TIPO_CUSTO,
                        custo.VALOR_CUSTO,
                        maoObraOrcamentoId,
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
