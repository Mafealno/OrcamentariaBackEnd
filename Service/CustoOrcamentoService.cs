using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class CustoOrcamentoService
    {

        private ICustoOrcamentoRepository CustoOrcamentoRepository;
        private MetodosGenericosService MetodosGenericosService;
        private CustoService CustoService;

        public CustoOrcamentoService(ICustoOrcamentoRepository custoOrcamentoRepository, MetodosGenericosService metodosGenericosService,
            CustoService custoService)
        {
            this.CustoOrcamentoRepository = custoOrcamentoRepository;
            this.MetodosGenericosService = metodosGenericosService;
            this.CustoService = custoService;
        }

        public IEnumerable<CustoOrcamentoModel> Get()
        {
            try
            {
                var listCustoOrcamento = CustoOrcamentoRepository.List();

                foreach(CustoOrcamentoModel custoOrcamento in listCustoOrcamento)
                {
                    var custoId = MetodosGenericosService.DlookupOrcamentaria("CUSTO_ID", "T_ORCA_CUSTO_ORCAMENTO", $"CUSTO_ORCAMENTO_ID = {custoOrcamento.CUSTO_ORCAMENTO_ID}");

                    custoOrcamento.CUSTO_OBRA = CustoService.GetComParametro(new CustoQO(int.Parse(custoId), "")).ToArray()[0];
                }

                return listCustoOrcamento;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<CustoOrcamentoModel> GetComParametro(CustoOrcamentoQO custoOrcamento)
        {
            try
            {
                List<CustoOrcamentoModel> listCustoOrcamento;

                if (custoOrcamento.OrcamentoId != 0)
                {
                    listCustoOrcamento = CustoOrcamentoRepository.ListPorOrcamentoId(custoOrcamento.OrcamentoId).ToList();
                }
                else
                {
                    listCustoOrcamento = new List<CustoOrcamentoModel>();

                    listCustoOrcamento.Add(CustoOrcamentoRepository.Find(custoOrcamento.CustoOrcamentoId));
                }

                foreach (CustoOrcamentoModel custoOrcamentoModel in listCustoOrcamento)
                {
                    var custoId = MetodosGenericosService.DlookupOrcamentaria("CUSTO_ID", "T_ORCA_CUSTO_ORCAMENTO", $"CUSTO_ORCAMENTO_ID = {custoOrcamentoModel.CUSTO_ORCAMENTO_ID}");

                    custoOrcamentoModel.CUSTO_OBRA = CustoService.GetComParametro(new CustoQO(int.Parse(custoId), "")).ToArray()[0];
                }

                return listCustoOrcamento;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public CustoOrcamentoModel Post(CustoOrcamentoModel custoOrcamento)
        {
            try
            {
                var where = $"ORCAMENTO_ID = {custoOrcamento.ORCAMENTO_ID}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_ORCAMENTO", where)))
                {
                    throw new Exception();
                }

                where = $"CUSTO_ID = {custoOrcamento.CUSTO_OBRA.CUSTO_ID}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("CUSTO_ID", "T_ORCA_CUSTO", where)))
                {
                    throw new Exception();
                }

                return CustoOrcamentoRepository.Create(custoOrcamento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Put(int custoOrcamentoId, CustoOrcamentoModel custoOrcamento)
        {
            try
            {
                var where = $"CUSTO_ORCAMENTO_ID = {custoOrcamentoId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("CUSTO_ORCAMENTO_ID", "T_ORCA_CUSTO_ORCAMENTO", where)))
                {
                    throw new Exception();
                }

                where = $"CUSTO_ID = {custoOrcamento.CUSTO_OBRA.CUSTO_ID}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("CUSTO_ID", "T_ORCA_CUSTO", where)))
                {
                    throw new Exception();
                }

                var custoOrcamentoDB = GetComParametro(new CustoOrcamentoQO(custoOrcamentoId, 0)).ToArray()[0];

                if (custoOrcamento.CUSTO_OBRA.CUSTO_ID != custoOrcamentoDB.CUSTO_OBRA.CUSTO_ID)
                {
                    custoOrcamento.CUSTO_OBRA.VALOR_CUSTO = CustoService.GetComParametro(new CustoQO(custoOrcamento.CUSTO_OBRA.CUSTO_ID, "")).ToArray()[0].VALOR_CUSTO;
                }

                CustoOrcamentoRepository.Update(custoOrcamentoId, custoOrcamento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int DeleteComParametro(CustoOrcamentoQO custoOrcamento)
        {
            try
            {
                var orcamentoId = 0;

                if (custoOrcamento.OrcamentoId != 0)
                {
                    var where = $"ORCAMENTO_ID = {custoOrcamento.OrcamentoId}";
                    if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_ORCAMENTO", where)))
                    {
                        throw new Exception();
                    }

                    orcamentoId = custoOrcamento.OrcamentoId;

                    CustoOrcamentoRepository.DeletePorOrcamentoId(custoOrcamento.OrcamentoId);
                }
                else
                {
                    var where = $"CUSTO_ORCAMENTO_ID = {custoOrcamento.CustoOrcamentoId}";
                    if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("CUSTO_ORCAMENTO_ID", "T_ORCA_CUSTO_ORCAMENTO", where)))
                    {
                        throw new Exception();
                    }

                    orcamentoId = Int32.Parse(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_CUSTO_ORCAMENTO", where));

                    CustoOrcamentoRepository.Delete(custoOrcamento.CustoOrcamentoId);

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
