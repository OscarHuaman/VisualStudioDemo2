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
    using SGC.Empresarial.Entity;
    using SGC.Empresarial.Useful.Modulo;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BCMP_OrdenCompraDetalle
    {
        private CmpSql objCmpSql;
        private List<ECMP_OrdenCompraDetalle> ListECMP_OrdenCompraDetalle;

        /// <summary>
        /// Listado de Detalle de Orden de Compra
        /// </summary>
        /// <param name="ObjECMP_OrdenCompraDetalle">Objeto de la entidad ECMP_OrdenCompraDetalle</param>
        /// <returns>Lista deECMP_OrdenCompraDetalle </returns>
        public List<ECMP_OrdenCompraDetalle> ListAdministrarOrdenCompraDetalle(ECMP_OrdenCompra ObjECMP_OrdenCompra)
        {
            try
            {
                objCmpSql = new CmpSql(SGCVariables.ConectionString);
                ListECMP_OrdenCompraDetalle = new List<ECMP_OrdenCompraDetalle>();
				decimal dmlIGV = SGCVariables.ObjESGC_Retencion.IGV / 100;
                objCmpSql.CommandProcedure("spCMP_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "AdministrarOrdenCompraDetalle");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, ObjECMP_OrdenCompra.IdOrdenCompra);

                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    ListECMP_OrdenCompraDetalle.Add(new ECMP_OrdenCompraDetalle
                    {
                        ObjECMP_OrdenCompra = ObjECMP_OrdenCompra,
                        Item = (dt.Rows[x][1] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x][1]) : 0,
                        ObjEMNF_Articulo = new EMNF_Articulo
                        {
                            IdArticulo = (dt.Rows[x][2] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x][2]) : 0,
                            Articulo = (dt.Rows[x][3] != DBNull.Value) ? Convert.ToString(dt.Rows[x][3]) : string.Empty,
                            Codigo = (dt.Rows[x][4] != DBNull.Value) ? Convert.ToString(dt.Rows[x][4]) : string.Empty,
                            ObjEMNF_UnidadMedida = new EMNF_UnidadMedida
                            {
                                CodUndMedida = (dt.Rows[x][5] != DBNull.Value) ? Convert.ToString(dt.Rows[x][5]) : string.Empty,
                            },
                            ObjEMNF_OperacionIGV = new EMNF_OperacionIGV()
                            {
                                CodOperacionIGV = (dt.Rows[x][6] != DBNull.Value) ? Convert.ToString(dt.Rows[x][6]) : string.Empty,
                            }
                        },
                        PrecioUnitario = (dt.Rows[x][7] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x][7]) : 0,
						PrecioUnitarioTemp = (ObjECMP_OrdenCompra.IncluyeIGV) ? (((dt.Rows[x][7] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x][7]) : 0) * ((decimal.Round(dmlIGV * 100, 2) + 100) / 100)) : ((dt.Rows[x][7] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x][7]) : 0),
                        Cantidad = (dt.Rows[x][8] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x][8]) : 0,
                        CantidadRecep = (dt.Rows[x][9] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x][9]) : 0,
                        ImporteIGV = (dt.Rows[x][10] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x][10]) : 0,
                      
                        ObjESGC_Estado = new ESGC_Estado 
                        {
                            CodEstado = (dt.Rows[x][11] != DBNull.Value) ? Convert.ToString(dt.Rows[x][11]) : string.Empty,
                            Estado = (dt.Rows[x][12] != DBNull.Value) ? Convert.ToString(dt.Rows[x][12]) : string.Empty
                        },
                        Provisionado = (dt.Rows[x]["Provisionado"] != DBNull.Value) ? Convert.ToUInt16(dt.Rows[x]["Provisionado"]) : uint.Parse("0")
                    });
                }

                return ListECMP_OrdenCompraDetalle;
            }

            catch (Exception)
            {
                throw;
            }

        }
        
    }
}
