using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace OrcamentariaBackEnd
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {

        private PerfilService PerfilService;

        public PerfilController(PerfilService perfilService)
        {
            this.PerfilService = perfilService;
        }

        [HttpGet]
        public IEnumerable<PerfilModel> Get()
        {
            try
            {
                return PerfilService.Get();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("buscar")]
        public IEnumerable<PerfilModel> Get([FromQuery] PerfilQO perfil)
        {
            try
            {
                return PerfilService.GetComParametro(perfil);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public PerfilModel Post([FromBody] PerfilModel perfil)
        {
            try
            {
                return PerfilService.Post(perfil);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{perfilId}")]
        public void Put(int perfilId, [FromBody] PerfilModel perfil)
        {
            try
            {
                PerfilService.Put(perfilId, perfil);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{perfilId}")]
        public void Delete(int perfilId)
        {
            try
            {
                PerfilService.Delete(perfilId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
