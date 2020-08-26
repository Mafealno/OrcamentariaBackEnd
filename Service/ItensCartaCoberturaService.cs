using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class ItensCartaCoberturaService
    {
        private IItensCartaCoberturaRepository ItensCartaCoberturaRepository;
        private MetodosGenericosService MetodosGenericosService;

        public ItensCartaCoberturaService(IItensCartaCoberturaRepository itensCartaCoberturaRepository, MetodosGenericosService metodosGenericosService)
        {
            this.ItensCartaCoberturaRepository = itensCartaCoberturaRepository;
            this.MetodosGenericosService = metodosGenericosService;
        }

        public IEnumerable<ItensCartaCoberturaModel> Get()
        {
            try
            {
                return ItensCartaCoberturaRepository.List();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ItensCartaCoberturaModel> GetComParametro( ItensCartaCoberturaQO itensCartaCobertura)
        {
            try
            {
                if (!string.IsNullOrEmpty(itensCartaCobertura.TempoResistenciaFogo))
                {
                    return ItensCartaCoberturaRepository.ListPorTempoResistenciaFogo(itensCartaCobertura.TempoResistenciaFogo);
                }
                else if(itensCartaCobertura.CartaCoberturaId != 0)
                {
                    return ItensCartaCoberturaRepository.ListPorCartaCoberturaId(itensCartaCobertura.CartaCoberturaId);
                }
                else
                {
                    List<ItensCartaCoberturaModel> listItensCartaCobertura = new List<ItensCartaCoberturaModel>();

                    listItensCartaCobertura.Add(ItensCartaCoberturaRepository.Find(itensCartaCobertura.ItensCartaCoberturaId));

                    return listItensCartaCobertura;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ItensCartaCoberturaModel Post(ItensCartaCoberturaModel itensCartaCobertura)
        {
            try
            {
                var where = $"CARTA_COBERTURA_ID = {itensCartaCobertura.CARTA_COBERTURA_ID}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("CARTA_COBERTURA_ID", "T_ORCA_CARTA_COBERTURA", where)))
                {
                    throw new Exception();
                }

                return ItensCartaCoberturaRepository.Create(itensCartaCobertura);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Put(int itensCartaCoberturaId, ItensCartaCoberturaModel itensCartaCobertura)
        {
            try
            {
                var where = $"ITENS_CARTA_COBERTURA_ID = {itensCartaCobertura.ITENS_CARTA_COBERTURA_ID}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ITENS_CARTA_COBERTURA_ID", "T_ORCA_ITENS_CARTA_COBERTURA", where)))
                {
                    throw new Exception();
                }

                ItensCartaCoberturaRepository.Update(itensCartaCoberturaId, itensCartaCobertura);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int cartaCoberturaId, string tempoResistenciaFogo)
        {
            try
            {
                    var where = $"CARTA_COBERTURA_ID = {cartaCoberturaId}";
                    if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("CARTA_COBERTURA_ID", "T_ORCA_CARTA_COBERTURA", where)))
                    {
                        throw new Exception();
                    }

                    ItensCartaCoberturaRepository.DeletePorCartaCoberturaIdETempoResistenciaFogo(cartaCoberturaId, tempoResistenciaFogo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteComParametro(ItensCartaCoberturaQO itensCartaCobertura)
        {
            try
            {
                if (itensCartaCobertura.CartaCoberturaId != 0)
                {
                    var where = $"CARTA_COBERTURA_ID = {itensCartaCobertura.CartaCoberturaId}";
                    if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("CARTA_COBERTURA_ID", "T_ORCA_CARTA_COBERTURA", where)))
                    {
                        throw new Exception();
                    }

                    ItensCartaCoberturaRepository.DeletePorCartaCoberturaId(itensCartaCobertura.CartaCoberturaId);
                }
                else
                {
                    var where = $"ITENS_CARTA_COBERTURA_ID = {itensCartaCobertura.ItensCartaCoberturaId}";
                    if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ITENS_CARTA_COBERTURA_ID", "T_ORCA_ITENS_CARTA_COBERTURA", where)))
                    {
                        throw new Exception();
                    }

                    ItensCartaCoberturaRepository.Delete(itensCartaCobertura.ItensCartaCoberturaId);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
