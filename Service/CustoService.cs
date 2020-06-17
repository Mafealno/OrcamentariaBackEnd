using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class CustoService
    {
        private ICustoRepository CustoRepository;
        private IMetodosGenericosRepository MetodosGenericosRepository;

        public CustoService(ICustoRepository custoRepository, IMetodosGenericosRepository metodosGenericosRepository)
        {
            this.CustoRepository = custoRepository;
            this.MetodosGenericosRepository = metodosGenericosRepository;
        }

        public IEnumerable<CustoModel> Get()
        {
            try
            {
                return CustoRepository.List();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<CustoModel> GetComParametro(CustoQO custo) 
        {
            try
            {
                if (!string.IsNullOrEmpty(custo.NomeCusto))
                {
                    return CustoRepository.ListPorNomeCusto(custo.NomeCusto);
                }
                else
                {
                    List<CustoModel> listCusto = new List<CustoModel>();

                    listCusto.Add(CustoRepository.Find(custo.CustoId));

                    return listCusto;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CustoModel Post(CustoModel custo)
        {
            try
            {
                return CustoRepository.Create(custo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Put(int custoId, CustoModel custo)
        {
            try
            {
                var where = $"CUSTO_ID = {custoId}";
                //VERIFICA SE A PESSOA EXISTE
                if (string.IsNullOrEmpty(MetodosGenericosRepository.DlookupOrcamentaria("CUSTO_ID", "T_ORCA_CUSTO", where)))
                {
                    throw new Exception();
                }

                CustoRepository.Update(custoId, custo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int custoId)
        {
            try
            {
                var where = $"CUSTO_ID = {custoId}";
                //VERIFICA SE A PESSOA EXISTE
                if (string.IsNullOrEmpty(MetodosGenericosRepository.DlookupOrcamentaria("CUSTO_ID", "T_ORCA_CUSTO", where)))
                {
                    throw new Exception();
                }

                CustoRepository.Delete(custoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
