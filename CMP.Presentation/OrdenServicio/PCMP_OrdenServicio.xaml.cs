namespace CMP.Presentation.OrdenServicio
{
    using ALM.Business;
    using CMP.Business;
    using CMP.Entity;
    using CMP.Presentation.Method;
    using CMP.Presentation.OrdenServicio.Flyouts;
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
    using MNF.Presentation.ClienteProveedor.Flyouts;
    using MNF.Presentation.Servicio.Flyouts;
    using SGC.Empresarial.Business;
    using SGC.Empresarial.Entity;
    using SGC.Empresarial.Method;
    using SGC.Empresarial.Presentation.MasterTable.Empresa;
    using SGC.Empresarial.Presentation.MasterTable.Empresa.Flyouts;
    using SGC.Empresarial.Useful;
    using SGC.Empresarial.Useful.Modulo;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public partial class PCMP_OrdenServicio : UserControl, CmpINavigation
    {
        #region INSTANCIAS, VARIABLES, EVENTOS AGREGADOS

        private ECMP_OrdenServicio ObjECMP_OrdenServicio;
        private NumLetra   NumLetras = new NumLetra(); 
        private ESGC_UsuarioEmpresaSucursal ObjESGC_UsuarioEmpresaSucursal;
        private EMNF_ClienteProveedor ObjEMNF_ClienteProveedor;
        private EMNF_TipoDestino ObjEMNF_TipoDestino;
        private ESGC_Moneda ObjESGC_Moneda;
        private ESGC_Estado ObjESGC_Estado;
        private ESGC_FormaPago ObjESGC_FormaPago;
        private ESGC_Area ObjESGC_Area;

        public bool imprimir { get; set; }
        private decimal dmlIGV = 0;
        private decimal dmlRentaSegunda = 0;
        private string strNetoText = string.Empty;
        private List<EMNF_ClienteProveedor> ListEMNF_ClienteProveedor;
        private List<ECMP_ValueComboBox> ListPeriodoCapania;

        #endregion

        #region EVENTOS DE LA CLASE

        public PCMP_OrdenServicio()
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
                if (!(value is ECMP_OrdenServicio)) return;

                var ObjECMP_OrdenServicio = (ECMP_OrdenServicio)value;

                ListEMNF_ClienteProveedor = new List<EMNF_ClienteProveedor>();
                ListPeriodoCapania = new List<ECMP_ValueComboBox>();

                this.KeyDownCmpButtonTitleTecla(
                                                    ActionF10: btnAgregarServicioIsClicked,
                                                    ActionF11: btnQuitarArticuloIsClicked,
                                                    ActionF3: btnCancelarIsClicked,
                                                    ActionF12: btnGuardarIsClicked,
                                                    ActionF9: btnImprimirIsClicked,
                                                    ActionESC: btnSalirIsClicked);

                AddFlyout();
                ClearControl();

                btnImprimir.IsEnabled = (ObjECMP_OrdenServicio.Opcion == "U");
				cbxEstado.IsEnabled = false;
                this.ObjECMP_OrdenServicio = ObjECMP_OrdenServicio;
                this.ObjEMNF_ClienteProveedor = ObjECMP_OrdenServicio.ObjEMNF_ClienteProveedor;
                if (ObjECMP_OrdenServicio.Opcion == "U")
                    dtpFechaServicio.SelectedDate = ObjECMP_OrdenServicio.Fecha;
                LoadHeader();
                MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminCompra, CMPMensajes.GetAccesoRestringidoNull("Orden Servicio"), new Action<ESGC_PermisoPerfil>((P) =>
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
                        CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, strOutMessageError, CmpButton.Aceptar);
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
            LoadDataInSucursal(ObjESGC_UsuarioEmpresaSucursal);

            if (ObjESGC_UsuarioEmpresaSucursal == null)
                return;
            LoadTipoDestino();
        }

        private void cbxMoneda_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            LoadSelRateTipoCambio();
        }

        private void dtpFechaServicio_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
        {            
            if (dtpFechaServicio.SelectedDate != null && cbxPeriodo.SelectedValue != null)
            {
                int intAnio = Convert.ToInt32(cbxPeriodo.SelectedValue.ToString().Substring(0, 4));
                int intMes = Convert.ToInt32(cbxPeriodo.SelectedValue.ToString().Substring(4, 2));

                if (dtpFechaServicio.SelectedDate.Value.ToString("yyyyMM") != cbxPeriodo.SelectedValue.ToString())
                {
					var vrFechaTemp = new DateTime(((intMes == 12) ? intAnio++ : intAnio), ((intMes != 12) ? (intMes + 1) : 1), 1);
                    dtpFechaServicio.SelectedDate = vrFechaTemp.AddDays(-1);
                }
            }
            LoadSelRateTipoCambio();
			LoadTipoDestino();
            if (dtpFechaServicio.SelectedDate != null)
            {
                dtpFechaInicio.DisplayDateStart = dtpFechaServicio.SelectedDate.Value;
                dtpFechaFin.DisplayDateStart = dtpFechaServicio.SelectedDate.Value;

                dtpFechaInicio.SelectedDate = dtpFechaServicio.SelectedDate.Value;
                dtpFechaFin.SelectedDate = dtpFechaServicio.SelectedDate.Value;
            }

            var vrFecha = new DateTime();
            vrFecha = (dtpFechaServicio.SelectedDate) ?? ObjECMP_OrdenServicio.Fecha;

            if (vrFecha == null) { return; }

            if (dtpFechaServicio.SelectedDate != null)
                vrFecha = dtpFechaServicio.SelectedDate.Value;
            
        }

        private void dtpFechaInicio_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (dtpFechaFin.SelectedDate != null && dtpFechaInicio.SelectedDate != null)
            {
                if (dtpFechaInicio.SelectedDate.Value > dtpFechaFin.SelectedDate.Value)
                    dtpFechaFin.SelectedDate = dtpFechaInicio.SelectedDate.Value;
            }
        }
       
        private void cbxTipoDestino_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            LoadTipoDestino();
        }

        private void cbxPeriodo_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
			int AnioActual = 0;
            if((EMNF_Periodo)((ComboBox)sender).SelectedItem != null)
                AnioActual = Convert.ToInt32(((EMNF_Periodo)((ComboBox)sender).SelectedItem).Periodo.ToString().Substring(0, ObjECMP_OrdenServicio.Periodo.Length - 2));
            int AnioTemp = Convert.ToInt32(ObjECMP_OrdenServicio.Periodo.Substring(0, ObjECMP_OrdenServicio.Periodo.Length - 2));
            if (cbxPeriodo.SelectedItem == null) return;
            string strPeriodo = ((EMNF_Periodo)cbxPeriodo.SelectedItem).Periodo;
            ObjECMP_OrdenServicio.Periodo = strPeriodo;
			if (AnioActual != AnioTemp)
                LoadTipoDestino();
            MDatePicker.DateStartToDateEnd(dtpFechaServicio, strPeriodo, true);
        }

        private void cbxEstado_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ObjESGC_Estado = (ESGC_Estado)cbxEstado.SelectedItem;
        }

        private void cbxFormaPago_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ObjESGC_FormaPago = (ESGC_FormaPago)cbxFormaPago.SelectedItem;
            ObjECMP_OrdenServicio.ObjESGC_FormaPago = ObjESGC_FormaPago;
        }

        private void cbxArea_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ObjESGC_Area = (ESGC_Area)cbxArea.SelectedItem;
        }

        private void ClickCheckBox(object sender, System.Windows.RoutedEventArgs e)
        {
            var ListECMP_OrdenServicioDetalle = (dgDetalleServicio.Items.OfType<ECMP_OrdenServicioDetalle>()).ToList();
            if (((CheckBox)sender).Name == "rbExonerado")
            {
                rbIncluidoIGV.IsChecked = false;
                chkAplicarRetencion.IsChecked = (chkAplicarRetencion.IsEnabled);
                if (!((CheckBox)sender).IsChecked.Value)
                {
                    if (ListECMP_OrdenServicioDetalle.Count == 0) return;

                    var First = ListECMP_OrdenServicioDetalle.FirstOrDefault();
                    ListECMP_OrdenServicioDetalle = ListECMP_OrdenServicioDetalle.Where(x => x.ObjEMNF_Servicio.ObjEMNF_OperacionIGV.CodOperacionIGV == First.ObjEMNF_Servicio.ObjEMNF_OperacionIGV.CodOperacionIGV).ToList();
                    dgDetalleServicio.Items.Clear();
                    foreach (var item in ListECMP_OrdenServicioDetalle)
                    {
                        dgDetalleServicio.Items.Add(item);
                    };
					dgDetalleServicio.Columns[5].Header = "IGV";
                }
				else
					dgDetalleServicio.Columns[5].Header = "Retención";
            }
            else if (((CheckBox)sender).Name == "chkAplicarRetencion")
            {
                rbIncluidoIGV.IsChecked = false;
                CalcularTotales();
            }
            else if (((CheckBox)sender).Name == "rbIncluidoIGV")
            {
				dgDetalleServicio.Columns[5].Header = "IGV";
                rbExonerado.IsChecked = false;
                chkAplicarRetencion.IsChecked = false;
            }

            if(ObjECMP_OrdenServicio.Opcion=="U")
            {
                dmlIGV = (rbExonerado.IsChecked.Value) ? ((ObjECMP_OrdenServicio.Exonerado != 21) ? (SGCMethod.GetTasaHonorario(ObjECMP_OrdenServicio.Gravada) / 100) : dmlIGV) : (SGCVariables.ObjESGC_Retencion.IGV / 100);
            }

            foreach (var item in ListECMP_OrdenServicioDetalle)
            {
                CalcularTotalesItems(item);
            }
        }
        
        private void btnAdministrarSucursalIsClicked(object sender, System.Windows.RoutedEventArgs e)
        {
            MSGC_UpdatePrivilege.Process(this, "SGC", CMPMensajes.TitleAdminOrdenServicio, CMPMensajes.GetAccesoRestringidoNull("Sucursal"), new Action<ESGC_PermisoPerfil>((P) =>
            {
                new PSGC_ShowListadoEmpresa(P).ShowDialog();
                LoadHeader();
                ObjESGC_UsuarioEmpresaSucursal = null;
            }), "PSGC_ListadoEmpresa");
        }

        private void npdCantidad_LostFocus_1(object sender, System.Windows.RoutedEventArgs e)
        {
            var vrObjECMP_OrdenServicioDetalle = (ECMP_OrdenServicioDetalle)dgDetalleServicio.CurrentCell.Item;
            if (vrObjECMP_OrdenServicioDetalle == null) return;
			vrObjECMP_OrdenServicioDetalle.PrecioUnitarioTemp = (!rbIncluidoIGV.IsChecked.Value) ? vrObjECMP_OrdenServicioDetalle.PrecioUnitario : (vrObjECMP_OrdenServicioDetalle.PrecioUnitario * ((decimal.Round(dmlIGV * 100, 2) + 100) / 100));
            CalcularTotalesItems(vrObjECMP_OrdenServicioDetalle);
        }

        private void dtgdgDetalleServicioObervacion_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            var vrObjECMP_OrdenServicioDetalle = (ECMP_OrdenServicioDetalle)dgDetalleServicio.CurrentCell.Item;
            if (vrObjECMP_OrdenServicioDetalle == null) { return; }
            this.FlyoutIsOpen("Obervaciones", ((value) =>
            {
                if (value is Obervaciones)
                {
                    var MyObervaciones = (Obervaciones)value;
                    MyObervaciones.InitializeObervaciones(vrObjECMP_OrdenServicioDetalle);
                    MyObervaciones.IsOpen = true;
                }
            }));
        }

        private string strTempValueTitle = string.Empty;
        private bool blIsGravada;

		private MtdCalculosTotales MyMtdCalculosTotales = new MtdCalculosTotales();
        private void CalCularPreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                #region VALIDACION
                    string message = string.Empty;
                    if (dgDetalleServicio.Items.Count <= 0)
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, "Ingrese Servicio para poder editar este campo.", CmpButton.Aceptar);
                        return;
                    }
                    else
                    {
                        var vrObjListECMP_OrdenServicioDetalle = new List<ECMP_OrdenServicioDetalle>();
                        vrObjListECMP_OrdenServicioDetalle = (dgDetalleServicio.Items.OfType<ECMP_OrdenServicioDetalle>()).ToList();
                        vrObjListECMP_OrdenServicioDetalle.ForEach((f) =>
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
                MyMtdCalculosTotales.Calcular(txtGravada, txtImporteIGV, blIsGravada, () =>
                {
                    if (blIsGravada)
                    {
                        txtGravada.IsReadOnly = false;
                        strTempValueTitle = lblTitleOrdenServicio02.Text;
                        lblTitleOrdenServicio02.Text += " (Enter)";
                        lblTitleOrdenServicio02.Foreground = Brushes.Orange;
                    }
                    else
                    {
                        txtImporteIGV.IsReadOnly = false;
                        strTempValueTitle = lblTitleOrdenServicio03.Text;
                        lblTitleOrdenServicio03.Text += " (Enter)";
                        lblTitleOrdenServicio03.Foreground = Brushes.Orange;
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
                if (txtImporteIGV.Text.Contains(" "))
                {
                    txtImporteIGV.Text = txtImporteIGV.Text.Replace(" ", "");
                }
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    if (blIsGravada)
                    {
                        lblTitleOrdenServicio02.Text = strTempValueTitle;
                        lblTitleOrdenServicio02.Foreground = Brushes.White;
                        txtTotal.Focus();
                        txtImporteIGV.IsReadOnly = false;
                    }
                    else
                    {
                        lblTitleOrdenServicio03.Text = strTempValueTitle;
                        lblTitleOrdenServicio03.Foreground = Brushes.White;
                        txtTotal.Focus();
                        txtGravada.IsReadOnly = false;
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
                indexOfTitleIgv = lblTitleOrdenServicio03.Text.IndexOf("(");
                if (indexOfTitleIgv > 0)
                {
                    strTempValueTitle = lblTitleOrdenServicio03.Text.Substring(0, indexOfTitleIgv - 1);
                    lblTitleOrdenServicio03.Text = strTempValueTitle;
                }
                indexOfTitleGravada = lblTitleOrdenServicio02.Text.IndexOf("(");
                if (indexOfTitleGravada > 0)
                {
                    strTempValueTitle = lblTitleOrdenServicio02.Text.Substring(0, indexOfTitleGravada - 1);
                    lblTitleOrdenServicio02.Text = strTempValueTitle;
                }
                txtGravada.IsReadOnly = true;
                txtImporteIGV.IsReadOnly = true;

            #endregion

            blIsGravada = (((TextBox)sender).Name == "txtGravada");
            if (blIsGravada)
            {
                strTempValueTitle = lblTitleOrdenServicio02.Text;
                lblTitleOrdenServicio02.Text = strTempValueTitle;
                lblTitleOrdenServicio02.Foreground = Brushes.White;
                MyMtdCalculosTotales.Calcular(txtGravada, txtImporteIGV, blIsGravada, () =>
                {
                    txtGravada.IsReadOnly = true;
                });
            }
            else
            {
                strTempValueTitle = lblTitleOrdenServicio03.Text;
                lblTitleOrdenServicio03.Text = strTempValueTitle;
                lblTitleOrdenServicio03.Foreground = Brushes.White;
                MyMtdCalculosTotales.Calcular(txtGravada, txtImporteIGV, blIsGravada, () =>
                {
                    txtTotal.IsReadOnly = true;
                });
            }
            txtTotal.Focus();
            if (rbExonerado.IsChecked.Value)
                MyMtdCalculosTotales.NewValue(txtGravada, txtImporteIGV, Convert.ToDecimal(txtTotal.Text), SumarOrRestarGravadaTotalIGV.RESTAR);
            else
                MyMtdCalculosTotales.NewValue(txtGravada, txtImporteIGV, Convert.ToDecimal(txtTotal.Text));

            if (chkAplicarRetencion.IsChecked.Value || rbExonerado.IsChecked.Value)
                txtTotal.Text = (Convert.ToDecimal(txtGravada.Text) - Convert.ToDecimal(txtImporteIGV.Text)).ToString("###,###,##0.#0");
            else
                txtTotal.Text = (Convert.ToDecimal(txtGravada.Text) + Convert.ToDecimal(txtImporteIGV.Text)).ToString("###,###,##0.#0");
        }

        public void btnAgregarServicioIsClicked()
        {
            if (!btnAgregarServicio.IsEnabled)
                return;

            OperacionIGV MyOperacionIGV = OperacionIGV.Todo;
            if (rbIncluidoIGV.IsChecked.Value)
                MyOperacionIGV = OperacionIGV.Gravada;
            else if (rbExonerado.IsChecked.Value)
                MyOperacionIGV = OperacionIGV.Todo;
            //else if (rbExoneradoIGV.IsChecked.Value)
            //    MyOperacionIGV = OperacionIGV.Exonerada;

            MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminOrdenServicio, CMPMensajes.GetAccesoRestringidoNull("Servicios"), new Action<ESGC_PermisoPerfil>((P) =>
            {
                if (P.Consulta)
                {
                    this.FlyoutIsOpen("PMNF_BuscarServicios", ((value) =>
                    {
                        if (value is PMNF_BuscarServicios)
                        {
                            var FlyoutsPMNF_BuscarServicios = (PMNF_BuscarServicios)value;
                            FlyoutsPMNF_BuscarServicios.InitializePMNF_BuscarServicios();
                            FlyoutsPMNF_BuscarServicios.LoadHeader(MyOperacionIGV);
                            FlyoutsPMNF_BuscarServicios.IsOpen = true;
                        }
                    }));
                }
                else
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, CMPMensajes.GetAccesoRestringidoBuscar("Servicios"), CmpButton.Aceptar);
                }
            }));
        }

        public void btnCancelarIsClicked()
        {
            if (!btnAnularProceso.IsEnabled)
                return;

            CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, CMPMensajes.PreguntaContinuarProceso, CmpButton.AceptarCancelar, () =>
            {
                ClearControl();
                Parameter = new ECMP_OrdenServicio(null, TipoConstructor.Insert);
            });
        }

        public void btnGuardarIsClicked()
        {
            if (!btnGuardar.IsEnabled)
                return;

            MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminOrdenServicio, CMPMensajes.GetAccesoRestringidoNull("Orden Servicio"), new Action<ESGC_PermisoPerfil>((P) =>
            {
                if (P.Nuevo || P.Editar)
                {
                    if (ValidaDatos()) { return; }
                    var varObjECMP_OrdenServicio = (ECMP_OrdenServicio)MyHeader.DataContext;
                    varObjECMP_OrdenServicio.ObjESGC_EmpresaSucursal = ObjESGC_UsuarioEmpresaSucursal.ObjESGC_EmpresaSucursal;
                    varObjECMP_OrdenServicio.ObjEMNF_ClienteProveedor = ObjEMNF_ClienteProveedor;
                    varObjECMP_OrdenServicio.ObjESGC_Moneda = ObjESGC_Moneda;
                    varObjECMP_OrdenServicio.ObjESGC_Estado = ObjESGC_Estado;
                    varObjECMP_OrdenServicio.ObjEMNF_TipoDestino = (EMNF_TipoDestino)cbxTipoDestino.SelectedItem;
                    varObjECMP_OrdenServicio.ObjESGC_FormaPago = ObjESGC_FormaPago;
                    varObjECMP_OrdenServicio.ObjESGC_Area = ObjESGC_Area;
                    varObjECMP_OrdenServicio.Gravada = Convert.ToDecimal(txtGravada.Text);
                   
                    if (rbExonerado.IsChecked.Value)
                    {
                        //Si es un Recibo por Honorario
                        varObjECMP_OrdenServicio.IGV = (chkAplicarRetencion.IsChecked.Value) ? (SGCMethod.GetTasaHonorario(ObjECMP_OrdenServicio.Gravada) / 100) : 0;
                    }
                    else if(chkAplicarRetencion.IsChecked.Value)
                    {
                        varObjECMP_OrdenServicio.IGV = dmlRentaSegunda;
                    }
                    else
                    {
                        varObjECMP_OrdenServicio.IGV = dmlIGV;
                    }                        

                    varObjECMP_OrdenServicio.ImporteIGV = Convert.ToDecimal(txtImporteIGV.Text);

                    if (ObjEMNF_TipoDestino.CodTipoDestino != "CTDIS" && ObjEMNF_TipoDestino.CodTipoDestino != "CTSUC" && dgDetalleServicio.Items.OfType<ECMP_OrdenServicioDetalle>().ToList().Exists(x => x.IdDestino == 0)) //Cuenta por Distribuir General
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, "Se requiere seleccionar " + ObjEMNF_TipoDestino.TipoDestino + " del los registro del detalle.", CmpButton.Aceptar);
                        return;
                    }

                    varObjECMP_OrdenServicio.CadenaXML = XmlDetalleOrdenServicio();

                    if (!rbExonerado.IsChecked.Value && !rbIncluidoIGV.IsChecked.Value)
                    {
                        varObjECMP_OrdenServicio.Exonerado = 11;
                    }
                    if (!rbExonerado.IsChecked.Value && rbIncluidoIGV.IsChecked.Value)
                    {
                        varObjECMP_OrdenServicio.Exonerado = 12;
                    }
                    if (rbExonerado.IsChecked.Value && !rbIncluidoIGV.IsChecked.Value)
                    {
                        varObjECMP_OrdenServicio.Exonerado = 21;
                    }
                    varObjECMP_OrdenServicio.Retencion = chkAplicarRetencion.IsChecked.Value;

                    var ListECMP_OrdenServicioDetalle = (dgDetalleServicio.Items.OfType<ECMP_OrdenServicioDetalle>()).ToList(); 
                    var First = ListECMP_OrdenServicioDetalle.FirstOrDefault();

                    if (First.ObjEMNF_Servicio.ObjEMNF_OperacionIGV.CodOperacionIGV == "EX" && !rbExonerado.IsChecked.Value)
                    {
                        varObjECMP_OrdenServicio.Exonerado = 22;
                    }


                    string strOutMessageError = string.Empty;
                    CmpMessageBox.Proccess(CMPMensajes.TitleAdminOrdenServicio, CMPMensajes.ProcesandoDatos, () =>
                    {
                        try
                        {
                            new BCMP_OrdenServicio().TransOrdenServicio(varObjECMP_OrdenServicio);
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
                            CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, strOutMessageError, CmpButton.Aceptar);
                        }
                        else
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, CMPMensajes.DatoProcesados + "\n¿Desea Imprimir el documento de Orden de Servicio?", CmpButton.AceptarCancelar, () => 
                            {

                                ImprimirOrdenServicio();
                                btnSalirIsClicked();
                            }
                            , () => 
                            {
                                btnSalirIsClicked();
                            });
                        }
                    });
                }
                else
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, CMPMensajes.GetAccesoRestringidoNuevo("Orden Servicio"), CmpButton.Aceptar);
                }
            }));
        }

        public void btnQuitarArticuloIsClicked()
        {
            if (!btnQuitarArticulo.IsEnabled)
                return;

            if (dgDetalleServicio.Items.Count <= 0) return;
			if (!(dgDetalleServicio.CurrentCell.Item is ECMP_OrdenServicioDetalle)) return;
            var resutl = (ECMP_OrdenServicioDetalle)dgDetalleServicio.CurrentCell.Item;
            if (resutl == null) { return; }

            CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, CMPMensajes.QuitarItems, CmpButton.AceptarCancelar, () =>
            {
                dgDetalleServicio.Items.Remove(resutl);
                CalcularTotales();
                var ObjListEIngresoSalidaDetalle = (dgDetalleServicio.Items.OfType<ECMP_OrdenServicioDetalle>()).ToList();
                dgDetalleServicio.Items.Clear();

                int intItem = 1;
                foreach (var item in ObjListEIngresoSalidaDetalle)
                {
                    item.Item = intItem;
                    dgDetalleServicio.Items.Add(item);
                    intItem++;
                }
            });
        }

        public void btnImprimirIsClicked()
        {
            if (!btnImprimir.IsEnabled)
                return;

            ImprimirOrdenServicio();
        }

        public void btnSalirIsClicked()
        {
            if (!btnSalir.IsEnabled)
                return;

            MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminCompra, CMPMensajes.GetAccesoRestringidoNull("Orden Servicio"), ((P) =>
            {
                new CmpNavigationService().Volver(_MyFrame, new ECMP_OrdenServicio());
            }),
            MyNameFomulario: "PCMP_ListadoOrdenServicio",
            MyActionAbort: () =>
            {
                this.Close(TipoModulo.ManuFactura);
            });
        }

        #endregion

        #region MÉTODOS Y FUNCIONES

        /// <summary>
        /// Agregacion lista de Flyout
        /// </summary>
        private void AddFlyout()
        {
            #region BUSCAR PROVEEDOR

            PMNF_BuscarClienteProveedor MyPMNF_BuscarClienteProveedor = new PMNF_BuscarClienteProveedor();
            MyPMNF_BuscarClienteProveedor.IsSelected += new PMNF_BuscarClienteProveedor.isSelected((value) => { AddValueProveedor(value); });

            #endregion

            #region AGREGAR Servicios

            PMNF_BuscarServicios MyPMNF_BuscarServicios = new PMNF_BuscarServicios();
            MyPMNF_BuscarServicios.IsSelected += new PMNF_BuscarServicios.isSelected((value) => { AddItemsServicio(value); });

            #endregion

            #region ADMINISTRAR SUCURSAL

            PSGC_EmpresaSucursal MyPSGC_EmpresaSucursal = new PSGC_EmpresaSucursal();

            #endregion

            #region Observaciones

            Obervaciones MyObervaciones = new Obervaciones();

            #endregion

            this.FlyoutInitialize();
            this.FlyoutAdd(MyPMNF_BuscarClienteProveedor);
            this.FlyoutAdd(MyPMNF_BuscarServicios);
            this.FlyoutAdd(MyPSGC_EmpresaSucursal);
            this.FlyoutAdd(MyObervaciones);
        }

        /// <summary>
        /// Instancia y pinta valor del Proveedor seleccionado de la busqueda
        /// </summary>
        /// <param name="ObjEMNF_ClienteProveedor">Objeto de la clase Proveedor</param>
        private void AddValueProveedor(EMNF_ClienteProveedor ObjEMNF_ClienteProveedor) 
        {
            if (ObjEMNF_ClienteProveedor != null)
            {
                this.ObjEMNF_ClienteProveedor = ObjEMNF_ClienteProveedor;
                txtProveedorRazonSocial.Text = ObjEMNF_ClienteProveedor.RazonSocial;
                txtContacto.Text = ObjEMNF_ClienteProveedor.Contacto;
                cbxFormaPago.SelectedValue = ObjEMNF_ClienteProveedor.ObjEMNF_FormaPago.IdFormaPago;
            }
        }

        /// <summary>
        /// Instancia y pinta valor del servicio seleccionado de la busqueda
        /// </summary>
        /// <param name="ObjEMNF_Servicio">Objeto de la clase Servicio</param>
        private void AddItemsServicio(EMNF_Servicio ObjEMNF_Servicio)
        {
            var ListECMP_OrdenServicioDetalle = (dgDetalleServicio.Items.OfType<ECMP_OrdenServicioDetalle>()).ToList();
            if (ObjEMNF_Servicio.ObjEMNF_OperacionIGV.CodOperacionIGV == "GB")
            {
                lblTitleOrdenServicio02.Text = "Gravada";
            }
            else
            {
                lblTitleOrdenServicio02.Text = "Exonerado";
            }
            if (ListECMP_OrdenServicioDetalle.Count > 0 && !rbExonerado.IsChecked.Value)
            {
                var First = ListECMP_OrdenServicioDetalle.FirstOrDefault();
                
                if (ObjEMNF_Servicio.ObjEMNF_OperacionIGV.CodOperacionIGV != First.ObjEMNF_Servicio.ObjEMNF_OperacionIGV.CodOperacionIGV)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, "La operación de IGV del " + ObjEMNF_Servicio.Servicio + " no es igual al registro ya seleccionado anteriormente.", CmpButton.Aceptar);
                    return;
                }
            }

            var vrObjECMP_OrdenServicioDetalle = new ECMP_OrdenServicioDetalle
            {
                Item = ListECMP_OrdenServicioDetalle.Count + 1,
                ListPeriodoCampania = ListPeriodoCapania,
                ObjEMNF_Servicio = new EMNF_Servicio()
                {
                    IdServicio = ObjEMNF_Servicio.IdServicio,
                    Codigo = ObjEMNF_Servicio.Codigo,
                    Servicio = ObjEMNF_Servicio.Servicio,
                    ObjEMNF_OperacionIGV = ObjEMNF_Servicio.ObjEMNF_OperacionIGV
                },
            };
            LoadTipoDestino();
            dgDetalleServicio.Items.Add(vrObjECMP_OrdenServicioDetalle);
            //CalcularTotales();
            btnQuitarArticulo.IsEnabled = (dgDetalleServicio.Items.Count > 0);
        }

        /// <summary>
        /// Carga datos para la administración de orden de compra
        /// </summary>
        private void LoadHeader() 
        {
            var vrListESGC_UsuarioEmpresaSucursal = new List<ESGC_UsuarioEmpresaSucursal>();
            var vrListESGC_Moneda = new List<ESGC_Moneda>();
            var vrListESGC_FormaPago = new List<ESGC_FormaPago>();
            var vrObjListArea = new List<ESGC_Area>();
            var vrListESGC_FormularioSetting = new List<ESGC_FormularioSetting>();
            var vrListEMNF_Periodo = new List<EMNF_Periodo>();
            var ListESGC_Estado = new List<ESGC_Estado>();
            var vrListTipoDestino = new ObservableCollection<EMNF_TipoDestino>();

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
                    ListESGC_Estado = new BSGC_Estado().ListFiltrarEstado(SGCMethod.GetNameNameTableInXaml(this));
                    vrListESGC_FormaPago = new BSGC_FormaPago().ListGetFormaPago();
                    vrObjListArea = new BSGC_Area().ListGetArea();
                    vrListTipoDestino = new BMNF_TipoDestino().CollectionTipoDestino();

                    if (ObjECMP_OrdenServicio.Opcion == "I")
                    {
                        vrListESGC_FormularioSetting = new BSGC_FormularioSetting().ListGetFormularioSetting(this.GetType().Name);
                        this.Dispatcher.Invoke(() =>
                        {
                            MDatePicker.DateStartToDateEnd(dtpFechaServicio, strPeriodoActual, true);
                            ObjECMP_OrdenServicio.Fecha = dtpFechaServicio.SelectedDate.Value;
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
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, strOutMessageError, CmpButton.Aceptar);
                }
                else
                {
                    cbxUsuarioEmpresaSucursal.ItemsSource = vrListESGC_UsuarioEmpresaSucursal;
                    cbxPeriodo.ItemsSource = (ObjECMP_OrdenServicio.Opcion == "I") ? vrListEMNF_Periodo.Where(x => x.Estado == "A") : vrListEMNF_Periodo;
                    cbxMoneda.ItemsSource = vrListESGC_Moneda;
                    cbxFormaPago.ItemsSource = vrListESGC_FormaPago;
                    cbxArea.ItemsSource = vrObjListArea;
                    cbxTipoDestino.ItemsSource = vrListTipoDestino;

                    if (ObjECMP_OrdenServicio.Opcion == "I")
                    {
						cbxUsuarioEmpresaSucursal.SelectedItem = vrListESGC_UsuarioEmpresaSucursal.FirstOrDefault();
                        dmlIGV = SGCVariables.ObjESGC_Retencion.IGV / 100;
                        dmlRentaSegunda = SGCVariables.ObjESGC_Retencion.PjeRentaSegunda / 100;
                        ObjECMP_OrdenServicio.Periodo = strPeriodoActual;
                        AddValueDefault(vrListESGC_FormularioSetting);
                        cbxEstado.ItemsSource = ListESGC_Estado.Where(x => x.Campo == "CodEstado");
                        chkAplicarRetencion.IsEnabled = true;
						cbxPeriodo.SelectedItem = vrListEMNF_Periodo.FirstOrDefault(x => x.Activo == 1);
                    }
                    else
                    {
                        cbxEstado.ItemsSource = ListESGC_Estado;
                        cbxPeriodo.SelectedItem = vrListEMNF_Periodo.FirstOrDefault(x => x.Periodo == ObjECMP_OrdenServicio.Periodo);
                        cbxMoneda.IsEnabled = !(ObjECMP_OrdenServicio.ObjESGC_Estado.CodEstado == "PECOS" || ObjECMP_OrdenServicio.ObjESGC_Estado.CodEstado == "APCOS");
                        dmlIGV = ObjECMP_OrdenServicio.IGV;
                        dmlRentaSegunda = (ObjECMP_OrdenServicio.Retencion) ? ObjECMP_OrdenServicio.IGV : SGCVariables.ObjESGC_Retencion.PjeRentaSegunda;
                        if (ObjECMP_OrdenServicio.Exonerado == 11)
                        {
                            rbExonerado.IsChecked = false;
                            rbIncluidoIGV.IsChecked = false;
                        }
                        else if (ObjECMP_OrdenServicio.Exonerado == 12)
                        {
                            rbExonerado.IsChecked = false;
                            rbIncluidoIGV.IsChecked = true;
                        }
                        else if (ObjECMP_OrdenServicio.Exonerado == 21)
                        {
                            rbExonerado.IsChecked = true;
                            rbIncluidoIGV.IsChecked = false;
                        }
                        else if (ObjECMP_OrdenServicio.Exonerado == 22)
                        {
                            rbIncluidoIGV.IsChecked = false;
                            rbExonerado.IsChecked = false;
                        }

                        chkAplicarRetencion.IsChecked = ObjECMP_OrdenServicio.Retencion;

                        rbIncluidoIGV.IsEnabled = (ObjECMP_OrdenServicio.ObjESGC_Estado.CodEstado == "PECOS" || ObjECMP_OrdenServicio.ObjESGC_Estado.CodEstado == "APCOS") ? true : false;
                        rbExonerado.IsEnabled = (ObjECMP_OrdenServicio.ObjESGC_Estado.CodEstado == "PECOS" || ObjECMP_OrdenServicio.ObjESGC_Estado.CodEstado == "APCOS") ? true : false;
                    }

                    lblTitleOrdenServicio03.Text = "IGV " + decimal.Round(dmlIGV * 100, 2) + "%";
                    

                    MyHeader.DataContext = null;
                    MyHeader.DataContext = ObjECMP_OrdenServicio;

                    LoadDetail();
                }
            });
        }

        /// <summary>
        /// Carga datos del orden de compra
        /// </summary>
        private void LoadDetail()
        {
            dgDetalleServicio.Items.Clear();
            var vrListECMP_OrdenServicioDetalle = new List<ECMP_OrdenServicioDetalle>();

            string strOutMessageError = string.Empty;
            CmpTask.Process(
            () =>
            {
                try
                {
                    if (ObjECMP_OrdenServicio.Opcion == "U")
                        vrListECMP_OrdenServicioDetalle = new BCMP_OrdenServicioDetalle().ListAdministrarOrdenServicioDetalle(ObjECMP_OrdenServicio);
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
                    if (ObjECMP_OrdenServicio.ImporteIGV != 0)
                        lblTitleOrdenServicio02.Text = "Gravada";
                    else
                        lblTitleOrdenServicio02.Text = "Exonerado";
                    foreach (var item in vrListECMP_OrdenServicioDetalle)
                    {
                        if (ObjECMP_OrdenServicio.Exonerado == 12)
                        {
                            decimal dmlCalculoIGV = (item.PrecioUnitario * dmlIGV);
                            item.PrecioUnitario = decimal.Round(dmlCalculoIGV + item.PrecioUnitario, 2);
                        }
                        dgDetalleServicio.Items.Add(item);
                        CalcularTotalesItems(item);
                    }
					LoadTipoDestino();

                    btnQuitarArticulo.IsEnabled = (dgDetalleServicio.Items.Count > 0);

                    txtGravada.Text = decimal.Round(ObjECMP_OrdenServicio.Gravada, 2).ToString("###,###,##0.#0");
                    txtImporteIGV.Text = decimal.Round(ObjECMP_OrdenServicio.ImporteIGV, 2).ToString("###,###,##0.#0");

                    //factura
                    if (rbIncluidoIGV.IsChecked.Value)
                    {
                        //Calculo con incluir IGV
                        txtTotal.Text = (ObjECMP_OrdenServicio.Gravada + ObjECMP_OrdenServicio.ImporteIGV).ToString("###,###,##0.#0");
                    }
                    else if (rbExonerado.IsChecked.Value)
                    {
                        //Calculo cuando es Honorario
                        dgDetalleServicio.Columns[5].Header = "Retención";
                        txtTotal.Text = (ObjECMP_OrdenServicio.Gravada - ObjECMP_OrdenServicio.ImporteIGV).ToString("###,###,##0.#0");
                    }
                    else if (chkAplicarRetencion.IsChecked.Value)
                    {
                        //Calculo cuando es Honorario
                        dgDetalleServicio.Columns[5].Header = "Retención";
                        txtTotal.Text = (ObjECMP_OrdenServicio.Gravada - ObjECMP_OrdenServicio.ImporteIGV).ToString("###,###,##0.#0");
                    }
                    else
                    {
                        //Calculo sin incluir IGV
                        txtTotal.Text = (ObjECMP_OrdenServicio.Gravada + ObjECMP_OrdenServicio.ImporteIGV).ToString("###,###,##0.#0");
                    }
                }
            });
        }

        /// <summary>
        /// Carga datos de los sucursales dependiendo al sucursal
        /// </summary>
        /// <param name="ObjESGC_UsuarioEmpresaSucursal">Objeto de la entidad Usuario Empresa Sucursal</param>
        private void LoadDataInSucursal(ESGC_UsuarioEmpresaSucursal ObjESGC_UsuarioEmpresaSucursal)
        {
            if (ObjESGC_UsuarioEmpresaSucursal == null) { return; }
            var vrObjESGC_Documento = new ESGC_Documento();

            string strOutMessageError = string.Empty;
            CmpTask.Process(
            () =>
            {
                try
                {
                    vrObjESGC_Documento = new BSGC_Documento().GetNroDocumento("ODS", ObjESGC_UsuarioEmpresaSucursal.ObjESGC_EmpresaSucursal);
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
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, strOutMessageError, CmpButton.Aceptar);
                }
                else
                {
                    if (ObjECMP_OrdenServicio.Opcion == "I")
                    {
                        if (vrObjESGC_Documento.Correlativo == null || vrObjESGC_Documento.Serie == null)
                        {
                            txtCorrelativo.Text = string.Empty;
                            txtSerie.Text = string.Empty;
                            CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, "La sucursal seleccionada no cuenta con un tipo de documento configurado", CmpButton.Aceptar);
                            return;
                        }                       
                        else
                        {
                            txtCorrelativo.Text = vrObjESGC_Documento.Correlativo;
                            txtSerie.Text = vrObjESGC_Documento.Serie;
                        }
                    }
                }
            });
        }

        private async void LoadTipoDestino()
        {
            try
            {
                ObjEMNF_TipoDestino = (EMNF_TipoDestino)cbxTipoDestino.SelectedItem;
                if (ObjEMNF_TipoDestino == null)
                {
                    dtgColumnaPeriodoCompania.Visibility = System.Windows.Visibility.Collapsed;
                    dtgColumnaTipoDestino.Visibility = System.Windows.Visibility.Collapsed;
                    return;
                }

				ListPeriodoCapania.Clear();
                int Anio = Convert.ToInt32(ObjECMP_OrdenServicio.Periodo.Substring(0, ObjECMP_OrdenServicio.Periodo.Length - 2));
                for (int i = Anio - 1; i <= Anio + 1; i++)
                {
                    ListPeriodoCapania.Add(new ECMP_ValueComboBox() { Item = i.ToString(), Value = i.ToString() });
                }

                var vrListECMP_ValueComboBox = new List<ECMP_ValueComboBox>();
                if (ObjEMNF_TipoDestino.CodTipoDestino == "CTDIS" || ObjEMNF_TipoDestino.CodTipoDestino == "CTSUC") //Cuenta por Distribuir General
                {
                    dtgColumnaTipoDestino.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    dtgColumnaTipoDestino.Header = ObjEMNF_TipoDestino.TipoDestino;
                    dtgColumnaTipoDestino.Visibility = System.Windows.Visibility.Visible;

                    string strOutMessageError = string.Empty;
                    await System.Threading.Tasks.Task.Factory.StartNew(
                    () =>
                    {
                        try
                        {
                            if (ObjEMNF_TipoDestino.CodTipoDestino == "CCOST") //Categoría Centro Costo (Listado Centro de Costo)
                            {
                                var vrListCentroCosto = new BMNF_CentroCosto().ListFiltrarCentroCosto(ObjESGC_UsuarioEmpresaSucursal.ObjESGC_EmpresaSucursal);
                                foreach (var item in vrListCentroCosto)
                                {
                                    vrListECMP_ValueComboBox.Add(new ECMP_ValueComboBox() { Item = item.Descripcion, Value = item.IdCentroCosto.ToString() });
                                }
                            }
                            else if (ObjEMNF_TipoDestino.CodTipoDestino == "SCOST") //Centro Costo (Listado Sub Centro de Costo)
                            {
                                var vrListCentroCosto = new BMNF_SubCentroCosto().ListGetSubCentroCosto(ObjESGC_UsuarioEmpresaSucursal.ObjESGC_EmpresaSucursal);
                                foreach (var item in vrListCentroCosto)
                                {
                                    if (Convert.ToDateTime(item.Fecha).Date <= ObjECMP_OrdenServicio.Fecha.Date)
                                        vrListECMP_ValueComboBox.Add(new ECMP_ValueComboBox() { Item = item.Descripcion, Value = item.IdSubCenCosto.ToString() });
                                }
                            }
                            if (vrListECMP_ValueComboBox.Count == 0)
                                CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, "No hay Centro de Costo Registrado hasta la fecha " + ObjECMP_OrdenServicio.Fecha.Date.ToString("dd/MM/yyyy"), CmpButton.Aceptar);
                        }
                        catch (Exception ex)
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, ex.Message, CmpButton.Aceptar);
                        }
                    });
                }

				var vrObjListECMP_OrdenServicioDetalle = (dgDetalleServicio.Items.OfType<ECMP_OrdenServicioDetalle>()).ToList();
                foreach (var item in vrObjListECMP_OrdenServicioDetalle)
                {
                    item.ListCentroCosto = vrListECMP_ValueComboBox;
                    item.ListPeriodoCampania = ListPeriodoCapania;
                }

                dgDetalleServicio.Items.Refresh();
            }
            catch (Exception ex)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, ex.Message, CmpButton.Aceptar);
            }
        }

        /// <summary>
        /// Carga el tipo de cambio
        /// </summary>
        /// <param name="ObjBSGC_Moneda">Objeto de la referencia Moneda</param>
        private void LoadSelRateTipoCambio()
        {
            ObjESGC_Moneda = (ESGC_Moneda)cbxMoneda.SelectedItem;
            if (ObjESGC_Moneda != null)
            {
                lblTitleOrdenServicio04.Text = "Total " + ObjESGC_Moneda.Simbolo;
                if (ObjESGC_Moneda.Defecto != true && ObjECMP_OrdenServicio.ObjESGC_Estado.CodEstado == "PECOS")
                {
                    var vrFecha = new DateTime();
                    vrFecha = ObjECMP_OrdenServicio.Fecha;
                  
                    if (vrFecha == null) { return; }
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
                                CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, "No se ha registrado el tipo de cambio de la moneda extranjera para el día " + vrFecha.ToShortDateString(), CmpButton.Aceptar);
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
                        lblTitleOrdenServicio02.Text = strTempValueTitle;
                        lblTitleOrdenServicio02.Foreground = Brushes.White;
                    }
                    else
                    {

                        lblTitleOrdenServicio03.Text = strTempValueTitle;
                        lblTitleOrdenServicio03.Foreground = Brushes.White;
                    }
                }

                var ListECMP_OrdenServicioDetalle = (dgDetalleServicio.Items.OfType<ECMP_OrdenServicioDetalle>()).ToList();
                if (ListECMP_OrdenServicioDetalle != null && ListECMP_OrdenServicioDetalle.Count > 0)
                {
                    decimal dmlTotal = 0;
                    decimal dmlGravada = 0;
                    decimal dmlImporteRetencionIGV = 0;

                    var First = ListECMP_OrdenServicioDetalle.FirstOrDefault();

                    if (First.ObjEMNF_Servicio.ObjEMNF_OperacionIGV.CodOperacionIGV == "EX" && !rbExonerado.IsChecked.Value)
                    {
                        //CALCULA 22
                        dmlTotal = ListECMP_OrdenServicioDetalle.Sum(o => o.Importe);
                        dmlGravada = decimal.Round(dmlTotal, 2) / (((0 * 100) + 100) / 100);
                        dmlImporteRetencionIGV = 0;

                        lblTitleOrdenServicio02.Text = "Exonerado";
                        lblTitleOrdenServicio03.Text = "IGV " + decimal.Round(dmlIGV * 100, 2) + "%";
                        lblTitleOrdenServicio04.Text = "Total " + ObjESGC_Moneda.Simbolo;
                        strNetoText = "Importe Total";
                    }
                    else
                    {
                        #region
                        //factura
                        if (rbIncluidoIGV.IsChecked.Value)
                        {
                            //Calculo con incluir IGV
                            dmlGravada = ListECMP_OrdenServicioDetalle.Sum(x => (x.ObjEMNF_Servicio.ObjEMNF_OperacionIGV.CodOperacionIGV == "GB" ? x.Importe : 0));
                            dmlImporteRetencionIGV = ListECMP_OrdenServicioDetalle.Sum(x => (x.ObjEMNF_Servicio.ObjEMNF_OperacionIGV.CodOperacionIGV == "GB" ? x.ImporteIGV : 0));
                            dmlTotal = dmlGravada + dmlImporteRetencionIGV;
                            
                            lblTitleOrdenServicio02.Text = "Gravada";
                            lblTitleOrdenServicio03.Text = "IGV " + decimal.Round(dmlIGV * 100, 2) + "%";
                            lblTitleOrdenServicio04.Text = "Total " + ObjESGC_Moneda.Simbolo;
                            strNetoText = "Importe Total";
                        }
                        else if (rbExonerado.IsChecked.Value)
                        {
                            //Calculo cuando es Honorario
                            if (chkAplicarRetencion.IsChecked.Value)
                                ListECMP_OrdenServicioDetalle.ForEach( x => dmlGravada += x.PrecioUnitario * x.Cantidad);
                            else
                                dmlGravada = ListECMP_OrdenServicioDetalle.Sum(o => o.Importe);
                            decimal dmlTasa = 0;
                            if (chkAplicarRetencion.IsChecked.Value)
                                dmlTasa = (SGCMethod.GetTasaHonorario(dmlGravada) / 100);
                            dmlImporteRetencionIGV = dmlGravada * dmlTasa;
                            dmlTotal = dmlGravada - dmlImporteRetencionIGV;

                            lblTitleOrdenServicio02.Text = "Total Honorario";
                            lblTitleOrdenServicio03.Text = "Retención " + decimal.Round(dmlTasa * 100, 2) + "%";
                            lblTitleOrdenServicio04.Text = "Total Neto " + ObjESGC_Moneda.Simbolo;
                            strNetoText = "Total Neto";
                        }
                        else if (chkAplicarRetencion.IsChecked.Value)
                        {
                            dmlGravada = ListECMP_OrdenServicioDetalle.Where(x => x.ObjEMNF_Servicio.ObjEMNF_OperacionIGV.CodOperacionIGV == "GB").Sum(o => o.Importe);

                            decimal dmlTasa = dmlRentaSegunda;

                            dmlImporteRetencionIGV = dmlGravada * dmlTasa;
                            dmlTotal = dmlGravada - dmlImporteRetencionIGV;

                            lblTitleOrdenServicio02.Text = "Renta Bruta";
                            lblTitleOrdenServicio03.Text = "Retención " + decimal.Round(dmlTasa * 100, 2) + "%";
                            lblTitleOrdenServicio04.Text = "Renta Neta " + ObjESGC_Moneda.Simbolo;
                            strNetoText = "Renta Total";
                        }
                        else
                        {
                            //Calculo sin incluir IGV
                            ListECMP_OrdenServicioDetalle.ForEach(x => { if (x.ObjEMNF_Servicio.ObjEMNF_OperacionIGV.CodOperacionIGV == "GB") dmlGravada += x.PrecioUnitario * x.Cantidad; });
                            dmlImporteRetencionIGV = dmlGravada * dmlIGV;
                            dmlTotal = dmlGravada + dmlImporteRetencionIGV;

                            lblTitleOrdenServicio02.Text = "Gravada";
                            lblTitleOrdenServicio03.Text = "IGV " + decimal.Round(dmlIGV * 100, 2) + "%";
                            lblTitleOrdenServicio04.Text = "Total " + ObjESGC_Moneda.Simbolo;
                            strNetoText = "Importe Total";
                        }
                        #endregion
                    }
                    txtLineas.Text = ListECMP_OrdenServicioDetalle.Count.ToString();
                    txtGravada.Text = decimal.Round(dmlGravada, 2).ToString("###,###,##0.#0");
                    txtImporteIGV.Text = decimal.Round(dmlImporteRetencionIGV, 2).ToString("###,###,##0.#0");
                    txtTotal.Text = Convert.ToDouble(dmlTotal).ToString("N2");
                }
                else
                {
                    txtLineas.Text = ("0");
                    txtGravada.Text = ("0.00");
                    txtImporteIGV.Text = ("0.00");
                    txtTotal.Text = ("0.00");
                }
                dgDetalleServicio.Items.Refresh();
                CmpGridSelectColumn.SelectCellByIndex(dgDetalleServicio, new List<CmpGridItem>()
                {
                    new CmpGridItem(){ColumnIni = 2, ColumnFin = 3}
                });

            }
            catch (Exception) { }
        }

        /// <summary>
        /// Calcula los valores del registro seleccionado
        /// </summary>
        private void CalcularTotalesItems(ECMP_OrdenServicioDetalle ObjECMP_OrdenServicioDetalle)
        {
            try
            {
                if (ObjECMP_OrdenServicioDetalle.ObjEMNF_Servicio.ObjEMNF_OperacionIGV.CodOperacionIGV == "EX" && !rbExonerado.IsChecked.Value)
                {
                    //Calculo con incluir IGV [22]
                    decimal dmlImporte = ObjECMP_OrdenServicioDetalle.PrecioUnitario * ObjECMP_OrdenServicioDetalle.Cantidad;
                    decimal dmlImporteIGV = (dmlImporte / (((0 * 100) + 100) / 100)) * 0;

                    ObjECMP_OrdenServicioDetalle.Importe = decimal.Round(dmlImporte, 8);
                    ObjECMP_OrdenServicioDetalle.ImporteIGV = decimal.Round(dmlImporteIGV, 8);
                }
                else
                {
                    #region
                    //factura
                    if (rbIncluidoIGV.IsChecked.Value)
                    {
                        //Calculo con incluir IGV [12]
                        decimal dmlPrecioUnitario = decimal.Round((ObjECMP_OrdenServicioDetalle.PrecioUnitarioTemp / ((decimal.Round(dmlIGV * 100, 2) + 100) / 100)), 8);
                        decimal dmlImporte = decimal.Round((dmlPrecioUnitario * ObjECMP_OrdenServicioDetalle.Cantidad), 8);
                        decimal dmlImporteIGV = decimal.Round(dmlImporte * dmlIGV, 8);

                        ObjECMP_OrdenServicioDetalle.PrecioUnitario = dmlPrecioUnitario;
                        ObjECMP_OrdenServicioDetalle.Importe = dmlImporte;
						ObjECMP_OrdenServicioDetalle.ImporteIGV = dmlImporteIGV;
                    }
                    else if (rbExonerado.IsChecked.Value)
                    {
                        //Calculo cuando es Honorario [21]
                        decimal dmlTasa = 0;
                        decimal dmlImporte = ObjECMP_OrdenServicioDetalle.PrecioUnitarioTemp * ObjECMP_OrdenServicioDetalle.Cantidad;
                        if (chkAplicarRetencion.IsChecked.Value)
                            dmlTasa = (SGCMethod.GetTasaHonorario(dmlImporte) / 100);
                        decimal dmlImporteIGV = (dmlImporte * dmlTasa);

                        ObjECMP_OrdenServicioDetalle.PrecioUnitario = decimal.Round(ObjECMP_OrdenServicioDetalle.PrecioUnitarioTemp, 8);
                        ObjECMP_OrdenServicioDetalle.Importe = decimal.Round(dmlImporte, 8);
                        ObjECMP_OrdenServicioDetalle.ImporteIGV = decimal.Round(dmlImporteIGV, 8);
                    }
                    else if (chkAplicarRetencion.IsChecked.Value)
                    {
                        decimal dmlTasa = decimal.Round(SGCVariables.ObjESGC_Retencion.PjeRentaSegunda / 100,2);
                        decimal dmlImporte = ObjECMP_OrdenServicioDetalle.PrecioUnitarioTemp * ObjECMP_OrdenServicioDetalle.Cantidad;
                        decimal dmlImporteIGV = (dmlImporte * dmlTasa);
                        dmlRentaSegunda = dmlTasa;

                        ObjECMP_OrdenServicioDetalle.PrecioUnitario = decimal.Round(ObjECMP_OrdenServicioDetalle.PrecioUnitarioTemp, 8);
                        ObjECMP_OrdenServicioDetalle.Importe = decimal.Round(dmlImporte, 8);
                        ObjECMP_OrdenServicioDetalle.ImporteIGV = decimal.Round(dmlImporteIGV, 8);
                    }
                    else
                    {
                        //Calculo sin incluir IGV [11]
                        decimal dmlImporte = decimal.Round((ObjECMP_OrdenServicioDetalle.PrecioUnitarioTemp * ObjECMP_OrdenServicioDetalle.Cantidad), 8);
                        decimal dmlImporteIGV = decimal.Round(dmlImporte * dmlIGV, 8);

                        ObjECMP_OrdenServicioDetalle.PrecioUnitario = ObjECMP_OrdenServicioDetalle.PrecioUnitarioTemp;
                        ObjECMP_OrdenServicioDetalle.Importe = dmlImporte;
                        ObjECMP_OrdenServicioDetalle.ImporteIGV = dmlImporteIGV;
                    }
                    #endregion
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
            var ListECMP_OrdenServicioDetalle = (dgDetalleServicio.Items.OfType<ECMP_OrdenServicioDetalle>()).ToList();

            if (ObjESGC_UsuarioEmpresaSucursal == null)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, "Por favor seleccione un sucursal.", CmpButton.Aceptar);
                blResult = true;
            }
            else if (ObjEMNF_ClienteProveedor == null)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, "Por favor seleccione un proveedor.", CmpButton.Aceptar);
                blResult = true;
            }
            else if (ObjESGC_Moneda == null)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, "Por favor seleccione el tipo de moneda.", CmpButton.Aceptar);
                blResult = true;
            }
            else if (ObjESGC_Estado == null)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, "Por favor seleccione un estado.", CmpButton.Aceptar);
                blResult = true;
            }
            else if (ObjEMNF_TipoDestino == null)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, "Por favor seleccione un Tipo Destino.", CmpButton.Aceptar);
                blResult = true;
            }
            else if (ObjESGC_FormaPago == null)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, "Por favor seleccione forma pago.", CmpButton.Aceptar);
                blResult = true;
            }
            else if (ObjESGC_Area == null)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, "Por favor seleccione un área.", CmpButton.Aceptar);
                blResult = true;
            }
            else if (txtSelRateTipoCambio.Value.ToString().Trim().Length == 0)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, "Por favor ingrese el tipo de cambio.", CmpButton.Aceptar);
                blResult = true;
            }
            else if (ListECMP_OrdenServicioDetalle.Count == 0) 
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, "Por favor ingrese el item del Servicio en el detalle.", CmpButton.Aceptar);
                blResult = true;
            }
            else
            {
                bool blEstadoCantidad = false;
                string strArtDescrip = string.Empty;

                foreach (var ff in ListECMP_OrdenServicioDetalle)
                {
                    if (ff.PrecioUnitario == 0)
                    {
                        blEstadoCantidad = true;
                        strArtDescrip = "Precio Unitario ingresada del Servicio " + ff.ObjEMNF_Servicio.Servicio + " debe ser mayor a cero.";

                        break;
                    }
                    else if (ff.Cantidad == 0)
                    {
                        blEstadoCantidad = true;
                        strArtDescrip = "La cantidad ingresada del Servicio " + ff.ObjEMNF_Servicio.Servicio + " debe ser mayor a cero.";
                        break;
                    }
                    else if (ff.PeriodoCampania == null || ff.PeriodoCampania == string.Empty)
                    {
                        blEstadoCantidad = true;
                        strArtDescrip = "Seleccionar un Periodo Campaña";
                        break;
                    }
                }

                if (blEstadoCantidad)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, strArtDescrip, CmpButton.Aceptar);
                    blResult = true;
                }
            }

            return blResult;
        }

        /// <summary>
        /// Crea XML del detalle
        /// </summary>
        /// <returns></returns>
        private string XmlDetalleOrdenServicio()
        {
            var ListEECMP_OrdenServicioDetalle = (dgDetalleServicio.Items.OfType<ECMP_OrdenServicioDetalle>()).ToList();
            string strCadXml = "";
            strCadXml = "<ROOT>";


            ListEECMP_OrdenServicioDetalle.ForEach((ECMP_OrdenServicioDetalle f) =>
            {
                if (rbIncluidoIGV.IsChecked.Value)
                {
                    f.PrecioUnitario = f.PrecioUnitarioTemp / (((dmlIGV * 100) + 100) / 100);
                }


                strCadXml += "<Listar xIdServicio = \'" + f.ObjEMNF_Servicio.IdServicio;
                strCadXml += "\' xCantidad = \'" + f.Cantidad;
                strCadXml += "\' xPrecioUnitario = \'" + f.PrecioUnitario;
                strCadXml += "\' xImporteIGV = \'" + f.ImporteIGV;
                strCadXml += "\' xIdDestino = \'" + ((ObjEMNF_TipoDestino.CodTipoDestino != "CTSUC") ? f.IdDestino : ObjECMP_OrdenServicio.ObjESGC_EmpresaSucursal.IdEmpSucursal);
                strCadXml += "\' xPeriodoCampania = \'" + f.PeriodoCampania;
                strCadXml += "\' xObservaciones = \'" + f.Observaciones;
                 strCadXml += "\' ></Listar>";
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
            catch (Exception) { }

            dgDetalleServicio.Items.Clear();
            MyHeader.DataContext = null;
            //this.DataContext = null;
            ObjESGC_UsuarioEmpresaSucursal = null;
            ObjEMNF_ClienteProveedor = null;
            ObjESGC_Moneda = null;
            ObjESGC_Estado = null;
            ObjESGC_FormaPago = null;

            txtLineas.Text = ("0");
            txtGravada.Text = ("0.00");
            txtImporteIGV.Text = ("0.00");
            txtTotal.Text = ("0.00");

            lblTitleOrdenServicio02.Text = "Gravada";
            lblTitleOrdenServicio03.Text = "IGV " + decimal.Round(dmlIGV * 100, 2) + "%";
            lblTitleOrdenServicio04.Text = "Total";
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
                    ObjECMP_OrdenServicio.ObjESGC_EmpresaSucursal = new ESGC_EmpresaSucursal() { IdEmpSucursal = Convert.ToInt32(vrIdEmpSucursal.Codigo) };
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, "Error al asignar datos por defecto a [IdEmpSucursal] \n" + ex.Message, CmpButton.Aceptar);
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
                    ObjECMP_OrdenServicio.ObjESGC_Moneda = new ESGC_Moneda() { CodMoneda = vrCodMoneda.Codigo };
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, "Error al asignar datos por defecto a [CodMoneda] \n" + ex.Message, CmpButton.Aceptar);
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
                    ObjECMP_OrdenServicio.ObjESGC_Estado = new ESGC_Estado() { CodEstado = vrCodEstado.Codigo };
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, "Error al asignar datos por defecto a [CodEstado] \n" + ex.Message, CmpButton.Aceptar);
                }

                #endregion
            }

            //ObjESGC_FormaPago
            var vrIdFormaPago = vrListESGC_FormularioSetting.FirstOrDefault(x => x.Campo == "IdFormaPago");
            if (vrIdFormaPago != null)
            {
                #region ASIGNACIÓN

                try
                {
                    ObjECMP_OrdenServicio.ObjESGC_FormaPago = new ESGC_FormaPago() { IdFormaPago = Convert.ToInt32(vrIdFormaPago.Codigo) };
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, "Error al asignar datos por defecto a [IdFormaPago] \n" + ex.Message, CmpButton.Aceptar);
                }

                #endregion
            }

            //ObjESGC_Area
            var vrIdArea = vrListESGC_FormularioSetting.FirstOrDefault(x => x.Campo == "IdArea");
            if (vrIdArea != null)
            {
                #region ASIGNACIÓN

                try
                {
                    ObjECMP_OrdenServicio.ObjESGC_Area = new ESGC_Area() { IdArea = Convert.ToInt32(vrIdArea.Codigo) };
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, "Error al asignar datos por defecto a [IdArea] \n" + ex.Message, CmpButton.Aceptar);
                }

                #endregion
            }
        }

        /// <summary>
        /// Imprime un reporte orden de Servicio
        /// </summary>
        private void ImprimirOrdenServicio()
        {
            try
            {
                var vrObjListECMP_OrdenServicioDetalle = (dgDetalleServicio.Items.OfType<ECMP_OrdenServicioDetalle>()).ToList();
                
                string[] parametro;

                string direccion = new BALM_Almacen().ListFiltrarAlmacen(ObjECMP_OrdenServicio.ObjESGC_EmpresaSucursal).FirstOrDefault().Direccion;
                string Monto = Convert.ToDecimal(txtTotal.Text).ToString();
                parametro = new string[]
                {
                    "prmRazonSocial|"       + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.RazonSocial,
                    "prmRuc|"               + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.Ruc,
                    "prmDireccionEmpresa|"  + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.DireccionFiscal,
                    "prmTelefonoEmpresa|"   + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.Telefono,
                    "prmFechaDocumento|"    + ObjECMP_OrdenServicio.Fecha.ToShortDateString(),
                    "prmFechaLetra|"        + DateTime.Now.ToLongDateString().Split(',').ElementAt(1),
                    "prmNumOrden|"          + ObjECMP_OrdenServicio.Serie + " - " + ObjECMP_OrdenServicio.Numero,
                    "prmProveedor|"         + ObjECMP_OrdenServicio.ObjEMNF_ClienteProveedor.RazonSocial,
                    "prmRucProveedor|"      + ObjECMP_OrdenServicio.ObjEMNF_ClienteProveedor.NroDocIdentidad,
                    "prmDireccionProv|"     + ObjECMP_OrdenServicio.ObjEMNF_ClienteProveedor.Direccion,
                    "prmCondicionPago|"     + ObjECMP_OrdenServicio.ObjESGC_FormaPago.FormaPago,
                    "prmNetoLetra|"         + NumLetras.Convertir(Monto,true,ObjECMP_OrdenServicio.ObjESGC_Moneda.Descripcion),
                    "prmTotal|"             + ObjECMP_OrdenServicio.ObjESGC_Moneda.Simbolo + " "+ txtGravada.Text,
                    "prmIgv|"               + ObjECMP_OrdenServicio.ObjESGC_Moneda.Simbolo + " "+ txtImporteIGV.Text,
                    "prmGravadaText|"       + lblTitleOrdenServicio02.Text,
                    "prmIgvText|"           + lblTitleOrdenServicio03.Text,
                    "prmNetoText|"          + strNetoText,
                    "prmIgv|"               + ObjECMP_OrdenServicio.ObjESGC_Moneda.Simbolo + " "+ txtImporteIGV.Text,
                    "prmNeto|"              + ObjECMP_OrdenServicio.ObjESGC_Moneda.Simbolo + " "+ txtTotal.Text,
                    "prmSucursal|"          + ObjECMP_OrdenServicio.ObjESGC_EmpresaSucursal.Sucursal + " "+ txtTotal.Text,
                    "prmDireccionSucursal|" + direccion,
                    "prmFechaFin|"          + ObjECMP_OrdenServicio.FechaFin.ToShortDateString(),
                    "prmMoneda|"            + ObjECMP_OrdenServicio.ObjESGC_Moneda.Descripcion,
                    "prmCreadopor|"         + SGCVariables.ObjESGC_Usuario.Nombres + " " +  SGCVariables.ObjESGC_Usuario.Apellidos ,
                    "prmAprobadopor|"       + string.Empty,
                };


                MainRerport ObjMainRerport = new MainRerport();
                ObjMainRerport.InitializeMainRerport("CMP.Reports.Files.RptOrdenServicio.rdlc", "dtsOrdenServicioDetalle", vrObjListECMP_OrdenServicioDetalle.ToList(), parametro);
                ObjMainRerport.ShowDialog();
            }
            catch (Exception ex)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, ex.Message, CmpButton.Aceptar);
            }
        }

        #endregion
    }
}
