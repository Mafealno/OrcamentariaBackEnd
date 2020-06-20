using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class OrcamentoService
    {
        private IOrcamentoRepository OrcamentoRepository;
        private MetodosGenericosService MetodosGenericosService;
        private PessoaService PessoaService;
        private ItensOrcamentoGeralService ItensOrcamentoGeralService;
        private MaoObraOrcamentoService MaoObraOrcamentoService;
        private EquipamentoOrcamentoService EquipamentoOrcamentoService;
        private CustoOrcamentoService CustoOrcamentoService;
        private TotaisOrcamentoService TotaisOrcamentoService;

        public OrcamentoService(IOrcamentoRepository orcamentoRepository, MetodosGenericosService metodosGenericosService,
            PessoaService pessoaService, ItensOrcamentoGeralService itensOrcamentoGeralService, 
            MaoObraOrcamentoService maoObraOrcamentoService, EquipamentoOrcamentoService equipamentoOrcamentoService, 
            CustoOrcamentoService custoOrcamentoService, TotaisOrcamentoService totaisOrcamentoService)
        {
            this.OrcamentoRepository = orcamentoRepository;
            this.MetodosGenericosService = metodosGenericosService;
            this.PessoaService = pessoaService;
            this.ItensOrcamentoGeralService = itensOrcamentoGeralService;
            this.MaoObraOrcamentoService = maoObraOrcamentoService;
            this.EquipamentoOrcamentoService = equipamentoOrcamentoService;
            this.CustoOrcamentoService = custoOrcamentoService;
            this.TotaisOrcamentoService = totaisOrcamentoService;
        }

        public IEnumerable<OrcamentoModel> Get()
        {
            try
            {
                var listOrcamento = OrcamentoRepository.List();

                foreach(OrcamentoModel orcamento in listOrcamento)
                {
                    var pessoaId = MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_ORCAMENTO", $"ORCAMENTO_ID = {orcamento.ORCAMENTO_ID}");
                    orcamento.CLIENTE_ORCAMENTO = PessoaService.GetComParametro(new PessoaQO(int.Parse(pessoaId), "")).ToArray()[0];

                    orcamento.LIST_ITENS_ORCAMENTO_GERAL = ItensOrcamentoGeralService.GetComParametro(new ItensOrcamentoQO(0, orcamento.ORCAMENTO_ID)).ToList();
                    orcamento.LIST_MAO_OBRA_ORCAMENTO = MaoObraOrcamentoService.Get(orcamento.ORCAMENTO_ID).ToList();
                    orcamento.LIST_CUSTO_ORCAMENTO = CustoOrcamentoService.GetComParametro(new CustoOrcamentoQO(0, orcamento.ORCAMENTO_ID)).ToList();
                    orcamento.LIST_EQUIPAMENTO_ORCAMENTO = EquipamentoOrcamentoService.GetComParametro(new EquipamentoOrcamentoQO(0, orcamento.ORCAMENTO_ID)).ToList();
                    orcamento.TOTAIS_ORCAMENTO = TotaisOrcamentoService.GetComParametro(new TotaisOrcamentoQO(0, orcamento.ORCAMENTO_ID)).ToArray()[0];
                }

                return listOrcamento;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<OrcamentoModel> Get(int orcamentoId)
        {
            try
            {
                List<OrcamentoModel> listOrcamento = new List<OrcamentoModel>();
                listOrcamento.Add(OrcamentoRepository.Find(orcamentoId));

                foreach (OrcamentoModel orcamento in listOrcamento)
                {
                    var pessoaId = MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_ORCAMENTO", $"ORCAMENTO_ID = {orcamento.ORCAMENTO_ID}");
                    orcamento.CLIENTE_ORCAMENTO = PessoaService.GetComParametro(new PessoaQO(int.Parse(pessoaId), "")).ToArray()[0];

                    orcamento.LIST_ITENS_ORCAMENTO_GERAL = ItensOrcamentoGeralService.GetComParametro(new ItensOrcamentoQO(0, orcamento.ORCAMENTO_ID)).ToList();
                    orcamento.LIST_MAO_OBRA_ORCAMENTO = MaoObraOrcamentoService.Get(orcamento.ORCAMENTO_ID).ToList();
                    orcamento.LIST_CUSTO_ORCAMENTO = CustoOrcamentoService.GetComParametro(new CustoOrcamentoQO(0, orcamento.ORCAMENTO_ID)).ToList();
                    orcamento.LIST_EQUIPAMENTO_ORCAMENTO = EquipamentoOrcamentoService.GetComParametro(new EquipamentoOrcamentoQO(0, orcamento.ORCAMENTO_ID)).ToList();
                    orcamento.TOTAIS_ORCAMENTO = TotaisOrcamentoService.GetComParametro(new TotaisOrcamentoQO(0, orcamento.ORCAMENTO_ID)).ToArray()[0];
                }

                return listOrcamento;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public OrcamentoModel Post(OrcamentoModel orcamento)
        {
            try
            {
                var where = $"PESSOA_ID = {orcamento.CLIENTE_ORCAMENTO.PESSOA_ID}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_PESSOA", where)))
                {
                    throw new Exception();
                }

                orcamento.CLIENTE_ORCAMENTO = PessoaService.GetComParametro(new PessoaQO(orcamento.CLIENTE_ORCAMENTO.PESSOA_ID, "")).ToArray()[0];

                return OrcamentoRepository.Create(orcamento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Put(int orcamentoId, OrcamentoModel orcamento)
        {
            try
            {
                var where = $"ORCAMENTO_ID = {orcamentoId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_ORCAMENTO", where)))
                {
                    throw new Exception();
                }

                var orcamentoDB = Get(orcamentoId).ToArray()[0];

                if(orcamento.CLIENTE_ORCAMENTO.PESSOA_ID != orcamentoDB.CLIENTE_ORCAMENTO.PESSOA_ID)
                {
                    orcamento.CLIENTE_ORCAMENTO = PessoaService.GetComParametro(new PessoaQO(orcamento.CLIENTE_ORCAMENTO.PESSOA_ID, "")).ToArray()[0];
                }

                OrcamentoRepository.Update(orcamentoId, orcamento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int orcamentoId)
        {
            try
            {
                MetodosGenericosService.StartTransactionCommitRollbackOrcamentaria(MetodosGenericosEnum.START);

                var where = $"ORCAMENTO_ID = {orcamentoId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_ORCAMENTO", where)))
                {
                    throw new Exception();
                }

                TotaisOrcamentoService.DeleteComParametro(new TotaisOrcamentoQO(0, orcamentoId));

                MaoObraOrcamentoService.Delete(orcamentoId);
                
                CustoOrcamentoService.DeleteComParametro(new CustoOrcamentoQO(0, orcamentoId));
                
                EquipamentoOrcamentoService.DeleteComParamenro(new EquipamentoOrcamentoQO(0, orcamentoId));
                
                ItensOrcamentoGeralService.DeleteComParametro(new ItensOrcamentoQO(0, orcamentoId));

                OrcamentoRepository.Delete(orcamentoId);

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
