namespace CMP.Presentation.OrdenCompra
{
    using ALM.Business;
    using ALM.Entity;
    using ALM.Presentation.Almacen;
    using CMP.Business;
    using CMP.Entity;
    using CMP.Presentation.Method;
    using CMP.Reports;
    using CMP.Useful.Metodo;
    using CMP.Useful.Modulo;
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.Buttons;
    using ComputerSystems.WPF.Acciones.Controles.DataGrids;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using MNF.Business;
    using MNF.Entity;
    using MNF.Presentation.Articulo.Flyouts;
    using MNF.Presentation.ClienteProveedor.Flyouts;
    using SGC.Empresarial.Business;
    using SGC.Empresarial.Entity;
    using SGC.Empresarial.Method;
    using SGC.Empresarial.Presentation.MasterTable.Empresa.Flyouts;
    using SGC.Empresarial.Useful;
    using SGC.Empresarial.Useful.Modulo;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public partial class PCMP_OrdenCompra : UserControl, CmpINavigation
    {

        #region INSTANCIAS, VARIABLES, EVENTOS AGREGADOS
        private NumLetra NumLetras = new NumLetra();
        private ECMP_OrdenCompra ObjECMP_OrdenCompra;

        private ESGC_UsuarioEmpresaSucursal ObjESGC_UsuarioEmpresaSucursal;
        private EMNF_ClienteProveedor ObjEMNF_ClienteProveedor;
        private EALM_Almacen ObjEALM_Almacen;
        private List<ESGC_Estado> ListESGC_EstadoDetail;

        private ESGC_Moneda ObjESGC_Moneda;
        private ESGC_Estado ObjESGC_Estado;
        private ESGC_FormaPago objESGC_FormaPago;
        
        private bool imprimir { get; set; }
        private decimal dmlIGV = 0;

        private List<EMNF_ClienteProveedor> ListEMNF_ClienteProveedor;

        private bool Load = false;
        #endregion

        #region EVENTOS DE LA CLASE

        public PCMP_OrdenCompra()
        {
            DataContext = this;
            InitializeComponent();
        }

        private Frame _MyFrame;
        public Frame MyFrame
        {
            set { _MyFrame = value; }
        }

        public object Parameter
        {
            set
            {
                if (!(value is ECMP_OrdenCompra)) return;

                var ObjECMP_OrdenCompra = (ECMP_OrdenCompra)value;

                Load = true;
                ListEMNF_ClienteProveedor = new List<EMNF_ClienteProveedor>();
                this.KeyDownCmpButtonTitleTecla(
                                                    ActionF10: btnAgregarArticuloIsClicked,
                                                    ActionF11: btnQuitarArticuloIsClicked,
                                                    ActionF3: btnCancelarProcesoIsClicked,
                                                    ActionF12: btnGuardarIsClicked,
                                                    ActionF9: btnImprimirIsClicked,
                                                    ActionESC: btnSalirIsClicked);
                AddFlyout();
                ClearControl();

                this.ObjECMP_OrdenCompra = ObjECMP_OrdenCompra;
                this.ObjEMNF_ClienteProveedor = ObjECMP_OrdenCompra.ObjEMNF_ClienteProveedor;
                dtpFechaEntrega.SelectedDate = ObjECMP_OrdenCompra.FechaEntrega;
                LoadHeader(() =>
                {
                    LoadDetail();
                });

                MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminCompra, CMPMensajes.GetAccesoRestringidoNull("Orden Compra"), new Action<ESGC_PermisoPerfil>((P) =>
                {
                    if (P == null) { return; }
                    btnGuardar.IsEnabled = (P.Nuevo || P.Editar);
                }));
            }
        }

