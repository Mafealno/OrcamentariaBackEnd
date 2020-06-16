﻿using Dapper;
using Orcamentaria.Model.Cadastro;
using Orcamentaria.Model.Orcamento;
using OrcamentariaBackEnd.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd.Repositories
{
    public class MaoObraOrcamentoRepository : IMaoObraOrcamentoRepository
    {

        private IConexao Conexao;
        private IFuncionarioRepository Funcionario;
        private ICustosMaoObraRepository CustosMaoObra;

        public MaoObraOrcamentoRepository(IConexao conexao, IFuncionarioRepository funcionarioRepository, ICustosMaoObraRepository custosMaoObraRepository)
        {
            this.Conexao = conexao;
            this.Funcionario = funcionarioRepository;
            this.CustosMaoObra = custosMaoObraRepository;
        }

        public MaoObraOrcamentoModel Create(MaoObraOrcamentoModel maoObraOrcamento)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"INSERT INTO T_ORCA_MAO_OBRA_ORCAMENTO (PESSOA_ID, NOME_PESSOA, VALOR_DIA_TRABALHADO) 
                                VALUES(@PESSOA_ID, @NOME_PESSOA, @VALOR_DIA_TRABALHADO)", new
                    { 
                        maoObraOrcamento.FUNCIONARIO.PESSOA_ID,
                        maoObraOrcamento.FUNCIONARIO.NOME_PESSOA,
                        maoObraOrcamento.FUNCIONARIO.VALOR_DIA_TRABALHADO
                    });

                    var maoObraOrcamentoCriado = Find(cn.Query<int>("SELECT LAST_INSERT_ID()").ToArray()[0], maoObraOrcamento.ORCAMENTO_ID);

                    cn.Execute(@"INSERT INTO T_ORCA_OBRA (ORCAMENTO_ID, MAO_OBRA_ORCAMENTO_ID, PESSOA_ID) 
                                VALUES(@ORCAMENTO_ID, @MAO_OBRA_ORCAMENTO_ID, @PESSOA_ID)", new
                    {
                        maoObraOrcamentoCriado.ORCAMENTO_ID,
                        maoObraOrcamentoCriado.MAO_OBRA_ORCAMENTO_ID,
                        maoObraOrcamento.FUNCIONARIO.PESSOA_ID
                    });

                    return maoObraOrcamentoCriado;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int maoObraOrcamentoId, int orcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"DELETE FROM T_ORCA_OBRA WHERE ORCAMENTO_ID = @orcamentoId 
                                AND MAO_OBRA_ORCAMENTO_ID = @maoObraOrcamentoId", new
                    {
                       orcamentoId,
                       maoObraOrcamentoId
                    });

                    CustosMaoObra.DeletePorMaoObraOrcamentoId(maoObraOrcamentoId);

                    cn.Execute(@"DELETE FROM T_ORCA_MAO_OBRA_ORCAMENTO WHERE MAO_OBRA_ORCAMENTO_ID = @maoObraOrcamentoId", new { maoObraOrcamentoId });
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

                    var listMaoObraOrcamentoId = cn.Query<int>("SELECT DISTINCT MAO_OBRA_ORCAMENTO_ID FROM T_ORCA_OBRA WHERE ORCAMENTO_ID = @orcamentId", new { orcamentoId });

                    cn.Execute(@"DELETE FROM T_ORCA_OBRA WHERE ORCAMENTO_ID = @orcamentoId ", new { orcamentoId });

                    foreach(int id in listMaoObraOrcamentoId)
                    {
                        CustosMaoObra.DeletePorMaoObraOrcamentoId(id);
                    }

                    cn.Execute(@"DELETE FROM T_ORCA_MAO_OBRA_ORCAMENTO WHERE MAO_OBRA_ORCAMENTO_ID IN @listMaoObraOrcamentoId",
                                new { listMaoObraOrcamentoId = listMaoObraOrcamentoId });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public MaoObraOrcamentoModel Find(int maoObraOrcamentoId, int orcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<MaoObraOrcamentoModel>(@"SELECT * FROM T_ORCA_MAO_OBRA_ORCAMENTO INNER JOIN T_ORCA_OBRA ON 
                                                                    T_ORCA_MAO_OBRA_ORCAMENTO.MAO_OBRA_ORCAMENTO_ID = T_ORCA_OBRA.MAO_OBRA_ORCAMENTO_ID
                                                                    WHERE T_ORCA_MAO_OBRA_ORCAMENTO.MAO_OBRA_ORCAMENTO_ID = @maoObraOrcamentoId 
                                                                    AND T_ORCA_OBRA.ORCAMENTO_ID = orcamentoId", new 
                    { 
                        maoObraOrcamentoId, 
                        orcamentoId 
                    });

                    var pessoaId = cn.Query<int>(@"SELECT PESSOA_ID FROM T_ORCA_MAO_OBRA_ORCAMENTO WHERE 
                                                   MAO_OBRA_ORCAMENTO_ID = @maoObraOrcamentoId", new { maoObraOrcamentoId });

                    resposta.ToArray()[0].FUNCIONARIO = Funcionario.Find(pessoaId.ToArray()[0]);

                    resposta.ToArray()[0].LIST_CUSTO = CustosMaoObra.List(maoObraOrcamentoId).ToList();

                    return resposta.ToArray()[0];
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<MaoObraOrcamentoModel> List()
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<MaoObraOrcamentoModel>(@"SELECT * FROM T_ORCA_MAO_OBRA_ORCAMENTO INNER JOIN T_ORCA_OBRA ON 
                                                                 T_ORCA_MAO_OBRA_ORCAMENTO.MAO_OBRA_ORCAMENTO_ID = T_ORCA_OBRA.MAO_OBRA_ORCAMENTO_ID");

                    List<MaoObraOrcamentoModel> listMaoObraOrcamento = new List<MaoObraOrcamentoModel>();

                    foreach(MaoObraOrcamentoModel maoObraOrcamento in resposta)
                    {
                        listMaoObraOrcamento.Add(Find(maoObraOrcamento.MAO_OBRA_ORCAMENTO_ID, maoObraOrcamento.ORCAMENTO_ID));
                    }

                    return listMaoObraOrcamento;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<MaoObraOrcamentoModel> ListPorOrcamentoId(int orcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<MaoObraOrcamentoModel>(@"SELECT * FROM T_ORCA_MAO_OBRA_ORCAMENTO INNER JOIN T_ORCA_OBRA ON 
                                                                    T_ORCA_MAO_OBRA_ORCAMENTO.MAO_OBRA_ORCAMENTO_ID = T_ORCA_OBRA.MAO_OBRA_ORCAMENTO_ID
                                                                    T_ORCA_OBRA.ORCAMENTO_ID = orcamentoId");

                    List<MaoObraOrcamentoModel> listMaoObraOrcamento = new List<MaoObraOrcamentoModel>();

                    foreach (MaoObraOrcamentoModel maoObraOrcamento in resposta)
                    {
                        listMaoObraOrcamento.Add(Find(maoObraOrcamento.MAO_OBRA_ORCAMENTO_ID, maoObraOrcamento.ORCAMENTO_ID));
                    }

                    return listMaoObraOrcamento;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(int maoObraOrcamentoId, MaoObraOrcamentoModel maoObraOrcamento)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"UPDATE T_ORCA_MAO_OBRA_ORCAMENTO SET PESSOA_ID = @PESSOA_ID, NOME_PESSOA = @NOME_PESSOA, 
                                VALOR_DIA_TRABALHADO = @VALOR_DIA_TRABALHADO WHERE MAO_OBRA_ORCAMENTO_ID = @maoObraOrcamentoId", new
                    {
                        maoObraOrcamento.FUNCIONARIO.PESSOA_ID,
                        maoObraOrcamento.FUNCIONARIO.NOME_PESSOA,
                        maoObraOrcamento.FUNCIONARIO.VALOR_DIA_TRABALHADO,
                        maoObraOrcamentoId
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