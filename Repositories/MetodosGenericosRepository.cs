using Dapper;
using OrcamentariaBackEnd.Database;
using System;
using System.Linq;


namespace OrcamentariaBackEnd
{
    public class MetodosGenericosRepository : IMetodosGenericosRepository
    {

        private IConexao Conexao;

        public MetodosGenericosRepository(IConexao conexao)
        {
            this.Conexao = conexao;
        }

        public string DlookupOrcamentaria(string campoBuscado, string tabela, string where)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<string>("SELECT " + campoBuscado +  " FROM " + tabela + " WHERE " + where + " LIMIT 1");

                    if (resposta.Count() == 0)
                    {
                        return "";
                    }

                    return resposta.ToArray()[0];
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void StartTransactionCommitRollback(MetodosGenericosEnum metodosGenericos)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    switch (metodosGenericos)
                    {
                        case MetodosGenericosEnum.START:
                            cn.Execute("START TRANSACTION");
                            break;
                        case MetodosGenericosEnum.COMMIT:
                            cn.Execute("COMMIT");
                            break;
                        case MetodosGenericosEnum.ROLLBACK:
                            cn.Execute("ROLLBACK");
                            break;
                        default:
                            cn.Execute("ROLLBACK");
                            break;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
