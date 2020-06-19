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

        public IEnumerable<CustoModel> Get(int maoObraOrcamentoId, int custoId)
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

                List<CustoModel> listCustosMaoObra = new List<CustoModel>();

                listCustosMaoObra.Add(CustosMaoObraRepository.Find(maoObraOrcamentoId, custoId));

                return listCustosMaoObra;
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

                where = $"PESSOA_ID = {maoObraOrcamento.FUNCIONARIO.PESSOA_ID}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_PESSOA", where)))
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

        public void Put(int maoObraOrcamentoId, int custoId, MaoObraOrcamentoModel maoObraOrcamento, CustoModel custo)
        {
            try
            {
                var where = $"MAO_OBRA_ORCAMENTO_ID = {maoObraOrcamentoId} AND CUSTO_ID = {custoId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("MAO_OBRA_ORCAMENTO_ID", "T_ORCA_CUSTOS_MAO_OBRA", where)))
                {
                    throw new Exception();
                }

                var custosMaoObraDB = Get(maoObraOrcamentoId, custoId).ToArray()[0];

                if(custo.CUSTO_ID != custosMaoObraDB.CUSTO_ID)
                {
                    custo.NOME_CUSTO = custosMaoObraDB.NOME_CUSTO;
                    custo.TIPO_CUSTO = custosMaoObraDB.TIPO_CUSTO;
                    custo.VALOR_CUSTO = custosMaoObraDB.VALOR_CUSTO;
                }

                CustosMaoObraRepository.Update(maoObraOrcamentoId, custoId, maoObraOrcamento, custo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int maoObraOrcamentoId)
        {
            try
            {
                var where = $"MAO_OBRA_ORCAMENTO_ID = {maoObraOrcamentoId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("MAO_OBRA_ORCAMENTO_ID", "T_ORCA_MAO_OBRA_ORCAMENTO", where)))
                {
                    throw new Exception();
                }

                CustosMaoObraRepository.DeletePorMaoObraOrcamentoId(maoObraOrcamentoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int maoObraOrcamentoId, int custoId)
        {
            try
            {
                var where = $"MAO_OBRA_ORCAMENTO_ID = {maoObraOrcamentoId} AND CUSTO_ID = {custoId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("MAO_OBRA_ORCAMENTO_ID", "T_ORCA_CUSTOS_MAO_OBRA", where)))
                {
                    throw new Exception();
                }

                CustosMaoObraRepository.Delete(maoObraOrcamentoId, custoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
