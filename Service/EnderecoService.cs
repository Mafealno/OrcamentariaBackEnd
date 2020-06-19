using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class EnderecoService
    {
        private IEnderecoRepository EnderecoRepository;
        private MetodosGenericosService MetodosGenericosService;

        public EnderecoService(IEnderecoRepository enderecoRepository, MetodosGenericosService metodosGenericosService)
        {
            this.EnderecoRepository = enderecoRepository;
            this.MetodosGenericosService = metodosGenericosService;
        }

        public IEnumerable<EnderecoModel> Get()
        {
            try
            {
                return EnderecoRepository.List();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<EnderecoModel> GetComParametro(EnderecoQO endereco)
        {
            try
            {
                if (endereco.PessoaId != 0)
                {
                    var where = $"PESSOA_ID = {endereco.PessoaId}";
                    if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_PESSOA", where)))
                    {
                        throw new Exception();
                    }

                    return EnderecoRepository.ListPorPessoaId(endereco.PessoaId);
                }
                else
                {
                    List<EnderecoModel> listEndereco = new List<EnderecoModel>();

                    listEndereco.Add(EnderecoRepository.Find(endereco.EnderecoId));

                    return listEndereco;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public EnderecoModel Post(EnderecoModel endereco)
        {
            try
            {
                var where = $"PESSOA_ID = {endereco.PESSOA_ID}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_PESSOA", where)))
                {
                    throw new Exception();
                }

                if (endereco.ENDERECO_PADRAO)
                {
                    var enderecoDB = EnderecoRepository.FindPorEnderecoPadrao(endereco.PESSOA_ID);

                    if (enderecoDB.ENDERECO_PADRAO)
                    {
                        enderecoDB.ENDERECO_PADRAO = false;

                        EnderecoRepository.Update(enderecoDB.ENDERECO_ID, enderecoDB);
                    }
                }
                
                return EnderecoRepository.Create(endereco);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Put(int enderecoId, EnderecoModel endereco)
        {
            try
            {
                var where = $"ENDERECO_ID = {enderecoId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ENDERECO_ID", "T_ORCA_ENDERECO", where)))
                {
                    throw new Exception();
                }

                if (endereco.ENDERECO_PADRAO)
                {
                    var enderecoDB = EnderecoRepository.FindPorEnderecoPadrao(endereco.PESSOA_ID);

                    if (enderecoDB.ENDERECO_PADRAO)
                    {
                        enderecoDB.ENDERECO_PADRAO = false;

                        EnderecoRepository.Update(enderecoDB.ENDERECO_ID, enderecoDB);
                    }
                }

                EnderecoRepository.Update(enderecoId, endereco);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteComParametro(EnderecoQO endereco)
        {
            try
            {
                if (endereco.PessoaId != 0)
                {
                    var where = $"PESSOA_ID = {endereco.PessoaId}";
                    if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_PESSOA", where)))
                    {
                        throw new Exception();
                    }

                    EnderecoRepository.DeletePorPessoaId(endereco.PessoaId);
                }
                else
                {
                    var where = $"ENDERECO_ID = {endereco.EnderecoId}";
                    if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ENDERECO_ID", "T_ORCA_ENDERECO", where)))
                    {
                        throw new Exception();
                    }

                    EnderecoRepository.Delete(endereco.EnderecoId);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
