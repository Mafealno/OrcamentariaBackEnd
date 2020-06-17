
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentariaBackEnd
{
    public class ContatoModel
    {
        private int _contatoId;
        private int _pessoaId;
        private string _contato;
        private string _tipoContato;
        private string _ddd;
        private string _ramal;
        private bool _contatoPadrao;

        public ContatoModel(int contatoId, int pessoaId, string contato, string ddd, string ramal, bool contatoPadrao, string tipoContato)
        {
            CONTATO_ID = contatoId;
            PESSOA_ID = pessoaId;
            CONTATO = contato;
            TIPO_CONTATO = tipoContato;
            DDD = ddd;
            RAMAL = ramal;
            CONTATO_PADRAO = contatoPadrao;
        }

        public ContatoModel()
        {

        }

        public int CONTATO_ID { get => _contatoId; set => _contatoId = value; }
        public int PESSOA_ID { get => _pessoaId; set => _pessoaId = value; }
        public string CONTATO { get => _contato; set => _contato = value; }
        public string TIPO_CONTATO { get => _tipoContato; set => _tipoContato = value; }
        public string DDD { get => _ddd; set => _ddd = value; }
        public string RAMAL { get => _ramal; set => _ramal = value; }
        public bool CONTATO_PADRAO { get => _contatoPadrao; set => _contatoPadrao = value; }
    }
}