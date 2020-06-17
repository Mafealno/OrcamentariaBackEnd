using System;
using System.Collections.Generic;
using System.Linq;

namespace OrcamentariaBackEnd
{
    public class MaterialService
    {

        private IMaterialRepository MaterialRepository;
        private IPessoaRepository PessoaRepository;
        private ICartaCoberturaRepository CartaCoberturaRepository;

        public MaterialService(IMaterialRepository materialRepository, ICartaCoberturaRepository cartaCoberturaRepository, IPessoaRepository pessoaRepository)
        {
            this.MaterialRepository = materialRepository;
            this.PessoaRepository = pessoaRepository;
            this.CartaCoberturaRepository = cartaCoberturaRepository;
        }

        public IEnumerable<MaterialModel> Get()
        {
            try
            {
                return MaterialRepository.List();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<MaterialModel> GetComParametro(MaterialQO material)
        {
            try
            {
                if (!string.IsNullOrEmpty(material.NomeMaterial))
                {
                    return MaterialRepository.ListPorNomeMaterial(material.NomeMaterial);
                }
                else if (!string.IsNullOrEmpty(material.NomeFabricante))
                {
                    return MaterialRepository.ListPorNomeFabricante(material.NomeFabricante);
                }
                else
                {
                    List<MaterialModel> listMaterial = new List<MaterialModel>();

                    listMaterial.Add(MaterialRepository.Find(material.MaterialId));

                    return listMaterial;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public MaterialModel Post(MaterialModel material)
        {
            try
            {
                material.FABRICANTE = PessoaRepository.Find(material.FABRICANTE.PESSOA_ID);

                return MaterialRepository.Create(material);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Put(int materialId, MaterialModel material)
        {
            try
            {
                MaterialModel materialDB = MaterialRepository.Find(materialId);

                if(material.FABRICANTE.PESSOA_ID != materialDB.FABRICANTE.PESSOA_ID)
                {
                    List<CartaCoberturaModel> listCartaCobertura = CartaCoberturaRepository.ListPorMaterialIdEPessoaId(materialId, materialDB.FABRICANTE.PESSOA_ID).ToList();

                    var fabricante = PessoaRepository.Find(material.FABRICANTE.PESSOA_ID);

                    foreach (CartaCoberturaModel cartaCobertura in listCartaCobertura)
                    {
                        cartaCobertura.MATERIAL.FABRICANTE = fabricante;
                        
                        CartaCoberturaRepository.Update(cartaCobertura.CARTA_COBERTURA_ID, cartaCobertura);
                    }

                    material.FABRICANTE = fabricante; 

                }
                
                MaterialRepository.Update(materialId, material);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
