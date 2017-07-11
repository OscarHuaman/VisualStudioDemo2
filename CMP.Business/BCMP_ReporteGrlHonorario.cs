/*********************************************************
'* NEGOCIO PARA EL REPORTE GENERAL DE HONORARIOS
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   JUAN LUIS GUERRA MENESES
'* FCH. CREACIÓN : 05/02/2016
**********************************************************/
namespace CMP.Business
{
    using CMP.Entity;
    using ComputerSystems.DataAccess.LibrarySql;
    using MNF.Entity;
    using SGC.Empresarial.Entity;
    using SGC.Empresarial.Useful.Modulo;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BCMP_ReporteGrlHonorario
    {
        private CmpSql objCmpSql;
        private List<ECMP_ReporteGrlHonorario> ListECMP_ReporteGrlHonorario;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ObjECMP_ReporteGrlDocumento"></param>
        /// <returns></returns>
        public List<ECMP_ReporteGrlHonorario> ListReporteGrlHonorario(ECMP_ReporteGrlHonorario ObjECMP_ReporteGrlDocumento)
        {
            try
            {
                objCmpSql = new CmpSql(SGCVariables.ConectionString);
                ListECMP_ReporteGrlHonorario = new List<ECMP_ReporteGrlHonorario>();
                objCmpSql.CommandProcedure("spCMP_GET_ConsultaGralHonorario");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, ObjECMP_ReporteGrlDocumento.Opcion);
                objCmpSql.AddParameter("@Periodo", SqlDbType.Char, ObjECMP_ReporteGrlDocumento.Periodo);
                objCmpSql.AddParameter("@IdCliProveedor", SqlDbType.VarChar, ObjECMP_ReporteGrlDocumento.ObjEMNF_ClienteProveedor.IdCliProveedor);
                objCmpSql.AddParameter("@CodMoneda", SqlDbType.VarChar, ObjECMP_ReporteGrlDocumento.ObjESGC_Moneda.CodMoneda);
                objCmpSql.AddParameter("@IdUsuario", SqlDbType.VarChar, SGCVariables.ObjESGC_Usuario.IdUsuario);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    ListECMP_ReporteGrlHonorario.Add(new ECMP_ReporteGrlHonorario()
                    {
                        FechaEmision = (dt.Rows[x]["FechaEmision"] != DBNull.Value) ? Convert.ToDateTime(dt.Rows[x]["FechaEmision"]) : DateTime.Now,
                        FechaContable = (dt.Rows[x]["FechaContable"] != DBNull.Value) ? Convert.ToDateTime(dt.Rows[x]["FechaContable"]) : DateTime.Now,
                        ObjESGC_Documento = new ESGC_Documento()
                        {
                            Descripcion = (dt.Rows[x]["Documento"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Documento"]) : string.Empty,
                        },
                        SerieNumero = (dt.Rows[x]["SerieNumero"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["SerieNumero"]) : string.Empty,
                        ObjEMNF_ClienteProveedor = new EMNF_ClienteProveedor()
                        {
                            RazonSocial = (dt.Rows[x]["Proveedor"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Proveedor"]) : string.Empty,
                            NroDocIdentidad = (dt.Rows[x]["NroDocIdentidad"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["NroDocIdentidad"]) : string.Empty,
                        },
                        ObjESGC_Moneda = new ESGC_Moneda()
                        {
                            Descripcion = (dt.Rows[x]["Moneda"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Moneda"]) : string.Empty,
                        },
                        TipoCambio = (dt.Rows[x]["TipoCambio"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["TipoCambio"]) : string.Empty,
                        FechaRecepcion = (dt.Rows[x]["FechaRecepcion"] != DBNull.Value) ? Convert.ToDateTime(dt.Rows[x]["FechaRecepcion"]) : DateTime.Now,
                        THonorario = (dt.Rows[x]["TotalHonorario"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["TotalHonorario"]) : 0,
                        TotalNeto = (dt.Rows[x]["Total"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Total"]) : 0,
                        PRetencion = (dt.Rows[x]["Retencion"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Retencion"]) : 0,
                        MRetencion = (dt.Rows[x]["ImpRetencion"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["ImpRetencion"]) : 0,
                        Glosa = (dt.Rows[x]["Glosa"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Glosa"]) : string.Empty,
                        ObjESGC_Estado = new ESGC_Estado()
                        {
                            Estado = (dt.Rows[x]["Estado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Estado"]) : string.Empty,
                        }
                    });
                }
                return ListECMP_ReporteGrlHonorario;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
