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
        public TotaisOrcamentoModel Get([FromQuery] TotaisOrcamentoQO totaisOrcamento)
        {
            try
            {
                return TotaisOrcamentoService.GetComParametro(totaisOrcamento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{orcamentoId}")]
        public void CalcularTotaisOrcamento(int orcamentoId)
        {
            try
            {
                //Totais
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
