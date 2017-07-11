/*********************************************************
'* ENTIDAD PARA REPORTE STOCK MINIMO
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   JUAN GUERRA MENESES
'* FCH. CREACIÓN : 19/02/2016
**********************************************************/
namespace CMP.ViewModels.ReporteStockMinimo.VM
{
    using ALM.Business;
    using ALM.Entity;
    using CMP.Business;
    using CMP.Entity;
    using CMP.Reports;
    using CMP.Useful.Modulo;
    using ComputerSystems;
    using ComputerSystems.MethodList;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using ComputerSystems.WPF.Acciones.Controles.Buttons;
    using MahApps.Metro.Controls;
    using MNF.Business;
    using MNF.Entity;
    using MNF.Presentation.Articulo.Flyouts;
    using SGC.Empresarial.Business;
    using SGC.Empresarial.Entity;
    using SGC.Empresarial.Useful.Modulo;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Input;
    using ComputerSystems.Loading;
    using System.Windows;

    public class VCMP_ListadoStockMinimo : CmpNavigationService, CmpINavigation
    {        
        public ECMP_ReporteStockMinimo ObjECMP_ReporteStockMinimo { get; set; }

        private CmpLoading CmpLoading { get; set; }

        public object Parameter
        {
            set
            {
                if (value is ECMP_ReporteStockMinimo)
                {
                    CmpLoading = new ComputerSystems.Loading.CmpLoading(LoadHeader,LoadDetail);
                    CmpLoading.Exceptions = ((e) => { CmpMessageBox.Show(CMPMensajes.TitleMessage, e.Message, CmpButton.Aceptar); });
                    ObjECMP_ReporteStockMinimo = new ECMP_ReporteStockMinimo();
                    ObjECMP_ReporteStockMinimo.IdEmpresa = SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.IdEmpresa;
                    ObjECMP_ReporteStockMinimo.ObjESGC_UsuarioEmpresaSucursal.ObjESGC_EmpresaSucursal = new ESGC_EmpresaSucursal() { IdEmpSucursal = 1 };
                    ObjECMP_ReporteStockMinimo.ObjEALM_Almacen = new EALM_Almacen() { IdAlmacen = 2 };
                    ObjECMP_ReporteStockMinimo.ObjEMNF_ArticuloClase = new EMNF_ArticuloClase() { IdArtClase = 1 };
                    ObjECMP_ReporteStockMinimo.ObjEMNF_ArticuloMarca = new EMNF_ArticuloMarca() {IdMarca = 1};                    
                    ObjECMP_ReporteStockMinimo.ObjEMNF_Articulo = new EMNF_Articulo() {IdArticulo = 0 };
                    CmpLoading.LoadHeader();                   
                }
            }
        }

        private Frame _MyFrame;
        public Frame MyFrame
        {
            set
            {
                #region Flyout Producto
                PMNF_BuscarArticulos ObjPMNF_BuscarArticulos = new PMNF_BuscarArticulos();
                ObjPMNF_BuscarArticulos.IsSelected += new PMNF_BuscarArticulos.isSelected((item) =>
                {
                    ObjECMP_ReporteStockMinimo.ObjEMNF_Articulo = item;
                    LoadDetail();
                });

                #endregion

                value.FlyoutInitialize();
                value.FlyoutAdd(ObjPMNF_BuscarArticulos);
                _MyFrame = value;
				if (value != null)
                {
                    value.KeyDownCmpButtonTitleTecla(
                        ActionF9: () => { if (IImprimir.CanExecute(true)) IImprimir.Execute(string.Empty); },
                        ActionShiftE: () => { if (IExportar.CanExecute(true))IExportar.Execute(string.Empty); },
                        ActionESC: () => { if (IClose.CanExecute(true))IClose.Execute(string.Empty); });
                }
            }
        }

        #region COLECCION DE DATOS

        private ObservableCollection<ECMP_ReporteStockMinimo> _ListECMP_ReporteStockMinimo;
        private ObservableCollection<ESGC_UsuarioEmpresaSucursal> _ListESGC_UsuarioEmpresaSucursal;
        private ObservableCollection<EALM_Almacen> _ListEALM_Almacen;
        private ObservableCollection<EMNF_ArticuloClase> _ListEMNF_ArticuloClase;
        private ObservableCollection<EMNF_ArticuloMarca> _ListEMNF_ArticuloMarca;

