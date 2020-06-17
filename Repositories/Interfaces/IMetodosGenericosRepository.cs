
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public interface IMetodosGenericosRepository
    {
        string DlookupOrcamentaria(string campoBuscado, string tabela, string where);

        void StartTransactionCommitRollback(MetodosGenericosEnum metodosGenericos);
    }
}
