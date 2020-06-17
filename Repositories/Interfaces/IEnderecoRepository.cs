
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrcamentariaBackEnd
{
    public interface IEnderecoRepository
    {
        EnderecoModel Create(EnderecoModel endereco);
        
        void Update(int enderecoId, EnderecoModel endereco);
        
        void Delete(int enderecoId);
        
        void DeletePorPessoaId(int pessoaId);
        
        IEnumerable<EnderecoModel> List();
        
        IEnumerable<EnderecoModel> ListPorPessoaId(int pessoaId);
        
        EnderecoModel Find(int enderecoId);

        EnderecoModel FindPorEnderecoPadrao(int pessoaId);
    }
}
