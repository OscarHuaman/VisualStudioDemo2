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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Linq;

    public class BCMP_NotaCreditoDebitoDetalle
    {
        private CmpSql ObjCmpSql;

        public ObservableCollection<ECMP_NotaCreditoDebitoDetalle> GetECMP_NotaCreditoDebitoDetalle(ECMP_NotaCreditoDebito ObjECMP_NotaCreditoDebito)
        {
            ObjCmpSql = new CmpSql(SGCVariables.ConectionString);
            ObjCmpSql.CommandProcedure("spCMP_GET_BusquedaGeneral");
            ObjCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "AdministrarNotaCreditoDebitoDetalle");
            ObjCmpSql.AddParameter("@Filtro", SqlDbType.Int, ObjECMP_NotaCreditoDebito.IdNotaCreDeb);
            DataTable dt = ObjCmpSql.ExecuteDataTable();

            var ListECMP_NotaCreditoDebitoDetalle = new ObservableCollection<ECMP_NotaCreditoDebitoDetalle>();

            for (int x = 0; x < dt.Rows.Count; x++)
            {
                ListECMP_NotaCreditoDebitoDetalle.Add(new ECMP_NotaCreditoDebitoDetalle
                {
                    ObjECMP_CompraDetalle=new ECMP_CompraDetalle()
                    {
                        Codigo = (dt.Rows[x]["Codigo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Codigo"]) : string.Empty,
                        ArticuloServicio = (dt.Rows[x]["ArticuloServicio"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["ArticuloServicio"]) : string.Empty,
                        CodUndMedida = (dt.Rows[x]["CodUndMedida"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodUndMedida"]) : string.Empty,
                        Cantidad = (dt.Rows[x]["Cantidad"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Cantidad"]) : (decimal)0.0,
                        PrecioUnitario = (dt.Rows[x]["PrecioUnitario"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["PrecioUnitario"]) : (decimal)0.0,
                        Importe=(dt.Rows[x]["Importe"]!=DBNull.Value)?Convert.ToDecimal(dt.Rows[x]["Importe"]):(decimal)0.0,
                        ImporteIGV = (dt.Rows[x]["ImporteIGV"]!=DBNull.Value)?Convert.ToDecimal(dt.Rows[x]["ImporteIGV"]):(decimal)0.0,
						CodOperacionIGV = (dt.Rows[x]["CodOperacionIGV"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodOperacionIGV"]) : string.Empty,
                        ObjECMP_Compra = new ECMP_Compra()
                        {                        
                            IdCompra = (dt.Rows[x]["IdCompra"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdCompra"]) : 0,
                            SerieNumero = (dt.Rows[x]["SerieNumero"]!=DBNull.Value)?Convert.ToString(dt.Rows[x]["SerieNumero"]):string.Empty,
                            IGV = (dt.Rows[x]["IGV"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["IGV"]) : 0,
							IncluyeIGV = (dt.Rows[x]["IncluyeIGV"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["IncluyeIGV"]) : false
                        },
                    
                    },
                    ArticuloServicio = (dt.Rows[x]["ArticuloServicio"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["ArticuloServicio"]) : string.Empty,
                    Item = ListECMP_NotaCreditoDebitoDetalle.Count+1,
                    Importe = (dt.Rows[x]["Importe"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Importe"]) : 0,
                    TempImporte = (dt.Rows[x]["Importe"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Importe"]) : 0,
                    CantidaDevolver = (dt.Rows[x]["CantidadDevolver"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["CantidadDevolver"]) : (decimal)0.0,
                    PrcDscBonificacion = (dt.Rows[x]["PrcDscBonificacion"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["PrcDscBonificacion"]) : (decimal)0.0,
                    ImpDscBonificacion = (dt.Rows[x]["ImpDscBonificacion"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["ImpDscBonificacion"]) : (decimal)0.0,
                    PreDscBoniOmision = (dt.Rows[x]["PreDscBoniOmision"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["PreDscBoniOmision"]) : (decimal)0.0,
                });
            }

            return ListECMP_NotaCreditoDebitoDetalle;
        
        }
     }
}
