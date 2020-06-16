using Dapper;
using Orcamentaria.Model.Orcamento;
using OrcamentariaBackEnd.Database;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd.Repositories
{
    public class ItensOrcamentoIntumescenteRepository : IItensOrcamentoIntumescenteRepository
    {

        private IConexao Conexao;
        private IMaterialRepository Material;
        private IPerfilRepository Perfil;

        public ItensOrcamentoIntumescenteRepository(IConexao conexao, IMaterialRepository material, IPerfilRepository perfilRepository)
        {
            this.Conexao = conexao;
            this.Material = material;
            this.Perfil = perfilRepository;
        }

        public ItensOrcamentoIntumescenteModel Create(ItensOrcamentoIntumescenteModel itensOrcamentoIntumescente)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"INSERT INTO T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE (ITENS_ORCAMENTO_ID, ORCAMENTO_ID, REFERENCIA, NUMERO_FACES,
                                VALOR_HP, VALOR_HP_A, VALOR_WD, QTDE, VALOR_ESPESSURA, QTDE_LITROS, VALOR_D, VALOR_BF, 
                                VALOR_TW, VALOR_TF, VALOR_KG_M) VALUES(@ITENS_ORCAMENTO_ID, @ORCAMENTO_ID, @REFERENCIA, @NUMERO_FACES,
                                @VALOR_HP, @VALOR_HP_A, @VALOR_WD, @QTDE, @VALOR_ESPESSURA, @QTDE_LITROS, @VALOR_D, @VALOR_BF, 
                                @VALOR_TW, @VALOR_TF, @VALOR_KG_M)", new
                    {
                        itensOrcamentoIntumescente.ITENS_ORCAMENTO_ID,
                        itensOrcamentoIntumescente.ORCAMENTO_ID,
                        itensOrcamentoIntumescente.REFERENCIA,
                        itensOrcamentoIntumescente.NUMERO_FACES,
                        itensOrcamentoIntumescente.VALOR_HP,
                        itensOrcamentoIntumescente.VALOR_HP_A,
                        itensOrcamentoIntumescente.PERFIL.PERFIL_ID,
                        itensOrcamentoIntumescente.PERFIL.VALOR_D,
                        itensOrcamentoIntumescente.PERFIL.VALOR_BF,
                        itensOrcamentoIntumescente.PERFIL.VALOR_TW,
                        itensOrcamentoIntumescente.PERFIL.VALOR_TF,
                        itensOrcamentoIntumescente.PERFIL.VALOR_KG_M
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
                    cn.Execute("DELETE FROM T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE WHERE ITENS_ORCAMENTO_ID = @itensOrcamentoId", new { itensOrcamentoId });
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
                    cn.Execute("DELETE FROM T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE WHERE ORCAMENTO_ID = @orcamentoId", new { orcamentoId });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ItensOrcamentoIntumescenteModel Find(int itensOrcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<ItensOrcamentoIntumescenteModel>(@"SELECT * FROM T_ORCA_ITENS_ORCAMENTO INNER JOIN T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE 
                                                                            ON T_ORCA_ITENS_ORCAMENTO.ITENS_ORCAMENTO_ID = T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE.ITENS_ORCAMENTO_ID 
                                                                            WHERE ITENS_ORCAMENTO_ID = @itensOrcamentoId", new { itensOrcamentoId });

                    var materialId = cn.Query<int>(@"SELECT MATERIAL_ID FROM T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE WHERE ITENS_ORCAMENTO_ID = @itensOrcamentoId", new { itensOrcamentoId });
                    var perfilId = cn.Query<int>(@"SELECT PERFIL_ID FROM T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE WHERE ITENS_ORCAMENTO_ID = @itensOrcamentoId", new { itensOrcamentoId });

                    resposta.ToArray()[0].PRODUTO = Material.Find(materialId.ToArray()[0]);
                    resposta.ToArray()[0].PERFIL = Perfil.Find(perfilId.ToArray()[0]);

                    return resposta.ToArray()[0];
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ItensOrcamentoIntumescenteModel> List()
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<ItensOrcamentoIntumescenteModel>(@"SELECT * FROM T_ORCA_ITENS_ORCAMENTO INNER JOIN T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE 
                                                                                ON T_ORCA_ITENS_ORCAMENTO.ITENS_ORCAMENTO_ID = T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE.ITENS_ORCAMENTO_ID");

                    List<ItensOrcamentoIntumescenteModel> listItensOrcamentoIntumescente = new List<ItensOrcamentoIntumescenteModel>();

                    foreach (ItensOrcamentoIntumescenteModel itensOrcamentoIntumescente in resposta)
                    {
                        listItensOrcamentoIntumescente.Add(Find(itensOrcamentoIntumescente.ITENS_ORCAMENTO_ID));
                    }

                    return listItensOrcamentoIntumescente;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ItensOrcamentoIntumescenteModel> ListPorOrcamentoId(int orcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<ItensOrcamentoIntumescenteModel>(@"SELECT * FROM T_ORCA_ITENS_ORCAMENTO INNER JOIN T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE 
                                                                                ON T_ORCA_ITENS_ORCAMENTO.ITENS_ORCAMENTO_ID = T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE.ITENS_ORCAMENTO_ID
                                                                                WHERE ORCAMENTO_ID = @orcamentoId", new { orcamentoId });

                    List<ItensOrcamentoIntumescenteModel> listItensOrcamentoIntumescente = new List<ItensOrcamentoIntumescenteModel>();

                    foreach (ItensOrcamentoIntumescenteModel itensOrcamentoIntumescente in resposta)
                    {
                        listItensOrcamentoIntumescente.Add(Find(itensOrcamentoIntumescente.ITENS_ORCAMENTO_ID));
                    }

                    return listItensOrcamentoIntumescente;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(int itensOrcamentoId, ItensOrcamentoIntumescenteModel itensOrcamentoIntumescente)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"UPDATE T_ORCA_ITENS_ORCAMENTO_GERAL SET REFERENCIA = @REFERENCIA, NUMERO_FACES = @NUMERO_FACES,
                                VALOR_HP = @VALOR_HP, VALOR_HP_A = @VALOR_HP_A, VALOR_WD = @VALOR_WD, QTDE = @QTDE, 
                                VALOR_ESPESSURA = @VALOR_ESPESSURA, QTDE_LITROS = @QTDE_LITROS, VALOR_D = @VALOR_D, 
                                VALOR_BF = @VALOR_BF, VALOR_TW = @VALOR_TW, VALOR_TF = @VALOR_TF, VALOR_KG_M = @VALOR_KG_M 
                                WHERE ITENS_ORCAMENTO_ID = @itensOrcamentoId", new
                    {
                        itensOrcamentoIntumescente.REFERENCIA,
                        itensOrcamentoIntumescente.NUMERO_FACES,
                        itensOrcamentoIntumescente.VALOR_HP,
                        itensOrcamentoIntumescente.VALOR_HP_A,
                        itensOrcamentoIntumescente.PERFIL.PERFIL_ID,
                        itensOrcamentoIntumescente.PERFIL.VALOR_D,
                        itensOrcamentoIntumescente.PERFIL.VALOR_BF,
                        itensOrcamentoIntumescente.PERFIL.VALOR_TW,
                        itensOrcamentoIntumescente.PERFIL.VALOR_TF,
                        itensOrcamentoIntumescente.PERFIL.VALOR_KG_M,
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
