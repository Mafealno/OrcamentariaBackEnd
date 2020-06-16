using Orcamentaria.Model.Orcamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd.Repositories
{
    public interface ITotaisOrcamentoRepository
    {
        TotaisOrcamentoModel Create(TotaisOrcamentoModel totaisOrcamento);

        void Update(int totaisOrcamentoId, TotaisOrcamentoModel totaisOrcamento);

        void Delete(int totaisOrcamentoId);

        void DeletePorOrcamentoId(int orcamentoId);

        IEnumerable<TotaisOrcamentoModel> List();

        TotaisOrcamentoModel Find(int totaisOrcamentoId);

        TotaisOrcamentoModel FindPorOrcamentoId(int orcamentoId);
    }
}
