using Orcamentaria.Model.Cadastro;
using Orcamentaria.Model.Orcamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd.Repositories
{
    public interface IMaoObraOrcamentoRepository
    {
        MaoObraOrcamentoModel Create(MaoObraOrcamentoModel maoObraOrcamento);

        void Update(int maoObraOrcamentoId, MaoObraOrcamentoModel maoObraOrcamento);

        void Delete(int maoObraOrcamentoId, int orcamentoId);

        void DeletePorOrcamentoId(int orcamentoId);

        IEnumerable<MaoObraOrcamentoModel> List();

        IEnumerable<MaoObraOrcamentoModel> ListPorOrcamentoId(int orcamentoId);

        MaoObraOrcamentoModel Find(int maoObraOrcamentoId, int orcamentoId);
    }
}
