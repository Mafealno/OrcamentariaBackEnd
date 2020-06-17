using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrcamentariaBackEnd
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartaCoberturaController : ControllerBase
    {

        private ICartaCoberturaRepository CartaCoberturaRepository;
        private CartaCoberturaService CartaCoberturaService;
        public CartaCoberturaController(ICartaCoberturaRepository cartaCoberturaRepository, CartaCoberturaService cartaCoberturaService)
        {
            this.CartaCoberturaRepository = cartaCoberturaRepository;
            this.CartaCoberturaService = cartaCoberturaService;
        }

        // GET: api/<CartaCoberturaController>
        [HttpGet]
        public IEnumerable<CartaCoberturaModel> Get()
        {
            try
            {
                return CartaCoberturaService.Get();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET api/<CartaCoberturaController>/5
        [HttpGet("buscar")]
        public IEnumerable<CartaCoberturaModel> Get([FromQuery] CartaCoberturaQO cartaCobertura)
        {
            try
            {
                return CartaCoberturaService.GetComParametro(cartaCobertura);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("buscar/{referencia}/{pessoaId}")]
        public IEnumerable<CartaCoberturaModel> Get(string referencia, int pessoaId)
        {
            try
            {
                return CartaCoberturaService.Get(referencia, pessoaId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("buscar/{referencia}/{pessoaId}/{material}")]
        public IEnumerable<CartaCoberturaModel> Get(string referencia, int pessoaId, int materialId)
        {
            try
            {
                return CartaCoberturaService.Get(referencia, pessoaId, materialId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST api/<CartaCoberturaController>
        [HttpPost]
        public CartaCoberturaModel Post([FromBody] CartaCoberturaModel cartaCobertura)
        {
            try
            {
                return CartaCoberturaService.Post(cartaCobertura);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // PUT api/<CartaCoberturaController>/5
        [HttpPut("{cartaCoberturaId}")]
        public void Put(int cartaCoberturaId, [FromBody] CartaCoberturaModel cartaCobertura)
        {
            try
            {
                CartaCoberturaService.Put(cartaCoberturaId, cartaCobertura);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // DELETE api/<CartaCoberturaController>/5
        [HttpDelete("{cartaCoberturaId}")]
        public void Delete(int cartaCoberturaId)
        {
            try
            {
                CartaCoberturaRepository.Delete(cartaCoberturaId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
