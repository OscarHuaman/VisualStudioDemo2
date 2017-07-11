/*********************************************************
'* NEGOCIO PARA EL REPORTE RECIBO DE GASTOS INTERNOS
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   JUAN LUIS GUERRA MENESES
'* FCH. CREACIÓN : 09/02/2016
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

    public class BCMP_ReporteGastosInternos
    {
        private CmpSql objCmpSql;
        private List<ECMP_ReporteGastosInternos> ListECMP_ReporteGastosInternos;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ObjECMP_ReporteGastosInternos"></param>
        /// <returns></returns>
        public List<ECMP_ReporteGastosInternos> ListReporteReciboGastosInternos(ECMP_ReporteGastosInternos ObjECMP_ReporteGastosInternos)
        {
            try
            {
                objCmpSql = new CmpSql(SGCVariables.ConectionString);
                ListECMP_ReporteGastosInternos = new List<ECMP_ReporteGastosInternos>();
                objCmpSql.CommandProcedure("spCMP_GET_ConsultaGralGastoInterno");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, ObjECMP_ReporteGastosInternos.Opcion);
                objCmpSql.AddParameter("@Periodo", SqlDbType.Char, ObjECMP_ReporteGastosInternos.Periodo);
                objCmpSql.AddParameter("@IdCliProveedor", SqlDbType.VarChar, ObjECMP_ReporteGastosInternos.ObjEMNF_ClienteProveedor.IdCliProveedor);
                objCmpSql.AddParameter("@CodMoneda", SqlDbType.VarChar, ObjECMP_ReporteGastosInternos.ObjESGC_Moneda.CodMoneda);
                objCmpSql.AddParameter("@IdUsuario", SqlDbType.VarChar, SGCVariables.ObjESGC_Usuario.IdUsuario);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    ListECMP_ReporteGastosInternos.Add(new ECMP_ReporteGastosInternos()
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
                        FechaRecepcion = (dt.Rows[x]["FechaRecepcion"] != DBNull.Value) ? Convert.ToDateTime(dt.Rows[x]["FechaRecepcion"]) : DateTime.Now,
                        
                        Total = (dt.Rows[x]["Total"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Total"]) : 0,
                        Glosa = (dt.Rows[x]["Glosa"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Glosa"]) : string.Empty,
                        ObjESGC_Estado = new ESGC_Estado()
                        {
                            Estado = (dt.Rows[x]["Estado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Estado"]) : string.Empty,
                        }
                    });
                }
                return ListECMP_ReporteGastosInternos;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
