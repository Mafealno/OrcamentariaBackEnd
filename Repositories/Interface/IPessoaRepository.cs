using Orcamentaria.Model.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd.Repositories
{
    public interface IPessoaRepository
    {
        PessoaModel Create(PessoaModel pessoa);

        void Update(int pessoaID, PessoaModel pessoa);

        void Delete(int pessoaId);

        IEnumerable<PessoaModel> List();
        
        IEnumerable<PessoaModel> ListPorNomePessoa(string nomePessoa);

        PessoaModel Find(int pessoaId);

    }
}
