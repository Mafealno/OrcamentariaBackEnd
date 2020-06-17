using Dapper;

using OrcamentariaBackEnd.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class ItensCartaCoberturaRepository : IItensCartaCoberturaRepository
    {

        private IConexao Conexao;

        public ItensCartaCoberturaRepository(IConexao conexao)
        {
            this.Conexao = conexao;
        }

        public ItensCartaCoberturaModel Create(ItensCartaCoberturaModel itensCartaCobertura)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"INSERT INTO T_ORCA_ITENS_CARTA_COBERTURA (CARTA_COBERTURA_ID, VALOR_HP_A, 
                                TEMPO_RESISTENCIA_FOGO, VALOR_ESPESSURA) VALUES(@CARTA_COBERTURA_ID, 
                                @VALOR_HP_A, @TEMPO_RESISTENCIA_FOGO, @VALOR_ESPESSURA)", itensCartaCobertura);

                    return Find(cn.Query<int>("SELECT LAST_INSERT_ID()").ToArray()[0]);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int itensCartaCoberturaId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute("DELETE FROM T_ORCA_ITENS_CARTA_COBERTURA WHERE ITENS_CARTA_COBERTURA_ID = @itensCartaCoberturaId", new { itensCartaCoberturaId });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeletePorCartaCoberturaId(int cartaCoberturaId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute("DELETE FROM T_ORCA_ITENS_CARTA_COBERTURA WHERE CARTA_COBERTURA_ID = @cartaCoberturaId", new { cartaCoberturaId });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ItensCartaCoberturaModel Find(int itensCartaCoberturaId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<ItensCartaCoberturaModel>("SELECT * FROM T_ORCA_ITENS_CARTA_COBERTURA WHERE ITENS_CARTA_COBERTURA_ID = @itensCartaCoberturaId", new { itensCartaCoberturaId });
                    return resposta.ToArray()[0];
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ItensCartaCoberturaModel FindPorValorHpaTempoResistenciaFogo(string valorHpa, string tempoResistenciaFogo)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<ItensCartaCoberturaModel>(@"SELECT * FROM T_ORCA_ITENS_CARTA_COBERTURA WHERE VALOR_HP_A = @valorHpa AND
                                                                      TEMPO_RESISTENCIA_FOGO = @tempoResistenciaFogo", new { valorHpa, tempoResistenciaFogo });
                    return resposta.ToArray()[0];
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ItensCartaCoberturaModel> List()
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<ItensCartaCoberturaModel>("SELECT * FROM T_ORCA_ITENS_CARTA_COBERTURA");
                    return resposta;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ItensCartaCoberturaModel> ListPorCartaCoberturaId(int cartaCoberturaId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<ItensCartaCoberturaModel>("SELECT * FROM T_ORCA_ITENS_CARTA_COBERTURA WHERE CARTA_COBERTURA_ID = @cartaCoberturaId", new { cartaCoberturaId });
                    return resposta;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ItensCartaCoberturaModel> ListPorTempoResistenciaFogo(string tempoResistenciaFogo)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<ItensCartaCoberturaModel>("SELECT * FROM T_ORCA_ITENS_CARTA_COBERTURA WHERE TEMPO_RESISTENCIA_FOGO = @tempoResistenciaFogo", new { tempoResistenciaFogo });
                    return resposta;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(int itensCartaCoberturaId, ItensCartaCoberturaModel itensCartaCobertura)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"UPDATE T_ORCA_ITENS_CARTA_COBERTURA SET VALOR_HP_A = @VALOR_HP_A, TEMPO_RESISTENCIA_FOGO = @TEMPO_RESISTENCIA_FOGO,
                                VALOR_ESPESSURA = @VALOR_ESPESSURA WHERE ITENS_CARTA_COBERTURA_ID = @itensCartaCoberturaId", new 
                    { 
                        itensCartaCobertura.VALOR_HP_A, 
                        itensCartaCobertura.TEMPO_RESISTENCIA_FOGO, 
                        itensCartaCobertura.VALOR_ESPESSURA,
                        itensCartaCoberturaId
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