        private void txtProveedorRazonSocial_KeyDown_1(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.B))
            {
                string strFiltro = txtProveedorRazonSocial.Text;

                string strOutMessageError = string.Empty;
                CmpTask.Process(
                () =>
                {
                    try
                    {
                        ListEMNF_ClienteProveedor = new BMNF_ClienteProveedor().ListFiltrarClienteProveedor(strFiltro);
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
                        CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, strOutMessageError, CmpButton.Aceptar);
                    }
                    else
                    {
                        if (ListEMNF_ClienteProveedor.Count == 1)
                        {
                            ObjEMNF_ClienteProveedor = ListEMNF_ClienteProveedor.FirstOrDefault();
                            txtProveedorRazonSocial.Text = ObjEMNF_ClienteProveedor.RazonSocial;
                            cbxFormaPago.SelectedValue = ObjEMNF_ClienteProveedor.ObjEMNF_FormaPago.IdFormaPago;
                        }
                        else
                        {
                            this.FlyoutIsOpen("PMNF_BuscarClienteProveedor", ((value) =>
                            {
                                if (value is PMNF_BuscarClienteProveedor)
                                {
                                    var MyPMNF_BuscarClienteProveedor = (PMNF_BuscarClienteProveedor)value;
                                    MyPMNF_BuscarClienteProveedor.InitializePMNF_BuscarClienteProveedor();
                                    MyPMNF_BuscarClienteProveedor.SetValueFilter = strFiltro;
                                    MyPMNF_BuscarClienteProveedor.SetListEMNF_ClienteProveedor = ListEMNF_ClienteProveedor;
                                    MyPMNF_BuscarClienteProveedor.LoadDatil();
                                    MyPMNF_BuscarClienteProveedor.IsOpen = true;
                                }
                            }));
                        }                        
                    }
                });
            }
        }
        
        private void cbxUsuarioEmpresaSucursal_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ObjESGC_UsuarioEmpresaSucursal = (ESGC_UsuarioEmpresaSucursal)cbxUsuarioEmpresaSucursal.SelectedItem;
            LoadDataInUsuarioEmpSuc(ObjESGC_UsuarioEmpresaSucursal);
        }

        private void cbxMoneda_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            LoadSelRateTipoCambio();
        }

        private void dtpFechaOrden_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (dtpFechaOrden.SelectedDate != null && cbxPeriodo.SelectedValue != null)
            {
                int intAnio = Convert.ToInt32(cbxPeriodo.SelectedValue.ToString().Substring(0, 4));
                int intMes = Convert.ToInt32(cbxPeriodo.SelectedValue.ToString().Substring(4, 2));

                if (dtpFechaOrden.SelectedDate.Value.ToString("yyyyMM") != cbxPeriodo.SelectedValue.ToString())
                {
                    dtpFechaOrden.SelectedDate = new DateTime(intAnio, intMes + 1, 1).AddDays(-1);
                }                
            }            
            LoadSelRateTipoCambio();
            if (dtpFechaOrden.SelectedDate != null)
                dtpFechaEntrega.SelectedDate = dtpFechaOrden.SelectedDate.Value;
        }

        private void cbxPeriodo_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (cbxPeriodo.SelectedItem == null) return;
            string strPeriodo = ((EMNF_Periodo)cbxPeriodo.SelectedItem).Periodo;
            ObjECMP_OrdenCompra.Periodo = strPeriodo;

            MDatePicker.DateStartToDateEnd(dtpFechaOrden, strPeriodo, true);
        }

        private void cbxEstado_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ObjESGC_Estado = (ESGC_Estado)cbxEstado.SelectedItem;
        }

        private void cbxFormaPago_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            objESGC_FormaPago = (ESGC_FormaPago)cbxFormaPago.SelectedItem;
        }

        private void cbxAlmacenDestino_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ObjEALM_Almacen = (EALM_Almacen)cbxAlmacenDestino.SelectedItem;
            if (ObjEALM_Almacen == null) { return; }
            txtLugarEntrega.Text = ObjEALM_Almacen.Direccion;
        }

        private void btnAdministrarSucursalIsClicked(object sender, System.Windows.RoutedEventArgs e)
        {
            MSGC_UpdatePrivilege.Process(this, "SGC", CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.GetAccesoRestringidoNull("Sucursal"), new Action<ESGC_PermisoPerfil>((P) =>
            {
                this.FlyoutIsOpen("PSGC_EmpresaSucursal", ((value) => 
                {
                    if (value is PSGC_EmpresaSucursal)
                    {
                        var MyPSGC_EmpresaSucursal = (PSGC_EmpresaSucursal)value;
                        MyPSGC_EmpresaSucursal.InitializeAdministrarEmpresaSucursal(P, SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa);
                        MyPSGC_EmpresaSucursal.IsOpen = true;
                    }
                }));
            }), "PSGC_EmpresaSucursal");
        }

        private void btnAdministrarAlmacen_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            MSGC_UpdatePrivilege.Process(this, "ALM", CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.GetAccesoRestringidoNull("Almacen"), new Action<ESGC_PermisoPerfil>((P) =>
            {
                new CmpNavigationService().Show(new PALM_ListadoAlmacen(), null, CmpViewNavigationTipeShow.ShowDialog, "Administración Almacen");
                LoadDataInUsuarioEmpSuc((ESGC_UsuarioEmpresaSucursal)cbxUsuarioEmpresaSucursal.SelectedItem);
                ObjEALM_Almacen = null;
            }), "PALM_ListadoAlmacen");
        }

        //Calcula 
        private void npdCantidad_LostFocus_1(object sender, System.Windows.RoutedEventArgs e)
        {
            //var vrObjOrdenCompra = (ECMP_OrdenCompraDetalle)dgDetalleArticulo.SelectedItem;
            var vrObjOrdenCompra = (ECMP_OrdenCompraDetalle)dgDetalleArticulo.CurrentCell.Item;
            if (vrObjOrdenCompra == null) { return; }
            vrObjOrdenCompra.PrecioUnitarioTemp = (!ObjECMP_OrdenCompra.IncluyeIGV) ? vrObjOrdenCompra.PrecioUnitario : (vrObjOrdenCompra.PrecioUnitario * ((decimal.Round(dmlIGV * 100, 2) + 100) / 100));
            CalcularTotalesItems(vrObjOrdenCompra);
        }
        
        private void chkPrecioIncluidoIGV_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            var varObjECMP_OrdenCompra = (ECMP_OrdenCompra)MyHeader.DataContext;
            varObjECMP_OrdenCompra.IncluyeIGV = chkPrecioIncluidoIGV.IsChecked.Value;
            var ListECMP_OrdenCompraDetalle = (dgDetalleArticulo.Items.OfType<ECMP_OrdenCompraDetalle>()).ToList();
            foreach (var item in ListECMP_OrdenCompraDetalle)
            {
                CalcularTotalesItems(item);
            }
        }

        private string strTempValueTitle = string.Empty;
        private bool blIsGravada;

        private MtdCalculosTotales MyMtdCalculosTotales = new MtdCalculosTotales();
        private void CalCularPreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {

                #region VALIDACION  //AGREGADO 20160919 OSCAR HUAMAN CABRERA
                string message = string.Empty;
                if (dgDetalleArticulo.Items.Count <= 0)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, "Ingrese Artículo para poder editar este campo.", CmpButton.Aceptar);
                    return;
                }
                else
                {
                    var vrObjListECMP_OrdenCompraDetalle = new List<ECMP_OrdenCompraDetalle>();
                    vrObjListECMP_OrdenCompraDetalle = (dgDetalleArticulo.Items.OfType<ECMP_OrdenCompraDetalle>()).ToList();
                    vrObjListECMP_OrdenCompraDetalle.ForEach((f) =>
                    {
                        if (f.Cantidad == 0)
                            message = "Cantidad";
                    });
                    if (message.Length > 0)
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "Ingrese " + message + " del Articulo", CmpButton.Aceptar);
                        return;
                    }
                }
                #endregion

                blIsGravada = (((TextBox)sender).Name == "txtGravada");
                MyMtdCalculosTotales.Calcular(txtGravada, txtTotalIgv, blIsGravada, () =>
                {
                    if (blIsGravada)
                    {
                        txtGravada.IsReadOnly = false;
                        strTempValueTitle = lblTitleOrdenCompra03.Text;
                        lblTitleOrdenCompra03.Text += " (Enter)";
                        lblTitleOrdenCompra03.Foreground = Brushes.Orange;
                    }
                    else
                    {
                        txtTotalIgv.IsReadOnly = false;
                        strTempValueTitle = lblTitleOrdenCompra04.Text;
                        lblTitleOrdenCompra04.Text += " (Enter)";
                        lblTitleOrdenCompra04.Foreground = Brushes.Orange;
                    }
                });

            }
            catch (Exception ex)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, ex.Message, CmpButton.Aceptar);
            }
        }
        private void CalCularKeyEnter(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
                if (txtGravada.Text.Contains(" "))
                {
                    txtGravada.Text = txtGravada.Text.Replace(" ", "");
                }
                if (txtTotalIgv.Text.Contains(" "))
                {
                    txtTotalIgv.Text = txtTotalIgv.Text.Replace(" ", "");
                }
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    if (blIsGravada)
                    {
                        lblTitleOrdenCompra03.Text = strTempValueTitle;
                        lblTitleOrdenCompra03.Foreground = Brushes.White;
                        txtTotalNeto.Focus();
                        txtGravada.IsReadOnly = true;
                    }
                    else
                    {
                        lblTitleOrdenCompra04.Text = strTempValueTitle;
                        lblTitleOrdenCompra04.Foreground = Brushes.White;
                        txtTotalNeto.Focus();
                        txtTotalIgv.IsReadOnly = true;
                    }
                }

            }
            catch (Exception ex)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, ex.Message, CmpButton.Aceptar);
            }
        }
        private void txtGravada_LostFocus_1(object sender, System.Windows.RoutedEventArgs e)
        {

            #region Quitar (Enter) del titulo
            int indexOfTitleGravada = 0;
            int indexOfTitleIgv = 0;
            indexOfTitleIgv = lblTitleOrdenCompra04.Text.IndexOf("(");
            if (indexOfTitleIgv > 0)
            {
                strTempValueTitle = lblTitleOrdenCompra04.Text.Substring(0, indexOfTitleIgv - 1);
                lblTitleOrdenCompra04.Text = strTempValueTitle;
            }
            indexOfTitleGravada = lblTitleOrdenCompra03.Text.IndexOf("(");
            if (indexOfTitleGravada > 0)
            {
                strTempValueTitle = lblTitleOrdenCompra03.Text.Substring(0, indexOfTitleGravada - 1);
                lblTitleOrdenCompra03.Text = strTempValueTitle;
            }
            txtGravada.IsReadOnly = true;
            txtTotalIgv.IsReadOnly = true;
            #endregion

            blIsGravada = (((TextBox)sender).Name == "txtGravada");
            if (blIsGravada)
            {
                strTempValueTitle = lblTitleOrdenCompra03.Text;
                lblTitleOrdenCompra03.Text = strTempValueTitle;
                lblTitleOrdenCompra03.Foreground = Brushes.White;
                MyMtdCalculosTotales.Calcular(txtGravada, txtTotalIgv, blIsGravada, () =>
                {
                    txtGravada.IsReadOnly = true;
                });
            }
            else
            {
                strTempValueTitle = lblTitleOrdenCompra04.Text;
                lblTitleOrdenCompra04.Text = strTempValueTitle;
                lblTitleOrdenCompra04.Foreground = Brushes.White;
                MyMtdCalculosTotales.Calcular(txtGravada, txtTotalIgv, blIsGravada, () =>
                {
                    txtTotalIgv.IsReadOnly = true;
                });
            }
            txtTotalNeto.Focus();
            MyMtdCalculosTotales.NewValue(txtGravada, txtTotalIgv, Convert.ToDecimal(txtTotalNeto.Text));
            txtTotalNeto.Text = (Convert.ToDecimal(txtGravada.Text) + Convert.ToDecimal(txtTotalIgv.Text)).ToString("###,###,##0.#0");
        }

        public void btnAgregarArticuloIsClicked()
        {
            if (!btnAgregarArticulo.IsEnabled)
                return;

            MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.GetAccesoRestringidoNull("Artículos"), new Action<ESGC_PermisoPerfil>((P) =>
            {
                if (P.Consulta)
                {
                    this.FlyoutIsOpen("PMNF_BuscarArticulos", ((value) => 
                    {
                        if (value is PMNF_BuscarArticulos)
                        {
                            var FlyoutsPMNF_BuscarArticulos = (PMNF_BuscarArticulos)value;
                            FlyoutsPMNF_BuscarArticulos.InitializePMNF_BuscarArticulos();
                            FlyoutsPMNF_BuscarArticulos.LoadHeader();
                            FlyoutsPMNF_BuscarArticulos.IsOpen = true;
                        }
                    }));
                }
                else
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.GetAccesoRestringidoBuscar("Artículos"), CmpButton.Aceptar);
                }
            }));
        }

        public void btnCancelarProcesoIsClicked()
        {
            if (!btnAnularProceso.IsEnabled)
                return;

            CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.PreguntaContinuarProceso, CmpButton.AceptarCancelar, () =>
            {
                ClearControl();
                Parameter = new ECMP_OrdenCompra(null, TipoConstructor.Insert);
            });
        }

        public void btnGuardarIsClicked()
        {
            if (!btnGuardar.IsEnabled)
                return;

            MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.GetAccesoRestringidoNull("Orden Compra"), new Action<ESGC_PermisoPerfil>((P) =>
            {
                if (P.Nuevo || P.Editar)
                {
                    if (ValidaDatos()) { return; }

                    var varObjECMP_OrdenCompra = (ECMP_OrdenCompra)MyHeader.DataContext;
                    varObjECMP_OrdenCompra.ObjESGC_EmpresaSucursal = ObjESGC_UsuarioEmpresaSucursal.ObjESGC_EmpresaSucursal;
                    varObjECMP_OrdenCompra.ObjEMNF_ClienteProveedor = ObjEMNF_ClienteProveedor;
                    varObjECMP_OrdenCompra.ObjEALM_Almacen = ObjEALM_Almacen;
                    varObjECMP_OrdenCompra.ObjESGC_Moneda = ObjESGC_Moneda;
                    varObjECMP_OrdenCompra.ObjESGC_Estado = ObjESGC_Estado;
                    varObjECMP_OrdenCompra.ObjESGC_FormaPago = objESGC_FormaPago;
                    varObjECMP_OrdenCompra.Gravada = Convert.ToDecimal(txtGravada.Text);
                    varObjECMP_OrdenCompra.Exonerada = Convert.ToDecimal(lblExonerado.Text);
                    varObjECMP_OrdenCompra.ImporteIGV = Convert.ToDecimal(txtTotalIgv.Text);
                    varObjECMP_OrdenCompra.IGV = dmlIGV;
                    varObjECMP_OrdenCompra.IncluyeIGV = chkPrecioIncluidoIGV.IsChecked.Value;
                    varObjECMP_OrdenCompra.CadenaXML = XmlDetalleOrdenCompra();
                    string strOutMessageError = string.Empty;
                    CmpMessageBox.Proccess(CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.ProcesandoDatos, () =>
                    {
                        try
                        {
                            new BCMP_OrdenCompra().TransOrdenCompra(varObjECMP_OrdenCompra);
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
                            CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, strOutMessageError, CmpButton.Aceptar);
                        }
                        else
                        {
                            txtGravada.Text =string.Format("{0:N2}" ,varObjECMP_OrdenCompra.Gravada);
                            lblExonerado.Text =string.Format("{0:N2}" , varObjECMP_OrdenCompra.Exonerada);
                            txtTotalIgv.Text = string.Format("{0:N2}", varObjECMP_OrdenCompra.ImporteIGV);
                            CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.DatoProcesados + "\n ¿Desea Imprimir el documento de Orden de Compra?", CmpButton.AceptarCancelar, () =>
                            {
                                ImprimirOrdenCompra();
                                btnSalirIsClicked();
                            }, 
                            () =>
                            {
                                btnSalirIsClicked();
                            });
                        }
                    });
                }
                else
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.GetAccesoRestringidoNuevo("Orden Compra"), CmpButton.Aceptar);
                }
            }));
        }

        public void btnQuitarArticuloIsClicked()
        {
            if (!btnQuitarArticulo.IsEnabled)
                return;

            if (dgDetalleArticulo.Items.Count <= 0) return;
			if (!(dgDetalleArticulo.CurrentCell.Item is ECMP_OrdenCompraDetalle)) return;
            var resutl = (ECMP_OrdenCompraDetalle)dgDetalleArticulo.CurrentCell.Item;
            if (resutl == null) { return; }

            CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.QuitarItems, CmpButton.AceptarCancelar, () =>
            {
                dgDetalleArticulo.Items.Remove(resutl);
                CalcularTotales();
                var ObjListEIngresoSalidaDetalle = (dgDetalleArticulo.Items.OfType<ECMP_OrdenCompraDetalle>()).ToList();
                dgDetalleArticulo.Items.Clear();

                int intItem = 1;
                foreach (var item in ObjListEIngresoSalidaDetalle)
                {
                    item.Item = intItem;
                    dgDetalleArticulo.Items.Add(item);
                    intItem++;
                }
            });
            btnQuitarArticulo.IsEnabled = (dgDetalleArticulo.Items.Count > 0);
        }

        public void btnImprimirIsClicked()
        {
            if (!btnImprimir.IsEnabled)
                return;

            ImprimirOrdenCompra();
        }

        public void btnSalirIsClicked()
        {
            if (!btnSalir.IsEnabled)
                return;

            MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminCompra, CMPMensajes.GetAccesoRestringidoNull("Orden Compra"), ((P) =>
            {
                new CmpNavigationService().Volver(_MyFrame, ObjECMP_OrdenCompra);
            }),
            MyNameFomulario: "PCMP_ListadoOrdenCompra",
            MyActionAbort: () =>
            {
                this.Close(TipoModulo.ManuFactura);
            });
        }

        #endregion

        #region MÉTODOS Y FUNCIONES

        /// <summary>
        /// Lista de Flyouts
        /// </summary>
        public void AddFlyout()
        {
            #region BUSCAR PROVEEDOR
           
            PMNF_BuscarClienteProveedor MyPMNF_BuscarClienteProveedor = new PMNF_BuscarClienteProveedor();
            MyPMNF_BuscarClienteProveedor.IsSelected += new PMNF_BuscarClienteProveedor.isSelected((value) => { AddValueProveedor(value); });
            
            #endregion

            #region AGREGAR ARTÍCULO

            PMNF_BuscarArticulos MyPMNF_BuscarArticulos = new PMNF_BuscarArticulos();
            MyPMNF_BuscarArticulos.IsSelected += new PMNF_BuscarArticulos.isSelected((value) => { AddItemsArticulos(value); });

            #endregion

            #region ADMINISTRAR SUCURSAL

            PSGC_EmpresaSucursal MyPSGC_EmpresaSucursal = new PSGC_EmpresaSucursal();

            #endregion

            this.FlyoutInitialize();
            this.FlyoutAdd(MyPMNF_BuscarClienteProveedor);
            this.FlyoutAdd(MyPMNF_BuscarArticulos);
            this.FlyoutAdd(MyPSGC_EmpresaSucursal);
        }

        /// <summary>
        /// Metodo al terminar de administrar Sucursal
        /// </summary>
        public void FinalizedAdministrarSucursal()
        {
            LoadHeader(null);
            ObjESGC_UsuarioEmpresaSucursal = null;
        }

        /// <summary>
        /// Instancia y pinta valor del Proveedor seleccionado de la busqueda
        /// </summary>
        /// <param name="ObjEMNF_ClienteProveedor">Objeto de la clase Proveedor</param>
        public void AddValueProveedor(EMNF_ClienteProveedor ObjEMNF_ClienteProveedor) 
        {
            if (ObjEMNF_ClienteProveedor != null)
            {
                this.ObjEMNF_ClienteProveedor = ObjEMNF_ClienteProveedor;
                txtProveedorRazonSocial.Text = ObjEMNF_ClienteProveedor.RazonSocial;
                cbxFormaPago.SelectedValue = ObjEMNF_ClienteProveedor.ObjEMNF_FormaPago.IdFormaPago;
            }
        }

        /// <summary>
        /// Instancia y pinta valor del Artículo seleccionado de la busqueda
        /// </summary>
        /// <param name="ObjEMNF_Articulo">Objeto de la clase Artículo</param>
        public void AddItemsArticulos(EMNF_Articulo ObjEMNF_Articulo)
        {
            var ListECMP_OrdenCompraDetalle = (dgDetalleArticulo.Items.OfType<ECMP_OrdenCompraDetalle>()).ToList();
            bool existeArticulo = ListECMP_OrdenCompraDetalle.Exists(x => x.ObjEMNF_Articulo.IdArticulo == ObjEMNF_Articulo.IdArticulo);
            if (existeArticulo)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, "El item seleccionado ya existe.", CmpButton.Aceptar);
                return;
            }
            var ObjECMP_OrdenCompraDetalle = new ECMP_OrdenCompraDetalle
            {
                Item = ListECMP_OrdenCompraDetalle.Count + 1,
                ObjEMNF_Articulo = ObjEMNF_Articulo,
                ListEstado = ListESGC_EstadoDetail.Where(x => x.Campo == "CodEstRecepcion").ToList(),
                ObjECMP_OrdenCompra = ObjECMP_OrdenCompra,
                IsEnableEstado = false
            };
            dgDetalleArticulo.Items.Add(ObjECMP_OrdenCompraDetalle);
            CalcularTotales();
            btnQuitarArticulo.IsEnabled = (dgDetalleArticulo.Items.Count > 0);
        }

        /// <summary>
        /// Carga datos para la administración de orden de compra
        /// </summary>
        private void LoadHeader(Action MyAction)
        {
            var vrListESGC_UsuarioEmpresaSucursal = new List<ESGC_UsuarioEmpresaSucursal>();
            var vrListESGC_Moneda = new List<ESGC_Moneda>();
            var vrListESGC_FormaPago = new List<ESGC_FormaPago>();
            var vrListEMNF_Periodo = new List<EMNF_Periodo>();
            var vrListESGC_Estado = new List<ESGC_Estado>();
            var vrListESGC_FormularioSetting = new List<ESGC_FormularioSetting>();

            string strPeriodoActual = new BMNF_Periodo().GetPeriodoActual();

            string strOutMessageError = string.Empty;
            CmpTask.Process(
            () =>
            {
                try
                {
                    vrListESGC_UsuarioEmpresaSucursal = new BSGC_UsuarioEmpresaSucursal().ListFiltrarUsuarioEmpresaSucursal(SGCVariables.ObjESGC_Usuario);
                    vrListESGC_Moneda = new BSGC_Moneda().ListGetMoneda();
                    vrListEMNF_Periodo = new BMNF_Periodo().ListPeriodo();
                    vrListESGC_Estado = new BSGC_Estado().ListFiltrarEstado(SGCMethod.GetNameNameTableInXaml(this));
                    vrListESGC_FormaPago = new BSGC_FormaPago().ListGetFormaPago();

                    if (ObjECMP_OrdenCompra.Opcion == "I")
                    {
                        vrListESGC_FormularioSetting = new BSGC_FormularioSetting().ListGetFormularioSetting(this.GetType().Name);
                        
                        this.Dispatcher.Invoke(() =>
                        {
                            MDatePicker.DateStartToDateEnd(dtpFechaOrden, strPeriodoActual, true);
                            ObjECMP_OrdenCompra.Fecha = dtpFechaOrden.SelectedDate.Value;
                            ObjECMP_OrdenCompra.FechaEntrega = dtpFechaEntrega.SelectedDate.Value;
                        });
                    }
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
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, strOutMessageError, CmpButton.Aceptar);
                }
                else
                {
                    cbxUsuarioEmpresaSucursal.ItemsSource = vrListESGC_UsuarioEmpresaSucursal;
                    cbxMoneda.ItemsSource = vrListESGC_Moneda;
                    cbxPeriodo.ItemsSource = (ObjECMP_OrdenCompra.Opcion == "I") ? vrListEMNF_Periodo.Where(x => x.Estado == "A") : vrListEMNF_Periodo;
                    cbxFormaPago.ItemsSource = vrListESGC_FormaPago;

                    if (MyAction != null) { MyAction.Invoke(); }

                    //Si es inser agregamos valores predefinidos de la base de datos
                    if (ObjECMP_OrdenCompra.Opcion == "I")
                    {
                        btnImprimir.IsEnabled = false;
                        dmlIGV = SGCVariables.ObjESGC_Retencion.IGV / 100;
                        AddValueDefault(vrListESGC_FormularioSetting);                        
                        ObjECMP_OrdenCompra.Periodo = strPeriodoActual;

                        cbxEstado.ItemsSource = vrListESGC_Estado.Where(x => x.Campo == "CodEstado");
                    }
                    else
                    {
                        cbxMoneda.IsEnabled = !(ObjECMP_OrdenCompra.ObjESGC_Estado.CodEstado == "ATCOC");
                        chkPrecioIncluidoIGV.IsChecked = ObjECMP_OrdenCompra.IncluyeIGV;
                        btnImprimir.IsEnabled = true;
                        dmlIGV = ObjECMP_OrdenCompra.IGV;
                        cbxEstado.ItemsSource = vrListESGC_Estado;
                    }

                    lblTitleOrdenCompra04.Text = "IGV " + decimal.Round(dmlIGV * 100, 2) + "%";

                    MyHeader.DataContext = ObjECMP_OrdenCompra;
                    cbxUsuarioEmpresaSucursal.Focus();

                }
            });
        }

        /// <summary>
        /// Carga datos del orden de compra
        /// </summary>
        private void LoadDetail()
        {
            dgDetalleArticulo.Items.Clear();
            var vrListECompraDetalle = new List<ECMP_OrdenCompraDetalle>();

            string strOutMessageError = string.Empty;
            CmpTask.Process(
            () =>
            {
                try
                {
                    ListESGC_EstadoDetail = new BSGC_Estado().ListFiltrarEstado(SGCMethod.GetNameNameTableInXaml(new ECMP_OrdenCompraDetalle()));
                    if (ObjECMP_OrdenCompra.Opcion == "U")
                    {
                        vrListECompraDetalle = new BCMP_OrdenCompraDetalle().ListAdministrarOrdenCompraDetalle(ObjECMP_OrdenCompra);
                        vrListECompraDetalle.ForEach(x => x.IsEnableEstado = false);
                    }
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
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, strOutMessageError, CmpButton.Aceptar);
                }
                else
                {
                    vrListECompraDetalle.ForEach(ysr => ysr.ListEstado = ListESGC_EstadoDetail.Where(x => x.Campo == "CodEstRecepcion").ToList());

                    foreach (var item in vrListECompraDetalle)
                    {
                        if (item.ObjEMNF_Articulo.ObjEMNF_OperacionIGV.CodOperacionIGV == "GB")
                        {
                            if (chkPrecioIncluidoIGV.IsChecked.Value || ObjECMP_OrdenCompra.IncluyeIGV)
                            {
                                //pRECIO INCLUIDO IGV
                                //decimal dmlCalculoIGV =decimal.Round( item.PrecioUnitario * dmlIGV,8);
                                decimal dmlCalculoIGV = item.PrecioUnitario * dmlIGV ;

                                item.PrecioUnitario = decimal.Round(dmlCalculoIGV + item.PrecioUnitario, 8);
                            }
                        }

                        dgDetalleArticulo.Items.Add(item);
                        CalcularTotalesItems(item);
                    }
                    btnQuitarArticulo.IsEnabled = (dgDetalleArticulo.Items.Count > 0);

                    lblExonerado.Text = ObjECMP_OrdenCompra.Exonerada.ToString("###,###,##0.#0");
                    txtGravada.Text = ObjECMP_OrdenCompra.Gravada.ToString("###,###,##0.#0");
                    txtTotalIgv.Text = ObjECMP_OrdenCompra.ImporteIGV.ToString("###,###,##0.#0");
                    txtTotalNeto.Text = (ObjECMP_OrdenCompra.Exonerada + ObjECMP_OrdenCompra.Gravada + ObjECMP_OrdenCompra.ImporteIGV).ToString("###,###,##0.#0");

                    Load = false;
                }
            });            
        }

        /// <summary>
        /// Carga datos de los sucursales dependiendo al sucursal
        /// </summary>
        /// <param name="ObjESGC_UsuarioEmpresaSucursal">Objeto de la entidad Usuario Empresa Sucursal</param>
        private void LoadDataInUsuarioEmpSuc(ESGC_UsuarioEmpresaSucursal ObjESGC_UsuarioEmpresaSucursal)
        {
            ObjEALM_Almacen = null;
            if (ObjESGC_UsuarioEmpresaSucursal == null) { return; }
            var vrListEALM_Almacen = new List<EALM_Almacen>();
            var vrObjESGC_Documento = new ESGC_Documento();

            string strOutMessageError = string.Empty;
            CmpTask.Process(
            () =>
            {
                try
                {
                    if (ObjECMP_OrdenCompra.Opcion == "I")
                    {
                        vrObjESGC_Documento = new BSGC_Documento().GetNroDocumento("ODC", ObjESGC_UsuarioEmpresaSucursal.ObjESGC_EmpresaSucursal);
                    }
                    vrListEALM_Almacen = new BALM_Almacen().ListFiltrarAlmacen(ObjESGC_UsuarioEmpresaSucursal.ObjESGC_EmpresaSucursal);
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
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, strOutMessageError, CmpButton.Aceptar);
                }
                else
                {
                    //Si es inser agregamos valores predefinidos de la base de datos
                    if (ObjECMP_OrdenCompra.Opcion == "I")
                    {

                        if ((vrObjESGC_Documento.Correlativo == null || vrObjESGC_Documento.Serie == null) && vrListEALM_Almacen.Count == 0)
                        {
                            txtCorrelativo.Text = string.Empty;
                            txtSerie.Text = string.Empty;
                            cbxAlmacenDestino.ItemsSource = vrListEALM_Almacen;
                            CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, "La sucursal seleccionada no cuenta con un tipo de documento configurado \nLa sucursal seleccionada no cuenta con almacén creado", CmpButton.Aceptar);
                            return;
                        }
                        else if (vrObjESGC_Documento.Correlativo == null || vrObjESGC_Documento.Serie == null)
                        {
                            txtCorrelativo.Text = string.Empty;
                            txtSerie.Text = string.Empty;
                            cbxAlmacenDestino.ItemsSource = vrListEALM_Almacen;
                            CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, "La sucursal seleccionada no cuenta con un tipo de documento configurado", CmpButton.Aceptar);
                            return;
                        }
                        else if (vrListEALM_Almacen.Count == 0)
                        {
                            cbxAlmacenDestino.ItemsSource = vrListEALM_Almacen;
                            CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, "La sucursal seleccionada no cuenta con almacén creado", CmpButton.Aceptar);
                            return;
                        }
                        else
                        {
                            txtCorrelativo.Text = vrObjESGC_Documento.Correlativo;
                            txtSerie.Text = vrObjESGC_Documento.Serie;
                        }
                    }
                    else
                    {
                        cbxUsuarioEmpresaSucursal.IsEnabled = false; 
                    }
                    
                    
                    cbxAlmacenDestino.ItemsSource = vrListEALM_Almacen;

                    cbxAlmacenDestino.IsEnabled = (cbxAlmacenDestino.Items.Count > 0); 
                }
            });
        }

        /// <summary>
        /// Carga el tipo de cambio
        /// </summary>
        /// <param name="ObjBSGC_Moneda">Objeto de la referencia Moneda</param>
        private void LoadSelRateTipoCambio()
        {
            ObjESGC_Moneda = (ESGC_Moneda)cbxMoneda.SelectedItem;
            if (ObjESGC_Moneda != null && ObjECMP_OrdenCompra.ObjESGC_Estado != null)
            {
                lblTitleOrdenCompra05.Text = "Total " + ObjESGC_Moneda.Simbolo;
                if (ObjESGC_Moneda.Defecto != true && (ObjECMP_OrdenCompra.ObjESGC_Estado.CodEstado == "PECOC" || ObjECMP_OrdenCompra.ObjESGC_Estado.CodEstado == "APCOC"))
                {
                    var vrFecha = new DateTime();

                    if (dtpFechaOrden.SelectedDate == null || (Load && ObjECMP_OrdenCompra.TipoCambio > 0))
                    { 
                        return; 
                    }

                    vrFecha = (dtpFechaOrden.SelectedDate) ?? ObjECMP_OrdenCompra.Fecha;
                                       
                    string strSelRateTipoCambio = string.Empty;

                    string strOutMessageError = string.Empty;
                    CmpTask.Process(
                    () =>
                    {
                        try
                        {
                            strSelRateTipoCambio = new BSGC_Tipocambio().PrintSelRateTipocambio(new ESGC_Tipocambio()
                            {
                                ObjESGC_Moneda = ObjESGC_Moneda
                            }, vrFecha);
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
                            CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, strOutMessageError, CmpButton.Aceptar);
                        }
                        else
                        {
                            if (strSelRateTipoCambio.Trim().Length > 0)
                            {
                                txtSelRateTipoCambio.IsEnabled = true;
                                txtSelRateTipoCambio.Value = Convert.ToDouble(strSelRateTipoCambio);
                            }
                            else
                            {
                                txtSelRateTipoCambio.Value = 0;
                                txtSelRateTipoCambio.IsEnabled = false;
                                if (dtpFechaOrden.SelectedDate != null)
                                {
                                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, "No se ha registrado el tipo de cambio de la moneda extranjera para el día " + vrFecha.ToShortDateString(), CmpButton.Aceptar);
                                }
                            }
                        }
                    });
                }
                else if (ObjESGC_Moneda.Defecto)
                {
                    txtSelRateTipoCambio.Value = 1;
                    txtSelRateTipoCambio.IsEnabled = false;
                }
                else
                {
                    txtSelRateTipoCambio.Value = 0;
                    txtSelRateTipoCambio.IsEnabled = false;
                }
            }

        }

        /// <summary>
        /// Calculta los totales
        /// </summary>
        private void CalcularTotales()
        {
            try
            {
                if (strTempValueTitle.Trim().Length > 0)
                {
                    if (blIsGravada)
                    {
                        lblTitleOrdenCompra03.Text = strTempValueTitle;
                        lblTitleOrdenCompra03.Foreground = Brushes.White;
                    }
                    else
                    {
                        lblTitleOrdenCompra04.Text = strTempValueTitle;
                        lblTitleOrdenCompra04.Foreground = Brushes.White;
                    }
                }

                var ListECMP_OrdenCompraDetalle = (dgDetalleArticulo.Items.OfType<ECMP_OrdenCompraDetalle>()).ToList();
                if (ListECMP_OrdenCompraDetalle != null && ListECMP_OrdenCompraDetalle.Count > 0)
                {
                    decimal dmlTotal = 0;
                    decimal dmlGravada = 0;
                    decimal dmlImporteIGV = 0;

                    //factura
                    if (chkPrecioIncluidoIGV.IsChecked.Value || ObjECMP_OrdenCompra.IncluyeIGV)
                    {
                        //Calculo con incluir IGV
                        dmlGravada = ListECMP_OrdenCompraDetalle.Sum(x => (x.ObjEMNF_Articulo.ObjEMNF_OperacionIGV.CodOperacionIGV == "GB" ? x.Importe : 0));
                        dmlImporteIGV = ListECMP_OrdenCompraDetalle.Sum(x => (x.ObjEMNF_Articulo.ObjEMNF_OperacionIGV.CodOperacionIGV == "GB" ? x.ImporteIGV : 0));
                        dmlTotal = dmlGravada + dmlImporteIGV;
                    }
                    else
                    {
                        //Calculo sin incluir IGV
                        ListECMP_OrdenCompraDetalle.ForEach(x => { if (x.ObjEMNF_Articulo.ObjEMNF_OperacionIGV.CodOperacionIGV == "GB") dmlGravada += x.PrecioUnitario * x.Cantidad; });
                        dmlImporteIGV = dmlGravada * dmlIGV;
                        dmlTotal = dmlGravada + dmlImporteIGV;
                    }

                    lblExonerado.Text = ListECMP_OrdenCompraDetalle.Sum(o => (o.ObjEMNF_Articulo.ObjEMNF_OperacionIGV.CodOperacionIGV == "EX" ? o.Importe : 0)).ToString("###,###,##0.#0");

                    ObjECMP_OrdenCompra.Gravada = dmlGravada;
                    ObjECMP_OrdenCompra.ImporteIGV = dmlImporteIGV;

                    lblLineas.Text = ListECMP_OrdenCompraDetalle.Count.ToString();
                    txtGravada.Text = dmlGravada.ToString("###,###,##0.#0");
                    txtTotalIgv.Text = dmlImporteIGV.ToString("###,###,##0.#0");
                    txtTotalNeto.Text = Convert.ToDouble(dmlTotal + Convert.ToDecimal(lblExonerado.Text)).ToString("N2");
                }
                else
                {
                    lblLineas.Text = ("0");
                    txtGravada.Text = ("0.00");
                    txtTotalIgv.Text = ("0.00");
                    lblExonerado.Text = ("0.00");
                    txtTotalNeto.Text = ("0.00");
                }
                dgDetalleArticulo.Items.Refresh();
                CmpGridSelectColumn.SelectCellByIndex(dgDetalleArticulo, new List<CmpGridItem>()
                {
                    new CmpGridItem(){ColumnIni = 4, ColumnFin = 5} 
                });
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Calcula los valores del registro seleccionado
        /// </summary>
        private void CalcularTotalesItems(ECMP_OrdenCompraDetalle ObjECMP_OrdenCompraDetalle)
        {
            try
            {
                var varObjECMP_OrdenCompra = (ECMP_OrdenCompra)MyHeader.DataContext;
                //factura
                if (ObjECMP_OrdenCompraDetalle.ObjEMNF_Articulo.ObjEMNF_OperacionIGV.CodOperacionIGV == "GB")
                {
                    if (chkPrecioIncluidoIGV.IsChecked.Value || varObjECMP_OrdenCompra.IncluyeIGV)
                    {
                        //Calculo con incluir IGV
                        decimal dmlPrecioUnitario = decimal.Round((ObjECMP_OrdenCompraDetalle.PrecioUnitarioTemp / ((decimal.Round(dmlIGV * 100, 2) + 100) / 100)), 8);
                        decimal dmlImporte = decimal.Round((dmlPrecioUnitario * ObjECMP_OrdenCompraDetalle.Cantidad), 8);
                        decimal dmlImporteIGV = decimal.Round(dmlImporte  * dmlIGV, 8);

                        ObjECMP_OrdenCompraDetalle.PrecioUnitario = dmlPrecioUnitario;
                        ObjECMP_OrdenCompraDetalle.Importe = dmlImporte;
                        ObjECMP_OrdenCompraDetalle.ImporteIGV = dmlImporteIGV;
                    }

                    else
                    {
                        //Calculo sin incluir IGV
                        decimal dmlImporte = decimal.Round((ObjECMP_OrdenCompraDetalle.PrecioUnitarioTemp * ObjECMP_OrdenCompraDetalle.Cantidad), 8);
                        decimal dmlImporteIGV = decimal.Round(dmlImporte * dmlIGV, 8);

                        ObjECMP_OrdenCompraDetalle.PrecioUnitario = ObjECMP_OrdenCompraDetalle.PrecioUnitarioTemp;
                        ObjECMP_OrdenCompraDetalle.Importe = dmlImporte;
                        ObjECMP_OrdenCompraDetalle.ImporteIGV = dmlImporteIGV;
                    }
                }
                else
                {
                    ObjECMP_OrdenCompraDetalle.ImporteIGV = 0;
                    ObjECMP_OrdenCompraDetalle.Importe =decimal.Round(( (ObjECMP_OrdenCompraDetalle.PrecioUnitario * ObjECMP_OrdenCompraDetalle.Cantidad) + ObjECMP_OrdenCompraDetalle.ImporteIGV),8);
                }
                CalcularTotales();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Valida datos requeridos
        /// </summary>
        /// <returns></returns>
        private bool ValidaDatos()
        {
            bool blResult = false;
            var ListECMP_OrdenCompraDetalle = (dgDetalleArticulo.Items.OfType<ECMP_OrdenCompraDetalle>()).ToList();

            if (ObjESGC_UsuarioEmpresaSucursal == null)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, "Por favor seleccione un sucursal.", CmpButton.Aceptar);
                blResult = true;
            }
            else if (ObjEMNF_ClienteProveedor == null)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, "Por favor seleccione un proveedor.", CmpButton.Aceptar);
                blResult = true;
            }
            else if (ObjEALM_Almacen == null)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, "Por favor seleccione un almacen.", CmpButton.Aceptar);
                blResult = true;
            }
            else if (ObjESGC_Moneda == null)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, "Por favor seleccione el tipo de moneda.", CmpButton.Aceptar);
                blResult = true;
            }

            else if (ObjESGC_Estado == null)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, "Por favor seleccione un estado.", CmpButton.Aceptar);
                blResult = true;
            }
            else if (objESGC_FormaPago == null)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, "Por favor seleccione forma pago.", CmpButton.Aceptar);
                blResult = true;
            }
            else if (ListECMP_OrdenCompraDetalle.Count == 0)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, "Por favor ingrese el item del producto en el detalle.", CmpButton.Aceptar);
                blResult = true;
            }
            else if (txtSelRateTipoCambio.Value.ToString().Trim().Length == 0)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, "Por favor ingrese el tipo de cambio.", CmpButton.Aceptar);
                blResult = true;
            }
            else if (txtSerie.Text.Trim().Length == 0 || txtCorrelativo.Text.Trim().Length == 0)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, "Serie Y Número de documento son obligatorio.", CmpButton.Aceptar);
                blResult = true;
            }
            else
            {
                bool blEstadoCantidad = false;
                string strArtDescrip = string.Empty;

                foreach (var ff in ListECMP_OrdenCompraDetalle)
                {
                    if (ff.PrecioUnitario == 0)
                    {
                        blEstadoCantidad = true;
                        strArtDescrip = "Precio Unitario ingresada del producto " + ff.ObjEMNF_Articulo.Articulo + " debe ser mayor a cero.";

                        break;
                    }
                    else if (ff.Cantidad == 0)
                    {
                        blEstadoCantidad = true;
                        strArtDescrip = "La cantidad ingresada del producto " + ff.ObjEMNF_Articulo.Articulo + " debe ser mayor a cero.";
                        break;
                    }
                }

                if (blEstadoCantidad)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, strArtDescrip, CmpButton.Aceptar);
                    blResult = true;
                }
            }

            return blResult;
        }

        /// <summary>
        /// Crea XML del detalle
        /// </summary>
        /// <returns></returns>
        private string XmlDetalleOrdenCompra()
        {
            var ListECMP_OrdenCompraDetalle = (dgDetalleArticulo.Items.OfType<ECMP_OrdenCompraDetalle>()).ToList();
            string strCadXml = "";
            strCadXml = "<ROOT>";
            ListECMP_OrdenCompraDetalle.ForEach((ECMP_OrdenCompraDetalle f) =>
            {
                if (f.ObjEMNF_Articulo.IdArticulo != 0)
                {
                    decimal dmlPrecioUnitario  = 0;

                    if (!(f.ObjEMNF_Articulo.ObjEMNF_OperacionIGV.CodOperacionIGV == "GB"))
                    {
                        dmlPrecioUnitario = f.PrecioUnitario;
                    }
                    else
                    {
                        if (ObjECMP_OrdenCompra.IncluyeIGV)
                        {
                            dmlPrecioUnitario = (f.PrecioUnitarioTemp) / ((decimal.Round(dmlIGV * 100, 2) + 100) / 100);
                        }
                        else
                        {
                            dmlPrecioUnitario = f.PrecioUnitarioTemp;
                        }
                    }

                    strCadXml += "<Listar xIdArticulo = \'" + f.ObjEMNF_Articulo.IdArticulo +
                                    "\' xCodEstRecepcion = \'" + f.ObjESGC_Estado.CodEstado +
                                    "\' xCodUndMedida = \'" + f.ObjEMNF_Articulo.ObjEMNF_UnidadMedida.CodUndMedida +
                                    "\' xCantidad = \'" + f.Cantidad +
                                    "\' xPrecioUnitario = \'" + dmlPrecioUnitario +
                                    "\' xImporteIGV = \'" + f.ImporteIGV +
                                    "\' ></Listar>";
                }
            });
            strCadXml += "</ROOT>";

            return strCadXml;
        }

        /// <summary>
        /// Limpia datos de los controles y de las instancias creadas
        /// </summary>
        private void ClearControl()
        {
            try
            {
                this.ClearControls();
            }
            catch(Exception){}

            dgDetalleArticulo.Items.Clear();

            //this.DataContext = null;
            MyHeader.DataContext = null;
            ObjESGC_UsuarioEmpresaSucursal = null;
            ObjEMNF_ClienteProveedor = null;
            ObjEALM_Almacen = null;
            ObjESGC_Moneda = null;
            ObjESGC_Estado = null;
            objESGC_FormaPago = null;
            
            cbxAlmacenDestino.ItemsSource = null;
            cbxAlmacenDestino.IsEnabled = false; 


            lblTitleOrdenCompra04.Text = "IGV " + decimal.Round(dmlIGV * 100, 2) + "%";
            lblLineas.Text = ("0");
            txtGravada.Text = ("0.00");
            lblExonerado.Text = ("0.00");
            txtTotalIgv.Text = ("0.00");
            txtTotalNeto.Text = ("0.00");

        }

        /// <summary>
        /// Asigna valor por defecto a la entidad actual
        /// </summary>
        /// <param name="vrListESGC_FormularioSetting"></param>
        private void AddValueDefault(List<ESGC_FormularioSetting> vrListESGC_FormularioSetting)
        {
            //ESGC_EmpresaSucursal
            var vrIdEmpSucursal = vrListESGC_FormularioSetting.FirstOrDefault(x => x.Campo == "IdEmpSucursal");
            if (vrIdEmpSucursal != null)
            {
                #region ASIGNACIÓN

                try
                {
                    ObjECMP_OrdenCompra.ObjESGC_EmpresaSucursal = new ESGC_EmpresaSucursal() { IdEmpSucursal = Convert.ToInt32(vrIdEmpSucursal.Codigo) };
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, "Error al asignar datos por defecto a [IdEmpSucursal] \n" + ex.Message, CmpButton.Aceptar);
                }

                #endregion
            }

            //ObjESGC_Moneda
            var vrCodMoneda = vrListESGC_FormularioSetting.FirstOrDefault(x => x.Campo == "CodMoneda");
            if (vrCodMoneda != null)
            {
                #region ASIGNACIÓN

                try
                {
                    ObjECMP_OrdenCompra.ObjESGC_Moneda = new ESGC_Moneda() { CodMoneda = vrCodMoneda.Codigo };
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, "Error al asignar datos por defecto a [CodMoneda] \n" + ex.Message, CmpButton.Aceptar);
                }

                #endregion
            }

            //ObjESGC_Estado
            var vrCodEstado = vrListESGC_FormularioSetting.FirstOrDefault(x => x.Campo == "CodEstado");
            if (vrCodEstado != null)
            {
                #region ASIGNACIÓN

                try
                {
                    ObjECMP_OrdenCompra.ObjESGC_Estado = new ESGC_Estado() { CodEstado = vrCodEstado.Codigo };
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, "Error al asignar datos por defecto a [CodEstado] \n" + ex.Message, CmpButton.Aceptar);
                }

                #endregion
            }

            //vrIdFormaPago
            var vrIdFormaPago = vrListESGC_FormularioSetting.FirstOrDefault(x => x.Campo == "IdFormaPago");
            if (vrIdFormaPago != null)
            {
                #region ASIGNACIÓN

                try
                {
                    ObjECMP_OrdenCompra.ObjESGC_FormaPago = new ESGC_FormaPago() { IdFormaPago = Convert.ToInt32(vrIdFormaPago.Codigo) };
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, "Error al asignar datos por defecto a [IdFormaPago] \n" + ex.Message, CmpButton.Aceptar);
                }

                #endregion
            }
        }

        /// <summary>
        /// Imprime un reporte de la venta
        /// </summary>
        private void ImprimirOrdenCompra()
        {
            try
            {
                var vrObjListECMP_OrdenCompraDetalle = new List<ECMP_OrdenCompraDetalle>();
               
                vrObjListECMP_OrdenCompraDetalle = (dgDetalleArticulo.Items.OfType<ECMP_OrdenCompraDetalle>()).ToList();
                string[] parametro;
                string Monto = Convert.ToDecimal(txtTotalNeto.Text).ToString();
                parametro = new string[]
                {
                    "prmRazonSocial|"       + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.RazonSocial,
                    "prmRuc|"               + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.Ruc,
                    "prmDireccionEmpresa|"  + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.DireccionFiscal,
                    "prmTelefonoEmpresa|"   + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.Telefono,
                    "prmFechaDocumento|"    + ObjECMP_OrdenCompra.Fecha.ToShortDateString(),
                    "prmFechaLetra|"        + DateTime.Now.ToLongDateString().Split(',').ElementAt(1),
                    "prmNumOrden|"          + ObjECMP_OrdenCompra.Serie + " - " + ObjECMP_OrdenCompra.Numero,
                    "prmProveedor|"         + ObjECMP_OrdenCompra.ObjEMNF_ClienteProveedor.RazonSocial,
                    "prmRucProveedor|"      + ObjECMP_OrdenCompra.ObjEMNF_ClienteProveedor.NroDocIdentidad,
                    "prmDireccionProv|"     + ObjECMP_OrdenCompra.ObjEMNF_ClienteProveedor.Direccion,
                    "prmCondicionPago|"     + ObjECMP_OrdenCompra.ObjESGC_FormaPago.FormaPago,
                    "prmLugarEntrega|"      + txtLugarEntrega.Text,
                    "prmIgv|"               + ObjECMP_OrdenCompra.ObjESGC_Moneda.Simbolo + " "+txtTotalIgv.Text,
                    "prmNetoLetra|"         + NumLetras.Convertir(Monto,true,ObjECMP_OrdenCompra.ObjESGC_Moneda.Descripcion),
                    "prmNeto|"              + ObjECMP_OrdenCompra.ObjESGC_Moneda.Simbolo + " "+txtTotalNeto.Text,
                    "prmExonerado|"         + ObjECMP_OrdenCompra.ObjESGC_Moneda.Simbolo + " "+lblExonerado.Text,  
                    "prmIgvText|"           + lblTitleOrdenCompra04.Text,
                    "prmGravada|"           + ObjECMP_OrdenCompra.ObjESGC_Moneda.Simbolo + " "+txtGravada.Text,
                    "prmSucursal|"          + ObjECMP_OrdenCompra.ObjESGC_EmpresaSucursal.Sucursal,
                    "prmDireccionSucursal|" + txtLugarEntrega.Text,
                    "prmFechaEntrega|"      + ObjECMP_OrdenCompra.FechaEntrega.ToShortDateString(),
                    "prmCreadopor|"         + ObjECMP_OrdenCompra.Creacion,
                    "prmMoneda|"            + ObjECMP_OrdenCompra.ObjESGC_Moneda.Descripcion,
                    "prmAprobadorpor|"      + string.Empty
                };

                MainRerport ObjMainRerport = new MainRerport();
                ObjMainRerport.InitializeMainRerport("CMP.Reports.Files.RptOrdenCompra.rdlc", "dtsOrdenCompra", vrObjListECMP_OrdenCompraDetalle, parametro);
                ObjMainRerport.ShowDialog();

            }
            catch (Exception ex)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, ex.Message, CmpButton.Aceptar);
            }
        }

        #endregion
		
    }
}
