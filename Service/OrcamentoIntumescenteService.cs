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
        private OrcamentoService OrcamentoService;
        private ItensOrcamentoIntumescenteService ItensOrcamentoIntumescenteService;
        private MaoObraOrcamentoService MaoObraOrcamentoService;
        private EquipamentoOrcamentoService EquipamentoOrcamentoService;
        private MaterialOrcamentoService MaterialOrcamentoService;
        private CustoOrcamentoService CustoOrcamentoService;
        private TotaisOrcamentoRepository TotaisOrcamentoRepository;

        public OrcamentoIntumescenteService(IOrcamentoIntumescenteRepository orcamentoIntumescenteRepository, MetodosGenericosService metodosGenericosService,
            PessoaService pessoaService, OrcamentoService orcamentoService, ItensOrcamentoIntumescenteService itensOrcamentoIntumescenteService,
            MaoObraOrcamentoService maoObraOrcamentoService, EquipamentoOrcamentoService equipamentoOrcamentoService, MaterialOrcamentoService materialOrcamentoService,
        CustoOrcamentoService custoOrcamentoService, TotaisOrcamentoRepository totaisOrcamentoRepository)
        {
            this.OrcamentoIntumescenteRepository = orcamentoIntumescenteRepository;
            this.MetodosGenericosService = metodosGenericosService;
            this.PessoaService = pessoaService;
            this.OrcamentoService = orcamentoService;
            this.ItensOrcamentoIntumescenteService = itensOrcamentoIntumescenteService;
            this.MaoObraOrcamentoService = maoObraOrcamentoService;
            this.EquipamentoOrcamentoService = equipamentoOrcamentoService;
            this.MaterialOrcamentoService = materialOrcamentoService;
            this.CustoOrcamentoService = custoOrcamentoService;
            this.TotaisOrcamentoRepository = totaisOrcamentoRepository;
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
                    orcamentoIntumescente.LIST_MATERIAL_ORCAMENTO = MaterialOrcamentoService.GetComParametro(new MaterialOrcamentoQO(0, orcamentoIntumescente.ORCAMENTO_ID)).ToList();
                    orcamentoIntumescente.TOTAIS_ORCAMENTO = TotaisOrcamentoRepository.FindPorOrcamentoId(orcamentoIntumescente.ORCAMENTO_ID);
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
                    orcamentoIntumescente.LIST_MATERIAL_ORCAMENTO = MaterialOrcamentoService.GetComParametro(new MaterialOrcamentoQO(0, orcamentoIntumescente.ORCAMENTO_ID)).ToList();
                    orcamentoIntumescente.TOTAIS_ORCAMENTO = TotaisOrcamentoRepository.FindPorOrcamentoId(orcamentoIntumescente.ORCAMENTO_ID);
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

                var orcamentoCadastrado = OrcamentoService.Post(orcamentoIntumescente);

                orcamentoIntumescente.ORCAMENTO_ID = orcamentoCadastrado.ORCAMENTO_ID;

                orcamentoIntumescente.CLIENTE_ORCAMENTO = orcamentoCadastrado.CLIENTE_ORCAMENTO;

                OrcamentoIntumescenteRepository.Create(orcamentoIntumescente);

                return orcamentoIntumescente;  
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

                ItensOrcamentoIntumescenteService.DeleteComParametro(new ItensOrcamentoQO(0, orcamentoId));

                OrcamentoIntumescenteRepository.Delete(orcamentoId);

                OrcamentoService.Delete(orcamentoId);

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
