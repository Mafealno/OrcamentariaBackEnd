using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class CartaCoberturaQO
    {
        public int CartaCoberturaId { get; set; }

        public int PessoaId { get; set; }

        public int MaterialId { get; set; }

        public string Referencia { get; set; }

        public CartaCoberturaQO(int cartaCoberturaId, int pessoaId, int materialId, string referencia)
        {
            CartaCoberturaId = cartaCoberturaId;
            PessoaId = pessoaId;
            MaterialId = materialId;
            Referencia = referencia;
        }

        public CartaCoberturaQO()
        {
        }
    }
}
