/*********************************************************
'* NEGOCIO PARA LA TABLA  
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 19/11/2015
 * -------------------------------------------------------
 * MODIFICADO POR: Miller R. Vega Zuloaga
 * FCH. MODIFICA : 02/10/2016
 * ASUNTO        : Se agregó parametro de IdUsuario al Procedure de ArticuloListaPrecio
**********************************************************/
namespace CMP.Business
{
    using CMP.Entity;
    using ComputerSystems.DataAccess.LibrarySql;
    using SGC.Empresarial.Useful.Modulo;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    public class BCMP_TempArticuloListaPrecio
    {
        private CmpSql ObjCmpSql;

        /// <summary>
        /// Lista Articulo Precios
        /// </summary>
        /// <returns></returns>
        public List<string> ListArticuloListaPercio(string IdCategoria, string IdSubCategoria, string IdMarca, string IdArticulo, string IdProveedor, string PeridoIni, string PeriodoFin)
        {
            try
            {
                ObjCmpSql = new CmpSql(SGCVariables.ConectionString);
                ObjCmpSql.CommandProcedure("spCMP_GET_ArticuloListaPrecio");

                var listDetalle = new List<string>();

                ObjCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "LISTADO");
                ObjCmpSql.AddParameter("@IdCategoria", SqlDbType.VarChar, (IdCategoria != "") ? IdCategoria : "0");
                ObjCmpSql.AddParameter("@IdSubCategoria", SqlDbType.VarChar, (IdSubCategoria != "") ? IdSubCategoria : "0");
                ObjCmpSql.AddParameter("@IdMarca", SqlDbType.VarChar, (IdMarca != "") ? IdMarca : "0");
                ObjCmpSql.AddParameter("@IdArticulo", SqlDbType.VarChar, (IdArticulo != "") ? IdArticulo : "0");
                ObjCmpSql.AddParameter("@IdCliProveedor", SqlDbType.VarChar, (IdProveedor != "") ? IdProveedor : "0");
                ObjCmpSql.AddParameter("@PeriodoIni", SqlDbType.Char, PeridoIni);
                ObjCmpSql.AddParameter("@PeriodoFin", SqlDbType.Char, PeriodoFin);
                ObjCmpSql.AddParameter("@IdUsuario", SqlDbType.Int, SGCVariables.ObjESGC_Usuario.IdUsuario);
                DataTable dt = ObjCmpSql.ExecuteDataTable();

                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                string cabecera = string.Empty;
                string Detalle = string.Empty;
                foreach (DataColumn item in dt.Columns)
                {
                    cabecera += item.ToString() + "/";
                }
                cabecera = cabecera.Substring(0, cabecera.Length - 1);
                listDetalle.Add(cabecera);


                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    string strValue = string.Empty;

                    for (int y = 0; y < dt.Columns.Count; y++)
                    {
                        strValue += Convert.ToString(dt.Rows[x][y]) + ",";
                    }

                    strValue = strValue.Substring(0, strValue.Length - 1);
                    listDetalle.Add(strValue);
                }

                return listDetalle;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ECMP_DataMartixReport> ListArticuloListaPrecio_Preview(string IdCategoria, string IdSubCategoria, string IdMarca, string IdArticulo, string IdProveedor, string PeridoIni, string PeriodoFin)
        {
            ObjCmpSql = new CmpSql(SGCVariables.ConectionString);
            ObjCmpSql.CommandProcedure("spCMP_GET_ArticuloListaPrecio");
            ObjCmpSql.AddParameter("@Opcion", SqlDbType.VarChar,"REPORTE");
            ObjCmpSql.AddParameter("@IdCategoria", SqlDbType.VarChar, (IdCategoria != "") ? IdCategoria : "0");
            ObjCmpSql.AddParameter("@IdUsuario", SqlDbType.VarChar, "0");
            ObjCmpSql.AddParameter("@IdSubCategoria", SqlDbType.VarChar, (IdSubCategoria != "") ? IdSubCategoria : "0");
            ObjCmpSql.AddParameter("@IdMarca", SqlDbType.VarChar, (IdMarca != "") ? IdMarca : "0");
            ObjCmpSql.AddParameter("@IdArticulo", SqlDbType.VarChar, (IdArticulo != "") ? IdArticulo : "0");
            ObjCmpSql.AddParameter("@IdCliProveedor", SqlDbType.VarChar, (IdProveedor != "") ? IdProveedor : "0");
            ObjCmpSql.AddParameter("@PeriodoIni", SqlDbType.Char, PeridoIni);
            ObjCmpSql.AddParameter("@PeriodoFin", SqlDbType.Char, PeriodoFin);
            DataTable dt = ObjCmpSql.ExecuteDataTable();
            var list = (from DataRow x in dt.Rows
                        select new ECMP_DataMartixReport
                        {
                            Codigo = (x["Código"] == DBNull.Value ? "" : x["Código"]).ToString(),
                            Articulo = (x["Artículo"] == DBNull.Value ? "" : x["Artículo"]).ToString(),
                            CodUndMedida = (x["Und. Medida"] == DBNull.Value ? "" : x["Und. Medida"]).ToString(),
                            Marca = (x["Marca"] == DBNull.Value ? "" : x["Marca"]).ToString(),
                            Categoria = (x["Categoría"] == DBNull.Value ? "" : x["Categoría"]).ToString(),
                            SubCategoria = (x["Sub-Categoría"] == DBNull.Value ? "" : x["Sub-Categoría"]).ToString(),
                            NroDocIdentidad = (x["RUC-DNI"] == DBNull.Value ? "" : x["RUC-DNI"]).ToString(),
                            RazonSocial = (x["Razón Social"] == DBNull.Value ? "" : x["Razón Social"]).ToString(),
                            CodMoneda = (x["Moneda"] == DBNull.Value ? "" : x["Moneda"]).ToString(),
                            Periodo = (x["Periodo"] == DBNull.Value ? "" : x["Periodo"]).ToString(),
                            Precio = Convert.ToDouble(x["Precio"] == DBNull.Value ? 0 : x["Precio"])
                        }).ToList();
            return list;
        }       
    }
}


