using Orcamentaria.Model.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd.Repositories
{
    public interface IMaterialRepository
    {
        MaterialModel Create(MaterialModel material);

        void Update(int materialId, MaterialModel material);

        void Delete(int materialId);

        IEnumerable<MaterialModel> List();

        IEnumerable<MaterialModel> ListPorNomeMaterial(string nomeMaterial);

        IEnumerable<MaterialModel> ListPorNomeFabricante(string nomeFabricante);

        MaterialModel Find(int materialId);
    }
}
