using Orcamentaria.Model.Cadastro;
using Orcamentaria.Model.Orcamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd.Repositories
{
    public interface ICustosMaoObraRepository
    {
        void Create(MaoObraOrcamentoModel maoObraOrcamento, CustoModel custo);

        void Update(int maoObraOrcamentoId, int custoId, MaoObraOrcamentoModel maoObraOrcamento, CustoModel custo);

        void Delete(int maoObraOrcamentoId, int custoId);

        void DeletePorMaoObraOrcamentoId(int maoObraOrcamentoId);

        IEnumerable<CustoModel> List(int maoObraOrcamentoId);

        CustoModel Find(int maoObraOrcamentoId, int custoId);
    }
}
