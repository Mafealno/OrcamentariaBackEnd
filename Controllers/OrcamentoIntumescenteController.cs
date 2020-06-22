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

        public OrcamentoIntumescenteController(OrcamentoIntumescenteService orcamentoIntumescenteService)
        {
            this.OrcamentoIntumescenteService = orcamentoIntumescenteService;
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
                return OrcamentoIntumescenteService.Post(orcamentoIntumescente);
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
