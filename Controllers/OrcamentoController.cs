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
        private TotaisOrcamentoService TotaisOrcamentoService;

        public OrcamentoController(OrcamentoService orcamentoService, TotaisOrcamentoService totaisOrcamentoService)
        {
            this.OrcamentoService = orcamentoService;
            this.TotaisOrcamentoService = totaisOrcamentoService;
        }

        [HttpGet]
        public IEnumerable<OrcamentoGeralModel> Get()
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
        public IEnumerable<OrcamentoGeralModel> Get(int orcamentoId)
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
        public OrcamentoModel Post([FromBody] OrcamentoGeralModel orcamento)
        {
            try
            {
                var orca = OrcamentoService.Post(orcamento);
                TotaisOrcamentoService.CalcularTotaisOrcamento(orca.ORCAMENTO_ID);
                return orca;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{orcamentoId}")]
        public void Put(int orcamentoId, [FromBody] OrcamentoGeralModel orcamento)
        {
            try
            {
                OrcamentoService.Put(orcamentoId, orcamento);
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
                OrcamentoService.Delete(orcamentoId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
