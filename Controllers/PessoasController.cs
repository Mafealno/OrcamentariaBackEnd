using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrcamentariaBackEnd
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private IPessoaRepository PessoaRepository;

        public PessoasController(IPessoaRepository pessoaRepository)
        {
            this.PessoaRepository = pessoaRepository;
        }

        [HttpGet]
        public IEnumerable<PessoaModel> Get()
        {
            try
            {
                return PessoaRepository.List();
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet("buscar")]
        public IEnumerable<PessoaModel> Get([FromQuery] PessoaQO pessoa)
        {
            try
            {
                if (string.IsNullOrEmpty(pessoa.NomePessoa))
                {
                    List<PessoaModel> listPessoas = new List<PessoaModel>();

                    listPessoas.Add(PessoaRepository.Find(pessoa.PessoaId));

                    return listPessoas;
                }
                else
                {
                    return PessoaRepository.ListPorNomePessoa(pessoa.NomePessoa);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST api/<PessoasController>
        [HttpPost]
        public PessoaModel Post([FromBody] PessoaModel pessoa)
        {
            try
            {
                return PessoaRepository.Create(pessoa);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // PUT api/<PessoasController>/5
        [HttpPut("{pessoaId}")]
        public void Put(int pessoaId, [FromBody] PessoaModel pessoa)
        {
            try
            {
                PessoaRepository.Update(pessoaId, pessoa);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // DELETE api/<PessoasController>/5
        [HttpDelete("{pessoaId}")]
        public void Delete(int pessoaId)
        {
            try
            {
                PessoaRepository.Delete(pessoaId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
