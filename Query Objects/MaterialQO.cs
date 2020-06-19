using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class MaterialQO
    {
        public int MaterialId { get; set; }

        public string NomeMaterial { get; set; }

        public string NomeFabricante { get; set; }

        public MaterialQO(int materialId, string nomeMaterial, string nomeFabricante)
        {
            MaterialId = materialId;
            NomeMaterial = nomeMaterial;
            NomeFabricante = nomeFabricante;
        }
    }
}
