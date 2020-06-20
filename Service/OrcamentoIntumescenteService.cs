using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class OrcamentoIntumescenteService
    {
        private IOrcamentoIntumescenteRepository OrcamentoIntumescenteRepository;
        private MetodosGenericosService MetodosGenericosService;
        private PessoaService PessoaService;
        private ItensOrcamentoIntumescenteService ItensOrcamentoIntumescenteService;
        private MaoObraOrcamentoService MaoObraOrcamentoService;
        private EquipamentoOrcamentoService EquipamentoOrcamentoService;
        private CustoOrcamentoService CustoOrcamentoService;
        private TotaisOrcamentoService TotaisOrcamentoService;

        public OrcamentoIntumescenteService(IOrcamentoIntumescenteRepository orcamentoIntumescenteRepository, MetodosGenericosService metodosGenericosService,
            PessoaService pessoaService, ItensOrcamentoIntumescenteService itensOrcamentoIntumescenteService,
            MaoObraOrcamentoService maoObraOrcamentoService, EquipamentoOrcamentoService equipamentoOrcamentoService,
            CustoOrcamentoService custoOrcamentoService, TotaisOrcamentoService totaisOrcamentoService)
        {
            this.OrcamentoIntumescenteRepository = orcamentoIntumescenteRepository;
            this.PessoaService = pessoaService;
            this.MetodosGenericosService = metodosGenericosService;
            this.ItensOrcamentoIntumescenteService = itensOrcamentoIntumescenteService;
            this.MaoObraOrcamentoService = maoObraOrcamentoService;
            this.EquipamentoOrcamentoService = equipamentoOrcamentoService;
            this.CustoOrcamentoService = custoOrcamentoService;
            this.TotaisOrcamentoService = totaisOrcamentoService;
        }

        public IEnumerable<OrcamentoIntumescenteModel> Get()
        {
            try
            {
                var listOrcamentoIntumescente = OrcamentoIntumescenteRepository.List();

                foreach (OrcamentoIntumescenteModel orcamentoIntumescente in listOrcamentoIntumescente)
                {
                    var pessoaId = MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_ORCAMENTO", $"ORCAMENTO_ID = {orcamentoIntumescente.ORCAMENTO_ID}");
                    orcamentoIntumescente.CLIENTE_ORCAMENTO = PessoaService.GetComParametro(new PessoaQO(int.Parse(pessoaId), "")).ToArray()[0];

                    orcamentoIntumescente.LIST_ITENS_ORCAMENTO_INTUMESCENTE = ItensOrcamentoIntumescenteService.GetComParametro(new ItensOrcamentoQO(0, orcamentoIntumescente.ORCAMENTO_ID)).ToList();
                    orcamentoIntumescente.LIST_MAO_OBRA_ORCAMENTO = MaoObraOrcamentoService.Get(orcamentoIntumescente.ORCAMENTO_ID).ToList();
                    orcamentoIntumescente.LIST_CUSTO_ORCAMENTO = CustoOrcamentoService.GetComParametro(new CustoOrcamentoQO(0, orcamentoIntumescente.ORCAMENTO_ID)).ToList();
                    orcamentoIntumescente.LIST_EQUIPAMENTO_ORCAMENTO = EquipamentoOrcamentoService.GetComParametro(new EquipamentoOrcamentoQO(0, orcamentoIntumescente.ORCAMENTO_ID)).ToList();
                    orcamentoIntumescente.TOTAIS_ORCAMENTO = TotaisOrcamentoService.GetComParametro(new TotaisOrcamentoQO(0, orcamentoIntumescente.ORCAMENTO_ID)).ToArray()[0];
                }

                return listOrcamentoIntumescente;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<OrcamentoIntumescenteModel> Get(int orcamentoId)
        {
            try
            {
                List<OrcamentoIntumescenteModel> listOrcamentoIntumescente = new List<OrcamentoIntumescenteModel>();
                listOrcamentoIntumescente.Add(OrcamentoIntumescenteRepository.Find(orcamentoId));

                foreach (OrcamentoIntumescenteModel orcamentoIntumescente in listOrcamentoIntumescente)
                {
                    var pessoaId = MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_ORCAMENTO", $"ORCAMENTO_ID = {orcamentoIntumescente.ORCAMENTO_ID}");
                    orcamentoIntumescente.CLIENTE_ORCAMENTO = PessoaService.GetComParametro(new PessoaQO(int.Parse(pessoaId), "")).ToArray()[0];

                    orcamentoIntumescente.LIST_ITENS_ORCAMENTO_INTUMESCENTE = ItensOrcamentoIntumescenteService.GetComParametro(new ItensOrcamentoQO(0, orcamentoIntumescente.ORCAMENTO_ID)).ToList();
                    orcamentoIntumescente.LIST_MAO_OBRA_ORCAMENTO = MaoObraOrcamentoService.Get(orcamentoIntumescente.ORCAMENTO_ID).ToList();
                    orcamentoIntumescente.LIST_CUSTO_ORCAMENTO = CustoOrcamentoService.GetComParametro(new CustoOrcamentoQO(0, orcamentoIntumescente.ORCAMENTO_ID)).ToList();
                    orcamentoIntumescente.LIST_EQUIPAMENTO_ORCAMENTO = EquipamentoOrcamentoService.GetComParametro(new EquipamentoOrcamentoQO(0, orcamentoIntumescente.ORCAMENTO_ID)).ToList();
                    orcamentoIntumescente.TOTAIS_ORCAMENTO = TotaisOrcamentoService.GetComParametro(new TotaisOrcamentoQO(0, orcamentoIntumescente.ORCAMENTO_ID)).ToArray()[0];
                }

                return listOrcamentoIntumescente;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public OrcamentoIntumescenteModel Post(OrcamentoIntumescenteModel orcamentoIntumescente)
        {
            try
            {
                var where = $"PESSOA_ID = {orcamentoIntumescente.CLIENTE_ORCAMENTO.PESSOA_ID}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_PESSOA", where)))
                {
                    throw new Exception();
                }

                orcamentoIntumescente.CLIENTE_ORCAMENTO = PessoaService.GetComParametro(new PessoaQO(orcamentoIntumescente.CLIENTE_ORCAMENTO.PESSOA_ID, "")).ToArray()[0];

                return OrcamentoIntumescenteRepository.Create(orcamentoIntumescente);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Put(int orcamentoId, OrcamentoIntumescenteModel orcamentoIntumescente)
        {
            try
            {
                var where = $"ORCAMENTO_ID = {orcamentoId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_ORCAMENTO", where)))
                {
                    throw new Exception();
                }

                var orcamentoIntumescenteDB = Get(orcamentoId).ToArray()[0];

                if (orcamentoIntumescente.CLIENTE_ORCAMENTO.PESSOA_ID != orcamentoIntumescenteDB.CLIENTE_ORCAMENTO.PESSOA_ID)
                {
                    orcamentoIntumescente.CLIENTE_ORCAMENTO = PessoaService.GetComParametro(new PessoaQO(orcamentoIntumescente.CLIENTE_ORCAMENTO.PESSOA_ID, "")).ToArray()[0];
                }

                OrcamentoIntumescenteRepository.Update(orcamentoId, orcamentoIntumescente);
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

                ItensOrcamentoIntumescenteService.DeleteComParametro(new ItensOrcamentoQO(0, orcamentoId));

                OrcamentoIntumescenteRepository.Delete(orcamentoId);

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
