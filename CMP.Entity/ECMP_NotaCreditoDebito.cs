/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 11/02/2016
**********************************************************/
namespace CMP.Entity
{
    using SGC.Empresarial.Entity;
    using MNF.Entity;
    using System;
    using System.Collections.Generic;
    using ComputerSystems;
    [Serializable]
    public class ECMP_NotaCreditoDebito : CmpNotifyPropertyChanged
    {
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

        private EMNF_MotivoNotaCreditoDebito _ObjEMNF_MotivoNotaCreditoDebito;
        public EMNF_MotivoNotaCreditoDebito ObjEMNF_MotivoNotaCreditoDebito
        {
            get
            {
                return _ObjEMNF_MotivoNotaCreditoDebito;
            }
            set
            {
                _ObjEMNF_MotivoNotaCreditoDebito = value;
                //OnPropertyChanged();
            }
        }
        private string _Serie;
        public string Serie
        {
            get
            {
                return _Serie;
            }
            set
            {
                if (value.Trim().Length != 0)
                {
                    string strCeros = string.Empty;
                    string valueaux = value.Substring(0, 1);
                    try
                    {
                        Convert.ToInt16(valueaux);
                        for (int i = 0; i < (4 - value.Trim().Length); i++)
                        {
                            strCeros += "0";
                        }
                        _Serie = strCeros + value;
                    }
                    catch (Exception)
                    {
                        string serieAux = value;
                        if (value.Trim().Length < 4)
                        {
                            serieAux = serieAux.Substring(1, (value.Trim().Length - 1));
                            for (int i = 0; i < (3 - serieAux.Trim().Length); i++)
                            {
                                strCeros += "0";
                            }
                            _Serie = valueaux + strCeros + serieAux;
                        }
                        else
                        {
                            _Serie = value;
                        }
                    }
                }

                OnPropertyChanged();
            }
        }
        private string _Numero;
        public string Numero
        {
            get
            {
                return _Numero;
            }
            set
            {
                string strCeros = string.Empty;
                if (value.Trim().Length != 0)
                {
                    for (int i = 0; i < (7 - value.Trim().ToString().Length); i++)
                    {
                        strCeros += "0";
                    }
                }
                _Numero = strCeros + value;
                OnPropertyChanged();
            }
        }
        private decimal _TipoCambio;
        public decimal TipoCambio
        {
            get
            {
                return _TipoCambio;
            }
            set
            {
                _TipoCambio = value;
                OnPropertyChanged();
            }
        }
        private decimal _Exonerada;
        public decimal Exonerada
        {
            get
            {
                return Convert.ToDecimal(Convert.ToDouble(_Exonerada).ToString("N2"));
            }
            set
            {
                _Exonerada = value;
                OnPropertyChanged();
            }
        }
        private decimal _Gravada;
        public decimal Gravada
        {
            get
            {
                return Convert.ToDecimal(Convert.ToDouble(_Gravada).ToString("N2"));
            }
            set
            {
                _Gravada = value;
                OnPropertyChanged("Gravada");
            }
        }
        private decimal _IGV;
        public decimal IGV
        {
            get
            {
                return Convert.ToDecimal(Convert.ToDouble(_IGV).ToString("N2"));
            }
            set
            {
                _IGV = value;
                OnPropertyChanged("IGV");
            }
        }
        private decimal _ImporteIGV;
        public decimal ImporteIGV
        {
            get
            {
                return Convert.ToDecimal(Convert.ToDouble(_ImporteIGV).ToString("N2"));
            }
            set
            {
                _ImporteIGV = value;
                OnPropertyChanged("ImporteIGV");
            }
        }
        private DateTime _Fecha;
        public DateTime Fecha
        {
            get
            {
                return _Fecha;
            }
            set
            {
                _Fecha = value;
                OnPropertyChanged();
            }
        }
        public string Opcion { get; set; }
        public int IdNotaCreDeb { get; set; }
        public string CodDocumento { get; set; }
        public string Periodo { get; set; }
        public string Glosa { get; set; }
        public string DetalleXML { get; set; }
        public ECMP_NotaCreditoDebito()
        {
            Serie = string.Empty;
            Numero = string.Empty;
        }
        //AGREGADO
        private decimal _Total;
        public decimal Total
        {
            get
            {
                return Convert.ToDecimal(Convert.ToDouble(_Total).ToString("N2"));
            }
            set
            {
                _Total = value;
                //OnPropertyChanged();
            }
        }
        public string CodDocumentoSerieNumero { get; set; }        
        public EMNF_ClienteProveedor _ObjEMNF_ClienteProveedor;
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
        public List<ECMP_NotaCreditoDebitoDetalle> ListDetailDocument { get; set; }
        public string DocuRefXML { get; set; }
        public bool _AfectaAlmacen;
        public bool AfectaAlmacen
        {
            get
            {
                return _AfectaAlmacen;
            }
            set
            {
                _AfectaAlmacen = value;
                OnPropertyChanged("AfectaAlmacen");
            }
        }
        private DateTime _FechaInicio;
        public  DateTime FechaInicio
        {
            get
            {
                return _FechaInicio;
            }
            set
            {
                _FechaInicio = value;
                OnPropertyChanged("FechaInicio");
            }
        }
        private DateTime _FechaHasta;
        public  DateTime FechaHasta
        {
            get
            {
                return _FechaHasta;
            }
            set
            {
                _FechaHasta = value;
                OnPropertyChanged("FechaHasta");
            }
        }
        private ESGC_Estado _ObjESGC_Estado;
        public  ESGC_Estado ObjESGC_Estado
        {
            get
            {
                if (_ObjESGC_Estado == null)
                    _ObjESGC_Estado = new ESGC_Estado();
                return _ObjESGC_Estado;
            }
            set
            {
                _ObjESGC_Estado = value;
                OnPropertyChanged("ObjESGC_Estado");
            }
        }
        private EMNF_DocumentoReferencia _ObjEMNF_DocumentoReferencia;
        public  EMNF_DocumentoReferencia ObjEMNF_DocumentoReferencia
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
                OnPropertyChanged("ObjEMNF_DocumentoReferencia");
            }

        }
        private string _Filtro;
        public string Filtro
        {
            get
            {
                return _Filtro;
            }
            set
            {
                _Filtro = value;
                OnPropertyChanged("Filtro");
            }
        }
    }
}
