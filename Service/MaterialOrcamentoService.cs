using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class MaterialOrcamentoService
    {
        private IMaterialOrcamentoRepository MaterialOrcamentoRepository;
        private MetodosGenericosService MetodosGenericosService;
        private MaterialService MaterialService;

        public MaterialOrcamentoService(IMaterialOrcamentoRepository materialOrcamentoRepository, MetodosGenericosService metodosGenericosService,
            MaterialService materialService)
        {
            this.MaterialOrcamentoRepository = materialOrcamentoRepository;
            this.MetodosGenericosService = metodosGenericosService;
            this.MaterialService = materialService;
        }

        public IEnumerable<MaterialOrcamentoModel> Get()
        {
            try
            {
                var listMaterialOrcamento = MaterialOrcamentoRepository.List();

                foreach (MaterialOrcamentoModel materialOrcamento in listMaterialOrcamento)
                {
                    var materialId = MetodosGenericosService.DlookupOrcamentaria("MATERIAL_ID", "T_ORCA_MATERIAL_ORCAMENTO", $"MATERIAL_ORCAMENTO_ID = {materialOrcamento.MATERIAL_ORCAMENTO_ID}");

                    materialOrcamento.MATERIAL = MaterialService.GetComParametro(new MaterialQO(int.Parse(materialId), "", "")).ToArray()[0];
                }

                return listMaterialOrcamento;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<MaterialOrcamentoModel> GetComParametro(MaterialOrcamentoQO materialOrcamento)
        {
            try
            {
                List<MaterialOrcamentoModel> listMaterialOrcamento;

                if (materialOrcamento.OrcamentoId != 0)
                {
                    var where = $"ORCAMENTO_ID = {materialOrcamento.OrcamentoId}";
                    if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_ORCAMENTO", where)))
                    {
                        throw new Exception();
                    }

                    listMaterialOrcamento = MaterialOrcamentoRepository.ListPorOrcamentoId(materialOrcamento.OrcamentoId).ToList();
                }
                else
                {
                    listMaterialOrcamento = new List<MaterialOrcamentoModel>();

                    listMaterialOrcamento.Add(MaterialOrcamentoRepository.Find(materialOrcamento.MaterialOrcamentoId));
                }

                foreach (MaterialOrcamentoModel materialOrcamentoModel in listMaterialOrcamento)
                {
                    var materialId = MetodosGenericosService.DlookupOrcamentaria("MATERIAL_ID", "T_ORCA_MATERIAL_ORCAMENTO", $"MATERIAL_ORCAMENTO_ID = {materialOrcamentoModel.MATERIAL_ORCAMENTO_ID}");

                    materialOrcamentoModel.MATERIAL = MaterialService.GetComParametro(new MaterialQO(int.Parse(materialId), "", "")).ToArray()[0];
                }

                return listMaterialOrcamento;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public MaterialOrcamentoModel Post(MaterialOrcamentoModel materialOrcamento)
        {
            try
            {
                var where = $"ORCAMENTO_ID = {materialOrcamento.ORCAMENTO_ID}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_ORCAMENTO", where)))
                {
                    throw new Exception();
                }

                where = $"MATERIAL_ID = {materialOrcamento.MATERIAL.MATERIAL_ID}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("MATERIAL_ID", "T_ORCA_MATERIAL", where)))
                {
                    throw new Exception();
                }

                if (materialOrcamento.VALOR_UNITARIO_MATERIAL < 0 || materialOrcamento.QTDE_MATERIAL < 0)
                {
                    throw new Exception();
                }

                materialOrcamento.MATERIAL = MaterialService.GetComParametro(new MaterialQO(materialOrcamento.MATERIAL.MATERIAL_ID, "", "")).FirstOrDefault();

                var materialOrcamentoCadastrado = MaterialOrcamentoRepository.Create(materialOrcamento);

                materialOrcamentoCadastrado.MATERIAL = materialOrcamento.MATERIAL;

                return materialOrcamentoCadastrado;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Put(int materialOrcamentoId, MaterialOrcamentoModel materialOrcamento)
        {
            try
            {
                var where = $"MATERIAL_ORCAMENTO_ID = {materialOrcamentoId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("MATERIAL_ORCAMENTO_ID", "T_ORCA_MATERIAL_ORCAMENTO", where)))
                {
                    throw new Exception();
                }

                var equipamentoOrcamentoDB = GetComParametro(new MaterialOrcamentoQO(materialOrcamentoId, 0)).ToArray()[0];

                if (materialOrcamento.MATERIAL.MATERIAL_ID != equipamentoOrcamentoDB.MATERIAL.MATERIAL_ID)
                {
                    materialOrcamento.MATERIAL = MaterialService.GetComParametro(new MaterialQO(materialOrcamento.MATERIAL.MATERIAL_ID, "", "")).ToArray()[0];
                }

                if (materialOrcamento.VALOR_UNITARIO_MATERIAL < 0 || materialOrcamento.QTDE_MATERIAL < 0)
                {
                    throw new Exception();
                }

                MaterialOrcamentoRepository.Update(materialOrcamentoId, materialOrcamento);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public int DeleteComParamenro(MaterialOrcamentoQO materialOrcamento)
        {
            try
            {

                var orcamentoId = 0;
                if (materialOrcamento.OrcamentoId != 0)
                {
                    var where = $"ORCAMENTO_ID = {materialOrcamento.OrcamentoId}";
                    if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_ORCAMENTO", where)))
                    {
                        throw new Exception();
                    }

                    orcamentoId = materialOrcamento.OrcamentoId;

                    MaterialOrcamentoRepository.DeletePorOrcamentoId(materialOrcamento.OrcamentoId);
                }
                else
                {
                    var where = $"MATERIAL_ORCAMENTO_ID = {materialOrcamento.MaterialOrcamentoId}";
                    if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("MATERIAL_ORCAMENTO_ID", "T_ORCA_MATERIAL_ORCAMENTO", where)))
                    {
                        throw new Exception();
                    }

                    orcamentoId = Int32.Parse(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_MATERIAL_ORCAMENTO", where));

                    MaterialOrcamentoRepository.Delete(materialOrcamento.MaterialOrcamentoId);
                }

                return orcamentoId;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
