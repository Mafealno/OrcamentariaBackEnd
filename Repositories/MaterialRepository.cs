﻿using Orcamentaria.Model.Cadastro;
using OrcamentariaBackEnd.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace OrcamentariaBackEnd.Repositories
{
    public class MaterialRepository : IMaterialRepository
    {
        private IConexao Conexao;
        private IPessoaRepository Pessoa;

        public MaterialRepository(IConexao conexao, IPessoaRepository pessoa)
        {
            this.Conexao = conexao;
            this.Pessoa = pessoa;
        }

        public MaterialModel Create(MaterialModel material)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"INSERT INTO T_ORCA_MATERIAL (PESSOA_ID, NOME_MATERIAL, DESCRICAO_MATERIAL, 
                                TIPO_MATERIAL, NOME_PESSOA) VALUES(@PESSOA_ID, @NOME_MATERIAL, @DESCRICAO_MATERIAL, 
                                @TIPO_MATERIAL, @NOME_PESSOA)", new 
                    { 
                        material.NOME_MATERIAL, 
                        material.DESCRICAO_MATERIAL,
                        material.TIPO_MATERIAL, 
                        material.FABRICANTE.PESSOA_ID, 
                        material.FABRICANTE.NOME_PESSOA});

                    return Find(cn.Query<int>("SELECT LAST_INSERT_ID()").ToArray()[0]);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int materialId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute("DELETE FROM T_ORCA_MATERIAL WHERE MATERIAL_ID = @materialId", new { materialId });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public MaterialModel Find(int materialId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<MaterialModel>("SELECT * FROM T_ORCA_MATERIAL WHERE MATERIAL_ID = @materialId", new { materialId });

                    var pessoaId = cn.Query<int>("SELECT PESSOA_ID FROM T_ORCA_MATERIAL WHERE MATERIAL_ID = @materialId", new { materialId });
                    
                    resposta.ToArray()[0].FABRICANTE = Pessoa.Find(pessoaId.ToArray()[0]);

                    return resposta.ToArray()[0];
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<MaterialModel> List()
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<MaterialModel>("SELECT * FROM T_ORCA_MATERIAL");

                    List<MaterialModel> listMaterial = new List<MaterialModel>();

                    foreach (MaterialModel material in resposta)
                    {
                        listMaterial.Add(Find(material.MATERIAL_ID));
                    }

                    return listMaterial;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<MaterialModel> ListPorNomeFabricante(string nomeFabricante)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<MaterialModel>("SELECT * FROM T_ORCA_MATERIAL WHERE NOME_PESSOA LIKE @nomeFabricante", new { nomeFabricante = nomeFabricante + '%' });

                    List<MaterialModel> listMaterial = new List<MaterialModel>();

                    foreach (MaterialModel material in resposta)
                    {
                        listMaterial.Add(Find(material.MATERIAL_ID));
                    }

                    return listMaterial;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<MaterialModel> ListPorNomeMaterial(string nomeMaterial)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<MaterialModel>("SELECT * FROM T_ORCA_MATERIAL WHERE MATERIAL_ID LIKE @nomeMaterial", new { nomeMaterial = nomeMaterial + '%' });

                    List<MaterialModel> listMaterial = new List<MaterialModel>();

                    foreach (MaterialModel material in resposta)
                    {
                        listMaterial.Add(Find(material.MATERIAL_ID));
                    }

                    return listMaterial;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(int materialId, MaterialModel material)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"UPDATE T_ORCA_MATERIAL SET NOME_MATERIAL = @NOME_MATERIAL, DESCRICAO_MATERIAL = @DESCRICAO_MATERIAL, 
                                TIPO_MATERIAL = @TIPO_MATERIAL, NOME_PESSOA = @NOME_PESSOA WHERE MATERIAL_ID = @materialId", new 
                    { 
                        material.NOME_MATERIAL, 
                        material.DESCRICAO_MATERIAL, 
                        material.TIPO_MATERIAL, 
                        material.FABRICANTE.NOME_PESSOA, 
                        materialId
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