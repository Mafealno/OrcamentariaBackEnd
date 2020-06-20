using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class EquipamentoOrcamentoService
    {

        private IEquipamentoOrcamentoRepository EquipamentoOrcamentoRepository;
        private MetodosGenericosService MetodosGenericosService;
        private EquipamentoService EquipamentoService;

        public EquipamentoOrcamentoService(IEquipamentoOrcamentoRepository equipamentoOrcamentoRepository, MetodosGenericosService metodosGenericosService,
            EquipamentoService equipamentoService)
        {
            this.EquipamentoOrcamentoRepository = equipamentoOrcamentoRepository;
            this.MetodosGenericosService = metodosGenericosService;
            this.EquipamentoService = equipamentoService;
        }

        public IEnumerable<EquipamentoOrcamentoModel> Get()
        {
            try
            {
                var listEquipamentoOrcamento = EquipamentoOrcamentoRepository.List();

                foreach(EquipamentoOrcamentoModel equipamentoOrcamento in listEquipamentoOrcamento)
                {
                    var equipamentoId = MetodosGenericosService.DlookupOrcamentaria("EQUIPAMENTO_ID", "T_ORCA_EQUIPAMENTO_ORCAMENTO", $"EQUIPAMENTO_ORCAMENTO_ID = {equipamentoOrcamento.EQUIPAMENTO_ORCAMENTO_ID}");

                    equipamentoOrcamento.EQUIPAMENTO = EquipamentoService.GetComParametro(new EquipamentoQO(int.Parse(equipamentoId), "")).ToArray()[0] ;
                }

                return listEquipamentoOrcamento;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<EquipamentoOrcamentoModel> GetComParametro(EquipamentoOrcamentoQO equipamentoOrcamento)
        {
            try
            {
                List<EquipamentoOrcamentoModel> listEquipamentoOrcamento;

                if (equipamentoOrcamento.OrcamentoId != 0)
                {
                    var where = $"ORCAMENTO_ID = {equipamentoOrcamento.OrcamentoId}";
                    if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_ORCAMENTO", where)))
                    {
                        throw new Exception();
                    }

                    listEquipamentoOrcamento = EquipamentoOrcamentoRepository.ListPorOrcamentoId(equipamentoOrcamento.OrcamentoId).ToList();
                }
                else
                {
                    listEquipamentoOrcamento = new List<EquipamentoOrcamentoModel>();

                    listEquipamentoOrcamento.Add(EquipamentoOrcamentoRepository.Find(equipamentoOrcamento.EquipamentoOrcamentoId));
                }

                foreach (EquipamentoOrcamentoModel equipamentoOrcamentoModel in listEquipamentoOrcamento)
                {
                    var equipamentoId = MetodosGenericosService.DlookupOrcamentaria("EQUIPAMENTO_ID", "T_ORCA_EQUIPAMENTO_ORCAMENTO", $"EQUIPAMENTO_ORCAMENTO_ID = {equipamentoOrcamentoModel.EQUIPAMENTO_ORCAMENTO_ID}");

                    equipamentoOrcamentoModel.EQUIPAMENTO = EquipamentoService.GetComParametro(new EquipamentoQO(int.Parse(equipamentoId), "")).ToArray()[0];
                }

                return listEquipamentoOrcamento;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public EquipamentoOrcamentoModel Post(EquipamentoOrcamentoModel equipamentoOrcamento)
        {
            try
            {
                var where = $"ORCAMENTO_ID = {equipamentoOrcamento.ORCAMENTO_ID}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_ORCAMENTO", where)))
                {
                    throw new Exception();
                }

                where = $"EQUIPAMENTO_ID = {equipamentoOrcamento.EQUIPAMENTO.EQUIPAMENTO_ID}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("EQUIPAMENTO_ID", "T_ORCA_EQUIPAMENTO", where)))
                {
                    throw new Exception();
                }

                if (equipamentoOrcamento.VALOR_UNITARIO_EQUIPAMENTO < 0 || equipamentoOrcamento.QTDE_EQUIPAMENTO < 0)
                {
                    throw new Exception();
                }

                equipamentoOrcamento.EQUIPAMENTO = EquipamentoService.GetComParametro(new EquipamentoQO(equipamentoOrcamento.EQUIPAMENTO.EQUIPAMENTO_ID, "")).ToArray()[0];

                return EquipamentoOrcamentoRepository.Create(equipamentoOrcamento);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Put(int equipamentoOrcamentoId, EquipamentoOrcamentoModel equipamentoOrcamento)
        {
            try
            {
                var where = $"EQUIPAMENTO_ORCAMENTO_ID = {equipamentoOrcamentoId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("EQUIPAMENTO_ORCAMENTO_ID", "T_ORCA_EQUIPAMENTO_ORCAMENTO", where)))
                {
                    throw new Exception();
                }

                var equipamentoOrcamentoDB = GetComParametro(new EquipamentoOrcamentoQO(equipamentoOrcamentoId, 0)).ToArray()[0];

                if(equipamentoOrcamento.EQUIPAMENTO.EQUIPAMENTO_ID != equipamentoOrcamentoDB.EQUIPAMENTO.EQUIPAMENTO_ID)
                {
                    equipamentoOrcamento.EQUIPAMENTO = EquipamentoService.GetComParametro(new EquipamentoQO(equipamentoOrcamento.EQUIPAMENTO.EQUIPAMENTO_ID, "")).ToArray()[0];
                }

                if(equipamentoOrcamento.VALOR_UNITARIO_EQUIPAMENTO < 0 || equipamentoOrcamento.QTDE_EQUIPAMENTO < 0)
                {
                    throw new Exception();
                }

                EquipamentoOrcamentoRepository.Update(equipamentoOrcamentoId, equipamentoOrcamento);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteComParamenro(EquipamentoOrcamentoQO equipamentoOrcamento)
        {
            try
            {
                if (equipamentoOrcamento.OrcamentoId != 0)
                {
                    var where = $"ORCAMENTO_ID = {equipamentoOrcamento.OrcamentoId}";
                    if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_ORCAMENTO", where)))
                    {
                        throw new Exception();
                    }

                    EquipamentoOrcamentoRepository.DeletePorOrcamentoId(equipamentoOrcamento.OrcamentoId);
                }
                else
                {
                    var where = $"EQUIPAMENTO_ORCAMENTO_ID = {equipamentoOrcamento.EquipamentoOrcamentoId}";
                    if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("EQUIPAMENTO_ORCAMENTO_ID", "T_ORCA_EQUIPAMENTO_ORCAMENTO", where)))
                    {
                        throw new Exception();
                    }

                    EquipamentoOrcamentoRepository.Delete(equipamentoOrcamento.EquipamentoOrcamentoId);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
