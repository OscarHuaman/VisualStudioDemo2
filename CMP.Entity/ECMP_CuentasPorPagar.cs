/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 23/02/2016
**********************************************************/
namespace CMP.Entity
{
    using SGC.Empresarial.Entity;
    using System;

    public class ECMP_CuentasPorPagar
    {
        public DateTime Fecha { get; set; }
        public ESGC_Documento ObjESGC_Documento { get; set; }
        public ESGC_Moneda ObjESGC_Moneda { get; set; }
        public string SerieDocumento { get; set; }
        public string NroDocumento { get; set; }
		public string Numero { get; set; }
        public int IdCliProveedor { get; set; }
        public string Proveedor { get; set; }
        public string NroDocIdentidad { get; set; }
        public decimal Debe { get; set; }
        public decimal Haber { get; set; }
        public decimal Saldo_SOL { get; set; }
        public decimal Saldo_USD { get; set; }
    }
}
