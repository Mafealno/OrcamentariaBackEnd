using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OrcamentariaBackEnd
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustoOrcamentoController : ControllerBase
    {

        private CustoOrcamentoService CustoOrcamentoService;
        private TotaisOrcamentoService TotaisOrcamentoService;

        public CustoOrcamentoController(CustoOrcamentoService custoOrcamentoService, TotaisOrcamentoService totaisOrcamentoService)
        {
            this.CustoOrcamentoService = custoOrcamentoService;
            this.TotaisOrcamentoService = totaisOrcamentoService;
        }

        [HttpGet]
        public IEnumerable<CustoOrcamentoModel> Get()
        {
            try
            {
                return CustoOrcamentoService.Get();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{buscar}")]
        public IEnumerable<CustoOrcamentoModel> Get([FromQuery] CustoOrcamentoQO custoOrcamento)
        {
            try
            {
                return CustoOrcamentoService.GetComParametro(custoOrcamento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public CustoOrcamentoModel Post([FromBody] CustoOrcamentoModel custoOrcamento)
        {
            try
            {
                var custo = CustoOrcamentoService.Post(custoOrcamento);
                TotaisOrcamentoService.CalcularTotaisOrcamento(custoOrcamento.ORCAMENTO_ID);
                return custo;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{custoOrcamentoId}")]
        public void Put(int custoOrcamentoId, [FromBody] CustoOrcamentoModel custoOrcamento)
        {
            try
            {
                CustoOrcamentoService.Put(custoOrcamentoId, custoOrcamento);
                TotaisOrcamentoService.CalcularTotaisOrcamento(custoOrcamento.ORCAMENTO_ID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("deletar")]
        public void Delete([FromQuery] CustoOrcamentoQO custoOrcamento)
        {
            try
            {
                TotaisOrcamentoService.CalcularTotaisOrcamento(CustoOrcamentoService.DeleteComParametro(custoOrcamento));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
