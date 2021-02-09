using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class MaoObraOrcamentoService
    {

        private IMaoObraOrcamentoRepository MaoObraOrcamentoRepository;
        private MetodosGenericosService MetodosGenericosService;
        private FuncionarioService FuncionarioService;
        private CustosMaoObraService CustosMaoObraService;

        public MaoObraOrcamentoService(IMaoObraOrcamentoRepository maoObraOrcamentoRepository, MetodosGenericosService metodosGenericosService,
            FuncionarioService funcionarioService, CustosMaoObraService custosMaoObraService)
        {
            this.MaoObraOrcamentoRepository = maoObraOrcamentoRepository;
            this.MetodosGenericosService = metodosGenericosService;
            this.FuncionarioService = funcionarioService;
            this.CustosMaoObraService = custosMaoObraService;
        }

        public IEnumerable<MaoObraOrcamentoModel> Get()
        {
            try
            {
                var listMaoObraOrcamento = MaoObraOrcamentoRepository.List();

                foreach(MaoObraOrcamentoModel maoObraOrcamento in listMaoObraOrcamento)
                {
                    var pessoaId = MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_MAO_OBRA_ORCAMENTO", $"MAO_OBRA_ORCAMENTO_ID = {maoObraOrcamento.MAO_OBRA_ORCAMENTO_ID}");

                    maoObraOrcamento.FUNCIONARIO = FuncionarioService.GetComParametro(new FuncionarioQO(int.Parse(pessoaId), "")).ToArray()[0];

                    maoObraOrcamento.LIST_CUSTO = CustosMaoObraService.Get(maoObraOrcamento.MAO_OBRA_ORCAMENTO_ID).ToList();
                }

                return listMaoObraOrcamento;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<MaoObraOrcamentoModel> Get(int orcamentoId)
        {
            try
            {
                var where = $"ORCAMENTO_ID = {orcamentoId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_ORCAMENTO", where)))
                {
                    throw new Exception();
                }

                var listMaoObraOrcamento = MaoObraOrcamentoRepository.ListPorOrcamentoId(orcamentoId);

                foreach (MaoObraOrcamentoModel maoObraOrcamento in listMaoObraOrcamento)
                {
                    var pessoaId = MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_MAO_OBRA_ORCAMENTO", $"MAO_OBRA_ORCAMENTO_ID = {maoObraOrcamento.MAO_OBRA_ORCAMENTO_ID}");

                    maoObraOrcamento.FUNCIONARIO = FuncionarioService.GetComParametro(new FuncionarioQO(int.Parse(pessoaId), "")).ToArray()[0];

                    maoObraOrcamento.LIST_CUSTO = CustosMaoObraService.Get(maoObraOrcamento.MAO_OBRA_ORCAMENTO_ID).ToList();
                }

                return listMaoObraOrcamento;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<MaoObraOrcamentoModel> Get(int maoObraOrcamentoId, int orcamentoId)
        {
            try
            {
                var where = $"ORCAMENTO_ID = {orcamentoId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_ORCAMENTO", where)))
                {
                    throw new Exception();
                }

                List<MaoObraOrcamentoModel> listMaoObraOrcamento = new List<MaoObraOrcamentoModel>();

                listMaoObraOrcamento.Add(MaoObraOrcamentoRepository.Find(maoObraOrcamentoId, orcamentoId));

                foreach (MaoObraOrcamentoModel maoObraOrcamento in listMaoObraOrcamento)
                {
                    var pessoaId = MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_MAO_OBRA_ORCAMENTO", $"MAO_OBRA_ORCAMENTO_ID = {maoObraOrcamento.MAO_OBRA_ORCAMENTO_ID}");

                    maoObraOrcamento.FUNCIONARIO = FuncionarioService.GetComParametro(new FuncionarioQO(int.Parse(pessoaId), "")).ToArray()[0];

                    maoObraOrcamento.LIST_CUSTO = CustosMaoObraService.Get(maoObraOrcamento.MAO_OBRA_ORCAMENTO_ID).ToList();
                }

                return listMaoObraOrcamento;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public MaoObraOrcamentoModel Post(MaoObraOrcamentoModel maoObraOrcamento)
        {
            try
            {
                var where = $"ORCAMENTO_ID = {maoObraOrcamento.ORCAMENTO_ID}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_ORCAMENTO", where)))
                {
                    throw new Exception();
                }

                where = $"PESSOA_ID = {maoObraOrcamento.FUNCIONARIO.PESSOA_ID}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_PESSOA", where)))
                {
                    throw new Exception();
                }

                maoObraOrcamento.FUNCIONARIO = FuncionarioService.GetComParametro(new FuncionarioQO(maoObraOrcamento.FUNCIONARIO.PESSOA_ID, "")).ToArray()[0];

                MetodosGenericosService.StartTransactionCommitRollbackOrcamentaria(MetodosGenericosEnum.START);

                var retorno = MaoObraOrcamentoRepository.Create(maoObraOrcamento);

                retorno.FUNCIONARIO = maoObraOrcamento.FUNCIONARIO;

                retorno.LIST_CUSTO = maoObraOrcamento.LIST_CUSTO;

                MetodosGenericosService.StartTransactionCommitRollbackOrcamentaria(MetodosGenericosEnum.COMMIT);

                return retorno;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Put(int maoObraOrcamentoId, MaoObraOrcamentoModel maoObraOrcamento)
        {
            try
            {
                var where = $"MAO_OBRA_ORCAMENTO_ID = {maoObraOrcamento.MAO_OBRA_ORCAMENTO_ID}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("MAO_OBRA_ORCAMENTO_ID", "T_ORCA_MAO_OBRA_ORCAMENTO", where)))
                {
                    throw new Exception();
                }

                maoObraOrcamento.FUNCIONARIO = FuncionarioService.GetComParametro(new FuncionarioQO(maoObraOrcamento.FUNCIONARIO.PESSOA_ID, "")).FirstOrDefault();

                MaoObraOrcamentoRepository.Update(maoObraOrcamentoId, maoObraOrcamento);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(int maoObraOrcamentoId, int orcamentoId)
        {
            try
            {
                var where = $"MAO_OBRA_ORCAMENTO_ID = {maoObraOrcamentoId} AND ORCAMENTO_ID = {orcamentoId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("MAO_OBRA_ORCAMENTO_ID", "T_ORCA_OBRA", where)))
                {
                    throw new Exception();
                }

                MetodosGenericosService.StartTransactionCommitRollbackOrcamentaria(MetodosGenericosEnum.START);

                CustosMaoObraService.Delete(maoObraOrcamentoId);

                MaoObraOrcamentoRepository.Delete(maoObraOrcamentoId, orcamentoId);

                MetodosGenericosService.StartTransactionCommitRollbackOrcamentaria(MetodosGenericosEnum.COMMIT);
            }
            catch (Exception)
            {
                MetodosGenericosService.StartTransactionCommitRollbackOrcamentaria(MetodosGenericosEnum.ROLLBACK);
                throw;
            }
        }

        public void Delete(int orcamentoId)
        {
            try
            {
                var where = $"ORCAMENTO_ID = {orcamentoId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_ORCAMENTO", where)))
                {
                    throw new Exception();
                }

                MetodosGenericosService.StartTransactionCommitRollbackOrcamentaria(MetodosGenericosEnum.START);

                var listMaoObraOrcamento = MaoObraOrcamentoRepository.ListPorOrcamentoId(orcamentoId);

                foreach (MaoObraOrcamentoModel maoObraOrcamento in listMaoObraOrcamento)
                {
                    CustosMaoObraService.Delete(maoObraOrcamento.MAO_OBRA_ORCAMENTO_ID);
                }

                MaoObraOrcamentoRepository.DeletePorOrcamentoId(orcamentoId);

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
