using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public interface IMaterialOrcamentoRepository
    {
        MaterialOrcamentoModel Create(MaterialOrcamentoModel materialOrcamento);

        void Update(int materialOrcamentoId, MaterialOrcamentoModel materialOrcamento);

        void Delete(int materialOrcamentoId);

        void DeletePorOrcamentoId(int orcamentoId);

        IEnumerable<MaterialOrcamentoModel> List();

        IEnumerable<MaterialOrcamentoModel> ListPorOrcamentoId(int orcamentoId);

        MaterialOrcamentoModel Find(int materialOrcamentoId);
    }
}
