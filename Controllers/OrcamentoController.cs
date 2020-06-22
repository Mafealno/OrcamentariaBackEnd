using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace OrcamentariaBackEnd
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrcamentoController : ControllerBase
    {

        private OrcamentoService OrcamentoService;

        public OrcamentoController(OrcamentoService orcamentoService)
        {
            this.OrcamentoService = orcamentoService;
        }

        [HttpGet]
        public IEnumerable<OrcamentoModel> Get()
        {
            try
            {
                return OrcamentoService.Get();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{orcamentoId}")]
        public IEnumerable<OrcamentoModel> Get(int orcamentoId)
        {
            try
            {
                return OrcamentoService.Get(orcamentoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public OrcamentoModel Post([FromBody] OrcamentoModel orcamento)
        {
            try
            {
                return OrcamentoService.Post(orcamento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{orcamentoId}")]
        public void Put(int orcamentoId, [FromBody] OrcamentoModel orcamento)
        {
            try
            {
                OrcamentoService.Put(orcamentoId, orcamento);
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
                OrcamentoService.Delete(orcamentoId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
