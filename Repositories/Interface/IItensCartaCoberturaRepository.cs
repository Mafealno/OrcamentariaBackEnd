using Orcamentaria.Model.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd.Repositories
{
    public interface IItensCartaCoberturaRepository
    {
        ItensCartaCoberturaModel Create(ItensCartaCoberturaModel itensCartaCobertura);

        void Update(int itensCartaCoberturaId, ItensCartaCoberturaModel itensCartaCobertura);

        void Delete(int itensCartaCoberturaId);

        void DeletePorCartaCoberturaId(int cartaCoberturaId);

        IEnumerable<ItensCartaCoberturaModel> List();

        IEnumerable<ItensCartaCoberturaModel> ListPorCartaCoberturaId(int cartaCoberturaId);

        IEnumerable<ItensCartaCoberturaModel> ListPorTempoResistenciaFogo(string tempoResistenciaFogo);

        ItensCartaCoberturaModel Find(int itensCartaCoberturaId);
       
        ItensCartaCoberturaModel FindPorValorHpaTempoResistenciaFogo(string valorHpa, string tempoResistenciaFogo);

    }
}
