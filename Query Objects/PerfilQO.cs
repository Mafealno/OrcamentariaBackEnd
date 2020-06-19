using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class PerfilQO
    {
        public int PerfilId { get; set; }

        public string NomePerfil { get; set; }

        public PerfilQO(int perfilId, string nomePerfil)
        {
            PerfilId = perfilId;
            NomePerfil = nomePerfil;
        }
    }
}
