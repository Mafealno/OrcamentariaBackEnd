using Orcamentaria.Model.Orcamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd.Repositories
{
    public interface ICustoOrcamentoRepository
    {
        CustoOrcamentoModel Create(CustoOrcamentoModel custoOrcamento);

        void Update(int custoOrcamentoId, CustoOrcamentoModel custoOrcamento);

        void Delete(int custoOrcamentoId);

        void DeletePorOrcamentoId(int orcamentoId);

        IEnumerable<CustoOrcamentoModel> List();

        IEnumerable<CustoOrcamentoModel> ListPorOrcamentoId(int orcamentoId);

        CustoOrcamentoModel Find(int custoOrcamentoId);

    }
}
