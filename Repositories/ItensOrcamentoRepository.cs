﻿using Dapper;
using Orcamentaria.Model.Orcamento;
using OrcamentariaBackEnd.Database;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd.Repositories
{
    public class ItensOrcamentoRepository : IItensOrcamentoRepository
    {

        private IConexao Conexao;
        private IMaterialRepository Material;

        public ItensOrcamentoRepository(IConexao conexao, IMaterialRepository material)
        {
            this.Conexao = conexao;
            this.Material = material;
        }

        public ItensOrcamentoModel Create(ItensOrcamentoModel itensOrcamento)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"INSERT INTO T_ORCA_ITENS_ORCAMENTO (ORCAMENTO_ID, NUMERO_LINHA, VALOR_COMPRIMENTO, AREA,
                                MATERIAL_ID, NOME_MATERIAL, DESCRICAO_MATERIAL) VALUES(@ORCAMENTO_ID, @NUMERO_LINHA, @VALOR_COMPRIMENTO,
                                @AREA, @MATERIAL_ID, @MATERIAL_ID, @NOME_MATERIAL, @DESCRICAO_MATERIAL)", new 
                    { 
                        itensOrcamento.ORCAMENTO_ID,
                        itensOrcamento.NUMERO_LINHA, 
                        itensOrcamento.VALOR_COMPRIMENTO, 
                        itensOrcamento.AREA, 
                        itensOrcamento.PRODUTO.MATERIAL_ID,
                        itensOrcamento.PRODUTO.NOME_MATERIAL, 
                        itensOrcamento.PRODUTO.DESCRICAO_MATERIAL 
                    });

                    return Find(cn.Query<int>("SELECT LAST_INSERT_ID()").ToArray()[0]);
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

                    var materialId = cn.Query<int>(@"SELECT MATERIAL_ID FROM T_ORCA_ITENS_ORCAMENTO WHERE ITENS_ORCAMENTO_ID = @itensOrcamentoId", new { itensOrcamentoId });

                    resposta.ToArray()[0].PRODUTO = Material.Find(materialId.ToArray()[0]);

                    return resposta.ToArray()[0];
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

                    List<ItensOrcamentoModel> listItensOrcamento = new List<ItensOrcamentoModel>();

                    foreach(ItensOrcamentoModel itensOrcamento in resposta)
                    {
                        listItensOrcamento.Add(Find(itensOrcamento.ITENS_ORCAMENTO_ID));
                    }

                    return listItensOrcamento;
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

                    List<ItensOrcamentoModel> listItensOrcamento = new List<ItensOrcamentoModel>();

                    foreach (ItensOrcamentoModel itensOrcamento in resposta)
                    {
                        listItensOrcamento.Add(Find(itensOrcamento.ITENS_ORCAMENTO_ID));
                    }

                    return listItensOrcamento;
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
                    cn.Execute(@"UPDATE T_ORCA_ITENS_ORCAMENTO SET NUMERO_LINHA = @NUMERO_LINHA, VALOR_COMPRIMENTO = @VALOR_COMPRIMENTO, AREA = @AREA,
                                MATERIAL_ID = @MATERIAL_ID, NOME_MATERIAL = @NOME_MATERIAL, DESCRICAO_MATERIAL = @DESCRICAO_MATERIAL
                                WHERE ITENS_ORCAMENTO_ID = @itensOrcamentoId", new 
                    { 
                        itensOrcamento.NUMERO_LINHA, 
                        itensOrcamento.VALOR_COMPRIMENTO,
                        itensOrcamento.AREA, 
                        itensOrcamento.PRODUTO.MATERIAL_ID, 
                        itensOrcamento.PRODUTO.NOME_MATERIAL, 
                        itensOrcamento.PRODUTO.DESCRICAO_MATERIAL,
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