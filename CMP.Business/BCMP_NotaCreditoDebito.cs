/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 11/02/2016
**********************************************************/
namespace CMP.Business
{
    using CMP.Entity;
    using ComputerSystems.DataAccess.LibrarySql;
    using MNF.Entity;
    using SGC.Empresarial.Entity;
    using SGC.Empresarial.Useful.Modulo;
    using System;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public class BCMP_NotaCreditoDebito
    {
        private CmpSql ObjCmpSql;

        public BCMP_NotaCreditoDebito()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-PE");
        }

        public void TransNotaCreditoDebito(ECMP_NotaCreditoDebito ObjECMP_NotaCreditoDebito)
        {
            try
            {
                ObjCmpSql = new CmpSql(SGCVariables.ConectionString);
                ObjCmpSql.CommandProcedure("spCMP_SET_NotaCreditoDebito");
                ObjCmpSql.AddParameter("@Opcion", SqlDbType.Char, ObjECMP_NotaCreditoDebito.Opcion);
                ObjCmpSql.AddParameter("@IdNotaCreDeb", SqlDbType.Int, ObjECMP_NotaCreditoDebito.IdNotaCreDeb);
                ObjCmpSql.AddParameter("@IdMotCreDeb", SqlDbType.Int, ObjECMP_NotaCreditoDebito.ObjEMNF_MotivoNotaCreditoDebito.IdMotCreDeb);
                ObjCmpSql.AddParameter("@IdCliProveedor", SqlDbType.Int, ObjECMP_NotaCreditoDebito.ObjEMNF_ClienteProveedor.IdCliProveedor);
                ObjCmpSql.AddParameter("@CodEstado", SqlDbType.VarChar, ObjECMP_NotaCreditoDebito.ObjESGC_Estado.CodEstado);
                ObjCmpSql.AddParameter("@CodMoneda", SqlDbType.Char, ObjECMP_NotaCreditoDebito.ObjESGC_Moneda.CodMoneda);
                ObjCmpSql.AddParameter("@CodDocumento", SqlDbType.Char, ObjECMP_NotaCreditoDebito.CodDocumento);
                ObjCmpSql.AddParameter("@Serie", SqlDbType.Char, ObjECMP_NotaCreditoDebito.Serie);
                ObjCmpSql.AddParameter("@Numero", SqlDbType.VarChar, ObjECMP_NotaCreditoDebito.Numero);
                ObjCmpSql.AddParameter("@Fecha", SqlDbType.SmallDateTime, ObjECMP_NotaCreditoDebito.Fecha.ToShortDateString());
                ObjCmpSql.AddParameter("@Periodo", SqlDbType.Char, ObjECMP_NotaCreditoDebito.Periodo);
                ObjCmpSql.AddParameter("@TipoCambio", SqlDbType.Decimal, ObjECMP_NotaCreditoDebito.TipoCambio);
                ObjCmpSql.AddParameter("@Exonerada", SqlDbType.Decimal, ObjECMP_NotaCreditoDebito.Exonerada);
                ObjCmpSql.AddParameter("@Gravada", SqlDbType.Decimal, ObjECMP_NotaCreditoDebito.Gravada);
                ObjCmpSql.AddParameter("@IGV", SqlDbType.Decimal, ObjECMP_NotaCreditoDebito.IGV);
                ObjCmpSql.AddParameter("@ImporteIGV", SqlDbType.Decimal, ObjECMP_NotaCreditoDebito.ImporteIGV);
                ObjCmpSql.AddParameter("@Glosa", SqlDbType.VarChar, ObjECMP_NotaCreditoDebito.Glosa);
                ObjCmpSql.AddParameter("@AfectaAlmacen", SqlDbType.Bit, ObjECMP_NotaCreditoDebito.AfectaAlmacen);
                ObjCmpSql.AddParameter("@DocumentoRefXML", SqlDbType.NText, ObjECMP_NotaCreditoDebito.DocuRefXML);
                ObjCmpSql.AddParameter("@DetalleXML", SqlDbType.NText, ObjECMP_NotaCreditoDebito.DetalleXML);
                ObjCmpSql.AddParameter("@IdUsuario", SqlDbType.VarChar, SGCVariables.ObjESGC_Usuario.IdUsuario);
                ObjCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private ObservableCollection<ECMP_NotaCreditoDebito> ListECMP_NotaCreditoDebito;
        public ObservableCollection<ECMP_NotaCreditoDebito> GETNotaCreditoDebito(string Opcion, ECMP_NotaCreditoDebito ObjECMP_NotaCreditoDebito)
        {
            try
            {
                ListECMP_NotaCreditoDebito = new ObservableCollection<ECMP_NotaCreditoDebito>();
                ObjCmpSql = new CmpSql(SGCVariables.ConectionString);
                ObjCmpSql.CommandProcedure("spCMP_GET_NotaCreditoDebito");
                ObjCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, Opcion);
                ObjCmpSql.AddParameter("@FechaIni", SqlDbType.SmallDateTime, ObjECMP_NotaCreditoDebito.FechaInicio.ToShortDateString());
                ObjCmpSql.AddParameter("@FechaFin", SqlDbType.SmallDateTime, ObjECMP_NotaCreditoDebito.FechaHasta.ToShortDateString());
                ObjCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, ObjECMP_NotaCreditoDebito.Filtro.Replace("'",string.Empty));
                DataTable dt = ObjCmpSql.ExecuteDataTable();
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                   ListECMP_NotaCreditoDebito.Add(new ECMP_NotaCreditoDebito
                      {
                            IdNotaCreDeb = (dt.Rows[x]["IdNotaCreDeb"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdNotaCreDeb"]) : 0,
                            ObjEMNF_MotivoNotaCreditoDebito = new EMNF_MotivoNotaCreditoDebito()
                            {
                                IdMotCreDeb = (dt.Rows[x]["IdMotCreDeb"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdMotCreDeb"]) :0,
                               Motivo = (dt.Rows[x]["Motivo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Motivo"]) : string.Empty,
                                CodMotivo = (dt.Rows[x]["CodMotivo"]!=DBNull.Value)?Convert.ToString(dt.Rows[x]["CodMotivo"]):string.Empty
                            },
                            ObjESGC_Moneda = new SGC.Empresarial.Entity.ESGC_Moneda()
                            {
                                CodMoneda = (dt.Rows[x]["CodMoneda"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodMoneda"]) : string.Empty,
                                Descripcion = (dt.Rows[x]["Descripcion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Descripcion"]) : string.Empty,
                                Simbolo=(dt.Rows[x]["Simbolo"]!=DBNull.Value)?Convert.ToString(dt.Rows[x]["Simbolo"]):string.Empty
                            },
                            ObjESGC_Estado=new ESGC_Estado()
                            {
                                CodEstado=(dt.Rows[x]["CodEstado"]!=DBNull.Value)?Convert.ToString(dt.Rows[x]["CodEstado"]):string.Empty,
                                Estado=(dt.Rows[x]["Estado"]!=DBNull.Value)?Convert.ToString(dt.Rows[x]["Estado"]):string.Empty
                            },
                            CodDocumento = (dt.Rows[x]["CodDocumento"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodDocumento"]) : string.Empty,
                            Serie = (dt.Rows[x]["Serie"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Serie"]) : string.Empty,
                            Numero = (dt.Rows[x]["Numero"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Numero"]) : string.Empty,
                            CodDocumentoSerieNumero = (dt.Rows[x]["CodDocumentoSerieNumero"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodDocumentoSerieNumero"]) : string.Empty,
                            Fecha = (dt.Rows[x]["Fecha"] != DBNull.Value) ? Convert.ToDateTime(dt.Rows[x]["Fecha"]) : DateTime.Now,
                            Periodo = (dt.Rows[x]["Periodo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Periodo"]) : string.Empty,
                            TipoCambio = (dt.Rows[x]["TipoCambio"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["TipoCambio"]) : 0,
                            Exonerada = (dt.Rows[x]["Exonerada"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Exonerada"]) : 0,
                            Gravada = (dt.Rows[x]["Gravada"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Gravada"]) : 0,
                            IGV = (dt.Rows[x]["IGV"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["IGV"]) : 0,
                            ImporteIGV = (dt.Rows[x]["ImporteIGV"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["ImporteIGV"]) : 0,
                            Glosa = (dt.Rows[x]["Glosa"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Glosa"]) : string.Empty,
                            ObjEMNF_ClienteProveedor = new EMNF_ClienteProveedor()
                            {
                                IdCliProveedor = (dt.Rows[x]["IdCliProveedor"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdCliProveedor"]) : 0,
                                RazonSocial = (dt.Rows[x]["RazonSocial"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["RazonSocial"]) : string.Empty,
                                NroDocIdentidad = (dt.Rows[x]["NroDocIdentidad"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["NroDocIdentidad"]) : string.Empty,
                                Direccion = (dt.Rows[x]["Direccion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Direccion"]) : string.Empty
                            },
                            AfectaAlmacen = (dt.Rows[x]["AfectaAlmacen"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["AfectaAlmacen"]) : false,
                            });
                }
                return ListECMP_NotaCreditoDebito;
            }
            catch (Exception)
            {
                throw;
            }
          
        }
    }
}

