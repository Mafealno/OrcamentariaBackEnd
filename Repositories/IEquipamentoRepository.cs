using Orcamentaria.Model.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd.Repositories
{
    public interface IEquipamentoRepository
    {
        EquipamentoModel Create(EquipamentoModel equipamento);

        void Update(int equipamentoId, EquipamentoModel equipamento);

        void Delete(int equipamentoId);

        IEnumerable<EquipamentoModel> List();

        IEnumerable<EquipamentoModel> ListPorNomeEquipamento(string nomeEquipamento);

        EquipamentoModel Find(int equipamentoId);
    }
}
