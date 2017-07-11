/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   JUAN LUIS GUERRA MENESES
'* FCH. CREACIÓN : 09/02/2016
 * *******************************************************
 * MODIFICADO POR : CRISTIAN HERNANDEZ VILLO
 * MOTIVO         : IMPLEMENTACION DE CONTROL PERIODO
**********************************************************/
namespace CMP.ViewModels.CuentasPorPagar.VM
{
    using CMP.Business;
    using CMP.Entity;
    using CMP.Reports;
    using CMP.Useful.Modulo;
    using ComputerSystems;
    using ComputerSystems.MethodList;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.Buttons;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using MahApps.Metro.Controls;
    using MNF.Business;
    using MNF.Entity;
    using MNF.Presentation.ClienteProveedor.Flyouts;
    using SGC.Empresarial.Business;
    using SGC.Empresarial.Entity;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class VCMP_ListadoReciboGastosInternos : CmpNavigationService, CmpINavigation
    {
        public ECMP_ReporteGastosInternos ObjECMP_ReporteGastosInternos { get; set; }

        public object Parameter
        {
            set
            {
                if (value is ECMP_ReporteGastosInternos)
                {
                   ObjECMP_ReporteGastosInternos = new ECMP_ReporteGastosInternos();
                   LoadHeader();
                }
            }
        }
        private Frame _MyFrame;
        public Frame MyFrame
        {
            set
            {
                #region Flyout Cliente Proveedor
                PMNF_BuscarClienteProveedor ObjPMNF_BuscarClienteProveedor = new PMNF_BuscarClienteProveedor();
                ObjPMNF_BuscarClienteProveedor.IsSelected += new PMNF_BuscarClienteProveedor.isSelected((item) =>                   
                {
                    ObjECMP_ReporteGastosInternos.ObjEMNF_ClienteProveedor = item;
                    RazonSocial = item.RazonSocial;
                    LoadDetail();
                });

                #endregion

                value.FlyoutInitialize();
                value.FlyoutAdd(ObjPMNF_BuscarClienteProveedor);
                _MyFrame = value;
                if (value != null)
                {
                    value.KeyDownCmpButtonTitleTecla(
                        ActionF9:     () => { if (IImprimir.CanExecute(true)) IImprimir.Execute(string.Empty); },
                        ActionShiftE: () => { if (IExportar.CanExecute(true))IExportar.Execute(string.Empty); },
                        ActionESC:    () => { if (IClose.CanExecute(true))IClose.Execute(string.Empty); });

                }
            }
        }

        #region COLECCION DE DATOS

        private ObservableCollection<ECMP_ReporteGastosInternos> _ListECMP_ReporteGastosInternos;

        private ObservableCollection<ESGC_Moneda> _ListESGC_Moneda;
        private ObservableCollection<EMNF_Periodo> _ListEMNF_Periodo;
        private ObservableCollection<ItemReferencia> _ListOpciones;

        public ObservableCollection<ECMP_ReporteGastosInternos> ListECMP_ReporteGastosInternos
        {
            get
            {
                if (_ListECMP_ReporteGastosInternos == null)
                    _ListECMP_ReporteGastosInternos = new ObservableCollection<ECMP_ReporteGastosInternos>();
                return _ListECMP_ReporteGastosInternos;
            }
            set
            {
                _ListECMP_ReporteGastosInternos = value;
                this.OnPropertyChanged();
            }
        }

        public ObservableCollection<ESGC_Moneda> ListESGC_Moneda
        {
            get
            {
                if (_ListESGC_Moneda == null)
                {
                    _ListESGC_Moneda = new ObservableCollection<ESGC_Moneda>();
                }
                return _ListESGC_Moneda;
            }
            set
            {
                _ListESGC_Moneda = value;
                this.OnPropertyChanged();
            }
        }
        public ObservableCollection<EMNF_Periodo> ListEMNF_Periodo
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
        public ObservableCollection<ItemReferencia> ListOpciones
        {
            get
            {
                if (_ListOpciones == null)
                {
                    _ListOpciones = new ObservableCollection<ItemReferencia>();
                }
                return _ListOpciones;
            }
            set
            {
                _ListOpciones = value;
                this.OnPropertyChanged();
            }
        }

        //AGREGADO

        private ObservableCollection<string> _ListPeriodoCmpPeriodo;
        public ObservableCollection<string> ListPeriodoCmpPeriodo
        {
            get
            {
                if (_ListPeriodoCmpPeriodo == null)
                    _ListPeriodoCmpPeriodo = new ObservableCollection<string>();
                return _ListPeriodoCmpPeriodo;
            }
            set
            {
                _ListPeriodoCmpPeriodo = value;
                OnPropertyChanged("ListPeriodoCmpPeriodo");
            }
        }

        #endregion

        #region  OBJ SECUNDARIOS

        private ItemReferencia _SelectOpcion;
        private ESGC_Moneda _SelectMoneda;

        public ItemReferencia SelectOpcion
        {
            get
            {
                return _SelectOpcion;
            }
            set
            {
                _SelectOpcion = value;
                ObjECMP_ReporteGastosInternos.Opcion = value.Value;
                if (ObjECMP_ReporteGastosInternos.ObjEMNF_ClienteProveedor != null)
                {
                    if (ObjECMP_ReporteGastosInternos.ObjEMNF_ClienteProveedor.RazonSocial == string.Empty)
                    {
                        ObjECMP_ReporteGastosInternos.ObjEMNF_ClienteProveedor = new EMNF_ClienteProveedor();
                    }
                }
                LoadDetail();
                this.OnPropertyChanged();
            }
        }
        public ESGC_Moneda SelectMoneda
        {
            get
            {
                return _SelectMoneda;
            }
            set
            {
                _SelectMoneda = value;
                ObjECMP_ReporteGastosInternos.ObjESGC_Moneda = value;
                if (ObjECMP_ReporteGastosInternos.ObjEMNF_ClienteProveedor != null)
                {
                    if (ObjECMP_ReporteGastosInternos.ObjEMNF_ClienteProveedor.RazonSocial == string.Empty)
                    {
                        ObjECMP_ReporteGastosInternos.ObjEMNF_ClienteProveedor = new EMNF_ClienteProveedor();
                    }
                }
                LoadDetail();
                this.OnPropertyChanged();
            }
        }

        private string _RazonSocial;
        public string RazonSocial
        {
            get
            {
                return _RazonSocial;
            }
            set
            {
                _RazonSocial = value;
                if (value != null)
                {
                    if (value.Trim().Length == 0)  
                    {
                        /*Reiniciamos los valores en blanco*/
                        ObjECMP_ReporteGastosInternos.ObjEMNF_ClienteProveedor = new EMNF_ClienteProveedor() { RazonSocial = string.Empty };
                    }
                    LoadDetail();
                }
                OnPropertyChanged("RazonSocial");
            }
        }


        //AGREGADO

        private string _SelectPeriodoCmpPeriodo;
        public string SelectPeriodoCmpPeriodo
        {
            get
            {
                return _SelectPeriodoCmpPeriodo;
            }
            set
            {
                _SelectPeriodoCmpPeriodo = value;
                if (value != null)
                {
                    ObjECMP_ReporteGastosInternos.Periodo = value;
                }
                else
                {
                    ObjECMP_ReporteGastosInternos.Periodo = string.Empty;
                }
                LoadDetail();
                OnPropertyChanged("SelectPeriodoCmpPeriodo");
            }

        }
        #endregion

        #region ICOMMAND

        public ICommand IBuscarProveedor
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    var vrListEMNF_ClienteProveedor = new ObservableCollection<EMNF_ClienteProveedor>();
                    string strOutMessageError = string.Empty;
                    CmpTask.Process(
                    () =>
                    {
                        try
                        {
                            vrListEMNF_ClienteProveedor = new ObservableCollection<EMNF_ClienteProveedor>(new BMNF_ClienteProveedor().ListFiltrarClienteProveedor(T.ToString()));
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

                            if (vrListEMNF_ClienteProveedor.Count == 1)
                            {
                                ObjECMP_ReporteGastosInternos.ObjEMNF_ClienteProveedor = vrListEMNF_ClienteProveedor.FirstOrDefault();
                                RazonSocial = vrListEMNF_ClienteProveedor.FirstOrDefault().RazonSocial;
                            }
                            else
                            {
                                _MyFrame.FlyoutIsOpen("PMNF_BuscarClienteProveedor", new Action<Flyout>((value) =>
                                {
                                    if (value is PMNF_BuscarClienteProveedor)
                                    {
                                        var MyFlyout = (PMNF_BuscarClienteProveedor)value;
                                        MyFlyout.InitializePMNF_BuscarClienteProveedor();
                                        MyFlyout.LoadDatil("%");
                                        MyFlyout.IsOpen = true;
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
                        if (ListECMP_ReporteGastosInternos.Count == 0)
                        {
                             CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, "No hay registros para poder imprimir", CmpButton.Aceptar);
                             return;
                        }
                        string[] parametro;
                        parametro = new string[]
                        {
                            //"prmPeriodo|"   + "AL MES DE " + SelectPeriodo.NombreMes + " DEL " + SelectPeriodo.Anio,
                            "prmPeriodo|"   + "PERIODO: " + SelectPeriodoCmpPeriodo,
                            "prmOpcion|"    + SelectOpcion.Item,
                            "prmProveedor|" + ((ObjECMP_ReporteGastosInternos.ObjEMNF_ClienteProveedor.IdCliProveedor == 0 ||ObjECMP_ReporteGastosInternos.ObjEMNF_ClienteProveedor.RazonSocial == null ) ? "TODOS" : ObjECMP_ReporteGastosInternos.ObjEMNF_ClienteProveedor.RazonSocial),
                            "prmMoneda|"    + ((ObjECMP_ReporteGastosInternos.ObjESGC_Moneda.CodMoneda == "%") ? "TODOS"  :ObjECMP_ReporteGastosInternos.ObjESGC_Moneda.Descripcion),
                            
                        };

                        MainRerport ObjMainRerport = new MainRerport();
                        ObjMainRerport.InitializeMainRerport("CMP.Reports.Files.RptGastosInternos.rdlc", "DtsReporteGastosInternos", ListECMP_ReporteGastosInternos, parametro);
                        ObjMainRerport.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleMessage, ex.Message, CmpButton.Aceptar);
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
                        if (ListECMP_ReporteGastosInternos.Count == 0)
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleMessage, "No hay registros que exportar", CmpButton.Aceptar);
                            return;
                        }
                        var ListExport = ListECMP_ReporteGastosInternos.Select((x) => new
                        {
                            FechaEmision = x.FechaEmision.ToShortDateString(),
                            FechaContable = x.FechaContable.ToShortDateString(),
                            Documento = x.ObjESGC_Documento.Descripcion,
                            SerieNumero = x.SerieNumero,
                            ClienteProveedor = x.ObjEMNF_ClienteProveedor.RazonSocial,
                            NumeroDocIdentidad = x.ObjEMNF_ClienteProveedor.NroDocIdentidad,
                            Moneda = x.ObjESGC_Moneda.Descripcion,
                            FechaRecepcion = x.FechaEmision.ToShortDateString(),
                            Total = x.Total,
                            Glosa = x.Glosa

                        }).ToList();
                        ListExport.Export("ListGatosInternos", ExportType.Excel, (value) =>
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, value, CmpButton.Aceptar);
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
            string strOutMessageError = string.Empty;
            string strPeriodoActivo = new BMNF_Periodo().GetPeriodoActual();
            CmpTask.Process(
            () =>
            {
                try
                {                
                    ListOpciones = GetOpciones();
                    ListEMNF_Periodo = new ObservableCollection<EMNF_Periodo>(new BMNF_Periodo().ListPeriodo());
                    ListESGC_Moneda = new ObservableCollection<ESGC_Moneda>(new BSGC_Moneda().ListGetMoneda());
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
                    ListESGC_Moneda.Add(new ESGC_Moneda() { CodMoneda = "%", Descripcion = "TODOS" });
                    ListEMNF_Periodo.ToList().ForEach(x => ListPeriodoCmpPeriodo.Add(x.Periodo));
                    SelectOpcion = ListOpciones.LastOrDefault();
                    SelectMoneda = ListESGC_Moneda.LastOrDefault();
                    SelectPeriodoCmpPeriodo = strPeriodoActivo;
                }
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
                    ObjECMP_ReporteGastosInternos.ObjEMNF_ClienteProveedor = (ObjECMP_ReporteGastosInternos.ObjEMNF_ClienteProveedor == null) ? new EMNF_ClienteProveedor() : ObjECMP_ReporteGastosInternos.ObjEMNF_ClienteProveedor;
                    ObjECMP_ReporteGastosInternos.ObjESGC_Moneda = (ObjECMP_ReporteGastosInternos.ObjESGC_Moneda == null) ? new ESGC_Moneda() { CodMoneda = "%" } : ObjECMP_ReporteGastosInternos.ObjESGC_Moneda;
                    ObjECMP_ReporteGastosInternos.Opcion = (ObjECMP_ReporteGastosInternos.Opcion == null) ? string.Empty : ObjECMP_ReporteGastosInternos.Opcion;
                    ListECMP_ReporteGastosInternos = new ObservableCollection<ECMP_ReporteGastosInternos>(new BCMP_ReporteGastosInternos().ListReporteReciboGastosInternos(ObjECMP_ReporteGastosInternos));
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
            });
        }

        private ObservableCollection<ItemReferencia> GetOpciones()
        {
            ObservableCollection<ItemReferencia> ListOpciones = new ObservableCollection<ItemReferencia>();

            ListOpciones.Add(new ItemReferencia() { Item = "FECHA EMISIÓN", Value = "Fecha Emisión" });
            ListOpciones.Add(new ItemReferencia() { Item = "FECHA RECEPCIÓN", Value = "Fecha Recepción" });
            ListOpciones.Add(new ItemReferencia() { Item = "FECHA CONTABLE", Value = "Fecha Contable" });

            return ListOpciones;
        }

        #endregion
    }

    #region CLASE AUXILIAR
    /// <summary>
    /// Temporal de Meses
    /// </summary>
    public class ItemReferencia
    {
        public string Item { get; set; }
        public string Value { get; set; }

    }
    #endregion
}
