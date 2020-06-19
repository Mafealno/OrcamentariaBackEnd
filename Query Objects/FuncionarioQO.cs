using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class FuncionarioQO
    {
        public int PessoaId { get; set; }

        public string NomePessoa { get; set; }

        public FuncionarioQO(int pessoaId, string nomePessoa)
        {
            PessoaId = pessoaId;
            NomePessoa = nomePessoa;
        }
    }
}
