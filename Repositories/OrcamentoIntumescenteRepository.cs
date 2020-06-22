﻿using Dapper;
using OrcamentariaBackEnd.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd.Repositories
{
    public class OrcamentoIntumescenteRepository : IOrcamentoIntumescenteRepository
    {

        private IConexao Conexao;

        public OrcamentoIntumescenteRepository(IConexao conexao)
        {
            this.Conexao = conexao;
        }

        public OrcamentoIntumescenteModel Create(OrcamentoIntumescenteModel orcamentoIntumescente)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"INSERT INTO T_ORCA_ORCAMENTO_INTUMESCENTE (ORCAMENTO_ID, GRUPO, OCUPACAO_USO, DIVISAO, CLASSE, TEMPO_RESISTENCIA_FOGO
                                QTDE_LITROS_TOTAL, PERCENTUAL_PERDA, QTDE_BALDES, QTDE_BALDES_REAL) VALUES(@ORCAMENTO_ID, @GRUPO, 
                                @OCUPACAO_USO, @DIVISAO, @CLASSE, @TEMPO_RESISTENCIA_FOGO, @QTDE_LITROS_TOTAL, @PERCENTUAL_PERDA, @QTDE_BALDES, 
                                @QTDE_BALDES_REAL)", new
                    {
                        orcamentoIntumescente.ORCAMENTO_ID,
                        orcamentoIntumescente.GRUPO,
                        orcamentoIntumescente.OCUPACAO_USO,
                        orcamentoIntumescente.DIVISAO,
                        orcamentoIntumescente.CLASSE,
                        orcamentoIntumescente.TEMPO_RESISTENCIA_FOGO,
                        orcamentoIntumescente.QTDE_LITROS_TOTAL,
                        orcamentoIntumescente.PERCENTUAL_PERDA,
                        orcamentoIntumescente.QTDE_BALDES,
                        orcamentoIntumescente.QTDE_BALDES_REAL
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
                    cn.Execute("DELETE FROM T_ORCA_ORCAMENTO_INTUMESCENTE WHERE ORCAMENTO_ID = @orcamentoId", new { orcamentoId });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public OrcamentoIntumescenteModel Find(int orcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<OrcamentoIntumescenteModel>("SELECT * FROM T_ORCA_ORCAMENTO_INTUMESCENTE WHERE ORCAMENTO_ID = @orcamentoId", new { orcamentoId });

                    return resposta.ToArray()[0];
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<OrcamentoIntumescenteModel> List()
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<OrcamentoIntumescenteModel>("SELECT * FROM T_ORCA_ORCAMENTO_INTUMESCENTE");

                    return resposta;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(int orcamentoId, OrcamentoIntumescenteModel orcamentoIntumescente)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"UPDATE T_ORCA_ORCAMENTO_INTUMESCENTE SET GRUPO = @GRUPO, OCUPACAO_USO = @OCUPACAO_USO, 
                                DIVISAO = @DIVISAO, CLASSE = @CLASSE, TEMPO_RESISTENCIA_FOGO = @TEMPO_RESISTENCIA_FOGO, 
                                QTDE_LITROS_TOTAL = @QTDE_LITROS_TOTAL, PERCENTUAL_PERDA = @PERCENTUAL_PERDA, QTDE_BALDES = @QTDE_BALDES, 
                                QTDE_BALDES_REAL = @QTDE_BALDES_REAL WHERE ORCAMENTO_ID = @orcamentoId", new
                    {
                        orcamentoIntumescente.GRUPO,
                        orcamentoIntumescente.OCUPACAO_USO,
                        orcamentoIntumescente.DIVISAO,
                        orcamentoIntumescente.CLASSE,
                        orcamentoIntumescente.TEMPO_RESISTENCIA_FOGO,
                        orcamentoIntumescente.QTDE_LITROS_TOTAL,
                        orcamentoIntumescente.PERCENTUAL_PERDA,
                        orcamentoIntumescente.QTDE_BALDES,
                        orcamentoIntumescente.QTDE_BALDES_REAL,
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