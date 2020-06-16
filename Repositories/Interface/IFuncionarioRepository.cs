using Orcamentaria.Model.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd.Repositories
{
    public interface IFuncionarioRepository
    {
        FuncionarioModel Create(FuncionarioModel funcionario);

        void Update(int pessoaID, FuncionarioModel funcionario);

        void Delete(int pessoaId);

        IEnumerable<FuncionarioModel> List();

        IEnumerable<FuncionarioModel> ListPorNomePessoa(string nomePessoa);

        FuncionarioModel Find(int pessoaId);
    }
}
