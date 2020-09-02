using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OrcamentariaBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase
    {

        private ContatoService ContatoService;

        public ContatoController(ContatoService contatoService)
        {
            this.ContatoService = contatoService;
        }

        [HttpGet]
        public IEnumerable<ContatoModel> Get()
        {
            try
            {
                return ContatoService.Get();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("buscar/{pessoaId}/{tipoContato}")]
        public IEnumerable<ContatoModel> Get(int pessoaId, string tipoContato)
        {
            try
            {
                return ContatoService.Get(pessoaId, tipoContato);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("buscar")]
        public IEnumerable<ContatoModel> Get([FromQuery] ContatoQO contato)
        {
            try
            {
                return ContatoService.GetComParametro(contato);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ContatoModel Post([FromBody] ContatoModel contato)
        {
            try
            {
                return ContatoService.Post(contato);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{contatoId}")]
        public void Put(int contatoId, [FromBody] ContatoModel contato)
        {
            try
            {
                ContatoService.Put(contatoId, contato);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("deletar")]
        public void Delete([FromQuery] ContatoQO contato)
        {
            try
            {
                ContatoService.DeleteComParametro(contato);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
