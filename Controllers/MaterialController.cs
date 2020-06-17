using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Mvc;
using Renci.SshNet.Messages;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrcamentariaBackEnd
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {

        private MaterialService MaterialService;

        public MaterialController(MaterialService materialService)
        {
            this.MaterialService = materialService;
        }

        // GET: api/<MaterialController>
        [HttpGet]
        public IEnumerable<MaterialModel> Get()
        {
            try
            {
                return MaterialService.Get();
            }
            catch (Exception)
            {

                throw;
            }
        }

        //GET api/<MaterialController>/5
        [HttpGet("buscar")]
        public IEnumerable<MaterialModel> Get([FromQuery] MaterialQO material)
        {
            try
            {
                return MaterialService.GetComParametro(material);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //POST api/<MaterialController>
        [HttpPost]
        public MaterialModel Post([FromBody] MaterialModel material)
        {
            try
            {
                return MaterialService.Post(material);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // PUT api/<MaterialController>/5
        [HttpPut("{materialId}")]
        public void Put(int materialId, [FromBody] MaterialModel material)
        {
            try
            {
                MaterialService.Put(materialId, material);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // DELETE api/<MaterialController>/5
        [HttpDelete("{materialId}")]
        public void Delete(int materialId)
        {
            try
            {
                Debug.Print("FUNCIONALIDADE NÃO ATIVA");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
