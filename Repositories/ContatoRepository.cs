using Orcamentaria.Model.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using OrcamentariaBackEnd.Database;
using Renci.SshNet;

namespace OrcamentariaBackEnd.Repositories.Cadastro
{
    public class ContatoRepository : IContatoRepository
    {

        private IConexao Conexao;

        public ContatoRepository(IConexao conexao)
        {
            this.Conexao = conexao;
        }

        public ContatoModel Create(ContatoModel contato)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"INSERT INTO T_ORCA_CONTATO (PESSOA_ID, CONTATO, TIPO_CONTATO, 
                                DDD, RAMAL, CONTATO_PADRAO) VALUES(@PESSOA_ID, @CONTATO, @TIPO_CONTATO, 
                                @DDD, @RAMAL, @CONTATO_PADRAO)", contato);

                    return Find(cn.Query<int>("SELECT LAST_INSERT_ID()").ToArray()[0]);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int contatoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute("DELETE FROM T_ORCA_CONTATO WHERE CONTATO_ID = @contatoId", new { contatoId });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public void DeletePorPessoaId(int pessoaId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute("DELETE FROM T_ORCA_CONTATO WHERE PESSOA_ID = @pessoaId", new { pessoaId });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ContatoModel Find(int contatoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<ContatoModel>("SELECT * FROM T_ORCA_CONTATO WHERE CONTATO_ID = @contatoId", new { contatoId });
                    return resposta.ToArray()[0];
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ContatoModel FindPorContatoPadraoETipoContato(int pessoaId, string tipoContato)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<ContatoModel>(@"SELECT * FROM T_ORCA_CONTATO WHERE PESSOA_ID = @pessoaId AND TIPO_CONTATO = @tipoContato
                                            AND CONTATO_PADRAO = true", new { pessoaId, tipoContato });

                    return resposta.ToArray()[0];
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ContatoModel> List()
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<ContatoModel>("SELECT * FROM T_ORCA_CONTATO");
                    return resposta;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public IEnumerable<ContatoModel> ListPorPessoaId(int pessoaId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<ContatoModel>("SELECT * FROM T_ORCA_CONTATO WHERE PESSOA_ID = @pessoaId", new { pessoaId });
                    return resposta;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(int contatoId, ContatoModel contato)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"UPDATE T_ORCA_CONTATO SET CONTATO = @CONTATO, TIPO_CONTATO = @TIPO_CONTATO,
                                DDD = @DDD, RAMAL = @RAMAL, CONTATO_PADRAO = @CONTATO_PADRAO
                                WHERE CONTATO_ID = @contatoId", new 
                    { 
                        contato.CONTATO, 
                        contato.TIPO_CONTATO, 
                        contato.DDD, 
                        contato.RAMAL, 
                        contato.CONTATO_PADRAO, 
                        contatoId 
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
