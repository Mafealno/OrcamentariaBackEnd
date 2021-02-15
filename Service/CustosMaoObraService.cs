using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class CustosMaoObraService
    {
        private ICustosMaoObraRepository CustosMaoObraRepository;
        private MetodosGenericosService MetodosGenericosService;

        public CustosMaoObraService(ICustosMaoObraRepository custosMaoObraRepository, MetodosGenericosService metodosGenericosService)
        {
            this.CustosMaoObraRepository = custosMaoObraRepository;
            this.MetodosGenericosService = metodosGenericosService;
        }

        public IEnumerable<CustoModel> Get(int maoObraOrcamentoId)
        {
            try
            {
                var where = $"MAO_OBRA_ORCAMENTO_ID = {maoObraOrcamentoId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("MAO_OBRA_ORCAMENTO_ID", "T_ORCA_MAO_OBRA_ORCAMENTO", where)))
                {
                    throw new Exception();
                }

                return CustosMaoObraRepository.List(maoObraOrcamentoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CustoModel Get(int maoObraOrcamentoId, int custoId)
        {
            try
            {
                var where = $"MAO_OBRA_ORCAMENTO_ID = {maoObraOrcamentoId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("MAO_OBRA_ORCAMENTO_ID", "T_ORCA_MAO_OBRA_ORCAMENTO", where)))
                {
                    throw new Exception();
                }

                where = $"CUSTO_ID = {custoId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("CUSTO_ID", "T_ORCA_CUSTO", where)))
                {
                    throw new Exception();
                }

                return CustosMaoObraRepository.Find(maoObraOrcamentoId, custoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Post(MaoObraOrcamentoModel maoObraOrcamento, CustoModel custo)
        {
            try
            {
                var where = $"MAO_OBRA_ORCAMENTO_ID = {maoObraOrcamento.MAO_OBRA_ORCAMENTO_ID}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("MAO_OBRA_ORCAMENTO_ID", "T_ORCA_MAO_OBRA_ORCAMENTO", where)))
                {
                    throw new Exception();
                }

                where = $"CUSTO_ID = {custo.CUSTO_ID}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("CUSTO_ID", "T_ORCA_CUSTO", where)))
                {
                    throw new Exception();
                }

                CustosMaoObraRepository.Create(maoObraOrcamento, custo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Put(int maoObraOrcamentoId, int custoId, MaoObraOrcamentoModel maoObraOrcamento)
        {
            try
            {
                var orcamentoId = 0;

                var where = $"MAO_OBRA_ORCAMENTO_ID = {maoObraOrcamentoId} AND CUSTO_ID = {custoId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("MAO_OBRA_ORCAMENTO_ID", "T_ORCA_CUSTOS_MAO_OBRA", where)))
                {
                    throw new Exception();
                }

                where = $"MAO_OBRA_ORCAMENTO_ID = {maoObraOrcamentoId}";
                orcamentoId = Int32.Parse(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_MAO_OBRA_ORCAMENTO", where));

                var custosMaoObraDB = Get(maoObraOrcamentoId, custoId);

                CustosMaoObraRepository.Update(maoObraOrcamentoId, custoId, maoObraOrcamento, maoObraOrcamento.LIST_CUSTO[0]);

                return orcamentoId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Delete(int maoObraOrcamentoId)
        {
            try
            {
                var orcamentoId = 0;
                
                var where = $"MAO_OBRA_ORCAMENTO_ID = {maoObraOrcamentoId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("MAO_OBRA_ORCAMENTO_ID", "T_ORCA_MAO_OBRA_ORCAMENTO", where)))
                {
                    throw new Exception();
                }

                orcamentoId = Int32.Parse(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_OBRA", where));

                CustosMaoObraRepository.DeletePorMaoObraOrcamentoId(maoObraOrcamentoId);

                return orcamentoId;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Delete(int maoObraOrcamentoId, int custoId)
        {
            try
            {
                var orcamentoId = 0;
                
                var where = $"MAO_OBRA_ORCAMENTO_ID = {maoObraOrcamentoId} AND CUSTO_ID = {custoId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("MAO_OBRA_ORCAMENTO_ID", "T_ORCA_CUSTOS_MAO_OBRA", where)))
                {
                    throw new Exception();
                }

                where = $"MAO_OBRA_ORCAMENTO_ID = {maoObraOrcamentoId}";
                orcamentoId = Int32.Parse(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_OBRA", where));

                CustosMaoObraRepository.Delete(maoObraOrcamentoId, custoId);

                return orcamentoId;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
