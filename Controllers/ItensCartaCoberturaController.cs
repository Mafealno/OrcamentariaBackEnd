using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OrcamentariaBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItensCartaCoberturaController : ControllerBase
    {

        private ItensCartaCoberturaService ItensCartaCoberturaService;

        public ItensCartaCoberturaController(ItensCartaCoberturaService itensCartaCoberturaService)
        {
            this.ItensCartaCoberturaService = itensCartaCoberturaService;
        }

        [HttpGet]
        public IEnumerable<ItensCartaCoberturaModel> Get()
        {
            try
            {
                return ItensCartaCoberturaService.Get();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("buscar")]
        public IEnumerable<ItensCartaCoberturaModel> Get([FromQuery] ItensCartaCoberturaQO itensCartaCobertura)
        {
            try
            {
                return ItensCartaCoberturaService.GetComParametro(itensCartaCobertura);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ItensCartaCoberturaModel Post([FromBody] ItensCartaCoberturaModel itensCartaCobertura)
        {
            try
            {
                return ItensCartaCoberturaService.Post(itensCartaCobertura);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{itensCartaCoberturaId}")]
        public void Put(int itensCartaCoberturaId, [FromBody] ItensCartaCoberturaModel itensCartaCobertura)
        {
            try
            {
                ItensCartaCoberturaService.Put(itensCartaCoberturaId, itensCartaCobertura);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("deletar")]
        public void Delete([FromQuery] ItensCartaCoberturaQO itensCartaCobertura)
        {
            try
            {
                ItensCartaCoberturaService.DeleteComParametro(itensCartaCobertura);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{cartaCoberturaId}/{tempoResistenciaFogo}")]
        public void Delete(int cartaCoberturaId, string tempoResistenciaFogo)
        {
            try
            {
                ItensCartaCoberturaService.Delete(cartaCoberturaId, tempoResistenciaFogo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
