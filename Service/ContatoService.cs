using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class ContatoService
    {

        private IContatoRepository ContatoRepository;
        private MetodosGenericosService MetodosGenericosService;

        public ContatoService(IContatoRepository contatoRepository, MetodosGenericosService metodosGenericosService)
        {
            this.ContatoRepository = contatoRepository;
            this.MetodosGenericosService = metodosGenericosService;
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

        public IEnumerable<ContatoModel> Get(int pessoaId, string tipoContato)
        {
            try
            {
                var where = $"PESSOA_ID = {pessoaId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_PESSOA", where)))
                {
                    throw new Exception();
                }

                List<ContatoModel> listContato = new List<ContatoModel>();

                listContato.Add(ContatoRepository.FindPorContatoPadraoETipoContato(pessoaId, tipoContato));

                return listContato;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ContatoModel> GetComParametro( ContatoQO contato)
        {
            try
            {
                if(contato.PessoaId != 0)
                {
                    var where = $"PESSOA_ID = {contato.PessoaId}";
                    if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_PESSOA", where)))
                    {
                        throw new Exception();
                    }

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
                if(string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_PESSOA", where)))
                {
                    throw new Exception();
                }

                if (contato.CONTATO_PADRAO)
                {
                    var contatoDB = ContatoRepository.FindPorContatoPadraoETipoContato(contato.PESSOA_ID, contato.TIPO_CONTATO);

                    if (contatoDB.CONTATO_PADRAO)
                    {
                        contatoDB.CONTATO_PADRAO = false;

                        ContatoRepository.Update(contatoDB.CONTATO_ID, contatoDB);
                    }
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
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("CONTATO_ID", "T_ORCA_CONTATO", where)))
                {
                    throw new Exception();
                }

                if (contato.CONTATO_PADRAO)
                {
                    var contatoDB = ContatoRepository.FindPorContatoPadraoETipoContato(contato.PESSOA_ID, contato.TIPO_CONTATO);

                    if (contatoDB.CONTATO_PADRAO)
                    {
                        contatoDB.CONTATO_PADRAO = false;

                        ContatoRepository.Update(contatoDB.CONTATO_ID, contatoDB);
                    }
                }

                ContatoRepository.Update(contatoId, contato);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteComParametro(ContatoQO contato)
        {
            try
            {
                if (contato.PessoaId != 0)
                {
                    var where = $"PESSOA_ID = {contato.PessoaId}";
                    if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_PESSOA", where)))
                    {
                        throw new Exception();
                    }

                    ContatoRepository.DeletePorPessoaId(contato.PessoaId);
                }
                else
                {
                    var where = $"CONTATO_ID = {contato.ContatoId}";
                    if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("CONTATO_ID", "T_ORCA_CONTATO", where)))
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
