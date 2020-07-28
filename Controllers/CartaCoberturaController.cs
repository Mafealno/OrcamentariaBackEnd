using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace OrcamentariaBackEnd
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartaCoberturaController : ControllerBase
    {
        
        private CartaCoberturaService CartaCoberturaService;
        public CartaCoberturaController(CartaCoberturaService cartaCoberturaService)
        {
            this.CartaCoberturaService = cartaCoberturaService;
        }

        public CartaCoberturaController()
        {
        }

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

        [HttpGet("buscar/{materialId}/{pessoaId}")]
        public IEnumerable<CartaCoberturaModel> Get(int materialId, int pessoaId)
        {
            try
            {
                return CartaCoberturaService.Get(materialId, pessoaId);
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

        [HttpDelete("{cartaCoberturaId}")]
        public void Delete(int cartaCoberturaId)
        {
            try
            {
                CartaCoberturaService.Delete(cartaCoberturaId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
