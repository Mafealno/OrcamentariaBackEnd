
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public interface ICartaCoberturaRepository
    {
        CartaCoberturaModel Create(CartaCoberturaModel cartaCobertura);

        void Update(int cartaCoberturaId, CartaCoberturaModel cartaCobertura);

        void Delete(int cartaCoberturaId);

        IEnumerable<CartaCoberturaModel> List();

        IEnumerable<CartaCoberturaModel> ListPorPessoaId(int pessoaId);

        IEnumerable<CartaCoberturaModel> ListPorMaterialId(int materialId);

        IEnumerable<CartaCoberturaModel> ListPorReferencia(string referencia);

        IEnumerable<CartaCoberturaModel> ListPorReferenciaEPessoaId(string referencia, int pessoaId);

        IEnumerable<CartaCoberturaModel> ListPorMaterialIdEPessoaId(int materialId, int pessoaId);

        IEnumerable<CartaCoberturaModel> ListPorReferenciaEPessoaIdEMaterialId(string referencia, int pessoaId, int materialId);

        CartaCoberturaModel Find(int cartaCoberturaId);
    }
}
