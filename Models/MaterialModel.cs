using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrcamentariaBackEnd
{
    public class MaterialModel
    {
        private int _materialId;
        private string _nome;
        private string _descricao;
        private string _tipoMaterial;
        private PessoaModel _fabricante;

        public MaterialModel(int materialId, string nome, string descricao, string tipoMaterial, PessoaModel fabricante)
        {
            MATERIAL_ID = materialId;
            NOME_MATERIAL = nome;
            DESCRICAO_MATERIAL = descricao;
            TIPO_MATERIAL = tipoMaterial;
            FABRICANTE = fabricante;
        }

        public MaterialModel()
        {

        }
        public int MATERIAL_ID { get => _materialId; set => _materialId = value; }
        public string NOME_MATERIAL { get => _nome; set => _nome = value; }
        public string DESCRICAO_MATERIAL { get => _descricao; set => _descricao = value; }
        public string TIPO_MATERIAL { get => _tipoMaterial; set => _tipoMaterial = value; }
        public PessoaModel FABRICANTE { get => _fabricante; set => _fabricante = value; }
    }
}