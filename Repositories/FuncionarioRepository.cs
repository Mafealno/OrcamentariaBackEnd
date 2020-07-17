
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using OrcamentariaBackEnd.Database;
using System.Net.Http;
using Microsoft.AspNetCore.Components.RenderTree;
using System.Runtime.InteropServices;

namespace OrcamentariaBackEnd
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private IConexao Conexao;

        public FuncionarioRepository(IConexao conexao)
        {
            this.Conexao = conexao;
        }

        public FuncionarioModel Create(FuncionarioModel funcionario)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"INSERT INTO T_ORCA_FUNCIONARIO (PESSOA_ID, CARGO_FUNCIONARIO, DATA_ADMISSAO, 
                                VALOR_DIA_TRABALHADO, STATUS_FUNCIONARIO) VALUES(@PESSOA_ID, @CARGO_FUNCIONARIO, 
                                @DATA_ADMISSAO, @VALOR_DIA_TRABALHADO, @STATUS_FUNCIONARIO)", funcionario);

                    return Find(cn.Query<int>("SELECT LAST_INSERT_ID()").ToArray()[0]);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int pessoaId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute("DELETE FROM T_ORCA_FUNCIONARIO WHERE PESSOA_ID = @pessoaId", new { pessoaId });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public FuncionarioModel Find(int pessoaId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<FuncionarioModel>(@"SELECT * FROM T_ORCA_PESSOA INNER JOIN T_ORCA_FUNCIONARIO
                                                              ON T_ORCA_PESSOA.PESSOA_ID = T_ORCA_FUNCIONARIO.PESSOA_ID 
                                                              WHERE T_ORCA_FUNCIONARIO.PESSOA_ID = @pessoaId", new { pessoaId });

                    if (resposta.Count() == 0)
                    {
                        return new FuncionarioModel();
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

        public IEnumerable<FuncionarioModel> List()
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {

                    var resposta = cn.Query<FuncionarioModel>(@"SELECT * FROM T_ORCA_PESSOA INNER JOIN T_ORCA_FUNCIONARIO
                                                          ON T_ORCA_PESSOA.PESSOA_ID = T_ORCA_FUNCIONARIO.PESSOA_ID");

                    return resposta;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<FuncionarioModel> ListPorNomePessoa(string nomePessoa)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {

                    var resposta = cn.Query<FuncionarioModel>(@"SELECT * FROM T_ORCA_PESSOA INNER JOIN T_ORCA_FUNCIONARIO
                                                                ON T_ORCA_PESSOA.PESSOA_ID = T_ORCA_FUNCIONARIO.PESSOA_ID
                                                                WHERE NOME_PESSOA LIKE @nomePessoa", new { nomePessoa = nomePessoa + "%" });

                    return resposta;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(int pessoaID, FuncionarioModel funcionario)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"UPDATE T_ORCA_FUNCIONARIO SET CARGO_FUNCIONARIO = @CARGO_FUNCIONARIO, 
                                DATA_ADMISSAO = @DATA_ADMISSAO, VALOR_DIA_TRABALHADO = @VALOR_DIA_TRABALHADO, 
                                STATUS_FUNCIONARIO = @STATUS_FUNCIONARIO WHERE PESSOA_ID = @pessoaId", new 
                    { 
                        funcionario.CARGO_FUNCIONARIO, 
                        funcionario.DATA_ADMISSAO, 
                        funcionario.VALOR_DIA_TRABALHADO, 
                        funcionario.STATUS_FUNCIONARIO, 
                        pessoaID 
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
