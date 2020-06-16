using Orcamentaria.Model.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using OrcamentariaBackEnd.Database;

namespace OrcamentariaBackEnd.Repositories
{
    public class EquipamentoRepository : IEquipamentoRepository
    {
        private IConexao Conexao;
        private IPessoaRepository Pessoa;

        public EquipamentoRepository(IConexao conexao, IPessoaRepository pessoa)
        {
            this.Conexao = conexao;
            this.Pessoa = pessoa;
        }

        public EquipamentoModel Create(EquipamentoModel equipamento)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"INSERT INTO T_ORCA_EQUIPAMENTO (NOME_EQUIPAMENTO, DESCRICAO, PESSOA_ID, 
                                NOME_PESSOA) VALUES(@NOME_EQUIPAMENTO, @DESCRICAO, @PESSOA_ID, 
                                @NOME_PESSOA)", new 
                    { 
                        equipamento.NOME_EQUIPAMENTO, 
                        equipamento.DESCRICAO, 
                        equipamento.FABRICANTE.PESSOA_ID, 
                        equipamento.FABRICANTE.NOME_PESSOA
                    });

                    return Find(cn.Query<int>("SELECT LAST_INSERT_ID()").ToArray()[0]);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int equipamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute("DELETE FROM T_ORCA_EQUIPAMENTO WHERE EQUIPAMENTO_ID = @equipamentoId", new { equipamentoId });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public EquipamentoModel Find(int equipamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<EquipamentoModel>("SELECT * FROM T_ORCA_EQUIPAMENTO WHERE EQUIPAMENTO_ID = @equipamentoId", new { equipamentoId });

                    var pessoaId = cn.Query<int>("SELECT PESSOA_ID FROM T_ORCA_EQUIPAMENTO WHERE EQUIPAMENTO_ID = @equipamentoId", new { equipamentoId });

                    resposta.ToArray()[0].FABRICANTE = Pessoa.Find(pessoaId.ToArray()[0]);

                    return resposta.ToArray()[0];
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<EquipamentoModel> List()
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<EquipamentoModel>("SELECT * FROM T_ORCA_EQUIPAMENTO");

                    List<EquipamentoModel> listEquipamento = new List<EquipamentoModel>();

                    foreach (EquipamentoModel equipamento in resposta)
                    {
                        listEquipamento.Add(Find(equipamento.EQUIPAMENTO_ID));
                    }

                    return listEquipamento;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<EquipamentoModel> ListPorNomeEquipamento(string nomeEquipamento)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<EquipamentoModel>("SELECT * FROM T_ORCA_EQUIPAMENTO WHERE NOME_EQUIPAMENTO LIKE @nomeEquipamento", new { nomeEquipamento = nomeEquipamento + '%' });

                    List<EquipamentoModel> listEquipamento = new List<EquipamentoModel>();

                    foreach (EquipamentoModel equipamento in resposta)
                    {
                        listEquipamento.Add(Find(equipamento.EQUIPAMENTO_ID));
                    }

                    return listEquipamento;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(int equipamentoId, EquipamentoModel equipamento)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"UPDATE T_ORCA_EQUIPAMENTO SET NOME_EQUIPAMENTO = @NOME_EQUIPAMENTO, DESCRICAO = @DESCRICAO, 
                                NOME_PESSOA = @NOME_PESSOA WHERE EQUIPAMENTO_ID =  @equipamentoId", new 
                    { 
                        equipamento.NOME_EQUIPAMENTO, 
                        equipamento.DESCRICAO, 
                        equipamento.FABRICANTE.PESSOA_ID, 
                        equipamentoId
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
