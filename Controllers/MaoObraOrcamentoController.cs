using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace OrcamentariaBackEnd
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaoObraOrcamentoController : ControllerBase
    {

        private MaoObraOrcamentoService MaoObraOrcamentoService;
        private TotaisOrcamentoService TotaisOrcamentoService;

        public MaoObraOrcamentoController(MaoObraOrcamentoService maoObraOrcamentoService, TotaisOrcamentoService totaisOrcamentoService)
        {
            this.MaoObraOrcamentoService = maoObraOrcamentoService;
            this.TotaisOrcamentoService = totaisOrcamentoService;
    }

        [HttpGet]
        public IEnumerable<MaoObraOrcamentoModel> Get()
        {
            try
            {
                return MaoObraOrcamentoService.Get();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("buscar/{orcamentoId}")]
        public IEnumerable<MaoObraOrcamentoModel> Get(int orcamentoId)
        {
            try
            {
                return MaoObraOrcamentoService.Get(orcamentoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("buscar/{maoObraOrcamentoId}/{orcamentoId}")]
        public IEnumerable<MaoObraOrcamentoModel> Get(int maoObraOrcamentoId, int orcamentoId)
        {
            try
            {
                return MaoObraOrcamentoService.Get(maoObraOrcamentoId, orcamentoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public MaoObraOrcamentoModel Post([FromBody] MaoObraOrcamentoModel maoObraOrcamento)
        {
            try
            {
                var maoObra = MaoObraOrcamentoService.Post(maoObraOrcamento);
                TotaisOrcamentoService.CalcularTotaisOrcamento(maoObraOrcamento.ORCAMENTO_ID);
                return maoObra;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{maoObraOrcamentoId}")]
        public void Put(int maoObraOrcamentoId, [FromBody] MaoObraOrcamentoModel maoObraOrcamento)
        {
            try
            {
                MaoObraOrcamentoService.Put(maoObraOrcamentoId, maoObraOrcamento);
                TotaisOrcamentoService.CalcularTotaisOrcamento(maoObraOrcamento.ORCAMENTO_ID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("deletar/{orcamentoId}")]
        public void Delete(int orcamentoId)
        {
            try
            {
                MaoObraOrcamentoService.Delete(orcamentoId);
                TotaisOrcamentoService.CalcularTotaisOrcamento(orcamentoId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("deletar/{maoObraOrcamentoId}/{orcamentoId}")]
        public void Delete(int maoObraOrcamentoId, int orcamentoId)
        {
            try
            {
                MaoObraOrcamentoService.Delete(maoObraOrcamentoId, orcamentoId);
                TotaisOrcamentoService.CalcularTotaisOrcamento(orcamentoId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
