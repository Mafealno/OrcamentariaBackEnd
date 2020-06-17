
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public interface IContatoRepository
    {
        ContatoModel Create(ContatoModel contato);
        
        void Update(int contatoId, ContatoModel contato);
        
        void Delete(int contatoId);
        
        void DeletePorPessoaId(int pessoaId);
        
        IEnumerable<ContatoModel> List();
        
        IEnumerable<ContatoModel> ListPorPessoaId(int pessoaId);
        
        ContatoModel Find(int contatoId);

        ContatoModel FindPorContatoPadraoETipoContato(int pessoaId, string tipoContato);
    }
}
