/*********************************************************
'* ENTIDAD PARA LA TABLA COMPRA DETALLE
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
    using ALM.Entity;
    using ComputerSystems;

    [Serializable]
    public class ECMP_CompraDetalle : CmpNotifyPropertyChanged
    {
        public ECMP_Compra ObjECMP_Compra { get; set; }
        public int Item { get; set; }
        public int IdArticuloServicio { get; set; }
        public int IdReferencia { get; set; }
        public EALM_Almacen ObjEALM_Almacen { get; set; }
        public string TipoDetalle { get; set; }
        public string CodUndMedida { get; set; }

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

        public decimal ImporteIGV { get; set; }

        private decimal _Importe;
        public decimal Importe 
        {
            get 
            {
                return _Importe;
            }
            set 
            {
                _Importe = value;
                OnPropertyChanged("Importe");
            }
        }

        //AGREGADO
        public List<EALM_Almacen> ListEALM_Almacen { get; set; }
        public string Codigo { get; set; }
        public string ArticuloServicio { get; set; }
        public string SerieNumero { get; set; }
        public string CodOperacionIGV { get; set; }
        public bool IsEnabledColumnPrecioUnitario { get; set; }
        public bool IsEnabledColumnCantidad { get; set; }
        public bool IsEnabledColumnAlmcen { get; set; }
        public decimal MaxCantidad { get; set; }
        public int IdEmpSucursal { get; set; }
        public int IdDestino { get; set; }
        public string PeriodoCampania { get; set; }
        public List<ECMP_ValueComboBox> ListCentroCosto { get; set; }
        public List<ECMP_ValueComboBox> ListPeriodoCampania { get; set; }
        //public EMNF_Articulo ObjEMNF_Articulo { get; set; }
        //public EMNF_Servicio ObjEMNF_Servicio { get; set; }

        public ECMP_CompraDetalle() 
        {
            this.ObjEALM_Almacen = new EALM_Almacen() { IdAlmacen = -1};
            this.IsEnabledColumnPrecioUnitario = true;
            this.IsEnabledColumnAlmcen = true;
            this.IsEnabledColumnCantidad = true;
            this.PrecioUnitario = (decimal)0.0;
            this.Cantidad = (decimal)0.0;
            this.MaxCantidad = (decimal)99999;
        }
    }
}
