/*********************************************************
'* ENTIDAD PARA LA TABLA ORDEN SERVICIO
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   JUAN LUIS GUERRA MENESES
'* FCH. CREACIÓN : 18/11/2015
**********************************************************/
namespace CMP.Entity
{
    using ALM.Entity;
    using MNF.Entity;
    using SGC.Empresarial.Entity;
    using System;
using System.Collections.Generic;

    [Serializable]
    public class ECMP_OrdenServicio
    {
        public string Opcion { get; set; }
        public int IdOrdenServicio { get; set; }
        public ESGC_EmpresaSucursal ObjESGC_EmpresaSucursal { get; set; }
        public ESGC_Area ObjESGC_Area { get; set; }
        public EMNF_ClienteProveedor ObjEMNF_ClienteProveedor { get; set; }
        public ESGC_FormaPago ObjESGC_FormaPago { get; set; }
        public ESGC_Estado ObjESGC_Estado { get; set; }
        public ESGC_Moneda ObjESGC_Moneda { get; set; }
        public ESGC_Documento ObjESGC_Documento { get; set; }
        public string DocumenSerie { get; set; }
        public string Periodo { get; set; }
        public Int32 DiasRetraso { get; set; }
        private DateTime fecha = DateTime.Now;
        public DateTime Fecha
        {
            get
            {
                return fecha;
            }
            set 
            {
                if (value > DateTime.Now)
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

        public DateTime FechaInicio { get; set; }

        private DateTime fechaFin = DateTime.Now;
        public DateTime FechaFin
        {
            get { return fechaFin; }
            set 
            {
                if (value < FechaInicio)
                {
                    fechaFin = FechaInicio;
                }
                else
                {
                    fechaFin = value;
                }
            }
        }

        public string Contacto { get; set; }
        public EMNF_TipoDestino ObjEMNF_TipoDestino { get; set; }
        public decimal TipoCambio { get; set; }
        public decimal Gravada { get; set; }
        public decimal IGV { get; set; }
        public decimal ImporteIGV { get; set; }
        public int Exonerado { get; set; }
        public bool Retencion { get; set; }
        public string CadenaXML { get; set; }

        public string Creacion { get; set; }
        public string Aprobacion { get; set; }

        public ECMP_OrdenServicio()
        {
            this.Fecha = DateTime.Now;
            this.FechaInicio = DateTime.Now;
            this.FechaFin = DateTime.Now;
            this.TipoCambio = 1;
            this.Exonerado = 11;
        }

        public ECMP_OrdenServicio(object DataGridSelectedItem, TipoConstructor MyTipoConstructor)
        {
            if (MyTipoConstructor == TipoConstructor.Insert || DataGridSelectedItem == null)
            {
                this.Opcion = "I";
                this.Fecha = DateTime.Now;
                this.FechaInicio = DateTime.Now;
                this.FechaFin = DateTime.Now;
                this.TipoCambio = 1;
                this.Exonerado = 11;
            }
            else
            {
                if (DataGridSelectedItem is ECMP_OrdenServicio)
                {
                    var ObjECMP_OrdenServicio = (ECMP_OrdenServicio)DataGridSelectedItem;
                    this.Opcion = (MyTipoConstructor == TipoConstructor.Update) ? "U" : "D";
                    this.IdOrdenServicio = ObjECMP_OrdenServicio.IdOrdenServicio;
                    this.ObjESGC_EmpresaSucursal = ObjECMP_OrdenServicio.ObjESGC_EmpresaSucursal;
                    this.ObjESGC_Area = ObjECMP_OrdenServicio.ObjESGC_Area;
                    this.ObjEMNF_ClienteProveedor = ObjECMP_OrdenServicio.ObjEMNF_ClienteProveedor;
                    this.ObjEMNF_TipoDestino = ObjECMP_OrdenServicio.ObjEMNF_TipoDestino;
                    this.ObjESGC_FormaPago = ObjECMP_OrdenServicio.ObjESGC_FormaPago;
                    this.ObjESGC_Estado = ObjECMP_OrdenServicio.ObjESGC_Estado;
                    this.ObjESGC_Moneda = ObjECMP_OrdenServicio.ObjESGC_Moneda;
                    this.ObjESGC_Documento = ObjECMP_OrdenServicio.ObjESGC_Documento;
                    this.Periodo = ObjECMP_OrdenServicio.Periodo;
                    this.Fecha = ObjECMP_OrdenServicio.Fecha;
                    this.Serie = ObjECMP_OrdenServicio.Serie;
                    this.Numero = ObjECMP_OrdenServicio.Numero;
                    this.FechaInicio = ObjECMP_OrdenServicio.FechaInicio;
                    this.FechaFin = ObjECMP_OrdenServicio.FechaFin;
                    this.Contacto = ObjECMP_OrdenServicio.Contacto;
                    this.TipoCambio = ObjECMP_OrdenServicio.TipoCambio;
                    this.Gravada = ObjECMP_OrdenServicio.Gravada;
                    this.IGV = ObjECMP_OrdenServicio.IGV;
                    this.ImporteIGV = ObjECMP_OrdenServicio.ImporteIGV;
                    this.Exonerado = ObjECMP_OrdenServicio.Exonerado;
                    this.Retencion = ObjECMP_OrdenServicio.Retencion;
                }
            }
        }

    }
}
 