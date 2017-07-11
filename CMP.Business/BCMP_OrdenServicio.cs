/*********************************************************
'* NEGOCIO PARA LA TABLA ORDEN SERVICIO
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   JUAN LUIS GUERRA MENESES
'* FCH. CREACIÓN : 19/11/2015
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

    public class BCMP_OrdenServicio
    {
        private CmpSql ObjCmpSql;
        private List<ECMP_OrdenServicio> ListECMP_OrdenServicio;

        /// <summary>
        /// Insertar, Editar Orden de Servicio
        /// </summary>
        /// <param name="ObjECMP_OrdenServicio">Objeto de ECMP_OrdenServicio</param>
        public void TransOrdenServicio(ECMP_OrdenServicio ObjECMP_OrdenServicio) 
        {
            try
            {
                ObjCmpSql = new CmpSql(SGCVariables.ConectionString);

                ObjCmpSql.CommandProcedure("spCMP_SET_OrdenServicio");
                ObjCmpSql.AddParameter("@Opcion", SqlDbType.Char, ObjECMP_OrdenServicio.Opcion);
                ObjCmpSql.AddParameter("@IdOrdenServicio", SqlDbType.Int, ObjECMP_OrdenServicio.IdOrdenServicio);
                ObjCmpSql.AddParameter("@IdEmpSucursal", SqlDbType.SmallInt, ObjECMP_OrdenServicio.ObjESGC_EmpresaSucursal.IdEmpSucursal);
                ObjCmpSql.AddParameter("@IdArea", SqlDbType.Int, ObjECMP_OrdenServicio.ObjESGC_Area.IdArea);
                ObjCmpSql.AddParameter("@IdCliProveedor", SqlDbType.Int, ObjECMP_OrdenServicio.ObjEMNF_ClienteProveedor.IdCliProveedor);
                ObjCmpSql.AddParameter("@CodTipoDestino", SqlDbType.VarChar, ObjECMP_OrdenServicio.ObjEMNF_TipoDestino.CodTipoDestino);
                ObjCmpSql.AddParameter("@IdFormaPago", SqlDbType.Int, ObjECMP_OrdenServicio.ObjESGC_FormaPago.IdFormaPago);
                ObjCmpSql.AddParameter("@CodMoneda", SqlDbType.Char, ObjECMP_OrdenServicio.ObjESGC_Moneda.CodMoneda);
                ObjCmpSql.AddParameter("@Periodo", SqlDbType.VarChar, ObjECMP_OrdenServicio.Periodo);
                ObjCmpSql.AddParameter("@Fecha", SqlDbType.DateTime, ObjECMP_OrdenServicio.Fecha);
                ObjCmpSql.AddParameter("@Serie", SqlDbType.VarChar, ObjECMP_OrdenServicio.Serie);
                ObjCmpSql.AddParameter("@TipoCambio", SqlDbType.Decimal, ObjECMP_OrdenServicio.TipoCambio);
                ObjCmpSql.AddParameter("@FechaInicio", SqlDbType.DateTime, ObjECMP_OrdenServicio.FechaInicio);
                ObjCmpSql.AddParameter("@FechaFin", SqlDbType.DateTime, ObjECMP_OrdenServicio.FechaFin);
                ObjCmpSql.AddParameter("@Contacto", SqlDbType.VarChar, ObjECMP_OrdenServicio.Contacto);
                ObjCmpSql.AddParameter("@Gravada", SqlDbType.Decimal, ObjECMP_OrdenServicio.Gravada);
                ObjCmpSql.AddParameter("@IGV", SqlDbType.Decimal, ObjECMP_OrdenServicio.IGV);
                ObjCmpSql.AddParameter("@ImporteIGV", SqlDbType.Decimal, ObjECMP_OrdenServicio.ImporteIGV);
                ObjCmpSql.AddParameter("@Exonerado", SqlDbType.TinyInt, ObjECMP_OrdenServicio.Exonerado);
                ObjCmpSql.AddParameter("@Retencion", SqlDbType.Bit, ObjECMP_OrdenServicio.Retencion);
                ObjCmpSql.AddParameter("@CadenaXML", SqlDbType.NText, ObjECMP_OrdenServicio.CadenaXML);
                ObjCmpSql.AddParameter("@IdUsuario", SqlDbType.Int, SGCVariables.ObjESGC_Usuario.IdUsuario);

                if (ObjECMP_OrdenServicio.Opcion == "I")
                {
                    ObjCmpSql.AddParameterOut("@Numero", SqlDbType.VarChar, 8);
                    ObjCmpSql.AddParameterOut("@CodEstado", SqlDbType.VarChar, 5);
                    ObjCmpSql.ExecuteNonQuery();

                    ObjECMP_OrdenServicio.Numero = (string)ObjCmpSql.GetParameterOut("@Numero");
                    ObjECMP_OrdenServicio.ObjESGC_Estado.CodEstado = (string)ObjCmpSql.GetParameterOut("@CodEstado");
                }
                else
                {
                    ObjCmpSql.AddParameter("@Numero", SqlDbType.VarChar, ObjECMP_OrdenServicio.Numero);
                    ObjCmpSql.AddParameter("@CodEstado", SqlDbType.Char, ObjECMP_OrdenServicio.ObjESGC_Estado.CodEstado);
                    ObjCmpSql.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Listado de Orden de Servicio
        /// </summary>
        /// <param name="Opcion">Opcion de Filtro de Orden de Servicio  : (T) => Muestra Todos los registros ; (F) => Filtra por Rango de Fecha</param>
        /// <param name="FechaDesde">Fecha de Inicio de Busqueda</param>
        /// <param name="FechaHasta">Fecha de Fin de Busqueda</param>
        /// <returns>Lista ECMP_OrdenServicio</returns>
        public List<ECMP_OrdenServicio> ListFiltrarOrdenServicio(ECMP_OrdenServicio ObjECMP_OrdenServicio, string Filtro) 
        {
            try
            {
                ObjCmpSql = new CmpSql(SGCVariables.ConectionString);
                ListECMP_OrdenServicio = new List<ECMP_OrdenServicio>();

                ObjCmpSql.CommandProcedure("spCMP_GET_OrdenServicio");
                ObjCmpSql.AddParameter("@Opcion", SqlDbType.Char, ObjECMP_OrdenServicio.Opcion);
                ObjCmpSql.AddParameter("@Filtro", SqlDbType.Char, (Filtro.Length == 0) ? "%" : Filtro);
                ObjCmpSql.AddParameter("@FechaDesde", SqlDbType.SmallDateTime, ObjECMP_OrdenServicio.FechaInicio);
                ObjCmpSql.AddParameter("@FechaHasta", SqlDbType.SmallDateTime, ObjECMP_OrdenServicio.FechaFin);
                ObjCmpSql.AddParameter("@CodEstado", SqlDbType.VarChar, ObjECMP_OrdenServicio.ObjESGC_Estado.CodEstado);
                ObjCmpSql.AddParameter("@IdCliProveedor", SqlDbType.VarChar, ObjECMP_OrdenServicio.ObjEMNF_ClienteProveedor.IdCliProveedor);
                ObjCmpSql.AddParameter("@CodMoneda", SqlDbType.VarChar, ObjECMP_OrdenServicio.ObjESGC_Moneda.CodMoneda);
                ObjCmpSql.AddParameter("@ParameterId", SqlDbType.Char, SGCVariables.ObjESGC_Usuario.IdUsuario);

                DataTable dt = ObjCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    ListECMP_OrdenServicio.Add(new ECMP_OrdenServicio
                    {
                        IdOrdenServicio = (dt.Rows[x]["IdOrdenServicio"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdOrdenServicio"]) : 0,
                        ObjESGC_EmpresaSucursal = new ESGC_EmpresaSucursal
                        {
                            IdEmpSucursal = (dt.Rows[x]["IdEmpSucursal"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdEmpSucursal"]) : 0,
                            Sucursal = (dt.Rows[x]["Sucursal"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Sucursal"]) : string.Empty
                        },
                        ObjESGC_Area = new ESGC_Area 
                        {
                            IdArea = (dt.Rows[x]["IdArea"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdArea"]) : 0,
                            Area = (dt.Rows[x]["Area"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Area"]) : string.Empty,
                        },
                        ObjEMNF_ClienteProveedor = new EMNF_ClienteProveedor
                        {
                            IdCliProveedor = (dt.Rows[x]["IdCliProveedor"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdCliProveedor"]) : 0,
                            RazonSocial = (dt.Rows[x]["RazonSocial"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["RazonSocial"]) : string.Empty,
                            NroDocIdentidad = (dt.Rows[x]["NroDocIdentidad"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["NroDocIdentidad"]) : string.Empty,
                            Direccion = (dt.Rows[x]["Direccion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Direccion"]) : string.Empty,
                        },
                        //ObjEALM_Almacen = new EALM_Almacen
                        //{
                        //    IdAlmacen = (dt.Rows[x][9] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x][9]) : 0,
                        //    Almacen = (dt.Rows[x][10] != DBNull.Value) ? Convert.ToString(dt.Rows[x][10]) : string.Empty,
                        //},
                        ObjESGC_Moneda = new ESGC_Moneda
                        {
                            CodMoneda = (dt.Rows[x]["CodMoneda"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodMoneda"]) : string.Empty,
                            Descripcion = (dt.Rows[x]["Descripcion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Descripcion"]) : string.Empty,
                            Simbolo = (dt.Rows[x]["Simbolo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Simbolo"]) : string.Empty
                        },
                        ObjESGC_Estado = new ESGC_Estado
                        {
                            CodEstado = (dt.Rows[x]["CodEstado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodEstado"]) : string.Empty,
                            Estado = (dt.Rows[x]["Estado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Estado"]) : string.Empty
                        },
                        ObjESGC_Documento = new ESGC_Documento
                        {
                            CodDocumento = (dt.Rows[x]["CodDocumento"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodDocumento"]) : string.Empty
                        },
                        Serie = (dt.Rows[x]["Serie"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Serie"]) : string.Empty,
                        Numero = (dt.Rows[x]["Numero"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Numero"]) : string.Empty,
                        ObjESGC_FormaPago = new ESGC_FormaPago
                        {
                            IdFormaPago = (dt.Rows[x]["IdFormaPago"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdFormaPago"]) : 0,
                            FormaPago = (dt.Rows[x]["FormaPago"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["FormaPago"]) : string.Empty
                        },
                        ObjEMNF_TipoDestino = new EMNF_TipoDestino()
                        {
                            CodTipoDestino = (dt.Rows[x]["CodTipoDestino"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodTipoDestino"]) : string.Empty,
                        },
                        Periodo = (dt.Rows[x]["Periodo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Periodo"]) : string.Empty,
                        Fecha = (dt.Rows[x]["Fecha"] != DBNull.Value) ? Convert.ToDateTime(dt.Rows[x]["Fecha"]) : DateTime.Now,
                        FechaInicio = (dt.Rows[x]["FechaInicio"] != DBNull.Value) ? Convert.ToDateTime(dt.Rows[x]["FechaInicio"]) : DateTime.Now,
                        FechaFin = (dt.Rows[x]["FechaFin"] != DBNull.Value) ? Convert.ToDateTime(dt.Rows[x]["FechaFin"]) : DateTime.Now,
                        Contacto = (dt.Rows[x]["Contacto"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Contacto"]) : string.Empty,
                        TipoCambio = (dt.Rows[x]["TipoCambio"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["TipoCambio"]) : 0,
                        Gravada = (dt.Rows[x]["Gravada"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Gravada"]) : 0,
                        IGV = (dt.Rows[x]["IGV"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["IGV"]) : 0,
                        ImporteIGV = (dt.Rows[x]["ImporteIGV"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["ImporteIGV"]) : 0,
                        Exonerado = (dt.Rows[x]["Exonerado"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["Exonerado"]) : 11,
                        Retencion = (dt.Rows[x]["Retencion"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["Retencion"]) : false,
                        DocumenSerie = (dt.Rows[x]["DocumenSerie"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["DocumenSerie"]) : string.Empty,
                        Creacion = (dt.Rows[x]["Creado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Creado"]) : string.Empty,
                        Aprobacion = (dt.Rows[x]["Aprobado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Aprobado"]) : string.Empty
                    });
                }
                return ListECMP_OrdenServicio;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
