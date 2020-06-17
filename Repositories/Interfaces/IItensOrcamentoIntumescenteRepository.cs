
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public interface IItensOrcamentoIntumescenteRepository
    {
        ItensOrcamentoIntumescenteModel Create(ItensOrcamentoIntumescenteModel itensOrcamentoIntumescente);

        void Update(int itensOrcamentoId, ItensOrcamentoIntumescenteModel itensOrcamentoIntumescente);

        void Delete(int itensOrcamentoId);

        void DeletePorOrcamentoId(int orcamentoId);

        IEnumerable<ItensOrcamentoIntumescenteModel> List();

        IEnumerable<ItensOrcamentoIntumescenteModel> ListPorOrcamentoId(int orcamentoId);

        ItensOrcamentoIntumescenteModel Find(int itensOrcamentoId);
    }
}
