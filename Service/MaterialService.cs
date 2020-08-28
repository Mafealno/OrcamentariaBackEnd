using Microsoft.AspNetCore.Mvc;
using OrcamentariaBackEnd.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrcamentariaBackEnd
{
    public class MaterialService
    {

        private IMaterialRepository MaterialRepository;
        private MetodosGenericosService MetodosGenericosService;
        private PessoaService PessoaService;
        private ItensCartaCoberturaService ItensCartaCoberturaService;

        public MaterialService(IMaterialRepository materialRepository, MetodosGenericosService metodosGenericosService, 
            PessoaService pessoaService, ItensCartaCoberturaService itensCartaCoberturaService)
        {
            this.MaterialRepository = materialRepository;
            this.MetodosGenericosService = metodosGenericosService;
            this.PessoaService = pessoaService;
            this.ItensCartaCoberturaService = itensCartaCoberturaService;
        }

        public IEnumerable<MaterialModel> Get()
        {
            try
            {
                var listMaterial = MaterialRepository.List();

                foreach (MaterialModel materialModel in listMaterial)
                {
                    var pessoaId = MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_MATERIAL", $"MATERIAL_ID = {materialModel.MATERIAL_ID}");

                    materialModel.FABRICANTE = PessoaService.GetComParametro(new PessoaQO(int.Parse(pessoaId), "")).ToArray()[0];
                }

                return listMaterial;

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
                List<MaterialModel> listMaterial;

                if (!string.IsNullOrEmpty(material.NomeMaterial))
                {
                    listMaterial = MaterialRepository.ListPorNomeMaterial(material.NomeMaterial).ToList();
                }
                else if (!string.IsNullOrEmpty(material.NomeFabricante))
                {
                    listMaterial = MaterialRepository.ListPorNomeFabricante(material.NomeFabricante).ToList();
                }
                else
                {
                    listMaterial = new List<MaterialModel>();

                    listMaterial.Add(MaterialRepository.Find(material.MaterialId));
                }

                foreach(MaterialModel materialModel in listMaterial)
                {
                    var pessoaId = MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_MATERIAL", $"MATERIAL_ID = {materialModel.MATERIAL_ID}");

                    materialModel.FABRICANTE = PessoaService.GetComParametro(new PessoaQO(int.Parse(pessoaId), "")).ToArray()[0];
                }

                return listMaterial;
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
                var where = $"PESSOA_ID = {material.FABRICANTE.PESSOA_ID}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_PESSOA", where)))
                {
                    throw new Exception();
                }

                material.FABRICANTE = PessoaService.GetComParametro(new PessoaQO(material.FABRICANTE.PESSOA_ID, "")).ToArray()[0];

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
                var where = $"MATERIAL_ID = {materialId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("MATERIAL_ID", "T_ORCA_MATERIAL", where)))
                {
                    throw new Exception();
                }

                MaterialModel materialDB = GetComParametro(new MaterialQO(materialId, "", "")).ToList()[0];

                if (material.FABRICANTE.PESSOA_ID != materialDB.FABRICANTE.PESSOA_ID)
                {

                    CartaCoberturaRepository cartaCoberturaRepository = new CartaCoberturaRepository(new Conexao(MetodosGenericosService.RetornaConexao()));

                    List<CartaCoberturaModel> listCartaCobertura = cartaCoberturaRepository.ListPorMaterialId(materialId).ToList();

                    foreach (CartaCoberturaModel cartaCobertura in listCartaCobertura)
                    {
                        cartaCobertura.LIST_ITENS_CARTA_COBERTURA = ItensCartaCoberturaService.GetComParametro(new ItensCartaCoberturaQO(0, cartaCobertura.CARTA_COBERTURA_ID, "")).ToList();

                        cartaCobertura.MATERIAL = GetComParametro(new MaterialQO(materialId, "", "")).ToArray()[0];
                    }

                    where = $"PESSOA_ID = {material.FABRICANTE.PESSOA_ID}";
                    if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_PESSOA", where)))
                    {
                        throw new Exception();
                    }

                    var fabricante = PessoaService.GetComParametro(new PessoaQO(material.FABRICANTE.PESSOA_ID, "")).ToArray()[0];

                    foreach (CartaCoberturaModel cartaCobertura in listCartaCobertura)
                    {
                        cartaCobertura.MATERIAL.FABRICANTE = fabricante;

                        cartaCoberturaRepository.Update(cartaCobertura.CARTA_COBERTURA_ID, cartaCobertura);
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

        public void Delete(int materialId)
        {
            try
            {
                var where = $"MATERIAL_ID = {materialId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("MATERIAL_ID", "T_ORCA_MATERIAL", where)))
                {
                    throw new Exception();
                }

                MaterialRepository.Delete(materialId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
