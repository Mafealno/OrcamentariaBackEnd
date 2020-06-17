using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class MetodosGenericosService
    {
        private IMetodosGenericosRepository MetodosGenericosRepository;

        public MetodosGenericosService(IMetodosGenericosRepository metodosGenericosRepository)
        {
            this.MetodosGenericosRepository = metodosGenericosRepository;
        }

        public string DlookupOrcamentaria(string campoBuscado, string tabela, string where)
        {
            try
            {
                return MetodosGenericosRepository.DlookupOrcamentaria(campoBuscado, tabela, where);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void StartTransactionCommitRollback(MetodosGenericosEnum metodosGenericos)
        {
            try
            {
                MetodosGenericosRepository.StartTransactionCommitRollback(metodosGenericos);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
