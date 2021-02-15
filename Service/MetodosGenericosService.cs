using Microsoft.Extensions.Configuration;
using OrcamentariaBackEnd.Database;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace OrcamentariaBackEnd
{
    public class MetodosGenericosService
    {
        private IMetodosGenericosRepository MetodosGenericosRepository;
        public IConfiguration Configuration { get; }

        public MetodosGenericosService(IMetodosGenericosRepository metodosGenericosRepository, IConfiguration configuration)
        {
            this.MetodosGenericosRepository = metodosGenericosRepository;
            this.Configuration = configuration;
        }

        public string DlookupOrcamentaria(string campoBuscado, string tabela, string where)
        {
            try
            {
                return MetodosGenericosRepository.DlookupOrcamentaria(campoBuscado, tabela, where);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string RetornaConexao()
        {
            try
            {
                return Configuration.GetConnectionString("DefaultConnection");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void StartTransactionCommitRollbackOrcamentaria(MetodosGenericosEnum metodosGenericos)
        {
            try
            {
                MetodosGenericosRepository.StartTransactionCommitRollback(metodosGenericos);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static T IsNullOrcamentaria<T>(T obj, T padrao)
        {
            try
            {
                if (obj == null || (object)obj == DBNull.Value)
                {
                    return padrao;
                }
                if (obj is string)
                {
                    if (String.IsNullOrEmpty(obj.ToString()))
                    {
                        return padrao;
                    }
                }
                return obj;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public static T CopiarPropriedadesObj<T>(T objOrigem, T objPreencher)
        {
            try
            {
                foreach (PropertyInfo propriedadesObjDestino in objPreencher.GetType().GetProperties())
                {
                    foreach (PropertyInfo propriedadesObjOrigem in objOrigem.GetType().GetProperties())
                    {
                        if (propriedadesObjDestino.Name == propriedadesObjOrigem.Name)
                        {
                            if (!propriedadesObjOrigem.PropertyType.FullName.ToString().StartsWith("System"))
                            {
                                if (propriedadesObjDestino.GetValue(objPreencher) == null)
                                {
                                    var objApoio = Activator.CreateInstance(propriedadesObjOrigem.GetValue(objOrigem).GetType());

                                    propriedadesObjDestino.SetValue(objPreencher, CopiarPropriedadesObj(propriedadesObjOrigem.GetValue(objOrigem), objApoio));
                                }
                                else
                                {
                                    propriedadesObjDestino.SetValue(objPreencher, CopiarPropriedadesObj(propriedadesObjOrigem.GetValue(objOrigem), propriedadesObjOrigem.GetValue(objPreencher)));
                                }
                            }

                            if (propriedadesObjDestino.PropertyType.Name.StartsWith("String"))
                            {
                                propriedadesObjDestino.SetValue(objPreencher, IsNullOrcamentaria(propriedadesObjOrigem.GetValue(objOrigem), ""));
                            }
                            else if (propriedadesObjDestino.PropertyType.Name.StartsWith("Boolean"))
                            {
                                propriedadesObjDestino.SetValue(objPreencher, IsNullOrcamentaria(Convert.ToBoolean(Convert.ToInt32(propriedadesObjOrigem.GetValue(objOrigem))), false));
                            }
                            else if (propriedadesObjDestino.PropertyType.Name.StartsWith("List`1"))
                            {
                                propriedadesObjDestino.SetValue(objPreencher, IsNullOrcamentaria(propriedadesObjDestino.GetValue(objOrigem), Activator.CreateInstance(propriedadesObjDestino.PropertyType)));
                            }
                            else
                            {
                                propriedadesObjDestino.SetValue(objPreencher, IsNullOrcamentaria(propriedadesObjOrigem.GetValue(objOrigem), 0));
                            }
                            break;
                        }
                    }
                }
                return objPreencher;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public double RetornarFator(string tipo, int diasTrabalhado)
        {
            var fator = 1;
            switch (tipo)
            {
                case "UNICO":
                    fator = 1;
                    break;
                case "ANUAL":
                    fator = diasTrabalhado / 364;
                    break;
                case "MENSAL":
                    fator = diasTrabalhado / 30;
                    break;
                case "SEMANAL":
                    fator = diasTrabalhado / 7;
                    break;
                case "DIARIO":
                    fator = diasTrabalhado;
                    break;
                default:
                    fator = 1;
                    break;
            }

            return Math.Round(fator + 0.4);
        }
    }
}
