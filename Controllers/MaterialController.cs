using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

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

        [HttpDelete("{materialId}")]
        public void Delete(int materialId)
        {
            try
            {
                MaterialService.Delete(materialId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
