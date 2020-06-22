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

        public MaoObraOrcamentoController(MaoObraOrcamentoService maoObraOrcamentoService)
        {
            this.MaoObraOrcamentoService = maoObraOrcamentoService;
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
                return MaoObraOrcamentoService.Post(maoObraOrcamento);
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
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
