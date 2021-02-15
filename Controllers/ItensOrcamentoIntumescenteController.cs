using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OrcamentariaBackEnd
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItensOrcamentoIntumescenteController : ControllerBase
    {

        private ItensOrcamentoIntumescenteService ItensOrcamentoIntumescenteService;
        private TotaisOrcamentoService TotaisOrcamentoService;

        public ItensOrcamentoIntumescenteController(ItensOrcamentoIntumescenteService itensOrcamentoIntumescenteService, TotaisOrcamentoService totaisOrcamentoService)
        {
            this.ItensOrcamentoIntumescenteService = itensOrcamentoIntumescenteService;
            this.TotaisOrcamentoService = totaisOrcamentoService;
        }

        [HttpGet]
        public IEnumerable<ItensOrcamentoIntumescenteModel> Get()
        {
            try
            {
                return ItensOrcamentoIntumescenteService.Get();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("buscar")]
        public IEnumerable<ItensOrcamentoIntumescenteModel> Get([FromQuery] ItensOrcamentoQO itensOrcamentoIntumescente)
        {
            try
            {
                return ItensOrcamentoIntumescenteService.GetComParametro(itensOrcamentoIntumescente);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ItensOrcamentoIntumescenteModel Post([FromBody] ItensOrcamentoIntumescenteModel itensOrcamentoIntumescente)
        {
            try
            {
                var itens = ItensOrcamentoIntumescenteService.Post(itensOrcamentoIntumescente);
                TotaisOrcamentoService.CalcularTotaisOrcamento(itensOrcamentoIntumescente.ORCAMENTO_ID);
                return itens;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{itensOrcamentoId}")]
        public void Put(int itensOrcamentoId, [FromBody] ItensOrcamentoIntumescenteModel itensOrcamentoIntumescente)
        {
            try
            {
                ItensOrcamentoIntumescenteService.Put(itensOrcamentoId, itensOrcamentoIntumescente);
                TotaisOrcamentoService.CalcularTotaisOrcamento(itensOrcamentoIntumescente.ORCAMENTO_ID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("deletar")]
        public void Delete([FromQuery] ItensOrcamentoQO itensOrcamentoIntumescente)
        {
            try
            {
                TotaisOrcamentoService.CalcularTotaisOrcamento(ItensOrcamentoIntumescenteService.DeleteComParametro(itensOrcamentoIntumescente));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
