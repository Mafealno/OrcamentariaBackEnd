using KellermanSoftware.CompareNetObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class FuncionarioService
    {

        private IFuncionarioRepository FuncionarioRepository;
        private MetodosGenericosService MetodosGenericosService;
        private ContatoService ContatoService;
        private EnderecoService EnderecoService;
        private PessoaService PessoaService;

        public FuncionarioService(IFuncionarioRepository funcionarioRepository, MetodosGenericosService metodosGenericosService,
            ContatoService contatoService, EnderecoService enderecoService, PessoaService pessoaService)
        {
            this.FuncionarioRepository = funcionarioRepository;
            this.MetodosGenericosService = metodosGenericosService;
            this.ContatoService = contatoService;
            this.EnderecoService = enderecoService;
            this.PessoaService = pessoaService;
        }

        public IEnumerable<FuncionarioModel> Get()
        {
            try
            {
                var listFuncionarios = FuncionarioRepository.List();

                foreach (FuncionarioModel funcionario in listFuncionarios)
                {
                    funcionario.LIST_CONTATO = ContatoService.GetComParametro(new ContatoQO(0, funcionario.PESSOA_ID)).ToList();
                    funcionario.LIST_ENDERECO = EnderecoService.GetComParametro(new EnderecoQO(0, funcionario.PESSOA_ID)).ToList();
                }

                return listFuncionarios;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<FuncionarioModel> GetComParametro( FuncionarioQO funcionario)
        {
            try
            {
                List<FuncionarioModel> listFuncionarios;

                if (!string.IsNullOrEmpty(funcionario.NomePessoa))
                {
                    listFuncionarios = FuncionarioRepository.ListPorNomePessoa(funcionario.NomePessoa).ToList();
                }
                else
                {
                    listFuncionarios = new List<FuncionarioModel>();

                    listFuncionarios.Add(FuncionarioRepository.Find(funcionario.PessoaId));

                }

                foreach (FuncionarioModel funcionarioModel in listFuncionarios)
                {
                    funcionarioModel.LIST_CONTATO = ContatoService.GetComParametro(new ContatoQO(0, funcionarioModel.PESSOA_ID)).ToList();
                    funcionarioModel.LIST_ENDERECO = EnderecoService.GetComParametro(new EnderecoQO(0, funcionarioModel.PESSOA_ID)).ToList();
                }

                return listFuncionarios;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public FuncionarioModel Post(FuncionarioModel funcionario)
        {
            try
            {
                var where = $"PESSOA_ID = {funcionario.PESSOA_ID}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_PESSOA", where)))
                {
                    throw new Exception();
                }

                return FuncionarioRepository.Create(funcionario);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Put(int pessoaId, FuncionarioModel funcionario)
        {
            try
            {
                var where = $"PESSOA_ID = {pessoaId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_FUNCIONARIO", where)))
                {
                    throw new Exception();
                }

                var pessoa = MetodosGenericosService.CopiarPropriedadesObj(funcionario, new PessoaModel());
                var pessoaDB = PessoaService.GetComParametro(new PessoaQO(pessoaId, "")).ToArray()[0];

                ComparisonResult resultando = new CompareLogic().Compare(pessoa, pessoaDB);

                if (!resultando.AreEqual)
                {
                    var res = resultando.Differences;

                    PessoaService.Put(pessoaId, pessoa);
                }

                FuncionarioRepository.Update(pessoaId, funcionario);

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
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_FUNCIONARIO", where)))
                {
                    throw new Exception();
                }

                MetodosGenericosService.StartTransactionCommitRollbackOrcamentaria(MetodosGenericosEnum.START);

                PessoaService.Delete(pessoaId);

                FuncionarioRepository.Delete(pessoaId);

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
