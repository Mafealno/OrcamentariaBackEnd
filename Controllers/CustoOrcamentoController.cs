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

        public CustoOrcamentoController(CustoOrcamentoService custoOrcamentoService)
        {
            this.CustoOrcamentoService = custoOrcamentoService;
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
                return CustoOrcamentoService.Post(custoOrcamento);
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
                CustoOrcamentoService.DeleteComParametro(custoOrcamento);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
