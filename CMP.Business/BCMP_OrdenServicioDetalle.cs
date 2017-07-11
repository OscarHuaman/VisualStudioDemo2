/*********************************************************
'* NEGOCIO PARA LA TABLA ORDEN SERVICIO DETALLE
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   JUAN LUIS GUERRA MENESES
'* FCH. CREACIÓN : 19/11/2015
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

    public class BCMP_OrdenServicioDetalle
    {
        private CmpSql objCmpSql;
        private List<ECMP_OrdenServicioDetalle> ListECMP_OrdenServicioDetalle;

        /// <summary>
        /// Listado de Detalle de Orden de Servicio
        /// </summary>
        /// <param name="ObjECMP_OrdenCompraDetalle">Objeto de la entidad ECMP_OrdenServicioDetalle</param>
        /// <returns>Lista de ECMP_OrdenServicioDetalle </returns>
        public List<ECMP_OrdenServicioDetalle> ListAdministrarOrdenServicioDetalle(ECMP_OrdenServicio ObjECMP_OrdenServicio)
        {
            try
            {
                objCmpSql = new CmpSql(SGCVariables.ConectionString);
                ListECMP_OrdenServicioDetalle = new List<ECMP_OrdenServicioDetalle>();
				decimal dmlIGV = SGCVariables.ObjESGC_Retencion.IGV / 100;
                objCmpSql.CommandProcedure("spCMP_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "AdministrarOrdenServicioDetalle");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, ObjECMP_OrdenServicio.IdOrdenServicio);

                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    ListECMP_OrdenServicioDetalle.Add(new ECMP_OrdenServicioDetalle
                    {
                        ObjECMP_OrdenServicio = ObjECMP_OrdenServicio,
                        Item = (dt.Rows[x][1] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x][1]) : 0,
                        ObjEMNF_Servicio = new EMNF_Servicio
                        {
                            IdServicio = (dt.Rows[x][2] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x][2]) : 0,
                            Codigo = (dt.Rows[x]["Codigo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Codigo"]) : string.Empty,
                            Servicio = (dt.Rows[x]["Servicio"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Servicio"]) : string.Empty,
                            ObjEMNF_OperacionIGV = new EMNF_OperacionIGV()
                            {
                                CodOperacionIGV = (dt.Rows[x]["CodOperacionIGV"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodOperacionIGV"]) : string.Empty,
                            }
                        },
                        Cantidad = (dt.Rows[x]["Cantidad"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Cantidad"]) : 0,
                        PrecioUnitario = (dt.Rows[x]["PrecioUnitario"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["PrecioUnitario"]) : 0,
						PrecioUnitarioTemp = (ObjECMP_OrdenServicio.Exonerado == 12) ? (((dt.Rows[x]["PrecioUnitario"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["PrecioUnitario"]) : 0) * ((decimal.Round(dmlIGV * 100, 2) + 100) / 100)) : ((dt.Rows[x]["PrecioUnitario"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["PrecioUnitario"]) : 0),
                        IdDestino = (dt.Rows[x]["IdDestino"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdDestino"]) : 0,
                        PeriodoCampania = (dt.Rows[x]["PeriodoCampania"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["PeriodoCampania"]) : string.Empty,
                        Observaciones = (dt.Rows[x]["Observaciones"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Observaciones"]) : string.Empty,
                    });
                }

                return ListECMP_OrdenServicioDetalle;
            }

            catch (Exception)
            {
                throw;
            }

        }
    }
}