        public ObservableCollection<ECMP_ReporteStockMinimo> ListECMP_ReporteStockMinimo 
        {
            get 
            {
                if (_ListECMP_ReporteStockMinimo == null)
                    _ListECMP_ReporteStockMinimo = new ObservableCollection<ECMP_ReporteStockMinimo>();
                return _ListECMP_ReporteStockMinimo;
            }
            set 
            {
                _ListECMP_ReporteStockMinimo = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ESGC_UsuarioEmpresaSucursal> ListESGC_UsuarioEmpresaSucursal 
        {
            get 
            {
                if (_ListESGC_UsuarioEmpresaSucursal == null)
                    _ListESGC_UsuarioEmpresaSucursal = new ObservableCollection<ESGC_UsuarioEmpresaSucursal>();
                return _ListESGC_UsuarioEmpresaSucursal;
            }
            set 
            {
                _ListESGC_UsuarioEmpresaSucursal = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<EALM_Almacen> ListEALM_Almacen 
        {
            get 
            {
                if(_ListEALM_Almacen  == null)
                    _ListEALM_Almacen = new ObservableCollection<EALM_Almacen>();
                return _ListEALM_Almacen;
            }
            set 
            {
                _ListEALM_Almacen = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<EMNF_ArticuloClase> ListEMNF_ArticuloClase 
        {
            get 
            {
                if (_ListEMNF_ArticuloClase == null)
                    _ListEMNF_ArticuloClase = new ObservableCollection<EMNF_ArticuloClase>();
                return _ListEMNF_ArticuloClase;
            }
            set 
            {
                _ListEMNF_ArticuloClase = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<EMNF_ArticuloMarca> ListEMNF_ArticuloMarca 
        {
            get 
            {
                if (_ListEMNF_ArticuloMarca == null)
                    ListEMNF_ArticuloMarca = new ObservableCollection<EMNF_ArticuloMarca>();
                return _ListEMNF_ArticuloMarca;
            }
            set 
            {
                _ListEMNF_ArticuloMarca = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region OBJ SECUNDARIOS

        private ESGC_UsuarioEmpresaSucursal _SelectUsuarioEmpresaSucursal;
        private EALM_Almacen _SelectAlmacen;
        private EMNF_ArticuloClase _SelectArticuloClase;
        private EMNF_ArticuloMarca _SelectArticuloMarca;

        public ESGC_UsuarioEmpresaSucursal SelectUsuarioEmpresaSucursal
        {            
            get 
            {
                if (_SelectUsuarioEmpresaSucursal == null)
                {
                    _SelectUsuarioEmpresaSucursal = new ESGC_UsuarioEmpresaSucursal();
                }
                return _SelectUsuarioEmpresaSucursal;
            }
            set 
            {
                _SelectUsuarioEmpresaSucursal = value;
                if (value  != null)
                {
                    LoadAlmacen();
                }
                ObjECMP_ReporteStockMinimo.ObjESGC_UsuarioEmpresaSucursal = value;
                LoadDetail();
            }
        }
        public EMNF_ArticuloClase SelectArticuloClase 
        {
            get 
            {
                if (_SelectArticuloClase == null)
                    _SelectArticuloClase = new EMNF_ArticuloClase();
                return _SelectArticuloClase;
            }
            set 
            {
                _SelectArticuloClase = value;
                ObjECMP_ReporteStockMinimo.ObjEMNF_ArticuloClase = value;
                LoadDetail();
            }
        }
        public EMNF_ArticuloMarca SelectArticuloMarca
        {
            get
            {
                if (_SelectArticuloMarca == null)
                    _SelectArticuloMarca = new EMNF_ArticuloMarca();
                return _SelectArticuloMarca;
            }
            set
            {
                _SelectArticuloMarca = value;
                ObjECMP_ReporteStockMinimo.ObjEMNF_ArticuloMarca = value;
                LoadDetail();
            }
        }
        public EALM_Almacen SelectAlmacen 
        {
            get 
            {
                if (_SelectAlmacen == null)
                    _SelectAlmacen = new EALM_Almacen();
                return _SelectAlmacen;
            }
            set 
            {
                _SelectAlmacen = value;
                if (value == null)
                {
                    value = new EALM_Almacen() { IdAlmacen = 0 };
                    _SelectAlmacen = value;
                }
                ObjECMP_ReporteStockMinimo.ObjEALM_Almacen = value;
                LoadDetail();
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand IBuscarProducto
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (T.ToString().Trim().Length == 0)
                    {
                        ObjECMP_ReporteStockMinimo.ObjEMNF_Articulo = new EMNF_Articulo() { IdArticulo = 0 };
                        LoadDetail();
                    }

                    var vrObjArticulo = new EMNF_Articulo()
                    {
                        ObjEMNF_ArticuloClase = SelectArticuloClase,
                        ObjEMNF_ArticuloMarca = SelectArticuloMarca,
                        ObjEMNF_ArticuloSubCategoria = new EMNF_ArticuloSubCategoria() { IdSubCategoria = 0  }
                    };

                    var vrCategoria =   new EMNF_ArticuloCategoria() { IdCategoria = 0 };
                    var vrListEMNF_Articulo = new ObservableCollection<EMNF_Articulo>();

                    string strOutMessageError = string.Empty;
                    CmpTask.Process(
                    () =>
                    {
                        try
                        {
                            vrListEMNF_Articulo = new ObservableCollection<EMNF_Articulo>(new BMNF_Articulo().ListGetArticulos(vrObjArticulo, vrCategoria, T.ToString()));
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
                            CmpMessageBox.Show(CMPMensajes.TitleMessage, strOutMessageError, CmpButton.Aceptar);
                        }
                        else
                        {
                            if (vrListEMNF_Articulo.Count == 1)
                            {
                                ObjECMP_ReporteStockMinimo.ObjEMNF_Articulo = vrListEMNF_Articulo.FirstOrDefault();
								LoadDetail();
                            }
                            else
                            {
                                _MyFrame.FlyoutIsOpen("PMNF_BuscarArticulos", new Action<Flyout>((value) =>
                                {
                                    if (value is PMNF_BuscarArticulos)
                                    {
                                        var MyFlyout = (PMNF_BuscarArticulos)value;
                                        MyFlyout.InitializePMNF_BuscarArticulos();
                                        MyFlyout.SetValueFilter = T.ToString();
                                        MyFlyout.IsOpen = true;
                                        MyFlyout.LoadHeader();
                                    }
                                }));
                            }
                        }
                    });
                });
            }
        }

        public ICommand IImprimir
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    try
                    {
                        if (ListECMP_ReporteStockMinimo.Count == 0)
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleMessage, "No existen registros por imprimir", CmpButton.Aceptar);
                            return;
                        }
                        string[] parametro;
                        parametro = new string[]
                        {
                            "prmAlmacen|"    + ((SelectAlmacen != null || SelectAlmacen.IdAlmacen != 0 ) ?  SelectAlmacen.Almacen: "TODOS"),
                            "prmMarca|"      + ((SelectArticuloMarca.IdMarca == 0) ? "TODOS" : SelectArticuloMarca.Marca),
                            "prmClase|"      + ((SelectArticuloClase.IdArtClase == 0) ? "TODOS"  : SelectArticuloClase.Clase),
                            "prmArticulo|"   + ((ObjECMP_ReporteStockMinimo.ObjEMNF_Articulo.IdArticulo  == 0) ? "TODOS" : ObjECMP_ReporteStockMinimo.ObjEMNF_Articulo.Articulo  )
                        };

                        MainRerport ObjMainRerport = new MainRerport();
                        ObjMainRerport.InitializeMainRerport("CMP.Reports.Files.RptStockMinimo.rdlc", "DtsStockMinimo", ListECMP_ReporteStockMinimo, parametro);
                        ObjMainRerport.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, ex.Message, CmpButton.Aceptar);
                    }
                });
            }
        }

        public ICommand IExportar
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    try
                    {
                        if (ListECMP_ReporteStockMinimo.Count == 0)
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleMessage, "No hay registros que exportar", CmpButton.Aceptar);
                            return;
                        }
                        var ListExport = ListECMP_ReporteStockMinimo.Select((x) => new
                        {
                            SubCategoria = x.SubCategoria,
                            Marca = x.ObjEMNF_ArticuloMarca.Marca,
                            Articulo = x.ObjEMNF_Articulo.Articulo,
                            Sucursal = x.ObjEALM_Almacen.ObjESGC_EmpresaSucursal.Sucursal,
                            Almacen = x.ObjEALM_Almacen.Almacen,
                            StockMinimo = x.StockMinimo.ToString("###,###,##0.##0"),
                            StockActual = x.StockActual.ToString("###,###,##0.#######0")

                        }).ToList();
                        ListExport.Export("ListReporteStockMinimo", ExportType.Excel, (value) =>
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleMessage, value, CmpButton.Aceptar);
                        });
                    }
                    catch (Exception ex)
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleMessage, ex.Message, CmpButton.Aceptar);
                    }
                });
            }
        }

        public ICommand IClose
        {
            get
            {
                return CmpICommand.GetICommand((T) => { _MyFrame.Close(TipoModulo.ManuFactura); });
            }
        }

        #endregion

        #region METODOS

        private void LoadHeader()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                string strOutMessageError = string.Empty;
                CmpTask.ProcessAsync(
                () =>
                {
                    ListEMNF_ArticuloClase = new ObservableCollection<EMNF_ArticuloClase>(new BMNF_ArticuloClase().ListFiltrarArticuloClase("%"));
                    ListEMNF_ArticuloMarca = new ObservableCollection<EMNF_ArticuloMarca>(new BMNF_ArticuloMarca().ListFiltrarMarca("%"));
                    ListESGC_UsuarioEmpresaSucursal = new ObservableCollection<ESGC_UsuarioEmpresaSucursal>(new BSGC_UsuarioEmpresaSucursal().ListFiltrarUsuarioEmpresaSucursal(SGCVariables.ObjESGC_Usuario));
                },
                (e) =>
                {
                    if (e != null)
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleMessage, e.Message, CmpButton.Aceptar);
                        return;
                    }
                    else
                    {
                        ListEMNF_ArticuloClase.Add(new EMNF_ArticuloClase() { Clase = "TODOS", IdArtClase = 0 });
                        ListEMNF_ArticuloMarca.Add(new EMNF_ArticuloMarca() { Marca = "TODOS", IdMarca = 0 });
                        ListESGC_UsuarioEmpresaSucursal.Add(new ESGC_UsuarioEmpresaSucursal()
                        {
                            ObjESGC_EmpresaSucursal = new ESGC_EmpresaSucursal()
                            {
                                Sucursal = "TODOS",
                                IdEmpSucursal = 0
                            }
                        });
                        SelectArticuloClase = ListEMNF_ArticuloClase.FirstOrDefault(x => x.IdArtClase == 0);
                        SelectArticuloMarca = ListEMNF_ArticuloMarca.FirstOrDefault(x => x.IdMarca == 0);
                        SelectUsuarioEmpresaSucursal = ListESGC_UsuarioEmpresaSucursal.FirstOrDefault(x => x.ObjESGC_EmpresaSucursal.IdEmpSucursal == 0);
                    }
                });
            });
        }

        private void LoadAlmacen()
        {
            string strOutMessageError = string.Empty;
            CmpTask.Process(
            () =>
            {
                try
                {
                    ListEALM_Almacen = new ObservableCollection<EALM_Almacen>(new BALM_Almacen().ListFiltrarAlmacen(SelectUsuarioEmpresaSucursal.ObjESGC_EmpresaSucursal));
                    
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
                    CmpMessageBox.Show(CMPMensajes.TitleMessage, strOutMessageError, CmpButton.Aceptar);
                }
                else
                {
                    ListEALM_Almacen.Add(new EALM_Almacen() { Almacen = "TODOS", IdAlmacen = 0 });
                    SelectAlmacen = ListEALM_Almacen.FirstOrDefault(x => x.IdAlmacen == 0);
                }
            });
        }

        private void LoadDetail()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                string strOutMessageError = string.Empty;
                CmpTask.ProcessAsync(
                () =>
                {
                    if (ObjECMP_ReporteStockMinimo.ObjESGC_UsuarioEmpresaSucursal.ObjESGC_EmpresaSucursal == null)
                        ObjECMP_ReporteStockMinimo.ObjESGC_UsuarioEmpresaSucursal.ObjESGC_EmpresaSucursal = new ESGC_EmpresaSucursal();
                    ListECMP_ReporteStockMinimo = new ObservableCollection<ECMP_ReporteStockMinimo>(new BCMP_ReporteStockMinimo().ListReporteStockMinimo(ObjECMP_ReporteStockMinimo ?? new ECMP_ReporteStockMinimo()));
                },
                (e) =>
                {
                    if (e != null)
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleMessage, e.Message, CmpButton.Aceptar);
                        return;
                    }
                });
            });
        }

        #endregion
    }
}
