using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orcamentaria.Model.Cadastro
{
    public class PerfilModel
    {
        private int _perfilId;
        private string _nome;
        private double _valorD;
        private double _valorBf;
        private double _valorTw;
        private double _valorTf;
        private double _valorKgM;
        private string _tipoPerfil;

        public PerfilModel(int perfilId, string nome, double valorD, double valorBf, double valorTw, double valorTf, double valorKgM, string tipoPerfil)
        {
            PERFIL_ID = perfilId;
            NOME_PERFIL = nome;
            VALOR_D = valorD;
            VALOR_BF = valorBf;
            VALOR_TW = valorTw;
            VALOR_TF = valorTf;
            VALOR_KG_M = valorKgM;
            TIPO_PERFIL = tipoPerfil;
        }

        public PerfilModel()
        {

        }

        public int PERFIL_ID { get => _perfilId; set => _perfilId = value; }
        public string NOME_PERFIL { get => _nome; set => _nome = value; }
        public double VALOR_D { get => _valorD; set => _valorD = value; }
        public double VALOR_BF { get => _valorBf; set => _valorBf = value; }
        public double VALOR_TW { get => _valorTw; set => _valorTw = value; }
        public double VALOR_TF { get => _valorTf; set => _valorTf = value; }
        public double VALOR_KG_M { get => _valorKgM; set => _valorKgM = value; }
        public string TIPO_PERFIL { get => _tipoPerfil; set => _tipoPerfil = value; }
    }
}