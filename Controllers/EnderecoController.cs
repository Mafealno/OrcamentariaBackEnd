using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace OrcamentariaBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private EnderecoService EnderecoService;

        public EnderecoController(EnderecoService enderecoService)
        {
            this.EnderecoService = enderecoService;
        }

        [HttpGet]
        public IEnumerable<EnderecoModel> Get()
        {
            try
            {
                return EnderecoService.Get();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("buscar")]
        public IEnumerable<EnderecoModel> Get([FromQuery] EnderecoQO endereco)
        {
            try
            {
                return EnderecoService.GetComParametro(endereco);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public EnderecoModel Post([FromBody] EnderecoModel endereco)
        {
            try
            {
                return EnderecoService.Post(endereco);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{enderecoId}")]
        public void Put(int enderecoId, [FromBody] EnderecoModel endereco)
        {
            try
            {
                EnderecoService.Put(enderecoId, endereco);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("deletar")]
        public void Delete([FromQuery] EnderecoQO endereco)
        {
            try
            {
                EnderecoService.DeleteComParametro(endereco);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
