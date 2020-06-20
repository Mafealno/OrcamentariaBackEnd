using KellermanSoftware.CompareNetObjects;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class ItensOrcamentoIntumescenteService
    {
        private IItensOrcamentoIntumescenteRepository ItensOrcamentoIntumescenteRepository;
        private MetodosGenericosService MetodosGenericosService;
        private ItensOrcamentoService ItensOrcamentoService;
        private MaterialService MaterialService;
        private PerfilService PerfilService;
        private CartaCoberturaService CartaCoberturaService;

        public ItensOrcamentoIntumescenteService(IItensOrcamentoIntumescenteRepository itensOrcamentoIntumescenteRepository, MetodosGenericosService metodosGenericosService,
            ItensOrcamentoService itensOrcamentoService, MaterialService materialService, PerfilService perfilService, CartaCoberturaService cartaCoberturaService)
        {
            this.ItensOrcamentoIntumescenteRepository = itensOrcamentoIntumescenteRepository;
            this.MetodosGenericosService = metodosGenericosService;
            this.ItensOrcamentoService = itensOrcamentoService;
            this.MaterialService = materialService;
            this.PerfilService = perfilService;
            this.CartaCoberturaService = cartaCoberturaService;
        }

        public IEnumerable<ItensOrcamentoIntumescenteModel> Get()
        {
            try
            {
                var listItensOrcamentoIntumescente = ItensOrcamentoIntumescenteRepository.List();

                foreach (ItensOrcamentoIntumescenteModel itensOrcamentoIntumescente in listItensOrcamentoIntumescente)
                {
                    var materialId = MetodosGenericosService.DlookupOrcamentaria("MATERIAL_ID", "T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE", $"ITENS_ORCAMENTO_ID = {itensOrcamentoIntumescente.ITENS_ORCAMENTO_ID}");

                    itensOrcamentoIntumescente.PRODUTO = MaterialService.GetComParametro(new MaterialQO(int.Parse(materialId), "", "")).ToArray()[0];

                    var perfilId = MetodosGenericosService.DlookupOrcamentaria("PERFIL_ID", "T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE", $"ITENS_ORCAMENTO_ID = {itensOrcamentoIntumescente.ITENS_ORCAMENTO_ID}");

                    if(int.Parse(perfilId) != 0)
                    {
                        itensOrcamentoIntumescente.PERFIL = PerfilService.GetComParametro(new PerfilQO(int.Parse(perfilId), "")).ToArray()[0];
                    }

                    var cartaCoberturId = MetodosGenericosService.DlookupOrcamentaria("CARTA_COBERTURA_ID", "T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE", $"ITENS_ORCAMENTO_ID = {itensOrcamentoIntumescente.ITENS_ORCAMENTO_ID}");

                    if (int.Parse(cartaCoberturId) != 0)
                    {
                        itensOrcamentoIntumescente.CARTA_COBERTURA = CartaCoberturaService.GetComParametro(new CartaCoberturaQO(int.Parse(cartaCoberturId), 0, 0, "")).ToArray()[0];
                    }

                }

                return listItensOrcamentoIntumescente;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ItensOrcamentoIntumescenteModel> GetComParametro(ItensOrcamentoQO itensOrcamentoIntumescente)
        {
            try
            {
                List<ItensOrcamentoIntumescenteModel> listItensOrcamentoIntumescente;

                if (itensOrcamentoIntumescente.OrcamentoId != 0)
                {
                    listItensOrcamentoIntumescente = ItensOrcamentoIntumescenteRepository.ListPorOrcamentoId(itensOrcamentoIntumescente.OrcamentoId).ToList();
                }
                else
                {
                    listItensOrcamentoIntumescente = new List<ItensOrcamentoIntumescenteModel>();

                    listItensOrcamentoIntumescente.Add(ItensOrcamentoIntumescenteRepository.Find(itensOrcamentoIntumescente.ItensOrcamentoId));
                }

                foreach (ItensOrcamentoIntumescenteModel itensOrcamentoIntumescenteModel in listItensOrcamentoIntumescente)
                {
                    var materialId = MetodosGenericosService.DlookupOrcamentaria("MATERIAL_ID", "T_ORCA_ITENS_ORCAMENTO", $"ITENS_ORCAMENTO_ID = {itensOrcamentoIntumescenteModel.ITENS_ORCAMENTO_ID}");

                    itensOrcamentoIntumescenteModel.PRODUTO = MaterialService.GetComParametro(new MaterialQO(int.Parse(materialId), "", "")).ToArray()[0];

                    var perfilId = MetodosGenericosService.DlookupOrcamentaria("PERFIL_ID", "T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE", $"ITENS_ORCAMENTO_ID = {itensOrcamentoIntumescenteModel.ITENS_ORCAMENTO_ID}");

                    if (int.Parse(perfilId) != 0)
                    {
                        itensOrcamentoIntumescenteModel.PERFIL = PerfilService.GetComParametro(new PerfilQO(int.Parse(perfilId), "")).ToArray()[0];
                    }

                    var cartaCoberturId = MetodosGenericosService.DlookupOrcamentaria("CARTA_COBERTURA_ID", "T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE", $"ITENS_ORCAMENTO_ID = {itensOrcamentoIntumescenteModel.ITENS_ORCAMENTO_ID}");

                    if (int.Parse(cartaCoberturId) != 0)
                    {
                        itensOrcamentoIntumescenteModel.CARTA_COBERTURA = CartaCoberturaService.GetComParametro(new CartaCoberturaQO(int.Parse(cartaCoberturId), 0, 0, "")).ToArray()[0];
                    }
                }

                return listItensOrcamentoIntumescente;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ItensOrcamentoIntumescenteModel Post(ItensOrcamentoIntumescenteModel itensOrcamentoIntumescente)
        {
            try
            {
                var where = $"ORCAMENTO_ID = {itensOrcamentoIntumescente.ORCAMENTO_ID}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_ORCAMENTO", where)))
                {
                    throw new Exception();
                }

                if (itensOrcamentoIntumescente.NUMERO_FACES < 0 || itensOrcamentoIntumescente.QTDE < 0)
                {
                    throw new Exception();
                }

                if(itensOrcamentoIntumescente.PERFIL.PERFIL_ID != 0)
                {
                    itensOrcamentoIntumescente.PERFIL = PerfilService.GetComParametro(new PerfilQO(itensOrcamentoIntumescente.PERFIL.PERFIL_ID, "")).ToArray()[0];
                }

                if(itensOrcamentoIntumescente.CARTA_COBERTURA.CARTA_COBERTURA_ID != 0)
                {
                    itensOrcamentoIntumescente.CARTA_COBERTURA = CartaCoberturaService.GetComParametro(new CartaCoberturaQO(itensOrcamentoIntumescente.CARTA_COBERTURA.CARTA_COBERTURA_ID, 0, 0, "")).ToArray()[0];
                }

                MetodosGenericosService.StartTransactionCommitRollbackOrcamentaria(MetodosGenericosEnum.START);

                var itensOrcamento = MetodosGenericosService.CopiarPropriedadesObj(itensOrcamentoIntumescente, new ItensOrcamentoModel());

                itensOrcamento = ItensOrcamentoService.Post(itensOrcamento);

                itensOrcamentoIntumescente.ITENS_ORCAMENTO_ID = itensOrcamento.ITENS_ORCAMENTO_ID;

                itensOrcamentoIntumescente = ItensOrcamentoIntumescenteRepository.Create(itensOrcamentoIntumescente);

                MetodosGenericosService.StartTransactionCommitRollbackOrcamentaria(MetodosGenericosEnum.COMMIT);

                return itensOrcamentoIntumescente;

            }
            catch (Exception)
            {
                MetodosGenericosService.StartTransactionCommitRollbackOrcamentaria(MetodosGenericosEnum.ROLLBACK);
                throw;
            }
        }

        public void Put(int itensOrcamentoId, ItensOrcamentoIntumescenteModel itensOrcamentoIntumescente)
        {
            try
            {
                var where = $"ITENS_ORCAMENTO_ID = {itensOrcamentoId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ITENS_ORCAMENTO_ID", "T_ORCA_ITENS_ORCAMENTO_GERAL", where)))
                {
                    throw new Exception();
                }

                var itensOrcamento = MetodosGenericosService.CopiarPropriedadesObj(itensOrcamentoIntumescente, new ItensOrcamentoModel());

                var itensOrcamentoDB = ItensOrcamentoService.GetComParametro(new ItensOrcamentoQO(itensOrcamentoId, 0)).ToArray()[0];

                ComparisonResult resultando = new CompareLogic().Compare(itensOrcamento, itensOrcamentoDB);

                if (!resultando.AreEqual)
                {
                    var res = resultando.Differences;

                    ItensOrcamentoService.Put(itensOrcamentoId, itensOrcamento);
                }

                if (itensOrcamentoIntumescente.VALOR_COMPRIMENTO < 0 || itensOrcamentoIntumescente.AREA < 0)
                {
                    throw new Exception();
                }

                ItensOrcamentoIntumescenteRepository.Update(itensOrcamentoId, itensOrcamentoIntumescente);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteComParametro(ItensOrcamentoQO itensOrcamentoIntumescente)
        {
            try
            {
                MetodosGenericosService.StartTransactionCommitRollbackOrcamentaria(MetodosGenericosEnum.START);

                if (itensOrcamentoIntumescente.OrcamentoId != 0)
                {
                    ItensOrcamentoIntumescenteRepository.DeletePorOrcamentoId(itensOrcamentoIntumescente.OrcamentoId);
                    ItensOrcamentoService.DeleteComParametro(itensOrcamentoIntumescente);
                }
                else
                {
                    ItensOrcamentoIntumescenteRepository.Delete(itensOrcamentoIntumescente.ItensOrcamentoId);

                    ItensOrcamentoService.DeleteComParametro(itensOrcamentoIntumescente);
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
