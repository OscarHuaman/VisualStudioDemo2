/*********************************************************
'* NEGOCIO PARA LA TABLA ORDE COMPRA
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 19/11/2015
**********************************************************/
namespace CMP.Business
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using ALM.Entity;
    using CMP.Entity;
    using ComputerSystems.DataAccess.LibrarySql;
    using SGC.Empresarial.Entity;
    using SGC.Empresarial.Useful.Modulo;
    using MNF.Entity;

    public class BCMP_OrdenCompra
    {
        private CmpSql objCmpSql;
        private List<ECMP_OrdenCompra> ListECMP_OrdenCompra;
        
        /// <summary>
        /// Insertar, Editar y Eliminar Orden de Compra
        /// </summary>
        /// <param name="ObjECMP_OrdenCompra">Objecto de la Entidad ECMP_OrdenCompra</param>
        public void TransOrdenCompra(ECMP_OrdenCompra ObjECMP_OrdenCompra)
        {
            try
            {
                objCmpSql = new CmpSql(SGCVariables.ConectionString);
                objCmpSql.CommandProcedure("spCMP_SET_OrdenCompra");
                objCmpSql.AddParameter("@Opcion", SqlDbType.Char, ObjECMP_OrdenCompra.Opcion);
                objCmpSql.AddParameter("@IdOrdenCompra", SqlDbType.Int, ObjECMP_OrdenCompra.IdOrdenCompra);
                objCmpSql.AddParameter("@IdEmpSucursal", SqlDbType.SmallInt, ObjECMP_OrdenCompra.ObjESGC_EmpresaSucursal.IdEmpSucursal);
                objCmpSql.AddParameter("@IdCliProveedor", SqlDbType.Int, ObjECMP_OrdenCompra.ObjEMNF_ClienteProveedor.IdCliProveedor);
                objCmpSql.AddParameter("@IdAlmacen", SqlDbType.Int, ObjECMP_OrdenCompra.ObjEALM_Almacen.IdAlmacen);
                objCmpSql.AddParameter("@IdFormaPago", SqlDbType.Int, ObjECMP_OrdenCompra.ObjESGC_FormaPago.IdFormaPago);
                objCmpSql.AddParameter("@CodMoneda", SqlDbType.Char, ObjECMP_OrdenCompra.ObjESGC_Moneda.CodMoneda);                
                objCmpSql.AddParameter("@Periodo", SqlDbType.Char, ObjECMP_OrdenCompra.Periodo);
                objCmpSql.AddParameter("@Fecha", SqlDbType.SmallDateTime, ObjECMP_OrdenCompra.Fecha);
                objCmpSql.AddParameter("@Serie", SqlDbType.VarChar, ObjECMP_OrdenCompra.Serie);
                objCmpSql.AddParameter("@TipoCambio", SqlDbType.Decimal, ObjECMP_OrdenCompra.TipoCambio);
                objCmpSql.AddParameter("@Gravada", SqlDbType.Decimal, ObjECMP_OrdenCompra.Gravada);
                objCmpSql.AddParameter("@Exonerada", SqlDbType.Decimal, ObjECMP_OrdenCompra.Exonerada);
                objCmpSql.AddParameter("@IGV", SqlDbType.Decimal, ObjECMP_OrdenCompra.IGV);
                objCmpSql.AddParameter("@ImporteIGV", SqlDbType.Decimal, ObjECMP_OrdenCompra.ImporteIGV);
                objCmpSql.AddParameter("@IncluyeIGV", SqlDbType.Decimal, ObjECMP_OrdenCompra.IncluyeIGV);
                objCmpSql.AddParameter("@FechaEntrega", SqlDbType.SmallDateTime, ObjECMP_OrdenCompra.FechaEntrega);
                objCmpSql.AddParameter("@LugarEntrega", SqlDbType.VarChar, ObjECMP_OrdenCompra.LugarEntrega);                
                objCmpSql.AddParameter("@CadenaXML", SqlDbType.VarChar, ObjECMP_OrdenCompra.CadenaXML);
                objCmpSql.AddParameter("@IdUsuario", SqlDbType.Int, SGCVariables.ObjESGC_Usuario.IdUsuario);

                if (ObjECMP_OrdenCompra.Opcion == "I")
                {
                    objCmpSql.AddParameterOut("@Numero", SqlDbType.VarChar, 8);
                    objCmpSql.AddParameterOut("@CodEstado", SqlDbType.VarChar, 5);
                    objCmpSql.ExecuteNonQuery();

                    ObjECMP_OrdenCompra.Numero = (string)objCmpSql.GetParameterOut("@Numero");
                    ObjECMP_OrdenCompra.ObjESGC_Estado.CodEstado = (string)objCmpSql.GetParameterOut("@CodEstado");
                }
                else
                {
                    objCmpSql.AddParameter("@Numero", SqlDbType.VarChar, ObjECMP_OrdenCompra.Numero);
                    objCmpSql.AddParameter("@CodEstado", SqlDbType.Char, ObjECMP_OrdenCompra.ObjESGC_Estado.CodEstado);
                    objCmpSql.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Listado de Orden de Compra
        /// </summary>
        /// <param name="Opcion">Opcion de Filtro de Orden de Compra  : (T) => Muestra Todos los registros ; (F) => Filtra por Rango de Fecha</param>
        /// <returns>Lista de ECMP_OrdenCompra</returns>
        public List<ECMP_OrdenCompra> ListOrdenCompra(ECMP_OrdenCompra ObjECMP_OrdenCompra, string Filtro)
        {
            try
            {
                objCmpSql = new CmpSql(SGCVariables.ConectionString);
                ListECMP_OrdenCompra = new List<ECMP_OrdenCompra>();

                objCmpSql.CommandProcedure("spCMP_GET_OrdenCompra");
                objCmpSql.AddParameter("@Opcion", SqlDbType.Char, (ObjECMP_OrdenCompra.Opcion == null) ? "F" : ObjECMP_OrdenCompra.Opcion);
                objCmpSql.AddParameter("@Filtro", SqlDbType.Char, (Filtro.Length == 0) ? "%" : Filtro);
                objCmpSql.AddParameter("@FechaDesde", SqlDbType.SmallDateTime, ObjECMP_OrdenCompra.Fecha.ToShortDateString());
                objCmpSql.AddParameter("@FechaHasta", SqlDbType.SmallDateTime, ObjECMP_OrdenCompra.FechaEntrega.ToShortDateString());
                objCmpSql.AddParameter("@CodEstado", SqlDbType.VarChar, ObjECMP_OrdenCompra.ObjESGC_Estado.CodEstado);
                //objCmpSql.AddParameter("@IdFormaPago", SqlDbType.VarChar, ObjECMP_OrdenCompra.ObjESGC_FormaPago.IdFormaPago);
                objCmpSql.AddParameter("@CodMoneda", SqlDbType.VarChar, ObjECMP_OrdenCompra.ObjESGC_Moneda.CodMoneda);
                objCmpSql.AddParameter("@IdCliProveedor", SqlDbType.Char, ObjECMP_OrdenCompra.ObjEMNF_ClienteProveedor.IdCliProveedor);
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Char, SGCVariables.ObjESGC_Usuario.IdUsuario);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    ListECMP_OrdenCompra.Add(new ECMP_OrdenCompra
                    {
                        IdOrdenCompra = (dt.Rows[x]["IdOrdenCompra"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdOrdenCompra"]) : 0,
                        ObjESGC_EmpresaSucursal = new ESGC_EmpresaSucursal
                        {
                            IdEmpSucursal = (dt.Rows[x]["IdEmpSucursal"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdEmpSucursal"]) : 0,
                            Sucursal = (dt.Rows[x]["Sucursal"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Sucursal"]) : string.Empty
                        },
                        ObjEMNF_ClienteProveedor = new EMNF_ClienteProveedor
                        {
                            IdCliProveedor = (dt.Rows[x]["IdCliProveedor"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdCliProveedor"]) : 0,
                            RazonSocial = (dt.Rows[x]["RazonSocial"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["RazonSocial"]) : string.Empty,
                            NroDocIdentidad = (dt.Rows[x]["NroDocIdentidad"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["NroDocIdentidad"]) : string.Empty,
                            Direccion = (dt.Rows[x]["Direccion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Direccion"]) : string.Empty
                        },
                        ObjEALM_Almacen = new EALM_Almacen
                        {
                            IdAlmacen = (dt.Rows[x]["IdAlmacen"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdAlmacen"]) : 0,
                            Almacen = (dt.Rows[x]["Almacen"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Almacen"]) : string.Empty,
                        },
                        LugarEntrega = (dt.Rows[x]["LugarEntrega"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["LugarEntrega"]) : string.Empty,
                        Fecha = (dt.Rows[x]["Fecha"] != DBNull.Value) ? Convert.ToDateTime(dt.Rows[x]["Fecha"]) : DateTime.Now,
                        ObjESGC_Moneda = new ESGC_Moneda
                        {
                            CodMoneda = (dt.Rows[x]["CodMoneda"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodMoneda"]) : string.Empty,
                            Descripcion = (dt.Rows[x]["MonedaDescripcion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["MonedaDescripcion"]) : string.Empty,
                            Simbolo = (dt.Rows[x]["Simbolo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Simbolo"]) : string.Empty,
                        },
                        ObjESGC_Estado = new ESGC_Estado
                        {
                            CodEstado = (dt.Rows[x]["CodEstado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodEstado"]) : string.Empty,
                            Estado = (dt.Rows[x]["Estado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Estado"]) : string.Empty
                        },
                        ObjESGC_Documento = new ESGC_Documento
                        {
                            CodDocumento = (dt.Rows[x]["CodDocumento"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodDocumento"]) : string.Empty,
                            Descripcion = (dt.Rows[x]["DocumentoDescripcion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["DocumentoDescripcion"]) : string.Empty
                        },
                        Serie = (dt.Rows[x]["Serie"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Serie"]) : string.Empty,
                        Numero = (dt.Rows[x]["Numero"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Numero"]) : string.Empty,

                        ObjESGC_FormaPago = new ESGC_FormaPago
                        {
                            IdFormaPago = (dt.Rows[x]["IdFormaPago"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdFormaPago"]) : 0,
                            FormaPago = (dt.Rows[x]["FormaPago"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["FormaPago"]) : string.Empty
                        },
                        FechaEntrega = (dt.Rows[x]["FechaEntrega"] != DBNull.Value) ? Convert.ToDateTime(dt.Rows[x]["FechaEntrega"]) : DateTime.Now,
                        TipoCambio = (dt.Rows[x]["TipoCambio"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["TipoCambio"]) : 0,
                        Gravada = (dt.Rows[x]["Gravada"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Gravada"]) : 0,
                        Exonerada = (dt.Rows[x]["Exonerada"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Exonerada"]) : 0,
                        IGV = (dt.Rows[x]["IGV"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["IGV"]) : 0,
                        ImporteIGV = (dt.Rows[x]["ImporteIGV"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["ImporteIGV"]) : 0,
                        IncluyeIGV = (dt.Rows[x]["IncluyeIGV"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["IncluyeIGV"]) : false,
                        Creacion = (dt.Rows[x]["Creado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Creado"]) : string.Empty,
                        Aprobacion = (dt.Rows[x]["Aprobado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Aprobado"]) : string.Empty,
                        DocumenSerie = (dt.Rows[x]["DocumenSerie"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["DocumenSerie"]) : string.Empty,
                        Periodo = (dt.Rows[x]["Periodo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Periodo"]) : string.Empty,
                        Provisionado = (dt.Rows[x]["Provisionado"] != DBNull.Value) ? Convert.ToUInt16(dt.Rows[x]["Provisionado"]) : uint.Parse("0"),
                        ProvicionadoText = ((dt.Rows[x]["Provisionado"] != DBNull.Value) ? Convert.ToUInt16(dt.Rows[x]["Provisionado"]) : uint.Parse("0")) == 0 ? "NO PROVISIONADO" : "PROVISIONADO",
                    });
                }

                return ListECMP_OrdenCompra;
            }

            catch (Exception)
            {
                throw;
            }
        }

    }
}
