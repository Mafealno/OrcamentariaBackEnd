using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace OrcamentariaBackEnd
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustoController : ControllerBase
    {

        private CustoService CustoService;

        public CustoController(CustoService custoService)
        {
            CustoService = custoService;
        }

        [HttpGet]
        public IEnumerable<CustoModel> Get()
        {
            try
            {
                return CustoService.Get();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("buscar")]
        public IEnumerable<CustoModel> Get([FromQuery] CustoQO custo)
        {
            return CustoService.GetComParametro(custo);
        }

        [HttpPost]
        public CustoModel Post([FromBody] CustoModel custo)
        {
            try
            {
                return CustoService.Post(custo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{custoId}")]
        public void Put(int custoId, [FromBody] CustoModel custo)
        {
            try
            {
                CustoService.Put(custoId, custo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{custoId}")]
        public void Delete(int custoId)
        {
            try
            {
                CustoService.Delete(custoId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
