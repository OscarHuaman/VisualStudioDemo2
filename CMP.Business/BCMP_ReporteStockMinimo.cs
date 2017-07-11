/*********************************************************
'* NEGOCIOS PARA REPORTE DE STOCK MINIMO
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   JUAN GUERRA MENESES
'* FCH. CREACIÓN : 19/02/2016
**********************************************************/
namespace CMP.Business
{
    using ALM.Entity;
    using CMP.Entity;
    using ComputerSystems.DataAccess.LibrarySql;
    using MNF.Entity;
    using SGC.Empresarial.Entity;
    using SGC.Empresarial.Useful.Modulo;
    using System;
    using System.Collections.Generic;
    using System.Data;
    public class BCMP_ReporteStockMinimo
    {
        private CmpSql objCmpSql;
        private List<ECMP_ReporteStockMinimo> ListECMP_ReporteStockMinimo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ObjECMP_ReporteGrlDocumento"></param>
        /// <returns></returns>
        public List<ECMP_ReporteStockMinimo> ListReporteStockMinimo(ECMP_ReporteStockMinimo ObjECMP_ReporteStockMinimo)
        {
            try
            {
                objCmpSql = new CmpSql(SGCVariables.ConectionString);
                ListECMP_ReporteStockMinimo = new List<ECMP_ReporteStockMinimo>();
                objCmpSql.CommandProcedure("spCMP_GET_StockMinimoArticulo");
                objCmpSql.AddParameter("@IdEmpresa", SqlDbType.Int, ObjECMP_ReporteStockMinimo.IdEmpresa);
                objCmpSql.AddParameter("@IdEmpSucursal", SqlDbType.Int, ObjECMP_ReporteStockMinimo.ObjESGC_UsuarioEmpresaSucursal.ObjESGC_EmpresaSucursal.IdEmpSucursal);
                objCmpSql.AddParameter("@IdAlmacen", SqlDbType.Int, ObjECMP_ReporteStockMinimo.ObjEALM_Almacen.IdAlmacen);
                objCmpSql.AddParameter("@IdArtClase", SqlDbType.Int, ObjECMP_ReporteStockMinimo.ObjEMNF_ArticuloClase.IdArtClase);
                objCmpSql.AddParameter("@IdMarca", SqlDbType.Int, ObjECMP_ReporteStockMinimo.ObjEMNF_ArticuloMarca.IdMarca);
                objCmpSql.AddParameter("@IdArticulo", SqlDbType.Int, ObjECMP_ReporteStockMinimo.ObjEMNF_Articulo.IdArticulo);                
                
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    ListECMP_ReporteStockMinimo.Add(new ECMP_ReporteStockMinimo()
                    {
                        SubCategoria = (dt.Rows[x]["SubCategoria"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["SubCategoria"]) : string.Empty,
                        ObjEMNF_ArticuloMarca = new EMNF_ArticuloMarca()
                        {
                            Marca = (dt.Rows[x]["Marca"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Marca"]) : string.Empty,
                        },
                        ObjEMNF_Articulo = new EMNF_Articulo()
                        {
                            ObjEMNF_UnidadMedida = new EMNF_UnidadMedida()
                            {
                                CodUndMedida = (dt.Rows[x]["CodUndMedida"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodUndMedida"]) : string.Empty,
                            },
                            Codigo = (dt.Rows[x]["Codigo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Codigo"]) : string.Empty,
                            Articulo = (dt.Rows[x]["Articulo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Articulo"]) : string.Empty,
                        },
                        ObjEALM_Almacen = new EALM_Almacen()
                        {
                            ObjESGC_EmpresaSucursal = new ESGC_EmpresaSucursal() 
                            {
                                Sucursal = (dt.Rows[x]["Sucursal"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Sucursal"]) : string.Empty,
                            },
                            Almacen = (dt.Rows[x]["Almacen"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Almacen"]) : string.Empty,
                        },
                        StockMinimo = (dt.Rows[x]["StockMinimo"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["StockMinimo"]) : 0,
                        StockActual = (dt.Rows[x]["StockActual"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["StockActual"]) : 0,
                    });
                }
                return ListECMP_ReporteStockMinimo;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
