using Dapper;
using OrcamentariaBackEnd.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd.Repositories
{
    public class MaterialOrcamentoRepository : IMaterialOrcamentoRepository
    {

        private IConexao Conexao;

        public MaterialOrcamentoRepository(IConexao conexao)
        {
            this.Conexao = conexao;
        }

        public MaterialOrcamentoModel Create(MaterialOrcamentoModel materialOrcamento)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"INSERT INTO T_ORCA_MATERIAL_ORCAMENTO (ORCAMENTO_ID, VALOR_UNITARIO_MATERIAL, QTDE_MATERIAL, 
                                MATERIAL_ID, NOME_MATERIAL) VALUES(@ORCAMENTO_ID, @VALOR_UNITARIO_MATERIAL, @QTDE_MATERIAL, 
                                @MATERIAL_ID, @NOME_MATERIAL)", new
                    {
                        materialOrcamento.ORCAMENTO_ID,
                        materialOrcamento.VALOR_UNITARIO_MATERIAL,
                        materialOrcamento.QTDE_MATERIAL,
                        materialOrcamento.MATERIAL.MATERIAL_ID,
                        materialOrcamento.MATERIAL.NOME_MATERIAL
                    });

                    return Find(cn.Query<int>("SELECT MAX(MATERIAL_ORCAMENTO_ID) FROM T_ORCA_MATERIAL_ORCAMENTO").FirstOrDefault());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int materialOrcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute("DELETE FROM T_ORCA_MATERIAL_ORCAMENTO WHERE MATERIAL_ORCAMENTO_ID = @materialOrcamentoId", new { materialOrcamentoId });
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
                    cn.Execute("DELETE FROM T_ORCA_MATERIAL_ORCAMENTO WHERE ORCAMENTO_ID = @orcamentoId", new { orcamentoId });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public MaterialOrcamentoModel Find(int materialOrcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<MaterialOrcamentoModel>("SELECT * FROM T_ORCA_MATERIAL_ORCAMENTO WHERE MATERIAL_ORCAMENTO_ID = @materialOrcamentoId", new { materialOrcamentoId });

                    if (resposta.Count() == 0)
                    {
                        return new MaterialOrcamentoModel();
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

        public IEnumerable<MaterialOrcamentoModel> List()
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<MaterialOrcamentoModel>("SELECT * FROM T_ORCA_MATERIAL_ORCAMENTO");

                    return resposta;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<MaterialOrcamentoModel> ListPorOrcamentoId(int orcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<MaterialOrcamentoModel>("SELECT * FROM T_ORCA_MATERIAL_ORCAMENTO WHERE ORCAMENTO_ID = @orcamentoId", new { orcamentoId });

                    return resposta;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(int materialOrcamentoId, MaterialOrcamentoModel materialOrcamento)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"UPDATE T_ORCA_MATERIAL_ORCAMENTO SET VALOR_UNITARIO_MATERIAL = @VALOR_UNITARIO_MATERIAL, 
                                QTDE_MATERIAL = @QTDE_MATERIAL, MATERIAL_ID = @MATERIAL_ID, NOME_MATERIAL = @NOME_MATERIAL 
                                WHERE MATERIAL_ORCAMENTO_ID = @materialOrcamentoId", new
                    {
                        materialOrcamento.VALOR_UNITARIO_MATERIAL,
                        materialOrcamento.QTDE_MATERIAL,
                        materialOrcamento.MATERIAL.MATERIAL_ID,
                        materialOrcamento.MATERIAL.NOME_MATERIAL,
                        materialOrcamentoId
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
