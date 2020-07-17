using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class EnderecoQO
    {
        public int EnderecoId { get; set; }

        public int PessoaId { get; set; }

        public EnderecoQO(int enderecoId, int pessoaId)
        {
            this.EnderecoId = enderecoId;
            this.PessoaId = pessoaId;
        }

        public EnderecoQO()
        {
        }
    }
}
