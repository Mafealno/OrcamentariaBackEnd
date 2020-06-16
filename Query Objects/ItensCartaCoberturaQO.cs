using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd.Query_Objects
{
    public class ItensCartaCoberturaQO
    {
        public int ItensCartaCoberturaId { get; set; }

        public int CartaCoberturaId { get; set; }

        public string TempoResistenciaFogo { get; set; }
    }
}
