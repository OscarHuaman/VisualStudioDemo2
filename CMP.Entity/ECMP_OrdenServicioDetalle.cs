/*********************************************************
'* ENTIDAD PARA LA TABLA ORDEN SERVICIO DETALLE
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   JUAN LUIS GUERRA MENESES
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
    public class ECMP_OrdenServicioDetalle
    {
        public ECMP_OrdenServicio ObjECMP_OrdenServicio { get; set; }
        public int Item { get; set; }
        public EMNF_Servicio ObjEMNF_Servicio { get; set; }

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

        public decimal Importe { get; set; }
        public decimal ImporteIGV { get; set; }
        public int IdDestino { get; set; }
        public string PeriodoCampania { get; set; }

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
        public string Observaciones { get; set; }

        public List<ECMP_ValueComboBox> ListCentroCosto { get; set; }
        public List<ECMP_ValueComboBox> ListPeriodoCampania { get; set; }

        public ECMP_OrdenServicioDetalle() 
        {
            PrecioUnitario = (decimal)0.0;
            Cantidad = 0;
            //ObjEMNF_Servicio = new EMNF_Servicio();
        }

    }
}
