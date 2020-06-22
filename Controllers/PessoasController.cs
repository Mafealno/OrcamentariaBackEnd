using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace OrcamentariaBackEnd
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {

        private PessoaService PessoaService;

        public PessoasController(PessoaService pessoaService)
        {
            this.PessoaService = pessoaService;
        }

        [HttpGet]
        public IEnumerable<PessoaModel> Get()
        {
            try
            {
                return PessoaService.Get();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("buscar")]
        public IEnumerable<PessoaModel> Get([FromQuery] PessoaQO pessoa)
        {
            try
            {
                return PessoaService.GetComParametro(pessoa);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public PessoaModel Post([FromBody] PessoaModel pessoa)
        {
            try
            {
                return PessoaService.Post(pessoa);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{pessoaId}")]
        public void Put(int pessoaId, [FromBody] PessoaModel pessoa)
        {
            try
            {
                PessoaService.Put(pessoaId, pessoa);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{pessoaId}")]
        public void Delete(int pessoaId)
        {
            try
            {
                PessoaService.Delete(pessoaId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
