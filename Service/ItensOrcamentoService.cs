using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class ItensOrcamentoService
    {
        private IItensOrcamentoRepository ItensOrcamentoRepository;
        private MetodosGenericosService MetodosGenericosService;
        private MaterialService MaterialService;

        public ItensOrcamentoService(IItensOrcamentoRepository itensOrcamentoRepository, MetodosGenericosService metodosGenericosService,
            MaterialService materialService)
        {
            this.ItensOrcamentoRepository = itensOrcamentoRepository;
            this.MetodosGenericosService = metodosGenericosService;
            this.MaterialService = materialService;
        }

        public IEnumerable<ItensOrcamentoModel> Get()
        {
            try
            {
                var listItensOrcamento = ItensOrcamentoRepository.List();

                return listItensOrcamento;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ItensOrcamentoModel> GetComParametro(ItensOrcamentoQO itensOrcamento)
        {
            try
            {
                List<ItensOrcamentoModel> listItensOrcamento;

                if(itensOrcamento.OrcamentoId != 0)
                {
                    listItensOrcamento = ItensOrcamentoRepository.ListPorOrcamentoId(itensOrcamento.OrcamentoId).ToList();
                }
                else
                {
                    listItensOrcamento = new List<ItensOrcamentoModel>();

                    listItensOrcamento.Add(ItensOrcamentoRepository.Find(itensOrcamento.ItensOrcamentoId));
                }

                return listItensOrcamento;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ItensOrcamentoModel Post(ItensOrcamentoModel itensOrcamento)
        {
            try
            {
                var where = $"ORCAMENTO_ID = {itensOrcamento.ORCAMENTO_ID}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_ORCAMENTO", where)))
                {
                    throw new Exception();
                }

                if (itensOrcamento.VALOR_COMPRIMENTO < 0 || itensOrcamento.AREA < 0)
                {
                    throw new Exception();
                }

                where = $"ORCAMENTO_ID = {itensOrcamento.ORCAMENTO_ID}";
                var ultimaNumeroLinha = MetodosGenericosService.DlookupOrcamentaria("IF(MAX(NUMERO_LINHA) IS NULL, 0, MAX(NUMERO_LINHA))", "T_ORCA_ITENS_ORCAMENTO", where);

                itensOrcamento.NUMERO_LINHA = int.Parse(ultimaNumeroLinha) + 1;

                var novoItensOrcamento = ItensOrcamentoRepository.Create(itensOrcamento);

                return novoItensOrcamento;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Put(int itensOrcamentoId, ItensOrcamentoModel itensOrcamento)
        {
            try
            {
                var where = $"ITENS_ORCAMENTO_ID = {itensOrcamentoId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ITENS_ORCAMENTO_ID", "T_ORCA_ITENS_ORCAMENTO", where)))
                {
                    throw new Exception();
                }

                var itensOrcamentoDB = GetComParametro(new ItensOrcamentoQO(itensOrcamentoId, 0)).ToArray()[0];

                if (itensOrcamento.VALOR_COMPRIMENTO < 0 || itensOrcamento.AREA < 0)
                {
                    throw new Exception();
                }

                ItensOrcamentoRepository.Update(itensOrcamentoId, itensOrcamento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteComParametro(ItensOrcamentoQO itensOrcamento)
        {
            try
            {
                if (itensOrcamento.OrcamentoId != 0)
                {
                    ItensOrcamentoRepository.DeletePorOrcamentoId(itensOrcamento.OrcamentoId);
                }
                else
                {
                    var where = $"ITENS_ORCAMENTO_ID = {itensOrcamento.ItensOrcamentoId}";
                    var orcamentoId = MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_ITENS_ORCAMENTO", where);

                    var numeroLinha = MetodosGenericosService.DlookupOrcamentaria("NUMERO_LINHA", "T_ORCA_ITENS_ORCAMENTO", where);

                    var listitensOrcamento = GetComParametro(new ItensOrcamentoQO(0, int.Parse(orcamentoId)));

                    var itensOrcamentoFiltrados = listitensOrcamento.Where(itemOrcamento => itemOrcamento.NUMERO_LINHA > int.Parse(numeroLinha));

                    ItensOrcamentoRepository.Delete(itensOrcamento.ItensOrcamentoId);

                    foreach(ItensOrcamentoModel itensOrcamentoModel in itensOrcamentoFiltrados)
                    {
                        itensOrcamentoModel.NUMERO_LINHA -= 1;
                        ItensOrcamentoRepository.Update(itensOrcamentoModel.ITENS_ORCAMENTO_ID, itensOrcamentoModel);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
