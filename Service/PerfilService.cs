using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class PerfilService
    {
        private IPerfilRepository PerfilRepository;
        private MetodosGenericosService MetodosGenericosService;

        public PerfilService(IPerfilRepository perfilRepository, MetodosGenericosService metodosGenericosService)
        {
            this.PerfilRepository = perfilRepository;
            this.MetodosGenericosService = metodosGenericosService;
        }

        public IEnumerable<PerfilModel> Get()
        {
            try
            {
                return PerfilRepository.List();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<PerfilModel> GetComParametro(PerfilQO perfil)
        {
            try
            {
                if (!string.IsNullOrEmpty(perfil.NomePerfil))
                {
                    return PerfilRepository.ListPorNomePerfil(perfil.NomePerfil);
                }
                else
                {
                    List<PerfilModel> listPerfil = new List<PerfilModel>();

                    listPerfil.Add(PerfilRepository.Find(perfil.PerfilId));

                    return listPerfil;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public PerfilModel Post(PerfilModel perfil)
        {
            try
            {
                return PerfilRepository.Create(perfil);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Put(int perfilId, PerfilModel perfil)
        {
            try
            {
                var where = $"PERFIL_ID = {perfilId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("PERFIL_ID", "T_ORCA_PERFIL", where)))
                {
                    throw new Exception();
                }

                PerfilRepository.Update(perfilId, perfil);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int perfilId)
        {
            try
            {
                var where = $"PERFIL_ID = {perfilId}";
                if (string.IsNullOrEmpty(MetodosGenericosService.DlookupOrcamentaria("PERFIL_ID", "T_ORCA_PERFIL", where)))
                {
                    throw new Exception();
                }

                PerfilRepository.Delete(perfilId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
