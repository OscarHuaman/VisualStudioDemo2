/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 11/02/2016
**********************************************************/
namespace CMP.Entity
{
    using ComputerSystems;
using MNF.Entity;
using SGC.Empresarial.Entity;
using System;

    [Serializable]
    public class ECMP_NotaCreditoDebitoDetalle:CmpNotifyPropertyChanged
    {
        private decimal _TempImporte;
        public decimal TempImporte
        {
            get
            {
                return _TempImporte;
            }
            set
            {
                _TempImporte = value;
            }
        }
        private int _Item;
        public int Item
        { 
            get { return _Item; } 
            set 
            {
                _Item = value;
                OnPropertyChanged("Item"); 
            }
        }
        public ECMP_NotaCreditoDebito ObjECMP_NotaCreditoDebito { get; set; }
        public ECMP_Compra ObjECMP_Compra { get; set; }
        private ECMP_CompraDetalle _ObjECMP_CompraDetalle;
        public ECMP_CompraDetalle ObjECMP_CompraDetalle{ 
            get
            {
                if (_ObjECMP_CompraDetalle == null)
                    _ObjECMP_CompraDetalle = new ECMP_CompraDetalle();
                return _ObjECMP_CompraDetalle;
            }
            set
            {
                _ObjECMP_CompraDetalle = value;
                OnPropertyChanged("ObjECMP_CompraDetalle");
            }
        }
        private EMNF_DocumentoReferencia _ObjEMNF_DocumentoReferencia;
        public EMNF_DocumentoReferencia ObjEMNF_DocumentoReferencia 
        {
            get
            {
                if (_ObjEMNF_DocumentoReferencia == null)
                    _ObjEMNF_DocumentoReferencia = new EMNF_DocumentoReferencia();
                return _ObjEMNF_DocumentoReferencia;
            }
            set
            {
                _ObjEMNF_DocumentoReferencia = value;
                //OnPropertyChanged("ObjEMNF_DocumentoReferencia");
            }
        }
        public decimal Importe { get; set; }
        //AGREGADOS 18/11/2016
        private decimal _CantidadDevolver;
        public decimal CantidaDevolver
        {
            get
            {
                return Decimal.Round(_CantidadDevolver, 8);
            }
            set
            {
                _CantidadDevolver = value;
                OnPropertyChanged("CantidaDevolver");
            }
        }
        private decimal _PrcDscBonificacion;
        public decimal PrcDscBonificacion
        {
            get
            {
                return Decimal.Round(_PrcDscBonificacion, 8);
            }
            set
            {
                _PrcDscBonificacion = value;
                OnPropertyChanged("PrcDscBonificacion");
            }
        }
        private decimal _ImpDscBonificacion;
        public decimal ImpDscBonificacion
        {
            get
            {
                return Decimal.Round(_ImpDscBonificacion, 8);
            }
            set
            {
                _ImpDscBonificacion = value;
                OnPropertyChanged("ImpDscBonificacion");
            }

        }
        private decimal _PreDscBoniOmision;
        public decimal PreDscBoniOmision
        {
            get
            {
                return Decimal.Round(_PreDscBoniOmision, 8);
            }
            set
            {
                _PreDscBoniOmision = value;
                OnPropertyChanged("PreDscBoniOmision");
            }
        }
        private decimal _PrecioOmitido;
        public decimal PrecioOmitido
        {
            get
            {
                return Decimal.Round(_PrecioOmitido, 8);
            }
            set
            {
                _PrecioOmitido = value;
                //OnPropertyChanged("PrecioOmitido");
            }
        }
        private decimal _ImporteMotivo;
        public decimal ImporteMotivo
        {
            get
            {
                return Decimal.Round(_ImporteMotivo, 8);
            }
            set
            {
                _ImporteMotivo = value;
                OnPropertyChanged("ImporteMotivo");
            }
        }

        public string ArticuloServicio { get; set; }
        public string SerieNumero { get; set; }
        public string CodUndMedida { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        
    }
}
