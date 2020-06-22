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

        public ItensOrcamentoGeralController(ItensOrcamentoGeralService itensOrcamentoGeralService)
        {
            this.ItensOrcamentoGeralService = itensOrcamentoGeralService;
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
                return ItensOrcamentoGeralService.Post(itensOrcamentoGeral);
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
                ItensOrcamentoGeralService.DeleteComParametro(itensOrcamentoGeral);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
