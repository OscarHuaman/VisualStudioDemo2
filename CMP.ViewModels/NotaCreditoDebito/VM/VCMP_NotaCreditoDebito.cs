/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 17/02/2016
**********************************************************/
namespace CMP.ViewModels.NotaCreditoDebito.VM
{
    using CMP.Business;
    using CMP.Entity;
    using CMP.Reports;
    using CMP.Useful.Modulo;
    using CMP.ViewModels.Compra.Flyouts;
    using CMP.ViewModels.Compra.VM;
    using ComputerSystems;
    using ComputerSystems.Loading;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using MahApps.Metro.Controls;
    using MNF.Business;
    using MNF.Entity;
    using MNF.Presentation.ClienteProveedor.Flyouts;
    using SGC.Empresarial.Business;
    using SGC.Empresarial.Entity;
    using SGC.Empresarial.Method;
    using SGC.Empresarial.Useful;
    using SGC.Empresarial.Useful.Modulo;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class VCMP_NotaCreditoDebito : CmpNavigationService, CmpINavigation
    {
        private ECMP_NotaCreditoDebito _ObjECMP_NotaCreditoDebito;
        public  ECMP_NotaCreditoDebito ObjECMP_NotaCreditoDebito
        {
            get
            {
                return _ObjECMP_NotaCreditoDebito;
            }
            set
            {
                _ObjECMP_NotaCreditoDebito = value;
                OnPropertyChanged();
            }
        }

        public object Parameter
        {
            set
            {
                if (value is ECMP_NotaCreditoDebito)
                {
                    ObjECMP_NotaCreditoDebito = (ECMP_NotaCreditoDebito)value;
                    PorcentajeIgv = ObjECMP_NotaCreditoDebito.IGV + " %";
                    TitleTotal = "Total";
                    LoadHeader();
                }
            }
        }

        private Frame _MyFrame;
        public  Frame MyFrame
        {
            set
            {
                _MyFrame = value;
                AddFlyout();
            }
        }

        #region COLECCIÓN

        private ObservableCollection<EMNF_MotivoNotaCreditoDebito> _ListEMNF_MotivoNotaCreditoDebito;
        public  ObservableCollection<EMNF_MotivoNotaCreditoDebito> ListEMNF_MotivoNotaCreditoDebito
            {
                get
                {
                    if (_ListEMNF_MotivoNotaCreditoDebito == null)
                        _ListEMNF_MotivoNotaCreditoDebito = new ObservableCollection<EMNF_MotivoNotaCreditoDebito>();
                    return _ListEMNF_MotivoNotaCreditoDebito;
                }
                set
                {
                    _ListEMNF_MotivoNotaCreditoDebito = value;
                    OnPropertyChanged();
                }
            }
        private ObservableCollection<ECMP_NotaCreditoDebitoDetalle> _ListECMP_NotaCreditoDebitoDetalle;
        public  ObservableCollection<ECMP_NotaCreditoDebitoDetalle> ListECMP_NotaCreditoDebitoDetalle
            {
                get
                {
                    if (_ListECMP_NotaCreditoDebitoDetalle == null)
                        _ListECMP_NotaCreditoDebitoDetalle = new ObservableCollection<ECMP_NotaCreditoDebitoDetalle>();
                    return _ListECMP_NotaCreditoDebitoDetalle;
                }
                set
                {
                    _ListECMP_NotaCreditoDebitoDetalle = value;
                    OnPropertyChanged("ListECMP_NotaCreditoDebitoDetalle");
                }
            }
        private ObservableCollection<EMNF_DocumentoReferencia> _ListEMNF_DocumentoReferencia;
        public  ObservableCollection<EMNF_DocumentoReferencia> ListEMNF_DocumentoReferencia
        {
            get
            {
                if (_ListEMNF_DocumentoReferencia == null)
                    _ListEMNF_DocumentoReferencia = new ObservableCollection<EMNF_DocumentoReferencia>();
                return _ListEMNF_DocumentoReferencia;
            }
            set
            {
                _ListEMNF_DocumentoReferencia = value;
                OnPropertyChanged("ListEMNF_DocumentoReferencia");
            }
        }
        private ObservableCollection<EMNF_Periodo> _ListEMNF_Periodo;
        public  ObservableCollection<EMNF_Periodo> ListEMNF_Periodo
            {
                get
                {
                    if (_ListEMNF_Periodo == null)
                    {
                        _ListEMNF_Periodo = new ObservableCollection<EMNF_Periodo>();
                    }
                    return _ListEMNF_Periodo;
                }
                set
                {
                    _ListEMNF_Periodo = value;
                    this.OnPropertyChanged();
                }
            }
        private ObservableCollection<ESGC_Estado> _ListESGC_Estado;
        public  ObservableCollection<ESGC_Estado> ListESGC_Estado
        {
            get
            {
                if (_ListESGC_Estado == null)
                    _ListESGC_Estado = new ObservableCollection<ESGC_Estado>();
                return _ListESGC_Estado;
            }
            set
            {
                _ListESGC_Estado = value;
                OnPropertyChanged("ListESGC_Estado");
            }
        }
  
        #endregion

        #region OBJETO SECUNDARIO

         private EMNF_MotivoNotaCreditoDebito _SelectMotivoNotaCreditoDebito;
         public  EMNF_MotivoNotaCreditoDebito SelectMotivoNotaCreditoDebito
            {
                get { return _SelectMotivoNotaCreditoDebito; }
                set
                {
                    _SelectMotivoNotaCreditoDebito = value;

                    if (value != null)
                    {
                        ObjECMP_NotaCreditoDebito.ObjEMNF_MotivoNotaCreditoDebito = value;
                        if (value.IdMotCreDeb == 1 && value.TipoNota == "C")
                        {
                            IsVisibilityViewImporte = Visibility.Visible;
                            IsVisibilityEditImporte = Visibility.Collapsed;
                            IsVisibilityAnulacion = false;
                            IsVisibilityDescuento = false;
                            IsVisibilityDevolucion = false;
                            IsVisibilityFactInfDevida = false;
                            IsVisibilityPrecioUnitario = true;
                            IsVisibilityTodos = true;
                            IsEnabled = false;
                            IsEnabledAlmacen = true;
                        }
                        else if (value.IdMotCreDeb == 3 && value.TipoNota == "C")
                        {
                            IsVisibilityViewImporte = Visibility.Visible;
                            IsVisibilityEditImporte = Visibility.Collapsed;
                            IsEnabled = true;
                            IsVisibilityAnulacion = false;
                            IsVisibilityDescuento = true;
                            IsVisibilityDevolucion = false;
                            IsVisibilityPrecioUnitario = true;
                            IsVisibilityTodos = true;
                            HeaderPDescuento = "% Descuento";
                            HeaderDescuento = "Descuento";
                            HeaderPrecioDB = "Precio Desc.";
                            IsEnabled = true;
                            IsEnabledAlmacen = true;
                        }
                        else if (value.IdMotCreDeb == 4 && value.TipoNota == "C")
                        {
                            IsVisibilityAnulacion = false;
                            IsVisibilityDescuento = false;
                            IsVisibilityDevolucion = true;
                            IsVisibilityPrecioUnitario = true;
                            IsVisibilityTodos = true;
                            IsEnabled = true;
                        }
                        else if (value.IdMotCreDeb == 2 && value.TipoNota == "C")
                        {
                            IsVisibilityViewImporte = Visibility.Visible;
                            IsVisibilityEditImporte = Visibility.Collapsed;
                            IsVisibilityDevolucion = false;
                            IsVisibilityDescuento = true;
                            IsVisibilityTodos = true;
                            HeaderPDescuento = "% Bonificación";
                            HeaderDescuento = "Bonificación";
                            HeaderPrecioDB = "Precio Bonif.";
                            IsEnabled = true;
                            IsEnabledAlmacen = true;
                        }
                        else if (value.IdMotCreDeb == 6 )
                        {
                            IsVisibilityViewImporte = Visibility.Collapsed;
                            IsVisibilityEditImporte = Visibility.Visible;
                            IsVisibilityFactInfDevida = true;
                            IsVisibilityAnulacion = false;
                            IsVisibilityDescuento = false;
                            IsVisibilityDevolucion = false;
                            IsVisibilityTodos = true;
                            IsVisibilityPrecioUnitario = true;
                            IsEnabledAlmacen = true;
                            IsEnabled = true;
                        }
                        else
                        {
                            IsVisibilityViewImporte = Visibility.Collapsed;
                            IsVisibilityEditImporte = Visibility.Visible;
                            IsVisibilityFactInfDevida = false;
                            IsVisibilityAnulacion = false;
                            IsVisibilityDescuento = false;
                            IsVisibilityDevolucion = false;
                            IsVisibilityPrecioUnitario = false;
                            IsVisibilityTodos = false;
                            IsEnabledAlmacen = false;
                        }

                        if (ObjECMP_NotaCreditoDebito.Opcion == "I")
                        {
                            ListECMP_NotaCreditoDebitoDetalle = null;
                            ListEMNF_DocumentoReferencia = null;
                            CalcularTotales();
                        }
                    }
                    OnPropertyChanged("SelectMotivoNotaCreditoDebito");
                }
            }

         private ECMP_NotaCreditoDebitoDetalle _SelectNotaCreditoDebitoDetalle;
         public  ECMP_NotaCreditoDebitoDetalle SelectNotaCreditoDebitoDetalle
            {
                get
                {
                    return _SelectNotaCreditoDebitoDetalle;
                }
                set
                {
                    _SelectNotaCreditoDebitoDetalle = value;
                    OnPropertyChanged("SelectNotaCreditoDebitoDetalle");
                }
            }

         private string _State;
         public  string State
            {
                get { return _State; }
                set
                {
                    _State = value;
                    if (value != null)
                    {
                        ObjECMP_NotaCreditoDebito.CodDocumento = State == "C" ? "NCR" : "NDB";
                        string strOutMessageError = string.Empty;
                        CmpTask.Process(
                        () =>
                        {
                            try
                            {
                                ListEMNF_MotivoNotaCreditoDebito = new ObservableCollection<EMNF_MotivoNotaCreditoDebito>(new BMNF_MotivoNotaCreditoDebito().ListGetMotivoNotaCreditoDebito().Where(x => x.TipoNota == State && (x.ModuloPref.Trim().Length == 0 || x.ModuloPref == "CMP")));//MOTIVO NOTA CREDITO DEBITO MNF NEGOCIO
                            }
                            catch (Exception ex)
                            {
                                strOutMessageError = ex.Message;
                            }
                        },
                        () =>
                        {
                            if (strOutMessageError.Length > 0)
                            {
                                CmpMessageBox.Show(CMPMensajes.TitleAdminNotaCreditoDebito, strOutMessageError, CmpButton.Aceptar);
                            }
                            else
                            {
                                if (ObjECMP_NotaCreditoDebito.Opcion == "I")
                                    SelectMotivoNotaCreditoDebito = ListEMNF_MotivoNotaCreditoDebito.FirstOrDefault();
                                else
                                    SelectMotivoNotaCreditoDebito = ListEMNF_MotivoNotaCreditoDebito.FirstOrDefault(x => x.IdMotCreDeb == ObjECMP_NotaCreditoDebito.ObjEMNF_MotivoNotaCreditoDebito.IdMotCreDeb);
                            }
                        });
                    }
                    OnPropertyChanged("State");
                }
            }

         private Visibility _IsVisibilityViewImporte;
         public  Visibility IsVisibilityViewImporte
            {
                get
                {
                    return _IsVisibilityViewImporte;
                }
                set
                {
                    _IsVisibilityViewImporte = value;
                    OnPropertyChanged("IsVisibilityViewImporte");
                }
            }

         private Visibility _IsVisibilityEditImporte;
         public  Visibility IsVisibilityEditImporte
            {
                get
                {
                    return _IsVisibilityEditImporte;
                }
                set
                {
                    _IsVisibilityEditImporte = value;
                    OnPropertyChanged("IsVisibilityEditImporte");
                }
            }

         private string _PorcentajeIgv;
         public  string PorcentajeIgv
            {
                get
                {
                    return _PorcentajeIgv;
                }
                set
                {
                    _PorcentajeIgv = value;
                    OnPropertyChanged();
                }
            }

         private bool _IsFocusedNumero;
         public  bool IsFocusedNumero
            {
                get
                {
                    return _IsFocusedNumero;
                }
                set
                {
                    _IsFocusedNumero = value;
                    OnPropertyChanged();
                }
            }

         private EMNF_Periodo _SelectPeriodo;
         public  EMNF_Periodo SelectPeriodo
            {
                get
                {
                    return _SelectPeriodo;
                }
                set
                {
                    _SelectPeriodo = value;
                    ObjECMP_NotaCreditoDebito.Periodo = value.Periodo;

                    if (ObjECMP_NotaCreditoDebito.Opcion == "I")
                    {
                        DateTime dtmFechaEnd, dtmSelectFecha;
                        DateTime? dtmFechaStart;
                        CMP.ViewModels.Method.MDatePicker.DateStartToDateEnd(value.Periodo, out dtmFechaStart, out dtmFechaEnd, out dtmSelectFecha);
                        FechaEnd = dtmFechaEnd;
                        ObjECMP_NotaCreditoDebito.Fecha = dtmSelectFecha;
                    }
                    OnPropertyChanged("SelectPeriodo");
                }
            }

         private ESGC_Estado _SelectEstado;
         public  ESGC_Estado SelectEstado
         {
             get 
             {
                 return _SelectEstado;
             }
             set 
             {
                 _SelectEstado = value;
                 OnPropertyChanged("SelectEstado");
             }
         }

         private DateTime _FechaEnd;
         public  DateTime FechaEnd
            {
                get
                {
                    return _FechaEnd;
                }
                set
                {
                    _FechaEnd = value;
                    OnPropertyChanged("FechaEnd");
                }
            }

         private bool _IsVisibilityAnulacion;
         public  bool IsVisibilityAnulacion
            {
                get{return _IsVisibilityAnulacion;}
                set
                {
                    _IsVisibilityAnulacion = value;
                    OnPropertyChanged("IsVisibilityAnulacion");
                }
            }

         private bool _IsVisibilityDescuento;
         public  bool IsVisibilityDescuento
            {
                get
                {
                    return _IsVisibilityDescuento;
                }
                set
                {
                    _IsVisibilityDescuento = value;
                    OnPropertyChanged("IsVisibilityDescuento");
                }
            }

         private bool _IsVisibilityDevolucion;
         public  bool IsVisibilityDevolucion
            {
                get
                {
                    return _IsVisibilityDevolucion;
                }
                set
                {
                    _IsVisibilityDevolucion = value;
                    OnPropertyChanged("IsVisibilityDevolucion");
                }

            }

         private bool _IsVisibilityFacInfDebida;
         public  bool IsVisibilityFactInfDevida
            {
                get
                {
                    return _IsVisibilityFacInfDebida;
                }
                set
                {
                    _IsVisibilityFacInfDebida = value;
                    OnPropertyChanged("IsVisibilityFactInfDevida");
                }

            }

         private bool _IsVisibilityPrecioUnitario;
         public  bool IsVisibilityPrecioUnitario
            {
                get
                {
                    return _IsVisibilityPrecioUnitario;
                }
                set
                {
                    _IsVisibilityPrecioUnitario = value;
                    OnPropertyChanged("IsVisibilityPrecioUnitario");
                }
            }

         private bool _IsVisibilityTodos;
         public  bool IsVisibilityTodos
            {
                get
                {
                    return _IsVisibilityTodos;
                }
                set
                {
                    _IsVisibilityTodos = value;
                    OnPropertyChanged("IsVisibilityTodos");
                }
            }

         private string _HeaderPDescuento;
         public  string HeaderPDescuento
            {
                get
                {
                    return _HeaderPDescuento;
                }
                set
                {
                    _HeaderPDescuento = value;
                    OnPropertyChanged("HeaderPDescuento");
                }
            }

         private string _HeaderDescuento;
         public  string HeaderDescuento
            {
                get
                {
                    return _HeaderDescuento;
                }
                set
                {
                    _HeaderDescuento = value;
                    OnPropertyChanged("HeaderDescuento");
                }
            }

         private string _HeaderPrecioDB;
         public  string HeaderPrecioDB
            {
                get
                {
                    return _HeaderPrecioDB;
                }
                set
                {
                    _HeaderPrecioDB = value;
                    OnPropertyChanged("HeaderPrecioDB");
                }
            }

         private EMNF_DocumentoReferencia _SelectDocReferencia;
         public  EMNF_DocumentoReferencia SelectDocReferencia
            {
                get
                {
                    return _SelectDocReferencia;
                }
                set
                {
                    _SelectDocReferencia = value;
                    OnPropertyChanged("SelectDocReferencia");
                }
            }

         private decimal _Importe;
         public decimal Importe
         {
             get
             {
                 return _Importe;
             }
             set 
             {
                 _Importe=value;
                 OnPropertyChanged("Importe");
             }
         }

         private string _TitleTotal;
         public string TitleTotal
         {
             get
             {
                 return _TitleTotal;
             }
             set
             {
                 _TitleTotal = value;
                 OnPropertyChanged("TitleTotal");
             }
         }

         #region ENABLE

            private bool _IsEnabledTipoNota;
            public  bool IsEnabledTipoNota
            {
                get
                {
                    return _IsEnabledTipoNota;
                }
                set
                {
                    _IsEnabledTipoNota = value;
                    OnPropertyChanged();
                }
            }
            private bool _IsEnabledMotivo;
            public  bool IsEnabledMotivo
            {
                get
                {
                    return _IsEnabledMotivo;
                }
                set
                {
                    _IsEnabledMotivo = value;
                    OnPropertyChanged();
                }
            }
            private bool _IsEnabledClienteRazonSocial;
            public  bool IsEnabledClienteRazonSocial
            {
                get
                {
                    return _IsEnabledClienteRazonSocial;
                }
                set
                {
                    _IsEnabledClienteRazonSocial = value;
                    OnPropertyChanged();
                }
            }
            private bool _IsEnabled;
            public  bool IsEnabled
            {
                get
                {
                    return _IsEnabled;
                }
                set
                {
                    _IsEnabled = value;
                    OnPropertyChanged("IsEnabled");
                }

            }
            private bool _IsEnabledAlmacen;
            public  bool IsEnabledAlmacen
            {
                get
                {
                    return _IsEnabledAlmacen;
                }
                set
                {
                    _IsEnabledAlmacen = value;
                    OnPropertyChanged("IsEnabledAlmacen");
                }
            }
            private bool _IsEnabledImprimir;
            public bool IsEnabledImprimir
            {
                get
                {
                    return _IsEnabledImprimir;
                }
                set
                {
                    _IsEnabledImprimir = value;
                    OnPropertyChanged("IsEnabledImprimir");
                }
            }
         #endregion

        #endregion

        #region ICOMMAD

        public ICommand IBuscarProveedor
        {
            get
            {
                return CmpICommand.GetICommand(new Action<object>((T) =>
                {
                    MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminNotaCreditoDebito, CMPMensajes.GetAccesoRestringidoNull("Proveedor"), new Action<ESGC_PermisoPerfil>((P) =>
                    {
                        if (P.Consulta)
                        {
                            var vrListEMNF_ClienteProveedor = new List<EMNF_ClienteProveedor>();
                            string strOutMessageError = string.Empty;
                            CmpTask.Process(
                            () =>
                            {
                                try
                                {
                                    vrListEMNF_ClienteProveedor = new BMNF_ClienteProveedor().ListFiltrarClienteProveedor(T.ToString(), IsClienteOProveedor.Proveedor);
                                }
                                catch (Exception ex)
                                {
                                    strOutMessageError = ex.Message;
                                }
                            },
                            () =>
                            {
                                if (strOutMessageError.Length > 0)
                                {
                                    CmpMessageBox.Show(CMPMensajes.TitleAdminNotaCreditoDebito, strOutMessageError, CmpButton.Aceptar);
                                }
                                else
                                {
                                    if (vrListEMNF_ClienteProveedor.Count == 1)
                                    {
                                        ObjECMP_NotaCreditoDebito.ObjEMNF_ClienteProveedor = vrListEMNF_ClienteProveedor.FirstOrDefault();
                                    }
                                    else
                                    {
                                        _MyFrame.FlyoutIsOpen("PMNF_BuscarClienteProveedor", new Action<Flyout>((value) =>
                                        {
                                            if (value is PMNF_BuscarClienteProveedor)
                                            {
                                                var MyFlyout = (PMNF_BuscarClienteProveedor)value;
                                                MyFlyout.MyIsClienteOProveedor = IsClienteOProveedor.Proveedor;
                                                MyFlyout.InitializePMNF_BuscarClienteProveedor();
                                                MyFlyout.SetListEMNF_ClienteProveedor = vrListEMNF_ClienteProveedor;
                                                MyFlyout.SetValueFilter = T.ToString();
                                                MyFlyout.IsOpen = true;
                                            }
                                        }));
                                    }

                                    ObjECMP_NotaCreditoDebito.ObjESGC_Moneda = null;
                                    ListECMP_NotaCreditoDebitoDetalle.Clear();
                                }
                            });
                        }
                        else
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleAdminNotaCreditoDebito, CMPMensajes.GetAccesoRestringidoBuscar("Cliente"), CmpButton.Aceptar);
                        }
                    }));
                }));
            }
        }

        public ICommand ICalculaTotal
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    CalcularTotales();
                });
            }
        }

        public ICommand ISerie
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    string strCeros = string.Empty;
                    for (int i = 0; i < (3 - T.ToString().Length); i++)
                    {
                        strCeros += "0";
                    }

                    ObjECMP_NotaCreditoDebito.Serie = strCeros + T.ToString();
                    IsFocusedNumero = true;
                });
            }
        }

        public ICommand INumero
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    string strCeros = string.Empty;
                    for (int i = 0; i < (7 - T.ToString().Length); i++)
                    {
                        strCeros += "0";
                    }

                    ObjECMP_NotaCreditoDebito.Numero = strCeros + T.ToString();
                });
            }
        }

        public ICommand IGuardar
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (ValidaDatos())
                        return;

                    string strOutMessageError = string.Empty;
                    CmpTask.Process(
                    () =>
                    {
                        try
                        {
                            ObjECMP_NotaCreditoDebito.ObjESGC_Estado.CodEstado = SelectEstado.CodEstado;
                            ObjECMP_NotaCreditoDebito.DetalleXML = XmlDetalle();
                            ObjECMP_NotaCreditoDebito.DocuRefXML = XmlDocRef();
                            new BCMP_NotaCreditoDebito().TransNotaCreditoDebito(ObjECMP_NotaCreditoDebito);
                        }
                        catch (Exception ex)
                        {
                            strOutMessageError = ex.Message;
                        }
                    },
                    () =>
                    {
                        if (strOutMessageError.Trim().Length > 0)
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleAdminNotaCreditoDebito, strOutMessageError, CmpButton.Aceptar);
                        }
                        else
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleAdminNotaCreditoDebito, CMPMensajes.DatoProcesados + "\n¿Desea Imprimir el Documento?", CmpButton.AceptarCancelar, () =>
                            {
                                if (IImprimir.CanExecute(true))
                                    IImprimir.Execute(string.Empty);

                                if (IClose.CanExecute(true))
                                    IClose.Execute(string.Empty);
                            }, () =>
                            {
                                if (IClose.CanExecute(true))
                                    IClose.Execute(string.Empty);
                            });
                        }
                    });
                });
            }
        }

        public ICommand IAgregarItem
        {
            get
            {
                return CmpICommand.GetICommand(new Action<object>((T) =>
                {
                    if (ObjECMP_NotaCreditoDebito.ObjEMNF_ClienteProveedor == null)
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminNotaCreditoDebito, "Seleccione un Proveedor", CmpButton.Aceptar);
                        return;
                    }
                    if (SelectMotivoNotaCreditoDebito == null)
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminNotaCreditoDebito, "Seleccione un Motivo", CmpButton.Aceptar);
                        return;
                    }

                    _MyFrame.FlyoutIsOpen("PCMP_BuscarCompra", new Action<Flyout>((value) =>
                    {
                        if (value is PCMP_BuscarCompra)
                        {
                            var MyFlyout = (PCMP_BuscarCompra)value;
                            VCMP_BuscarCompra MyVCMP_BuscarCompra = new VCMP_BuscarCompra();
                            MyVCMP_BuscarCompra.SetKeyDownCmpButtonTitleTecla = MyFlyout;
                            MyVCMP_BuscarCompra.MySelectItem = (item) =>
                            {
                                foreach (var items in item)
                                {
                                    AddItemDocReferencia((ECMP_Compra)items);
                                }
                                MyFlyout.IsOpen = false;
                            };

                            var MyNotaCreditoDebitoDetalle = ListECMP_NotaCreditoDebitoDetalle.FirstOrDefault();
                           
                            string CodMoneda = ((MyNotaCreditoDebitoDetalle != null) ? MyNotaCreditoDebitoDetalle.ObjECMP_CompraDetalle.ObjECMP_Compra.ObjESGC_Moneda.CodMoneda : "%");
                            string CodDocumento = ((MyNotaCreditoDebitoDetalle != null) ? MyNotaCreditoDebitoDetalle.ObjECMP_CompraDetalle.ObjECMP_Compra.CodDocumento : "TODO");
                            switch (CodDocumento)
                            {
                                case "BOL":
                                    MyVCMP_BuscarCompra.LoadDetail(ObjECMP_NotaCreditoDebito.ObjEMNF_ClienteProveedor.IdCliProveedor, CodMoneda, TipoDocumento.Boleta);
                                    break;
                                case "FAC":
                                    MyVCMP_BuscarCompra.LoadDetail(ObjECMP_NotaCreditoDebito.ObjEMNF_ClienteProveedor.IdCliProveedor, CodMoneda, TipoDocumento.Factura);
                                    break;
                                case "TCK":
                                    MyVCMP_BuscarCompra.LoadDetail(ObjECMP_NotaCreditoDebito.ObjEMNF_ClienteProveedor.IdCliProveedor, CodMoneda, TipoDocumento.Ticket);
                                    break;
                                case "RCB":
                                    MyVCMP_BuscarCompra.LoadDetail(ObjECMP_NotaCreditoDebito.ObjEMNF_ClienteProveedor.IdCliProveedor, CodMoneda, TipoDocumento.Recibo);
                                    break;
                                default:
                                    MyVCMP_BuscarCompra.LoadDetail(ObjECMP_NotaCreditoDebito.ObjEMNF_ClienteProveedor.IdCliProveedor, CodMoneda, TipoDocumento.Todo);
                                    break;
                            }
                            MyFlyout.DataContext = MyVCMP_BuscarCompra;
                            MyFlyout.IsOpen = true;
                        }
                    }));
                }));
            }
        }

        public ICommand IQuitarItem
        {
           get
            {
                return CmpICommand.GetICommand(new Action<object>((T) =>
                {
                    if (ListECMP_NotaCreditoDebitoDetalle.Count <= 0) return;
                    if (SelectNotaCreditoDebitoDetalle == null)
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminNotaCreditoDebito, CMPMensajes.IsNullSelectItem, CmpButton.Aceptar);
                        return;
                    }                    

                    CmpMessageBox.Show(CMPMensajes.TitleAdminNotaCreditoDebito, CMPMensajes.QuitarItems, CmpButton.AceptarCancelar, () =>
                    {
                        int intItem = 0;
                        var vrIdCompra = SelectNotaCreditoDebitoDetalle.ObjECMP_CompraDetalle.ObjECMP_Compra.IdCompra;
                        
                        int conta = 0;
                        ListECMP_NotaCreditoDebitoDetalle.Remove(SelectNotaCreditoDebitoDetalle);
                        conta = ListECMP_NotaCreditoDebitoDetalle.Where(x => x.ObjECMP_CompraDetalle.ObjECMP_Compra.IdCompra == vrIdCompra).Count();

                        ListECMP_NotaCreditoDebitoDetalle.ToList().ForEach(item =>
                        {
                            intItem++;
                            item.Item = intItem;
                        });

                        if(conta == 0)
                        {
                            ObservableCollection<ECMP_NotaCreditoDebitoDetalle> List = new ObservableCollection<ECMP_NotaCreditoDebitoDetalle>();

                            ListEMNF_DocumentoReferencia.ToList().ForEach(x =>
                            {
                                if (vrIdCompra == x.IdDocReferencia)
                                    ListEMNF_DocumentoReferencia.Remove(x);
                            });
                        }
                        CalcularTotales();
                    });
                }));
            }
        }

        public ICommand IClose
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    Volver(_MyFrame, new ECMP_NotaCreditoDebito());
                });
            }
        }

        public ICommand ICalcularDescuento
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (SelectNotaCreditoDebitoDetalle != null)
                    {
                        SelectNotaCreditoDebitoDetalle.PrcDscBonificacion = Convert.ToDecimal((T.ToString().Trim().Length == 0) ? 0 : T);
                        SelectNotaCreditoDebitoDetalle.ImpDscBonificacion = (SelectNotaCreditoDebitoDetalle.ObjECMP_CompraDetalle.PrecioUnitario * (SelectNotaCreditoDebitoDetalle.PrcDscBonificacion / 100));
                        SelectNotaCreditoDebitoDetalle.PreDscBoniOmision = SelectNotaCreditoDebitoDetalle.ObjECMP_CompraDetalle.PrecioUnitario - SelectNotaCreditoDebitoDetalle.ImpDscBonificacion;
                        SelectNotaCreditoDebitoDetalle.ObjECMP_CompraDetalle.Importe = SelectNotaCreditoDebitoDetalle.ObjECMP_CompraDetalle.Cantidad * SelectNotaCreditoDebitoDetalle.ImpDscBonificacion;
                    }
                    CalcularTotales();
                });
            }
        }

        public ICommand ICalcularPorcentaje
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (SelectNotaCreditoDebitoDetalle != null)
                    {
                        if (SelectNotaCreditoDebitoDetalle.ImpDscBonificacion > SelectNotaCreditoDebitoDetalle.ObjECMP_CompraDetalle.PrecioUnitario)
                            SelectNotaCreditoDebitoDetalle.ImpDscBonificacion = SelectNotaCreditoDebitoDetalle.ObjECMP_CompraDetalle.PrecioUnitario;
                        SelectNotaCreditoDebitoDetalle.PrcDscBonificacion = ((100 * SelectNotaCreditoDebitoDetalle.ImpDscBonificacion) / SelectNotaCreditoDebitoDetalle.ObjECMP_CompraDetalle.PrecioUnitario);
                        SelectNotaCreditoDebitoDetalle.PreDscBoniOmision = SelectNotaCreditoDebitoDetalle.ObjECMP_CompraDetalle.PrecioUnitario - SelectNotaCreditoDebitoDetalle.ImpDscBonificacion;
                        SelectNotaCreditoDebitoDetalle.ObjECMP_CompraDetalle.Importe = SelectNotaCreditoDebitoDetalle.ObjECMP_CompraDetalle.Cantidad * SelectNotaCreditoDebitoDetalle.ImpDscBonificacion;

                        CalcularTotales();
                    }
                });
            }
        }

        public ICommand IEliminarRef
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (SelectDocReferencia == null || ObjECMP_NotaCreditoDebito.Opcion != "I") return;
                    CmpMessageBox.Show(CMPMensajes.TitleAdminNotaCreditoDebito, CMPMensajes.QuitarItems, CmpButton.AceptarCancelar, () =>
                    {
                        RemoveItemDocReferencia(SelectDocReferencia);
                        CalcularTotales();
                    });
                });
            }
        }

        public ICommand ICantidadDevolver
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (SelectNotaCreditoDebitoDetalle == null)
                        return;
                    if (SelectMotivoNotaCreditoDebito.IdMotCreDeb == 4)
                    {
                        if (SelectNotaCreditoDebitoDetalle.CantidaDevolver > SelectNotaCreditoDebitoDetalle.ObjECMP_CompraDetalle.Cantidad)
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleAdminNotaCreditoDebito, "La Cantidad de Devolución es mayor que la Cantidad", CmpButton.Aceptar);
                            SelectNotaCreditoDebitoDetalle.CantidaDevolver = SelectNotaCreditoDebitoDetalle.ObjECMP_CompraDetalle.Cantidad;
                        }
                        SelectNotaCreditoDebitoDetalle.ObjECMP_CompraDetalle.Importe = SelectNotaCreditoDebitoDetalle.CantidaDevolver * SelectNotaCreditoDebitoDetalle.ObjECMP_CompraDetalle.PrecioUnitario;
                        CalcularTotales();
                    }
                    if (SelectMotivoNotaCreditoDebito.IdMotCreDeb == 6)
                    {
                        SelectNotaCreditoDebitoDetalle.ObjECMP_CompraDetalle.Importe = SelectNotaCreditoDebitoDetalle.PreDscBoniOmision * SelectNotaCreditoDebitoDetalle.ObjECMP_CompraDetalle.Cantidad;
                        CalcularTotales();
                    }
                });
            }
        }

        public ICommand IImprimir
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    string[] parametro;

                       string strSerieNumero = string.Empty, strTempSerieNumero = string.Empty;
                        var varListECMP_NotaCreditoDetalle = new ObservableCollection<ECMP_NotaCreditoDebitoDetalle>();
                        ListECMP_NotaCreditoDebitoDetalle.ToList().ForEach(x =>
                        {
                            varListECMP_NotaCreditoDetalle.Add(new ECMP_NotaCreditoDebitoDetalle()
                            {
                                ArticuloServicio = x.ObjECMP_CompraDetalle.ArticuloServicio,
                                CodUndMedida = x.ObjECMP_CompraDetalle.CodUndMedida,
                                Importe = x.ObjECMP_CompraDetalle.Importe,
                                SerieNumero = x.ObjECMP_CompraDetalle.ObjECMP_Compra.SerieNumero,
                                Cantidad = x.ObjECMP_CompraDetalle.Cantidad,
                                PrcDscBonificacion = x.PrcDscBonificacion,
                                ImpDscBonificacion = x.ImpDscBonificacion,
                                PreDscBoniOmision = x.PreDscBoniOmision,
                                PrecioOmitido = x.PrecioOmitido,
                                PrecioUnitario = x.ObjECMP_CompraDetalle.PrecioUnitario,
                                CantidaDevolver = x.CantidaDevolver

                            });
                        });
                        ListEMNF_DocumentoReferencia.ToList().ForEach(x =>
                        {
                            strSerieNumero = x.SerieNumero + "  -  " + x.Fecha.ToShortDateString();
                            strTempSerieNumero += strSerieNumero + "\n";
                        });
                        var vrTotal = ObjECMP_NotaCreditoDebito.Gravada + ObjECMP_NotaCreditoDebito.ImporteIGV;
                        parametro = new string[]
                            {
                                "prmProveedorRZ|"       + ObjECMP_NotaCreditoDebito.ObjEMNF_ClienteProveedor.RazonSocial,
                                "prmDireccion|"         + ObjECMP_NotaCreditoDebito.ObjEMNF_ClienteProveedor.Direccion,
                                "prmRuc|"               + ObjECMP_NotaCreditoDebito.ObjEMNF_ClienteProveedor.NroDocIdentidad,
                                "prmUsuario|"           + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.RazonSocial,
                                "prmDireccionUsuario|"  + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.DireccionFiscal,
                                "prmRucUsuario|"        + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.Ruc,
                                "prmDiaActual|"         + ObjECMP_NotaCreditoDebito.Fecha.ToLongDateString().Split(',').ElementAt(1),
                                "prmSerie|"             + ObjECMP_NotaCreditoDebito.Serie,
                                "prmNumero|"            + ObjECMP_NotaCreditoDebito.Numero,
                                "prmMoneda|"            + ObjECMP_NotaCreditoDebito.ObjESGC_Moneda.Descripcion,
                                "prmTipoCambio|"        + ObjECMP_NotaCreditoDebito.TipoCambio,
                                "prmSimbolo|"           + ObjECMP_NotaCreditoDebito.ObjESGC_Moneda.Simbolo,
                                "prmSerieNumero|"       + strTempSerieNumero,
                                "prmCodMotivo|"         + ObjECMP_NotaCreditoDebito.ObjEMNF_MotivoNotaCreditoDebito.CodMotivo,
                                "prmMotivo|"            + ObjECMP_NotaCreditoDebito.ObjEMNF_MotivoNotaCreditoDebito.Motivo,
                                "prmGlosa|"             + ObjECMP_NotaCreditoDebito.Glosa,
                                "prmSubTotal|"          + ObjECMP_NotaCreditoDebito.Gravada,
                                "prmIgv|"               + (ObjECMP_NotaCreditoDebito.IGV * 100) + " %",
                                "prmImporteIGV|"        + ObjECMP_NotaCreditoDebito.ImporteIGV,
                                "prmTotal|"             + ObjECMP_NotaCreditoDebito.Total,
                                "prmTipo|"              + ((ObjECMP_NotaCreditoDebito.CodDocumento=="NCR")?"NOTA DE CRÉDITO":"NOTA DE DEBITO")

                            };
                        MainRerport ObjMainRerport = new MainRerport();
                        ObjMainRerport.InitializeMainRerport("CMP.Reports.Files.RptNotaCredito.rdlc", "dtsNotaCreditoDebitoDetalle", varListECMP_NotaCreditoDetalle, parametro);
                        ObjMainRerport.ShowDialog();

                    

                });
            }
        }

        #endregion

        #region MÉTODOS

        private void LoadHeader()
        {
            string strPeriodoActivo = new BMNF_Periodo().GetPeriodoActual();
            string strOutMessageError = string.Empty;
            CmpTask.Process(
            () =>
            {
                try
                {
                    ListEMNF_Periodo = (ObjECMP_NotaCreditoDebito.Opcion == "I") ? new ObservableCollection<EMNF_Periodo>(new BMNF_Periodo().ListPeriodo().Where(x => x.Estado == "A").ToList()) : new ObservableCollection<EMNF_Periodo>(new BMNF_Periodo().ListPeriodo().ToList());
                    ListESGC_Estado = new ObservableCollection<ESGC_Estado>(new BSGC_Estado().ListFiltrarEstado(SGCMethod.GetNameNameTableInXaml(this)));
                }
                catch (Exception ex)
                {
                    strOutMessageError = ex.Message;
                }
            },
            () =>
            {
                if (strOutMessageError.Length > 0)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminNotaCreditoDebito, strOutMessageError, CmpButton.Aceptar);
                }
                else
                {
                    if (ObjECMP_NotaCreditoDebito.Opcion == "I")
                    {
                        SelectPeriodo = ListEMNF_Periodo.FirstOrDefault(x => x.Periodo == strPeriodoActivo);
                        SelectEstado = ListESGC_Estado.FirstOrDefault(x => x.CodEstado == "ATCCD");
                        ObjECMP_NotaCreditoDebito.Glosa = string.Empty;
                        IsEnabledTipoNota = true;
                        IsEnabledMotivo = true;
                        IsEnabledClienteRazonSocial = true;
                        State = "C";
                        IsEnabledImprimir = false;
                    }
                    else
                    {
                        SelectPeriodo = ListEMNF_Periodo.FirstOrDefault(x => x.Periodo == ObjECMP_NotaCreditoDebito.Periodo);
                        SelectEstado = ListESGC_Estado.FirstOrDefault(x => x.CodEstado == ObjECMP_NotaCreditoDebito.ObjESGC_Estado.CodEstado);
                        ListEMNF_DocumentoReferencia = new ObservableCollection<EMNF_DocumentoReferencia>(new BMNF_DocumentoReferencia().ListGetDocumentoReferencia(ObjECMP_NotaCreditoDebito.IdNotaCreDeb, "CCD"));
                        SelectMotivoNotaCreditoDebito = ListEMNF_MotivoNotaCreditoDebito.FirstOrDefault(z => z.IdMotCreDeb == ObjECMP_NotaCreditoDebito.ObjEMNF_MotivoNotaCreditoDebito.IdMotCreDeb);
                        IsEnabledTipoNota = false;
                        IsEnabledMotivo = false;
                        IsEnabledAlmacen = false;
                        IsEnabledClienteRazonSocial = false;
                        State = ObjECMP_NotaCreditoDebito.CodDocumento == "NCR" ? "C" : "D";
                        IsEnabledImprimir = true;
                    }
                }
                LoadDetail();
            });
        }

        private void LoadDetail()
        {
            string strOutMessageError = string.Empty;
            CmpTask.Process(
            () =>
            {
                try
                {
                    ListECMP_NotaCreditoDebitoDetalle = new BCMP_NotaCreditoDebitoDetalle().GetECMP_NotaCreditoDebitoDetalle(ObjECMP_NotaCreditoDebito);
                    CalcularTotales();
                }
                catch (Exception ex)
                {
                    strOutMessageError = ex.Message;
                }
            },
            () =>
            {
                if (strOutMessageError.Length > 0)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminNotaCreditoDebito, strOutMessageError, CmpButton.Aceptar);
                }
            });
        }

        private void AddFlyout()
        {
            #region LISTADO CLIENTE PROVEEDOR

            PMNF_BuscarClienteProveedor MyPMNF_BuscarClienteProveedor = new PMNF_BuscarClienteProveedor();
            MyPMNF_BuscarClienteProveedor.IsSelected += new PMNF_BuscarClienteProveedor.isSelected((item) =>
            {
                ObjECMP_NotaCreditoDebito.ObjEMNF_ClienteProveedor = item;
            });

            #endregion

            _MyFrame.FlyoutInitialize();
            _MyFrame.FlyoutAdd(MyPMNF_BuscarClienteProveedor);
            _MyFrame.FlyoutAdd(new PCMP_BuscarCompra());
        }

        private void AddItemDocReferencia(ECMP_Compra MyECMP_Compra)
        {
            bool blEstado = true;

            var MyNotaCreditoDebitoDetalle = ListEMNF_DocumentoReferencia.FirstOrDefault();
            if (ListEMNF_DocumentoReferencia.ToList().Exists(x => x.IdDocReferencia == MyECMP_Compra.IdCompra))
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminNotaCreditoDebito, "El item ya se ha seleccionado.", CmpButton.Aceptar);
                blEstado = false;
            }
            if (blEstado)
            {
                ListEMNF_DocumentoReferencia.Add(new EMNF_DocumentoReferencia()
                {
                   IdDocReferencia = MyECMP_Compra.IdCompra,
                   SerieNumero = MyECMP_Compra.SerieNumero,
                   Fecha = MyECMP_Compra.Fecha, 
                });
                var vrListCompraDetalle = new BCMP_CompraDetalle().ListAdministrarCompraDetalle(MyECMP_Compra);
                int item = ListECMP_NotaCreditoDebitoDetalle.Count();
                if (SelectMotivoNotaCreditoDebito.CodMotivo == "IFPD" || SelectMotivoNotaCreditoDebito.CodMotivo == "GPED" || SelectMotivoNotaCreditoDebito.CodMotivo == "CBCD")
                {
                    var Lis = vrListCompraDetalle.FirstOrDefault();
                    item++;
                    ListECMP_NotaCreditoDebitoDetalle.Add(new ECMP_NotaCreditoDebitoDetalle()
                    {
                        Item = item,
                        ObjECMP_CompraDetalle = new ECMP_CompraDetalle()
                        {
                            Item = Lis.Item,
                            ObjECMP_Compra = Lis.ObjECMP_Compra,
                            Codigo = Lis.Codigo,
                            Cantidad = Lis.Cantidad,
							CodOperacionIGV = Lis.CodOperacionIGV,
                            PrecioUnitario = Lis.PrecioUnitario,
                            CodUndMedida = Lis.CodUndMedida,
                            ArticuloServicio = Lis.ArticuloServicio,
                            ImporteIGV = Lis.ImporteIGV,
                        }
                    });
                    ObjECMP_NotaCreditoDebito.ObjESGC_Moneda = MyECMP_Compra.ObjESGC_Moneda;
                    ObjECMP_NotaCreditoDebito.TipoCambio = MyECMP_Compra.TipoCambio;
                    CalcularTotales();
                }
                else
                {
                    vrListCompraDetalle.ForEach(x =>
                    {
                        item++;
                        decimal vrImporte;
                        if ((SelectMotivoNotaCreditoDebito.IdMotCreDeb == 2 || SelectMotivoNotaCreditoDebito.IdMotCreDeb == 3 || SelectMotivoNotaCreditoDebito.CodMotivo == "DEVC" || SelectMotivoNotaCreditoDebito.CodMotivo == "IFPD") && SelectMotivoNotaCreditoDebito.TipoNota == "C")
                            vrImporte = 0;
                        else
                            vrImporte = (x.PrecioUnitario * x.Cantidad);

                        ListECMP_NotaCreditoDebitoDetalle.Add(new ECMP_NotaCreditoDebitoDetalle()
                        {
                            Item = item,
                            ObjECMP_CompraDetalle = new ECMP_CompraDetalle()
                            {
                                Item = x.Item,
                                ObjECMP_Compra = x.ObjECMP_Compra,
                                Codigo = x.Codigo,
                                Cantidad = x.Cantidad,
								CodOperacionIGV = x.CodOperacionIGV,
                                PrecioUnitario = (x.PrecioUnitario),
                                CodUndMedida = x.CodUndMedida,
                                ArticuloServicio = x.ArticuloServicio,
                                ImporteIGV = x.ImporteIGV,
                                Importe = vrImporte
                            }
                        });
                    });
                    ObjECMP_NotaCreditoDebito.ObjESGC_Moneda = MyECMP_Compra.ObjESGC_Moneda;
                    ObjECMP_NotaCreditoDebito.TipoCambio = MyECMP_Compra.TipoCambio;
                    CalcularTotales();
                }
            }
        }

        private void RemoveItemDocReferencia(EMNF_DocumentoReferencia vrSelectDocReferencia)
        {
            int Item = 1;
            ObjECMP_NotaCreditoDebito.Total = 0;
            var Remover = ListECMP_NotaCreditoDebitoDetalle.Where(x => x.ObjECMP_CompraDetalle.ObjECMP_Compra.IdCompra == vrSelectDocReferencia.IdDocReferencia);
            Remover.ToList().ForEach(x => 
            {
                ListECMP_NotaCreditoDebitoDetalle.Remove(x);
            });

            if (ListECMP_NotaCreditoDebitoDetalle.Count > 0)
                ListECMP_NotaCreditoDebitoDetalle.ToList().ForEach(item =>
                {
                    item.Item = Item;
                    ObjECMP_NotaCreditoDebito.Total += item.ObjECMP_CompraDetalle.Importe;
                    Item++;
                });
            ListEMNF_DocumentoReferencia.Remove(SelectDocReferencia);
        }

        private bool ValidaDatos()
        {
            bool blEstado = false;

            if (SelectMotivoNotaCreditoDebito == null)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminNotaCreditoDebito, "Seleccione un Motivo", CmpButton.Aceptar);
                blEstado = true;
            }
            else if (ObjECMP_NotaCreditoDebito.ObjEMNF_ClienteProveedor == null)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminNotaCreditoDebito, "Seleccione un proveedor", CmpButton.Aceptar);
                blEstado = true;
            }
            else if( ObjECMP_NotaCreditoDebito.Serie==null || ObjECMP_NotaCreditoDebito.Numero==null ){
                CmpMessageBox.Show(CMPMensajes.TitleAdminNotaCreditoDebito, "Serie o Número son Obligatorio", CmpButton.Aceptar);
                blEstado = true;
            }
            else if (ObjECMP_NotaCreditoDebito.Serie != null || ObjECMP_NotaCreditoDebito.Numero != null)
            {
                if (ObjECMP_NotaCreditoDebito.Serie.Trim().Length == 0 || ObjECMP_NotaCreditoDebito.Numero.Trim().Length == 0)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminNotaCreditoDebito, "Serie o Número son Obligatorio", CmpButton.Aceptar);
                    blEstado = true;
                }
            }
            else if (ListECMP_NotaCreditoDebitoDetalle.Count == 0)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminNotaCreditoDebito, "Ingrese como mínimo un detalle", CmpButton.Aceptar);
                blEstado = true;
            }

            return blEstado;
        }

        private void CalcularTotales()
        {
            try
            {
				decimal dmlGravada = 0;
                decimal dmlExonerada = 0;
                decimal dmlImporteIGV = 0;
                decimal dmlIGV = 0;
                var MyDetalleFirst = ListECMP_NotaCreditoDebitoDetalle.FirstOrDefault();
                if (MyDetalleFirst == null && ListECMP_NotaCreditoDebitoDetalle.Count == 0)
                {
                    ObjECMP_NotaCreditoDebito.Gravada = Convert.ToDecimal("0.00");
                    ObjECMP_NotaCreditoDebito.Exonerada = Convert.ToDecimal("0.00");
                    ObjECMP_NotaCreditoDebito.ImporteIGV = Convert.ToDecimal("0.00");
                    ObjECMP_NotaCreditoDebito.Total = Convert.ToDecimal("0.00");
                    return;
                }

               if (MyDetalleFirst.ObjECMP_CompraDetalle.ObjECMP_Compra.IncluyeIGV)
                {
                   //Calculo con incluir IGV
                    if (State == "D")
                        dmlGravada = ListECMP_NotaCreditoDebitoDetalle.Sum(o => o.ObjECMP_CompraDetalle.Importe);
                    else
                        dmlGravada = ListECMP_NotaCreditoDebitoDetalle.Where(x => x.ObjECMP_CompraDetalle.CodOperacionIGV == "GB").Sum(o => o.ObjECMP_CompraDetalle.Importe);
                    dmlImporteIGV = dmlGravada * MyDetalleFirst.ObjECMP_CompraDetalle.ObjECMP_Compra.IGV;
                }
                else
                {
                    //Calculo sin incluir IGV
                    dmlGravada = ListECMP_NotaCreditoDebitoDetalle.Where(x => x.ObjECMP_CompraDetalle.CodOperacionIGV == "GB").Sum(o => o.ObjECMP_CompraDetalle.Importe);
                    dmlImporteIGV = dmlGravada * MyDetalleFirst.ObjECMP_CompraDetalle.ObjECMP_Compra.IGV;
                }

                dmlIGV = MyDetalleFirst.ObjECMP_CompraDetalle.ObjECMP_Compra.IGV;
                dmlExonerada = ListECMP_NotaCreditoDebitoDetalle.Where(x => x.ObjECMP_CompraDetalle.CodOperacionIGV == "EX").Sum( o => o.ObjECMP_CompraDetalle.Importe );

				ObjECMP_NotaCreditoDebito.IGV = Decimal.Round(dmlIGV, 2);
                ObjECMP_NotaCreditoDebito.Exonerada = Decimal.Round(dmlExonerada, 2);
                ObjECMP_NotaCreditoDebito.Gravada = Decimal.Round(dmlGravada, 2);
                ObjECMP_NotaCreditoDebito.ImporteIGV = Decimal.Round(dmlImporteIGV, 2);
                ObjECMP_NotaCreditoDebito.Total = Decimal.Round((dmlExonerada + dmlGravada + dmlImporteIGV), 2);
                PorcentajeIgv = Convert.ToDouble(MyDetalleFirst.ObjECMP_CompraDetalle.ObjECMP_Compra.IGV * 100).ToString("N2") + " %";

                TitleTotal = "Total " + ObjECMP_NotaCreditoDebito.ObjESGC_Moneda.Simbolo;
            }
            catch (Exception ex) 
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminNotaCreditoDebito, ex.Message, CmpButton.Aceptar);
            }
        }

        private string XmlDetalle()
        {
            string strCadXml = "";
            strCadXml = "<ROOT>";
            int IdMotCreDeb = SelectMotivoNotaCreditoDebito.IdMotCreDeb;
            ListECMP_NotaCreditoDebitoDetalle.ToList().ForEach((f) =>
                {
                    strCadXml += "<Listar xItem = \'" + f.Item;
                    strCadXml += "\' xIdCompra = \'" + f.ObjECMP_CompraDetalle.ObjECMP_Compra.IdCompra;
                    strCadXml += "\' xItemCompra = \'" + ((IdMotCreDeb == 5 || IdMotCreDeb == 7 || IdMotCreDeb == 8) ? 0 : f.ObjECMP_CompraDetalle.Item);
                    strCadXml += "\' xCantidadDevolver =\'" + f.CantidaDevolver;
                    strCadXml += "\' xPrcDscBonificacion =\'" + f.PrcDscBonificacion;
                    strCadXml += "\' xImpDscBonificacion =\'" + f.ImpDscBonificacion;
                    strCadXml += "\' xPreDscBoniOmision =\'" + f.PreDscBoniOmision;
                    strCadXml += "\' xImporte = \'" + f.ObjECMP_CompraDetalle.Importe;
                    strCadXml += "\'></Listar>";
                });
            strCadXml += "</ROOT>";
            return strCadXml;
        }

        private string XmlDocRef()
    {
        string strCadXml = "";
        strCadXml = "<ROOT>";
        ListEMNF_DocumentoReferencia.ToList().ForEach((x) => 
        {
            strCadXml += "<Listar xIdReferencia = \'" + x.IdDocReferencia;
            strCadXml += "\' xDocReferencia =\'" + "CMP";
            strCadXml += "\'></Listar>";
        });
        strCadXml += "</ROOT>";
        return strCadXml;
    }

        #endregion
    }
}
