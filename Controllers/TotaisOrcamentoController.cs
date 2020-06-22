using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OrcamentariaBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TotaisOrcamentoController : ControllerBase
    {

        private TotaisOrcamentoService TotaisOrcamentoService;

        public TotaisOrcamentoController(TotaisOrcamentoService totaisOrcamentoService)
        {
            this.TotaisOrcamentoService = totaisOrcamentoService;
        }

        [HttpGet]
        public IEnumerable<TotaisOrcamentoModel> Get()
        {
            try
            {
                return TotaisOrcamentoService.Get();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("buscar")]
        public IEnumerable<TotaisOrcamentoModel> Get([FromQuery] TotaisOrcamentoQO totaisOrcamento)
        {
            try
            {
                return TotaisOrcamentoService.Get();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public TotaisOrcamentoModel Post([FromBody] TotaisOrcamentoModel totaisOrcamento)
        {
            try
            {
                return TotaisOrcamentoService.Post(totaisOrcamento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{totaisOrcamentoId}")]
        public void Put(int totaisOrcamentoId, [FromBody] TotaisOrcamentoModel totaisOrcamento)
        {
            try
            {
                TotaisOrcamentoService.Put(totaisOrcamentoId, totaisOrcamento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // DELETE api/<TotaisOrcamentoController>/5
        [HttpDelete("deletar")]
        public void Delete([FromQuery] TotaisOrcamentoQO totaisOrcamento)
        {
            try
            {
                TotaisOrcamentoService.DeleteComParametro(totaisOrcamento);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
