/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 23/02/2016
'*********************************************************
'* MODIFICADO POR : COMPUTER SYSTEMS SOLUTION
'*                  OSCAR HUAMAN CABRERA
'* FCH. MODIFICACIÓN : 26/09/2016
**********************************************************/
namespace CMP.ViewModels.CuentasPorPagar.VM
{
    using CMP.Business;
    using CMP.Entity;
    using CMP.Reports;
    using CMP.Useful.Modulo;
    using CJB.ViewModels.CajaBanco.VM;
    using CJB.ViewModels.CajaBanco.Flyouts;
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using ComputerSystems.WPF.Acciones.Controles.Buttons;
    using MahApps.Metro.Controls;
    using MNF.Business;
    using MNF.Entity;
    using MNF.Presentation.ClienteProveedor.Flyouts;
    using SGC.Empresarial.Business;
    using SGC.Empresarial.Entity;
    using SGC.Empresarial.Method;
    using SGC.Empresarial.Useful.Modulo;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Input;
    using ComputerSystems.Loading;
    using System.Windows;

    public class VCMP_CuentasPorPagar : CmpNavigationService, CmpINavigation
    {
        private CmpLoading CmpLoading { get; set; }

        public object Parameter
        {
            set
            {
                CmpLoading = new CmpLoading(null, LoadDetail);
                CmpLoading.Exceptions = ((e) => { CmpMessageBox.Show(CMPMensajes.TitleAdminCuentaPorPagar, e.Message, CmpButton.Aceptar); });
                FechaInicio = DateTime.Now;
                FechaFin = DateTime.Now;
                CmpLoading.LoadHeader();
            }
        }

        private Frame _MyFrame;
        public Frame MyFrame
        {
            set
            {
                AddFlyout();
                _MyFrame = value;
                if (value != null)
                {
                    value.KeyDownCmpButtonTitleTecla(
                          ActionF9: () => { if (IImprimir.CanExecute(true)) IImprimir.Execute(string.Empty); }
                        , ActionESC: () => { if (ISalir.CanExecute(true)) ISalir.Execute(string.Empty); },
                        ActionShiftV: () => { if (IVerDetalle.CanExecute(true)) IVerDetalle.Execute(string.Empty); }
                        );
                    CmpLoading.LoadDetail();
                }
            }
        }

        #region COLECCIONES

        private ObservableCollection<ECMP_CuentasPorPagar> _ListECMP_CuentasPorPagar;

