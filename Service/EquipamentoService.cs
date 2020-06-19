using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class EquipamentoService
    {
        private IEquipamentoRepository EquipamentoRepository;
        private MetodosGenericosService MetodosGenericosService;
        private PessoaService PessoaService;

        public EquipamentoService(IEquipamentoRepository equipamentoRepository, MetodosGenericosService metodosGenericosService,
            PessoaService pessoaService)
        {
            this.EquipamentoRepository = equipamentoRepository;
            this.MetodosGenericosService = metodosGenericosService;
            this.PessoaService = pessoaService;
        }

        public IEnumerable<EquipamentoModel> Get()
        {
            try
            {
                var listEquipamento = EquipamentoRepository.List();

                foreach (EquipamentoModel equipamento in listEquipamento)
                {
                    var pessoaId = MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_EQUIPAMENTO", $"EQUIPAMENTO_ID = {equipamento.EQUIPAMENTO_ID}");

                    equipamento.FABRICANTE = PessoaService.GetComParametro(new PessoaQO(int.Parse(pessoaId), "")).ToArray()[0];
                }

                return listEquipamento;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<EquipamentoModel> GetComParametro(EquipamentoQO equipamento)
        {
            try
            {
                List<EquipamentoModel> listEquipamento;

                if (!string.IsNullOrEmpty(equipamento.NomeEquipamento))
                {
                    listEquipamento = EquipamentoRepository.ListPorNomeEquipamento(equipamento.NomeEquipamento).ToList();
                }
                else
                {
                    listEquipamento = new List<EquipamentoModel>();

                    listEquipamento.Add(EquipamentoRepository.Find(equipamento.EquipamentoId));

                }

                foreach (EquipamentoModel equipamentoModel in listEquipamento)
                {
                    var pessoaId = MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_EQUIPAMENTO", $"EQUIPAMENTO_ID = {equipamentoModel.EQUIPAMENTO_ID}");

                    equipamentoModel.FABRICANTE = PessoaService.GetComParametro(new PessoaQO(int.Parse(pessoaId), "")).ToArray()[0];
                }

                return listEquipamento;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public EquipamentoModel Post(EquipamentoModel equipamento)
        {
            try
            {
                var where = $"PESSOA_ID = {equipamento.FABRICANTE.PESSOA_ID}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("PESSOA_ID", "T_ORCA_PESSOA", where)))
                {
                    throw new Exception();
                }

                return EquipamentoRepository.Create(equipamento);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Put(int equipamentoId, EquipamentoModel equipamento)
        {
            try
            {
                var where = $"EQUIPAMENTO_ID = {equipamentoId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("EQUIPAMENTO_ID", "T_ORCA_EQUIPAMENTO", where)))
                {
                    throw new Exception();
                }

                EquipamentoRepository.Update(equipamentoId, equipamento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int equipamentoId)
        {
            try
            {
                var where = $"EQUIPAMENTO_ID = {equipamentoId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("EQUIPAMENTO_ID", "T_ORCA_EQUIPAMENTO", where)))
                {
                    throw new Exception();
                }

                EquipamentoRepository.Delete(equipamentoId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
