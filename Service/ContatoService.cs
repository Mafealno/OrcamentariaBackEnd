using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class ContatoService
    {

        private IContatoRepository ContatoRepository;
        private IMetodosGenericosRepository MetodosGenericosRepository;

        public ContatoService(IContatoRepository contatoRepository, IMetodosGenericosRepository metodosGenericosRepository)
        {
            this.ContatoRepository = contatoRepository;
            this.MetodosGenericosRepository = metodosGenericosRepository;
        }

        public IEnumerable<ContatoModel> Get()
        {
            try
            {
                return ContatoRepository.List();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ContatoModel Get(int pessoaId, string tipoContato)
        {
            try
            {
                return ContatoRepository.FindPorContatoPadraoETipoContato(pessoaId, tipoContato);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ContatoModel> GetComParametro(ContatoQO contato)
        {
            try
            {
                if(contato.PessoaId != 0)
                {
                    return ContatoRepository.ListPorPessoaId(contato.PessoaId);
                }
                else
                {
                    List<ContatoModel> listContato = new List<ContatoModel>();

                    listContato.Add(ContatoRepository.Find(contato.ContatoId));

                    return listContato;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ContatoModel Post(ContatoModel contato)
        {
            try
            {
                var where = $"PESSOA_ID = {contato.PESSOA_ID}";
                if(string.IsNullOrEmpty(MetodosGenericosRepository.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_PESSOA", where)))
                {
                    throw new Exception();
                }

                return ContatoRepository.Create(contato);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Put(int contatoId, ContatoModel contato)
        {
            try
            {
                var where = $"CONTATO_ID = {contatoId}";
                if (string.IsNullOrEmpty(MetodosGenericosRepository.DlookupOrcamentaria("CONTATO_ID", "T_ORCA_CONTATO", where)))
                {
                    throw new Exception();
                }

                ContatoRepository.Update(contatoId, contato);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(ContatoQO contato)
        {
            try
            {
                if (contato.PessoaId != 0)
                {
                    var where = $"PESSOA_ID = {contato.PessoaId}";
                    if (string.IsNullOrEmpty(MetodosGenericosRepository.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_PESSOA", where)))
                    {
                        throw new Exception();
                    }

                    ContatoRepository.DeletePorPessoaId(contato.PessoaId);
                }
                else
                {
                    var where = $"CONTATO_ID = {contato.ContatoId}";
                    if (string.IsNullOrEmpty(MetodosGenericosRepository.DlookupOrcamentaria("CONTATO_ID", "T_ORCA_CONTATO", where)))
                    {
                        throw new Exception();
                    }

                    ContatoRepository.Delete(contato.ContatoId);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
