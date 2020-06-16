using Orcamentaria.Model.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd.Repositories
{
    public interface IPerfilRepository
    {
        PerfilModel Create(PerfilModel perfil);

        void Update(int perfilId, PerfilModel perfil);

        void Delete(int perfilId);

        IEnumerable<PerfilModel> List();
        
        IEnumerable<PerfilModel> FindPorNomePerfil(string nomePerfil);

        PerfilModel Find(int perfilId);

    }
}
