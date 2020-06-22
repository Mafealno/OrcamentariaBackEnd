using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace OrcamentariaBackEnd
{

    [Route("api/[controller]")]
    [ApiController]
    public class EquipamentoController : ControllerBase
    {

        private EquipamentoService EquipamentoService;

        public EquipamentoController(EquipamentoService equipamentoService)
        {
            this.EquipamentoService = equipamentoService;
        }

        [HttpGet]
        public IEnumerable<EquipamentoModel> Get()
        {
            try
            {
                return EquipamentoService.Get();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("buscar")]
        public IEnumerable<EquipamentoModel> Get([FromQuery] EquipamentoQO equipamento)
        {
            try
            {
                return EquipamentoService.GetComParametro(equipamento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public EquipamentoModel Post([FromBody] EquipamentoModel equipamento)
        {
            try
            {
                return EquipamentoService.Post(equipamento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{equipamentoId}")]
        public void Put(int equipamentoId, [FromBody] EquipamentoModel equipamento)
        {
            try
            {
                EquipamentoService.Put(equipamentoId, equipamento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{equipamentoId}")]
        public void Delete(int equipamentoId)
        {
            try
            {
                EquipamentoService.Delete(equipamentoId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
