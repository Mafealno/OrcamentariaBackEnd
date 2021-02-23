using OrcamentariaBackEnd.Database;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class CartaCoberturaService
    {
        private ICartaCoberturaRepository CartaCoberturaRepository;
        private MetodosGenericosService MetodosGenericosService;
        private ItensCartaCoberturaService ItensCartaCoberturaService;
        private MaterialService MaterialService;

        public CartaCoberturaService(ICartaCoberturaRepository cartaCoberturaRepository, MetodosGenericosService metodosGenericosservice,
            ItensCartaCoberturaService itensCartaCoberturaService, MaterialService materialService)
        {
            this.CartaCoberturaRepository = cartaCoberturaRepository;
            this.ItensCartaCoberturaService = itensCartaCoberturaService;
            this.MetodosGenericosService = metodosGenericosservice;
            this.MaterialService = materialService;
        }

        public IEnumerable<CartaCoberturaModel> Get()
        {
            try
            {
                List<CartaCoberturaModel> listCartaCobertura = CartaCoberturaRepository.List().ToList();

                foreach(CartaCoberturaModel cartaCobertura in listCartaCobertura)
                {
                    cartaCobertura.LIST_ITENS_CARTA_COBERTURA = ItensCartaCoberturaService.GetComParametro(new ItensCartaCoberturaQO(0, cartaCobertura.CARTA_COBERTURA_ID, "")).ToList();

                    var materialId = MetodosGenericosService.DlookupOrcamentaria("MATERIAL_ID", "T_ORCA_CARTA_COBERTURA", $"CARTA_COBERTURA_ID = {cartaCobertura.CARTA_COBERTURA_ID}");

                    cartaCobertura.MATERIAL = MaterialService.GetComParametro(new MaterialQO(int.Parse(materialId), "", "")).ToArray()[0];
                }

                return listCartaCobertura;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<CartaCoberturaModel> Get(string referencia, int pessoaId)
        {
            try
            {
                var where = $"PESSOA_ID = {pessoaId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_PESSOA", where)))
                {
                    throw new Exception();
                }

                List<CartaCoberturaModel> listCartaCobertura = CartaCoberturaRepository.ListPorReferenciaEPessoaId(referencia, pessoaId).ToList();

                foreach (CartaCoberturaModel cartaCobertura in listCartaCobertura)
                {
                    cartaCobertura.LIST_ITENS_CARTA_COBERTURA = ItensCartaCoberturaService.GetComParametro(new ItensCartaCoberturaQO(0, cartaCobertura.CARTA_COBERTURA_ID, "")).ToList();

                    var materialId = MetodosGenericosService.DlookupOrcamentaria("MATERIAL_ID", "T_ORCA_CARTA_COBERTURA", $"CARTA_COBERTURA_ID = {cartaCobertura.CARTA_COBERTURA_ID}");

                    cartaCobertura.MATERIAL = MaterialService.GetComParametro(new MaterialQO(int.Parse(materialId), "", "")).ToArray()[0];
                }

                return listCartaCobertura;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<CartaCoberturaModel> Get(int materialId, int pessoaId)
        {
            try
            {
                var where = $"PESSOA_ID = {pessoaId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_PESSOA", where)))
                {
                    throw new Exception();
                }

                where = $"MATERIAL_ID = {materialId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("MATERIAL_ID", "T_ORCA_MATERIAL", where)))
                {
                    throw new Exception();
                }

                List<CartaCoberturaModel> listCartaCobertura = CartaCoberturaRepository.ListPorMaterialIdEPessoaId(materialId, pessoaId).ToList();

                foreach (CartaCoberturaModel cartaCobertura in listCartaCobertura)
                {
                    cartaCobertura.LIST_ITENS_CARTA_COBERTURA = ItensCartaCoberturaService.GetComParametro(new ItensCartaCoberturaQO(0, cartaCobertura.CARTA_COBERTURA_ID, "")).ToList();

                    cartaCobertura.MATERIAL = MaterialService.GetComParametro(new MaterialQO(materialId, "", "")).ToArray()[0];
                }

                return listCartaCobertura;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<CartaCoberturaModel> Get(string referencia, int pessoaId, int materialId)
        {
            try
            {

                var where = $"PESSOA_ID = {pessoaId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_PESSOA", where)))
                {
                    throw new Exception();
                }

                where = $"PESSOA_ID = {pessoaId} AND MATERIAL_ID = {materialId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("MATERIAL_ID", "T_ORCA_MATERIAL", where)))
                {
                    throw new Exception();
                }

                List<CartaCoberturaModel> listCartaCobertura = CartaCoberturaRepository.ListPorReferenciaEPessoaIdEMaterialId(referencia, pessoaId, materialId).ToList();

                foreach (CartaCoberturaModel cartaCobertura in listCartaCobertura)
                {
                    cartaCobertura.LIST_ITENS_CARTA_COBERTURA = ItensCartaCoberturaService.GetComParametro(new ItensCartaCoberturaQO(0, cartaCobertura.CARTA_COBERTURA_ID, "")).ToList();

                    cartaCobertura.MATERIAL = MaterialService.GetComParametro(new MaterialQO(materialId, "", "")).ToArray()[0];
                }

                return listCartaCobertura;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CartaCoberturaModel Get(int materialId, string referencia, string valorHpa, string tempoResistenciafogo)
        {
            try
            {
                var where = $"MATERIAL_ID = {materialId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("MATERIAL_ID", "T_ORCA_MATERIAL", where)))
                {
                    throw new Exception();
                }

                var material = MaterialService.GetComParametro(new MaterialQO(materialId, "", "")).FirstOrDefault();

                var cartaCobertura = CartaCoberturaRepository.ListPorReferenciaEPessoaIdEMaterialId(referencia, material.FABRICANTE.PESSOA_ID, materialId).FirstOrDefault();

                if(cartaCobertura != null)
                {
                    cartaCobertura.LIST_ITENS_CARTA_COBERTURA = new List<ItensCartaCoberturaModel>();

                    cartaCobertura.LIST_ITENS_CARTA_COBERTURA.Add(ItensCartaCoberturaService.Get(cartaCobertura.CARTA_COBERTURA_ID, valorHpa, tempoResistenciafogo));

                    cartaCobertura.MATERIAL = material;
                }
                else
                {
                    cartaCobertura = new CartaCoberturaModel();
                }


                return cartaCobertura;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<CartaCoberturaModel> GetComParametro(CartaCoberturaQO cartaCobertura)
        {
            try
            {
                List<CartaCoberturaModel> listCartaCobertura;

                if (!string.IsNullOrEmpty(cartaCobertura.Referencia))
                {
                    listCartaCobertura = CartaCoberturaRepository.ListPorReferencia(cartaCobertura.Referencia).ToList();
                }
                else if (cartaCobertura.MaterialId != 0)
                {
                    var where = $"MATERIAL_ID = {cartaCobertura.MaterialId}";
                    if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("MATERIAL_ID", "T_ORCA_MATERIAL", where)))
                    {
                        throw new Exception();
                    }

                    listCartaCobertura = CartaCoberturaRepository.ListPorMaterialId(cartaCobertura.MaterialId).ToList();
                }
                else if (cartaCobertura.PessoaId != 0)
                {
                    var where = $"PESSOA_ID = {cartaCobertura.PessoaId}";
                    if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_PESSOA", where)))
                    {
                        throw new Exception();
                    }

                    listCartaCobertura = CartaCoberturaRepository.ListPorPessoaId(cartaCobertura.PessoaId).ToList();
                }
                else
                {
                    listCartaCobertura = new List<CartaCoberturaModel>();

                    listCartaCobertura.Add(CartaCoberturaRepository.Find(cartaCobertura.CartaCoberturaId));
                }

                foreach (CartaCoberturaModel CartaCobertura in listCartaCobertura)
                {
                    CartaCobertura.LIST_ITENS_CARTA_COBERTURA = ItensCartaCoberturaService.GetComParametro(new ItensCartaCoberturaQO(0, CartaCobertura.CARTA_COBERTURA_ID, "")).ToList();

                    var materialId = MetodosGenericosService.DlookupOrcamentaria("MATERIAL_ID", "T_ORCA_CARTA_COBERTURA", $"CARTA_COBERTURA_ID = {CartaCobertura.CARTA_COBERTURA_ID}");

                    CartaCobertura.MATERIAL = MaterialService.GetComParametro(new MaterialQO(int.Parse(materialId), "", "")).ToArray()[0];
                }

                return listCartaCobertura;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CartaCoberturaModel Post(CartaCoberturaModel cartaCobertura)
        {
            try
            {
                var where = $"MATERIAL_ID = {cartaCobertura.MATERIAL.MATERIAL_ID}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("MATERIAL_ID", "T_ORCA_MATERIAL", where)))
                {
                    throw new Exception();
                }

                cartaCobertura.MATERIAL =  MaterialService.GetComParametro(new MaterialQO(cartaCobertura.MATERIAL.MATERIAL_ID, "", "")).ToArray()[0];

                var cartaCoberturaCadastrada = CartaCoberturaRepository.Create(cartaCobertura);

                cartaCoberturaCadastrada.LIST_ITENS_CARTA_COBERTURA = new List<ItensCartaCoberturaModel>();

                foreach (ItensCartaCoberturaModel itensCartaCobertura in cartaCobertura.LIST_ITENS_CARTA_COBERTURA)
                {
                    itensCartaCobertura.CARTA_COBERTURA_ID = cartaCoberturaCadastrada.CARTA_COBERTURA_ID;

                    cartaCoberturaCadastrada.LIST_ITENS_CARTA_COBERTURA.Add(ItensCartaCoberturaService.Post(itensCartaCobertura));
                }

                cartaCoberturaCadastrada.MATERIAL = cartaCobertura.MATERIAL;

                return cartaCoberturaCadastrada;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Put(int cartaCoberturaId, CartaCoberturaModel cartaCobertura)
        {
            try
            {
                var where = $"CARTA_COBERTURA_ID = {cartaCobertura.CARTA_COBERTURA_ID}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("CARTA_COBERTURA_ID", "T_ORCA_CARTA_COBERTURA", where)))
                {
                    throw new Exception();
                }

                cartaCobertura.MATERIAL = MaterialService.GetComParametro(new MaterialQO(cartaCobertura.MATERIAL.MATERIAL_ID, "", "")).ToArray()[0];

                CartaCoberturaRepository.Update(cartaCoberturaId, cartaCobertura);
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
                MetodosGenericosService.StartTransactionCommitRollbackOrcamentaria(MetodosGenericosEnum.START);

                var where = $"CARTA_COBERTURA_ID = {cartaCoberturaId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("CARTA_COBERTURA_ID", "T_ORCA_CARTA_COBERTURA", where)))
                {
                    throw new Exception();
                }

                ItensCartaCoberturaService.DeleteComParametro(new ItensCartaCoberturaQO(0, cartaCoberturaId, ""));

                CartaCoberturaRepository.Delete(cartaCoberturaId);

                MetodosGenericosService.StartTransactionCommitRollbackOrcamentaria(MetodosGenericosEnum.COMMIT);
            }
            catch (Exception)
            {
                MetodosGenericosService.StartTransactionCommitRollbackOrcamentaria(MetodosGenericosEnum.ROLLBACK);
                throw;
            }
        }
    }
}
