using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orcamentaria.Model.Cadastro;
using OrcamentariaBackEnd.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrcamentariaBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartaCoberturaController : ControllerBase
    {

        private ICartaCoberturaRepository CartaCoberturaRepository;

        public CartaCoberturaController(ICartaCoberturaRepository cartaCoberturaRepository)
        {
            this.CartaCoberturaRepository = cartaCoberturaRepository;
        }


        // GET: api/<CartaCoberturaController>
        [HttpGet]
        public IEnumerable<CartaCoberturaModel> Get()
        {
            return CartaCoberturaRepository.List();
        }

        // GET api/<CartaCoberturaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CartaCoberturaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CartaCoberturaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CartaCoberturaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
