using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public interface IOrcamentoIntumescenteRepository
    {
        OrcamentoIntumescenteModel Create(OrcamentoIntumescenteModel orcamento);

        void Update(int orcamentoId, OrcamentoIntumescenteModel orcamentoIntumescente);

        void Delete(int orcamentoId);

        IEnumerable<OrcamentoIntumescenteModel> List();

        OrcamentoIntumescenteModel Find(int orcamentoId);
    }
}
