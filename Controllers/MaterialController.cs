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
    public class MaterialController : ControllerBase
    {

        private IMaterialRepository MaterialRepository;

        public MaterialController(IMaterialRepository materialRepository)
        {
            this.MaterialRepository = materialRepository;
        }

        // GET: api/<MaterialController>
        [HttpGet]
        public IEnumerable<MaterialModel> Get()
        {
            try
            {
                return MaterialRepository.List();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET api/<MaterialController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MaterialController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MaterialController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MaterialController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
