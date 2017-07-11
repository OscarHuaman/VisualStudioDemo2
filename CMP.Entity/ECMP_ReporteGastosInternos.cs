/*********************************************************
'* ENTIDAD PARA REPORTE GENERAL DE HONORARIO
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   JUAN GUERRA MENESES
'* FCH. CREACIÓN : 23/01/2016
**********************************************************/
namespace CMP.Entity
{
    using ComputerSystems;
    using MNF.Entity;
    using SGC.Empresarial.Entity;
    using System;
   
    [Serializable]
    public class ECMP_ReporteGastosInternos : CmpNotifyPropertyChanged
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

                OnPropertyChanged();
            }
        }
        public ESGC_Documento ObjESGC_Documento { get; set; }
        public DateTime FechaEmision { get; set; }
        public string SerieNumero { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public decimal Total { get; set; }
        public string Glosa { get; set; }

        /*AGREGADO*/
        public DateTime FechaContable { get; set; }
        public ESGC_Estado ObjESGC_Estado { get; set; }
    }
}
