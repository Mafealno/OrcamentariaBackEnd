using KellermanSoftware.CompareNetObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class ItensOrcamentoGeralService
    {
        private IItensOrcamentoGeralRepository ItensOrcamentoGeralRepository;
        private MetodosGenericosService MetodosGenericosService;
        private ItensOrcamentoService ItensOrcamentoService;
        private MaterialService MaterialService;

        public ItensOrcamentoGeralService(IItensOrcamentoGeralRepository itensOrcamentoGeralRepository, MetodosGenericosService metodosGenericosService,
            ItensOrcamentoService itensOrcamentoService, MaterialService materialService)
        {
            this.ItensOrcamentoGeralRepository = itensOrcamentoGeralRepository;
            this.MetodosGenericosService = metodosGenericosService;
            this.ItensOrcamentoService = itensOrcamentoService;
            this.MaterialService = materialService;
        }

        public IEnumerable<ItensOrcamentoGeralModel> Get()
        {
            try
            {
                var listItensOrcamentoGeral = ItensOrcamentoGeralRepository.List();

                foreach (ItensOrcamentoGeralModel itensOrcamentoGeral in listItensOrcamentoGeral)
                {
                    var materialId = MetodosGenericosService.DlookupOrcamentaria("MATERIAL_ID", "T_ORCA_ITENS_ORCAMENTO", $"ITENS_ORCAMENTO_ID = {itensOrcamentoGeral.ITENS_ORCAMENTO_ID}");

                    itensOrcamentoGeral.PRODUTO = MaterialService.GetComParametro(new MaterialQO(int.Parse(materialId), "", "")).ToArray()[0];
                }

                return listItensOrcamentoGeral;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ItensOrcamentoGeralModel> GetComParametro(ItensOrcamentoQO itensOrcamentoGeral)
        {
            try
            {
                List<ItensOrcamentoGeralModel> listItensOrcamentoGeral;

                if (itensOrcamentoGeral.OrcamentoId != 0)
                {
                    listItensOrcamentoGeral = ItensOrcamentoGeralRepository.ListPorOrcamentoId(itensOrcamentoGeral.OrcamentoId).ToList();
                }
                else
                {
                    listItensOrcamentoGeral = new List<ItensOrcamentoGeralModel>();

                    listItensOrcamentoGeral.Add(ItensOrcamentoGeralRepository.Find(itensOrcamentoGeral.ItensOrcamentoId));
                }

                foreach (ItensOrcamentoGeralModel itensOrcamentoGeralModel in listItensOrcamentoGeral)
                {
                    var materialId = MetodosGenericosService.DlookupOrcamentaria("MATERIAL_ID", "T_ORCA_ITENS_ORCAMENTO", $"ITENS_ORCAMENTO_ID = {itensOrcamentoGeralModel.ITENS_ORCAMENTO_ID}");

                    itensOrcamentoGeralModel.PRODUTO = MaterialService.GetComParametro(new MaterialQO(int.Parse(materialId), "", "")).ToArray()[0];
                }

                return listItensOrcamentoGeral;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ItensOrcamentoGeralModel Post(ItensOrcamentoGeralModel itensOrcamentoGeral)
        {
            try
            {
                var where = $"ORCAMENTO_ID = {itensOrcamentoGeral.ORCAMENTO_ID}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_ORCAMENTO", where)))
                {
                    throw new Exception();
                }

                if (itensOrcamentoGeral.VALOR_LARGURA < 0 || itensOrcamentoGeral.VALOR_M_2 <0)
                {
                    throw new Exception();
                }

                MetodosGenericosService.StartTransactionCommitRollbackOrcamentaria(MetodosGenericosEnum.START);

                var itensOrcamento = MetodosGenericosService.CopiarPropriedadesObj(itensOrcamentoGeral, new ItensOrcamentoModel());

                itensOrcamento = ItensOrcamentoService.Post(itensOrcamento);

                itensOrcamentoGeral.ITENS_ORCAMENTO_ID = itensOrcamento.ITENS_ORCAMENTO_ID;

                itensOrcamentoGeral = ItensOrcamentoGeralRepository.Create(itensOrcamentoGeral);

                itensOrcamentoGeral.PRODUTO = itensOrcamento.PRODUTO;

                MetodosGenericosService.StartTransactionCommitRollbackOrcamentaria(MetodosGenericosEnum.COMMIT);

                return itensOrcamentoGeral;

            }
            catch (Exception)
            {
                MetodosGenericosService.StartTransactionCommitRollbackOrcamentaria(MetodosGenericosEnum.ROLLBACK);
                throw;
            }
        }

        public void Put(int itensOrcamentoId, ItensOrcamentoGeralModel itensOrcamentoGeral)
        {
            try
            {
                var where = $"ITENS_ORCAMENTO_ID = {itensOrcamentoId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ITENS_ORCAMENTO_ID", "T_ORCA_ITENS_ORCAMENTO_GERAL", where)))
                {
                    throw new Exception();
                }

                var itensOrcamento = MetodosGenericosService.CopiarPropriedadesObj(itensOrcamentoGeral, new ItensOrcamentoModel());

                ItensOrcamentoService.Put(itensOrcamentoId, itensOrcamento);
                
                if (itensOrcamentoGeral.VALOR_COMPRIMENTO < 0 || itensOrcamentoGeral.AREA < 0)
                {
                    throw new Exception();
                }

                ItensOrcamentoGeralRepository.Update(itensOrcamentoId, itensOrcamentoGeral);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteComParametro(ItensOrcamentoQO itensOrcamentoGeral)
        {
            try
            {
                MetodosGenericosService.StartTransactionCommitRollbackOrcamentaria(MetodosGenericosEnum.START);

                if (itensOrcamentoGeral.OrcamentoId != 0)
                {
                    ItensOrcamentoGeralRepository.DeletePorOrcamentoId(itensOrcamentoGeral.OrcamentoId);
                    ItensOrcamentoService.DeleteComParametro(itensOrcamentoGeral);
                }
                else
                {
                    ItensOrcamentoGeralRepository.Delete(itensOrcamentoGeral.ItensOrcamentoId);

                    ItensOrcamentoService.DeleteComParametro(itensOrcamentoGeral);
                }

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
