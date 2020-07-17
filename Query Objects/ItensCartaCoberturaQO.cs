using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class ItensCartaCoberturaQO
    {
        public int ItensCartaCoberturaId { get; set; }

        public int CartaCoberturaId { get; set; }

        public string TempoResistenciaFogo { get; set; }

        public ItensCartaCoberturaQO(int itensCartaCoberturaId, int cartaCoberturaId, string tempoResistenciaFogo)
        {
            ItensCartaCoberturaId = itensCartaCoberturaId;
            CartaCoberturaId = cartaCoberturaId;
            TempoResistenciaFogo = tempoResistenciaFogo;
        }

        public ItensCartaCoberturaQO()
        {
        }
    }
}
