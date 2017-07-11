/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 23/02/2016
**********************************************************/
namespace CMP.Business
{
    using CMP.Entity;
    using ComputerSystems.DataAccess.LibrarySql;
    using SGC.Empresarial.Entity;
    using SGC.Empresarial.Useful.Modulo;
    using System;
    using System.Collections.ObjectModel;
    using System.Data;

    public class BCMP_CuentasPorPagar
    {
        private CmpSql objCmpSql;

        public ObservableCollection<ECMP_CuentasPorPagar> ListCuentasPorPagar(int IdCliProveedor, int BusqFecha, DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                objCmpSql = new CmpSql(SGCVariables.ConectionString);
                var ListECMP_CuentaCorrientedeProveedor = new ObservableCollection<ECMP_CuentasPorPagar>();
                objCmpSql.CommandProcedure("spCMP_GET_CuentasPorPagar");
                objCmpSql.AddParameter("@IdCliProveedor", SqlDbType.Int, IdCliProveedor);
                objCmpSql.AddParameter("@BusqFecha", SqlDbType.Bit, BusqFecha);
                objCmpSql.AddParameter("@Fecha_Ini", SqlDbType.SmallDateTime, (FechaInicio.Year <= 1900) ? DateTime.Now : FechaInicio);
                objCmpSql.AddParameter("@Fecha_Fin", SqlDbType.SmallDateTime, (FechaFin.Year <= 1900) ? DateTime.Now : FechaFin);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    ListECMP_CuentaCorrientedeProveedor.Add(new ECMP_CuentasPorPagar()
                    {
                        Fecha = (dt.Rows[x]["Fecha"] != DBNull.Value) ? Convert.ToDateTime(dt.Rows[x]["Fecha"]) : DateTime.Now,
                        IdCliProveedor = (dt.Rows[x]["IdCliProveedor"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdCliProveedor"]) : 0,
                        Proveedor = (dt.Rows[x]["Proveedor"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Proveedor"]) : string.Empty,
                        NroDocIdentidad = (dt.Rows[x]["NroDocIdentidad"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["NroDocIdentidad"]) : string.Empty,
                        ObjESGC_Documento = new ESGC_Documento()
                        {
                            Descripcion = (dt.Rows[x]["Documento"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Documento"]) : string.Empty,
                            CodDocumento = (dt.Rows[x]["CodDocumento"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodDocumento"]) : string.Empty,
                        },
                        ObjESGC_Moneda = new ESGC_Moneda()
                        {
                            CodMoneda = (dt.Rows[x]["CodMoneda"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodMoneda"]) : string.Empty,
                            Descripcion = (dt.Rows[x]["Moneda"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Moneda"]) : string.Empty,
                            Simbolo = (dt.Rows[x]["Simbolo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Simbolo"]) : string.Empty,
                        },
                        SerieDocumento = (dt.Rows[x]["Serie"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Serie"]) : string.Empty,
                        Numero = (dt.Rows[x]["Numero"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Numero"]) : string.Empty,
						NroDocumento = (dt.Rows[x]["NroDocumento"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["NroDocumento"]) : string.Empty,
                        Debe = (dt.Rows[x]["Debe"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Debe"]) : 0,
                        Haber = (dt.Rows[x]["Haber"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Haber"]) : 0,
                        Saldo_SOL = (dt.Rows[x]["Saldo_SOL"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Saldo_SOL"]) : 0,
                        Saldo_USD = (dt.Rows[x]["Saldo_USD"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Saldo_USD"]) : 0,
                        
                    });
                }
                return ListECMP_CuentaCorrientedeProveedor;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
