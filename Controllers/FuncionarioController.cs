using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace OrcamentariaBackEnd
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {

        private IFuncionarioRepository FuncionarioRepository;
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
                if (string.IsNullOrEmpty(funcionario.NomePessoa))
                {
                    List<FuncionarioModel> listFuncionario = new List<FuncionarioModel>();

                    listFuncionario.Add(FuncionarioRepository.Find(funcionario.PessoaId));

                    return listFuncionario;
                }
                else
                {
                    return FuncionarioRepository.ListPorNomePessoa(funcionario.NomePessoa);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST api/<FuncionarioController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FuncionarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FuncionarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
