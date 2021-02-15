using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OrcamentariaBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrcamentoIntumescenteController : ControllerBase
    {
        private OrcamentoIntumescenteService OrcamentoIntumescenteService;
        private TotaisOrcamentoService TotaisOrcamentoService;

        public OrcamentoIntumescenteController(OrcamentoIntumescenteService orcamentoIntumescenteService, TotaisOrcamentoService totaisOrcamentoService)
        {
            this.OrcamentoIntumescenteService = orcamentoIntumescenteService;
            this.TotaisOrcamentoService = totaisOrcamentoService;
        }

        [HttpGet]
        public IEnumerable<OrcamentoIntumescenteModel> Get()
        {
            try
            {
                return OrcamentoIntumescenteService.Get();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{orcamentoId}")]
        public IEnumerable<OrcamentoIntumescenteModel> Get(int orcamentoId)
        {
            try
            {
                return OrcamentoIntumescenteService.Get(orcamentoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public OrcamentoIntumescenteModel Post([FromBody] OrcamentoIntumescenteModel orcamentoIntumescente)
        {
            try
            {
                var orca = OrcamentoIntumescenteService.Post(orcamentoIntumescente);
                TotaisOrcamentoService.CalcularTotaisOrcamento(orca.ORCAMENTO_ID);
                return orca;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{orcamentoId}")]
        public void Put(int orcamentoId, [FromBody] OrcamentoIntumescenteModel orcamentoIntumescente)
        {
            try
            {
                OrcamentoIntumescenteService.Put(orcamentoId, orcamentoIntumescente);
                TotaisOrcamentoService.CalcularTotaisOrcamento(orcamentoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{orcamentoId}")]
        public void Delete(int orcamentoId)
        {
            try
            {
                OrcamentoIntumescenteService.Delete(orcamentoId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
