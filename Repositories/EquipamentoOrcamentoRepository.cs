using Dapper;


using OrcamentariaBackEnd.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class EquipamentoOrcamentoRepository : IEquipamentoOrcamentoRepository
    {

        private IConexao Conexao;
        private IEquipamentoRepository Equipamento;

        public EquipamentoOrcamentoRepository(IConexao conexao, IEquipamentoRepository equipamento)
        {
            this.Conexao = conexao;
            this.Equipamento = equipamento;
        }

        public EquipamentoOrcamentoModel Create(EquipamentoOrcamentoModel equipamentoOrcamento)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"INSERT INTO T_ORCA_EQUIPAMENTO_ORCAMENTO (ORCAMENTO_ID, VALOR_UNITARIO_EQUIPAMENTO, QTDE_EQUIPAMENTO, 
                                EQUIPAMENTO_ID, NOME_EQUIPAMENTO) VALUES(@ORCAMENTO_ID, @VALOR_UNITARIO_EQUIPAMENTO, @QTDE_EQUIPAMENTO, 
                                @EQUIPAMENTO_ID, @NOME_EQUIPAMENTO)", equipamentoOrcamento);

                    return Find(cn.Query<int>("SELECT LAST_INSERT_ID()").ToArray()[0]);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int equipamentoOrcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute("DELETE FROM T_ORCA_EQUIPAMENTO_ORCAMENTO WHERE EQUIPAMENTO_ORCAMENTO_ID = @equipamentoOrcamentoId", new { equipamentoOrcamentoId });
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
                    cn.Execute("DELETE FROM T_ORCA_EQUIPAMENTO_ORCAMENTO WHERE ORCAMENTO_ID = @orcamentoId", new { orcamentoId });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public EquipamentoOrcamentoModel Find(int equipamentoOrcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<EquipamentoOrcamentoModel>("SELECT * FROM T_ORCA_EQUIPAMENTO_ORCAMENTO WHERE EQUIPAMENTO_ORCAMENTO_ID = @equipamentoOrcamentoId", new { equipamentoOrcamentoId });

                    var equipamentoId = cn.Query<int>("SELECT * FROM T_ORCA_EQUIPAMENTO_ORCAMENTO WHERE EQUIPAMENTO_ORCAMENTO_ID = @equipamentoOrcamentoId", new { equipamentoOrcamentoId });

                    resposta.ToArray()[0].EQUIPAMENTO = Equipamento.Find(equipamentoId.ToArray()[0]);

                    return resposta.ToArray()[0];
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<EquipamentoOrcamentoModel> List()
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<EquipamentoOrcamentoModel>("SELECT * FROM T_ORCA_EQUIPAMENTO_ORCAMENTO");

                    List<EquipamentoOrcamentoModel> listEquipamentoOrcamento = new List<EquipamentoOrcamentoModel>();

                    foreach (EquipamentoOrcamentoModel equipamentoOrcamento in resposta)
                    {
                        listEquipamentoOrcamento.Add(Find(equipamentoOrcamento.EQUIPAMENTO_ORCAMENTO_ID));
                    }

                    return listEquipamentoOrcamento;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<EquipamentoOrcamentoModel> ListPorOrcamentoId(int orcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<EquipamentoOrcamentoModel>("SELECT * FROM T_ORCA_EQUIPAMENTO_ORCAMENTO WHERE ORCAMENTO_ID = @orcamentoId", new { orcamentoId });

                    List<EquipamentoOrcamentoModel> listEquipamentoOrcamento = new List<EquipamentoOrcamentoModel>();

                    foreach (EquipamentoOrcamentoModel equipamentoOrcamento in resposta)
                    {
                        listEquipamentoOrcamento.Add(Find(equipamentoOrcamento.EQUIPAMENTO_ORCAMENTO_ID));
                    }

                    return listEquipamentoOrcamento;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(int equipamentoOrcamentoId, EquipamentoOrcamentoModel equipamentoOrcamento)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"UPDATE T_ORCA_EQUIPAMENTO_ORCAMENTO SET VALOR_UNITARIO_EQUIPAMENTO = @VALOR_UNITARIO_EQUIPAMENTO, 
                                EQUIPAMENTO_ID = @EQUIPAMENTO_ID, NOME_EQUIPAMENTO = @NOME_EQUIPAMENTO WHERE 
                                EQUIPAMENTO_ORCAMENTO_ID = @equipamentoOrcamentoId", new 
                    {
                        equipamentoOrcamento.VALOR_UNITARIO_EQUIPAMENTO,
                        equipamentoOrcamento.QTDE_EQUIPAMENTO, 
                        equipamentoOrcamento.EQUIPAMENTO.EQUIPAMENTO_ID,
                        equipamentoOrcamento.EQUIPAMENTO.NOME_EQUIPAMENTO, 
                        equipamentoOrcamentoId 
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
