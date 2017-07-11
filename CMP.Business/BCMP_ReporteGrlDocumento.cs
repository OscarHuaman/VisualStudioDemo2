/*********************************************************
'* NEGOCIOS PARA REPORTE GENERAL DE DOCUMENTO
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   JUAN GUERRA MENESES
'* FCH. CREACIÓN : 23/01/2016
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
    public class BCMP_ReporteGrlDocumento
    {
        private CmpSql objCmpSql;
        private List<ECMP_ReporteGrlDocumento> ListECMP_ReporteGrlDocumento;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ObjECMP_ReporteGrlDocumento"></param>
        /// <returns></returns>
        public List<ECMP_ReporteGrlDocumento> ListReporteGrlDocumento(ECMP_ReporteGrlDocumento ObjECMP_ReporteGrlDocumento) 
        {
            try
            {
                objCmpSql = new CmpSql(SGCVariables.ConectionString);
                ListECMP_ReporteGrlDocumento = new List<ECMP_ReporteGrlDocumento>();
                objCmpSql.CommandProcedure("spCMP_GET_ConsultaGralDocumento");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, ObjECMP_ReporteGrlDocumento.Opcion);
                objCmpSql.AddParameter("@Periodo", SqlDbType.Char, ObjECMP_ReporteGrlDocumento.Periodo);
                objCmpSql.AddParameter("@IdCliProveedor", SqlDbType.VarChar, ObjECMP_ReporteGrlDocumento.ObjEMNF_ClienteProveedor.IdCliProveedor);
                objCmpSql.AddParameter("@CodMoneda", SqlDbType.VarChar, ObjECMP_ReporteGrlDocumento.ObjESGC_Moneda.CodMoneda);
                objCmpSql.AddParameter("@CodDocumento", SqlDbType.VarChar, ObjECMP_ReporteGrlDocumento.ObjESGC_Documento.CodDocumento);
                objCmpSql.AddParameter("@IdUsuario", SqlDbType.VarChar, SGCVariables.ObjESGC_Usuario.IdUsuario);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    ListECMP_ReporteGrlDocumento.Add(new ECMP_ReporteGrlDocumento()
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
                        Exonerada = (dt.Rows[x]["Exonerada"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Exonerada"]) : 0,
                        Gravada = (dt.Rows[x]["Gravada"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Gravada"]) : 0,
                        IGV = (dt.Rows[x]["ImporteIGV"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["ImporteIGV"]) : 0,
                        PIGV = (dt.Rows[x]["IGV"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["IGV"]) : 0,
                        Percepcion = (dt.Rows[x]["Percepcion"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Percepcion"]) : 0,
                        PPercepcion = (dt.Rows[x]["ImpPercepcion"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["ImpPercepcion"]) : 0,
                        OCargos = (dt.Rows[x]["OtrosCargos"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["OtrosCargos"]) : 0,
                        Total = (dt.Rows[x]["Total"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Total"]) : 0,
                        ImpTotal = (dt.Rows[x]["ImporteTotal"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["ImporteTotal"]) : 0,
                        Detraccion = (dt.Rows[x]["Detraccion"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Detraccion"]) : 0,
                        PDetraccion = (dt.Rows[x]["ImpDetraccion"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["ImpDetraccion"]):0,
                        Glosa = (dt.Rows[x]["Glosa"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Glosa"]) : string.Empty
                       
                    });
                }
                return ListECMP_ReporteGrlDocumento;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
