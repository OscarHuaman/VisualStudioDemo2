/*********************************************************
'* NEGOCIO PARA LA TABLA ORDEN COMPRA DETALLE
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 19/11/2015
**********************************************************/
namespace CMP.Business
{
    using CMP.Entity;
    using ComputerSystems.DataAccess.LibrarySql;
    using MNF.Entity;
    using SGC.Empresarial.Useful.Modulo;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BCMP_CompraDetalle
    {
        private CmpSql objCmpSql;
        private List<ECMP_CompraDetalle> ListECMP_CompraDetalle;

        /// <summary>
        /// Listado de ObjBCMP_CompraDetalle 
        /// </summary>
        /// <param name="ObjECMP_Compra">Objeto de la entidad ECMP_Compra</param>
        /// <returns>Lista ECMP_OrdenCompraDetalle </returns>
        public List<ECMP_CompraDetalle> ListAdministrarCompraDetalle(ECMP_Compra ObjECMP_Compra)
        {
            try
            {
                objCmpSql = new CmpSql(SGCVariables.ConectionString);
                ListECMP_CompraDetalle = new List<ECMP_CompraDetalle>();
				decimal dmlIGV = SGCVariables.ObjESGC_Retencion.IGV / 100;
                objCmpSql.CommandProcedure("spCMP_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "AdministrarCompraDetalle");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, ObjECMP_Compra.IdCompra);

                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    ECMP_CompraDetalle ObjECMP_CompraDetalle = new ECMP_CompraDetalle();
                    ObjECMP_CompraDetalle.ObjECMP_Compra = ObjECMP_Compra;                    
                    ObjECMP_CompraDetalle.Item = (dt.Rows[x]["Item"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["Item"]) : 0;
                    ObjECMP_CompraDetalle.IdArticuloServicio = (dt.Rows[x]["IdArticuloServicio"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdArticuloServicio"]) : 0;
                    ObjECMP_CompraDetalle.Codigo = (dt.Rows[x]["Codigo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Codigo"]) : string.Empty;
                    ObjECMP_CompraDetalle.ArticuloServicio = (dt.Rows[x]["ArticuloServicio"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["ArticuloServicio"]) : string.Empty;
                    ObjECMP_CompraDetalle.TipoDetalle = (dt.Rows[x]["TipoDetalle"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["TipoDetalle"]) : string.Empty;
                    ObjECMP_CompraDetalle.CodUndMedida = (dt.Rows[x]["CodUndMedida"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodUndMedida"]) : string.Empty;
                    ObjECMP_CompraDetalle.Cantidad = (dt.Rows[x]["Cantidad"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Cantidad"]) : 0;
                    ObjECMP_CompraDetalle.PrecioUnitario = (dt.Rows[x]["PrecioUnitario"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["PrecioUnitario"]) : 0;
					ObjECMP_CompraDetalle.PrecioUnitarioTemp = (ObjECMP_Compra.IncluyeIGV) ? (((dt.Rows[x]["PrecioUnitario"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["PrecioUnitario"]) : 0) * ((decimal.Round(dmlIGV * 100, 2) + 100) / 100)) : ((dt.Rows[x]["PrecioUnitario"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["PrecioUnitario"]) : 0);
                    ObjECMP_CompraDetalle.ImporteIGV = (dt.Rows[x]["ImporteIGV"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["ImporteIGV"]) : 0;
                    ObjECMP_CompraDetalle.CodOperacionIGV = (dt.Rows[x]["CodOperacionIGV"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodOperacionIGV"]) : string.Empty;
                    ObjECMP_CompraDetalle.ObjEALM_Almacen = new ALM.Entity.EALM_Almacen() 
                    {
                        IdAlmacen = (dt.Rows[x]["IdAlmacen"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdAlmacen"]) : -1
                    };
                    ObjECMP_CompraDetalle.IdReferencia = (dt.Rows[x]["IdReferencia"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdReferencia"]) : 0;
                    ObjECMP_CompraDetalle.SerieNumero = (dt.Rows[x]["NroDocumento"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["NroDocumento"]) : string.Empty;
                    ObjECMP_CompraDetalle.IdEmpSucursal = (dt.Rows[x]["IdEmpSucursal"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdEmpSucursal"]) : 0;
                    ObjECMP_CompraDetalle.IdDestino = (dt.Rows[x]["IdDestino"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdDestino"]) : 0;
                    ObjECMP_CompraDetalle.PeriodoCampania = (dt.Rows[x]["PeriodoCampania"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["PeriodoCampania"]) : string.Empty;

                    ListECMP_CompraDetalle.Add(ObjECMP_CompraDetalle);
                }

                return ListECMP_CompraDetalle;
            }

            catch (Exception)
            {
                throw;
            }

        }
        
    }
}
