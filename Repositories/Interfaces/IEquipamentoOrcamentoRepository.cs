
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public interface IEquipamentoOrcamentoRepository
    {
        EquipamentoOrcamentoModel Create(EquipamentoOrcamentoModel equipamentoOrcamento);

        void Update(int equipamentoOrcamentoId, EquipamentoOrcamentoModel equipamentoOrcamento);

        void Delete(int equipamentoOrcamentoId);

        void DeletePorOrcamentoId(int orcamentoId);

        IEnumerable<EquipamentoOrcamentoModel> List();

        IEnumerable<EquipamentoOrcamentoModel> ListPorOrcamentoId(int orcamentoId);

        EquipamentoOrcamentoModel Find(int equipamentoOrcamentoId);
    }
}
