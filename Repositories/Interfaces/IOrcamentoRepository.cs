using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public interface IOrcamentoRepository
    {
        OrcamentoModel Create(OrcamentoModel orcamento);

        void Update(int orcamentoId, OrcamentoModel orcamento);

        void Delete(int orcamentoId);

        IEnumerable<OrcamentoModel> List();

        OrcamentoModel Find(int orcamentoId);
    }
}
