using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class TotaisOrcamentoService
    {
        private ITotaisOrcamentoRepository TotaisOrcamentoRepository;
        private MetodosGenericosService MetodosGenericosService;
        private OrcamentoGeralService OrcamentoGeralService;
        private OrcamentoIntumescenteService OrcamentoIntumescenteService;

        public TotaisOrcamentoService(ITotaisOrcamentoRepository totaisOrcamentoRepository, MetodosGenericosService metodosGenericosService,
            OrcamentoGeralService orcamentoGeralService, OrcamentoIntumescenteService orcamentoIntumescenteService)
        {
            this.TotaisOrcamentoRepository = totaisOrcamentoRepository;
            this.MetodosGenericosService = metodosGenericosService;
            this.OrcamentoGeralService = orcamentoGeralService;
            this.OrcamentoIntumescenteService = orcamentoIntumescenteService;
        }

        public IEnumerable<TotaisOrcamentoModel> Get()
        {
            try
            {
                return TotaisOrcamentoRepository.List();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TotaisOrcamentoModel GetComParametro(TotaisOrcamentoQO totaisOrcamento)
        {
            try
            {
                List<TotaisOrcamentoModel> listTotaisOrcamento = new List<TotaisOrcamentoModel>();

                if (totaisOrcamento.OrcamentoId != 0)
                {
                    listTotaisOrcamento.Add(TotaisOrcamentoRepository.FindPorOrcamentoId(totaisOrcamento.OrcamentoId));
                }
                else
                {
                    listTotaisOrcamento.Add(TotaisOrcamentoRepository.Find(totaisOrcamento.TotaisOrcamentoId));
                }

                return listTotaisOrcamento[0];
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TotaisOrcamentoModel Post(TotaisOrcamentoModel totaisOrcamento)
        {
            try
            {
                var where = $"ORCAMENTO_ID = {totaisOrcamento.ORCAMENTO_ID}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_ORCAMENTO", where)))
                {
                    throw new Exception();
                }

                return TotaisOrcamentoRepository.Create(totaisOrcamento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Put(int totaisOrcamentoId, TotaisOrcamentoModel totaisOrcamento)
        {
            try
            {
                var where = $"TOTAIS_ORCAMENTO_ID = {totaisOrcamentoId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("TOTAIS_ORCAMENTO_ID", "T_ORCA_TOTAIS_ORCAMENTO", where)))
                {
                    throw new Exception();
                }

                TotaisOrcamentoRepository.Update(totaisOrcamentoId, totaisOrcamento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteComParametro(TotaisOrcamentoQO totaisOrcamento)
        {
            try
            {
                if (totaisOrcamento.OrcamentoId != 0)
                {
                    var where = $"ORCAMENTO_ID = {totaisOrcamento.OrcamentoId}";
                    if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_ORCAMENTO", where)))
                    {
                        throw new Exception();
                    }

                    TotaisOrcamentoRepository.DeletePorOrcamentoId(totaisOrcamento.OrcamentoId);
                }
                else
                {
                    var where = $"TOTAIS_ORCAMENTO_ID = {totaisOrcamento.TotaisOrcamentoId}";
                    if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("totaisOrcamentoId", "T_ORCA_TOTAIS_ORCAMENTO", where)))
                    {
                        throw new Exception();
                    }
                    TotaisOrcamentoRepository.DeletePorOrcamentoId(totaisOrcamento.TotaisOrcamentoId);
                }   
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void CalcularTotaisOrcamento(int orcamentoId)
        {
            try
            {
                var where = $"ORCAMENTO_ID = {orcamentoId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_ORCAMENTO", where)))
                {
                    throw new Exception();
                }

                var totaisOrcamento = new TotaisOrcamentoModel();
                var totaisMaoObra = 0.0;
                var totaisEquipamentos = 0.0;
                var totaisMateriais = 0.0;
                var totaisCustos = 0.0;
                var totaisItens = 0.0;
                var totalArea = 0.0;
                var totalGeral = 0.0;

                var orcamentoDb = OrcamentoGeralService.Get(orcamentoId);

                var diasTrabalhado = orcamentoDb.FirstOrDefault().DIAS_TRABALHADO;

                foreach (MaoObraOrcamentoModel maoObraOrcamento in orcamentoDb.FirstOrDefault().LIST_MAO_OBRA_ORCAMENTO)
                {
                    totaisMaoObra += maoObraOrcamento.FUNCIONARIO.VALOR_DIA_TRABALHADO * diasTrabalhado;

                    totaisMaoObra += maoObraOrcamento.LIST_CUSTO.Aggregate(0.0, (acumulador, obj) => acumulador += obj.VALOR_CUSTO * MetodosGenericosService.RetornarFator(obj.TIPO_CUSTO, diasTrabalhado));
                }

                totaisEquipamentos = orcamentoDb.FirstOrDefault().LIST_EQUIPAMENTO_ORCAMENTO.Aggregate(0.0, (acumulador, obj) => acumulador += obj.VALOR_UNITARIO_EQUIPAMENTO * obj.QTDE_EQUIPAMENTO);
                totaisMateriais = orcamentoDb.FirstOrDefault().LIST_MATERIAL_ORCAMENTO.Aggregate(0.0, (acumulador, obj) => acumulador += obj.VALOR_UNITARIO_MATERIAL * obj.QTDE_MATERIAL);
                totaisCustos = orcamentoDb.FirstOrDefault().LIST_CUSTO_ORCAMENTO.Aggregate(0.0, (acumulador, obj) => acumulador += obj.VALOR_CUSTO * MetodosGenericosService.RetornarFator(obj.CUSTO_OBRA.TIPO_CUSTO, diasTrabalhado));

                if (orcamentoDb.FirstOrDefault().TIPO_OBRA != "Geral")
                {
                    if (!string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_ORCAMENTO_INTUMESCENTE", where)))
                    {                    
                        var orcamentoIntumescenteDb = OrcamentoIntumescenteService.Get(orcamentoId);
                        foreach (ItensOrcamentoIntumescenteModel itensOrcamentoIntumescente in orcamentoIntumescenteDb.FirstOrDefault().LIST_ITENS_ORCAMENTO_INTUMESCENTE)
                        {

                            totalArea += itensOrcamentoIntumescente.VALOR_HP * itensOrcamentoIntumescente.QTDE * itensOrcamentoIntumescente.VALOR_COMPRIMENTO;
                        }

                        totaisItens = totalArea * orcamentoIntumescenteDb.FirstOrDefault().VALOR_UNITARIO_INTUMESCENTE;

                        totaisItens += Math.Round((orcamentoIntumescenteDb.FirstOrDefault().QTDE_BALDES_REAL + 0.4)) * orcamentoIntumescenteDb.FirstOrDefault().VALOR_BALDE_INTUMESCENTE;
                    }
                }
                else
                {
                    foreach (ItensOrcamentoGeralModel itensOrcamentoGeralModel in orcamentoDb.FirstOrDefault().LIST_ITENS_ORCAMENTO_GERAL)
                    {
                        var areaAux = itensOrcamentoGeralModel.AREA;
                        totalArea += areaAux;

                        totaisItens += areaAux * itensOrcamentoGeralModel.VALOR_M_2;
                    }
                }

                totalGeral = totaisItens + totaisMaoObra + totaisEquipamentos + totaisCustos + totaisMateriais;

                totaisOrcamento = new TotaisOrcamentoModel(0, orcamentoId, totaisItens, totaisMaoObra, totaisEquipamentos, totaisMateriais, totaisCustos, totalGeral, totalArea);

                if (orcamentoDb.FirstOrDefault().TOTAIS_ORCAMENTO.TOTAIS_ORCAMENTO_ID == 0)
                {
                    Post(totaisOrcamento);
                }
                else
                {
                    Put(orcamentoDb.FirstOrDefault().TOTAIS_ORCAMENTO.TOTAIS_ORCAMENTO_ID, totaisOrcamento);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
