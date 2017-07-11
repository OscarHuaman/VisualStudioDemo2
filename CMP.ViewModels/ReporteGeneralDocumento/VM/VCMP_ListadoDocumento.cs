/*********************************************************
'* VIEWMODEL PARA EL REPORTE GENERAL DE DOCUMENTO
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   JUAN LUIS GUERRA MENESES
'* FCH. CREACIÓN : 25/01/2016
**********************************************************
'* MODIFICADO POR : COMPUTER SYSTEMS SOLUTION
'*				    OSCAR HUAMAN CABRERA
'* FCH. MODIFICACION : 27/09/2016
**********************************************************/
namespace CMP.ViewModels.ReporteGeneralDocumento.VM
{
    using CMP.Business;
    using CMP.Entity;
    using CMP.Reports;
    using CMP.Useful.Modulo;
    using ComputerSystems;
    using ComputerSystems.MethodList;
    using ComputerSystems.WPF.Acciones.Controles.Buttons;
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
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class VCMP_ListadoDocumento : CmpNavigationService, CmpINavigation
    { 
        public ECMP_ReporteGrlDocumento ObjECMP_ReporteGrlDocumento { get; set; }

        public object Parameter
        {
            set
            {
                if (value is ECMP_ReporteGrlDocumento)
                {
                    ObjECMP_ReporteGrlDocumento = new ECMP_ReporteGrlDocumento();
                    ObjECMP_ReporteGrlDocumento.ObjEMNF_ClienteProveedor = new EMNF_ClienteProveedor() { RazonSocial = string.Empty };
                    ObjECMP_ReporteGrlDocumento.ObjESGC_Documento =  new ESGC_Documento() { CodDocumento = "%" } ;
                    ObjECMP_ReporteGrlDocumento.ObjESGC_Moneda = new ESGC_Moneda() { CodMoneda = "%" } ;
                    ObjECMP_ReporteGrlDocumento.Opcion = string.Empty ;
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
                    ObjECMP_ReporteGrlDocumento.ObjEMNF_ClienteProveedor = item;
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

        private ObservableCollection<ECMP_ReporteGrlDocumento> _ListECMP_ReporteGrlDocumento;
        private ObservableCollection<ESGC_Documento> _ListESGC_Documento;
        private ObservableCollection<ESGC_Moneda> _ListESGC_Moneda;
        private ObservableCollection<EMNF_Periodo> _ListEMNF_Periodo;
        private ObservableCollection<ItemReferencia> _ListOpciones;

        public ObservableCollection<ECMP_ReporteGrlDocumento> ListECMP_ReporteGrlDocumento
        {
            get
            {
                if (_ListECMP_ReporteGrlDocumento == null)
                    _ListECMP_ReporteGrlDocumento = new ObservableCollection<ECMP_ReporteGrlDocumento>();
                return _ListECMP_ReporteGrlDocumento;
            }
            set
            {
                _ListECMP_ReporteGrlDocumento = value;
                this.OnPropertyChanged();
            }
        }
        public ObservableCollection<ESGC_Documento> ListESGC_Documento
        {
            get
            {
                if (_ListESGC_Documento == null)
                    _ListESGC_Documento = new ObservableCollection<ESGC_Documento>();
                return _ListESGC_Documento;
            }
            set
            {
                _ListESGC_Documento = value;
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

        //AGREGADO

        private ObservableCollection<string> _ListPeriodoCmpPeriodo;
        public  ObservableCollection<string> ListPeriodoCmpPeriodo
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

        private EMNF_Periodo _SelectPeriodo;
        private ItemReferencia _SelectOpcion;
        private ESGC_Documento _SelectDocumento;
        private ESGC_Moneda _SelectMoneda;

        public EMNF_Periodo SelectPeriodo 
        {
            get 
            {
                return _SelectPeriodo;
            }
            set 
            {
                _SelectPeriodo = value;
                if (value != null)
                {
                    ObjECMP_ReporteGrlDocumento.Periodo = value.Periodo;
                }
                else
                {
                    ObjECMP_ReporteGrlDocumento.Periodo = string.Empty;                  
                }
                LoadDetail();
            }
        }
        public ItemReferencia SelectOpcion
        {
            get
            {
                return _SelectOpcion;
            }
            set
            {
                _SelectOpcion = value;
                ObjECMP_ReporteGrlDocumento.Opcion = value.Value;

                LoadDetail();
                OnPropertyChanged();
            }
        }
        public ESGC_Documento SelectDocumento 
        {
            get 
            { 
                return _SelectDocumento;
            }
            set 
            { 
                _SelectDocumento = value;
                ObjECMP_ReporteGrlDocumento.ObjESGC_Documento = value;

                LoadDetail();
                OnPropertyChanged();
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
                ObjECMP_ReporteGrlDocumento.ObjESGC_Moneda = value;

                LoadDetail();
                OnPropertyChanged();
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
                        //Reiniciamos los valores en blanco
                        ObjECMP_ReporteGrlDocumento.ObjEMNF_ClienteProveedor = new EMNF_ClienteProveedor() { RazonSocial = string.Empty};
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
                    ObjECMP_ReporteGrlDocumento.Periodo = value;
                }
                else
                {
                    ObjECMP_ReporteGrlDocumento.Periodo = string.Empty;
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
                    if (T.ToString().Trim().Length == 0)
                        LoadDetail();

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
                                ObjECMP_ReporteGrlDocumento.ObjEMNF_ClienteProveedor = vrListEMNF_ClienteProveedor.FirstOrDefault();
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

        public ICommand IExportar
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    try
                    {
                        if (ListECMP_ReporteGrlDocumento.Count == 0)
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleMessage, "No hay registros que exportar", CmpButton.Aceptar);
                            return;
                        }
                        var ListExport = ListECMP_ReporteGrlDocumento.Select((x) => new
                        {                       
                            FechaEmision = x.FechaEmision.ToShortDateString(),
                            FechaContable = x.FechaContable.ToShortDateString(),
                            Documento = x.ObjESGC_Documento.Descripcion,
                            SerieNumero = x.SerieNumero,
                            ClienteProveedor = x.ObjEMNF_ClienteProveedor.RazonSocial,
                            NumeroDocIdentidad = x.ObjEMNF_ClienteProveedor.NroDocIdentidad,
                            Moneda = x.ObjESGC_Moneda.Descripcion,
                            TipoCambio = x.TipoCambio,
                            FechaRecepcion = x.FechaEmision.ToShortDateString(),                         
                            Exonerada = x.Exonerada,
                            Grabada = x.Gravada,
                            PorcentajeIGV = x.PIGV,
                            IGV = x.IGV,
                            Total = x.Total,
                            PorcentajePercepcion = x.PPercepcion,
                            Percepcion = x.Percepcion,
                            OtrosCargos = x.OCargos,                           
                            ImporteTotal = x.ImpTotal,
                            PorcentajeDetraccion=x.Detraccion,
                            Detraccion = x.PDetraccion,
                            Glosa= x.Glosa

                        }).ToList();
                        ListExport.Export("ListReporteGeneralDocumento", ExportType.Excel, (value) =>
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

        public ICommand IImprimir
        {
            get
            {
                return CmpICommand.GetICommand((T) => 
                {
                    try
                    {
                        if (ListECMP_ReporteGrlDocumento.Count == 0)
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleMessage, "No existen registros por imprimir", CmpButton.Aceptar);
                            return;
                        }
                        string[] parametro;
                        parametro = new string[]
                        {
                            /*"prmPeriodo|"   + "AL MES DE " + SelectPeriodo.NombreMes + " DEL " + SelectPeriodo.Anio,*/
                            "prmPeriodo|"   + "PERIODO: " + SelectPeriodoCmpPeriodo,
                            "prmOpcion|"    + ((SelectOpcion != null) ? SelectOpcion.Item : string.Empty),
                            "prmProveedor|" + ((ObjECMP_ReporteGrlDocumento.ObjEMNF_ClienteProveedor.IdCliProveedor == 0) ? "TODOS" : ObjECMP_ReporteGrlDocumento.ObjEMNF_ClienteProveedor.RazonSocial),
                            "prmMoneda|"    + ((ObjECMP_ReporteGrlDocumento.ObjESGC_Moneda.CodMoneda == "%") ? "TODOS"  :ObjECMP_ReporteGrlDocumento.ObjESGC_Moneda.Descripcion),
                            "prmDocumento|" + ((ObjECMP_ReporteGrlDocumento.ObjESGC_Documento.CodDocumento  =="%") ? "TODOS" : ObjECMP_ReporteGrlDocumento.ObjESGC_Documento.Descripcion )
                        };

                        MainRerport ObjMainRerport = new MainRerport();
                        ObjMainRerport.InitializeMainRerport("CMP.Reports.Files.RptGeneralDocumentos.rdlc", "DtsReporteGrlDocumento", ListECMP_ReporteGrlDocumento, parametro);
                        ObjMainRerport.ShowDialog();
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
            CmpTask.ProcessAsync(
            () =>
            {
               
                    ListOpciones = GetOpciones();
                    ListEMNF_Periodo = new ObservableCollection<EMNF_Periodo>(new BMNF_Periodo().ListPeriodo());
                    ListESGC_Moneda = new ObservableCollection<ESGC_Moneda>(new BSGC_Moneda().ListGetMoneda());
                    ListESGC_Documento = GetDocumento();
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
                    ListESGC_Moneda.Add(new ESGC_Moneda() { CodMoneda = "%", Descripcion = "TODOS" });
                    ListESGC_Documento.Add(new ESGC_Documento() { CodDocumento = "%", Descripcion = "TODOS" });
                    ListEMNF_Periodo.ToList().ForEach(x => ListPeriodoCmpPeriodo.Add(x.Periodo));
                    SelectOpcion = ListOpciones.LastOrDefault();
                    SelectPeriodo = ListEMNF_Periodo.LastOrDefault(x => x.Periodo == strPeriodoActivo);
                    SelectMoneda = ListESGC_Moneda.LastOrDefault();
                    SelectDocumento = ListESGC_Documento.LastOrDefault();
                    SelectPeriodoCmpPeriodo = strPeriodoActivo;
                }
            });
        }

        private void LoadDetail()
        {
            string strOutMessageError = string.Empty;
            CmpTask.ProcessAsync(
            () =>
            {
                if (ObjECMP_ReporteGrlDocumento.ObjEMNF_ClienteProveedor.RazonSocial.Trim().Length == 0)
                    ObjECMP_ReporteGrlDocumento.ObjEMNF_ClienteProveedor.IdCliProveedor = 0;
                ListECMP_ReporteGrlDocumento = new ObservableCollection<ECMP_ReporteGrlDocumento>(new BCMP_ReporteGrlDocumento().ListReporteGrlDocumento(ObjECMP_ReporteGrlDocumento));
            },
            (e) =>
            {
                if (e != null)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleMessage, e.Message, CmpButton.Aceptar);
                    return;
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

        private ObservableCollection<ESGC_Documento> GetDocumento() 
        {
          ObservableCollection<ESGC_Documento> ListDocumento = new ObservableCollection<ESGC_Documento>();
          ListDocumento = new ObservableCollection<ESGC_Documento>() 
            {
                new ESGC_Documento(){ CodDocumento = "FAC", Descripcion = "FACTURA" },
                new ESGC_Documento(){ CodDocumento = "BOL", Descripcion = "BOLETA" },
                new ESGC_Documento(){ CodDocumento = "RCB", Descripcion = "RECIBO" },
                new ESGC_Documento(){ CodDocumento = "NCR", Descripcion = "NOTA CREDITO" },
                new ESGC_Documento(){ CodDocumento = "NDB", Descripcion = "NOTA DEBITO" },
                        
            };
          return ListDocumento;
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
