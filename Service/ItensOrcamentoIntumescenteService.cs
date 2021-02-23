using KellermanSoftware.CompareNetObjects;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class ItensOrcamentoIntumescenteService
    {
        private IItensOrcamentoIntumescenteRepository ItensOrcamentoIntumescenteRepository;
        private MetodosGenericosService MetodosGenericosService;
        private ItensOrcamentoService ItensOrcamentoService;
        private MaterialService MaterialService;
        private PerfilService PerfilService;
        private CartaCoberturaService CartaCoberturaService;

        public ItensOrcamentoIntumescenteService(IItensOrcamentoIntumescenteRepository itensOrcamentoIntumescenteRepository, MetodosGenericosService metodosGenericosService,
            ItensOrcamentoService itensOrcamentoService, MaterialService materialService, PerfilService perfilService, CartaCoberturaService cartaCoberturaService)
        {
            this.ItensOrcamentoIntumescenteRepository = itensOrcamentoIntumescenteRepository;
            this.MetodosGenericosService = metodosGenericosService;
            this.ItensOrcamentoService = itensOrcamentoService;
            this.MaterialService = materialService;
            this.PerfilService = perfilService;
            this.CartaCoberturaService = cartaCoberturaService;
        }

        public IEnumerable<ItensOrcamentoIntumescenteModel> Get()
        {
            try
            {
                var listItensOrcamentoIntumescente = ItensOrcamentoIntumescenteRepository.List();

                foreach (ItensOrcamentoIntumescenteModel itensOrcamentoIntumescente in listItensOrcamentoIntumescente)
                {
                    var perfilId = MetodosGenericosService.DlookupOrcamentaria("PERFIL_ID", "T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE", $"ITENS_ORCAMENTO_ID = {itensOrcamentoIntumescente.ITENS_ORCAMENTO_ID}");

                    if(int.Parse(perfilId) != 0)
                    {
                        itensOrcamentoIntumescente.PERFIL = PerfilService.GetComParametro(new PerfilQO(int.Parse(perfilId), "")).ToArray()[0];
                    }

                    var cartaCoberturId = MetodosGenericosService.DlookupOrcamentaria("CARTA_COBERTURA_ID", "T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE", $"ITENS_ORCAMENTO_ID = {itensOrcamentoIntumescente.ITENS_ORCAMENTO_ID}");

                    if (int.Parse(cartaCoberturId) != 0)
                    {
                        itensOrcamentoIntumescente.CARTA_COBERTURA = CartaCoberturaService.GetComParametro(new CartaCoberturaQO(int.Parse(cartaCoberturId), 0, 0, "")).ToArray()[0];
                    }

                }

                return listItensOrcamentoIntumescente;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ItensOrcamentoIntumescenteModel> GetComParametro(ItensOrcamentoQO itensOrcamentoIntumescente)
        {
            try
            {
                List<ItensOrcamentoIntumescenteModel> listItensOrcamentoIntumescente;

                if (itensOrcamentoIntumescente.OrcamentoId != 0)
                {
                    listItensOrcamentoIntumescente = ItensOrcamentoIntumescenteRepository.ListPorOrcamentoId(itensOrcamentoIntumescente.OrcamentoId).ToList();
                }
                else
                {
                    listItensOrcamentoIntumescente = new List<ItensOrcamentoIntumescenteModel>();

                    listItensOrcamentoIntumescente.Add(ItensOrcamentoIntumescenteRepository.Find(itensOrcamentoIntumescente.ItensOrcamentoId));
                }

                foreach (ItensOrcamentoIntumescenteModel itensOrcamentoIntumescenteModel in listItensOrcamentoIntumescente)
                {

                    var perfilId = MetodosGenericosService.DlookupOrcamentaria("PERFIL_ID", "T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE", $"ITENS_ORCAMENTO_ID = {itensOrcamentoIntumescenteModel.ITENS_ORCAMENTO_ID}");

                    if (int.Parse(perfilId) != 0)
                    {
                        itensOrcamentoIntumescenteModel.PERFIL = PerfilService.GetComParametro(new PerfilQO(int.Parse(perfilId), "")).ToArray()[0];
                    }

                    var cartaCoberturId = MetodosGenericosService.DlookupOrcamentaria("CARTA_COBERTURA_ID", "T_ORCA_ITENS_ORCAMENTO_INTUMESCENTE", $"ITENS_ORCAMENTO_ID = {itensOrcamentoIntumescenteModel.ITENS_ORCAMENTO_ID}");

                    if (int.Parse(cartaCoberturId) != 0)
                    {
                        itensOrcamentoIntumescenteModel.CARTA_COBERTURA = CartaCoberturaService.GetComParametro(new CartaCoberturaQO(int.Parse(cartaCoberturId), 0, 0, "")).ToArray()[0];
                    }
                }

                return listItensOrcamentoIntumescente;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ItensOrcamentoIntumescenteModel> GetValoresCalculados(List<ItensOrcamentoIntumescenteModel> listItensOrcamentoIntumescente, int materialId, string tempoResitenciaFogo)
        {
            try
            {
                foreach (ItensOrcamentoIntumescenteModel itemIntumescente in listItensOrcamentoIntumescente)
                {
                    var cartaCobertura = CartaCoberturaService.Get(materialId, itemIntumescente.REFERENCIA, itemIntumescente.VALOR_HP_A.ToString(), tempoResitenciaFogo);

                    if (cartaCobertura.CARTA_COBERTURA_ID != 0)
                    {
                        if (cartaCobertura.LIST_ITENS_CARTA_COBERTURA[0] != null)
                        {
                            var valoresCalculados = CalcularValoresIntumescente(itemIntumescente, itemIntumescente.PERFIL, cartaCobertura.LIST_ITENS_CARTA_COBERTURA.FirstOrDefault().VALOR_ESPESSURA);

                            itemIntumescente.VALOR_HP = valoresCalculados.Hp;
                            itemIntumescente.VALOR_WD = valoresCalculados.WD;
                            itemIntumescente.VALOR_HP_A = valoresCalculados.HpA;
                            itemIntumescente.AREA = valoresCalculados.Area;
                            itemIntumescente.QTDE_LITROS = valoresCalculados.TotalLitros;
                            itemIntumescente.VALOR_ESPESSURA = cartaCobertura.LIST_ITENS_CARTA_COBERTURA.FirstOrDefault().VALOR_ESPESSURA;
                        }
                        else
                        {
                            itemIntumescente.VALOR_HP = 0;
                            itemIntumescente.VALOR_WD = 0;
                            itemIntumescente.VALOR_HP_A = 0;
                            itemIntumescente.AREA = 0;
                            itemIntumescente.QTDE_LITROS = 0;
                            itemIntumescente.VALOR_ESPESSURA = 0;
                        }
                    }
                    else
                    {
                        itemIntumescente.VALOR_HP = 0;
                        itemIntumescente.VALOR_WD = 0;
                        itemIntumescente.VALOR_HP_A = 0;
                        itemIntumescente.AREA = 0;
                        itemIntumescente.QTDE_LITROS = 0;
                        itemIntumescente.VALOR_ESPESSURA = 0;
                    }
                }

                return listItensOrcamentoIntumescente;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ItensOrcamentoIntumescenteModel Post(ItensOrcamentoIntumescenteModel itensOrcamentoIntumescente)
        {
            try
            {
                var where = $"ORCAMENTO_ID = {itensOrcamentoIntumescente.ORCAMENTO_ID}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_ORCAMENTO", where)))
                {
                    throw new Exception();
                }

                if (itensOrcamentoIntumescente.NUMERO_FACES < 0 || itensOrcamentoIntumescente.QTDE < 0)
                {
                    throw new Exception();
                }

                if(itensOrcamentoIntumescente.PERFIL.PERFIL_ID != 0)
                {
                    itensOrcamentoIntumescente.PERFIL = PerfilService.GetComParametro(new PerfilQO(itensOrcamentoIntumescente.PERFIL.PERFIL_ID, "")).ToArray()[0];
                }

                if(itensOrcamentoIntumescente.CARTA_COBERTURA.CARTA_COBERTURA_ID != 0)
                {
                    itensOrcamentoIntumescente.CARTA_COBERTURA = CartaCoberturaService.GetComParametro(new CartaCoberturaQO(itensOrcamentoIntumescente.CARTA_COBERTURA.CARTA_COBERTURA_ID, 0, 0, "")).ToArray()[0];
                }

                MetodosGenericosService.StartTransactionCommitRollbackOrcamentaria(MetodosGenericosEnum.START);

                var itensOrcamento = MetodosGenericosService.CopiarPropriedadesObj(itensOrcamentoIntumescente, new ItensOrcamentoModel());

                itensOrcamento = ItensOrcamentoService.Post(itensOrcamento);

                itensOrcamentoIntumescente.ITENS_ORCAMENTO_ID = itensOrcamento.ITENS_ORCAMENTO_ID;

                var itensOrcamentoIntumescenteCadastrado = ItensOrcamentoIntumescenteRepository.Create(itensOrcamentoIntumescente);

                itensOrcamentoIntumescenteCadastrado.PERFIL = itensOrcamentoIntumescente.PERFIL;
                itensOrcamentoIntumescenteCadastrado.CARTA_COBERTURA = itensOrcamentoIntumescente.CARTA_COBERTURA;

                MetodosGenericosService.StartTransactionCommitRollbackOrcamentaria(MetodosGenericosEnum.COMMIT);

                return itensOrcamentoIntumescenteCadastrado;

            }
            catch (Exception)
            {
                MetodosGenericosService.StartTransactionCommitRollbackOrcamentaria(MetodosGenericosEnum.ROLLBACK);
                throw;
            }
        }

        public void Put(int itensOrcamentoId, ItensOrcamentoIntumescenteModel itensOrcamentoIntumescente)
        {
            try
            {
                var where = $"ITENS_ORCAMENTO_ID = {itensOrcamentoId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ITENS_ORCAMENTO_ID", "T_ORCA_ITENS_ORCAMENTO_GERAL", where)))
                {
                    throw new Exception();
                }

                var itensOrcamento = MetodosGenericosService.CopiarPropriedadesObj(itensOrcamentoIntumescente, new ItensOrcamentoModel());

                var itensOrcamentoDB = ItensOrcamentoService.GetComParametro(new ItensOrcamentoQO(itensOrcamentoId, 0)).ToArray()[0];

                ComparisonResult resultando = new CompareLogic().Compare(itensOrcamento, itensOrcamentoDB);

                if (!resultando.AreEqual)
                {
                    var res = resultando.Differences;

                    ItensOrcamentoService.Put(itensOrcamentoId, itensOrcamento);
                }

                if (itensOrcamentoIntumescente.VALOR_COMPRIMENTO < 0 || itensOrcamentoIntumescente.AREA < 0)
                {
                    throw new Exception();
                }

                ItensOrcamentoIntumescenteRepository.Update(itensOrcamentoId, itensOrcamentoIntumescente);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int DeleteComParametro(ItensOrcamentoQO itensOrcamentoIntumescente)
        {
            try
            {
                MetodosGenericosService.StartTransactionCommitRollbackOrcamentaria(MetodosGenericosEnum.START);

                var orcamentoId = 0;

                if (itensOrcamentoIntumescente.OrcamentoId != 0)
                {
                    var where = $"ORCAMENTO_ID = {itensOrcamentoIntumescente.OrcamentoId}";
                    if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_ORCAMENTO", where)))
                    {
                        throw new Exception();
                    }

                    orcamentoId = itensOrcamentoIntumescente.OrcamentoId;

                    ItensOrcamentoIntumescenteRepository.DeletePorOrcamentoId(itensOrcamentoIntumescente.OrcamentoId);
                    ItensOrcamentoService.DeleteComParametro(itensOrcamentoIntumescente);
                }
                else
                {
                    var where = $"ORCAMENTO_ID = {itensOrcamentoIntumescente.OrcamentoId}";
                    if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_ORCAMENTO", where)))
                    {
                        throw new Exception();
                    }

                    orcamentoId = Int32.Parse(MetodosGenericosService.DlookupOrcamentaria("ORCAMENTO_ID", "T_ORCA_ITENS_ORCAMENTO_GERAL", where));

                    ItensOrcamentoIntumescenteRepository.Delete(itensOrcamentoIntumescente.ItensOrcamentoId);
                    ItensOrcamentoService.DeleteComParametro(itensOrcamentoIntumescente);
                }

                MetodosGenericosService.StartTransactionCommitRollbackOrcamentaria(MetodosGenericosEnum.COMMIT);

                return orcamentoId;
            }
            catch (Exception)
            {
                MetodosGenericosService.StartTransactionCommitRollbackOrcamentaria(MetodosGenericosEnum.ROLLBACK);
                throw;
            }
        }

        public dynamic CalcularValoresIntumescente(ItensOrcamentoIntumescenteModel itemItumescente, PerfilModel perfil, double valorEspessura)
        {
            try
            {
                double HpAux = ((2 * perfil.VALOR_D) + (itemItumescente.NUMERO_FACES * perfil.VALOR_BF)) / 1000;
                double WDAux = 39.70008 / (perfil.VALOR_KG_M * 2.2);
                double HpAAux = HpAux / (perfil.VALOR_KG_M / 7850);
                double AreaAux = itemItumescente.VALOR_COMPRIMENTO * itemItumescente.QTDE * HpAux;

                double TotalLitrosAux = 0;
                if (valorEspessura > 0)
                {
                    TotalLitrosAux = 1.4 * valorEspessura * AreaAux;
                }

                return new
                {
                    Hp = float.Parse(HpAux.ToString("N2")),
                    WD = float.Parse(WDAux.ToString("N2")),
                    HpA = float.Parse(HpAAux.ToString("N2")),
                    Area = float.Parse(AreaAux.ToString("N2")),
                    TotalLitros = float.Parse(TotalLitrosAux.ToString("N2"))
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
