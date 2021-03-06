﻿using Dapper;

using OrcamentariaBackEnd.Database;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class ItensOrcamentoIntumescenteRepository : IItensOrcamentoIntumescenteRepository
    {

        private IConexao Conexao;

        public ItensOrcamentoIntumescenteRepository(IConexao conexao)
        {
            this.Conexao = conexao;
        }

        public ItensOrcamentoIntumescenteModel Create(ItensOrcamentoIntumescenteModel itensOrcamentoIntumescente)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"INSERT INTO T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE (ITENS_ORCAMENTO_ID, ORCAMENTO_ID, REFERENCIA, NUMERO_FACES,
                                VALOR_HP, VALOR_HP_A, VALOR_WD, QTDE, VALOR_ESPESSURA, QTDE_LITROS, PERFIL_ID, VALOR_D, VALOR_BF, 
                                VALOR_TW, VALOR_TF, VALOR_KG_M, CARTA_COBERTURA_ID) VALUES(@ITENS_ORCAMENTO_ID, @ORCAMENTO_ID, @REFERENCIA, @NUMERO_FACES,
                                @VALOR_HP, @VALOR_HP_A, @VALOR_WD, @QTDE, @VALOR_ESPESSURA, @QTDE_LITROS, @PERFIL_ID, @VALOR_D, @VALOR_BF, 
                                @VALOR_TW, @VALOR_TF, @VALOR_KG_M, @CARTA_COBERTURA_ID)", new
                    {
                        itensOrcamentoIntumescente.ITENS_ORCAMENTO_ID,
                        itensOrcamentoIntumescente.ORCAMENTO_ID,
                        itensOrcamentoIntumescente.REFERENCIA,
                        itensOrcamentoIntumescente.NUMERO_FACES,
                        itensOrcamentoIntumescente.VALOR_HP,
                        itensOrcamentoIntumescente.VALOR_HP_A,
                        itensOrcamentoIntumescente.VALOR_WD,
                        itensOrcamentoIntumescente.QTDE,
                        itensOrcamentoIntumescente.VALOR_ESPESSURA,
                        itensOrcamentoIntumescente.QTDE_LITROS,
                        itensOrcamentoIntumescente.PERFIL.PERFIL_ID,
                        itensOrcamentoIntumescente.PERFIL.VALOR_D,
                        itensOrcamentoIntumescente.PERFIL.VALOR_BF,
                        itensOrcamentoIntumescente.PERFIL.VALOR_TW,
                        itensOrcamentoIntumescente.PERFIL.VALOR_TF,
                        itensOrcamentoIntumescente.PERFIL.VALOR_KG_M,
                        itensOrcamentoIntumescente.CARTA_COBERTURA.CARTA_COBERTURA_ID
                    });

                    return Find(cn.Query<int>("SELECT MAX(ITENS_ORCAMENTO_ID) FROM T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE").FirstOrDefault());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int itensOrcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute("DELETE FROM T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE WHERE ITENS_ORCAMENTO_ID = @itensOrcamentoId", new { itensOrcamentoId });
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
                    cn.Execute("DELETE FROM T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE WHERE ORCAMENTO_ID = @orcamentoId", new { orcamentoId });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ItensOrcamentoIntumescenteModel Find(int itensOrcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<ItensOrcamentoIntumescenteModel>(@"SELECT * FROM T_ORCA_ITENS_ORCAMENTO INNER JOIN T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE 
                                                                            ON T_ORCA_ITENS_ORCAMENTO.ITENS_ORCAMENTO_ID = T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE.ITENS_ORCAMENTO_ID 
                                                                            WHERE T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE.ITENS_ORCAMENTO_ID = @itensOrcamentoId", new { itensOrcamentoId });

                    if (resposta.Count() == 0)
                    {
                        return new ItensOrcamentoIntumescenteModel();
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

        public IEnumerable<ItensOrcamentoIntumescenteModel> List()
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<ItensOrcamentoIntumescenteModel>(@"SELECT * FROM T_ORCA_ITENS_ORCAMENTO INNER JOIN T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE 
                                                                                ON T_ORCA_ITENS_ORCAMENTO.ITENS_ORCAMENTO_ID = T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE.ITENS_ORCAMENTO_ID");

                    return resposta;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ItensOrcamentoIntumescenteModel> ListPorOrcamentoId(int orcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<ItensOrcamentoIntumescenteModel>(@"SELECT * FROM T_ORCA_ITENS_ORCAMENTO INNER JOIN T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE 
                                                                                ON T_ORCA_ITENS_ORCAMENTO.ITENS_ORCAMENTO_ID = T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE.ITENS_ORCAMENTO_ID
                                                                                WHERE T_ORCA_ITENS_ORCAMENTO.ORCAMENTO_ID = @orcamentoId", new { orcamentoId });
                    
                    return resposta;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(int itensOrcamentoId, ItensOrcamentoIntumescenteModel itensOrcamentoIntumescente)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"UPDATE T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE SET REFERENCIA = @REFERENCIA, NUMERO_FACES = @NUMERO_FACES,
                                VALOR_HP = @VALOR_HP, VALOR_HP_A = @VALOR_HP_A, VALOR_WD = @VALOR_WD, QTDE = @QTDE, 
                                VALOR_ESPESSURA = @VALOR_ESPESSURA, QTDE_LITROS = @QTDE_LITROS, PERFIL_ID = @PERFIL_ID, VALOR_D = @VALOR_D, 
                                VALOR_BF = @VALOR_BF, VALOR_TW = @VALOR_TW, VALOR_TF = @VALOR_TF, VALOR_KG_M = @VALOR_KG_M, 
                                CARTA_COBERTURA_ID = @CARTA_COBERTURA_ID WHERE ITENS_ORCAMENTO_ID = @itensOrcamentoId", new
                    {
                        itensOrcamentoIntumescente.REFERENCIA,
                        itensOrcamentoIntumescente.NUMERO_FACES,
                        itensOrcamentoIntumescente.VALOR_HP,
                        itensOrcamentoIntumescente.VALOR_HP_A,
                        itensOrcamentoIntumescente.VALOR_WD,
                        itensOrcamentoIntumescente.QTDE,
                        itensOrcamentoIntumescente.VALOR_ESPESSURA,
                        itensOrcamentoIntumescente.QTDE_LITROS,
                        itensOrcamentoIntumescente.PERFIL.PERFIL_ID,
                        itensOrcamentoIntumescente.PERFIL.VALOR_D,
                        itensOrcamentoIntumescente.PERFIL.VALOR_BF,
                        itensOrcamentoIntumescente.PERFIL.VALOR_TW,
                        itensOrcamentoIntumescente.PERFIL.VALOR_TF,
                        itensOrcamentoIntumescente.PERFIL.VALOR_KG_M,
                        itensOrcamentoIntumescente.CARTA_COBERTURA.CARTA_COBERTURA_ID,
                        itensOrcamentoId
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
