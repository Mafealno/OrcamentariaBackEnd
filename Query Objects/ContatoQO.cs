using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class ContatoQO
    {
        public int ContatoId { get; set; }

        public int PessoaId { get; set; }

        public ContatoQO(int contatoId, int pessoaId)
        {
            this.ContatoId = contatoId;
            this.PessoaId = pessoaId;
        }

    }
}
