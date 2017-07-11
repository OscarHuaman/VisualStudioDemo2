/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 11/02/2016
**********************************************************/
namespace CMP.Entity
{
    public class ECMP_DetailAccounts
    {
        public int Row { get; set; }
        public int IdCompra { get; set; }
        public string Fecha { get; set; }
        public string Descripcion { get; set; }
        public string NroDocumento { get; set; }
        public string Moneda { get; set; }
        public string Debe { get; set; }
        public string Haber { get; set; }
        public string Saldo { get; set; }
        public string Sucursal { get; set; }
        public string Proveedor { get; set; }
        public string Tipo { get; set; }
    }
}
