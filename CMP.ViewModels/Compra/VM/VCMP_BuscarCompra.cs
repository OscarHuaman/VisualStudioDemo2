/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 16/02/2016
**********************************************************
'* MODIFICADO POR : COMPUTER SYSTEMS SOLUTION
'*                  OSCAR HUAMAN CABRERA
'* FCH. MODIFICACION : 26/09/2016
**********************************************************/
namespace CMP.ViewModels.Compra.VM
{
    using CMP.Business;
    using CMP.Entity;
    using CMP.Useful.Modulo;
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Notificaciones;
    using ComputerSystems.WPF.Acciones.Controles.Buttons;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Input;
    using MahApps.Metro.Controls;
    using System.Windows;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class VCMP_BuscarCompra : CmpNotifyPropertyChanged
    {
        public Action<ObservableCollection<ECMP_Compra>> MySelectItem { get; set; }
        private TipoDocumento MyTipoDocumento { get; set; }
        private TipoBusqueda MyTipoBusqueda { get; set; }
        private TipoModulo MyTipoModulo { get; set; }
        private DataGrid MyDataGrid;

        public VCMP_BuscarCompra() { }
        public VCMP_BuscarCompra(DataGrid dataGrid) 
        {
            this.MyDataGrid = dataGrid;
        }

        public Flyout SetKeyDownCmpButtonTitleTecla
        {
            set
            {
                if (value != null)
                {
                    value.KeyDownCmpButtonTitleTecla(
                        ActionF7: () => { if (ISelectedItem.CanExecute(true)) ISelectedItem.Execute(string.Empty); },
                        ActionCtrlD: () => { if (IVolver.CanExecute(true)) IVolver.Execute(string.Empty); }
                        );
                }
                Monto = 0;
            }
        }

        #region COLECCIONES DE DATOS

        private CmpObservableCollection<ECMP_Compra> _ListECMP_Compra;
        public CmpObservableCollection<ECMP_Compra> ListECMP_Compra
        {
            get 
            {
                if (_ListECMP_Compra == null)
                    _ListECMP_Compra = new CmpObservableCollection<ECMP_Compra>();
                return _ListECMP_Compra;
            }
            set 
            {
                _ListECMP_Compra = value;
                OnPropertyChanged("ListECMP_Compra");
            }
        }

        private CmpObservableCollection<ECMP_Compra> _MultiSelectionItem;
        public CmpObservableCollection<ECMP_Compra> MultiSelectionItem
        {
            get
            {
                if (_MultiSelectionItem == null)
                    _MultiSelectionItem = new CmpObservableCollection<ECMP_Compra>();
                return _MultiSelectionItem;
            }
        }

        private CmpObservableCollection<ECMP_Compra> _ListECMP_CompraSeleccion;
        public CmpObservableCollection<ECMP_Compra> ListECMP_CompraSeleccion
        {
            get
            {
                if (_ListECMP_CompraSeleccion == null)
                    _ListECMP_CompraSeleccion = new CmpObservableCollection<ECMP_Compra>();
                return _ListECMP_CompraSeleccion;
            }
            set
            {
                _ListECMP_CompraSeleccion = value;
                OnPropertyChanged();
            }
        }

        private CmpObservableCollection<ECMP_Compra> vrSelectedItems { get; set; }
        
        #endregion

        #region OBJ SECUNDARIO 

        private string _CodMotMovimiento;
        public string CodMotMovimiento
        {
            get
            {
                return _CodMotMovimiento;
            }
            set
            {
                _CodMotMovimiento = value;
                OnPropertyChanged("CodMotMovimiento");
            }
        }

        private bool _IsOpenFlyout;
        public bool IsOpenFlyout 
        {
            get 
            {
                return _IsOpenFlyout;
            }
            set 
            {
                _IsOpenFlyout = value;
                OnPropertyChanged();
            }
        }

        private string _FiltrarDocumento;
        public string FiltrarDocumento
        {
            get
            {
                return _FiltrarDocumento;
            }
            set
            {
                _FiltrarDocumento = value;
                LoadDetail(IdClienteProveedor, CodMoneda, MyTipoDocumento, MyTipoBusqueda, MyTipoModulo);
                OnPropertyChanged("FiltrarDocumento");
            }
        }

        private int _IdClienteProveedor;
        public int IdClienteProveedor
        {
            get
            {
                return _IdClienteProveedor;
            }
            set
            {
                _IdClienteProveedor = value;
                OnPropertyChanged();
            }
        }

        private string _CodMoneda;
        public string CodMoneda
        {
            get
            {
                return _CodMoneda;
            }
            set
            {
                _CodMoneda = value;
                OnPropertyChanged();
            }
        }

		private bool _IsVisibleColumnTotal;
        public bool IsVisibleColumnTotal
        {
            get
            {
                return _IsVisibleColumnTotal;
            }
            set
            {
                _IsVisibleColumnTotal = value;
                OnPropertyChanged("IsVisibleColumnTotal");
            }
        }

        private bool _IsVisibleColumnSaldo;
        public bool IsVisibleColumnSaldo
        {
            get
            {
                return _IsVisibleColumnSaldo;
            }
            set
            {
                _IsVisibleColumnSaldo = value;
                OnPropertyChanged("IsVisibleColumnSaldo");
            }
        }

        private bool _IsVisibleColumnDetraccion;
        public bool IsVisibleColumnDetraccion
        {
            get
            {
                return _IsVisibleColumnDetraccion;
            }
            set
            {
                _IsVisibleColumnDetraccion = value;
                OnPropertyChanged("IsVisibleColumnDetraccion");
            }
        }

        private Visibility _VisibilityMonto;
        public Visibility VisibilityMonto
        {
            get
            {
                return _VisibilityMonto;
            }
            set
            {
                _VisibilityMonto = value;
                OnPropertyChanged("VisibilityMonto");
            }
        }

        private Visibility _VisibilitySumaSeleccionados;
        public Visibility VisibilitySumaSeleccionados
        {
            get
            {
                return _VisibilitySumaSeleccionados;
            }
            set
            {
                _VisibilitySumaSeleccionados = value;
                OnPropertyChanged("VisibilitySumaSeleccionados");
            }
        }

        private int _ColumnSpanBuscador;
        public int ColumnSpanBuscador
        {
            get
            {
                return _ColumnSpanBuscador;
            }
            set
            {
                _ColumnSpanBuscador = value;
                OnPropertyChanged("ColumnSpanBuscador");
            }
        }

        private decimal _SumaSeleccionados;
        public decimal SumaSeleccionados
        {
            get
            {
                return _SumaSeleccionados;
            }
            set
            {
                _SumaSeleccionados = value;
                OnPropertyChanged("SumaSeleccionados");
            }
        }

        private decimal _Monto;
        public decimal Monto
        {
            get
            {
                return _Monto;
            }
            set
            {
                _Monto = value;
                if (value > 0)
                {
                    VisibilityCompraSeleccion = Visibility.Visible;
                }
                else
                {
                    ListECMP_CompraSeleccion.Clear();
                    VisibilityCompraSeleccion = Visibility.Collapsed;
                }
                Calcular();
                OnPropertyChanged("Monto");
            }
        }

        private Visibility _VisibleChkDetraccion;
        public Visibility VisibleChkDetraccion
        {
            get
            {
                return _VisibleChkDetraccion;
            }
            set
            {
                _VisibleChkDetraccion = value;
                OnPropertyChanged("VisibleChkDetraccion");
            }
        }

        private ECMP_Compra _SelectedItem;
        public ECMP_Compra SelectedItem
        {
            get
            {
                return _SelectedItem;
            }
            set
            {
                _SelectedItem = value;
                //if (VisibilityCompraSeleccion == Visibility.Collapsed)
                //    Calcular();
                OnPropertyChanged("SelectedItem");
            }
        }

        private ECMP_Compra _SelectedItemSeleccion;
        public ECMP_Compra SelectedItemSeleccion
        {
            get
            {
                return _SelectedItemSeleccion;
            }
            set
            {
                _SelectedItemSeleccion = value;
                OnPropertyChanged("SelectedItemSeleccion");
            }
        }

        private decimal vrResta { get; set; }

        private Visibility _VisibilityCompraSeleccion;
        public Visibility VisibilityCompraSeleccion
        {
            get
            {
                return _VisibilityCompraSeleccion;
            }
            set
            {
                _VisibilityCompraSeleccion = value;
                OnPropertyChanged("VisibilityCompraSeleccion");
            }
        }

        #endregion

        #region PROPERTY

        private string _PropertyHeaderDetraccion;
        public string PropertyHeaderDetraccion
        {
            get
            {
                return _PropertyHeaderDetraccion;
            }
            set
            {
                _PropertyHeaderDetraccion = value;
                OnPropertyChanged("PropertyHeaderDetraccion");
            }
        }

        private bool _IsVisibleColumnMoneda;
        public bool IsVisibleColumnMoneda
        {
            get
            {
                return _IsVisibleColumnMoneda;
            }
            set
            {
                _IsVisibleColumnMoneda = value;
                OnPropertyChanged("IsVisibleColumnMoneda");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand ICalcular
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    Calcular();
                });
            }
        }

        public ICommand ISelectedItem
        {
            get
            {
                return CmpICommand.GetICommand(((I) => 
                {
                    if ((VisibilityCompraSeleccion == Visibility.Visible && ListECMP_CompraSeleccion.Count == 0) || MultiSelectionItem.Count == 0)
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, CMPMensajes.IsNullSelectItem, CmpButton.Aceptar);
                        return;
                    }
                    
                    vrSelectedItems = new CmpObservableCollection<ECMP_Compra>();
                    if (VisibilityCompraSeleccion == Visibility.Visible)
                    {
                        decimal dmlSuma = 0;
                        if (Monto > 0)
                            foreach (var item in ListECMP_CompraSeleccion)
                            {
                                dmlSuma += item.SaldoCompra;
                                if (dmlSuma <= Monto)
                                    vrSelectedItems.Add(item);
                                else if ((dmlSuma - Monto) < item.SaldoCompra)
                                {
                                    item.SaldoCompra = item.SaldoCompra - (dmlSuma - Monto);
                                    vrSelectedItems.Add(item);
                                    break;
                                }
                            }
                        else
                            vrSelectedItems = ListECMP_CompraSeleccion;
                    }
                    else
                    {
                        vrSelectedItems = MultiSelectionItem;
                    }

					if (MySelectItem != null)
                        MySelectItem.Invoke(vrSelectedItems);
                }));
              
            }
        }

        public ICommand IAddItemSelection
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (VisibilityCompraSeleccion == Visibility.Visible)
                    {
                        if (!ListECMP_CompraSeleccion.ToList().Exists(x => x.IdCompra == SelectedItem.IdCompra) && (SumaSeleccionados + SelectedItem.SaldoCompra + (SelectedItem.IncluyeDetraccionCompra ? SelectedItem.DetraccionCompra : 0)) <= Monto)
                        {
                            SelectedItem.Total = (SelectedItem.SaldoCompra + ((SelectedItem.IncluyeDetraccionCompra) ? SelectedItem.DetraccionCompra : 0));
                            ListECMP_CompraSeleccion.Add(SelectedItem);
                        }

                        Calcular();
                    }
                    else
                    {
                        if (ISelectedItem.CanExecute(string.Empty)) ISelectedItem.Execute(string.Empty);
                    }
                });
            }
        }

        public ICommand IEliminarRef
        {
            get
            {
                return CmpICommand.GetICommand((E) =>
                {
                    ListECMP_CompraSeleccion.Remove(SelectedItemSeleccion);
                    Calcular();
                });
            }
        }

        public ICommand IVolver
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    IsOpenFlyout = false;
                });
            }
        }

        #endregion

        #region METODOS

        public async void LoadDetail(int IdClienteProveedor, string CodMoneda, TipoDocumento MyTipoDocumento, TipoBusqueda MyTipoBusqueda = TipoBusqueda.Todo, TipoModulo MyTipoModulo = TipoModulo.Todo)
        {
            this.MyTipoDocumento = MyTipoDocumento;
            this.MyTipoBusqueda = MyTipoBusqueda;
            this.MyTipoModulo = MyTipoModulo;
            this.IdClienteProveedor = IdClienteProveedor;
            this.CodMoneda = (CodMoneda == null || CodMoneda.Trim().Length == 0) ? "%" : CodMoneda;

            await Task.Factory.StartNew(
            () =>
            {
                try
                {
                    List<ECMP_Compra> vrListCompra = new List<ECMP_Compra>();

                    if (MyTipoModulo == TipoModulo.CJB)
                    {
                        vrListCompra = new BCMP_Compra().ListCompraBusqDocumentoCajaBanco(new ECMP_Compra()
                        {
                            ObjEMNF_ClienteProveedor = new MNF.Entity.EMNF_ClienteProveedor()
                            {
                                IdCliProveedor = IdClienteProveedor
                            },
                            ObjESGC_Moneda = new SGC.Empresarial.Entity.ESGC_Moneda()
                            {
                                CodMoneda = CodMoneda
                            }
                        }, FiltrarDocumento, (CodMotMovimiento == "EDB") ? 1 : 0).Where(x => (x.CodEstado == "PECMP" || x.CodEstado == "ATCMP")).ToList();
                    }
                    else
                    {
                        if (MyTipoBusqueda != TipoBusqueda.Anticipo)
                        {
                            vrListCompra = new BCMP_Compra().ListCompraBusqDocument(new ECMP_Compra()
                            {
                                ObjEMNF_ClienteProveedor = new MNF.Entity.EMNF_ClienteProveedor()
                                {
                                    IdCliProveedor = IdClienteProveedor
                                },
                                ObjESGC_Moneda = new SGC.Empresarial.Entity.ESGC_Moneda()
                                {
                                    CodMoneda = CodMoneda
                                }
                            }, FiltrarDocumento).Where(x => (x.CodEstado == "PECMP" || x.CodEstado == "ATCMP")).ToList();
                        }
                        else
                        {

                            vrListCompra = new BCMP_Compra().ListCompraBusqDocumentoAnticipo(new ECMP_Compra()
                            {
                                ObjEMNF_ClienteProveedor = new MNF.Entity.EMNF_ClienteProveedor()
                                {
                                    IdCliProveedor = IdClienteProveedor
                                },
                                ObjESGC_Moneda = new SGC.Empresarial.Entity.ESGC_Moneda()
                                {
                                    CodMoneda = CodMoneda
                                }
                            }, FiltrarDocumento).Where(x => (x.CodEstado == "PECMP" || x.CodEstado == "ATCMP")).ToList();
                            PropertyHeaderDetraccion = "Detracción";
                        }
                    }

                    if (MyTipoBusqueda == TipoBusqueda.Anticipo || MyTipoBusqueda == TipoBusqueda.Todo)
					{
                        vrListCompra = (MyTipoBusqueda == TipoBusqueda.Anticipo) ? vrListCompra.Where(x => x.Anticipo == true).ToList() : vrListCompra.Where(x => x.SaldoCompra != 0).ToList();
                        IsVisibleColumnTotal = true;
                        IsVisibleColumnSaldo = true;
                        IsVisibleColumnDetraccion = true;
					}
                    else if (MyTipoBusqueda == TipoBusqueda.Detraccion)
                    {
                        vrListCompra = vrListCompra.Where(x => x.PagoDetraccion == false && x.DetraccionCompra != 0).ToList();
                        IsVisibleColumnTotal = false;
                        IsVisibleColumnSaldo = false;
                        IsVisibleColumnDetraccion = true;
                    }
					else if (MyTipoBusqueda == TipoBusqueda.Letra)
                    {
                        IsVisibleColumnTotal = false;
                        IsVisibleColumnSaldo = true;
                        IsVisibleColumnDetraccion = true;
                    }

                    if (MyTipoDocumento == TipoDocumento.Boleta)
                        ListECMP_Compra.Source = new CmpObservableCollection<ECMP_Compra>(vrListCompra.Where(x => x.CodDocumento == "BOL").ToList());
                    else if (MyTipoDocumento == TipoDocumento.Factura)
                        ListECMP_Compra.Source = new CmpObservableCollection<ECMP_Compra>(vrListCompra.Where(x => x.CodDocumento == "FAC").ToList());
                    else if (MyTipoDocumento == TipoDocumento.Ticket)
                        ListECMP_Compra.Source = new CmpObservableCollection<ECMP_Compra>(vrListCompra.Where(x => x.CodDocumento == "TCK").ToList());
                    else if (MyTipoDocumento == TipoDocumento.Recibo)
                        ListECMP_Compra.Source = new CmpObservableCollection<ECMP_Compra>(vrListCompra.Where(x => x.CodDocumento == "RCB").ToList());
                    else if (MyTipoDocumento == TipoDocumento.RIG)
                        ListECMP_Compra.Source = new CmpObservableCollection<ECMP_Compra>(vrListCompra.Where(x => x.CodDocumento == "RIG").ToList());
                    else if (MyTipoDocumento == TipoDocumento.Todo)
                        ListECMP_Compra.Source = new CmpObservableCollection<ECMP_Compra>(vrListCompra);

                    if (MyTipoModulo == TipoModulo.LTR)
                    {
                        ColumnSpanBuscador = 2;
                        VisibilityMonto = Visibility.Hidden;
                        VisibilitySumaSeleccionados = Visibility.Hidden;
                        VisibleChkDetraccion = Visibility.Visible;
                    }
                    else if (MyTipoModulo == TipoModulo.CJB)
                    {
                        ColumnSpanBuscador = (CodMotMovimiento != "EDB") ? 1 : 2;
                        VisibilityMonto = (CodMotMovimiento != "EDB") ? Visibility.Visible : Visibility.Collapsed;
                        VisibilitySumaSeleccionados = (CodMotMovimiento != "EDB") ? Visibility.Visible : Visibility.Collapsed;
                        VisibleChkDetraccion = Visibility.Visible;
                    }
                    else if (MyTipoModulo == TipoModulo.Todo)
                    {
                        ColumnSpanBuscador = 2;
                        VisibilityMonto = Visibility.Hidden;
                        VisibilitySumaSeleccionados = Visibility.Hidden;
                        VisibleChkDetraccion = Visibility.Hidden;
                    }
                    MethodModifiedDataGrid(CodMotMovimiento);
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        private void Calcular()
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler((s, e) =>
            {
                 SumaSeleccionados = (VisibilityCompraSeleccion == Visibility.Visible) ? ListECMP_CompraSeleccion.Sum(x => x.SaldoCompra + (x.IncluyeDetraccionCompra ? x.DetraccionCompra : 0)) : MultiSelectionItem.Sum(x => x.SaldoCompra + (x.IncluyeDetraccionCompra ? x.DetraccionCompra : 0));
                
                if (Monto > 0 && decimal.Round(SumaSeleccionados, 2) > Monto)
                {
                    vrResta = SumaSeleccionados - Monto;
                    CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "La Suma de Elementos Seleccionados supera el monto ingresado en " + vrResta.ToString("N2"), CmpButton.Aceptar);
                }
                dispatcherTimer.Stop();
            });
            if (SelectedItem != null)
            {
                if (VisibilityCompraSeleccion == Visibility.Visible)
                    ListECMP_CompraSeleccion.ToList().ForEach(x =>
                    {
                        x.Total = (x.SaldoCompra + ((x.IncluyeDetraccionCompra) ? x.DetraccionCompra : 0));
                    });
            }
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            dispatcherTimer.Start();
        }

        private void MethodModifiedDataGrid(string strCodMotMovimiento)
        {
            if (strCodMotMovimiento != null && strCodMotMovimiento == "EDB")
            {
                IsVisibleColumnMoneda = false;
                PropertyHeaderDetraccion = "Detracción S/.";
            }
            else
            {
                IsVisibleColumnMoneda = true;
                PropertyHeaderDetraccion = "Detracción";
            }
        }

        #endregion

    }

    public enum TipoDocumento 
    {
        Boleta,
        Factura,
        Recibo,
        Ticket,
        RIG,
        Todo
    }

    public enum TipoBusqueda
    {
        Anticipo,
        Detraccion,
		Letra,
        Todo
    }

    public enum TipoModulo
    {
        LTR,
        CJB,
        Todo
    }
}
