using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;

using OrcamentariaBackEnd.Database;

namespace OrcamentariaBackEnd
{
    public class CartaCoberturaRepository : ICartaCoberturaRepository
    {
        private IConexao Conexao;

        public CartaCoberturaRepository(IConexao conexao)
        {
            this.Conexao = conexao;
        }

        public CartaCoberturaModel Create(CartaCoberturaModel cartaCobertura)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"INSERT INTO T_ORCA_CARTA_COBERTURA (REFERENCIA, MATERIAL_ID, NOME_MATERIAL, PESSOA_ID, 
                                NOME_PESSOA) VALUES(@REFERENCIA, @MATERIAL_ID, @NOME_MATERIAL, @PESSOA_ID, 
                                @NOME_PESSOA)", new 
                    { 
                        cartaCobertura.REFERENCIA, 
                        cartaCobertura.MATERIAL.MATERIAL_ID,
                        cartaCobertura.MATERIAL.NOME_MATERIAL, 
                        cartaCobertura.MATERIAL.FABRICANTE.PESSOA_ID,
                        cartaCobertura.MATERIAL.FABRICANTE.NOME_PESSOA
                    });

                    return Find(cn.Query<int>("SELECT LAST_INSERT_ID()").ToArray()[0]);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int cartaCoberturaId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                { 
                    cn.Execute("DELETE FROM T_ORCA_CARTA_COBERTURA WHERE CARTA_COBERTURA_ID = @cartaCoberturaId", new { cartaCoberturaId });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CartaCoberturaModel Find(int cartaCoberturaId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<CartaCoberturaModel>("SELECT * FROM T_ORCA_CARTA_COBERTURA WHERE CARTA_COBERTURA_ID = @cartaCoberturaId", new { cartaCoberturaId });
                    return resposta.ToArray()[0];
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<CartaCoberturaModel> List()
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<CartaCoberturaModel>("SELECT * FROM T_ORCA_CARTA_COBERTURA");
                    return resposta;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<CartaCoberturaModel> ListPorMaterialId(int materialId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<CartaCoberturaModel>("SELECT * FROM T_ORCA_CARTA_COBERTURA WHERE MATERIAL_ID = @materialId", new { materialId });
                    return resposta;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<CartaCoberturaModel> ListPorMaterialIdEPessoaId(int materialId, int pessoaId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<CartaCoberturaModel>("SELECT * FROM T_ORCA_CARTA_COBERTURA WHERE MATERIAL_ID = @materialId AND PESSOA_ID = @pessoaId", new { materialId, pessoaId });

                    return resposta;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<CartaCoberturaModel> ListPorPessoaId(int pessoaId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<CartaCoberturaModel>("SELECT * FROM T_ORCA_CARTA_COBERTURA WHERE PESSOA_ID = @pessoaId", new { pessoaId });

                    return resposta;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<CartaCoberturaModel> ListPorReferencia(string referencia)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<CartaCoberturaModel>("SELECT * FROM T_ORCA_CARTA_COBERTURA WHERE REFERENCIA = @referencia", new { referencia });
                    return resposta;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<CartaCoberturaModel> ListPorReferenciaEPessoaId(string referencia, int pessoaId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<CartaCoberturaModel>("SELECT * FROM T_ORCA_CARTA_COBERTURA WHERE REFERENCIA = @referencia AND PESSOA_ID = @pessoaId", new { referencia, pessoaId });
                    return resposta;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<CartaCoberturaModel> ListPorReferenciaEPessoaIdEMaterialId(string referencia, int pessoaId, int materialId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<CartaCoberturaModel>("SELECT * FROM T_ORCA_CARTA_COBERTURA WHERE REFERENCIA = @referencia AND PESSOA_ID = @pessoaId AND MATERIAL = @materialId", new { referencia, pessoaId, materialId });
                    return resposta;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(int cartaCoberturaId, CartaCoberturaModel cartaCobertura)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"UPDATE T_ORCA_CARTA_COBERTURA SET REFERENCIA = @REFERENCIA, MATERIAL_ID = @MATERIAL_ID,
                                NOME_MATERIAL = @NOME_MATERIAL, PESSOA_ID = @PESSOA_ID, NOME_PESSOA = @NOME_PESSOA
                                WHERE CARTA_COBERTURA_ID = @cartaCoberturaId", new 
                    { 
                        cartaCobertura.REFERENCIA, 
                        cartaCobertura.MATERIAL.MATERIAL_ID, 
                        cartaCobertura.MATERIAL.NOME_MATERIAL,
                        cartaCobertura.MATERIAL.FABRICANTE.PESSOA_ID, 
                        cartaCobertura.MATERIAL.FABRICANTE.NOME_PESSOA,
                        cartaCoberturaId
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
