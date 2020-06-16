using Orcamentaria.Model.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd.Repositories
{
    public interface ICustoRepository
    {
        CustoModel Create(CustoModel custo);

        void Update(int custoId, CustoModel custo);

        void Delete(int custoId);

        IEnumerable<CustoModel> List();

        IEnumerable<CustoModel> ListPorNomeCusto(string nomeCusto);

        CustoModel Find(int custoId);

    }
}
