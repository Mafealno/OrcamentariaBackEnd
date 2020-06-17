using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class CartaCoberturaService
    {
        private MetodosGenericosService MetodosGenericosService;
        private ICartaCoberturaRepository CartaCoberturaRepository;
        private IItensCartaCoberturaRepository ItensCartaCoberturaRepository;
        private IMaterialRepository MaterialRepository;

        public CartaCoberturaService(MetodosGenericosService metodosGenericosservice, ICartaCoberturaRepository cartaCoberturaRepository,
            IItensCartaCoberturaRepository itensCartaCoberturaRepository, IMaterialRepository materialRepository)
        {
            this.CartaCoberturaRepository = cartaCoberturaRepository;
            this.ItensCartaCoberturaRepository = itensCartaCoberturaRepository;
            this.MetodosGenericosService = metodosGenericosservice;
            this.MaterialRepository = materialRepository;
        }

        public IEnumerable<CartaCoberturaModel> Get()
        {
            try
            {
                return CartaCoberturaRepository.List();
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
                //VERIFICA SE A PESSOA EXISTE
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_PESSOA", where)))
                {
                    throw new Exception();
                }

                return CartaCoberturaRepository.ListPorReferenciaEPessoaId(referencia, pessoaId);
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
                //VERIFICA SE A PESSOA EXISTE
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_PESSOA", where)))
                {
                    throw new Exception();
                }

                where = $"PESSOA_ID = {pessoaId} AND MATERIAL_ID = {materialId}";
                //VERIFICA SE O MATERIAL EXISTE
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("MATERIAL_ID", "T_ORCA_MATERIAL", where)))
                {
                    throw new Exception();
                }

                return CartaCoberturaRepository.ListPorReferenciaEPessoaIdEMaterialId(referencia, pessoaId, materialId);
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
                if (!string.IsNullOrEmpty(cartaCobertura.Referencia))
                {
                    return CartaCoberturaRepository.ListPorReferencia(cartaCobertura.Referencia);
                }
                else if (cartaCobertura.MaterialId != 0)
                {
                    var where = $"MATERIAL_ID = {cartaCobertura.MaterialId}";
                    //VERIFICA SE O MATERIAL EXISTE
                    if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("MATERIAL_ID", "T_ORCA_MATERIAL", where)))
                    {
                        throw new Exception();
                    }
                    return CartaCoberturaRepository.ListPorMaterialId(cartaCobertura.MaterialId);
                }
                else if (cartaCobertura.PessoaId != 0)
                {
                    var where = $"PESSOA_ID = {cartaCobertura.PessoaId}";
                    //VERIFICA SE A PESSOA EXISTE
                    if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_PESSOA", where)))
                    {
                        throw new Exception();
                    }
                    return CartaCoberturaRepository.ListPorPessoaId(cartaCobertura.PessoaId);
                }
                else
                {
                    List<CartaCoberturaModel> listCartaCobertura = new List<CartaCoberturaModel>();

                    listCartaCobertura.Add(CartaCoberturaRepository.Find(cartaCobertura.CartaCoberturaId));

                    return listCartaCobertura;
                }
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
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("MATERIAL_ID", "T_ORCA_PESSOA_ID", where)))
                {
                    throw new Exception();
                }

                cartaCobertura.MATERIAL = MaterialRepository.Find(cartaCobertura.MATERIAL.MATERIAL_ID);

                return CartaCoberturaRepository.Create(cartaCobertura);

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

                cartaCobertura.MATERIAL = MaterialRepository.Find(cartaCobertura.MATERIAL.MATERIAL_ID);

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
                MetodosGenericosService.StartTransactionCommitRollback(MetodosGenericosEnum.START);

                var where = $"CARTA_COBERTURA_ID = {cartaCoberturaId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("CARTA_COBERTURA_ID", "T_ORCA_CARTA_COBERTURA", where)))
                {
                    throw new Exception();
                }

                ItensCartaCoberturaRepository.DeletePorCartaCoberturaId(cartaCoberturaId);

                CartaCoberturaRepository.Delete(cartaCoberturaId);

                MetodosGenericosService.StartTransactionCommitRollback(MetodosGenericosEnum.COMMIT);
            }
            catch (Exception)
            {
                MetodosGenericosService.StartTransactionCommitRollback(MetodosGenericosEnum.ROLLBACK);
                throw;
            }
        }
    }
}
