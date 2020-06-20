using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class TotaisOrcamentoService
    {
        private ITotaisOrcamentoRepository TotaisOrcamentoRepository;
        private MetodosGenericosService MetodosGenericosService;

        public TotaisOrcamentoService(ITotaisOrcamentoRepository totaisOrcamentoRepository, MetodosGenericosService metodosGenericosService)
        {
            this.TotaisOrcamentoRepository = totaisOrcamentoRepository;
            this.MetodosGenericosService = metodosGenericosService;
        }

        public IEnumerable<TotaisOrcamentoModel> Get()
        {
            try
            {
                return TotaisOrcamentoRepository.List();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<TotaisOrcamentoModel> GetComParametro(TotaisOrcamentoQO totaisOrcamento)
        {
            try
            {
                List<TotaisOrcamentoModel> listTotaisOrcamento = new List<TotaisOrcamentoModel>();

                if (totaisOrcamento.OrcamentoId != 0)
                {
                    listTotaisOrcamento.Add(TotaisOrcamentoRepository.FindPorOrcamentoId(totaisOrcamento.OrcamentoId));
                }
                else
                {
                    listTotaisOrcamento.Add(TotaisOrcamentoRepository.Find(totaisOrcamento.TotaisOrcamentoId));
                }

                return listTotaisOrcamento;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TotaisOrcamentoModel Post(TotaisOrcamentoModel totaisOrcamento)
        {
            try
            {
                var where = $"ORCAMENTO_ID = {totaisOrcamento.ORCAMENTO_ID}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_ORCAMENTO", where)))
                {
                    throw new Exception();
                }

                return TotaisOrcamentoRepository.Create(totaisOrcamento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Put(int totaisOrcamentoId, TotaisOrcamentoModel totaisOrcamento)
        {
            try
            {
                var where = $"TOTAIS_ORCAMENTO_ID = {totaisOrcamentoId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("totaisOrcamentoId", "T_ORCA_TOTAIS_ORCAMENTO", where)))
                {
                    throw new Exception();
                }

                TotaisOrcamentoRepository.Update(totaisOrcamentoId, totaisOrcamento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteComParametro(TotaisOrcamentoQO totaisOrcamento)
        {
            try
            {
                if (totaisOrcamento.OrcamentoId != 0)
                {
                    var where = $"ORCAMENTO_ID = {totaisOrcamento.OrcamentoId}";
                    if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_ORCAMENTO", where)))
                    {
                        throw new Exception();
                    }

                    TotaisOrcamentoRepository.DeletePorOrcamentoId(totaisOrcamento.OrcamentoId);
                }
                else
                {
                    var where = $"TOTAIS_ORCAMENTO_ID = {totaisOrcamento.TotaisOrcamentoId}";
                    if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("totaisOrcamentoId", "T_ORCA_TOTAIS_ORCAMENTO", where)))
                    {
                        throw new Exception();
                    }
                    TotaisOrcamentoRepository.DeletePorOrcamentoId(totaisOrcamento.TotaisOrcamentoId);
                }   
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
