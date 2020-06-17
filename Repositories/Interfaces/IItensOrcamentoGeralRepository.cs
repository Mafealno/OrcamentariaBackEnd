
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public interface IItensOrcamentoGeralRepository
    {
        ItensOrcamentoGeralModel Create(ItensOrcamentoGeralModel itensOrcamentoGeral);

        void Update(int itensOrcamentoId, ItensOrcamentoGeralModel itensOrcamentoGeral);

        void Delete(int itensOrcamentoId);

        void DeletePorOrcamentoId(int orcamentoId);

        IEnumerable<ItensOrcamentoGeralModel> List();

        IEnumerable<ItensOrcamentoGeralModel> ListPorOrcamentoId(int orcamentoId);

        ItensOrcamentoGeralModel Find(int itensOrcamentoId);
    }
}
