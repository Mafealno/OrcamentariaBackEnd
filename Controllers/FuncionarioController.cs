using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace OrcamentariaBackEnd
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {

        private FuncionarioService FuncionarioService;

        public FuncionarioController(FuncionarioService funcionarioService)
        {
            this.FuncionarioService = funcionarioService;
        }

        [HttpGet]
        public IEnumerable<FuncionarioModel> Get()
        {
            try
            {
                return FuncionarioService.Get();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("buscar")]
        public IEnumerable<FuncionarioModel> Get([FromQuery] FuncionarioQO funcionario)
        {
            try
            {
                return FuncionarioService.GetComParametro(funcionario);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public FuncionarioModel Post([FromBody] FuncionarioModel funcionario)
        {
            try
            {
                return FuncionarioService.Post(funcionario);
            }
            catch (Exception)
            {

                    throw;
            }
        }

        [HttpPut("{pessoaId}")]
        public bool Put(int pessoaId, [FromBody] FuncionarioModel funcionario)
        {
            try
            {
                FuncionarioService.Put(pessoaId, funcionario);

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{pessoaId}")]
        public bool Delete(int pessoaId)
        {
            try
            {
                FuncionarioService.Delete(pessoaId);

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
