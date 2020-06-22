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

        public ItensOrcamentoIntumescenteController(ItensOrcamentoIntumescenteService itensOrcamentoIntumescenteService)
        {
            this.ItensOrcamentoIntumescenteService = itensOrcamentoIntumescenteService;
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
                return ItensOrcamentoIntumescenteService.Post(itensOrcamentoIntumescente);
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
                ItensOrcamentoIntumescenteService.DeleteComParametro(itensOrcamentoIntumescente);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
