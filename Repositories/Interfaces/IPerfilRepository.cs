
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public interface IPerfilRepository
    {
        PerfilModel Create(PerfilModel perfil);

        void Update(int perfilId, PerfilModel perfil);

        void Delete(int perfilId);

        IEnumerable<PerfilModel> List();
        
        IEnumerable<PerfilModel> ListPorNomePerfil(string nomePerfil);

        PerfilModel Find(int perfilId);

    }
}
