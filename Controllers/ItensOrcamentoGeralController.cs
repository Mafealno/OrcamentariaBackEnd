using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OrcamentariaBackEnd
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItensOrcamentoGeralController : ControllerBase
    {

        private ItensOrcamentoGeralService ItensOrcamentoGeralService;
        private TotaisOrcamentoService TotaisOrcamentoService;

        public ItensOrcamentoGeralController(ItensOrcamentoGeralService itensOrcamentoGeralService, TotaisOrcamentoService totaisOrcamentoService)
        {
            this.ItensOrcamentoGeralService = itensOrcamentoGeralService;
            this.TotaisOrcamentoService = totaisOrcamentoService;
        }

        [HttpGet]
        public IEnumerable<ItensOrcamentoGeralModel> Get()
        {
            try
            {
                return ItensOrcamentoGeralService.Get();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("buscar")]
        public IEnumerable<ItensOrcamentoGeralModel> Get([FromQuery] ItensOrcamentoQO itensOrcamentoGeral)
        {
            try
            {
                return ItensOrcamentoGeralService.GetComParametro(itensOrcamentoGeral);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ItensOrcamentoGeralModel Post([FromBody] ItensOrcamentoGeralModel itensOrcamentoGeral)
        {
            try
            {
                var itens = ItensOrcamentoGeralService.Post(itensOrcamentoGeral);
                TotaisOrcamentoService.CalcularTotaisOrcamento(itensOrcamentoGeral.ORCAMENTO_ID);
                return itens;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{itensOrcamentoId}")]
        public void Put(int itensOrcamentoId, [FromBody] ItensOrcamentoGeralModel itensOrcamentoGeral)
        {
            try
            {
                ItensOrcamentoGeralService.Put(itensOrcamentoId, itensOrcamentoGeral);
                TotaisOrcamentoService.CalcularTotaisOrcamento(itensOrcamentoGeral.ORCAMENTO_ID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("deletar")]
        public void Delete([FromQuery] ItensOrcamentoQO itensOrcamentoGeral)
        {
            try
            {
                TotaisOrcamentoService.CalcularTotaisOrcamento(ItensOrcamentoGeralService.DeleteComParametro(itensOrcamentoGeral));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
