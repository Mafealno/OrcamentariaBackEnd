﻿using System;
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
        private TotaisOrcamentoService TotaisOrcamentoService;

        public CustosMaoObraController(CustosMaoObraService custosMaoObraService, TotaisOrcamentoService totaisOrcamentoService)
        {
            this.CustosMaoObraService = custosMaoObraService;
            this.TotaisOrcamentoService = totaisOrcamentoService;
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
        public CustoModel Get(int maoObraOrcamentoId, int custoId)
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
        public void Post([FromBody] MaoObraOrcamentoModel maoObraOrcamento)
        {
            try
            {
                CustosMaoObraService.Post(maoObraOrcamento, maoObraOrcamento.LIST_CUSTO[0]);
                TotaisOrcamentoService.CalcularTotaisOrcamento(maoObraOrcamento.ORCAMENTO_ID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("atualizar/{maoObraOrcamentoId}/{custoId}")]
        public void Put(int maoObraOrcamentoId, int custoId, [FromBody] MaoObraOrcamentoModel maoObraOrcamento)
        {
            try
            {
                TotaisOrcamentoService.CalcularTotaisOrcamento(CustosMaoObraService.Put(maoObraOrcamentoId, custoId, maoObraOrcamento));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("deletar/{maoObraOrcamentoId}")]
        public void Delete(int maoObraOrcamentoId)
        {
            try
            {
                TotaisOrcamentoService.CalcularTotaisOrcamento(CustosMaoObraService.Delete(maoObraOrcamentoId));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("deletar/{maoObraOrcamentoId}/{custoId}")]
        public void Delete(int maoObraOrcamentoId, int custoId)
        {
            try
            {
                TotaisOrcamentoService.CalcularTotaisOrcamento(CustosMaoObraService.Delete(maoObraOrcamentoId, custoId));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
