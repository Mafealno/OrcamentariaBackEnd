﻿using Dapper;

using OrcamentariaBackEnd.Database;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class ItensOrcamentoRepository : IItensOrcamentoRepository
    {

        private IConexao Conexao;

        public ItensOrcamentoRepository(IConexao conexao)
        {
            this.Conexao = conexao;
        }

        public ItensOrcamentoModel Create(ItensOrcamentoModel itensOrcamento)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"INSERT INTO T_ORCA_ITENS_ORCAMENTO (ORCAMENTO_ID, NUMERO_LINHA, VALOR_COMPRIMENTO, AREA) 
                                    VALUES(@ORCAMENTO_ID, @NUMERO_LINHA, @VALOR_COMPRIMENTO, @AREA)", new 
                    { 
                        itensOrcamento.ORCAMENTO_ID,
                        itensOrcamento.NUMERO_LINHA, 
                        itensOrcamento.VALOR_COMPRIMENTO, 
                        itensOrcamento.AREA
                    });

                    return Find(cn.Query<int>("SELECT ITENS_ORCAMENTO_ID FROM T_ORCA_ITENS_ORCAMENTO ORDER BY ITENS_ORCAMENTO_ID DESC LIMIT 1").ToArray()[0]);
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
                    cn.Execute("DELETE FROM T_ORCA_ITENS_ORCAMENTO WHERE ITENS_ORCAMENTO_ID = @itensOrcamentoId", new { itensOrcamentoId });
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
                    cn.Execute("DELETE FROM T_ORCA_ITENS_ORCAMENTO WHERE ORCAMENTO_ID = @orcamentoId", new { orcamentoId });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ItensOrcamentoModel Find(int itensOrcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<ItensOrcamentoModel>(@"SELECT * FROM T_ORCA_ITENS_ORCAMENTO WHERE ITENS_ORCAMENTO_ID = @itensOrcamentoId", new { itensOrcamentoId });

                    if (resposta.Count() == 0)
                    {
                        return new ItensOrcamentoModel();
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

        public IEnumerable<ItensOrcamentoModel> List()
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<ItensOrcamentoModel>("SELECT * FROM T_ORCA_ITENS_ORCAMENTO");

                    return resposta;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ItensOrcamentoModel> ListPorOrcamentoId(int orcamentoId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<ItensOrcamentoModel>("SELECT * FROM T_ORCA_ITENS_ORCAMENTO WHERE ORCAMENTO_ID = @orcamentoId", new { orcamentoId });

                    return resposta;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(int itensOrcamentoId, ItensOrcamentoModel itensOrcamento)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"UPDATE T_ORCA_ITENS_ORCAMENTO SET NUMERO_LINHA = @NUMERO_LINHA, 
                                VALOR_COMPRIMENTO = @VALOR_COMPRIMENTO, AREA = @AREA
                                WHERE ITENS_ORCAMENTO_ID = @itensOrcamentoId", new 
                    { 
                        itensOrcamento.NUMERO_LINHA, 
                        itensOrcamento.VALOR_COMPRIMENTO,
                        itensOrcamento.AREA,
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
