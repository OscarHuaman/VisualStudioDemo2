/*********************************************************
'* ENTIDAD PARA LA TABLA ORDEN COMPRA DETALLE
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 18/11/2015
**********************************************************/
namespace CMP.Entity
{
    using MNF.Entity;
    using SGC.Empresarial.Entity;
    using System.Collections.Generic;
    using System;
    using System.ComponentModel;

    [Serializable]
    public class ECMP_OrdenCompraDetalle
    {
        public ECMP_OrdenCompra ObjECMP_OrdenCompra { get; set; }
        public int Item { get; set; }
        public EMNF_Articulo ObjEMNF_Articulo { get; set; }
        public ESGC_Estado ObjESGC_Estado { get; set; }
       
        private decimal _PrecioUnitario;
        public decimal PrecioUnitario 
        {
            get 
            {
                return _PrecioUnitario;
            }
            set 
            {
                if (value <= 9999999999)
                {
                    _PrecioUnitario = value;
                }
            }
        }

		private decimal _PrecioUnitarioTemp;
        public decimal PrecioUnitarioTemp
        {
            get
            {
                return _PrecioUnitarioTemp;
            }
            set
            {
                if (value <= 9999999999)
                {
                    _PrecioUnitarioTemp = value;
                }
            }
        }
        private decimal _Cantidad;
        public decimal Cantidad 
        {
            get 
            {
                return _Cantidad;
            }
            set 
            {
                if (value <= 9999999999)
                {
                    _Cantidad = value;
                }
            }
        }

        public decimal CantidadRecep { get; set; }

        public decimal ImporteIGV { get; set; }
        public decimal Importe { get; set; }

        public uint Provisionado { get; set; }

        //Procesos en grilla
        public List<ESGC_Estado> ListEstado { get; set; }
        public bool IsEnableEstado { get; set; }

        public ECMP_OrdenCompraDetalle() 
        {
            ObjESGC_Estado = new ESGC_Estado() { CodEstado = "PEDOC" };
            this.PrecioUnitario = (decimal)0.0;
            this.Cantidad = 0;
            this.IsEnableEstado = false;
        }
    }
}
