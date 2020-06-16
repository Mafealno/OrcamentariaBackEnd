using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orcamentaria.Model.Cadastro;
using OrcamentariaBackEnd.Query_Objects;
using OrcamentariaBackEnd.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrcamentariaBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {

        private IFuncionarioRepository FuncionarioRepository;

        public FuncionarioController(IFuncionarioRepository funcionarioRepository)
        {
            this.FuncionarioRepository = funcionarioRepository;
        }

        // GET: api/<FuncionarioController>
        [HttpGet]
        public IEnumerable<FuncionarioModel> Get()
        {
            try
            {
                return FuncionarioRepository.List();
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
                    return FuncionarioRepository.ListPorNomèPessoa(funcionario.NomePessoa);
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
