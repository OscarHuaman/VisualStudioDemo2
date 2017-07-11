/*********************************************************
'* ENTIDAD PARA LA TABLA ORDEN COMPRA
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 18/11/2015
**********************************************************/
namespace CMP.Entity
{
    using ALM.Entity;
    using MNF.Entity;
    using SGC.Empresarial.Entity;
    using System;

    [Serializable]
    public class ECMP_OrdenCompra
    {
        public string Opcion { get; set; }
        public int IdOrdenCompra { get; set; }
        public ESGC_EmpresaSucursal ObjESGC_EmpresaSucursal { get; set; }
        public EMNF_ClienteProveedor ObjEMNF_ClienteProveedor { get; set; }
        public EALM_Almacen ObjEALM_Almacen { get; set; }
        public ESGC_FormaPago ObjESGC_FormaPago { get; set; }
        public ESGC_Estado ObjESGC_Estado { get; set; }
        public ESGC_Moneda ObjESGC_Moneda { get; set; }
        public ESGC_Documento ObjESGC_Documento { get; set; }
        public string Periodo { get; set; }
        private DateTime fecha = DateTime.Now;
        public DateTime Fecha 
        {
            get { return fecha; }
            set 
            {
                if(value > DateTime.Now)
                {
                    fecha = DateTime.Now;
                }
                else
                {
                    fecha = value;
                }
            }
        }
        public string Serie { get; set; }
        public string Numero { get; set; }
        public decimal TipoCambio { get; set; }
        public decimal Gravada { get; set; }
        public decimal Exonerada { get; set; }
        public decimal IGV { get; set; }
        public decimal ImporteIGV { get; set; }
        public bool IncluyeIGV { get; set; }
        public uint Provisionado { get; set; }
        private DateTime fechaEntrega = DateTime.Now;
        public DateTime FechaEntrega
        {
            get
            {
                return fechaEntrega;
            }
            set
            {
                if (value < Fecha)
                {
                    fechaEntrega = Fecha;
                }
                else
                {
                    fechaEntrega = value;
                }
            }
        }
        public string LugarEntrega { get; set; }
        public string ProvicionadoText { get; set; }
        public string CadenaXML { get; set; }
        public Int32 DiasRetraso { get; set; }
        public string Aprobacion { get; set; }
        public string Creacion { get; set; }
        public ECMP_OrdenCompra() { }

        //Agregado
        public string DocumenSerie { get; set; }

        public ECMP_OrdenCompra(object DataGridSelectedItem, TipoConstructor MyTipoConstructor)
        {
            if (MyTipoConstructor == TipoConstructor.Insert || DataGridSelectedItem == null)
            {
                this.Opcion = "I";
                this.Fecha = DateTime.Now;
                this.FechaEntrega = DateTime.Now;
                this.TipoCambio = 1;
                this.Serie = string.Empty;
                this.Numero = string.Empty;
            }
            else
            {
                if (DataGridSelectedItem is ECMP_OrdenCompra)
                {
                    var ObjECMP_OrdenCompra = (ECMP_OrdenCompra)DataGridSelectedItem;
                    this.Opcion = (MyTipoConstructor == TipoConstructor.Update) ? "U" : "D";
                    this.IdOrdenCompra = ObjECMP_OrdenCompra.IdOrdenCompra;
                    this.ObjESGC_EmpresaSucursal = ObjECMP_OrdenCompra.ObjESGC_EmpresaSucursal;
                    this.ObjEMNF_ClienteProveedor = ObjECMP_OrdenCompra.ObjEMNF_ClienteProveedor;
                    this.ObjEALM_Almacen = ObjECMP_OrdenCompra.ObjEALM_Almacen;
                    this.ObjESGC_Moneda = ObjECMP_OrdenCompra.ObjESGC_Moneda;
                    this.ObjESGC_Estado = ObjECMP_OrdenCompra.ObjESGC_Estado;
                    this.ObjESGC_FormaPago = ObjECMP_OrdenCompra.ObjESGC_FormaPago;
                    this.ObjESGC_Documento = ObjECMP_OrdenCompra.ObjESGC_Documento;
                    this.Periodo = ObjECMP_OrdenCompra.Periodo;
                    this.Fecha = ObjECMP_OrdenCompra.Fecha;
                    this.Serie = ObjECMP_OrdenCompra.Serie;
                    this.Numero = ObjECMP_OrdenCompra.Numero;
                    this.TipoCambio = ObjECMP_OrdenCompra.TipoCambio;
                    this.Gravada = ObjECMP_OrdenCompra.Gravada;
                    this.Exonerada = ObjECMP_OrdenCompra.Exonerada;
                    this.IGV = ObjECMP_OrdenCompra.IGV;
                    this.ImporteIGV = ObjECMP_OrdenCompra.ImporteIGV;
                    this.IncluyeIGV = ObjECMP_OrdenCompra.IncluyeIGV;
                    this.FechaEntrega = ObjECMP_OrdenCompra.FechaEntrega;
                    this.LugarEntrega = ObjECMP_OrdenCompra.LugarEntrega;
                    this.Creacion = ObjECMP_OrdenCompra.Creacion;
                    this.Aprobacion = ObjECMP_OrdenCompra.Aprobacion;
                }
            }
        }
    }
}
