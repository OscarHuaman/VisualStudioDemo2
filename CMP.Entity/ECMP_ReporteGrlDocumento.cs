/*********************************************************
'* ENTIDAD PARA REPORTE GENERAL DE DOCUMENTO
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   JUAN GUERRA MENESES
'* FCH. CREACIÓN : 23/01/2016
**********************************************************/
namespace CMP.Entity
{
    using MNF.Entity;
    using SGC.Empresarial.Entity;
    using System;
    using ComputerSystems;
    [Serializable]
    public class ECMP_ReporteGrlDocumento : CmpNotifyPropertyChanged
    {
        public string Opcion { get; set; }
        public string Periodo { get; set; }
        private EMNF_ClienteProveedor _ObjEMNF_ClienteProveedor; 
        public EMNF_ClienteProveedor ObjEMNF_ClienteProveedor 
        {
            get 
            {
                return _ObjEMNF_ClienteProveedor; 
            }
            set 
            {
                _ObjEMNF_ClienteProveedor = value;
                OnPropertyChanged();
            }
        }
        private ESGC_Moneda _ObjESGC_Moneda;
        public ESGC_Moneda ObjESGC_Moneda 
        {
            get 
            {
                return _ObjESGC_Moneda;
            }
            set 
            {
                _ObjESGC_Moneda = value;

                OnPropertyChanged("ObjESGC_Moneda");
            }
        }
        private ESGC_Documento _ObjESGC_Documento;
        public ESGC_Documento ObjESGC_Documento 
        {
            get 
            {
                return _ObjESGC_Documento;
            }
            set 
            {
                _ObjESGC_Documento = value;
                OnPropertyChanged();
            }
        }
        private EMNF_Periodo _ObjEMNF_Periodo;
        private EMNF_Periodo ObjEMNF_Periodo 
        {
            get 
            {
                return _ObjEMNF_Periodo;
            }
            set 
            {
                _ObjEMNF_Periodo = value;
                OnPropertyChanged();
            }
        }
        public DateTime FechaEmision { get; set; }
        public string SerieNumero { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public decimal Exonerada { get; set; }
        public decimal Gravada { get; set; }
        public decimal PIGV { get; set; }
        public decimal IGV { get; set; }
        public decimal Percepcion { get; set; }
        public decimal PPercepcion { get; set; }
        public decimal OCargos { get; set; }
        public decimal ImpTotal { get; set; }
        public decimal Total { get; set; } 

        /*AGREGADO*/
        public ESGC_Estado ObjESGC_Estado { get; set; }
        public DateTime FechaContable { get; set; }
        public string TipoCambio { get; set; }
        public decimal Detraccion { get; set; }
        public decimal PDetraccion { get; set; }
        public string Glosa { get; set; }
    }
}
