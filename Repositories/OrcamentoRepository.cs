using Dapper;
using OrcamentariaBackEnd.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class OrcamentoRepository : IOrcamentoRepository
    {

        private IConexao Conexao;
        private IPessoaRepository Pessoa;
        private IMaoObraOrcamentoRepository MaoObraOrcamento;
        private IEquipamentoOrcamentoRepository EquipamentoOrcamento;
        private ICustoOrcamentoRepository CustoOrcamento;
        private ITotaisOrcamentoRepository TotaisOrcamento;

        public OrcamentoRepository(IConexao conexao, IPessoaRepository pessoaRepository, IMaoObraOrcamentoRepository maoObraOrcamentoRepository, 
            IEquipamentoOrcamentoRepository equipamentoOrcamentoRepository, ITotaisOrcamentoRepository totaisOrcamentoRepository,
            ICustoOrcamentoRepository custoOrcamentoRepository)
        {
            this.Conexao = conexao;
            this.Pessoa = pessoaRepository;
            this.MaoObraOrcamento = maoObraOrcamentoRepository;
            this.EquipamentoOrcamento = equipamentoOrcamentoRepository;
            this.CustoOrcamento = custoOrcamentoRepository;
            this.TotaisOrcamento = totaisOrcamentoRepository;
        }

        public OrcamentoModel Create(OrcamentoModel orcamento)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"INSERT INTO T_ORCA_ORCAMENTO (NOME_OBRA, REFERENCIA, PRAZO_ENTREGA, DATA_CRIACAO_ORCAMENTO, 
                                A_C, TIPO_OBRA, PESSOA_ID, NOME_CLIENTE, BAIRRO, CIDADE, UF) VALUES(@NOME_OBRA, @REFERENCIA, 
                                @PRAZO_ENTREGA, @DATA_CRIACAO_ORCAMENTO, @A_C, @TIPO_OBRA, @PESSOA_ID, @NOME_PESSOA, @BAIRRO, 
                                @CIDADE, UF)", new 
                    { 
                        orcamento.NOME_OBRA,
                        orcamento.REFERENCIA,
                        orcamento.PRAZO_ENTREGA,
                        orcamento.DATA_CRIACAO_ORCAMENTO,
                        orcamento.A_C,
                        orcamento.TIPO_OBRA,
                        orcamento.CLIENTE_ORCAMENTO.PESSOA_ID,
                        orcamento.CLIENTE_ORCAMENTO.NOME_PESSOA,
                        orcamento.CLIENTE_ORCAMENTO.LIST_ENDERECO[0].BAIRRO,
                        orcamento.CLIENTE_ORCAMENTO.LIST_ENDERECO[0].CIDADE,
                        orcamento.CLIENTE_ORCAMENTO.LIST_ENDERECO[0].UF
                    });

                    return Find(cn.Query<int>("SELECT LAST_INSERT_ID()").ToArray()[0]);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int orcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    MaoObraOrcamento.DeletePorOrcamentoId(orcamentoId);
                    EquipamentoOrcamento.DeletePorOrcamentoId(orcamentoId);
                    CustoOrcamento.DeletePorOrcamentoId(orcamentoId);
                    TotaisOrcamento.DeletePorOrcamentoId(orcamentoId);
                    cn.Execute("DELETE FROM T_ORCA_ORCAMENTO WHERE ORCAMENTO_ID = @orcamentoId", new { orcamentoId });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public OrcamentoModel Find(int orcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<OrcamentoModel>("SELECT * FROM T_ORCA_ORCAMENTO WHERE ORCAMENTO_ID = @orcamentoId", new { orcamentoId });

                    var pessoaId = cn.Query<int>("SELECT PESSOA_ID FROM T_ORCA_ORCAMENTO WHERE ORCAMENTO_ID = @orcamentoId", new { orcamentoId });

                    resposta.ToArray()[0].CLIENTE_ORCAMENTO = Pessoa.Find(pessoaId.ToArray()[0]);

                    resposta.ToArray()[0].LIST_MAO_OBRA_ORCAMENTO = MaoObraOrcamento.ListPorOrcamentoId(orcamentoId).ToList();
                    resposta.ToArray()[0].LIST_EQUIPAMENTO_ORCAMENTO = EquipamentoOrcamento.ListPorOrcamentoId(orcamentoId).ToList();
                    resposta.ToArray()[0].LIST_CUSTO_ORCAMENTO = CustoOrcamento.ListPorOrcamentoId(orcamentoId).ToList();

                    resposta.ToArray()[0].TOTAIS_ORCAMENTO = TotaisOrcamento.FindPorOrcamentoId(orcamentoId);

                    return resposta.ToArray()[0];
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<OrcamentoModel> List()
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<OrcamentoModel>("SELECT * FROM T_ORCA_ORCAMENTO");

                    List<OrcamentoModel> listOrcamento = new List<OrcamentoModel>();

                    foreach (OrcamentoModel orcamento in resposta)
                    {
                        listOrcamento.Add(Find(orcamento.ORCAMENTO_ID));
                    }

                    return listOrcamento;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(int orcamentoId, OrcamentoModel orcamento)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"UPDATE T_ORCA_ORCAMENTO SET NOME_OBRA = @NOME_OBRA, REFERENCIA = @REFERENCIA, 
                                PRAZO_ENTREGA = @PRAZO_ENTREGA, A_C = @A_C, TIPO_OBRA = @TIPO_OBRA, PESSOA_ID = @PESSOA_ID, 
                                NOME_CLIENTE = @NOME_PESSOA, BAIRRO = @BAIRRO, CIDADE = @CIDADE, UF = @UF WHERE 
                                ORCAMENTO_ID = @orcamentoId", new
                    {
                        orcamento.NOME_OBRA,
                        orcamento.REFERENCIA,
                        orcamento.PRAZO_ENTREGA,
                        orcamento.A_C,
                        orcamento.TIPO_OBRA,
                        orcamento.CLIENTE_ORCAMENTO.PESSOA_ID,
                        orcamento.CLIENTE_ORCAMENTO.NOME_PESSOA,
                        orcamento.CLIENTE_ORCAMENTO.LIST_ENDERECO[0].BAIRRO,
                        orcamento.CLIENTE_ORCAMENTO.LIST_ENDERECO[0].CIDADE,
                        orcamento.CLIENTE_ORCAMENTO.LIST_ENDERECO[0].UF,
                        orcamentoId
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
