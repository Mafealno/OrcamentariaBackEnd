using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OrcamentariaBackEnd
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustosMaoObraController : ControllerBase
    {

        private CustosMaoObraService CustosMaoObraService;

        public CustosMaoObraController(CustosMaoObraService custosMaoObraService)
        {
            this.CustosMaoObraService = custosMaoObraService;
        }


        [HttpGet("buscar/{maoObraOrcamentoId}")]
        public IEnumerable<CustoModel> Get(int maoObraOrcamentoId)
        {
            try
            {
                return CustosMaoObraService.Get(maoObraOrcamentoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("buscar/{maoObraOrcamentoId}/{custoId}")]
        public IEnumerable<CustoModel> Get(int maoObraOrcamentoId, int custoId)
        {
            try
            {
                return CustosMaoObraService.Get(maoObraOrcamentoId, custoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public void Post([FromBody] MaoObraOrcamentoModel maoObraOrcamento, [FromBody] CustoModel custo)
        {
            try
            {
                CustosMaoObraService.Post(maoObraOrcamento, custo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // PUT api/<CustosMaoObraController>/5
        [HttpPut("atualizar/{maoObraOrcamentoId}/{custoId}")]
        public void Put(int maoObraOrcamentoId, int custoId, [FromBody] MaoObraOrcamentoModel maoObraOrcamento, [FromBody] CustoModel custo)
        {
            try
            {
                CustosMaoObraService.Put(maoObraOrcamentoId, custoId, maoObraOrcamento, custo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // DELETE api/<CustosMaoObraController>/5
        [HttpDelete("deletar/{maoObraOrcamentoId}")]
        public void Delete(int maoObraOrcamentoId)
        {
            try
            {
                CustosMaoObraService.Delete(maoObraOrcamentoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // DELETE api/<CustosMaoObraController>/5
        [HttpDelete("deletar/{maoObraOrcamentoId}/{custoId}")]
        public void Delete(int maoObraOrcamentoId, int custoId)
        {
            try
            {
                CustosMaoObraService.Delete(maoObraOrcamentoId, custoId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
