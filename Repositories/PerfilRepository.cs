using Orcamentaria.Model.Cadastro;
using OrcamentariaBackEnd.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace OrcamentariaBackEnd.Repositories
{
    public class PerfilRepository : IPerfilRepository
    {
        private IConexao Conexao;

        public PerfilRepository(IConexao conexao)
        {
            this.Conexao = conexao;
        }

        public PerfilModel Create(PerfilModel perfil)
        {
            try
            {
                using(var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"INSERT INTO T_ORCA_PERFIL (NOME_PERFIL, VALOR_D, VALOR_BF, 
                                VALOR_TW, VALOR_TF, VALOR_KG_M, TIPO_PERFIL) VALUES(@NOME_PERFIL, @VALOR_D, @VALOR_BF, 
                                @VALOR_TW, @VALOR_TF, @VALOR_KG_M, @TIPO_PERFIL)", perfil);

                    return Find(cn.Query<int>("SELECT LAST_INSERT_ID()").ToArray()[0]);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int perfilId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute("DELETE FROM T_ORCA_PERFIL WHERE PERFIL_ID = @perfilId", new { perfilId });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public PerfilModel Find(int perfilId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<PerfilModel>("SELECT * FROM T_ORCA_PERFIL WHERE PERFIL_ID = @perfilId", new { perfilId });
                    return resposta.ToArray()[0];
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<PerfilModel> FindPorNomePerfil(string nomePerfil)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<PerfilModel>("SELECT * FROM T_ORCA_PERFIL WHERE PERFIL_ID LIKE @nomePerfil", new { nomePerfil = nomePerfil + '%' });
                    return resposta;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<PerfilModel> List()
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<PerfilModel>("SELECT * FROM T_ORCA_PERFIL");
                    return resposta;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(int perfilId, PerfilModel perfil)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"UPDATE T_ORCA_PERFIL SET NOME_PERFIL = @NOME_PERFIL, VALOR_D = @VALOR_D,
                                VALOR_BF = @VALOR_BF, VALOR_TW = @VALOR_TW, VALOR_TF = @VALOR_TF, VALOR_KG_M = @VALOR_KG_M, 
                                TIPO_PERFIL = @TIPO_PERFIL WHERE PERFIL_ID = @perfilId", new 
                    { 
                        perfil.NOME_PERFIL, 
                        perfil.VALOR_D, 
                        perfil.VALOR_BF, 
                        perfil.VALOR_TW, 
                        perfil.VALOR_TF,
                        perfil.VALOR_KG_M, 
                        perfil.TIPO_PERFIL, 
                        perfilId
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
