﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orcamentaria.Model.Cadastro;

namespace Orcamentaria.Model.Orcamento
{
    public class OrcamentoIntumescenteModel : OrcamentoModel
    {
        private string _grupo;
        private string _ocupacaoUso;
        private string _classe;
        private string _divisao;
        private string _tempoResistenciaFogo;
        private double _qtdeLitrosTotal;
        private double _percentualPerda;
        private double _qtdeBaldes;
        private double _qtdeBaldesReal;
        private List<ItensOrcamentoIntumescenteModel> _itensOrcamentoIntumescente;

        public OrcamentoIntumescenteModel(string grupo, string ocupacaoUso, string classe, string divisao, string tempoResistenciaFogo, 
                                            double qtdeLitrosTotal, double percentualPerda, double qtdeBaldes, double qtdeBaldesReal, List<ItensOrcamentoIntumescenteModel> itensOrcamentoIntumescente,
                                            int orcamentoId, string nomeObra, string referencia, string prazoEntrega, DateTime dataCriacaoOrcamento, 
                                            string contatoObra, string tipoObra, TotaisOrcamentoModel totaisOrcamento, PessoaModel clienteOrcamento, 
                                            List<ItensOrcamentoModel> itensOrcamento, List<MaoObraOrcamentoModel> maoObraOrcamento, 
                                            List<CustoOrcamentoModel> custoOrcamento, List<EquipamentoOrcamentoModel> equipamentoOrcamento) : base(orcamentoId, 
                                            nomeObra, referencia, prazoEntrega, dataCriacaoOrcamento, contatoObra, tipoObra, totaisOrcamento, clienteOrcamento, itensOrcamento,
                                            maoObraOrcamento, custoOrcamento, equipamentoOrcamento)
        {
            GRUPO = grupo;
            OCUPACAO_USO = ocupacaoUso;
            CLASSE = classe;
            DIVISAO = divisao;
            TEMPO_RESISTENCIA_FOGO = tempoResistenciaFogo;
            QTDE_LITROS_TOTAL = qtdeLitrosTotal;
            PERCENTUAL_PERDA = percentualPerda;
            QTDE_BALDES = qtdeBaldes;
            QTDE_BALDES_REAL = qtdeBaldesReal;
            LIST_ITENS_ORCAMENTO_INTUMESCENTE = itensOrcamentoIntumescente;
            ORCAMENTO_ID = orcamentoId;
            NOME_OBRA = nomeObra;
            REFERENCIA = referencia;
            PRAZO_ENTREGA = prazoEntrega;
            DATA_CRIACAO_ORCAMENTO = dataCriacaoOrcamento;
            A_C = contatoObra;
            TIPO_OBRA = tipoObra;
            TOTAIS_ORCAMENTO = totaisOrcamento;
            CLIENTE_ORCAMENTO = clienteOrcamento;
            LIST_ITENS_ORCAMENTO = itensOrcamento;
            LIST_MAO_OBRA_ORCAMENTO = maoObraOrcamento;
            LIST_CUSTO_ORCAMENTO = custoOrcamento;
            LIST_EQUIPAMENTO_ORCAMENTO = equipamentoOrcamento;
        }

        public OrcamentoIntumescenteModel()
        {

        }

        public string GRUPO { get => _grupo; set => _grupo = value; }
        public string OCUPACAO_USO { get => _ocupacaoUso; set => _ocupacaoUso = value; }
        public string DIVISAO { get => _divisao; set => _divisao = value; }
        public string CLASSE { get => _classe; set => _classe = value; }
        public string TEMPO_RESISTENCIA_FOGO { get => _tempoResistenciaFogo; set => _tempoResistenciaFogo = value; }
        public double QTDE_LITROS_TOTAL { get => _qtdeLitrosTotal; set => _qtdeLitrosTotal = value; }
        public double PERCENTUAL_PERDA { get => _percentualPerda; set => _percentualPerda = value; }
        public double QTDE_BALDES { get => _qtdeBaldes; set => _qtdeBaldes = value; }
        public double QTDE_BALDES_REAL { get => _qtdeBaldesReal; set => _qtdeBaldesReal = value; }
        public List<ItensOrcamentoIntumescenteModel> LIST_ITENS_ORCAMENTO_INTUMESCENTE { get => _itensOrcamentoIntumescente; set => _itensOrcamentoIntumescente = value; }

    }
}