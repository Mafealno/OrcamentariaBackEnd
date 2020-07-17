using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class PessoaQO
    {
        public int PessoaId { get; set; }

        public string NomePessoa { get; set; }

        public PessoaQO(int pessoaId, string nomePessoa)
        {
            PessoaId = pessoaId;
            NomePessoa = nomePessoa;
        }

        public PessoaQO()
        {
        }

    }
}
