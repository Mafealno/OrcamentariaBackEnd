using Orcamentaria.Model.Orcamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd.Repositories
{
    public interface IItensOrcamentoRepository
    {
        ItensOrcamentoModel Create(ItensOrcamentoModel itensOrcamento);

        void Update(int itensOrcamentoId, ItensOrcamentoModel itensOrcamento);

        void Delete(int itensOrcamentoId);

        void DeletePorOrcamentoId(int orcamentoId);

        IEnumerable<ItensOrcamentoModel> List();

        IEnumerable<ItensOrcamentoModel> ListPorOrcamentoId(int orcamentoId);

        ItensOrcamentoModel Find(int itensOrcamentoId);
    }
}
