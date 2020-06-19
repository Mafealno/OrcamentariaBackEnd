using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class PessoaService
    {
        private IPessoaRepository PessoaRepository;
        private MetodosGenericosService MetodosGenericosService;
        private ContatoService ContatoService;
        private EnderecoService EnderecoService;

        public PessoaService(IPessoaRepository pessoaRepository, MetodosGenericosService metodosGenericosService, 
            ContatoService contatoService, EnderecoService enderecoService)
        {
            this.PessoaRepository = pessoaRepository;
            this.MetodosGenericosService = metodosGenericosService;
            this.ContatoService = contatoService;
            this.EnderecoService = enderecoService;
        }

        public IEnumerable<PessoaModel> Get()
        {
            try
            {
                var listPessoas = PessoaRepository.List();

                foreach(PessoaModel pessoa in listPessoas)
                {
                    pessoa.LIST_CONTATO = ContatoService.GetComParametro(new ContatoQO(0, pessoa.PESSOA_ID)).ToList();
                    pessoa.LIST_ENDERECO = EnderecoService.GetComParametro(new EnderecoQO(0, pessoa.PESSOA_ID)).ToList();
                }

                return listPessoas;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<PessoaModel> GetComParametro(PessoaQO pessoa)
        {
            try
            {
                List<PessoaModel> listPessoas;

                if (!string.IsNullOrEmpty(pessoa.NomePessoa))
                {
                    listPessoas = PessoaRepository.ListPorNomePessoa(pessoa.NomePessoa).ToList();
                }
                else
                {
                    listPessoas = new List<PessoaModel>();

                    listPessoas.Add(PessoaRepository.Find(pessoa.PessoaId));
                }

                foreach (PessoaModel pessoaModel in listPessoas)
                {
                    pessoaModel.LIST_CONTATO = ContatoService.GetComParametro(new ContatoQO(0, pessoaModel.PESSOA_ID)).ToList();
                    pessoaModel.LIST_ENDERECO = EnderecoService.GetComParametro(new EnderecoQO(0, pessoaModel.PESSOA_ID)).ToList();
                }

                return listPessoas;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public PessoaModel Post(PessoaModel pessoa)
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

        public void Put(int pessoaId, PessoaModel pessoa)
        {
            try
            {
                var where = $"PESSOA_ID = {pessoaId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_PESSOA", where)))
                {
                    throw new Exception();
                }

                PessoaRepository.Update(pessoaId, pessoa);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int pessoaId)
        {
            try
            {
                var where = $"PESSOA_ID = {pessoaId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_PESSOA", where)))
                {
                    throw new Exception();
                }

                MetodosGenericosService.StartTransactionCommitRollbackOrcamentaria(MetodosGenericosEnum.START);

                ContatoService.DeleteComParametro(new ContatoQO(0, pessoaId));
                EnderecoService.DeleteComParametro(new EnderecoQO(0, pessoaId));

                PessoaRepository.Delete(pessoaId);

                MetodosGenericosService.StartTransactionCommitRollbackOrcamentaria(MetodosGenericosEnum.COMMIT);
            }
            catch (Exception)
            {
                MetodosGenericosService.StartTransactionCommitRollbackOrcamentaria(MetodosGenericosEnum.ROLLBACK);
                throw;
            }
        }
    }
}
