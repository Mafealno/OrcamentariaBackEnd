using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Dapper;
using Orcamentaria.Model.Cadastro;
using OrcamentariaBackEnd.Database;

namespace OrcamentariaBackEnd.Repositories
{
    public class CartaCoberturaRepository : ICartaCoberturaRepository
    {
        private IConexao Conexao;
        private IMaterialRepository Material;
        private IItensCartaCoberturaRepository ItensCartaCobertura;

        public CartaCoberturaRepository(IConexao conexao, IMaterialRepository material, IItensCartaCoberturaRepository itensCartaCobertura)
        {
            this.Conexao = conexao;
            this.Material = material;
            this.ItensCartaCobertura = itensCartaCobertura;
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
                    ItensCartaCobertura.DeletePorCartaCoberturaId(cartaCoberturaId);
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
                    resposta.ToArray()[0].LIST_ITENS_CARTA_COBERTURA = ItensCartaCobertura.ListPorCartaCoberturaId(cartaCoberturaId).ToList();
                    var materialId = cn.Query<int>("SELECT MATERIAL_ID FROM T_ORCA_CARTA_COBERTURA WHERE CARTA_COBERTURA_ID = @cartaCoberturaId", new { cartaCoberturaId });
                    resposta.ToArray()[0].MATERIAL = Material.Find(materialId.ToArray()[0]);
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

                    List<CartaCoberturaModel> listCartaCobertura = new List<CartaCoberturaModel>();
                    
                    foreach(CartaCoberturaModel cartaCobertura in resposta)
                    {
                        listCartaCobertura.Add(Find(cartaCobertura.CARTA_COBERTURA_ID));
                    }

                    return listCartaCobertura;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<CartaCoberturaModel> ListPorMaterialId(string materialId)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<CartaCoberturaModel>("SELECT * FROM T_ORCA_CARTA_COBERTURA WHERE MATERIAL_ID = @materialId", new { materialId });

                    List<CartaCoberturaModel> listCartaCobertura = new List<CartaCoberturaModel>();

                    foreach (CartaCoberturaModel cartaCobertura in resposta)
                    {
                        listCartaCobertura.Add(Find(cartaCobertura.CARTA_COBERTURA_ID));
                    }

                    return listCartaCobertura;
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

                    List<CartaCoberturaModel> listCartaCobertura = new List<CartaCoberturaModel>();

                    foreach (CartaCoberturaModel cartaCobertura in resposta)
                    {
                        listCartaCobertura.Add(Find(cartaCobertura.CARTA_COBERTURA_ID));
                    }

                    return listCartaCobertura;
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

                    List<CartaCoberturaModel> listCartaCobertura = new List<CartaCoberturaModel>();

                    foreach (CartaCoberturaModel cartaCobertura in resposta)
                    {
                        listCartaCobertura.Add(Find(cartaCobertura.CARTA_COBERTURA_ID));
                    }

                    return listCartaCobertura;
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

                    List<CartaCoberturaModel> listCartaCobertura = new List<CartaCoberturaModel>();

                    foreach (CartaCoberturaModel cartaCobertura in resposta)
                    {
                        listCartaCobertura.Add(Find(cartaCobertura.CARTA_COBERTURA_ID));
                    }

                    return listCartaCobertura;
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

                    List<CartaCoberturaModel> listCartaCobertura = new List<CartaCoberturaModel>();

                    foreach (CartaCoberturaModel cartaCobertura in resposta)
                    {
                        listCartaCobertura.Add(Find(cartaCobertura.CARTA_COBERTURA_ID));
                    }

                    return listCartaCobertura;
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
                        cartaCobertura.MATERIAL.FABRICANTE.NOME_PESSOA
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