        public ObservableCollection<ECMP_CuentasPorPagar> ListECMP_CuentasPorPagar
        {
            get
            {
                if (_ListECMP_CuentasPorPagar == null)
                    _ListECMP_CuentasPorPagar = new ObservableCollection<ECMP_CuentasPorPagar>();
                return _ListECMP_CuentasPorPagar;
            }
            set
            {
                _ListECMP_CuentasPorPagar = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region OBJETO SECUNDARIO

        private DateTime _FechaInicio;
        private DateTime _FechaFin;
        private EMNF_ClienteProveedor _ObjEMNF_ClienteProveedor;
        private bool _IsCheckedPorFecha;

        
        public DateTime FechaInicio
        {
            get
            {
                if (_FechaInicio == null)
                    _FechaInicio = DateTime.Now;
                return _FechaInicio;
            }
            set
            {
                _FechaInicio = value;
                CmpLoading.LoadDetail();
                OnPropertyChanged();
              
            }
        }
        public DateTime FechaFin
        {
            get
            {
                if (_FechaFin == null)
                    _FechaFin = DateTime.Now;
                return _FechaFin;
            }
            set
            {
                _FechaFin = value;
                OnPropertyChanged();
                CmpLoading.LoadDetail();
            }
        }
        public EMNF_ClienteProveedor ObjEMNF_ClienteProveedor
        {
            get
            {
                if (_ObjEMNF_ClienteProveedor == null)
                    _ObjEMNF_ClienteProveedor = new EMNF_ClienteProveedor();

                if (_ObjEMNF_ClienteProveedor.RazonSocial != null)
                {
                    if (_ObjEMNF_ClienteProveedor.RazonSocial.Trim().Length == 0)
                    {
                        _ObjEMNF_ClienteProveedor.RazonSocial = null;
                        _ObjEMNF_ClienteProveedor.IdCliProveedor = 0;
                        LoadDetail();
                    }
                }
                return _ObjEMNF_ClienteProveedor;
            }
            set
            {
                _ObjEMNF_ClienteProveedor = value;
                CmpLoading.LoadDetail();
                OnPropertyChanged("RazonSocial");
            }
        }//MODIFICADO 20160926 OSCAR HUAMAN CABRERA
        public bool IsCheckedPorFecha 
        {
            get 
            {
                return _IsCheckedPorFecha;
            }
            set 
            {
                _IsCheckedPorFecha = value;
                CmpLoading.LoadDetail();
                OnPropertyChanged();
            }
        }

        #region AGREGADO 20160926 OSCAR HUAMAN CABRERA

        private ECMP_CuentasPorPagar _SelectedItem;
        public ECMP_CuentasPorPagar SelectedItem
        {
            get
            {
                return _SelectedItem;
            }
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #endregion

        #region ICOMMAND

        public ICommand IBuscarProveedor
        {
            get
            {
                return CmpICommand.GetICommand(new Action<object>((T) =>
                {
                    MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminCuentaPorPagar, CMPMensajes.GetAccesoRestringidoNull("Proveedor"), new Action<ESGC_PermisoPerfil>((P) =>
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
                                        ObjEMNF_ClienteProveedor = vrListEMNF_ClienteProveedor.FirstOrDefault();
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
                                }
                            });
                        }
                        else
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleAdminNotaCreditoDebito, CMPMensajes.GetAccesoRestringidoBuscar("Proveedor"), CmpButton.Aceptar);
                        }
                    }));
                }));
            }
        }

        public ICommand IImprimir
        {
            get
            {
                return CmpICommand.GetICommand(new Action<object>((T) =>
                {
                    try
                    {
                        var CollectionTemp = new ObservableCollection<ECMP_CuentasPorPagar>();
                        if (ListECMP_CuentasPorPagar.Count > 0)
                        {
                            var IdProveedor = ListECMP_CuentasPorPagar.Select(x => x.IdCliProveedor).Distinct();
                            IdProveedor.ToList().ForEach(x => CollectionTemp.Add(ListECMP_CuentasPorPagar.LastOrDefault(i => i.IdCliProveedor == x)));
                        }
                        string[] parametro;
                        if (ListECMP_CuentasPorPagar.Count > 0)
                        {
                            parametro = new string[]
                            {
                                "prmRazonSocial|"       + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.RazonSocial,
                                "prmRuc|"               + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.Ruc,
                                "prmDireccionEmpresa|"  + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.DireccionFiscal,
                                "prmTelefonoEmpresa|"   + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.Telefono,
                                "prmProveedor|"         + ((ObjEMNF_ClienteProveedor.IdCliProveedor != 0 ) ? ObjEMNF_ClienteProveedor.RazonSocial: "TODOS"),
                                "prmMoneda|"            +  ListECMP_CuentasPorPagar.FirstOrDefault().ObjESGC_Moneda.Descripcion,
                                "prmTotalUSD|"          + "$" + CollectionTemp.Sum(x=>  (x.Saldo_USD)),
                                "prmTotalSOL|"          + "S/." +CollectionTemp.Sum(x=>  (x.Saldo_SOL)),
                            };

                            MainRerport ObjMainRerport = new MainRerport();
                            ObjMainRerport.InitializeMainRerport("CMP.Reports.Files.RptCuentasPagar.rdlc", "DtsCuentaPorPagar", ListECMP_CuentasPorPagar, parametro);
                            ObjMainRerport.ShowDialog();
                        }
                    }
                    catch (Exception ex)
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, ex.Message, CmpButton.Aceptar);
                    }
                }));
            }
        }

        public ICommand IResumen
        {
            get
            {
                return CmpICommand.GetICommand(new Action<object>((T) =>
                {
                    try
                    {
                        string[] parametro;
                        if (ListECMP_CuentasPorPagar.Count > 0)
                        {
                            parametro = new string[]
                            {
                                "prmRazonSocial|"       + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.RazonSocial,
                                "prmRuc|"               + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.Ruc,
                                "prmDireccionEmpresa|"  + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.DireccionFiscal,
                                "prmTelefonoEmpresa|"   + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.Telefono,
                                "prmProveedor|"         + ((ObjEMNF_ClienteProveedor.IdCliProveedor != 0 ) ? ObjEMNF_ClienteProveedor.RazonSocial: "TODOS"),
                                "prmMoneda|"            + ListECMP_CuentasPorPagar.FirstOrDefault().ObjESGC_Moneda.Descripcion
                            };
                            var ListTempECMP_CuentasPorPagar = new ObservableCollection<ECMP_CuentasPorPagar>();
                            var vrListTemp = ListECMP_CuentasPorPagar.Select(x => x.IdCliProveedor).Distinct().ToArray();
                            vrListTemp.ToList().ForEach(o=> ListTempECMP_CuentasPorPagar.Add(ListECMP_CuentasPorPagar.LastOrDefault(x => x.IdCliProveedor == o)));
                            MainRerport ObjMainRerport = new MainRerport();
                            ObjMainRerport.InitializeMainRerport("CMP.Reports.Files.RptResumenCuentasPagar.rdlc", "DtsCuentaPorPagar", ListTempECMP_CuentasPorPagar, parametro);
                            ObjMainRerport.ShowDialog();
                        }
                    }
                    catch (Exception ex)
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, ex.Message, CmpButton.Aceptar);
                    }
                }));
            }
        }

        public ICommand ISalir
        {
            get
            {
                return CmpICommand.GetICommand(new Action<object>((I) =>
                {
                    _MyFrame.Close(TipoModulo.ManuFactura);
                }));
            }
        }

        public ICommand IVerDetalle
        {
            get
            {
                return CmpICommand.GetICommand(new Action<object>((T) =>
                {
                    try
                    {
                        if (SelectedItem != null)
                        {
                            if ((SelectedItem.ObjESGC_Documento.CodDocumento == "FAC" && SelectedItem.Haber == 0) || SelectedItem.ObjESGC_Documento.CodDocumento == "NCR" || SelectedItem.ObjESGC_Documento.CodDocumento == "NDB")
                            {
                                CmpMessageBox.Show(CMPMensajes.TitleAdminCuentaPorPagar, "El documento seleccionado, no cuenta con pagos registrados en el módulo de Caja y Bancos.", CmpButton.Aceptar);
                                return;
                            }

                            _MyFrame.FlyoutIsOpen("FCJB_MovimientoDetalle", new Action<Flyout>((value) =>
                            {
                                if (value is FCJB_MovimientoDetalle)
                                {
                                    var ValueFlyout = ((FCJB_MovimientoDetalle)value);
                                    var ValueDataContext = ((VCJB_MovimientoDetalle)ValueFlyout.DataContext);
                                    ValueDataContext.PropertySetKeyDownCmpButtonTitleTecla = ValueFlyout;
                                    ValueDataContext.PropertyTipoMovimientoDetalle = CJB.Useful.Enums.TipoMovimientoDetalle.Compra;
                                    ValueDataContext.ChildObjectEMNF_ClienteProveedor = new EMNF_ClienteProveedor() { IdCliProveedor = SelectedItem.IdCliProveedor, RazonSocial = SelectedItem.Proveedor, NroDocIdentidad = SelectedItem.NroDocIdentidad };
                                    ValueDataContext.ChildObjectESGC_Moneda = new ESGC_Moneda() { Descripcion = SelectedItem.ObjESGC_Moneda.Descripcion, CodMoneda = SelectedItem.ObjESGC_Moneda.CodMoneda };
                                    ValueDataContext.ChildObjectSerie = SelectedItem.SerieDocumento;
                                    ValueDataContext.ChildObjectNumero = SelectedItem.Numero;
                                    ValueDataContext.ChildObjectESGC_Documento = new ESGC_Documento() { Descripcion = SelectedItem.ObjESGC_Documento.Descripcion, CodDocumento = SelectedItem.ObjESGC_Documento.CodDocumento };
                                    ValueDataContext.ChildObjectFechaEmision = SelectedItem.Fecha;
                                    ValueDataContext.PropertyIsOpenFlyout = true;
                                }
                            }));
                        }
                        else
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleAdminCuentaPorPagar, CMPMensajes.IsNullSelectItem, CmpButton.Aceptar);
                        }
                    }
                    catch (Exception ex)
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminCuentaPorPagar, ex.Message, CmpButton.Aceptar);
                    }
                }));
            }
        }

        #endregion

        #region MÉTODOS

        private void AddFlyout()
        {
            #region LISTADO CLIENTE PROVEEDOR

            PMNF_BuscarClienteProveedor MyPMNF_BuscarClienteProveedor = new PMNF_BuscarClienteProveedor();
            MyPMNF_BuscarClienteProveedor.IsSelected += new PMNF_BuscarClienteProveedor.isSelected((item) =>
            {
                ObjEMNF_ClienteProveedor = item;
            });

            FCJB_MovimientoDetalle FCJB_MovimientoDetalle = new CJB.ViewModels.CajaBanco.Flyouts.FCJB_MovimientoDetalle();

            #endregion

            _MyFrame.FlyoutInitialize();
            _MyFrame.FlyoutAdd(MyPMNF_BuscarClienteProveedor);
            _MyFrame.FlyoutAdd(FCJB_MovimientoDetalle);
        }

        public void LoadDetail()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                CmpTask.ProcessAsync(
                () =>
                {
                    ListECMP_CuentasPorPagar = new BCMP_CuentasPorPagar().ListCuentasPorPagar(ObjEMNF_ClienteProveedor.IdCliProveedor, ((IsCheckedPorFecha) ? 1 : 0), FechaInicio, FechaFin);
                },
                (e) =>
                {
                    if (e != null)
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminCuentaPorPagar, e.Message, CmpButton.Aceptar);
                        return;
                    }
                });
            });
        }

        #endregion
    }
}
