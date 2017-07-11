/*********************************************************
'* NEGOCIO PARA LA TABLA COMPRA
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 11/12/2015
**********************************************************
'* REPOSITORIO OSCAR
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 11/07/2015
**********************************************************/
namespace CMP.Business
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using CMP.Entity;
    using ComputerSystems.DataAccess.LibrarySql;
    using SGC.Empresarial.Entity;
    using SGC.Empresarial.Useful.Modulo;
    using MNF.Entity;

    public class BCMP_Compra
    {
        private CmpSql objCmpSql;
        private List<ECMP_Compra> ListECMP_Compra;
        
        /// <summary>
        /// Insertar, Editar y Eliminar Compra
        /// </summary>
        /// <param name="ObjECMP_Compra">Objecto de la Entidad ECMP_Compra</param>
        public void TransCompra(ECMP_Compra ObjECMP_Compra)
        {
            try
            {
                objCmpSql = new CmpSql(SGCVariables.ConectionString);
                objCmpSql.CommandProcedure("spCMP_SET_Compra");
                objCmpSql.AddParameter("@Opcion", SqlDbType.Char, ObjECMP_Compra.Opcion);
                objCmpSql.AddParameter("@IdCompra", SqlDbType.Int, ObjECMP_Compra.IdCompra);
                objCmpSql.AddParameter("@IdEmpresa", SqlDbType.Int, SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.IdEmpresa);
                objCmpSql.AddParameter("@IdCliProveedor", SqlDbType.Int, ObjECMP_Compra.ObjEMNF_ClienteProveedor.IdCliProveedor);
                objCmpSql.AddParameter("@IdFormaPago", SqlDbType.Int, ObjECMP_Compra.ObjESGC_FormaPago.IdFormaPago);
                objCmpSql.AddParameter("@CodMotMovimiento", SqlDbType.Char, ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento);
                objCmpSql.AddParameter("@CodMediosPago", SqlDbType.Char, (ObjECMP_Compra.ObjEMNF_MedioPago != null) ? ObjECMP_Compra.ObjEMNF_MedioPago.CodMediosPago : "");
                objCmpSql.AddParameter("@IdSubdiario", SqlDbType.Int, ObjECMP_Compra.ObjEMNF_SubDiario.IdSubDiario);
                objCmpSql.AddParameter("@CodEstado", SqlDbType.Char, ObjECMP_Compra.ObjESGC_Estado.CodEstado);
                objCmpSql.AddParameter("@CodMoneda", SqlDbType.Char, ObjECMP_Compra.ObjESGC_Moneda.CodMoneda);
                objCmpSql.AddParameter("@CodDocumento", SqlDbType.Char, ObjECMP_Compra.CodDocumento);
                objCmpSql.AddParameter("@Numero", SqlDbType.VarChar, ObjECMP_Compra.Numero);
                objCmpSql.AddParameter("@Serie", SqlDbType.Char, ObjECMP_Compra.Serie);
                objCmpSql.AddParameter("@Periodo", SqlDbType.Char, ObjECMP_Compra.Periodo);
                objCmpSql.AddParameter("@Fecha", SqlDbType.SmallDateTime, ObjECMP_Compra.Fecha.ToShortDateString());
                objCmpSql.AddParameter("@FechaContable", SqlDbType.SmallDateTime, ObjECMP_Compra.FechaContable.ToShortDateString());
                objCmpSql.AddParameter("@TipoCambio", SqlDbType.Decimal, ObjECMP_Compra.TipoCambio);
                objCmpSql.AddParameter("@Gravada", SqlDbType.Decimal, ObjECMP_Compra.Gravada);
                objCmpSql.AddParameter("@Exonerada", SqlDbType.Decimal, ObjECMP_Compra.Exonerada);
                objCmpSql.AddParameter("@IGV", SqlDbType.Decimal, ObjECMP_Compra.IGV);
                objCmpSql.AddParameter("@ImporteIGV", SqlDbType.Decimal, ObjECMP_Compra.ImporteIGV);
                objCmpSql.AddParameter("@IncluyeIGV", SqlDbType.Bit,ObjECMP_Compra.IncluyeIGV);
                objCmpSql.AddParameter("@AfectoDetraccion", SqlDbType.Bit,ObjECMP_Compra.AfectoDetraccion);
                objCmpSql.AddParameter("@AfectoPercepcion", SqlDbType.Bit,ObjECMP_Compra.AfectoPercepcion);
                objCmpSql.AddParameter("@Detraccion", SqlDbType.Decimal, (ObjECMP_Compra.AfectoDetraccion) ? (ObjECMP_Compra.Detraccion / 100) : 0);
                objCmpSql.AddParameter("@Percepcion", SqlDbType.Decimal, (ObjECMP_Compra.AfectoPercepcion) ? ObjECMP_Compra.Percepcion : 0);
                objCmpSql.AddParameter("@Retencion", SqlDbType.Bit, ObjECMP_Compra.Retencion);
                objCmpSql.AddParameter("@GuiaRemision", SqlDbType.VarChar, ObjECMP_Compra.GuiaRemision);
                objCmpSql.AddParameter("@AfectaAlmacen", SqlDbType.Bit, ObjECMP_Compra.AfectaAlmacen);
                objCmpSql.AddParameter("@Distribucion", SqlDbType.Bit, ObjECMP_Compra.Distribucion);
                objCmpSql.AddParameter("@Planilla", SqlDbType.Bit, ObjECMP_Compra.Planilla);
                objCmpSql.AddParameter("@CajaBanco", SqlDbType.Bit, ObjECMP_Compra.CajaBanco);
                objCmpSql.AddParameter("@Anticipo", SqlDbType.Bit, ObjECMP_Compra.Anticipo);
                objCmpSql.AddParameter("@CodTipoDestino", SqlDbType.Char, ObjECMP_Compra.ObjEMNF_TipoDestino.CodTipoDestino);
                objCmpSql.AddParameter("@Glosa", SqlDbType.VarChar, (ObjECMP_Compra.Glosa == null) ? " " : ObjECMP_Compra.Glosa);
                objCmpSql.AddParameter("@CodDocumentoRef", SqlDbType.Char, ObjECMP_Compra.CodDocumentoRef);
                objCmpSql.AddParameter("@CadenaXML", SqlDbType.NText, ObjECMP_Compra.CadenaXML);
                objCmpSql.AddParameter("@DocumentoRefXML", SqlDbType.NText, ObjECMP_Compra.DocumentoRefXML);
                objCmpSql.AddParameter("@CompraAnticipoXML", SqlDbType.NText, ObjECMP_Compra.CompraAnticipoXML);
                objCmpSql.AddParameter("@IdUsuario", SqlDbType.Int, SGCVariables.ObjESGC_Usuario.IdUsuario);

                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Listado de Compra
        /// </summary>
        /// <param name="Opcion">Opcion de Filtro de Orden de Compra  : (T) => Muestra Todos los registros ; (F) => Filtra por Rango de Fecha</param>
        /// <param name="FechaDesde">Fecha de Inicio de Busqueda</param>
        /// <param name="FechaHasta">Fecha de Fin de Busqueda</param>
        /// <returns>Lista de ECMP_OrdenCompra</returns>
        public List<ECMP_Compra> ListCompra(string Opcion, DateTime FechaDesde, DateTime FechaHasta, string CodEstado, string Filtro)
        {
            try
            {
                objCmpSql = new CmpSql(SGCVariables.ConectionString);
                ListECMP_Compra = new List<ECMP_Compra>();

                objCmpSql.CommandProcedure("spCMP_GET_Compra");
                objCmpSql.AddParameter("@Opcion", SqlDbType.Char, Opcion);
                objCmpSql.AddParameter("@Filtro", SqlDbType.Char, (Filtro.Length == 0) ? "%" : Filtro);
                objCmpSql.AddParameter("@FechaDesde", SqlDbType.SmallDateTime, FechaDesde.ToShortDateString());
                objCmpSql.AddParameter("@FechaHasta", SqlDbType.SmallDateTime, FechaHasta.ToShortDateString());
                objCmpSql.AddParameter("@CodEstado", SqlDbType.Char, CodEstado);
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Char, SGCVariables.ObjESGC_Usuario.IdUsuario);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    ListECMP_Compra.Add(new ECMP_Compra
                    {
                        IdCompra = (dt.Rows[x]["IdCompra"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdCompra"]) : 0,
                        ObjEMNF_ClienteProveedor = new EMNF_ClienteProveedor
                        {
                            IdCliProveedor = (dt.Rows[x]["IdCliProveedor"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdCliProveedor"]) : 0,
                            RazonSocial = (dt.Rows[x]["RazonSocial"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["RazonSocial"]) : string.Empty,
                            NroDocIdentidad = (dt.Rows[x]["NroDocIdentidad"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["NroDocIdentidad"]) : string.Empty,
                            Direccion = (dt.Rows[x]["Direccion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Direccion"]) : string.Empty
                        },
                        Fecha = (dt.Rows[x]["Fecha"] != DBNull.Value) ? Convert.ToDateTime(dt.Rows[x]["Fecha"]) : DateTime.Now,
                        FechaContable = (dt.Rows[x]["FechaContable"] != DBNull.Value) ? Convert.ToDateTime(dt.Rows[x]["FechaContable"]) : DateTime.Now,
                        ObjESGC_Moneda = new ESGC_Moneda
                        {
                            CodMoneda = (dt.Rows[x]["CodMoneda"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodMoneda"]) : string.Empty,
                            Descripcion = (dt.Rows[x]["MonedaDescripcion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["MonedaDescripcion"]) : string.Empty,
                        },
                        ObjESGC_Estado = new ESGC_Estado
                        {
                            CodEstado = (dt.Rows[x]["CodEstado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodEstado"]) : string.Empty,
                            Estado = (dt.Rows[x]["Estado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Estado"]) : string.Empty
                        },
                        CodDocumento = (dt.Rows[x]["CodDocumento"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodDocumento"]) : string.Empty,
                        Serie = (dt.Rows[x]["Serie"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Serie"]) : string.Empty,
                        Numero = (dt.Rows[x]["Numero"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Numero"]) : string.Empty,

                        ObjESGC_FormaPago = new ESGC_FormaPago
                        {
                            IdFormaPago = (dt.Rows[x]["IdFormaPago"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdFormaPago"]) : 0,
                            FormaPago = (dt.Rows[x]["FormaPago"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["FormaPago"]) : string.Empty
                        },
                        FechaVencimiento = (dt.Rows[x]["FechaVencimiento"] != DBNull.Value) ? Convert.ToDateTime(dt.Rows[x]["FechaVencimiento"]) : DateTime.Now,
                        TipoCambio = (dt.Rows[x]["TipoCambio"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["TipoCambio"]) : 0,
                        Gravada = (dt.Rows[x]["Gravada"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Gravada"]) : 0,
                        Exonerada = (dt.Rows[x]["Exonerada"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Exonerada"]) : 0,
                        IGV = (dt.Rows[x]["IGV"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["IGV"]) : 0,
                        ImporteIGV = (dt.Rows[x]["ImporteIGV"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["ImporteIGV"]) : 0,
                        IncluyeIGV = (dt.Rows[x]["IncluyeIGV"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["IncluyeIGV"]) : false,
                        Planilla = (dt.Rows[x]["Planilla"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["Planilla"]) : false,
                        AfectoDetraccion = (dt.Rows[x]["AfectoDetraccion"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["AfectoDetraccion"]) : false,
                        AfectoPercepcion = (dt.Rows[x]["AfectoPercepcion"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["AfectoPercepcion"]) : false,
                        ObjEMNF_OperacionMovimiento = new EMNF_OperacionMovimiento()
                        {
                            CodOpeMovimiento = (dt.Rows[x]["CodOpeMovimiento"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodOpeMovimiento"]) : string.Empty,
                        },
                        ObjEMNF_MotivoMovimiento = new EMNF_MotivoMovimiento()
                        {
                            CodMotMovimiento = (dt.Rows[x]["CodMotMovimiento"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodMotMovimiento"]) : string.Empty,
                            MotMovimiento = (dt.Rows[x]["MotMovimiento"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["MotMovimiento"]) : string.Empty,
                        },
                        ObjEMNF_MedioPago = new EMNF_MedioPago()
                        {
                            CodMediosPago = (dt.Rows[x]["CodMediosPago"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodMediosPago"]) : string.Empty,
                        },
                        ObjEMNF_SubDiario = new EMNF_SubDiario()
                        {
                            IdSubDiario = (dt.Rows[x]["IdSubDiario"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdSubDiario"]) : 0,
                        },
                        ObjEMNF_TipoDestino = new EMNF_TipoDestino()
                        {
                            CodTipoDestino = (dt.Rows[x]["CodTipoDestino"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodTipoDestino"]) : string.Empty,
                        },
                        CodDocumentoRef = (dt.Rows[x]["CodDocumentoRef"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodDocumentoRef"]) : string.Empty,
                        Detraccion = (dt.Rows[x]["Detraccion"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Detraccion"]) * 100 : 0,
                        GuiaRemision = (dt.Rows[x]["GuiaRemision"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["GuiaRemision"]) : string.Empty,
                        AfectaAlmacen = (dt.Rows[x]["AfectaAlmacen"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["AfectaAlmacen"]) : false,
                        Distribucion = (dt.Rows[x]["Distribucion"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["Distribucion"]) : false,
                        CajaBanco = (dt.Rows[x]["CajaBanco"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["CajaBanco"]) : false,
                        Anticipo = (dt.Rows[x]["Anticipo"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["Anticipo"]) : false,
                        Percepcion = (dt.Rows[x]["Percepcion"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Percepcion"]) : 0,
                        Retencion = (dt.Rows[x]["Retencion"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["Retencion"]) : false,
                        Periodo = (dt.Rows[x]["Periodo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Periodo"]) : string.Empty,
                        Glosa = (dt.Rows[x]["Glosa"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Glosa"]) : string.Empty
                    });
                }

                return ListECMP_Compra;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public List<ECMP_Compra> ListCompraBusqDocument(ECMP_Compra ObjECMP_Compra, string FiltrarDocumento)
        {
            try
            {
                objCmpSql = new CmpSql(SGCVariables.ConectionString);
                ListECMP_Compra = new List<ECMP_Compra>();

                objCmpSql.CommandProcedure("spCMP_GET_CompraBusqDocumento");
                objCmpSql.AddParameter("@IdCliProveedor", SqlDbType.VarChar, ObjECMP_Compra.ObjEMNF_ClienteProveedor.IdCliProveedor);
                objCmpSql.AddParameter("@CodMoneda", SqlDbType.VarChar, (ObjECMP_Compra.ObjESGC_Moneda.CodMoneda == null) ? "%" : ObjECMP_Compra.ObjESGC_Moneda.CodMoneda);
                objCmpSql.AddParameter("@FiltrarDocumento", SqlDbType.VarChar, (FiltrarDocumento == "" || FiltrarDocumento == null) ? "%" : FiltrarDocumento);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    ListECMP_Compra.Add(new ECMP_Compra
                    {
                        IdCompra = (dt.Rows[x]["IdCompra"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdCompra"]) : 0,
                        ObjEMNF_ClienteProveedor = new EMNF_ClienteProveedor
                        {
                            IdCliProveedor = (dt.Rows[x]["IdCliProveedor"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdCliProveedor"]) : 0,
                            RazonSocial = (dt.Rows[x]["RazonSocial"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["RazonSocial"]) : string.Empty,
                        },
                        ObjESGC_Moneda = new ESGC_Moneda
                        {
                            CodMoneda = (dt.Rows[x]["CodMoneda"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodMoneda"]) : string.Empty,
                            Descripcion = (dt.Rows[x]["Descripcion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Descripcion"]) : string.Empty,
                            Defecto = (dt.Rows[x]["Defecto"] != DBNull.Value) ? Convert.ToBoolean(Convert.ToInt32(dt.Rows[x]["Defecto"])) : false,
                            Simbolo = (dt.Rows[x]["Simbolo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Simbolo"]) : string.Empty,
                        },
                        ObjESGC_Estado = new ESGC_Estado()
                        {
                            Estado = (dt.Rows[x]["Estado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Estado"]) : string.Empty,
                        },
                        TipoCambio = (dt.Rows[x]["TipoCambio"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["TipoCambio"]) : 0,
                        CodDocumento = (dt.Rows[x]["CodDocumento"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodDocumento"]) : string.Empty,
                        DescDocumento = (dt.Rows[x]["DescDocumento"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["DescDocumento"]) : string.Empty,
                        Descripcion = (dt.Rows[x]["DescDocumento"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["DescDocumento"]) : string.Empty,
                        Serie = (dt.Rows[x]["Serie"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Serie"]) : string.Empty,
                        Numero = (dt.Rows[x]["Numero"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Numero"]) : string.Empty,
                        Fecha = (dt.Rows[x]["Fecha"] != DBNull.Value) ? Convert.ToDateTime(dt.Rows[x]["Fecha"]) : DateTime.Now,
                        IGV = (dt.Rows[x]["IGV"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["IGV"]) : 0,
                        IncluyeIGV = (dt.Rows[x]["IncluyeIGV"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["IncluyeIGV"]) : false,
                        Anticipo = (dt.Rows[x]["Anticipo"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["Anticipo"]) : false,
                        Gravada = (dt.Rows[x]["Gravada"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Gravada"]) : 0,
                        Exonerada = (dt.Rows[x]["Exonerada"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Exonerada"]) : 0,
                        ImporteIGV = (dt.Rows[x]["ImporteIGV"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["ImporteIGV"]) : 0,
                        CodEstado = (dt.Rows[x]["CodEstado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodEstado"]) : string.Empty,
                        Total = (dt.Rows[x]["Total"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Total"]) : 0,
                        SaldoCompra = (dt.Rows[x]["Saldo"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Saldo"]) : 0,
                        DetraccionCompra = (dt.Rows[x]["Detraccion"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Detraccion"]) : 0,
                        PagoDetraccion = (dt.Rows[x]["PagoDetraccion"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["PagoDetraccion"]) : false,
                    });
                }

                return ListECMP_Compra;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public List<ECMP_Compra> ListCompraBusqDocumentoCajaBanco(ECMP_Compra ObjECMP_Compra, string FiltrarDocumento, int Condicion)
        {
            try
            {
                objCmpSql = new CmpSql(SGCVariables.ConectionString);
                ListECMP_Compra = new List<ECMP_Compra>();

                objCmpSql.CommandProcedure("spCMP_GET_DocumentoCajaBanco");
                objCmpSql.AddParameter("@IdCliProveedor", SqlDbType.VarChar, ObjECMP_Compra.ObjEMNF_ClienteProveedor.IdCliProveedor);
                objCmpSql.AddParameter("@CodMoneda", SqlDbType.VarChar, (ObjECMP_Compra.ObjESGC_Moneda.CodMoneda == null) ? "%" : ObjECMP_Compra.ObjESGC_Moneda.CodMoneda);
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, (FiltrarDocumento == "" || FiltrarDocumento == null) ? "%" : FiltrarDocumento);
                objCmpSql.AddParameter("@Condicion", SqlDbType.TinyInt, Condicion);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    ListECMP_Compra.Add(new ECMP_Compra
                    {
                        IdCompra = (dt.Rows[x]["IdCompra"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdCompra"]) : 0,
                        ObjEMNF_ClienteProveedor = new EMNF_ClienteProveedor
                        {
                            IdCliProveedor = (dt.Rows[x]["IdCliProveedor"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdCliProveedor"]) : 0,
                            RazonSocial = (dt.Rows[x]["RazonSocial"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["RazonSocial"]) : string.Empty,
                        },
                        ObjESGC_Moneda = new ESGC_Moneda
                        {
                            CodMoneda = (dt.Rows[x]["CodMoneda"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodMoneda"]) : string.Empty,
                            Descripcion = (dt.Rows[x]["Descripcion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Descripcion"]) : string.Empty,
                            Defecto = (dt.Rows[x]["Defecto"] != DBNull.Value) ? Convert.ToBoolean(Convert.ToInt32(dt.Rows[x]["Defecto"])) : false,
                            Simbolo = (dt.Rows[x]["Simbolo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Simbolo"]) : string.Empty,
                        },
                        ObjESGC_Estado = new ESGC_Estado()
                        {
                            Estado = (dt.Rows[x]["Estado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Estado"]) : string.Empty,
                        },
                        TipoCambio = (dt.Rows[x]["TipoCambio"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["TipoCambio"]) : 0,
                        CodDocumento = (dt.Rows[x]["CodDocumento"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodDocumento"]) : string.Empty,
                        DescDocumento = (dt.Rows[x]["DescDocumento"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["DescDocumento"]) : string.Empty,
                        Descripcion = (dt.Rows[x]["DescDocumento"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["DescDocumento"]) : string.Empty,
                        Serie = (dt.Rows[x]["Serie"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Serie"]) : string.Empty,
                        Numero = (dt.Rows[x]["Numero"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Numero"]) : string.Empty,
                        Fecha = (dt.Rows[x]["Fecha"] != DBNull.Value) ? Convert.ToDateTime(dt.Rows[x]["Fecha"]) : DateTime.Now,
                        IGV = (dt.Rows[x]["IGV"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["IGV"]) : 0,
                        IncluyeIGV = (dt.Rows[x]["IncluyeIGV"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["IncluyeIGV"]) : false,
                        Anticipo = (dt.Rows[x]["Anticipo"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["Anticipo"]) : false,
                        Gravada = (dt.Rows[x]["Gravada"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Gravada"]) : 0,
                        Exonerada = (dt.Rows[x]["Exonerada"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Exonerada"]) : 0,
                        ImporteIGV = (dt.Rows[x]["ImporteIGV"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["ImporteIGV"]) : 0,
                        CodEstado = (dt.Rows[x]["CodEstado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodEstado"]) : string.Empty,
                        Total = (dt.Rows[x]["Total"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Total"]) : 0,
                        SaldoCompra = (dt.Rows[x]["Saldo"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Saldo"]) : 0,
                        DetraccionCompra = (dt.Rows[x]["Detraccion"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Detraccion"]) : 0,
                        PagoDetraccion = (dt.Rows[x]["PagoDetraccion"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["PagoDetraccion"]) : false,
                    });
                }

                return ListECMP_Compra;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public List<ECMP_Compra> ListAdministrarCompraAnticipo(ECMP_Compra ObjECMP_Compra)
        {
            try
            {
                objCmpSql = new CmpSql(SGCVariables.ConectionString);
                ListECMP_Compra = new List<ECMP_Compra>();
                objCmpSql.CommandProcedure("spCMP_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "FiltrarCompraAnticipo");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, ObjECMP_Compra.IdCompra);

                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    ListECMP_Compra.Add(new ECMP_Compra()
                    {
                        IdCompra = (dt.Rows[x]["IdCompraAnticipo"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdCompraAnticipo"]) : 0,
                        Fecha = (dt.Rows[x]["Fecha"] != DBNull.Value) ? Convert.ToDateTime(dt.Rows[x]["Fecha"]) : DateTime.Now,
                        Descripcion = (dt.Rows[x]["Descripcion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Descripcion"]) : string.Empty,
                        SerieNumero = (dt.Rows[x]["SerieNumero"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["SerieNumero"]) : string.Empty,
                        ObjESGC_Estado = new ESGC_Estado()
                        {
                            Estado = (dt.Rows[x]["Estado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Estado"]) : string.Empty,
                        }
                    });
                }

                return ListECMP_Compra;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public List<ECMP_Compra> ListCompraBusqDocumentoAnticipo(ECMP_Compra ObjECMP_Compra, string FiltrarDocumento)
        {
            try
            {
                objCmpSql = new CmpSql(SGCVariables.ConectionString);
                ListECMP_Compra = new List<ECMP_Compra>();

                objCmpSql.CommandProcedure("spCMP_GET_DocumentoAnticipo");
                objCmpSql.AddParameter("@IdCliProveedor", SqlDbType.VarChar, ObjECMP_Compra.ObjEMNF_ClienteProveedor.IdCliProveedor);
                objCmpSql.AddParameter("@CodMoneda", SqlDbType.VarChar, (ObjECMP_Compra.ObjESGC_Moneda.CodMoneda == null) ? "%" : ObjECMP_Compra.ObjESGC_Moneda.CodMoneda);
                objCmpSql.AddParameter("@FiltrarDocumento", SqlDbType.VarChar, (FiltrarDocumento == "" || FiltrarDocumento == null) ? "%" : FiltrarDocumento);
                DataTable dt = objCmpSql.ExecuteDataTable();
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    ListECMP_Compra.Add(new ECMP_Compra
                    {
                        IdCompra = (dt.Rows[x]["IdCompra"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdCompra"]) : 0,
                        ObjEMNF_ClienteProveedor = new EMNF_ClienteProveedor
                        {
                            IdCliProveedor = (dt.Rows[x]["IdCliProveedor"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdCliProveedor"]) : 0,
                            RazonSocial = (dt.Rows[x]["RazonSocial"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["RazonSocial"]) : string.Empty,
                        },
                        ObjESGC_Moneda = new ESGC_Moneda
                        {
                            CodMoneda = (dt.Rows[x]["CodMoneda"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodMoneda"]) : string.Empty,
                            Descripcion = (dt.Rows[x]["Descripcion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Descripcion"]) : string.Empty,
                            Defecto = (dt.Rows[x]["Defecto"] != DBNull.Value) ? Convert.ToBoolean(Convert.ToInt32(dt.Rows[x]["Defecto"])) : false,
                            Simbolo = (dt.Rows[x]["Simbolo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Simbolo"]) : string.Empty,
                        },
                        ObjESGC_Estado = new ESGC_Estado()
                        {
                            Estado = (dt.Rows[x]["Estado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Estado"]) : string.Empty,
                        },
                        TipoCambio = (dt.Rows[x]["TipoCambio"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["TipoCambio"]) : 0,
                        CodDocumento = (dt.Rows[x]["CodDocumento"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodDocumento"]) : string.Empty,
                        DescDocumento = (dt.Rows[x]["DescDocumento"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["DescDocumento"]) : string.Empty,
                        Descripcion = (dt.Rows[x]["DescDocumento"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["DescDocumento"]) : string.Empty,
                        Serie = (dt.Rows[x]["Serie"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Serie"]) : string.Empty,
                        Numero = (dt.Rows[x]["Numero"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Numero"]) : string.Empty,
                        Fecha = (dt.Rows[x]["Fecha"] != DBNull.Value) ? Convert.ToDateTime(dt.Rows[x]["Fecha"]) : DateTime.Now,
                        IGV = (dt.Rows[x]["IGV"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["IGV"]) : 0,
                        IncluyeIGV = (dt.Rows[x]["IncluyeIGV"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["IncluyeIGV"]) : false,
                        Anticipo = (dt.Rows[x]["Anticipo"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["Anticipo"]) : false,
                        Gravada = (dt.Rows[x]["Gravada"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Gravada"]) : 0,
                        Exonerada = (dt.Rows[x]["Exonerada"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Exonerada"]) : 0,
                        ImporteIGV = (dt.Rows[x]["ImporteIGV"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["ImporteIGV"]) : 0,
                        CodEstado = (dt.Rows[x]["CodEstado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodEstado"]) : string.Empty,
                        Total = (dt.Rows[x]["Total"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Total"]) : 0,
                        SaldoCompra = (dt.Rows[x]["Saldo"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Saldo"]) : 0,
                        DetraccionCompra = (dt.Rows[x]["Detraccion"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Detraccion"]) : 0,
                        PagoDetraccion = (dt.Rows[x]["PagoDetraccion"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["PagoDetraccion"]) : false,
                    });
                }

                return ListECMP_Compra;
            }

            catch (Exception)
            {
                throw;
            }
        }

    }
}
