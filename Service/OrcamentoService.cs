using System;
using System.Collections.Generic;
using System.Linq;

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
        private MaterialOrcamentoService MaterialOrcamentoService;
        private CustoOrcamentoService CustoOrcamentoService;
        private TotaisOrcamentoRepository TotaisOrcamentoRepository;

        public OrcamentoService(IOrcamentoRepository orcamentoRepository, MetodosGenericosService metodosGenericosService,
            PessoaService pessoaService, ItensOrcamentoGeralService itensOrcamentoGeralService, 
            MaoObraOrcamentoService maoObraOrcamentoService, EquipamentoOrcamentoService equipamentoOrcamentoService,
            MaterialOrcamentoService materialOrcamentoService, CustoOrcamentoService custoOrcamentoService, 
            TotaisOrcamentoRepository totaisOrcamentoRepository)
        {
            this.OrcamentoRepository = orcamentoRepository;
            this.MetodosGenericosService = metodosGenericosService;
            this.PessoaService = pessoaService;
            this.ItensOrcamentoGeralService = itensOrcamentoGeralService;
            this.MaoObraOrcamentoService = maoObraOrcamentoService;
            this.EquipamentoOrcamentoService = equipamentoOrcamentoService;
            this.MaterialOrcamentoService = materialOrcamentoService;
            this.CustoOrcamentoService = custoOrcamentoService;
            this.TotaisOrcamentoRepository = totaisOrcamentoRepository;
        }

        public IEnumerable<OrcamentoGeralModel> Get()
        {
            try
            {
                var listOrcamento = OrcamentoRepository.List();

                foreach(OrcamentoGeralModel orcamento in listOrcamento)
                {
                    var pessoaId = MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_ORCAMENTO", $"ORCAMENTO_ID = {orcamento.ORCAMENTO_ID}");
                    orcamento.CLIENTE_ORCAMENTO = PessoaService.GetComParametro(new PessoaQO(int.Parse(pessoaId), "")).ToArray()[0];

                    orcamento.LIST_ITENS_ORCAMENTO_GERAL = ItensOrcamentoGeralService.GetComParametro(new ItensOrcamentoQO(0, orcamento.ORCAMENTO_ID)).ToList();
                    orcamento.LIST_MAO_OBRA_ORCAMENTO = MaoObraOrcamentoService.Get(orcamento.ORCAMENTO_ID).ToList();
                    orcamento.LIST_CUSTO_ORCAMENTO = CustoOrcamentoService.GetComParametro(new CustoOrcamentoQO(0, orcamento.ORCAMENTO_ID)).ToList();
                    orcamento.LIST_EQUIPAMENTO_ORCAMENTO = EquipamentoOrcamentoService.GetComParametro(new EquipamentoOrcamentoQO(0, orcamento.ORCAMENTO_ID)).ToList();
                    orcamento.LIST_MATERIAL_ORCAMENTO = MaterialOrcamentoService.GetComParametro(new MaterialOrcamentoQO(0, orcamento.ORCAMENTO_ID)).ToList();
                    orcamento.TOTAIS_ORCAMENTO = TotaisOrcamentoRepository.FindPorOrcamentoId(orcamento.ORCAMENTO_ID);
                }

                return listOrcamento;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<OrcamentoGeralModel> Get(int orcamentoId)
        {
            try
            {
                List<OrcamentoGeralModel> listOrcamento = new List<OrcamentoGeralModel>();
                listOrcamento.Add(OrcamentoRepository.Find(orcamentoId));

                foreach (OrcamentoGeralModel orcamento in listOrcamento)
                {
                    var pessoaId = MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_ORCAMENTO", $"ORCAMENTO_ID = {orcamento.ORCAMENTO_ID}");
                    orcamento.CLIENTE_ORCAMENTO = PessoaService.GetComParametro(new PessoaQO(int.Parse(pessoaId), "")).ToArray()[0];

                    orcamento.LIST_ITENS_ORCAMENTO_GERAL = ItensOrcamentoGeralService.GetComParametro(new ItensOrcamentoQO(0, orcamento.ORCAMENTO_ID)).ToList();
                    orcamento.LIST_MAO_OBRA_ORCAMENTO = MaoObraOrcamentoService.Get(orcamento.ORCAMENTO_ID).ToList();
                    orcamento.LIST_CUSTO_ORCAMENTO = CustoOrcamentoService.GetComParametro(new CustoOrcamentoQO(0, orcamento.ORCAMENTO_ID)).ToList();
                    orcamento.LIST_EQUIPAMENTO_ORCAMENTO = EquipamentoOrcamentoService.GetComParametro(new EquipamentoOrcamentoQO(0, orcamento.ORCAMENTO_ID)).ToList();
                    orcamento.LIST_MATERIAL_ORCAMENTO = MaterialOrcamentoService.GetComParametro(new MaterialOrcamentoQO(0, orcamento.ORCAMENTO_ID)).ToList();
                    orcamento.TOTAIS_ORCAMENTO = TotaisOrcamentoRepository.FindPorOrcamentoId(orcamento.ORCAMENTO_ID);
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

                OrcamentoModel orcamentoCriado = OrcamentoRepository.Create(orcamento);

                orcamentoCriado.CLIENTE_ORCAMENTO = orcamento.CLIENTE_ORCAMENTO;

                return orcamentoCriado;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Put(int orcamentoId, OrcamentoGeralModel orcamento)
        {
            try
            {
                var where = $"ORCAMENTO_ID = {orcamentoId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_ORCAMENTO", where)))
                {
                    throw new Exception();
                }

                var orcamentoDB = Get(orcamentoId).ToArray()[0];

                orcamento.CLIENTE_ORCAMENTO = PessoaService.GetComParametro(new PessoaQO(orcamento.CLIENTE_ORCAMENTO.PESSOA_ID, "")).ToArray()[0];

                if (orcamento.CLIENTE_ORCAMENTO.PESSOA_ID != orcamentoDB.CLIENTE_ORCAMENTO.PESSOA_ID)
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

                TotaisOrcamentoRepository.DeletePorOrcamentoId(orcamentoId);

                MaoObraOrcamentoService.Delete(orcamentoId);
                
                CustoOrcamentoService.DeleteComParametro(new CustoOrcamentoQO(0, orcamentoId));
                
                EquipamentoOrcamentoService.DeleteComParamenro(new EquipamentoOrcamentoQO(0, orcamentoId));

                MaterialOrcamentoService.DeleteComParamenro(new MaterialOrcamentoQO(0, orcamentoId));

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
