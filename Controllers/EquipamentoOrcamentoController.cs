using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OrcamentariaBackEnd
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipamentoOrcamentoController : ControllerBase
    {

        private EquipamentoOrcamentoService EquipamentoOrcamentoService;

        public EquipamentoOrcamentoController(EquipamentoOrcamentoService equipamentoOrcamentoService)
        {
            this.EquipamentoOrcamentoService = equipamentoOrcamentoService;
        }


        [HttpGet]
        public IEnumerable<EquipamentoOrcamentoModel> Get()
        {
            try
            {
                return EquipamentoOrcamentoService.Get();
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet("buscar")]
        public IEnumerable<EquipamentoOrcamentoModel> Get([FromQuery] EquipamentoOrcamentoQO equipamentoOrcamento)
        {
            try
            {
                return EquipamentoOrcamentoService.GetComParametro(equipamentoOrcamento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public EquipamentoOrcamentoModel Post([FromBody] EquipamentoOrcamentoModel equipamentoOrcamento)
        {
            try
            {
                return EquipamentoOrcamentoService.Post(equipamentoOrcamento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{equipamentoOrcamentoId}")]
        public void Put(int equipamentoOrcamentoId, [FromBody] EquipamentoOrcamentoModel equipamentoOrcamento)
        {
            try
            {
                EquipamentoOrcamentoService.Put(equipamentoOrcamentoId, equipamentoOrcamento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{deletar}")]
        public void Delete([FromQuery] EquipamentoOrcamentoQO equipamentoOrcamento)
        {
            try
            {
                EquipamentoOrcamentoService.DeleteComParamenro(equipamentoOrcamento);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
