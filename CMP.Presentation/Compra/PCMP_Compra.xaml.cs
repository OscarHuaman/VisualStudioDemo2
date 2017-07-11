/*********************************************************
'* MODIFICADO POR : COMPUTER SYSTEMS SOLUTION
'*				    Oscar Huamán Cabrera
'* FCH. MODIFICA. : 26/09/2016
'*********************************************************
'* MODIFICADO POR : Oscar Huamán Cabrera
'* FCH. MODIFICA. : 10/10/2016
'* ASUNTO         : Se agregó check Anticipo
**********************************************************/
namespace CMP.Presentation.Compra
{
    using ALM.Business;
    using ALM.Entity;
    using ALM.Presentation.IngresoSalida.RecepcionGuia.Flyouts;
    using CMP.Business;
    using CMP.Entity;
    using CMP.Presentation.Compra.Flyouts;
    using CMP.Presentation.Method;
    using CMP.Presentation.OrdenCompra.Flyouts;
    using CMP.Presentation.OrdenServicio.Flyouts;
    using CMP.Reports;
    using CMP.Useful.Metodo;
    using CMP.Useful.Modulo;
    using CMP.ViewModels.Compra.Flyouts;
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
    using MNF.Presentation.Servicio.Flyouts;
    using SGC.Empresarial.Business;
    using SGC.Empresarial.Entity;
    using SGC.Empresarial.Method;
    using SGC.Empresarial.Useful;
    using SGC.Empresarial.Useful.Modulo;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public partial class PCMP_Compra : UserControl, CmpINavigation
    {
        #region INSTANCIAS, VARIABLES, EVENTOS AGREGADOS

        private ECMP_Compra ObjECMP_Compra;
        private bool boolRetencion { get; set; }
        private bool imprimir { get; set; }
        private decimal dmlIGV = 0;
        private decimal dmlPercepcion = 0;
        private decimal dmlTipoCambio = 0;
        private decimal dmlRentaSegunda = 0;
        private List<EMNF_ClienteProveedor> ListEMNF_ClienteProveedor;
        private List<ECMP_OrdenCompra> ListECMP_OrdenCompra;
        private List<ECMP_OrdenServicio> ListECMP_OrdenServicio;
        private List<EALM_IngresoSalida> ListEALM_IngresoSalida;
        private List<EALM_Almacen> ListEALM_Almacen;
        private EMNF_TipoDestino ObjEMNF_TipoDestino;
        //private ECMP_ValueComboBox ObjEMNF_TipoDestino;
        private ESGC_UsuarioEmpresaSucursal ObjESGC_UsuarioEmpresaSucursal = new ESGC_UsuarioEmpresaSucursal();
        private List<ECMP_ValueComboBox> ListPeriodoCampania;
        private string Message;
        private List<int> lstIdCompra = new List<int>();
        private bool Load = false;
        private List<EMNF_SubCentroCosto> ListCentroCosto;

        #endregion

        #region EVENTOS DE LA CLASE

        public PCMP_Compra()
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
                if (!(value is ECMP_Compra))
                    return;

                this.ObjECMP_Compra = (ECMP_Compra)value;
                Load = true;
                ListECMP_OrdenCompra = new List<ECMP_OrdenCompra>();
                ListEMNF_ClienteProveedor = new List<EMNF_ClienteProveedor>();
                ListPeriodoCampania = new List<ECMP_ValueComboBox>();
                this.KeyDownCmpButtonTitleTecla(ActionF10: btnAgregarItemsIsClicked,
                                                ActionF11: btnQuitarItemsIsClicked,
                                                ActionF12: btnGuardarIsClicked,
                                                ActionF9: btnImprimirIsClicked,
                                                ActionESC: btnSalirIsClicked);
                AddFlyout();
                ClearControl();

                this.ObjECMP_Compra.ObjEMNF_ClienteProveedor = ObjECMP_Compra.ObjEMNF_ClienteProveedor;
                this.ObjECMP_Compra.ObjEMNF_OperacionMovimiento = ObjECMP_Compra.ObjEMNF_OperacionMovimiento;
                LoadHeader(() =>
                {
                    LoadDocumentoRefAndDetail();
                });

                cbxSubDiario.Focus();

                btnAnticipo.IsEnabled = !(chkAnticipo.IsChecked.Value);
                MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminCompra, CMPMensajes.GetAccesoRestringidoNull("Compra"), new Action<ESGC_PermisoPerfil>((P) =>
                {
                    if (P == null) { return; }
                    btnGuardarArtServ.IsEnabled = (P.Nuevo || P.Editar);
                }));
            }
        }
        
        private void txtProveedorRazonSocial_KeyDown_1(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (txtProveedorRazonSocial.Text.Trim().Length == 0)
                ObjECMP_Compra.ObjEMNF_ClienteProveedor = null;

            if (ObjECMP_Compra.Opcion != "I") return;

            MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminCompra, CMPMensajes.GetAccesoRestringidoNull("Proveedor"), new Action<ESGC_PermisoPerfil>((P) =>
            {
                if (P.Consulta)
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
                                ListEMNF_ClienteProveedor = new BMNF_ClienteProveedor().ListFiltrarClienteProveedor(strFiltro, IsClienteOProveedor.Proveedor);
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
                                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, strOutMessageError, CmpButton.Aceptar);
                            }
                            else
                            {
                                if (ListEMNF_ClienteProveedor.Count == 1)
                                {
                                    ObjECMP_Compra.ObjEMNF_ClienteProveedor = ListEMNF_ClienteProveedor.FirstOrDefault();
                                    txtProveedorRazonSocial.Text = ObjECMP_Compra.ObjEMNF_ClienteProveedor.RazonSocial;
                                    cbxFormaPago.SelectedValue = ObjECMP_Compra.ObjEMNF_ClienteProveedor.ObjEMNF_FormaPago.IdFormaPago;
                                }
                                else
                                {

                                    this.FlyoutIsOpen("PMNF_BuscarClienteProveedor", ((value) =>
                                    {
                                        if (value is PMNF_BuscarClienteProveedor)
                                        {
                                            var MyPMNF_BuscarClienteProveedor = (PMNF_BuscarClienteProveedor)value;
                                            MyPMNF_BuscarClienteProveedor.MyIsClienteOProveedor = IsClienteOProveedor.Proveedor;
                                            MyPMNF_BuscarClienteProveedor.InitializePMNF_BuscarClienteProveedor();
                                            MyPMNF_BuscarClienteProveedor.SetValueFilter = strFiltro;
                                            MyPMNF_BuscarClienteProveedor.SetListEMNF_ClienteProveedor = ListEMNF_ClienteProveedor;
                                            MyPMNF_BuscarClienteProveedor.LoadDatil("%", IsClienteOProveedor.Proveedor);
                                            MyPMNF_BuscarClienteProveedor.IsOpen = true;
                                        }
                                    }));
                                }
                                dgCompraDetalle.Items.Clear();
                                dgReferencias.Items.Clear();
                            }
                        });
                    }
                }
                else
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, CMPMensajes.GetAccesoRestringidoBuscar("Proveedor"), CmpButton.Aceptar);
                }
            }));
        }

        private void txtDocumentoRef_KeyDown_1(object sender, System.Windows.Input.KeyEventArgs e)
        {
            MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminCompra, CMPMensajes.GetAccesoRestringidoNull("Recepsión Guía, Compra, Orden Compra"), new Action<ESGC_PermisoPerfil>((P) =>
            {
                if (P.Consulta)
                {
                    if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.B))
                    {
                        if (ObjECMP_Compra.ObjEMNF_ClienteProveedor == null || txtProveedorRazonSocial.Text.Trim().Length == 0)
                            CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "Seleccione un Proveedor", CmpButton.Aceptar);
                        else if (ObjECMP_Compra.ObjESGC_Moneda == null)
                            CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "Seleccione una Moneda", CmpButton.Aceptar);
                        else if (ObjECMP_Compra.Opcion != "I")
                            return;
                        else if (cbxDocReferencia.SelectedValue.ToString() == "ODC")
                            BuscarOrdenCompra();
                        else if (cbxDocReferencia.SelectedValue.ToString() == "ODS")
                            BuscarOrdenServicio();
                        else if (cbxDocReferencia.SelectedValue.ToString() == "GRC")
                            BuscarRecepcionGuia();
                    }
                }
                else
                    CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, CMPMensajes.GetAccesoRestringidoBuscar("Recepsión Guía, Compra, Orden Compra"), CmpButton.Aceptar);
            }));
        }

        private void cbxPeriodo_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (cbxPeriodo.SelectedItem == null) return;
            string strPeriodo = ((EMNF_Periodo)cbxPeriodo.SelectedItem).Periodo;
            ObjECMP_Compra.Periodo = strPeriodo;
			LoadTipoDestino();
            MDatePicker.DateStartToDateEnd(dtpFechaOrden, strPeriodo);
            MDatePicker.DateStartToDateEnd(dtpFechaContable, strPeriodo, true);            
        }

        private void cbxEstado_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ObjECMP_Compra.ObjESGC_Estado = (ESGC_Estado)cbxEstado.SelectedItem;
        }

        private void cbxFormaPago_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ObjECMP_Compra.ObjESGC_FormaPago = (ESGC_FormaPago)cbxFormaPago.SelectedItem;
        }

        private void cbxOperacionMovimiento_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ObjECMP_Compra.ObjEMNF_OperacionMovimiento = (EMNF_OperacionMovimiento)cbxOperacionMovimiento.SelectedItem;
            if (ObjECMP_Compra.ObjEMNF_OperacionMovimiento == null) return;

            if (ObjECMP_Compra.ObjEMNF_OperacionMovimiento.CodOpeMovimiento == "PGID")
                dtpFechaContable.IsEnabled = false;
            else
                dtpFechaContable.IsEnabled = true;
            
            if (ObjECMP_Compra.ObjEMNF_OperacionMovimiento.CodOpeMovimiento != "PPFC")
                chkAfectoAlmacen.Visibility = System.Windows.Visibility.Hidden;

            dtgColumnaTipoDestino.Visibility = System.Windows.Visibility.Collapsed;
            dtgColumnaPeriodoCompania.Visibility = System.Windows.Visibility.Collapsed;

            cbxTipoDestino.Visibility = (ObjECMP_Compra.Opcion == "I") ? System.Windows.Visibility.Collapsed : ((ObjECMP_Compra.Distribucion) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed);
            lblTipoDestino.Visibility = cbxTipoDestino.Visibility;
            dtgColumnaTipoDestino.Visibility = (ObjECMP_Compra.Opcion == "I") ? System.Windows.Visibility.Collapsed : ((ObjECMP_Compra.Distribucion) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed);
            dtgColumnaPeriodoCompania.Visibility = (ObjECMP_Compra.Opcion == "I") ? System.Windows.Visibility.Collapsed : ((ObjECMP_Compra.Distribucion) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed);
            chkDistribucion.Visibility = System.Windows.Visibility.Collapsed;
            chkCajaBanco.Visibility = System.Windows.Visibility.Collapsed;
            chkAfectoPercepcion.IsEnabled = false;
            cbxDocumento.Items.Clear();
            cbxDocumento.IsEnabled = false;
            chkAnticipo.IsEnabled = false;
            btnAnticipo.IsEnabled = false;
            cbxDocReferencia.ItemsSource = null;
            cbxDocReferencia.IsEnabled = false;
            txtDocumentoRef.Text = string.Empty;
            txtDocumentoRef.IsEnabled = false;
            btnAgregarArtServ.IsEnabled = false;
            btnQuitarArtServ.IsEnabled = false;
            LoadDataInOperacionMoviento(ObjECMP_Compra.ObjEMNF_OperacionMovimiento);
        }

        private void cbxMotivoMovimiento_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ObjECMP_Compra.ObjEMNF_MotivoMovimiento = (EMNF_MotivoMovimiento)cbxMotivoMovimiento.SelectedItem;
            if (ObjECMP_Compra.ObjEMNF_OperacionMovimiento == null || ObjECMP_Compra.ObjEMNF_MotivoMovimiento == null) { return; }
            chkDistribucion.Visibility = System.Windows.Visibility.Collapsed;
            chkCajaBanco.Visibility = System.Windows.Visibility.Collapsed;
            chkPlanilla.Visibility = System.Windows.Visibility.Collapsed;
            chkPlanilla.IsChecked = false;
            chkPlanilla_Checked_1(null, new System.Windows.RoutedEventArgs());
            chkDistribucion.IsChecked = (ObjECMP_Compra.Opcion == "I") ? false : ObjECMP_Compra.Distribucion;
            cbxMotivoMovimiento.IsEnabled = (ObjECMP_Compra.Opcion == "I");
            cbxDocReferencia.IsEnabled = (ObjECMP_Compra.Opcion == "I");
            chkIncluyeIGV.IsChecked = (ObjECMP_Compra.Opcion == "I") ? false : ObjECMP_Compra.IncluyeIGV;
            chkIncluyeIGV.IsEnabled = false;
            chkAfectoDetraccion.IsChecked = ObjECMP_Compra.AfectoDetraccion;
            chkAfectoDetraccion.IsEnabled = true;
            chkAfectoPercepcion.IsEnabled = true;
            dtpFechaContable.IsEnabled = true;
            txtGuiaRemision.IsEnabled = false;
			btnAnticipo.IsEnabled = false;
            dgCompraDetalle.Columns[9].Header = "IGV";
            cbxEmpresaSucursal.Visibility = System.Windows.Visibility.Collapsed;
            cbxDocReferencia.Visibility = System.Windows.Visibility.Visible;
            cbxTipoDestino.Visibility = (ObjECMP_Compra.Opcion == "I") ? System.Windows.Visibility.Collapsed : ((ObjECMP_Compra.Distribucion) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed);
            lblTipoDestino.Visibility = cbxTipoDestino.Visibility;
            dtgColumnaTipoDestino.Visibility = (ObjECMP_Compra.Opcion == "I") ? System.Windows.Visibility.Collapsed : ((ObjECMP_Compra.Distribucion) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed);
            dtgColumnaPeriodoCompania.Visibility = (ObjECMP_Compra.Opcion == "I") ? System.Windows.Visibility.Collapsed : ((ObjECMP_Compra.Distribucion) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed);
            lblDocReferencia.Text = "Tipo de referencia";
            lblTitleCompra02.Text = "Exonerado";
            string strCodOpeMovimiento = ObjECMP_Compra.ObjEMNF_OperacionMovimiento.CodOpeMovimiento;
            string strCodMovMovimiento = ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento;
            if (strCodOpeMovimiento == "PPFC")
            {
                #region PPFC
                cbxDocumento.IsEnabled = true;
                cbxDocumento.Items.Clear();
                cbxDocumento.Items.Add("FAC");
                cbxDocumento.Items.Add("BOL");
                cbxDocumento.Items.Add("TCK");
                chkRetencion.Visibility = System.Windows.Visibility.Hidden;
                chkAfectoAlmacen.Visibility = System.Windows.Visibility.Collapsed;
                chkAfectoPercepcion.Visibility = System.Windows.Visibility.Visible;
                chkAnticipo.Visibility = System.Windows.Visibility.Visible;
                chkAnticipo.IsEnabled = false;
                if (strCodMovMovimiento == "ISF" || strCodMovMovimiento == "IDS")
                {
                    dgColumnAlmacen.Visibility = System.Windows.Visibility.Collapsed;
                    dgColumnSerieNum.Visibility = System.Windows.Visibility.Collapsed;
                    ColumnCal01.Visibility = System.Windows.Visibility.Collapsed;
                    if (strCodMovMovimiento == "IDS")
                    {
                        chkRetencion.IsEnabled = false;

                        cbxEmpresaSucursal.ItemsSource = new BSGC_UsuarioEmpresaSucursal().ListFiltrarUsuarioEmpresaSucursal(SGCVariables.ObjESGC_Usuario);
                        cbxDocReferencia.Visibility = System.Windows.Visibility.Collapsed;
                        cbxEmpresaSucursal.Visibility = System.Windows.Visibility.Visible;   
                        cbxDocReferencia.ItemsSource = DocumentoReferencia.GetDirecta;
                        chkAfectoPercepcion.IsEnabled = true;
                        btnAgregarArtServ.IsEnabled = (ObjECMP_Compra.Opcion == "I");
                        txtGuiaRemision.IsEnabled = false;
                        chkAnticipo.IsEnabled = true;
						btnAnticipo.IsEnabled = true;
                        cbxDocumento.Items.Add("RCB");
                        if (ObjECMP_Compra.Opcion == "I")
                            cbxEmpresaSucursal.SelectedIndex = 0;
                        else
                            cbxDocumento.SelectedValue = ObjECMP_Compra.CodDocumento;
                        
                        chkIncluyeIGV.IsChecked = (ObjECMP_Compra.Opcion == "I") ? false : ObjECMP_Compra.IncluyeIGV;
                        chkIncluyeIGV.IsEnabled = true;
                        lblDocReferencia.Text = "Empresa Sucursal";
                    }
                    else 
                    {
                        txtGuiaRemision.IsEnabled = true;
                        GridDocReferencias.IsEnabled = true;
                        chkRetencion.IsEnabled = false;
                        cbxDocReferencia.ItemsSource = DocumentoReferencia.GetHonorario;
                        cbxDocumento.Items.Add("RCB");
                    }
                    btnAgregarArtServ.ContentTitle = "Agregar Servicio";
                    btnQuitarArtServ.ContentTitle = "Quitar Servicio";
                }
                else
                {
                    if (strCodMovMovimiento == "IAF")
                    {
                        chkAnticipo.Visibility = System.Windows.Visibility.Collapsed;
                        cbxDocReferencia.ItemsSource = DocumentoReferencia.GetProvisionActivo;
                        cbxDocReferencia.Visibility = System.Windows.Visibility.Collapsed;
                        lblDocReferencia.Text = "Empresa Sucursal";
                        cbxEmpresaSucursal.Visibility = System.Windows.Visibility.Visible;
                        cbxEmpresaSucursal.ItemsSource = new BSGC_UsuarioEmpresaSucursal().ListFiltrarUsuarioEmpresaSucursal(SGCVariables.ObjESGC_Usuario);
                        dgColumnSerieNum.Visibility = System.Windows.Visibility.Collapsed;
                        dgColumnAlmacen.Visibility = System.Windows.Visibility.Collapsed;
                        btnAgregarArtServ.IsEnabled = (ObjECMP_Compra.Opcion == "I");
                        chkIncluyeIGV.IsEnabled = true;
                        chkRetencion.IsEnabled = false;
                        chkRetencion.Visibility = System.Windows.Visibility.Hidden;
                        
                        if (ObjECMP_Compra.Opcion == "I")
                            cbxEmpresaSucursal.SelectedIndex = 0;
                        else
                            cbxDocumento.SelectedValue = ObjECMP_Compra.CodDocumento;
                    }
                    else if (strCodMovMovimiento == "ICD")
                    {
                        cbxDocReferencia.ItemsSource = DocumentoReferencia.GetProvisionActivo;
                        cbxDocReferencia.Visibility = System.Windows.Visibility.Collapsed;
                        cbxEmpresaSucursal.Visibility = System.Windows.Visibility.Visible;
                        if (ObjECMP_Compra.Opcion == "U")
                        {
                            dtpFechaOrden.IsEnabled = ((ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "ICD" && (chkAfectoAlmacen.IsChecked.Value||ObjECMP_Compra.AfectaAlmacen)) ? false : true);
                            txtSelRateTipoCambio.IsEnabled = ((ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "ICD" && (chkAfectoAlmacen.IsChecked.Value || ObjECMP_Compra.AfectaAlmacen)) ? false : true);
                            dtpFechaContable.IsEnabled = ((ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "ICD" && (chkAfectoAlmacen.IsChecked.Value || ObjECMP_Compra.AfectaAlmacen)) ? false : true);
                            cbxMoneda.IsEnabled = ((ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "ICD" && (chkAfectoAlmacen.IsChecked.Value || ObjECMP_Compra.AfectaAlmacen)) ? false : true);
                        }
                        lblDocReferencia.Text = "Empresa Sucursal";
                        cbxEmpresaSucursal.ItemsSource = new BSGC_UsuarioEmpresaSucursal().ListFiltrarUsuarioEmpresaSucursal(SGCVariables.ObjESGC_Usuario);
                        
                        if (ObjECMP_Compra.Opcion == "I")
                            cbxEmpresaSucursal.SelectedIndex = 0;
                        else
                            cbxDocumento.SelectedValue = ObjECMP_Compra.CodDocumento;

                        dgColumnSerieNum.Visibility = System.Windows.Visibility.Collapsed;
                        chkAnticipo.IsEnabled = true;
                        btnAnticipo.IsEnabled = true;
                        chkRetencion.IsEnabled = false;
                        txtGuiaRemision.IsEnabled = true;
                        txtGuiaRemision.IsReadOnly = false;
                        chkIncluyeIGV.IsEnabled = (ObjECMP_Compra.Opcion == "I");
                        GridGuiaRemision.Visibility = System.Windows.Visibility.Visible;
                        
                    }
                    else if (strCodMovMovimiento == "ICP")
                    {
                        cbxDocReferencia.ItemsSource = DocumentoReferencia.GetProvisionInsumo;
                        txtGuiaRemision.IsEnabled = true;
                        if (ObjECMP_Compra.Opcion == "U")
                        {
                            dtpFechaOrden.IsEnabled = false;
                            cbxMoneda.IsEnabled = false;
                            dtpFechaContable.IsEnabled = false;
                            txtSelRateTipoCambio.IsEnabled = false;
                        }
                        GridDocReferencias.IsEnabled = true;
                        chkRetencion.IsEnabled = false;
                        dtpFechaContable.IsEnabled = false;
                        chkRetencion.Visibility = System.Windows.Visibility.Hidden;
                        dgColumnAlmacen.Visibility = System.Windows.Visibility.Visible;
                        dgColumnSerieNum.Visibility = System.Windows.Visibility.Collapsed;
                    }
                    else if (strCodMovMovimiento == "IDP")
                    {
                        cbxDocumento.Items.Remove("BOL");
                        cbxDocumento.Items.Remove("TCK");
                        if (ObjECMP_Compra.Opcion == "U")
                        {
                            dtpFechaOrden.IsEnabled = true;
                            cbxMoneda.IsEnabled = true;
                            dtpFechaContable.IsEnabled = true;
                            txtSelRateTipoCambio.IsEnabled = true;
                        }
                        cbxDocReferencia.ItemsSource = DocumentoReferencia.GetDirecta;
                        cbxDocReferencia.Visibility = System.Windows.Visibility.Collapsed;
                        cbxEmpresaSucursal.Visibility = System.Windows.Visibility.Visible;
                        cbxEmpresaSucursal.ItemsSource = new BSGC_UsuarioEmpresaSucursal().ListFiltrarUsuarioEmpresaSucursal(SGCVariables.ObjESGC_Usuario);
                        lblTipoDestino.Visibility = System.Windows.Visibility.Visible;
                        cbxTipoDestino.Visibility = System.Windows.Visibility.Visible;
                        lblDocReferencia.Text = "Empresa Sucursal";
                        if (ObjECMP_Compra.Opcion == "I")
                        {
                            cbxEmpresaSucursal.SelectedIndex = 0;
                            cbxTipoDestino.SelectedIndex = 0;
                        }
                        else
                        {
                            cbxDocumento.SelectedValue = ObjECMP_Compra.CodDocumento;
                            cbxTipoDestino.SelectedValue = ObjECMP_Compra.ObjEMNF_TipoDestino.CodTipoDestino;
                        }
                        btnAgregarArtServ.IsEnabled = (ObjECMP_Compra.Opcion == "I");
                        btnQuitarArtServ.IsEnabled = (ObjECMP_Compra.Opcion == "I");
                        dgColumnSerieNum.Visibility = System.Windows.Visibility.Collapsed;
                        dgColumnAlmacen.Visibility = System.Windows.Visibility.Collapsed;
                        dgCompraDetalle.Columns[9].Header = "Retención";
                        lblTitleCompra03.Text = "Renta Bruta";
                        lblTitleCompra04.Text = "Retención " + ((ObjECMP_Compra.Retencion) ? decimal.Round(ObjECMP_Compra.IGV * 100, 2) : 0);
                        lblTitleCompra05.Text = "Renta Neta " + ObjECMP_Compra.ObjESGC_Moneda.Simbolo;
                        ColumnCal01.Visibility = System.Windows.Visibility.Collapsed;
                        chkRetencion.Visibility = System.Windows.Visibility.Visible;
                        chkAfectoPercepcion.Visibility = System.Windows.Visibility.Collapsed;
                        dtgColumnaTipoDestino.Visibility = System.Windows.Visibility.Visible;
                        dtgColumnaPeriodoCompania.Visibility = (cbxTipoDestino.Visibility == System.Windows.Visibility.Visible) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                        chkRetencion.IsEnabled = true;
                        chkIncluyeIGV.IsEnabled = false;
                        chkAfectoPercepcion.IsEnabled = false;
                        btnAnticipo.IsEnabled = false;

                        btnAgregarArtServ.ContentTitle = "Agregar Servicio";
                        btnQuitarArtServ.ContentTitle = "Quitar Servicio";
                    }
                    else if (strCodMovMovimiento == "IPF")
                    {
                        cbxDocReferencia.Visibility = System.Windows.Visibility.Visible;
                        cbxDocReferencia.ItemsSource = DocumentoReferencia.GetHonorario;
                        cbxEmpresaSucursal.Visibility = System.Windows.Visibility.Collapsed;
                        if (ObjECMP_Compra.Opcion == "U")
                        {
                            dtpFechaOrden.IsEnabled = true;
                            cbxMoneda.IsEnabled = true;
                            dtpFechaContable.IsEnabled = true;
                            txtSelRateTipoCambio.IsEnabled = true;
                        }
                        dgCompraDetalle.Columns[9].Header = "Retención";
                        lblTitleCompra03.Text = "Renta Bruta";
                        lblTitleCompra04.Text = "Retención " + ((ObjECMP_Compra.Retencion) ? decimal.Round(ObjECMP_Compra.IGV * 100, 2) : 0);
                        lblTitleCompra05.Text = "Renta Neta " + ObjECMP_Compra.ObjESGC_Moneda.Simbolo;
                        dgColumnSerieNum.Visibility = System.Windows.Visibility.Collapsed;
                        dgColumnAlmacen.Visibility = System.Windows.Visibility.Collapsed;
                        dtgColumnaPeriodoCompania.Visibility = (ObjECMP_Compra.Distribucion) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed ;
                        GridDocReferencias.IsEnabled = true;
                        txtDocumentoRef.IsEnabled = true;
                        btnAnticipo.IsEnabled = false;
                        chkAfectoDetraccion.IsEnabled = false;
                        btnAgregarArtServ.IsEnabled = false;
                        btnQuitarArtServ.IsEnabled = false;
                        chkRetencion.IsEnabled = false;
                        ColumnCal01.Visibility = System.Windows.Visibility.Collapsed;
                        chkRetencion.Visibility = System.Windows.Visibility.Visible;
                        chkAfectoPercepcion.Visibility = System.Windows.Visibility.Collapsed;
                        btnAgregarArtServ.ContentTitle = "Agregar Servicio";
                        btnQuitarArtServ.ContentTitle = "Quitar Servicio";
                    }
                    else
                    {
                        dgColumnAlmacen.Visibility = System.Windows.Visibility.Visible;
                        ColumnCal01.Visibility = System.Windows.Visibility.Visible;
                        cbxDocReferencia.ItemsSource = DocumentoReferencia.GetFactura;
                    }
                    if (strCodMovMovimiento != "IDP" && strCodMovMovimiento != "IPF")
                    {
                        btnAgregarArtServ.ContentTitle = "Agregar Artículo";
                        btnQuitarArtServ.ContentTitle = "Quitar Artículo";
                    }
                }
                if (strCodMovMovimiento != "IDP" && strCodMovMovimiento != "IPF")
                {
                    lblTitleCompra03.Text = "Gravada";
                    lblTitleCompra04.Text = "IGV " + decimal.Round(dmlIGV * 100, 2) + "%";
                    lblTitleCompra05.Text = "Total";
                }
                
                #endregion
            }
            else if (strCodOpeMovimiento == "PPHN")
            {
                chkAfectoDetraccion.IsChecked = false;
                chkAfectoDetraccion.IsEnabled = false;
                txtGuiaRemision.IsEnabled = false;
                if (ObjECMP_Compra.Opcion == "U")
                {
                    dtpFechaOrden.IsEnabled = true;
                    cbxMoneda.IsEnabled = true;
                    dtpFechaContable.IsEnabled = true;
                    txtSelRateTipoCambio.IsEnabled = true;
                }
				cbxDocumento.IsEnabled = true;
                chkAfectoPercepcion.Visibility = System.Windows.Visibility.Collapsed;
                cbxDocReferencia.ItemsSource = DocumentoReferencia.GetHonorario;
                cbxDocumento.Items.Clear();
                cbxDocumento.Items.Add("HNR");
                if (strCodMovMovimiento == "IDH")
                {
                    #region IDH
                    chkRetencion.Visibility = System.Windows.Visibility.Visible;
                    btnAgregarArtServ.IsEnabled = (ObjECMP_Compra.Opcion == "I");
                    lblDocReferencia.Text = "Empresa Sucursal";
                    chkRetencion.IsEnabled = true;
                    cbxEmpresaSucursal.ItemsSource = new BSGC_UsuarioEmpresaSucursal().ListFiltrarUsuarioEmpresaSucursal(SGCVariables.ObjESGC_Usuario);
                    cbxDocReferencia.Visibility = System.Windows.Visibility.Collapsed;
                    cbxEmpresaSucursal.Visibility = System.Windows.Visibility.Visible;
                    lblTipoDestino.Visibility = System.Windows.Visibility.Visible;
                    cbxTipoDestino.Visibility = System.Windows.Visibility.Visible;
                    dtgColumnaPeriodoCompania.Visibility = System.Windows.Visibility.Visible;
                    if (ObjECMP_Compra.Opcion == "I")
                    {
                        cbxEmpresaSucursal.SelectedIndex = 0;
                        cbxTipoDestino.SelectedIndex = 0;
                    }
                    else
                    {
                        dtpFechaOrden.IsEnabled = true;
                        cbxMoneda.IsEnabled = true;
                        txtSelRateTipoCambio.IsEnabled = true;
                        dtpFechaContable.IsEnabled = true;
                        cbxTipoDestino.SelectedValue = ObjECMP_Compra.ObjEMNF_TipoDestino.CodTipoDestino;
                    }
                    #endregion
                }
                else
                {
                    btnAgregarArtServ.IsEnabled = false;
                    chkRetencion.IsEnabled = false;
                    chkRetencion.Visibility = System.Windows.Visibility.Visible;
                }
                dgColumnAlmacen.Visibility = System.Windows.Visibility.Collapsed;
                dgColumnSerieNum.Visibility = System.Windows.Visibility.Collapsed;
                ColumnCal01.Visibility = System.Windows.Visibility.Collapsed;
                dgCompraDetalle.Columns[9].Header = "Retención";
                btnAgregarArtServ.ContentTitle = "Agregar Servicio";
                btnQuitarArtServ.ContentTitle = "Quitar Servicio";
                lblTitleCompra03.Text = "Total Honorario";
                dtpFechaOrden.IsEnabled = true;
                cbxMoneda.IsEnabled = true;
                txtSelRateTipoCambio.IsEnabled = true;
                dtpFechaContable.IsEnabled = true;
                lblTitleCompra04.Text = "Retención 8%";
                lblTitleCompra05.Text = "Total Neto " + ObjECMP_Compra.ObjESGC_Moneda.Simbolo;
            }
            else if (strCodOpeMovimiento == "PGID")
			{
                chkRetencion.Visibility = System.Windows.Visibility.Hidden;
                if (strCodMovMovimiento == "GIS")
                {
                    #region GIS
                    dgColumnSerieNum.Visibility = System.Windows.Visibility.Collapsed;
                    chkCajaBanco.Visibility = System.Windows.Visibility.Visible;
                    chkCajaBanco.IsChecked = ObjECMP_Compra.CajaBanco;
                    GridGuiaRemision.Visibility = System.Windows.Visibility.Collapsed;
                    chkPlanilla.IsChecked = ObjECMP_Compra.Planilla;
                    chkPlanilla_Checked_1(null, new System.Windows.RoutedEventArgs());
                    chkPlanilla.Visibility = System.Windows.Visibility.Visible;
                    chkRetencion.IsEnabled = false;
                    dtpFechaOrden.IsEnabled = true;
                    if (ObjECMP_Compra.Opcion == "U")
                    {
                        chkPlanilla.IsEnabled = false;
                        dtpFechaOrden.IsEnabled = true;
                        cbxMoneda.IsEnabled = true;
                        dtpFechaContable.IsEnabled = true;
                        txtSelRateTipoCambio.IsEnabled = true;
                    }
                    chkIncluyeIGV.IsChecked = (ObjECMP_Compra.Opcion == "I") ? false : ObjECMP_Compra.IncluyeIGV;
                    chkIncluyeIGV.IsEnabled = (ObjECMP_Compra.Opcion == "I" || ObjECMP_Compra.CodEstado == "PECMP");
                    chkAfectoPercepcion.IsEnabled = false;
                    #endregion
                }
                else
                    ColumnCal01.Visibility = System.Windows.Visibility.Visible;

                btnAgregarArtServ.IsEnabled = (strCodMovMovimiento == "GIS");
                cbxDocumento.IsEnabled = true;
                txtGuiaRemision.IsEnabled = false;
                cbxDocumento.Items.Clear();
                cbxDocumento.Items.Add("RIG");

                dgColumnAlmacen.Visibility = System.Windows.Visibility.Collapsed;
                btnAgregarArtServ.ContentTitle = (strCodMovMovimiento == "GIA") ? "Agregar Artículo" : "Agregar Servicio";
                btnQuitarArtServ.ContentTitle = (strCodMovMovimiento == "GIA") ? "Quitar Artículo" : "Quitar Servicio";
                chkAfectoPercepcion.Visibility = System.Windows.Visibility.Visible;
                lblTitleCompra03.Text = "Gravada";
                lblTitleCompra04.Text = "IGV " + decimal.Round(dmlIGV * 100, 2) + "%";
                lblTitleCompra05.Text = "Total";
                dtpFechaOrden.IsEnabled = true;
                cbxMoneda.IsEnabled = true;
                txtSelRateTipoCambio.IsEnabled = true;
                dtpFechaContable.IsEnabled = true;
                cbxDocReferencia.ItemsSource = DocumentoReferencia.GetRID;
                chkAfectoDetraccion.IsChecked = false;
                chkAfectoDetraccion.IsEnabled = false;
            }
            else
            {
                cbxDocumento.Items.Clear();
                cbxDocumento.IsEnabled = false;
                cbxDocReferencia.ItemsSource = null;
                cbxDocReferencia.IsEnabled = false;
                txtDocumentoRef.Text = string.Empty;
                txtDocumentoRef.IsEnabled = false;
                chkRetencion.IsEnabled = false;
                if (ObjECMP_Compra.Opcion == "U")
                {
                    dtpFechaOrden.IsEnabled = true;
                    cbxMoneda.IsEnabled = true;
                    dtpFechaContable.IsEnabled = true;
                    txtSelRateTipoCambio.IsEnabled = true;
                }
            }

            ChkAlmacenDistribucion();

            if (ObjECMP_Compra.Opcion == "I")
            {
                cbxDocumento.SelectedIndex = 0;
                cbxDocReferencia.SelectedIndex  = 0;                
            }
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
                DateTime tmpDateEnd;
                if (intMes == DateTime.Now.Month)
                    tmpDateEnd = DateTime.Now.Date;
                else
				{
                    var vrFecha = new DateTime(((intMes == 12) ? intAnio + 1 : intAnio), ((intMes != 12) ? (intMes + 1) : 1), 1);
                    tmpDateEnd = vrFecha.AddDays(-1);
                }

                if (dtpFechaOrden.SelectedDate.Value > tmpDateEnd)
                {
                    dtpFechaOrden.SelectedDate = tmpDateEnd;
                }
                LoadTipoDestino();
            }
            LoadSelRateTipoCambio();
        }

        private void dtpFechaContable_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
        {            
            if (dtpFechaContable.SelectedDate != null && cbxPeriodo.SelectedValue != null)
            {
                int intAnio = Convert.ToInt32(cbxPeriodo.SelectedValue.ToString().Substring(0, 4));
                int intMes = Convert.ToInt32(cbxPeriodo.SelectedValue.ToString().Substring(4, 2));
                if (dtpFechaContable.SelectedDate.Value.ToString("yyyyMM") != cbxPeriodo.SelectedValue.ToString())
                    dtpFechaContable.SelectedDate = new DateTime(((intMes == 12) ? intAnio + 1 : intAnio), ((intMes != 12) ? (intMes + 1) : 1), 1).AddDays(-1);
            }
        }

        private void cbxSubDiario_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ObjECMP_Compra.ObjEMNF_SubDiario = (EMNF_SubDiario)cbxSubDiario.SelectedItem;
        }

        private void cbxDocReferencia_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {            
            if (ObjECMP_Compra.Opcion == "I")
            {
                dgCompraDetalle.Items.Clear();
                dgReferencias.Items.Clear();
                btnQuitarArtServ.IsEnabled = false;
                txtLineasArtServ.Text = ("0");
                txtExoneradoArtServ.Text = ("0.00");
                txtGravadaArtServ.Text = ("0.00");
                txtTotalIgvArtServ.Text = ("0.00");
                txtTotalNetoArtServ.Text = ("0.00");
                txtDocumentoRef.IsEnabled = true;
            }
            dtgDetalleColumnGuiaRemision.Visibility = System.Windows.Visibility.Visible;
            if (cbxMotivoMovimiento.SelectedValue != null)
				if (cbxDocReferencia.SelectedValue != null && cbxMotivoMovimiento.SelectedValue.ToString() == "ICD")
				{
					chkAnticipo.Visibility = System.Windows.Visibility.Visible;
					btnAgregarArtServ.IsEnabled = (ObjECMP_Compra.Opcion == "I");
					GridDocReferencias.Visibility = System.Windows.Visibility.Visible;
					dtgDetalleColumnGuiaRemision.Visibility = System.Windows.Visibility.Collapsed;
					if (lstIdCompra.Count != 0 || ObjECMP_Compra.Opcion != "I")
						chkAnticipo.IsEnabled = false;
					else
						chkAnticipo.IsEnabled = true;
					chkAnticipo.IsChecked = ObjECMP_Compra.Anticipo;
				}

            if (cbxDocReferencia.SelectedValue == null || ObjECMP_Compra.ObjEMNF_OperacionMovimiento == null || ObjECMP_Compra.ObjEMNF_MotivoMovimiento == null)
            {
                return;
            }
            string strDocReferencia = cbxDocReferencia.SelectedValue.ToString();
            string strCodMotMovimiento = ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento;

            if (ObjECMP_Compra.ObjEMNF_OperacionMovimiento.CodOpeMovimiento == "PPFC")
            {
                #region
                if (strCodMotMovimiento == "ISF" || strCodMotMovimiento == "IDS")
                {
                    ColumnCal01.Visibility = System.Windows.Visibility.Visible;
                    btnAgregarArtServ.ContentTitle = "Agregar Servicio";
                    btnQuitarArtServ.ContentTitle = "Quitar Servicio";
                }
                else if (strCodMotMovimiento == "IPF" || strCodMotMovimiento == "IDP")
                {
                    ColumnCal01.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    ColumnCal01.Visibility = System.Windows.Visibility.Visible;
                    btnAgregarArtServ.ContentTitle = "Agregar Artículo";
                    btnQuitarArtServ.ContentTitle = "Quitar Artículo";
                }
                #endregion
            }
            else if (ObjECMP_Compra.ObjEMNF_OperacionMovimiento.CodOpeMovimiento == "PPHN")
            {
                ColumnCal01.Visibility = System.Windows.Visibility.Collapsed;
                btnAgregarArtServ.ContentTitle = "Agregar Servicio";
                btnQuitarArtServ.ContentTitle = "Quitar Servicio";
            }

            if (cbxDocumento.SelectedValue != null)
            {
                if (strCodMotMovimiento != "ICD")
                {
                    if (cbxDocumento.SelectedValue.ToString() != "RIG")
                    {
                        btnAgregarArtServ.IsEnabled = (strDocReferencia == "NGN" || strCodMotMovimiento=="IDH");
                    }
                }
                else
                    btnAgregarArtServ.IsEnabled = (ObjECMP_Compra.Opcion == "I");
            }

            if (ObjECMP_Compra.ObjEMNF_OperacionMovimiento.CodOpeMovimiento == "PPFC" && (strCodMotMovimiento == "ICP" || strCodMotMovimiento == "ICD"))
            {
                if (strDocReferencia == "NGN")
                {
                    if (strCodMotMovimiento == "ICD")
                    {
                        GridGuiaRemision.Visibility = System.Windows.Visibility.Visible;
                        GridDocReferencias.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    {
                        GridGuiaRemision.Visibility = System.Windows.Visibility.Visible;
                        GridDocReferencias.Visibility = System.Windows.Visibility.Collapsed;
                    }
                    dgReferencias.Height = 70;
                }
                else
                {
                    if (strDocReferencia == "ODC")
                    {
                        dtgDetalleColumnGuiaRemision.Visibility = System.Windows.Visibility.Collapsed;
                        GridGuiaRemision.Visibility = System.Windows.Visibility.Visible;
                        dgReferencias.Height = 70;
                    }
                    else if (strDocReferencia == "GRC")
                    {
                        dtgDetalleColumnGuiaRemision.Visibility = System.Windows.Visibility.Visible;
                        GridGuiaRemision.Visibility = System.Windows.Visibility.Collapsed;
                        dgReferencias.Height = 115;
                    }
                    GridDocReferencias.Visibility = System.Windows.Visibility.Visible;
                }
            }
            else if (ObjECMP_Compra.ObjEMNF_OperacionMovimiento.CodOpeMovimiento == "PPHN" || ObjECMP_Compra.ObjEMNF_OperacionMovimiento.CodOpeMovimiento == "PPFC")
            {
                if (strDocReferencia == "ODS")
                {
                    GridGuiaRemision.Visibility = System.Windows.Visibility.Collapsed;
                    GridDocReferencias.Visibility = System.Windows.Visibility.Visible;
                    dtgDetalleColumnGuiaRemision.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    dtgDetalleColumnGuiaRemision.Visibility = System.Windows.Visibility.Visible;
                }
            }
            btnAnticipo.IsEnabled = btnAgregarArtServ.IsEnabled;
            txtDocumentoRef.IsEnabled = (strDocReferencia != "NGN");
        }

        private void cbxEmpresaSucursal_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (ObjECMP_Compra.ObjEMNF_MotivoMovimiento == null) return;
            if (ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "IDS" || ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "IDH" 
                || ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "IDP" || ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "IAF" 
                || ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "ICD")
            {
                if (cbxEmpresaSucursal.SelectedValue == null) return;
                ObjESGC_UsuarioEmpresaSucursal.ObjESGC_EmpresaSucursal = new ESGC_EmpresaSucursal() { IdEmpSucursal = Convert.ToInt32(cbxEmpresaSucursal.SelectedValue) };
                LoadTipoDestino();
            }
        }

        private void CalnpdPrecioUnitarioArtServ(object sender, System.Windows.RoutedEventArgs e)
        {
            var vrObjECMP_CompraDetalle = (ECMP_CompraDetalle)dgCompraDetalle.CurrentCell.Item;
            if (vrObjECMP_CompraDetalle == null) return; 
            vrObjECMP_CompraDetalle.PrecioUnitarioTemp = (!chkIncluyeIGV.IsChecked.Value) ? vrObjECMP_CompraDetalle.PrecioUnitario : (vrObjECMP_CompraDetalle.PrecioUnitario * ((decimal.Round(dmlIGV * 100, 2) + 100) / 100));
            txtExoneradoArtServ.Text = "0.00";

            if (vrObjECMP_CompraDetalle.TipoDetalle == "MNF_Articulo")
                CalcularItemsArticulo(vrObjECMP_CompraDetalle);
            else
                CalcularItemsServicio(vrObjECMP_CompraDetalle);
        }

        private void IsClickCheckBox(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ObjECMP_Compra.ObjEMNF_OperacionMovimiento == null || ObjECMP_Compra.ObjEMNF_MotivoMovimiento == null)
                return;

            string strCodOpeMovimiento = ObjECMP_Compra.ObjEMNF_OperacionMovimiento.CodOpeMovimiento;
            string strCodMotMovimiento = ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento;
            var ListECMP_CompraDetalle = (dgCompraDetalle.Items.OfType<ECMP_CompraDetalle>()).ToList();
            if (((CheckBox)sender).Name == "chkIncluyeIGV") ObjECMP_Compra.IncluyeIGV = ((CheckBox)sender).IsChecked.Value;
            if (strCodOpeMovimiento == "PPHN")
            {
				foreach (var item in ListECMP_CompraDetalle)
				{
					CalcularItemsArticulo(item);
				}
            }
            else if (strCodOpeMovimiento == "PPFC")
            {
				foreach (var item in ListECMP_CompraDetalle)
				{
                    if (strCodMotMovimiento == "ISF" || strCodMotMovimiento == "IDS")
                        CalcularItemsServicio(item);
                    else
                        CalcularItemsArticulo(item);
				}
            }
            else if (strCodOpeMovimiento == "PGID")
            {
				foreach (var item in ListECMP_CompraDetalle)
				{
                    if (strCodMotMovimiento == "GIS")
                        CalcularItemsServicio(item);
                    else
                        CalcularItemsArticulo(item);
				}
            }
        }

        private void chkAfectoDetraccionClick(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ObjECMP_Compra.AfectoDetraccion == false)
            {
                txtDetraccion.Text = "0";
            }
        }

        private void dtgDetalleColumnEliminar_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, CMPMensajes.QuitarItems, CmpButton.AceptarCancelar, () =>
            {
                var vrObjEMNF_DocumentoReferencia = (EMNF_DocumentoReferencia)dgReferencias.SelectedItem;
                if (vrObjEMNF_DocumentoReferencia != null)
                {
                    dgReferencias.Items.Remove(vrObjEMNF_DocumentoReferencia);

                    var vrListECMP_CompraDetalleArticulo = (dgCompraDetalle.Items.OfType<ECMP_CompraDetalle>()).ToList().Where(x => x.IdReferencia != vrObjEMNF_DocumentoReferencia.IdReferencia).ToList();
                    dgCompraDetalle.Items.Clear();
                    if (vrListECMP_CompraDetalleArticulo.Count > 0)
                    {
                        int intItem = 1;
                        foreach (var item in vrListECMP_CompraDetalleArticulo)
                        {
                            item.Item = intItem;
                            dgCompraDetalle.Items.Add(item);
                            intItem++;
                        }
                    }
                    if (cbxDocReferencia.SelectedValue == null) { return; }

                    if ((cbxDocReferencia.SelectedValue.ToString() == "GRC" || cbxDocReferencia.SelectedValue.ToString() == "ODC") && ObjECMP_Compra.ObjEMNF_OperacionMovimiento.CodOpeMovimiento == "PPFC")
                    {
                        CalcularTotalesArticulo();
                    }
                    else if ((cbxDocReferencia.SelectedValue.ToString() == "ODS") && (ObjECMP_Compra.ObjEMNF_OperacionMovimiento.CodOpeMovimiento == "PPHN" || ObjECMP_Compra.ObjEMNF_OperacionMovimiento.CodOpeMovimiento == "PPFC"))
                    {
                        CalcularTotalesServicio();
                    }                    
                }
                if (dgReferencias.Items.Count == 0 && cbxMotivoMovimiento.SelectedValue.ToString() != "IPF")
                {
                    btnAgregarArtServ.IsEnabled = true;
                }
            });
        }

        private void txtSerie_KeyDown_1(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                txtSerieValue();
            }
        }

        private void txtSerie_LostFocus_1(object sender, System.Windows.RoutedEventArgs e)
        {
            txtSerieValue();
        }

        private void txtCorrelativo_KeyDown_1(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                txtNumeroValue();
        }

        private void cbxDocumento_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (cbxDocumento.SelectedValue == null || ObjECMP_Compra.ObjEMNF_MotivoMovimiento == null) return;

            string strCodMovMovimiento = ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento;
            if (strCodMovMovimiento == "ISF" || strCodMovMovimiento == "IDS")
            {
            //    dgCompraDetalle.Columns[9].Header = "IGV";
            //    lblTitleCompra03.Text = "Gravada";
            //    lblTitleCompra04.Text = "IGV " + decimal.Round(dmlIGV * 100, 2) + "%";
            //    lblTitleCompra05.Text = "Importe Total";
            //    ColumnCal01.Visibility = System.Windows.Visibility.Visible;
            //    chkRetencion.Visibility = System.Windows.Visibility.Collapsed;
            //    chkAfectoPercepcion.Visibility = System.Windows.Visibility.Visible;
            //    chkIncluyeIGV.IsEnabled = true;
            //    chkRetencion.IsEnabled = false;
            //    chkAfectoPercepcion.IsEnabled = true;
                if (cbxDocumento.SelectedValue.ToString() == "RCB")
                {
                    lblTitleCompra02.Text = "Otros Cargos";
                    ColumnCal01.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    lblTitleCompra02.Text = "Exonerada";
                }
            }
			if (cbxDocumento.SelectedValue.ToString() == "RIG")
            {
                txtCorrelativo.IsEnabled = false;
				txtSerie.IsEnabled = true;
            }
            else
            {
                txtCorrelativo.IsEnabled = true;
                txtSerie.IsEnabled = true;
            }
        }

        private void txtExoneradoArtServ_KeyUp_1(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (cbxDocumento.SelectedValue == null || ObjECMP_Compra.ObjEMNF_MotivoMovimiento == null) return;

            if (ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "ISF" && cbxDocumento.SelectedValue.ToString() == "RCB")
            {
                txtExoneradoArtServ.IsReadOnly = false;
				if (txtExoneradoArtServ.Text.Trim().Length == 0)
                    txtExoneradoArtServ.Text = "0.00";
                txtTotalNetoArtServ.Text = decimal.Round((decimal.Parse(txtExoneradoArtServ.Text) + decimal.Parse(txtGravadaArtServ.Text) + decimal.Parse(txtTotalIgvArtServ.Text)), 2).ToString();
            }
            else
            {
                txtExoneradoArtServ.IsReadOnly = true;
            }
        }

        private void chkAfectoAlmacenClick(object sender, System.Windows.RoutedEventArgs e)
        {
            if (((CheckBox)sender).Name == "chkDistribucion")
            {
                if (((CheckBox)sender).IsChecked.Value)
                {
                    dgColumnAlmacen.Visibility = System.Windows.Visibility.Collapsed;
                    chkAfectoAlmacen.IsChecked = false;
                    lblTipoDestino.Visibility = System.Windows.Visibility.Visible;
                    cbxTipoDestino.Visibility = System.Windows.Visibility.Visible;
                    
                    dtgColumnaPeriodoCompania.Visibility = System.Windows.Visibility.Visible;
                    var vrObjListECMP_CompraDetalle = (dgCompraDetalle.Items.OfType<ECMP_CompraDetalle>()).ToList();
                    if (vrObjListECMP_CompraDetalle != null && vrObjListECMP_CompraDetalle.Count > 0)
                        LoadTipoDestino();
                }
                else
                {
                    chkAfectoAlmacen.Visibility = (ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "ICD") ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                    lblTipoDestino.Visibility = System.Windows.Visibility.Collapsed;
                    cbxTipoDestino.Visibility = System.Windows.Visibility.Collapsed;
                    dtgColumnaTipoDestino.Visibility = System.Windows.Visibility.Collapsed;
                    dtgColumnaPeriodoCompania.Visibility = System.Windows.Visibility.Collapsed;
                }
                return;
            }
            else if (((CheckBox)sender).Name == "chkAfectoAlmacen")
            {
                if (((CheckBox)sender).IsChecked.Value)
                {
                    lblTipoDestino.Visibility = System.Windows.Visibility.Collapsed;
                    cbxTipoDestino.Visibility = System.Windows.Visibility.Collapsed;
                    dgColumnAlmacen.Visibility = System.Windows.Visibility.Visible;
                    dtgColumnaTipoDestino.Visibility = System.Windows.Visibility.Collapsed;
                    dtgColumnaPeriodoCompania.Visibility = System.Windows.Visibility.Collapsed;
                    chkDistribucion.IsChecked = false;
                    txtGuiaRemision.IsReadOnly = false;
                }
                else
                {
                    dgColumnAlmacen.Visibility = System.Windows.Visibility.Collapsed;
                    txtGuiaRemision.Text = "";
                    txtGuiaRemision.IsReadOnly = true;
                }
            }
        }

        private void cbxTipoDestino_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            LoadTipoDestino();
            var vrObjListECMP_CompraDetalle = (dgCompraDetalle.Items.OfType<ECMP_CompraDetalle>()).ToList();
            foreach (var item in vrObjListECMP_CompraDetalle)
            {
                if (ObjECMP_Compra.Opcion == "I") item.IdDestino = 0;
            }
			if (cbxTipoDestino.SelectedItem != null)
            {
                ObjECMP_Compra.ObjEMNF_TipoDestino = (EMNF_TipoDestino)cbxTipoDestino.SelectedItem;
                ObjECMP_Compra.IncluyeIGV = chkIncluyeIGV.IsChecked.Value;
            }
        }

        private void chkAnticipo_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "IDS" || ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "ISF")
            {
                dgColumnAlmacen.Visibility = (chkAfectoAlmacen.IsChecked.Value) ? System.Windows.Visibility.Visible : dgColumnAlmacen.Visibility = System.Windows.Visibility.Collapsed;
                chkAfectoAlmacen.IsChecked = false;
            }
            else
            {
                chkAfectoAlmacen.IsChecked = (!(chkAnticipo.IsChecked.Value));
                chkAfectoAlmacen.IsEnabled = (!(chkAnticipo.IsChecked.Value));
                chkDistribucion.IsChecked = false;
                chkDistribucion.IsEnabled = (!(chkAnticipo.IsChecked.Value));

                if (chkAnticipo.IsChecked.Value)
                {
                    dtgColumnaTipoDestino.Visibility = System.Windows.Visibility.Collapsed;
                    lblTipoDestino.Visibility = System.Windows.Visibility.Collapsed;
                    cbxTipoDestino.Visibility = System.Windows.Visibility.Collapsed;
                }

                dtgColumnaPeriodoCompania.Visibility = System.Windows.Visibility.Collapsed;
                dgColumnAlmacen.Visibility = (chkAfectoAlmacen.IsChecked.Value) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                btnAnticipo.IsEnabled = (!(chkAnticipo.IsChecked.Value));
            }
        }

        private void btnAnticipo_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ObjECMP_Compra.ObjEMNF_ClienteProveedor == null || txtProveedorRazonSocial.Text.Trim().Length == 0)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "Seleccione un Proveedor", CmpButton.Aceptar);
                return;
            }
            else if (dgCompraDetalle.Items.Count == 0)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "Ingrese como mínimo un [Artículo / Servicio]", CmpButton.Aceptar);
                return;
            }
            
            this.FlyoutIsOpen("PCMP_BuscarAnticipo", ((value) =>
            {
                if (ObjECMP_Compra.Opcion == "I")
                {
                    var vrObjListECMP_CompraDetalleRemove = (dgCompraDetalle.Items.OfType<ECMP_CompraDetalle>()).ToList().Where(x => x.PrecioUnitario < 0);
                    foreach (var remove in vrObjListECMP_CompraDetalleRemove)
                    {
                        dgCompraDetalle.Items.Remove(remove);
                    }
                    lstIdCompra.Clear();
                }
                if (value is PCMP_BuscarAnticipo)
                {
                    var MyPCMP_BuscarAnticipo = (PCMP_BuscarAnticipo)value;
                    var MyFlyout = (PCMP_BuscarAnticipo)value;
                    MyPCMP_BuscarAnticipo.MySelectItem = (item) =>
                    {
                        AddItem((ECMP_Compra)item);
                        MyPCMP_BuscarAnticipo.IsOpen = false;
                    };
                    MyPCMP_BuscarAnticipo.InitializePCMP_BuscarAnticipo(ObjECMP_Compra);
                    if (ObjECMP_Compra.Opcion != "I")
                        MyPCMP_BuscarAnticipo.LoadDetail(ObjECMP_Compra);
                    MyPCMP_BuscarAnticipo.IsOpen = true;
                }
            }));
        }

        private void chkRetencion_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            ObjECMP_Compra.Retencion = chkRetencion.IsChecked.Value;
            var vrListCompraDetalle = (dgCompraDetalle.Items.OfType<ECMP_CompraDetalle>()).ToList();
            
            boolRetencion = (chkRetencion.IsChecked.Value);
            foreach (var item in vrListCompraDetalle)
            {
                CalcularItemsServicio(item);
            } 
        }

        private void chkPlanilla_Checked_1(object sender, System.Windows.RoutedEventArgs e)
        {
            if (chkIncluyeIGV.IsChecked.Value)
            {
                foreach (var item in dgCompraDetalle.Items)
                {
                    ((ECMP_CompraDetalle)item).PrecioUnitario = ((ECMP_CompraDetalle)item).PrecioUnitarioTemp;
                }
                chkIncluyeIGV.IsChecked = false;
                ObjECMP_Compra.IncluyeIGV = false;
                dgCompraDetalle.Items.Refresh();
            }

            if (chkPlanilla.IsChecked.Value)
            {
                dgDetalleArtServColumnCantidad.Header = "Sueldo Bruto";
                dgPrecioUnitario.Header = "Descuento";
                dgImporte.Header = "Sueldo Neto";
                dgIGV.Visibility = System.Windows.Visibility.Collapsed;
                ColumnCal01.Visibility = System.Windows.Visibility.Collapsed;
                chkIncluyeIGV.IsEnabled = false;
            }
            else
            {
                dgDetalleArtServColumnCantidad.Header = "Cantidad";
                dgPrecioUnitario.Header = "Precio Unitario";
                dgImporte.Header = "Importe";
                dgIGV.Visibility = System.Windows.Visibility.Visible;
                ColumnCal01.Visibility = System.Windows.Visibility.Visible;
                chkIncluyeIGV.IsEnabled = true;
            }

            if (dgCompraDetalle.Items.Count > 0)
                foreach (var item in dgCompraDetalle.Items)
                {
                    CalcularItemsServicio((ECMP_CompraDetalle)item);
                }
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
                if (dgCompraDetalle.Items.Count <= 0)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, "Ingrese Artículo para poder editar este campo.", CmpButton.Aceptar);
                    return;
                }
                else
                {
                    var vrObjListECMP_CompraDetalle = new List<ECMP_CompraDetalle>();
                    vrObjListECMP_CompraDetalle = (dgCompraDetalle.Items.OfType<ECMP_CompraDetalle>()).ToList();
                    vrObjListECMP_CompraDetalle.ForEach((f) =>
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

                blIsGravada = (((TextBox)sender).Name == "txtGravadaArtServ");
                MyMtdCalculosTotales.Calcular(txtGravadaArtServ, txtTotalIgvArtServ, blIsGravada, () =>
                {
                    if (blIsGravada)
                    {
                        txtGravadaArtServ.IsReadOnly = false;
                        strTempValueTitle = lblTitleCompra03.Text;
                        lblTitleCompra03.Text += " (Enter)";
                        lblTitleCompra03.Foreground = Brushes.Orange;
                    }
                    else
                    {
                        txtTotalIgvArtServ.IsReadOnly = false;
                        strTempValueTitle = lblTitleCompra04.Text;
                        lblTitleCompra04.Text += " (Enter)";
                        lblTitleCompra04.Foreground = Brushes.Orange;
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
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    if (blIsGravada)
                    {
                        lblTitleCompra03.Text = strTempValueTitle;
                        txtTotalNetoArtServ.Focus();
                        lblTitleCompra03.Foreground = Brushes.White;
                    }
                    else
                    {
                        lblTitleCompra04.Text = strTempValueTitle;
                        txtTotalNetoArtServ.Focus();
                        lblTitleCompra04.Foreground = Brushes.White;
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
            indexOfTitleGravada = lblTitleCompra03.Text.IndexOf("(");
            if (indexOfTitleGravada > 0)
            {
                strTempValueTitle = lblTitleCompra03.Text.Substring(0, indexOfTitleGravada - 1);
                lblTitleCompra03.Text = strTempValueTitle;
            }
            indexOfTitleIgv = lblTitleCompra04.Text.IndexOf("(");
            if (indexOfTitleIgv > 0)
            {
                strTempValueTitle = lblTitleCompra04.Text.Substring(0, indexOfTitleIgv - 1);
                lblTitleCompra04.Text = strTempValueTitle;
            }
            txtGravadaArtServ.IsReadOnly = true;
            txtTotalIgvArtServ.IsReadOnly = true;

            #endregion

            blIsGravada = (((TextBox)sender).Name == "txtGravadaArtServ");
            if (blIsGravada)
            {
                strTempValueTitle = lblTitleCompra03.Text;
                lblTitleCompra03.Text = strTempValueTitle;
                lblTitleCompra03.Foreground = Brushes.White;
                MyMtdCalculosTotales.Calcular(txtGravadaArtServ, txtTotalIgvArtServ, blIsGravada, () =>
                {
                    txtGravadaArtServ.IsReadOnly = true;
                });
            }
            else
            {
                strTempValueTitle = lblTitleCompra04.Text;
                lblTitleCompra04.Text = strTempValueTitle;
                lblTitleCompra04.Foreground = Brushes.White;
                MyMtdCalculosTotales.Calcular(txtGravadaArtServ, txtTotalIgvArtServ, blIsGravada, () =>
                {
                    txtTotalIgvArtServ.IsReadOnly = true;
                });
            }
            txtTotalNetoArtServ.Focus();

            if (ObjECMP_Compra.ObjEMNF_OperacionMovimiento.CodOpeMovimiento == "PPHN")
                MyMtdCalculosTotales.NewValue(txtGravadaArtServ, txtTotalIgvArtServ, Convert.ToDecimal(txtTotalNetoArtServ.Text), SumarOrRestarGravadaTotalIGV.RESTAR);
            else
                MyMtdCalculosTotales.NewValue(txtGravadaArtServ, txtTotalIgvArtServ, Convert.ToDecimal(txtTotalNetoArtServ.Text));

            txtTotalNetoArtServ.Text = (Convert.ToDecimal(txtGravadaArtServ.Text) + Convert.ToDecimal(txtTotalIgvArtServ.Text) + Convert.ToDecimal(txtExoneradoArtServ.Text)).ToString("###,###,##0.#0");
        }

        public void btnAgregarItemsIsClicked()
        {
            if (!btnAgregarArtServ.IsEnabled)
                return;

            if (ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == null) 
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "Seleccione una Motivo de Movimiento.", CmpButton.Aceptar);
                return;
            }
            else if (ObjECMP_Compra.ObjEMNF_OperacionMovimiento.CodOpeMovimiento == null)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "Seleccione una Operación de Movimiento.", CmpButton.Aceptar);
                return;
            }
            else if (ObjECMP_Compra.ObjEMNF_ClienteProveedor == null)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "Seleccione un Proveedor.", CmpButton.Aceptar);
                return;
            }

            string strCodMotMovimiento = ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento;

            if (ObjECMP_Compra.ObjEMNF_OperacionMovimiento.CodOpeMovimiento == "PPFC" || ObjECMP_Compra.ObjEMNF_OperacionMovimiento.CodOpeMovimiento == "PGID")
            {
                if (strCodMotMovimiento == "ISF" || strCodMotMovimiento == "IDS" || strCodMotMovimiento == "GIS" || strCodMotMovimiento == "IDP")
                {
                    if (strCodMotMovimiento == "GIS" && Convert.ToString(cbxDocReferencia.SelectedValue )== "NGN" && txtSerie.Text.Length == 0)
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminProviDocumento, "Ingrese la Serie del Documento", CmpButton.Aceptar);
                        return;
                    }
                    MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminCompra, CMPMensajes.GetAccesoRestringidoNull("Servicios"), new Action<ESGC_PermisoPerfil>((P) =>
                    {
                        if (P.Consulta)
                        {
                            this.FlyoutIsOpen("PMNF_BuscarServicios", ((value) => 
                            {
                                if (value is PMNF_BuscarServicios)
                                {
                                    var MyPMNF_BuscarServicios = (PMNF_BuscarServicios)value;
                                    MyPMNF_BuscarServicios.InitializePMNF_BuscarServicios();
                                    MyPMNF_BuscarServicios.LoadHeader(OperacionIGV.Todo);
                                    MyPMNF_BuscarServicios.IsOpen = true;
                                }                                
                            }));
                        }
                        else
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, CMPMensajes.GetAccesoRestringidoBuscar("Servicios"), CmpButton.Aceptar);
                        }
                    }));
                }
                else
                {
                    MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminCompra, CMPMensajes.GetAccesoRestringidoNull("Artículos"), new Action<ESGC_PermisoPerfil>((P) =>
                    {
                        if (P.Consulta)
                        {
                            this.FlyoutIsOpen("PMNF_BuscarArticulos", ((value) => 
                            {
                                if (value is PMNF_BuscarArticulos)
                                {
                                    var MyPMNF_BuscarArticulos = (PMNF_BuscarArticulos)value;
                                    MyPMNF_BuscarArticulos.InitializePMNF_BuscarArticulos();
                                    MyPMNF_BuscarArticulos.LoadHeader();
                                    MyPMNF_BuscarArticulos.IsOpen = true;
                                }
                            }));
                        }
                        else
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, CMPMensajes.GetAccesoRestringidoBuscar("Artículos"), CmpButton.Aceptar);
                        }
                    }));
                }
            }
            else if (ObjECMP_Compra.ObjEMNF_OperacionMovimiento.CodOpeMovimiento == "PPHN")
            {
                MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminCompra, CMPMensajes.GetAccesoRestringidoNull("Servicios"), new Action<ESGC_PermisoPerfil>((P) =>
                {
                    if (P.Consulta)
                    {
                        this.FlyoutIsOpen("PMNF_BuscarServicios", ((value) =>
                        {
                            if (value is PMNF_BuscarServicios)
                            {
                                var MyPMNF_BuscarServicios = (PMNF_BuscarServicios)value;
                                MyPMNF_BuscarServicios.InitializePMNF_BuscarServicios();
                                MyPMNF_BuscarServicios.LoadHeader(OperacionIGV.Todo);
                                MyPMNF_BuscarServicios.IsOpen = true;
                            }
                        }));
                    }
                    else
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, CMPMensajes.GetAccesoRestringidoBuscar("Servicios"), CmpButton.Aceptar);
                    }
                }));
            }
        }

        public void btnQuitarItemsIsClicked()
        {
            if (!btnQuitarArtServ.IsEnabled || !(dgCompraDetalle.CurrentCell.Item is ECMP_CompraDetalle))
                return;

            if (dgCompraDetalle.Items.Count <= 0 || dgCompraDetalle.SelectedCells.Count == 0 || cbxOperacionMovimiento.SelectedValue == null) return;
            var resutl = (ECMP_CompraDetalle)dgCompraDetalle.CurrentCell.Item;
            if (resutl == null && resutl.PrecioUnitario < 0) { return; }

            if (cbxOperacionMovimiento.SelectedValue.ToString() == "PPFC" || ObjECMP_Compra.ObjEMNF_OperacionMovimiento.CodOpeMovimiento == "PGID")
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, CMPMensajes.QuitarItems, CmpButton.AceptarCancelar, () =>
                {
                    dgCompraDetalle.Items.Remove(resutl);
                    CalcularTotalesArticulo();
                    var ObjListEIngresoSalidaDetalle = (dgCompraDetalle.Items.OfType<ECMP_CompraDetalle>()).ToList();
                    dgCompraDetalle.Items.Clear();

                    int intItem = 1;
                    foreach (var item in ObjListEIngresoSalidaDetalle)
                    {
                        item.Item = intItem;
                        dgCompraDetalle.Items.Add(item);
                        intItem++;
                    }
                });
            }
            else if (cbxOperacionMovimiento.SelectedValue.ToString() == "PPHN")
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, CMPMensajes.QuitarItems, CmpButton.AceptarCancelar, () =>
                {
                    dgCompraDetalle.Items.Remove(resutl);
                    CalcularTotalesServicio();
                    var ObjListEIngresoSalidaDetalle = (dgCompraDetalle.Items.OfType<ECMP_CompraDetalle>()).ToList();
                    dgCompraDetalle.Items.Clear();

                    int intItem = 1;
                    foreach (var item in ObjListEIngresoSalidaDetalle)
                    {
                        item.Item = intItem;
                        dgCompraDetalle.Items.Add(item);
                        intItem++;
                    }
                });
            }
        }

        public void btnImprimirIsClicked()
        {
            if (!btnImprimirArtServ.IsEnabled)
                return;

            ImprimirCompra();
        }
       
        public void btnGuardarIsClicked()
        {
            if (!btnGuardarArtServ.IsEnabled)
                return;
            
            if (ValidaGIS())
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, Message, CmpButton.Aceptar);
                return;
            }   

            MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminCompra, CMPMensajes.GetAccesoRestringidoNull("Compra"), new Action<ESGC_PermisoPerfil>((P) =>
            {
                if (P.Nuevo || P.Editar)
                {
                    if (ValidaDatos()) { return; }

                    var varObjECMP_Compra = (ECMP_Compra)MyHeader.DataContext;
                    varObjECMP_Compra.Opcion = ObjECMP_Compra.Opcion;
                    varObjECMP_Compra.ObjEMNF_ClienteProveedor = ObjECMP_Compra.ObjEMNF_ClienteProveedor;
                    varObjECMP_Compra.ObjESGC_Moneda = ObjECMP_Compra.ObjESGC_Moneda;
                    varObjECMP_Compra.ObjEMNF_OperacionMovimiento = ObjECMP_Compra.ObjEMNF_OperacionMovimiento;
                    varObjECMP_Compra.AfectoPercepcion = chkAfectoPercepcion.IsChecked.Value;
                    varObjECMP_Compra.CodDocumento = cbxDocumento.SelectedValue.ToString();
                    varObjECMP_Compra.Gravada = Convert.ToDecimal(txtGravadaArtServ.Text);
                    varObjECMP_Compra.Exonerada = Convert.ToDecimal(txtExoneradoArtServ.Text);
                    varObjECMP_Compra.ImporteIGV = Convert.ToDecimal(txtTotalIgvArtServ.Text);
                    varObjECMP_Compra.Periodo = cbxPeriodo.SelectedValue.ToString();
                    varObjECMP_Compra.AfectaAlmacen = chkAfectoAlmacen.IsChecked.Value;
                    varObjECMP_Compra.IncluyeIGV= chkIncluyeIGV.IsChecked.Value;
                    varObjECMP_Compra.Planilla = chkPlanilla.IsChecked.Value;
                    if (cbxTipoDestino.SelectedValue != null)
                        varObjECMP_Compra.ObjEMNF_TipoDestino.CodTipoDestino = cbxTipoDestino.SelectedValue.ToString();
                    if (cbxDocReferencia.SelectedValue != null)
                        varObjECMP_Compra.CodDocumentoRef = cbxDocReferencia.SelectedValue.ToString();
                    varObjECMP_Compra.Anticipo = chkAnticipo.IsChecked.Value;
                    
                    if (cbxOperacionMovimiento.SelectedValue.ToString() == "PPFC")
                    {
                        varObjECMP_Compra.IGV = (cbxMotivoMovimiento.SelectedValue.ToString() == "IDP" || cbxMotivoMovimiento.SelectedValue.ToString() == "IPF") ? dmlRentaSegunda : dmlIGV;
                    }
                    else if (cbxOperacionMovimiento.SelectedValue.ToString() == "PPHN")
                    {
                        varObjECMP_Compra.IGV = (varObjECMP_Compra.Opcion == "I") ? SGCMethod.GetTasaHonorario(varObjECMP_Compra.Gravada) / 100 : dmlIGV;
                    }
                    else if (cbxOperacionMovimiento.SelectedValue.ToString() == "PGID")
                    {
                        varObjECMP_Compra.IGV = dmlIGV;
                        varObjECMP_Compra.CajaBanco = chkCajaBanco.IsChecked.Value;
                    }

                    varObjECMP_Compra.CadenaXML = XmlDetalleCompra();
                    varObjECMP_Compra.DocumentoRefXML = XmlDocumentoRef();
                    varObjECMP_Compra.CompraAnticipoXML = XmlCompraAnticipo();

                    string strOutMessageError = string.Empty;
                    CmpMessageBox.Proccess(CMPMensajes.TitleAdminCompra, CMPMensajes.ProcesandoDatos, () =>
                    {
                        try
                        {
                            new BCMP_Compra().TransCompra(varObjECMP_Compra);
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
                            CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, strOutMessageError, CmpButton.Aceptar);
                        }
                        else
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, CMPMensajes.DatoProcesados + " \n¿Desea Imprimir el documento de Compra?", CmpButton.AceptarCancelar, () =>
                            {
                                ImprimirCompra();
                                btnSalirIsClicked();
                            }, () => 
                            {
                                btnSalirIsClicked();
                            });
                            
                        }
                    });
                }
                else
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, CMPMensajes.GetAccesoRestringidoNuevo("Compra"), CmpButton.Aceptar);
                }
            }));
        }

        public void btnSalirIsClicked()
        {
            if (!btnSalirArtServ.IsEnabled)
                return;

            MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminCompra, CMPMensajes.GetAccesoRestringidoNull("Compra"), ((P) =>
            {
                new CmpNavigationService().Volver(_MyFrame, new ECMP_Compra());
            }),
            MyNameFomulario: "PCMP_ListadoCompra",
            MyActionAbort: () =>
            {
                this.Close(TipoModulo.ManuFactura);
            });
        }

        #endregion

        #region MÉTODO DE LA CLASE

        private void AddFlyout()
        {

            #region BUSCAR ARTÍCULO
            PMNF_BuscarArticulos MyPMNF_BuscarArticulos = new PMNF_BuscarArticulos();
            MyPMNF_BuscarArticulos.IsSelected += new PMNF_BuscarArticulos.isSelected((value) => { AddItemsArticulos(value, new ECMP_CompraDetalle()); });
         
            #endregion

            #region BUSCAR PROVEEDOR

            PMNF_BuscarClienteProveedor MyPMNF_BuscarClienteProveedor = new PMNF_BuscarClienteProveedor();
            MyPMNF_BuscarClienteProveedor.IsSelected += new PMNF_BuscarClienteProveedor.isSelected((value) => { AddValueProveedor(value);});
         
            #endregion

            #region BUSCAR ORDEN SERVICIO

            PCMP_BuscarOrdenServicio MyPCMP_BuscarOrdenServicio = new PCMP_BuscarOrdenServicio();
            MyPCMP_BuscarOrdenServicio.IsSelected += new PCMP_BuscarOrdenServicio.isSelected((value) => { AddValueOrdenServicio(value); });

            #endregion

            #region BUSCAR RECEPCION GUIA

            PALM_BuscarRecepcionGuia MyPALM_BuscarRecepcionGuia = new PALM_BuscarRecepcionGuia();
            MyPALM_BuscarRecepcionGuia.IsSelected += new PALM_BuscarRecepcionGuia.isSelected((value) => { AddValueRecepcionGuia(value); });
            

            #endregion

            #region BUSCAR SERVICIO

            PMNF_BuscarServicios MyPMNF_BuscarServicios = new PMNF_BuscarServicios();
            MyPMNF_BuscarServicios.IsSelected += new PMNF_BuscarServicios.isSelected((value) => { 
                AddItemsServicio(value,new ECMP_CompraDetalle(),true, true, true);
                
            });
            

            #endregion

            #region BUSCAR ORDEN DE COMPRA

            PCMP_BuscarOrdenCompra MyPCMP_BuscarOrdenCompra = new PCMP_BuscarOrdenCompra();
            MyPCMP_BuscarOrdenCompra.IsSelected += new PCMP_BuscarOrdenCompra.isSelected((value3) => { AddValueOrdenCompra(value3); });
            //MyPCMP_BuscarOrdenCompra.MyIsOpenChanged(ObjCmpButtonTitleTecla);

            #endregion

            #region BUSCAR COMPRA
            PCMP_BuscarCompra MyPCMP_BuscarCompra = new PCMP_BuscarCompra();
            //MyPCMP_BuscarOrdenCompra.IsSelected += new PCMP_BuscarOrdenCompra.isSelected((value3) => { AddValueOrdenCompra(value3); });
	        #endregion

            #region BUSCAR DOCUMENTO

            PCMP_BuscarAnticipo MyPCMP_Compra = new PCMP_BuscarAnticipo();

            #endregion

            this.FlyoutInitialize();
            this.FlyoutAdd(MyPMNF_BuscarArticulos);
            this.FlyoutAdd(MyPMNF_BuscarClienteProveedor);
            this.FlyoutAdd(MyPCMP_BuscarOrdenServicio);
            this.FlyoutAdd(MyPALM_BuscarRecepcionGuia);
            this.FlyoutAdd(MyPMNF_BuscarServicios);
            this.FlyoutAdd(MyPCMP_BuscarOrdenCompra);
            this.FlyoutAdd(MyPCMP_BuscarCompra);
            this.FlyoutAdd(MyPCMP_Compra);

        }

        private void AddItem(ECMP_Compra objECMP_CompraAnticipo)
        {
            lstIdCompra.Add(objECMP_CompraAnticipo.IdCompra);
            if (lstIdCompra.Count != 0 || ObjECMP_Compra.Opcion != "I")
                chkAnticipo.IsEnabled = false;
            var vrListECMP_CompraDetalle = new List<ECMP_CompraDetalle>();
            vrListECMP_CompraDetalle = new BCMP_CompraDetalle().ListAdministrarCompraDetalle(objECMP_CompraAnticipo);
            vrListECMP_CompraDetalle.ForEach(x => 
            { 
                x.PrecioUnitario = (x.PrecioUnitario * -1);
                x.PrecioUnitarioTemp = (x.PrecioUnitarioTemp * -1);
                dgCompraDetalle.Items.Add(x);
            });
            dgCompraDetalle.Items.Refresh();
            var vrObjListECMP_CompraDetalle = (dgCompraDetalle.Items.OfType<ECMP_CompraDetalle>()).ToList();
            int items = 0;
            chkIncluyeIGV.IsChecked = objECMP_CompraAnticipo.IncluyeIGV;
            chkIncluyeIGV.IsEnabled = false;
            foreach (var item in vrObjListECMP_CompraDetalle)
            {
                items++;
                item.Item = items;
                CalcularItemsArticulo(item);
            }
        }

        /// <summary>
        /// Búsqueda general de Recepcion de Guia
        /// </summary>
        private void BuscarRecepcionGuia()
        {
            string strFiltro = txtDocumentoRef.Text;
            var vrObjEALM_IngresoSalida = new EALM_IngresoSalida()
            {
                Numero = string.Empty,
                ObjEMNF_OperacionMovimiento = new EMNF_OperacionMovimiento()
                {
                    CodOpeMovimiento = "IALM"
                },
                ObjEMNF_MotivoMovimiento = new EMNF_MotivoMovimiento()
                {
                    CodMotMovimiento = "ICA"
                },
                ObjEMNF_ClienteProveedor = ObjECMP_Compra.ObjEMNF_ClienteProveedor,
                ObjESGC_Moneda = ObjECMP_Compra.ObjESGC_Moneda
            };

            string strOutMessageError = string.Empty;
            CmpTask.Process(
            () =>
            {
                try
                {
                    ListEALM_IngresoSalida = new BALM_IngresoSalida().ListarIngresoSalida(vrObjEALM_IngresoSalida, "0", DateTime.Now, DateTime.Now).Where(x => x.Provisionado == 0 && x.ObjESGC_Estado.CodEstado == "PECIS").ToList();
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
                    CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, strOutMessageError, CmpButton.Aceptar);
                }
                else
                {
                    if (ListEALM_IngresoSalida.Count == 1)
                    {
                        vrObjEALM_IngresoSalida = ListEALM_IngresoSalida.FirstOrDefault();
                        AddValueRecepcionGuia(vrObjEALM_IngresoSalida);
                    }
                    else
                    {
                        this.FlyoutIsOpen("PALM_BuscarRecepcionGuia", ((value) => 
                        {
                            if (value is PALM_BuscarRecepcionGuia)
                            {
                                var FlyoutsPALM_BuscarRecepcionGuia = (PALM_BuscarRecepcionGuia)value;
                                FlyoutsPALM_BuscarRecepcionGuia.InitializePALM_BuscarRecepcionGuia(vrObjEALM_IngresoSalida);
                                FlyoutsPALM_BuscarRecepcionGuia.SetValueFilter = strFiltro;
                                FlyoutsPALM_BuscarRecepcionGuia.SetListEALM_IngresoSalida = ListEALM_IngresoSalida;
                                FlyoutsPALM_BuscarRecepcionGuia.IsOpen = true;
                            }
                        }));
                       
                    }
                }
            });
        }

        /// <summary>
        /// Búsqueda general de Orden de Compras
        /// </summary>
        private void BuscarOrdenCompra()
        {
            string strFiltro = txtDocumentoRef.Text;

            string strOutMessageError = string.Empty;
            CmpTask.Process(
            () =>
            {
                try
                {
                    ListECMP_OrdenCompra = new BCMP_OrdenCompra().ListOrdenCompra(new ECMP_OrdenCompra()
                    {
                        Opcion = "D",
                        ObjEMNF_ClienteProveedor = ObjECMP_Compra.ObjEMNF_ClienteProveedor,
                        Fecha = DateTime.Now,
                        FechaEntrega = DateTime.Now,
                        ObjESGC_Estado = new ESGC_Estado()
                        {
                            CodEstado = "CMP"
                        },
                        ObjESGC_Moneda = ObjECMP_Compra.ObjESGC_Moneda,
                    }, strFiltro).Where(x => x.Provisionado == 0).ToList();
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
                    CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, strOutMessageError, CmpButton.Aceptar);
                }
                else
                {
                    if (ListECMP_OrdenCompra.Count == 1)
                    {
                        var ObjECMP_OrdenCompra = ListECMP_OrdenCompra.FirstOrDefault();
                        AddValueOrdenCompra(ObjECMP_OrdenCompra);
                    }
                    else
                    {
                        this.FlyoutIsOpen("PCMP_BuscarOrdenCompra", ((value) =>
                        {
                            if (value is PCMP_BuscarOrdenCompra)
                            {
                                var FlyoutsPCMP_BuscarOrdenCompra = (PCMP_BuscarOrdenCompra)value;
                                FlyoutsPCMP_BuscarOrdenCompra.InitializePCMP_BuscarOrdenCompra(new ECMP_OrdenCompra()
                                {
                                    Opcion = "D",
                                    ObjESGC_Estado = new ESGC_Estado() { CodEstado = "CMP" },
                                    ObjESGC_Moneda = ObjECMP_Compra.ObjESGC_Moneda,
                                    ObjEMNF_ClienteProveedor = ObjECMP_Compra.ObjEMNF_ClienteProveedor
                                });
                                FlyoutsPCMP_BuscarOrdenCompra.SetValueFilter = strFiltro;
                                FlyoutsPCMP_BuscarOrdenCompra.SetListECMP_OrdenCompra = ListECMP_OrdenCompra;
                                FlyoutsPCMP_BuscarOrdenCompra.LoadDetail();
                                FlyoutsPCMP_BuscarOrdenCompra.IsOpen = true;
                            }
                        }));
                    }
                }
            });
        }

        /// <summary>
        /// Búsqueda general de Ordenes de Servicio
        /// </summary>
        private void BuscarOrdenServicio()
        {
            string strFiltro = txtDocumentoRef.Text;

            string strOutMessageError = string.Empty;
            CmpTask.Process(
            () =>
            {
                try
                {
                    ListECMP_OrdenServicio = new BCMP_OrdenServicio().ListFiltrarOrdenServicio(new ECMP_OrdenServicio()
                    {
                        Opcion = "D",
                        ObjEMNF_ClienteProveedor = ObjECMP_Compra.ObjEMNF_ClienteProveedor,
                        ObjESGC_Moneda = ObjECMP_Compra.ObjESGC_Moneda,
                        FechaInicio = DateTime.Now,
                        FechaFin = DateTime.Now,
                        ObjESGC_Estado = new ESGC_Estado() 
                        {
                            CodEstado = "APCOS"
                        }
                    }, strFiltro);
                    System.Threading.Thread.Sleep(350);

                    if (ObjECMP_Compra.ObjEMNF_OperacionMovimiento.CodOpeMovimiento == "PPFC")
                    {
                        if (ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "IPF")
                            ListECMP_OrdenServicio = ListECMP_OrdenServicio.Where(x => x.Exonerado == 11 && x.Retencion == true).ToList();
                        else
                            ListECMP_OrdenServicio = ListECMP_OrdenServicio.Where(x => x.Exonerado == 11 || x.Exonerado == 12 || x.Exonerado == 22).ToList();
                    }
                    else
                    {
                        ListECMP_OrdenServicio = ListECMP_OrdenServicio.Where(x => x.Exonerado == 21).ToList();
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
                    CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, strOutMessageError, CmpButton.Aceptar);
                }
                else
                {
                    if (ListECMP_OrdenServicio.Count == 1)
                    {
                        var ObjECMP_OrdenServicio = ListECMP_OrdenServicio.FirstOrDefault();
                        AddValueOrdenServicio(ObjECMP_OrdenServicio);
                    }
                    else
                    {
                        this.FlyoutIsOpen("PCMP_BuscarOrdenServicio", ((value) =>
                        {
                            if (value is PCMP_BuscarOrdenServicio)
                            {
                                var FlyoutsPCMP_BuscarOrdenServicio = (PCMP_BuscarOrdenServicio)value;
                                FlyoutsPCMP_BuscarOrdenServicio.InitializePCMP_BuscarOrdenServicio(
                                ((cbxOperacionMovimiento.SelectedIndex == 1) 
                                ? 
                                    ((ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "IPF") ? TypeFilterServicio.FacturaRetencion : TypeFilterServicio.Factura) 
                                :
                                    TypeFilterServicio.Honorario),
                                new ECMP_OrdenServicio()
                                {
                                    Opcion = "D",
                                    ObjEMNF_ClienteProveedor = ObjECMP_Compra.ObjEMNF_ClienteProveedor,
                                    ObjESGC_Moneda = ObjECMP_Compra.ObjESGC_Moneda,
                                    FechaInicio = DateTime.Now,
                                    FechaFin = DateTime.Now,
                                    ObjESGC_Estado = new ESGC_Estado()
                                    {
                                        CodEstado = "APCOS"
                                    }
                                });
                                FlyoutsPCMP_BuscarOrdenServicio.SetValueFilter = strFiltro;
                                //FlyoutsPCMP_BuscarOrdenServicio.SetListECMP_OrdenServicio = ListECMP_OrdenServicio;
                                FlyoutsPCMP_BuscarOrdenServicio.LoadDetail();
                                FlyoutsPCMP_BuscarOrdenServicio.IsOpen = true;
                            }
                        }));
                    }
                }
            });
        }

        /// <summary>
        /// Instancia y pinta valor del Orden Compra seleccionado de la busqueda
        /// </summary>
        /// <param name="ObjEALM_IngresoSalida">Objeto de la clase Orden Compra</param>
        public void AddValueRecepcionGuia(EALM_IngresoSalida ObjEALM_IngresoSalida)
        {
            if (ObjEALM_IngresoSalida != null)
            {
                var vrListEMNF_DocumentoReferencia = (dgReferencias.Items.OfType<EMNF_DocumentoReferencia>()).ToList();

                if (vrListEMNF_DocumentoReferencia.Count > 0 && chkIncluyeIGV.IsChecked.Value != ObjEALM_IngresoSalida.IncluyeIGV)
                {
                    string Mensaje = (ObjEALM_IngresoSalida.IncluyeIGV) ? "Debe de incluir Ig" : " no debe de incluir IG";
                    CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "EL documento de referencia a agregar " + Mensaje  , CmpButton.Aceptar);
                    return;
                }

                bool existeArticulo = vrListEMNF_DocumentoReferencia.Exists(x => x.IdReferencia == ObjEALM_IngresoSalida.IdIngresoSalida);
                if (existeArticulo)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "El item seleccionado ya existe", CmpButton.Aceptar);
                    return;
                }

                chkIncluyeIGV.IsChecked = ObjEALM_IngresoSalida.IncluyeIGV;

                dgReferencias.Items.Add(new EMNF_DocumentoReferencia()
                {
                    IdReferencia = ObjEALM_IngresoSalida.IdIngresoSalida,
                    SerieNumero = ObjEALM_IngresoSalida.Serie + " - " + ObjEALM_IngresoSalida.Numero,
                    GuiaRemision = ObjEALM_IngresoSalida.GuiaRemision
                });
                cbxMoneda.SelectedValue = ObjEALM_IngresoSalida.ObjESGC_Moneda.CodMoneda;
                dmlTipoCambio = ObjEALM_IngresoSalida.TipoCambio;
                LoadRecepcionGuiaDetail(ObjEALM_IngresoSalida);                
            }
        }

        /// <summary>
        /// Carga datos del Recepción Guía
        /// </summary>
        private void LoadRecepcionGuiaDetail(EALM_IngresoSalida ObjEALM_IngresoSalida)
        {
            var vrListEALM_SalidaConsumoDetalle = new List<EALM_IngresoSalidaDetalle>();

            string strOutMessageError = string.Empty;
            CmpTask.Process(
            () =>
            {
                try
                {
                    vrListEALM_SalidaConsumoDetalle = new BALM_IngresoSalidaDetalle().ListAdministrarIngresoSalidaDetalle(ObjEALM_IngresoSalida);
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
                    CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, strOutMessageError, CmpButton.Aceptar);
                }
                else
                {
                    foreach (var item in vrListEALM_SalidaConsumoDetalle)
                    {
                        if (item.ObjEMNF_Articulo.ObjEMNF_OperacionIGV.CodOperacionIGV == "GB")
                        {
                            if (chkIncluyeIGV.IsChecked.Value)
                            {
                                //Precio Incluye IGV
								decimal dmlCalculoIGV = decimal.Round((item.PrecioUnitario * dmlIGV), 10);
                                item.PrecioUnitario = dmlCalculoIGV + item.PrecioUnitario;
                            }
                        }

                        item.MaxCantidad = item.Cantidad;

                        var vrObjEALM_IngresoSalidaDetalle = new ECMP_CompraDetalle()
                        {
                            ObjECMP_Compra = ObjECMP_Compra,
                            ObjEALM_Almacen = ObjEALM_IngresoSalida.ObjEALM_Almacen,
                            IdReferencia = ObjEALM_IngresoSalida.IdIngresoSalida,
                            Item = item.Item,
                            Cantidad = item.Cantidad,
                            PrecioUnitario = item.PrecioUnitario,
                            PrecioUnitarioTemp = (!chkIncluyeIGV.IsChecked.Value) ? item.PrecioUnitario : (item.PrecioUnitario * ((decimal.Round(dmlIGV * 100, 2) + 100) / 100)),
                            MaxCantidad = item.Cantidad,
                            IsEnabledColumnPrecioUnitario = false,
                            IsEnabledColumnAlmcen = false,
                            SerieNumero = ObjEALM_IngresoSalida.Serie + " - " + ObjEALM_IngresoSalida.Numero,
                            ListEALM_Almacen = ListEALM_Almacen
                        };
                        AddItemsArticulos(item.ObjEMNF_Articulo, vrObjEALM_IngresoSalidaDetalle);
                        CalcularItemsArticulo(vrObjEALM_IngresoSalidaDetalle);
                    }
                    LoadTipoDestino();
                    btnAgregarArtServ.IsEnabled = false;
                    btnQuitarArtServ.IsEnabled = false;
                }
            });
        }

        /// <summary>
        /// Instancia y pinta valor del Orden Servicio seleccionado de la busqueda
        /// </summary>
        /// <param name="ObjECMP_OrdenServicio">Objeto de la clase Orden Servicio</param>
        public void AddValueOrdenServicio(ECMP_OrdenServicio ObjECMP_OrdenServicio)
        {
            if (ObjECMP_OrdenServicio != null)
            {
                var vrListEMNF_DocumentoReferencia = (dgReferencias.Items.OfType<EMNF_DocumentoReferencia>()).ToList();
                bool existeArticulo = vrListEMNF_DocumentoReferencia.Exists(x => x.IdReferencia == ObjECMP_OrdenServicio.IdOrdenServicio);
                if (existeArticulo)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "El item seleccionado ya existe", CmpButton.Aceptar);
                    return;
                }
                if (intExoneradoIGV != ObjECMP_OrdenServicio.Exonerado && dgReferencias.Items.Count > 0)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "El tipo de tipo de Operación IGV debe ser igual al Registro ya seleccionado", CmpButton.Aceptar);
                    return;
                }

                dgReferencias.Items.Add(new EMNF_DocumentoReferencia() 
                {
                    IdReferencia = ObjECMP_OrdenServicio.IdOrdenServicio,
                    SerieNumero = ObjECMP_OrdenServicio.Serie + " - " + ObjECMP_OrdenServicio.Numero
                });
                LoadTipoDestino();
                cbxMoneda.SelectedValue = ObjECMP_OrdenServicio.ObjESGC_Moneda.CodMoneda;
                dmlTipoCambio = ObjECMP_OrdenServicio.TipoCambio;
                ObjECMP_Compra.Retencion = ObjECMP_OrdenServicio.Retencion;

                //factura
                if (ObjECMP_OrdenServicio.Exonerado == 12)
                {
                    chkIncluyeIGV.IsChecked = true;
                }
                else
                {
                    chkIncluyeIGV.IsChecked = false;
                }
                LoadOrdenServicioDetail(ObjECMP_OrdenServicio);
            }
        }

        /// <summary>
        /// Carga datos del orden de servicio
        /// </summary>
        private void LoadOrdenServicioDetail(ECMP_OrdenServicio ObjECMP_OrdenServicio)
        {
            var vrListECMP_OrdenServicioDetalle = new List<ECMP_OrdenServicioDetalle>();

            string strOutMessageError = string.Empty;
            CmpTask.Process(
            () =>
            {
                try
                {
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
                    CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, strOutMessageError, CmpButton.Aceptar);
                }
                else
                {
                    foreach (var item in vrListECMP_OrdenServicioDetalle)
                    {
                        if (ObjECMP_OrdenServicio.Exonerado == 12)
                        {
                            decimal dmlCalculoIGV = (item.PrecioUnitario * dmlIGV);
                            item.PrecioUnitario = decimal.Round(dmlCalculoIGV + item.PrecioUnitario, 10);
                        }
                        var vrObjEALM_IngresoSalidaDetalle = new ECMP_CompraDetalle()
                        {
                            Item = item.Item,
                            IdReferencia = ObjECMP_OrdenServicio.IdOrdenServicio,
                            Cantidad = item.Cantidad,
                            PrecioUnitario = item.PrecioUnitario,
							PrecioUnitarioTemp = item.PrecioUnitarioTemp,
                            MaxCantidad = item.Cantidad,
                            IsEnabledColumnPrecioUnitario = false,
                            IsEnabledColumnAlmcen = false,
                            SerieNumero = ObjECMP_OrdenServicio.Serie + " - " + ObjECMP_OrdenServicio.Numero,
                            ListEALM_Almacen = ListEALM_Almacen
                        };
                        AddItemsServicio(item.ObjEMNF_Servicio, vrObjEALM_IngresoSalidaDetalle,false , false,false );
                        intExoneradoIGV = ObjECMP_OrdenServicio.Exonerado;
                        boolRetencion = ObjECMP_OrdenServicio.Retencion;
                        chkRetencion.IsChecked = boolRetencion;
                        CalcularItemsServicio(vrObjEALM_IngresoSalidaDetalle);
                    }
                    LoadTipoDestino();
                    btnAgregarArtServ.IsEnabled = false;
                    btnQuitarArtServ.IsEnabled = false;
                }
            });
        }

        /// <summary>
        /// Instancia y pinta valor del Orden Compra seleccionado de la busqueda
        /// </summary>
        /// <param name="ObjECMP_OrdenCompra">Objeto de la clase Orden Compra</param>
        public void AddValueOrdenCompra(ECMP_OrdenCompra ObjECMP_OrdenCompra)
        {
            if (ObjECMP_OrdenCompra != null)
            {
                var vrListEMNF_DocumentoReferencia = (dgReferencias.Items.OfType<EMNF_DocumentoReferencia>()).ToList();
                bool existeArticulo = vrListEMNF_DocumentoReferencia.Exists(x => x.IdReferencia == ObjECMP_OrdenCompra.IdOrdenCompra);
                if (existeArticulo)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "El item seleccionado ya existe", CmpButton.Aceptar);
                    return;
                }

                dgReferencias.Items.Add(new EMNF_DocumentoReferencia()
                {
                    IdReferencia = ObjECMP_OrdenCompra.IdOrdenCompra,
                    SerieNumero = ObjECMP_OrdenCompra.Serie + " - " + ObjECMP_OrdenCompra.Numero
                });
                cbxMoneda.SelectedValue = ObjECMP_OrdenCompra.ObjESGC_Moneda.CodMoneda;
                dmlTipoCambio = ObjECMP_OrdenCompra.TipoCambio;
                LoadOrdenCompraDetail(ObjECMP_OrdenCompra);
            }
        }

        /// <summary>
        /// Carga datos del orden de compra
        /// </summary>
        private void LoadOrdenCompraDetail(ECMP_OrdenCompra ObjECMP_OrdenCompra)
        {
            var vrListECompraDetalle = new List<ECMP_OrdenCompraDetalle>();

            string strOutMessageError = string.Empty;
            CmpTask.Process(
            () =>
            {
                try
                {
                    vrListECompraDetalle = new BCMP_OrdenCompraDetalle().ListAdministrarOrdenCompraDetalle(ObjECMP_OrdenCompra).Where(x => x.ObjESGC_Estado.CodEstado != "APDOC" && x.Provisionado == 0 && x.ObjECMP_OrdenCompra.Provisionado == 0).ToList();
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
                    CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, strOutMessageError, CmpButton.Aceptar);
                }
                else
                {
                    if (vrListECompraDetalle.Count > 0)
                    {
                        foreach (var item in vrListECompraDetalle)
                        {
                            if (item.ObjEMNF_Articulo.ObjEMNF_OperacionIGV.CodOperacionIGV == "GB")
                            {
                                if (chkIncluyeIGV.IsChecked.Value || ObjECMP_OrdenCompra.IncluyeIGV)
                                {
                                    //PRECIO INCLUIDO IGV
                                    chkIncluyeIGV.IsChecked = (ObjECMP_OrdenCompra.IncluyeIGV == true);
                                    decimal dmlCalculoIGV = decimal.Round((item.PrecioUnitario * dmlIGV), 10);
                                    item.PrecioUnitario = dmlCalculoIGV + item.PrecioUnitario;
                                }
                            }

                            var vrObjECMP_CompraDetalle = new ECMP_CompraDetalle()
                            {
                                ObjECMP_Compra = ObjECMP_Compra,
                                ObjEALM_Almacen = ObjECMP_OrdenCompra.ObjEALM_Almacen,
                                IdReferencia = ObjECMP_OrdenCompra.IdOrdenCompra,
                                Item = item.Item,
                                Cantidad = item.Cantidad,
                                PrecioUnitario = decimal.Round(item.PrecioUnitario, 8),
                                PrecioUnitarioTemp = decimal.Round(item.PrecioUnitarioTemp, 8),
                                MaxCantidad = item.Cantidad,
                                IsEnabledColumnPrecioUnitario = false,
                                IsEnabledColumnAlmcen = false,
                                SerieNumero = ObjECMP_OrdenCompra.Serie + " - " + ObjECMP_OrdenCompra.Numero,
                                ListEALM_Almacen = ListEALM_Almacen
                            };
                            AddItemsArticulos(item.ObjEMNF_Articulo, vrObjECMP_CompraDetalle);
                            CalcularItemsArticulo(vrObjECMP_CompraDetalle);
                        }
                    }
                    else
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "Las lineas de detalle del documento ya han sido provisionadas y/o recepcionadas parcialmente", CmpButton.Aceptar);
                    }
                    LoadTipoDestino();
                    btnAgregarArtServ.IsEnabled = false;
                    btnQuitarArtServ.IsEnabled = true;
                }
            });
        }

        /// <summary>
        /// Instancia y pinta valor del Proveedor seleccionado de la busqueda
        /// </summary>
        /// <param name="ObjECMP_Compra.ObjEMNF_ClienteProveedor">Objeto de la clase Proveedor</param>
        public void AddValueProveedor(EMNF_ClienteProveedor ObjEMNF_ClienteProveedor)
        {
            if (ObjEMNF_ClienteProveedor != null)
            {
                this.ObjECMP_Compra.ObjEMNF_ClienteProveedor = ObjEMNF_ClienteProveedor;
                txtProveedorRazonSocial.Text = ObjEMNF_ClienteProveedor.RazonSocial;
                cbxFormaPago.SelectedValue = ObjECMP_Compra.ObjEMNF_ClienteProveedor.ObjEMNF_FormaPago.IdFormaPago;
            }
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

            dgCompraDetalle.Items.Clear();
            dgReferencias.Items.Clear();
            intExoneradoIGV = 0;

            cbxEstado.IsEnabled = false;

            cbxEstado.ItemsSource = null;
            cbxMotivoMovimiento.ItemsSource = null;
            cbxOperacionMovimiento.IsEnabled = true;
            txtGuiaRemision.IsEnabled = true;
            txtCorrelativo.IsEnabled = true;
            cbxMotivoMovimiento.IsEnabled = false;

            dgColumnSerieNum.Visibility = System.Windows.Visibility.Visible;
            dtpFechaOrden.IsEnabled = true;

            cbxMoneda.IsEnabled = true;
            txtSelRateTipoCambio.IsReadOnly = false;

            chkAfectoDetraccion.IsChecked = false;
            //chkIncluyeIGV.IsChecked = false;

            txtDocumentoRef.Text = string.Empty;
            txtDocumentoRef.IsEnabled = false;

            cbxDocReferencia.IsEnabled = false;

            btnAgregarArtServ.ContentTitle = "Agregar Artículo";
            btnQuitarArtServ.ContentTitle = "Quitar Artículo";
            lblTitleCompra04.Text = "IGV " + decimal.Round(dmlIGV * 100, 2) + "%";

            txtLineasArtServ.Text = ("0");
            txtGravadaArtServ.Text = ("0.00");
            txtExoneradoArtServ.Text = ("0.00");
            txtTotalIgvArtServ.Text = ("0.00");
            txtTotalNetoArtServ.Text = ("0.00");

            txtProveedorRazonSocial.Focus();
        }

        /// <summary>
        /// Utilizado para el enter y cuando pierde el Focus
        /// </summary>
        private void txtSerieValue()
        {
            if (cbxDocumento.SelectedValue == null) { return; }

            string strSerie = txtSerie.Text.Trim();
            txtCorrelativo.IsEnabled = true;
            var vrCodDocumento = cbxDocumento.SelectedValue.ToString();

            int isNumeric;
            if (int.TryParse(txtSerie.Text, out isNumeric))
            {
                string strCeros = string.Empty;
                for (int i = 0; i < (4 - (txtSerie.Text.Length)); i++)
                {
                    strCeros += "0";
                }
                txtSerie.Text = strCeros + txtSerie.Text;
            }

            txtCorrelativo.Focus();
            txtCorrelativo.SelectAll();

            //Presenta número de correlativo del documento RIG
            if (vrCodDocumento == "RIG")
            {
                var vrObjESGC_Documento = new ESGC_Documento();
                var varObjECMP_Compra = (ECMP_Compra)MyHeader.DataContext;

                if (strSerie == string.Empty)
                    return;

                string strOutMessageError = string.Empty;
                CmpTask.Process(
                () =>
                {
                    try
                    {
                        if (varObjECMP_Compra.Opcion == "I")
                        {
                            vrObjESGC_Documento = new BSGC_Documento().GetNroDocumento(vrCodDocumento, Convert.ToInt32(strSerie));
                            ObjESGC_UsuarioEmpresaSucursal.ObjESGC_EmpresaSucursal = vrObjESGC_Documento.ObjESGC_EmpresaSucursal;
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
                        CmpMessageBox.Show(CMPMensajes.TitleAdminProviDocumento, strOutMessageError, CmpButton.Aceptar, () =>
                        {
                            txtCorrelativo.Text = string.Empty;
                            txtSerie.Text = string.Empty;
                        });
                    }
                    else
                    {
                        if (varObjECMP_Compra.Opcion == "I")
                        {
                            LoadTipoDestino();
                            if (vrObjESGC_Documento.Serie != null)
                            {
                                txtCorrelativo.Text = vrObjESGC_Documento.Correlativo;
                                txtSerie.Text = vrObjESGC_Documento.Serie;
                                txtCorrelativo.IsEnabled = false;
                            }
                            else
                            {
                                CmpMessageBox.Show(CMPMensajes.TitleAdminProviDocumento, "La Serie ingresada no ha sido creada", CmpButton.Aceptar, () =>
                                {
                                    txtCorrelativo.Text = string.Empty;
                                    txtSerie.Text = string.Empty;
                                });
                            }
                        }
                    }
                });
            }
        }

        /// <summary>
        /// Utilizado para el enter y cuando pierde el Focus
        /// </summary>
        private void txtNumeroValue()
        {
            string strCeros = string.Empty;
            for (int i = 0; i < (10 - txtCorrelativo.Text.Length); i++)
            {
                strCeros += "0";
            }

            txtCorrelativo.Text = strCeros + txtCorrelativo.Text;
        }
        
        /// <summary>
        /// Carga datos para la administración de orden de compra
        /// </summary>
        private void LoadHeader(Action MyAction)
        {
            var vrListEMNF_OperacionMovimiento = new List<EMNF_OperacionMovimiento>();
            var vrListESGC_FormaPago = new List<ESGC_FormaPago>();
            var vrListESGC_Estado = new List<ESGC_Estado>();
            var vrListESGC_Moneda = new List<ESGC_Moneda>();
            var vrListEMNF_SubDiario = new List<EMNF_SubDiario>();
            var vrListESGC_FormularioSetting = new List<ESGC_FormularioSetting>();
            var vrListEMNF_Periodo = new List<EMNF_Periodo>();
            var vrListTipoDestino = new CmpObservableCollection<EMNF_TipoDestino>();

            string strPeriodoActual = new BMNF_Periodo().GetPeriodoActual();

            string strOutMessageError = string.Empty;
            CmpTask.Process(
            () =>
            {
                try
                {
                    vrListEMNF_OperacionMovimiento = new BMNF_OperacionMovimiento().ListGetOperacionMovimiento(this.GetType().Name.Substring(1, 3));
                    vrListESGC_FormaPago = new BSGC_FormaPago().ListGetFormaPago();
                    vrListESGC_Estado = new BSGC_Estado().ListFiltrarEstado(SGCMethod.GetNameNameTableInXaml(this));
                    vrListESGC_Moneda = new BSGC_Moneda().ListGetMoneda();
                    vrListEMNF_SubDiario = new BMNF_SubDiario().ListGetSubDiario();
                    vrListEMNF_Periodo = new BMNF_Periodo().ListPeriodo();
                    vrListTipoDestino = new BMNF_TipoDestino().CollectionTipoDestino();

                    if (ObjECMP_Compra.Opcion == "I")
                        vrListESGC_FormularioSetting = new BSGC_FormularioSetting().ListGetFormularioSetting(this.GetType().Name);
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
                    CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, strOutMessageError, CmpButton.Aceptar);
                }
                else
                {
                    cbxOperacionMovimiento.ItemsSource = vrListEMNF_OperacionMovimiento.Where(x => x.CodOpeMovimiento == "PPFC" || x.CodOpeMovimiento == "PPHN" || x.CodOpeMovimiento == "PGID").ToList();
                    cbxFormaPago.ItemsSource = vrListESGC_FormaPago;
                    cbxMoneda.ItemsSource = vrListESGC_Moneda;
                    cbxSubDiario.ItemsSource = vrListEMNF_SubDiario;
                    cbxPeriodo.ItemsSource = (ObjECMP_Compra.Opcion == "I") ? vrListEMNF_Periodo.Where(x => x.Estado == "A").ToList() : vrListEMNF_Periodo;
                    cbxEstado.ItemsSource = vrListESGC_Estado.Where(x => x.Campo == "CodEstado");                    
                    cbxTipoDestino.ItemsSource = vrListTipoDestino;                        

                    //Si es insert agregamos valores predefinidos de la base de datos
                    if (ObjECMP_Compra.Opcion == "I")
                    {
                        dmlIGV = SGCVariables.ObjESGC_Retencion.IGV / 100;
                        dmlPercepcion = SGCVariables.ObjESGC_Retencion.Percepcion / 100;
                        dmlRentaSegunda = SGCVariables.ObjESGC_Retencion.PjeRentaSegunda / 100;
                        AddValueDefault(vrListESGC_FormularioSetting);
                        cbxMoneda.SelectedValue = ObjECMP_Compra.ObjESGC_Moneda.CodMoneda;
                        ObjECMP_Compra.Periodo = strPeriodoActual;
                        cbxTipoDestino.SelectedValue = vrListTipoDestino.FirstOrDefault(x => x.CodTipoDestino == "CTDIS");

                        MDatePicker.DateStartToDateEnd(dtpFechaOrden, strPeriodoActual);
                        MDatePicker.DateStartToDateEnd(dtpFechaContable, strPeriodoActual);
                        ObjECMP_Compra.Fecha = dtpFechaOrden.SelectedDate.Value;
                        ObjECMP_Compra.FechaContable = dtpFechaContable.SelectedDate.Value;
                    }
                    else
                    {
                        chkPlanilla.IsChecked = ObjECMP_Compra.Planilla;
                        cbxDocReferencia.SelectedValue = ObjECMP_Compra.CodDocumentoRef;
                        dmlIGV = ObjECMP_Compra.IGV;
                        dmlPercepcion = (ObjECMP_Compra.AfectoPercepcion) ? ObjECMP_Compra.Percepcion : SGCVariables.ObjESGC_Retencion.Percepcion / 100;
                        dmlRentaSegunda = (ObjECMP_Compra.Retencion) ? ObjECMP_Compra.IGV : SGCVariables.ObjESGC_Retencion.PjeRentaSegunda / 100;
                        dmlTipoCambio = ObjECMP_Compra.TipoCambio;
                        chkAfectoPercepcion.IsChecked = ObjECMP_Compra.AfectoPercepcion;
						chkAfectoPercepcion.IsEnabled = false;
                        chkIncluyeIGV.IsChecked = ObjECMP_Compra.IncluyeIGV;
                        cbxMoneda.IsEnabled = (ObjECMP_Compra.ObjESGC_Estado.CodEstado == "PECMP");
                        cbxMoneda.SelectedValue = ObjECMP_Compra.ObjESGC_Moneda.CodMoneda;
                        if (ObjECMP_Compra.Opcion == "U")
                        {
                            dtpFechaOrden.IsEnabled = ((ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "ICD" && chkAfectoAlmacen.IsChecked.Value) ? false : true);
                            cbxMoneda.IsEnabled = ((ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "ICD" && chkAfectoAlmacen.IsChecked.Value) ? false : true);
                            txtSelRateTipoCambio.IsEnabled = ((ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "ICD" && chkAfectoAlmacen.IsChecked.Value) ? false : true);
                            dtpFechaContable.IsEnabled = ((ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "ICD" && chkAfectoAlmacen.IsChecked.Value) ? false : true);
                        }
                        if (ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "GIS")
                            ObjESGC_UsuarioEmpresaSucursal.ObjESGC_EmpresaSucursal =  new BSGC_Documento().GetNroDocumento(ObjECMP_Compra.CodDocumento, Convert.ToInt32(ObjECMP_Compra.Serie.Substring(1, 3))).ObjESGC_EmpresaSucursal;
                        chkRetencion.IsChecked = ObjECMP_Compra.Retencion;
                        dgReferencias.IsEnabled = false;
                        cbxPeriodo.IsEnabled = false;
                        txtGuiaRemision.IsEnabled = false;
                        txtDocumentoRef.IsEnabled = false;
                        btnImprimirArtServ.IsEnabled = true;
                        chkCajaBanco.IsChecked = ObjECMP_Compra.CajaBanco;

                        chkAnticipo.IsChecked = (ObjECMP_Compra.Anticipo);

                        chkAfectoAlmacen.IsChecked = ObjECMP_Compra.AfectaAlmacen;
                        chkDistribucion.IsChecked = ObjECMP_Compra.Distribucion;
                        if (ObjECMP_Compra.Distribucion)
                        {
                            lblTipoDestino.Visibility = System.Windows.Visibility.Visible;
                            cbxTipoDestino.Visibility = System.Windows.Visibility.Visible;
                        }
                       
                        cbxMotivoMovimiento.IsEnabled = false;
                        txtProveedorRazonSocial.IsReadOnly = true;
                        chkAfectoAlmacen.IsEnabled = false;
                        chkDistribucion.IsEnabled = (!chkAfectoAlmacen.IsChecked.Value);// && !chkDistribucion.IsChecked.Value);
                        dtgColumnaTipoDestino.IsReadOnly = true;
                    }
                    lblTitleCompra04.Text = "IGV " + decimal.Round(dmlIGV * 100, 2) + "%";

                    MyHeader.DataContext = ObjECMP_Compra;

                    if (MyAction != null) { MyAction.Invoke(); }
                }
            });
        }

        /// <summary>
        /// Carga datos del orden de compra
        /// </summary>
        private void LoadDocumentoRefAndDetail()
        {
            dgCompraDetalle.Items.Clear();
            dgReferencias.Items.Clear();

            var vrListECompraDetalle = new List<ECMP_CompraDetalle>();
            var vrListEMNF_DocumentoReferencia = new List<EMNF_DocumentoReferencia>();

            string strOutMessageError = string.Empty;
            CmpTask.Process(
            () =>
            {
                try
                {
                    ListEALM_Almacen = new BALM_Almacen().ListGetAlmacen();
                    if (ObjECMP_Compra.Opcion == "U")
                    {
                        vrListECompraDetalle = new BCMP_CompraDetalle().ListAdministrarCompraDetalle(ObjECMP_Compra);
                        vrListEMNF_DocumentoReferencia = new BMNF_DocumentoReferencia().ListGetDocumentoReferencia(ObjECMP_Compra.IdCompra, "CMP");                        
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
                    CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, strOutMessageError, CmpButton.Aceptar);
                }
                else
                {
                    //Carga Documentos de Referencia
                    foreach (var item in vrListEMNF_DocumentoReferencia)
                    {
                        dgReferencias.Items.Add(item);
                    }

                    if (ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "IDS" || ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "IDH" || ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "IDP" || ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "IAF" || ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "ICD")
                    {
                        if (ObjECMP_Compra.Opcion == "U")
                        {
                            if (ObjECMP_Compra.ObjEMNF_TipoDestino.CodTipoDestino == "CTSUC" && ObjECMP_Compra.Distribucion)
                                ObjESGC_UsuarioEmpresaSucursal.ObjESGC_EmpresaSucursal = new ESGC_EmpresaSucursal() { IdEmpSucursal = vrListECompraDetalle.FirstOrDefault().IdDestino };
                            else
                                ObjESGC_UsuarioEmpresaSucursal.ObjESGC_EmpresaSucursal = new ESGC_EmpresaSucursal() { IdEmpSucursal = vrListECompraDetalle.FirstOrDefault().IdEmpSucursal };
                            
                            cbxEmpresaSucursal.SelectedValue = ObjESGC_UsuarioEmpresaSucursal.ObjESGC_EmpresaSucursal.IdEmpSucursal;
                        }
                        else
                            cbxEmpresaSucursal.SelectedIndex = 0;
                    }

                    vrListECompraDetalle.ForEach(x => x.ListEALM_Almacen = ListEALM_Almacen);
                    foreach (var item in vrListECompraDetalle)
                    {
                        
                        //Si contiene un precio en negativo se considera Anticipo
                        if (item.PrecioUnitario < 0)
                            chkAnticipo.IsEnabled = false;

                        //Verificamos que el Artículo sea grabada
                        item.IsEnabledColumnPrecioUnitario = !(dgReferencias.Items.Count > 0);
                        item.IsEnabledColumnAlmcen = !(dgReferencias.Items.Count > 0);
                 
                        if (ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "ICD")
                        {
                            item.IsEnabledColumnAlmcen = false;
                            item.IsEnabledColumnPrecioUnitario = false;
                        }

                        dgCompraDetalle.Items.Add(item);
                        if (item.TipoDetalle == "MNF_Articulo")
                            CalcularItemsArticulo(item);
                        else
                            CalcularItemsServicio(item);
                    }

                    //Carga Tipo de Destino con la Sucursal del Detalle de Compra
                    LoadTipoDestino();
                  
                    txtExoneradoArtServ.Text = decimal.Round(ObjECMP_Compra.Exonerada, 2).ToString("###,###,##0.#0");
                    txtGravadaArtServ.Text = decimal.Round(ObjECMP_Compra.Gravada, 2).ToString("###,###,##0.#0");
                    txtTotalIgvArtServ.Text = decimal.Round(ObjECMP_Compra.ImporteIGV, 2).ToString("###,###,##0.#0");

                    decimal decTotalNeto = 0;
                    if (ObjECMP_Compra.ObjEMNF_OperacionMovimiento.CodOpeMovimiento == "PPHN")
                    {
                        //Operación de Ingreso por Provisión de Honorarios
                        decTotalNeto = (ObjECMP_Compra.Exonerada + ObjECMP_Compra.Gravada) - ObjECMP_Compra.ImporteIGV;
                    }
                    else if (ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "IDP" || ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "IPF")
                    {
                        //Factura de Interes de Prestamos
                        decTotalNeto = ObjECMP_Compra.Exonerada + ObjECMP_Compra.Gravada - ObjECMP_Compra.ImporteIGV;
                    }
                    else if (chkPlanilla.IsChecked.Value)
                    {
                        decTotalNeto = ObjECMP_Compra.Gravada - ObjECMP_Compra.ImporteIGV;
                    }
                    else
                    {
                        //Cualquier otra Operación de Compra
                        decTotalNeto = ObjECMP_Compra.Exonerada + ObjECMP_Compra.Gravada + ObjECMP_Compra.ImporteIGV;
                    }

                    if (chkPlanilla.IsChecked.Value)
                        txtTotalNetoArtServ.Text = decimal.Round(decTotalNeto , 2).ToString("###,###,##0.#0");
                    else
                        txtTotalNetoArtServ.Text = decimal.Round(decTotalNeto + ((ObjECMP_Compra.AfectoPercepcion) ? (ObjECMP_Compra.Percepcion * decTotalNeto) : 0), 2).ToString("###,###,##0.#0");

                    Load = false;
                }
            });
        }

        /// <summary>
        /// Carga datos de los sucursales dependiendo al Operación Movimiento
        /// </summary>
        /// <param name="ObjECMP_Compra.ObjEMNF_OperacionMovimiento">Objeto de la entidad EMNF_OperacionMovimiento</param>
        private void LoadDataInOperacionMoviento(EMNF_OperacionMovimiento ObjEMNF_OperacionMovimiento)
        {
            if (ObjEMNF_OperacionMovimiento == null) { return; }
            var vrListEMNF_MotivoMovimiento = new List<EMNF_MotivoMovimiento>();

            string strOutMessageError = string.Empty;
            CmpTask.Process(
            () =>
            {
                try
                {
                    vrListEMNF_MotivoMovimiento = new BMNF_MotivoMovimiento().ListGetMotivoMovimiento(ObjECMP_Compra.ObjEMNF_OperacionMovimiento);
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
                    CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, strOutMessageError, CmpButton.Aceptar);
                }
                else
                {
                    cbxMotivoMovimiento.ItemsSource = vrListEMNF_MotivoMovimiento;
                    if (ObjECMP_Compra.Opcion == "I") cbxMotivoMovimiento.SelectedItem = vrListEMNF_MotivoMovimiento.FirstOrDefault(x => x.CodMotMovimiento == "ICD" || x.CodMotMovimiento == "GIS");
                }
            });
        }

        /// <summary>
        /// Carga el tipo de cambio
        /// </summary>
        private void LoadSelRateTipoCambio()
        {
            ObjECMP_Compra.ObjESGC_Moneda = (ESGC_Moneda)cbxMoneda.SelectedItem;
            if (ObjECMP_Compra.ObjESGC_Moneda != null)
            {
                lblTitleCompra05.Text = "Total " + ObjECMP_Compra.ObjESGC_Moneda.Simbolo;
                if (ObjECMP_Compra.ObjESGC_Moneda.Defecto != true && ObjECMP_Compra.ObjESGC_Estado.CodEstado == "PECMP")
                {
                    if (dtpFechaOrden.SelectedDate == null || (Load && ObjECMP_Compra.TipoCambio > 0))
                    {
                        return;
                    }
                    var vrFecha = dtpFechaOrden.SelectedDate.Value;
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
                                ObjESGC_Moneda = ObjECMP_Compra.ObjESGC_Moneda
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
                            CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, strOutMessageError, CmpButton.Aceptar);
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
                                txtSelRateTipoCambio.Value = Convert.ToDouble(0);
                                txtSelRateTipoCambio.IsEnabled = true;
                                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "No se ha registrado el tipo de cambio de la moneda extranjera para el día " + vrFecha.ToShortDateString(), CmpButton.Aceptar);
                            }
                        }
                    });
                }
                else if (ObjECMP_Compra.ObjESGC_Moneda.Defecto)
                {
                    txtSelRateTipoCambio.Value = 1;
                }
                else if (ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento != "ICD")
                {
                    dgCompraDetalle.Items.Clear();
                    dgReferencias.Items.Clear();
                }
            }


        }

        /// <summary>
        /// Valida datos requeridos
        /// </summary>
        /// <returns></returns>
        private bool ValidaDatos()
        {
            bool blResult = false;
            var vrvrListECMP_CompraDetalle = new List<ECMP_CompraDetalle>();
            vrvrListECMP_CompraDetalle = (dgCompraDetalle.Items.OfType<ECMP_CompraDetalle>()).ToList();

            if (cbxOperacionMovimiento.SelectedValue == null)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "Seleccione la Operación de Movimiento", CmpButton.Aceptar);
                blResult = true;
            }
            else if (ObjECMP_Compra.ObjEMNF_ClienteProveedor == null)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "Seleccione un Proveedor", CmpButton.Aceptar);
                blResult = true;
            }
            else if (ObjECMP_Compra.ObjESGC_Moneda == null)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "Seleccione el Tipo de moneda", CmpButton.Aceptar);
                blResult = true;
            }
            else if (ObjECMP_Compra.ObjEMNF_MotivoMovimiento == null)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "Seleccione el Motivo Movimiento", CmpButton.Aceptar);
                blResult = true;
            }
            else if (ObjECMP_Compra.ObjESGC_Estado == null)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "Seleccione un Estado", CmpButton.Aceptar);
                blResult = true;
            }
            else if (ObjECMP_Compra.ObjESGC_FormaPago == null)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "Seleccione la Forma Pago", CmpButton.Aceptar);
                blResult = true;
            }
            else if (ObjECMP_Compra.ObjEMNF_SubDiario == null)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "Seleccione el Sub-Diario", CmpButton.Aceptar);
                blResult = true;
            }
            else if (ObjECMP_Compra.CodDocumento.Trim().Length == 0)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "Seleccione un Documento", CmpButton.Aceptar);
                blResult = true;
            }
            else if (cbxDocumento.SelectedValue == null)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "Ingrese un Documento", CmpButton.Aceptar);
                blResult = true;
            }
            else if (ObjECMP_Compra.Serie.Trim().Length == 0)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "Ingrese la Serie del documento", CmpButton.Aceptar);
                blResult = true;
            }
            else if (ObjECMP_Compra.Numero.Trim().Length == 0)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "Ingrese el Número de Documento", CmpButton.Aceptar);
                blResult = true;
            }
            else if (vrvrListECMP_CompraDetalle.Count == 0)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "Ingrese un [Artículo/Servicio] en el detalle", CmpButton.Aceptar);
                blResult = true;
            }
            else if (txtSelRateTipoCambio.Value.ToString().Trim().Length == 0)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "Ingrese el Tipo de Cambio", CmpButton.Aceptar);
                blResult = true;
            }
            else if (vrvrListECMP_CompraDetalle.ToList().Count > 0)
            {
                string strArtDescrip = string.Empty;
                foreach (var ff in vrvrListECMP_CompraDetalle)
                {
                    if (ff.Cantidad == 0)
                    {
                        strArtDescrip = "Por lo menos debe de ingrese una cantidad en el [Artículo/Servicio] " + ff.ArticuloServicio;
                        break;
                    }
                }

                if (strArtDescrip.Trim().Length == 0 && chkAfectoAlmacen.IsChecked.Value)
                    foreach (var ff in vrvrListECMP_CompraDetalle)
                    {
                        if (ff.IdArticuloServicio != 9991 && (ff.ObjEALM_Almacen == null || ff.ObjEALM_Almacen.IdAlmacen == -1) && dgColumnAlmacen.Visibility == System.Windows.Visibility.Visible)
                        {
                            strArtDescrip = "Seleccione un Almacén Destino en el Artículo " + ff.ArticuloServicio;
                            break;
                        }
                    }

                if (strArtDescrip.Trim().Length == 0 && chkDistribucion.IsChecked.Value)
                    foreach (var item in vrvrListECMP_CompraDetalle)
                    {

                        if (item.IdArticuloServicio != 9991 && (item.IdDestino == 0 && Convert.ToString(cbxTipoDestino.SelectedValue) != "CTDIS" && Convert.ToString(cbxTipoDestino.SelectedValue) != "CTSUC"))
                        {
                            strArtDescrip = "Seleccione " + dtgColumnaTipoDestino.Header + " para el Artículo " + item.ArticuloServicio;
                            break;
                        }

                        if (item.IdArticuloServicio != 9991 && (item.PeriodoCampania == null && chkDistribucion.IsChecked.Value))
                        {
                            strArtDescrip = "Seleccione Periodo Campaña para el Artículo " + item.ArticuloServicio;
                            break;
                        }
                    }

                if (strArtDescrip.Trim().Length > 0)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, strArtDescrip, CmpButton.Aceptar);
                    blResult = true;
                }
            }

            return blResult;
        }

        /// <summary>
        /// Asigna valor por defecto a la entidad actual
        /// </summary>
        /// <param name="vrListESGC_FormularioSetting"></param>
        private void AddValueDefault(List<ESGC_FormularioSetting> vrListESGC_FormularioSetting)
        {
            //ObjECMP_Compra.ObjESGC_Moneda
            var vrCodMoneda = vrListESGC_FormularioSetting.FirstOrDefault(x => x.Campo == "CodMoneda");
            if (vrCodMoneda != null)
            {
                #region ASIGNACIÓN

                try
                {
                    ObjECMP_Compra.ObjESGC_Moneda = new ESGC_Moneda() { CodMoneda = vrCodMoneda.Codigo };
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "Error al asignar datos por defecto a [CodMoneda] \n" + ex.Message, CmpButton.Aceptar);
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
                    ObjECMP_Compra.ObjESGC_Estado = new ESGC_Estado() { CodEstado = vrCodEstado.Codigo };
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "Error al asignar datos por defecto a [CodEstado] \n" + ex.Message, CmpButton.Aceptar);
                }

                #endregion
            }


            var vrIdSubDiario = vrListESGC_FormularioSetting.FirstOrDefault(x => x.Campo == "IdSubDiario");
            if (vrIdSubDiario != null)
            {
                #region ASIGNACIÓN

                try
                {
                    ObjECMP_Compra.ObjEMNF_SubDiario = new EMNF_SubDiario() { IdSubDiario = Convert.ToInt32(vrIdSubDiario.Codigo) };
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "Error al asignar datos por defecto a [IdSubDiario] \n" + ex.Message, CmpButton.Aceptar);
                }

                #endregion
            }

            //vrCodOpeMovimiento
            var vrCodOpeMovimiento = vrListESGC_FormularioSetting.FirstOrDefault(x => x.Campo == "CodOpeMovimiento");
            if (vrCodOpeMovimiento != null)
            {
                #region ASIGNACIÓN

                try
                {
                    ObjECMP_Compra.ObjEMNF_OperacionMovimiento = new EMNF_OperacionMovimiento() { CodOpeMovimiento = vrCodOpeMovimiento.Codigo };
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "Error al asignar datos por defecto a [CodOpeMovimiento] \n" + ex.Message, CmpButton.Aceptar);
                }

                #endregion
            }

            //vrCodMotMovimiento
            var vrCodMotMovimiento = vrListESGC_FormularioSetting.FirstOrDefault(x => x.Campo == "CodMotMovimiento");
            if (vrCodMotMovimiento != null)
            {
                #region ASIGNACIÓN

                try
                {
                    ObjECMP_Compra.ObjEMNF_MotivoMovimiento = new EMNF_MotivoMovimiento() { CodMotMovimiento = vrCodMotMovimiento.Codigo };
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "Error al asignar datos por defecto a [CodMotMovimiento] \n" + ex.Message, CmpButton.Aceptar);
                }

                #endregion
            }
        }

        /// <summary>
        /// Imprime un reporte de la venta
        /// </summary>
        private void ImprimirCompra()
        {
            try
            {
                var vrvrListECMP_CompraDetalle = new List<ECMP_CompraDetalle>();
                vrvrListECMP_CompraDetalle = (dgCompraDetalle.Items.OfType<ECMP_CompraDetalle>()).ToList();
                string[] parametro;

                if(chkAfectoAlmacen.Visibility == System.Windows.Visibility.Visible)
                    if (chkAfectoAlmacen.IsChecked == false)
                        vrvrListECMP_CompraDetalle.ForEach(x => x.ObjEALM_Almacen.Almacen = "");

                if (ObjECMP_Compra.ObjEMNF_OperacionMovimiento.CodOpeMovimiento == "PPHN")

                {
                    parametro = new string[]
                    {
                        "prmRazonSocialEmpresa|"    + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.RazonSocial,
                        "prmRucEmpresa|"            + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.Ruc,
                        "prmDireccionEmpresa|"      + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.DireccionFiscal,
                        "prmTelefonoEmpresa|"       + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.Telefono,
                        "prmFechaLetra|"            + DateTime.Now.ToLongDateString().Split(',').ElementAt(1),
                        "prmNumeroCompra|"          + ObjECMP_Compra.Serie + " - " + ObjECMP_Compra.Numero,
                        "prmProveedor|"             + ObjECMP_Compra.ObjEMNF_ClienteProveedor.RazonSocial,
                        "prmPeriodo|"               + ObjECMP_Compra.Periodo,
                        "prmTitulo|"                + "Reporte de Servicio por Honorario",
                        "prmTittleAfecta|"          + "Retención " + decimal.Round(dmlRentaSegunda * 100, 2) + "%",
                        "prmlblAfecta|"             + ObjECMP_Compra.ObjESGC_Moneda.Simbolo + " " + txtTotalIgvArtServ.Text,
                        "prmTittleTotalHonorario|"  + "Total Honorario",
                        "prmTotalHonorario|"        + ObjECMP_Compra.ObjESGC_Moneda.Simbolo + " " + txtGravadaArtServ.Text,
                        "prmPuntos|"                + ":",
                        "prmOpMovimiento|"          + ObjECMP_Compra.ObjEMNF_OperacionMovimiento.OpMovimiento,
                        "prmMotMovimiento|"         + ObjECMP_Compra.ObjEMNF_MotivoMovimiento.MotMovimiento,
                        "prmTotal|"                 + ObjECMP_Compra.ObjESGC_Moneda.Simbolo + " " + txtTotalNetoArtServ.Text,
                        "prmRetencionIGV|"          + "RETENCIÓN",
                        "prmGlosa|"                 + ObjECMP_Compra.Glosa
                    };

                    MainRerport ObjMainRerport = new MainRerport();
                    ObjMainRerport.InitializeMainRerport("CMP.Reports.Files.RptCompraHonorario.rdlc", "DtsCompraHonorario", vrvrListECMP_CompraDetalle, parametro);
                    ObjMainRerport.ShowDialog();
                }
                if (ObjECMP_Compra.ObjEMNF_OperacionMovimiento.CodOpeMovimiento == "PGID")
                {
                    decimal dmlSubTotal = Convert.ToDecimal(txtExoneradoArtServ.Text) + Convert.ToDecimal(txtGravadaArtServ.Text);
                    parametro = new string[]
                    {

                        "prmRazonSocialEmpresa|"    + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.RazonSocial,
                        "prmRucEmpresa|"            + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.Ruc,
                        "prmDireccionEmpresa|"      + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.DireccionFiscal,
                        "prmTelefonoEmpresa|"       + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.Telefono,
                        "prmFechaLetra|"            + DateTime.Now.ToLongDateString().Split(',').ElementAt(1),
                        "prmNumeroCompra|"          + ObjECMP_Compra.Serie + " - " + ObjECMP_Compra.Numero,
                        "prmProveedor|"             + ObjECMP_Compra.ObjEMNF_ClienteProveedor.RazonSocial,
                        "prmPeriodo|"               + ObjECMP_Compra.Periodo,
						"prmTitulo|"                + ObjECMP_Compra.ObjEMNF_MotivoMovimiento.MotMovimiento,
                        "prmTittleAfecta|"          + "IGV " + decimal.Round(dmlIGV * 100, 2) + "%",
                        "prmTittleTotalHonorario|"   + "Sub Total",
                        "prmPuntos|"                + ":",
                        "prmTotalHonorario|"        + ObjECMP_Compra.ObjESGC_Moneda.Simbolo + " " + dmlSubTotal.ToString("#,###,###,##0.#0"),
                        "prmlblAfecta|"             + ObjECMP_Compra.ObjESGC_Moneda.Simbolo + " " + txtTotalIgvArtServ.Text,
                        "prmOpMovimiento|"          + ObjECMP_Compra.ObjEMNF_OperacionMovimiento.OpMovimiento,
                        "prmMotMovimiento|"         + ObjECMP_Compra.ObjEMNF_MotivoMovimiento.MotMovimiento,
                        "prmTotal|"                 + ObjECMP_Compra.ObjESGC_Moneda.Simbolo + " " + txtTotalNetoArtServ.Text,
                        "prmRetencionIGV|"          + "IMPORTE IGV",
                        "prmGlosa|"                 + ObjECMP_Compra.Glosa
                    };


                    MainRerport ObjMainRerport = new MainRerport();
                    ObjMainRerport.InitializeMainRerport("CMP.Reports.Files.RptCompraHonorario.rdlc", "DtsCompraHonorario", vrvrListECMP_CompraDetalle, parametro);
                    ObjMainRerport.ShowDialog();
                }

                if (ObjECMP_Compra.ObjEMNF_OperacionMovimiento.CodOpeMovimiento == "PPFC")
                {
                    string CodMotMovimiento = ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento;
                    string titulo = string.Empty;
                    switch (CodMotMovimiento)
                    {
                        case "ICD":
                            titulo = "INGRESO POR COMPRA DIRECTA DE INSUMOS";
                            break;
                        case "ICP":
                            titulo = "INGRESO POR PROVISIÓN DE INSUMOS";
                            break;
                        case "IDP":
                            titulo = "INGRESO POR PROVISION DIRECTA DE PRESTAMO";
                            break;
                        case "IPF":
                            titulo = "INGRESO POR PROVISION DE PRESTAMO";
                            break;
                        default:
                            titulo = ObjECMP_Compra.ObjEMNF_MotivoMovimiento.MotMovimiento;
                            break;
                    }
                    parametro = new string[]
                    {
                        "prmRazonSocialEmpresa|"    + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.RazonSocial,
                        "prmRucEmpresa|"            + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.Ruc,
                        "prmDireccionEmpresa|"      + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.DireccionFiscal,
                        "prmTelefonoEmpresa|"       + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.Telefono,
                        "prmFechaLetra|"            + DateTime.Now.ToLongDateString().Split(',').ElementAt(1),
                        "prmNumeroCompra|"          + ObjECMP_Compra.Serie + " - " + ObjECMP_Compra.Numero,
                        "prmProveedor|"             + ObjECMP_Compra.ObjEMNF_ClienteProveedor.RazonSocial,
                        "prmReporte|"               + titulo,
                        "prmOpMovimiento|"          + ObjECMP_Compra.ObjEMNF_OperacionMovimiento.OpMovimiento,
                        "prmMotMovimiento|"         + ObjECMP_Compra.ObjEMNF_MotivoMovimiento.MotMovimiento,
                        "prmIgvTexto|"              + ((CodMotMovimiento == "IDP" || CodMotMovimiento == "IPF") ? "Retención " + decimal.Round(dmlRentaSegunda * 100,2) + "%" :"IGV " + decimal.Round(dmlIGV * 100, 2) + "%"),
                        "prmTitleIGVRetencion|"     + ((CodMotMovimiento == "IDP" || CodMotMovimiento == "IPF") ? "Retención" :" Importe IGV "),
                        "prmExonerado|"             + ((CodMotMovimiento == "IDP" || CodMotMovimiento == "IPF") ? "" : ObjECMP_Compra.ObjESGC_Moneda.Simbolo + " " + txtExoneradoArtServ.Text),
                        "prmTitleExonerada|"        + ((CodMotMovimiento == "IDP" || CodMotMovimiento == "IPF") ? "" :"Exonerada"),
                        "prmDosPuntos|"             + ((CodMotMovimiento == "IDP" || CodMotMovimiento == "IPF") ? "" :":"),
                        "prmTitleGravada|"          + lblTitleCompra03.Text,
                        "prmTitleTotal|"            + lblTitleCompra05.Text,
                        "prmGravada|"               + ObjECMP_Compra.ObjESGC_Moneda.Simbolo + " " + txtGravadaArtServ.Text,
                        "prmImporteIgv|"            + ObjECMP_Compra.ObjESGC_Moneda.Simbolo + " " + txtTotalIgvArtServ.Text,
                        "prmTotal|"                 + ObjECMP_Compra.ObjESGC_Moneda.Simbolo + " " + txtTotalNetoArtServ.Text,
						"prmPeriodo|"               + ObjECMP_Compra.Periodo,
                        "prmGlosa|"                 + ObjECMP_Compra.Glosa
                    };

                    MainRerport ObjMainRerport = new MainRerport();
                    ObjMainRerport.InitializeMainRerport("CMP.Reports.Files.RptCompraProvision.rdlc", "DtsCompraInsumo", vrvrListECMP_CompraDetalle, parametro);
                    ObjMainRerport.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, ex.Message, CmpButton.Aceptar);
            }
        }

        /// <summary>
        /// Crea XML del detalle
        /// </summary>
        /// <returns></returns>
        private string XmlDetalleCompra()
        {
            string strCadXml = "";
            strCadXml = "<ROOT>";
            
            if (DetailArticulo.Visibility == System.Windows.Visibility.Visible)
            {
                var vrListECMP_CompraDetalle = (dgCompraDetalle.Items.OfType<ECMP_CompraDetalle>()).ToList();

                vrListECMP_CompraDetalle.ForEach((f) =>
                {
                    if (f.IdArticuloServicio != 0)
                    {
                        decimal dmlPrecioUnitario = 0;

                        if (!(f.CodOperacionIGV == "GB"))
                            dmlPrecioUnitario = f.PrecioUnitario;
                        else
                        {
                            if (ObjECMP_Compra.IncluyeIGV)
                                dmlPrecioUnitario = (f.PrecioUnitarioTemp) / ((decimal.Round(dmlIGV * 100, 2) + 100) / 100);
                            else
                                dmlPrecioUnitario = f.PrecioUnitarioTemp;
                        }

                        strCadXml += "<Listar xIdArticuloServicio = \'" + f.IdArticuloServicio;
                        strCadXml += "\' xCodUndMedida = \'" + f.CodUndMedida;
                        strCadXml += "\' xIdReferencia = \'" + f.IdReferencia;
                        if (chkAfectoAlmacen.IsChecked.Value|| (ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "ICP" || ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "ISF"))
                            strCadXml += ((ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "IDS") ? "" : "\' xIdAlmacen = \'" + f.ObjEALM_Almacen.IdAlmacen);
                        strCadXml += "\' xCantidad = \'" + f.Cantidad;
                        strCadXml += "\' xPrecioUnitario = \'" + decimal.Round(dmlPrecioUnitario, 8);
                        strCadXml += "\' xImporteIGV = \'" + f.ImporteIGV;
                        if (chkDistribucion.IsChecked.Value || ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "IDH" || ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "IDP")
                            strCadXml += "\' xIdDestino = \'" + ((ObjEMNF_TipoDestino.CodTipoDestino != "CTSUC") ? f.IdDestino : ObjESGC_UsuarioEmpresaSucursal.ObjESGC_EmpresaSucursal.IdEmpSucursal);
                        strCadXml += "\' xPeriodoCampania = \'" + ((chkDistribucion.IsChecked.Value) ? f.PeriodoCampania : "");
                        strCadXml += "\' ></Listar>";
                    }
                });
            }

            strCadXml += "</ROOT>";

            return strCadXml;
        }

        /// <summary>
        /// Crea XML del Documento Referencias
        /// </summary>
        /// <returns></returns>
        private string XmlDocumentoRef()
        {
            string strCadXml = "";
            strCadXml = "<ROOT>";

            if (DetailArticulo.Visibility == System.Windows.Visibility.Visible)
            {
                var vrListEMNF_DocumentoReferencia = (dgReferencias.Items.OfType<EMNF_DocumentoReferencia>()).ToList();

                vrListEMNF_DocumentoReferencia.ForEach((f) =>
                {
                    strCadXml += "<Listar xIdReferencia = \'" + f.IdReferencia +
                                    "\' ></Listar>";
                });
            }

            strCadXml += "</ROOT>";

            return strCadXml;
        }

        /// <summary>
        /// Crea XML del detalle
        /// </summary>
        /// <returns></returns>
        /// @CompraAnticipoXML
        private string XmlCompraAnticipo()
        {
            string strCadXml = "";
            strCadXml = "<ROOT>";
            
            if(lstIdCompra.Count > 0)
            {
                foreach(var item in lstIdCompra)
                {
                    strCadXml += "<Listar xIdCompraAnticipo = \'" + item;
                    strCadXml += "\' ></Listar>";
                }
            }
            
            strCadXml += "</ROOT>";

            return strCadXml;
        }

        private bool ValidaGIS()
        {
            bool valida = false;
            if (cbxMotivoMovimiento.SelectedValue != null && ObjECMP_Compra.Distribucion)
            {
                if (cbxMotivoMovimiento.SelectedValue.ToString() == "GIS")
                {
                    if (cbxTipoDestino.SelectedValue == null)
                    {
                        valida = true;
                        Message = "Ingrese un Tipo Destino para continuar.";
                    }
                    else
                    {
                        var vrObjListECMP_CompraDetalle = (dgCompraDetalle.Items.OfType<ECMP_CompraDetalle>()).ToList();
                        if (vrObjListECMP_CompraDetalle.Count > 0)
                        {
                            foreach (var item in vrObjListECMP_CompraDetalle)
                            {
                                if (item.PeriodoCampania == null)
                                {
                                    valida = true;
                                    Message = "Ingrese un Periodo Campaña para el [Artículo/Servicio] - " + item.ArticuloServicio;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            valida = true;
                            Message = "No hay datos para Guardar.";
                        }
                    }
                }
                else
                {
                    valida = false;
                }
            }
            return valida;
        }

        private void ChkAlmacenDistribucion()
        {
            if (ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == null) return;

            string CodMotivoMovimiento = ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento;

            if(CodMotivoMovimiento == "IDS")
            {
                chkDistribucion.IsChecked = (ObjECMP_Compra.Opcion == "I") ? true : ObjECMP_Compra.Distribucion;
                cbxTipoDestino.Visibility = (ObjECMP_Compra.Opcion == "I" || ObjECMP_Compra.Distribucion) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                lblTipoDestino.Visibility = cbxTipoDestino.Visibility;
                dtgColumnaPeriodoCompania.Visibility = (ObjECMP_Compra.Opcion == "I" || ObjECMP_Compra.Distribucion) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                chkDistribucion.Visibility = System.Windows.Visibility.Visible;
            }
            else if (CodMotivoMovimiento == "IAF")
            {
                cbxTipoDestino.Visibility = (ObjECMP_Compra.Opcion == "I" || ObjECMP_Compra.Distribucion) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                lblTipoDestino.Visibility = cbxTipoDestino.Visibility;
                chkDistribucion.Visibility = System.Windows.Visibility.Visible;
                chkDistribucion.IsChecked = (ObjECMP_Compra.Opcion == "I") ? true : ObjECMP_Compra.Distribucion;
                dtgColumnaPeriodoCompania.Visibility = (ObjECMP_Compra.Distribucion) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                dgColumnAlmacen.Visibility = System.Windows.Visibility.Collapsed;
            }
            else if (CodMotivoMovimiento == "ICD")
            {
                chkAfectoAlmacen.Visibility = System.Windows.Visibility.Visible;
                chkAfectoAlmacen.IsChecked = (ObjECMP_Compra.Opcion == "I") ? true : ObjECMP_Compra.AfectaAlmacen;
                dgColumnAlmacen.Visibility = (chkAfectoAlmacen.IsChecked.Value) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                chkDistribucion.Visibility = System.Windows.Visibility.Visible;
                chkDistribucion.IsChecked = (ObjECMP_Compra.Opcion == "I") ? false : ObjECMP_Compra.Distribucion;
                cbxTipoDestino.Visibility = (chkDistribucion.IsChecked.Value) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                lblTipoDestino.Visibility = cbxTipoDestino.Visibility;
                dtgColumnaPeriodoCompania.Visibility = (ObjECMP_Compra.Distribucion) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            }
            else if(CodMotivoMovimiento == "GIS")
            {
                cbxTipoDestino.Visibility = (ObjECMP_Compra.Opcion == "I" || ObjECMP_Compra.Distribucion) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                lblTipoDestino.Visibility = cbxTipoDestino.Visibility;
                chkDistribucion.Visibility = System.Windows.Visibility.Visible;
                chkDistribucion.IsChecked = (ObjECMP_Compra.Opcion == "I") ? true : ObjECMP_Compra.Distribucion;
                dtgColumnaPeriodoCompania.Visibility = (ObjECMP_Compra.Distribucion) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed ;
            }

            if (ObjECMP_Compra.ObjEMNF_TipoDestino != null && (CodMotivoMovimiento == "GIS" || CodMotivoMovimiento == "ICD" || CodMotivoMovimiento == "IAF" || CodMotivoMovimiento == "IDS"))
            {
                cbxTipoDestino.SelectedValue = ((ObjECMP_Compra.Opcion == "I") ? "CTDIS" : ObjECMP_Compra.ObjEMNF_TipoDestino.CodTipoDestino);
            }
        }

        #endregion

        #region MÉTODO SERVICIO

        int intExoneradoIGV = 0;
        /// <summary>
        /// Instancia y pinta valor del servicio seleccionado de la busqueda
        /// </summary>
        /// <param name="ObjEMNF_Servicio">Objeto de la clase Servicio</param>
        public void AddItemsServicio(EMNF_Servicio ObjEMNF_Servicio, ECMP_CompraDetalle ObjECMP_CompraDetalle, bool IsEnabledColumnCantidad, bool IsEnabledColumnAlmcen, bool IsEnabledColumnPrecioUnitario)
        {
            ObjECMP_CompraDetalle.Item = dgCompraDetalle.Items.Count + 1;
            ObjECMP_CompraDetalle.IdArticuloServicio = ObjEMNF_Servicio.IdServicio;
            ObjECMP_CompraDetalle.Codigo = ObjEMNF_Servicio.IdServicio.ToString();
            ObjECMP_CompraDetalle.ArticuloServicio = ObjEMNF_Servicio.Servicio;
            ObjECMP_CompraDetalle.CodUndMedida = "UND";
            ObjECMP_CompraDetalle.CodOperacionIGV = ObjEMNF_Servicio.ObjEMNF_OperacionIGV.CodOperacionIGV;
            ObjECMP_CompraDetalle.TipoDetalle = "MNF_Servicio";
            ObjECMP_CompraDetalle.ListEALM_Almacen = ListEALM_Almacen;
            ObjECMP_CompraDetalle.IsEnabledColumnCantidad = IsEnabledColumnCantidad;
            ObjECMP_CompraDetalle.IsEnabledColumnAlmcen = IsEnabledColumnAlmcen;
            ObjECMP_CompraDetalle.IsEnabledColumnPrecioUnitario = IsEnabledColumnPrecioUnitario;
            ObjECMP_CompraDetalle.IdDestino = 0;
            ObjECMP_CompraDetalle.ListPeriodoCampania = ListPeriodoCampania;
            dgCompraDetalle.Items.Add(ObjECMP_CompraDetalle);
            CalcularTotalesArticulo();
            btnQuitarArtServ.IsEnabled = (dgCompraDetalle.Items.Count > 0);
            LoadTipoDestino();
        }

        /// <summary>
        /// Calculta los totales
        /// </summary>
        private void CalcularTotalesServicio()
        {
            try
            {
                if (strTempValueTitle.Trim().Length > 0)
                {
                    if (blIsGravada)
                    {
                        lblTitleCompra03.Text = strTempValueTitle;
                        lblTitleCompra03.Foreground = Brushes.White;
                    }
                    else
                    {

                        lblTitleCompra04.Text = strTempValueTitle;
                        lblTitleCompra04.Foreground = Brushes.White;
                    }
                }

                if (ObjECMP_Compra.ObjEMNF_OperacionMovimiento == null) return;
                var ListECMP_CompraDetalle = (dgCompraDetalle.Items.OfType<ECMP_CompraDetalle>()).ToList();

                #region
                if (ListECMP_CompraDetalle.Count > 0)
                {
                    foreach (var item in ListECMP_CompraDetalle)
                    {
                        item.ListPeriodoCampania = ListPeriodoCampania;
                    }
                }
                #endregion

                if (ListECMP_CompraDetalle != null && ListECMP_CompraDetalle.Count > 0)
                {
                    decimal dmlTotal = 0;
                    decimal dmlGravada = 0;
                    decimal dmlImporteIGV = 0;

                    if (intExoneradoIGV == 22)
                    {
                        //CALCULA 22
                        dmlTotal = ListECMP_CompraDetalle.Sum(o => o.Importe);
                        dmlGravada = 0;
                        dmlImporteIGV =0;

                        var vrListECMP_CompraDetalle = (dgCompraDetalle.Items.OfType<ECMP_CompraDetalle>()).ToList();
                        txtExoneradoArtServ.Text = vrListECMP_CompraDetalle.Sum(o => (o.CodOperacionIGV == "EX" ? o.Importe : 0)).ToString("###,###,##0.#0");

                    }
                    else
                    {
                        #region
                        //factura
                        if ((chkIncluyeIGV.IsChecked.Value || ObjECMP_Compra.IncluyeIGV) && ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento != "IDP" && ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento != "IPF")
                        {
                            //Calculo con incluir IGV
                            dmlGravada = ListECMP_CompraDetalle.Sum(x => (x.CodOperacionIGV== "GB" ? decimal.Round(x.Importe,3) : 0));
                            dmlImporteIGV = ListECMP_CompraDetalle.Sum(x => (x.CodOperacionIGV == "GB" ? decimal.Round(x.ImporteIGV,3) : 0));
                            dmlTotal = dmlGravada + dmlImporteIGV;

                            //Afecto percepción
                            if (chkAfectoPercepcion.IsChecked.Value)
                            {
                                decimal dmlCal = decimal.Round(dmlTotal * (decimal)dmlPercepcion, 8);
                                dmlTotal = decimal.Round(dmlTotal + dmlCal, 8);
                            }

                            lblTitleCompra03.Text = "Gravada";
                            lblTitleCompra04.Text = "IGV " + decimal.Round(dmlIGV * 100, 2) + "%";
                            lblTitleCompra05.Text = "Importe Total";

                        }
                        else if (ObjECMP_Compra.ObjEMNF_OperacionMovimiento.CodOpeMovimiento == "PPHN")
                        {
                            //Calculo cuando es Honorario
							if (ObjECMP_Compra.Retencion || chkRetencion.IsChecked.Value)
                                ListECMP_CompraDetalle.ForEach(x => dmlGravada += x.PrecioUnitario * x.Cantidad );
                            else
                                dmlGravada = ListECMP_CompraDetalle.Where(x => x.CodOperacionIGV == "GB").Sum(o => o.Importe);
                            decimal dmlTasa = 0;
                            if (ObjECMP_Compra.Retencion || chkRetencion.IsChecked.Value)
                                dmlTasa = (SGCMethod.GetTasaHonorario(dmlGravada) / 100);
                            dmlImporteIGV = dmlGravada * dmlTasa;
                            dmlTotal = dmlGravada - dmlImporteIGV;

                            lblTitleCompra03.Text = "Total Honorario";
                            lblTitleCompra04.Text = "Retención " + (decimal.Round(dmlTasa * 100, 0)) + "%";
                            lblTitleCompra05.Text = "Total Neto";
                        }
                        else if (ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "IDP" || ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "IPF")
                        {
                            if (ObjECMP_Compra.Retencion || chkRetencion.IsChecked.Value)
                                ListECMP_CompraDetalle.ForEach(x => dmlGravada += x.PrecioUnitario * x.Cantidad);

                            decimal dmlTasa = 0;
                            
                            if (ObjECMP_Compra.Retencion || chkRetencion.IsChecked.Value)
                                dmlTasa = dmlRentaSegunda;
                            
                            dmlImporteIGV = dmlGravada * dmlTasa;
                            dmlTotal = dmlGravada - dmlImporteIGV;

                            lblTitleCompra03.Text = "Renta Bruta";
                            lblTitleCompra04.Text = "Retención " + (decimal.Round(dmlTasa * 100, 0)) + "%";
                            lblTitleCompra05.Text = "Renta Neta " + ObjECMP_Compra.ObjESGC_Moneda.Simbolo;
                        }
                        else
                        {
                            if (chkPlanilla.IsChecked.Value)
                            {
                                dmlGravada = ListECMP_CompraDetalle.Sum(x => x.Cantidad);
                                dmlImporteIGV = ListECMP_CompraDetalle.Sum(x => x.PrecioUnitario); 
                                dmlTotal = dmlGravada - dmlImporteIGV;

                                lblTitleCompra03.Text = "Sueldo Bruto";
                                lblTitleCompra04.Text = "Descuento";
                                lblTitleCompra05.Text = "Sueldo Neto";
                            }
                            else
                            {
                                //Calculo sin incluir IGV
                                ListECMP_CompraDetalle.ForEach(x => { if (x.CodOperacionIGV == "GB") dmlGravada += x.PrecioUnitario * x.Cantidad; });
                                dmlImporteIGV = dmlGravada * dmlIGV;
                                dmlTotal = dmlGravada + dmlImporteIGV;

                                //Afecto percepción
                                if (chkAfectoPercepcion.IsChecked.Value)
                                {
                                    decimal dmlCal = decimal.Round(dmlTotal, 2) * (decimal)dmlPercepcion;
                                    dmlTotal = dmlTotal + dmlCal;
                                }

                                lblTitleCompra03.Text = "Gravada";
                                lblTitleCompra04.Text = "IGV " + decimal.Round(dmlIGV * 100, 2) + "%";
                                lblTitleCompra05.Text = "Importe Total";
                            }

                        }
                        #endregion
                    }
					
					List<ECMP_CompraDetalle> vrListECMP_CompraDetalleA = (dgCompraDetalle.Items.OfType<ECMP_CompraDetalle>()).ToList();
                    decimal dmlExonerada = 
                        (ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "IDP" || ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "IPF" || chkPlanilla.IsChecked.Value) ? 0 
                        : vrListECMP_CompraDetalleA.Sum(o => (o.CodOperacionIGV == "EX" ? o.Importe : 0));

                    txtLineasArtServ.Text = ListECMP_CompraDetalle.Count.ToString();
					txtExoneradoArtServ.Text = decimal.Round(dmlExonerada, 3).ToString("###,###,##0.#0");
                    txtGravadaArtServ.Text = decimal.Round(dmlGravada, 3).ToString("###,###,##0.#0");
                    txtTotalIgvArtServ.Text = decimal.Round(dmlImporteIGV, 3).ToString("###,###,##0.#0");

                    if (chkPlanilla.IsChecked.Value)
                        txtTotalNetoArtServ.Text = Convert.ToDouble(dmlTotal).ToString("N2");
                    else
                        txtTotalNetoArtServ.Text = (dmlExonerada > 0) ? Convert.ToDouble(dmlTotal + dmlExonerada).ToString("N2") : (dmlTotal).ToString("N2");
                }
                else
                {
                    txtLineasArtServ.Text = ("0");
                    txtGravadaArtServ.Text = ("0.00");
                    txtTotalIgvArtServ.Text = ("0.00");
                    txtTotalNetoArtServ.Text = ("0.00");
                }
                dgCompraDetalle.Items.Refresh();

                CmpGridSelectColumn.SelectCellByIndex(dgCompraDetalle, new List<CmpGridItem>()
                {
                    new CmpGridItem(){ColumnIni = 6, ColumnFin = 7}
                });
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Calcula los valores del registro seleccionado
        /// </summary>
        private void CalcularItemsServicio(ECMP_CompraDetalle ObjECMP_CompraDetalle)
        {
            try
            {
                if (ObjECMP_Compra.ObjEMNF_OperacionMovimiento == null) return;

                if (intExoneradoIGV == 22 || ObjECMP_CompraDetalle.CodOperacionIGV == "EX") // [22]
                {
                    //Calculo con incluir IGV [22]
                    decimal dmlImporte = 0;
                    decimal dmlImporteIGV = 0;
                    if (chkPlanilla.IsChecked.Value)
                    {
                        dmlImporte = decimal.Round((ObjECMP_CompraDetalle.Cantidad - ObjECMP_CompraDetalle.PrecioUnitario), 8);
                    }
                    else
                    {
                        dmlImporte = ObjECMP_CompraDetalle.PrecioUnitario * ObjECMP_CompraDetalle.Cantidad;
                        dmlImporteIGV = (dmlImporte / (((0 * 100) + 100) / 100)) * 0;
                    }

                    ObjECMP_CompraDetalle.Importe = decimal.Round(dmlImporte, 8);
                    ObjECMP_CompraDetalle.ImporteIGV = decimal.Round(dmlImporteIGV, 8);
                }
                else
                {
                    //factura
                    if ((ObjECMP_Compra.ObjEMNF_OperacionMovimiento.CodOpeMovimiento == "PPFC" || ObjECMP_Compra.ObjEMNF_OperacionMovimiento.CodOpeMovimiento == "PGID") && ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento != "IDP" && ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento != "IPF")
                    {
                        if (chkIncluyeIGV.IsChecked.Value || ObjECMP_Compra.IncluyeIGV)
                        {
                            //Calculo con incluir IGV
							decimal dmlPrecioUnitario = decimal.Round((ObjECMP_CompraDetalle.PrecioUnitarioTemp / ((decimal.Round(dmlIGV * 100, 2) + 100) / 100)), 8);
                            decimal dmlImporte = decimal.Round((dmlPrecioUnitario * ObjECMP_CompraDetalle.Cantidad), 8);
                            decimal dmlImporteIGV = decimal.Round(dmlImporte * dmlIGV, 8);

                            ObjECMP_CompraDetalle.PrecioUnitario = dmlPrecioUnitario;
                            ObjECMP_CompraDetalle.Importe = dmlImporte;
                            ObjECMP_CompraDetalle.ImporteIGV = dmlImporteIGV;
                        }
                        else
                        {
                            decimal dmlImporte = 0;
                            decimal dmlImporteIGV = 0;
                            if (chkPlanilla.IsChecked.Value)
                            {
                                dmlImporte = decimal.Round((ObjECMP_CompraDetalle.Cantidad - ObjECMP_CompraDetalle.PrecioUnitario), 8);
                            }
                            else
                            {
                                //Calculo sin incluir IGV
                                dmlImporte = decimal.Round((ObjECMP_CompraDetalle.PrecioUnitarioTemp * ObjECMP_CompraDetalle.Cantidad), 8);
                                dmlImporteIGV = decimal.Round(dmlImporte * dmlIGV, 8);
                            }

                            ObjECMP_CompraDetalle.PrecioUnitario = ObjECMP_CompraDetalle.PrecioUnitarioTemp;
                            ObjECMP_CompraDetalle.Importe = dmlImporte;
                            ObjECMP_CompraDetalle.ImporteIGV = dmlImporteIGV;
                        }
                    }
                    else if (ObjECMP_Compra.ObjEMNF_OperacionMovimiento.CodOpeMovimiento == "PPHN")
                    {
                        //Calculo cuando es Honorario
                        decimal dmlTasa = 0;
                        decimal dmlImporte = decimal.Round((ObjECMP_CompraDetalle.PrecioUnitario * ObjECMP_CompraDetalle.Cantidad), 8);
                        boolRetencion = chkRetencion.IsChecked.Value;
                        if (chkRetencion.IsChecked.Value)
                            dmlTasa = (SGCMethod.GetTasaHonorario(dmlImporte) / 100);
                        decimal dmlImporteIGV = (dmlImporte * dmlTasa);

                        ObjECMP_CompraDetalle.Importe = dmlImporte - dmlImporteIGV;
                        ObjECMP_CompraDetalle.ImporteIGV = dmlImporteIGV;
                    }
                    else if (ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "IDP" || ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "IPF")
                    {
                        decimal dmlTasa = 0;
                        decimal dmlImporte = decimal.Round((ObjECMP_CompraDetalle.PrecioUnitario * ObjECMP_CompraDetalle.Cantidad), 8);
                        boolRetencion = chkRetencion.IsChecked.Value;
                        if (chkRetencion.IsChecked.Value)
                            dmlTasa = dmlRentaSegunda;
                        decimal dmlImporteIGV = (dmlImporte * dmlTasa);

                        ObjECMP_CompraDetalle.Importe = dmlImporte - dmlImporteIGV;
                        ObjECMP_CompraDetalle.ImporteIGV = dmlImporteIGV;
                    }
                }

                CalcularTotalesServicio();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Carga datos del Tipo Destino
        /// </summary>
        private async void LoadTipoDestino()
        {
            try
            {
                string strSerie = txtSerie.Text.Trim();
                var vrObjESGC_Documento = new ESGC_Documento();

                if (ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "IDS" || ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "IDH")
                {
                    if (cbxEmpresaSucursal.SelectedValue == null) return;
                    ObjESGC_UsuarioEmpresaSucursal.ObjESGC_EmpresaSucursal = new ESGC_EmpresaSucursal() { IdEmpSucursal = Convert.ToInt32(cbxEmpresaSucursal.SelectedValue) };
                }

                if (cbxDocumento.SelectedValue == null) return;
                var vrCodDocumento = cbxDocumento.SelectedValue.ToString();

                if (ObjECMP_Compra.Opcion == "U" && vrCodDocumento == "RIG")
                {                    
                    vrObjESGC_Documento = new BSGC_Documento().GetNroDocumento(vrCodDocumento, Convert.ToInt32(strSerie));
                    ObjESGC_UsuarioEmpresaSucursal.ObjESGC_EmpresaSucursal = vrObjESGC_Documento.ObjESGC_EmpresaSucursal;
                }

                #region PERIODO
                if (ListPeriodoCampania != null)
                {
					ListPeriodoCampania.Clear();
                    int Anio = Convert.ToInt32(ObjECMP_Compra.Periodo.Substring(0, ObjECMP_Compra.Periodo.Length - 2));
                    for (int i = Anio - 1; i <= Anio + 1; i++)
                    {
                        ListPeriodoCampania.Add(new ECMP_ValueComboBox() { Item = i.ToString(), Value = i.ToString() });
                    }
                }
                #endregion

                ObjEMNF_TipoDestino = (EMNF_TipoDestino)cbxTipoDestino.SelectedItem;
                if (ObjEMNF_TipoDestino == null) return;
                var vrListECMP_ValueComboBox = new List<ECMP_ValueComboBox>();
                if (ObjEMNF_TipoDestino.CodTipoDestino == "CTDIS" || ObjEMNF_TipoDestino.CodTipoDestino == "CTSUC") //Cuenta por Distribuir General
                {
                    dtgColumnaTipoDestino.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    dtgColumnaTipoDestino.Header = ObjEMNF_TipoDestino.TipoDestino;
                    dtgColumnaTipoDestino.Visibility = (ObjECMP_Compra.Distribucion || ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "IDH" || ObjECMP_Compra.ObjEMNF_MotivoMovimiento.CodMotMovimiento == "IDP") ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;

                    await System.Threading.Tasks.Task.Factory.StartNew(
                    () =>
                    {
                        try
                        {
                            vrListECMP_ValueComboBox.Clear();
                            if (ObjEMNF_TipoDestino.CodTipoDestino == "CCOST" && ObjESGC_UsuarioEmpresaSucursal.ObjESGC_EmpresaSucursal != null && ObjESGC_UsuarioEmpresaSucursal.ObjESGC_EmpresaSucursal.IdEmpSucursal != 0) //Categoría Centro Costo (Listado Centro de Costo)
                            {
                                var vrListCentroCosto = new BMNF_CentroCosto().ListFiltrarCentroCosto(ObjESGC_UsuarioEmpresaSucursal.ObjESGC_EmpresaSucursal);
                                foreach (var item in vrListCentroCosto)
                                {
                                    vrListECMP_ValueComboBox.Add(new ECMP_ValueComboBox() { Item = item.Descripcion, Value = item.IdCentroCosto.ToString() });
                                }
                            }
                            else if (ObjEMNF_TipoDestino.CodTipoDestino == "SCOST" && ObjESGC_UsuarioEmpresaSucursal.ObjESGC_EmpresaSucursal != null && ObjESGC_UsuarioEmpresaSucursal.ObjESGC_EmpresaSucursal.IdEmpSucursal != 0) //Centro Costo (Listado Sub Centro de Costo)
                            {
                                ListCentroCosto = new BMNF_SubCentroCosto().ListGetSubCentroCosto(ObjESGC_UsuarioEmpresaSucursal.ObjESGC_EmpresaSucursal);
                                foreach (var item in ListCentroCosto)
                                {
                                    if (Convert.ToDateTime(item.Fecha).Date <= ObjECMP_Compra.Fecha.Date)
                                        vrListECMP_ValueComboBox.Add(new ECMP_ValueComboBox() { Item = item.Descripcion, Value = item.IdSubCenCosto.ToString() });
                                }
                            }
                            if (vrListECMP_ValueComboBox.Count == 0 && ObjESGC_UsuarioEmpresaSucursal.ObjESGC_EmpresaSucursal != null && ObjESGC_UsuarioEmpresaSucursal.ObjESGC_EmpresaSucursal.IdEmpSucursal != 0)
                                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "No hay Centro de Costo Registrado hasta la fecha " + ObjECMP_Compra.Fecha.Date.ToString("dd/MM/yyyy"), CmpButton.Aceptar);
                        }
                        catch (Exception ex)
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, ex.Message, CmpButton.Aceptar);
                        }
                    });
                }
				var vrObjListECMP_CompraDetalle = (dgCompraDetalle.Items.OfType<ECMP_CompraDetalle>()).ToList();
                foreach (var item in vrObjListECMP_CompraDetalle)
                {
                    if (item.Codigo != "DEF0000001")
                    {
                        item.ListCentroCosto = vrListECMP_ValueComboBox;
                        if (item.PrecioUnitario >= 0)
                            item.ListPeriodoCampania = ListPeriodoCampania;

                    }
                    if (ObjECMP_Compra.Opcion == "I" && (ListCentroCosto != null && ListCentroCosto.Count > vrListECMP_ValueComboBox.Count())) item.IdDestino = 0;
                }
                
                dgCompraDetalle.Items.Refresh();
            }
            catch (Exception ex)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, ex.Message, CmpButton.Aceptar);
            }
        }

        #endregion

        #region MÉTODO ARTÍCULO

        /// <summary>
        /// Instancia y pinta valor del Artículo seleccionado de la busqueda
        /// </summary>
        /// <param name="ObjEMNF_Articulo">Objeto de la clase Artículo</param>
        public void AddItemsArticulos(EMNF_Articulo ObjEMNF_Articulo, ECMP_CompraDetalle ObjECMP_CompraDetalle)
        {
            ObjECMP_CompraDetalle.Item = dgCompraDetalle.Items.Count + 1;
            ObjECMP_CompraDetalle.IdArticuloServicio = ObjEMNF_Articulo.IdArticulo;
            ObjECMP_CompraDetalle.Codigo = ObjEMNF_Articulo.Codigo;
            ObjECMP_CompraDetalle.ArticuloServicio = ObjEMNF_Articulo.Articulo;
            ObjECMP_CompraDetalle.CodUndMedida = ObjEMNF_Articulo.ObjEMNF_UnidadMedida.CodUndMedida;
            ObjECMP_CompraDetalle.CodOperacionIGV = ObjEMNF_Articulo.ObjEMNF_OperacionIGV.CodOperacionIGV;
            ObjECMP_CompraDetalle.TipoDetalle = "MNF_Articulo";
            ObjECMP_CompraDetalle.ListEALM_Almacen = ListEALM_Almacen;
            dgCompraDetalle.Items.Add(ObjECMP_CompraDetalle);
            chkAnticipo.IsChecked = (ObjECMP_CompraDetalle.Codigo == "DEF0000001");
            chkAnticipo_Click_1(chkAnticipo, new System.Windows.RoutedEventArgs());
            CalcularTotalesArticulo();
            btnQuitarArtServ.IsEnabled = (dgCompraDetalle.Items.Count > 0);
        }

        /// <summary>
        /// Calculta los totales
        /// </summary>
        private void CalcularTotalesArticulo()
        {
            try
            {
                lblTitleCompra03.Text = "Gravada";
                lblTitleCompra04.Text = "IGV " + decimal.Round(dmlIGV * 100, 2) + "%";
                lblTitleCompra05.Text = "Total " + ObjECMP_Compra.ObjESGC_Moneda.Simbolo;

                if (strTempValueTitle.Trim().Length > 0)
                {
                    if (blIsGravada)
                    {
                        lblTitleCompra03.Text = strTempValueTitle;
                        lblTitleCompra03.Foreground = Brushes.White;
                    }
                    else
                    {
                        lblTitleCompra04.Text = strTempValueTitle;
                        lblTitleCompra04.Foreground = Brushes.White;
                    }
                }

                var vrListECMP_CompraDetalle = (dgCompraDetalle.Items.OfType<ECMP_CompraDetalle>()).ToList();
                if (vrListECMP_CompraDetalle != null && vrListECMP_CompraDetalle.Count > 0)
                {
                    decimal dmlTotal = 0;
                    decimal dmlGravada = 0;
                    decimal dmlImporteIGV = 0;

                    //factura
                    if (chkIncluyeIGV.IsChecked.Value || ObjECMP_Compra.IncluyeIGV)
                    {
                        //Calculo con incluir IGV
                        dmlGravada = vrListECMP_CompraDetalle.Sum(x => (x.CodOperacionIGV == "GB" ? x.Importe : 0));
                        dmlImporteIGV = vrListECMP_CompraDetalle.Sum(x => (x.CodOperacionIGV == "GB" ? x.ImporteIGV : 0));
                        dmlTotal = dmlGravada + dmlImporteIGV;
                    }
                    else
                    {
                        //Calculo sin incluir IGV
                        vrListECMP_CompraDetalle.ForEach(x => { if (x.CodOperacionIGV == "GB") dmlGravada += x.PrecioUnitario * x.Cantidad; });
                        dmlImporteIGV = dmlGravada * dmlIGV;
                        dmlTotal = dmlGravada + dmlImporteIGV;
                    }

                    //Afecto percepción
                    if (chkAfectoPercepcion.IsChecked.Value)
                    {
                        decimal dmlCal = (dmlTotal * dmlPercepcion);
                        dmlTotal = (dmlTotal + dmlCal);
                    }

					if(cbxDocumento.SelectedValue.ToString() != "RCB")
						txtExoneradoArtServ.Text = vrListECMP_CompraDetalle.Sum(o => (o.CodOperacionIGV == "EX" ? o.Importe : 0)).ToString("###,###,##0.#0");

                    txtLineasArtServ.Text = vrListECMP_CompraDetalle.Count.ToString();
                    txtGravadaArtServ.Text = (decimal.Round(dmlGravada,3)).ToString("N2");
                    txtTotalIgvArtServ.Text = (decimal.Round(dmlImporteIGV,3)).ToString("N2");
                    txtTotalNetoArtServ.Text = Convert.ToDouble(Convert.ToDecimal(txtGravadaArtServ.Text) + (Convert.ToDecimal(txtTotalIgvArtServ.Text) + Convert.ToDecimal(txtExoneradoArtServ.Text))).ToString("N2");
                    //ObjECMP_Compra.Percepcion = dmlPercepcion;
                    //ObjECMP_Compra.Exonerada = Convert.ToDecimal(txtExoneradoArtServ.Text);
                    //ObjECMP_Compra.Gravada = (dmlGravada);
                    //ObjECMP_Compra.ImporteIGV = (dmlImporteIGV);
                }
                else
                {
                    txtLineasArtServ.Text = ("0");
                    txtGravadaArtServ.Text = ("0.00");
                    txtTotalIgvArtServ.Text = ("0.00");
                    txtExoneradoArtServ.Text = ("0.00");
                    txtTotalNetoArtServ.Text = ("0.00");
                }
                dgCompraDetalle.Items.Refresh();
                CmpGridSelectColumn.SelectCellByIndex(dgCompraDetalle, new List<CmpGridItem>()
                {
                    new CmpGridItem(){ColumnIni = 6, ColumnFin = 7}
                });
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Calcula los valores del registro seleccionado
        /// </summary>
        private void CalcularItemsArticulo(ECMP_CompraDetalle ObjECMP_CompraDetalle)
        {
            try
            {
                //factura
                if (ObjECMP_CompraDetalle.CodOperacionIGV == "GB" && !chkRetencion.IsChecked.Value)
                {
                    if (chkIncluyeIGV.IsChecked.Value || ObjECMP_Compra.IncluyeIGV)
                    {
                        //Calculo con incluir IGV
                        decimal dmlPrecioUnitario = decimal.Round((ObjECMP_CompraDetalle.PrecioUnitarioTemp / ((decimal.Round(dmlIGV * 100, 2) + 100) / 100)), 8); 
                        decimal dmlImporte = decimal.Round((dmlPrecioUnitario * ObjECMP_CompraDetalle.Cantidad), 8);
                        decimal dmlImporteIGV = decimal.Round((ObjECMP_CompraDetalle.PrecioUnitario < 0) ? ObjECMP_CompraDetalle.ImporteIGV : (dmlImporte * dmlIGV), 8);

                        ObjECMP_CompraDetalle.PrecioUnitario = dmlPrecioUnitario;
                        ObjECMP_CompraDetalle.Importe = (ObjECMP_CompraDetalle.PrecioUnitario < 0) ? dmlImporte - dmlImporteIGV : dmlImporte;
                        ObjECMP_CompraDetalle.ImporteIGV = dmlImporteIGV;
                    }
                    else
                    {
                        //Calculo sin incluir IGV
                        decimal dmlImporte = decimal.Round((ObjECMP_CompraDetalle.PrecioUnitarioTemp * ObjECMP_CompraDetalle.Cantidad), 8);
                        decimal dmlImporteIGV = decimal.Round(dmlImporte * dmlIGV, 8);

                        ObjECMP_CompraDetalle.PrecioUnitario = ObjECMP_CompraDetalle.PrecioUnitarioTemp;
                        ObjECMP_CompraDetalle.Importe = dmlImporte;
                        ObjECMP_CompraDetalle.ImporteIGV = dmlImporteIGV;
                    }
                }
                else
                {
                    ObjECMP_CompraDetalle.ImporteIGV = 0;
                    ObjECMP_CompraDetalle.Importe = decimal.Round(((ObjECMP_CompraDetalle.PrecioUnitario * ObjECMP_CompraDetalle.Cantidad) + ObjECMP_CompraDetalle.ImporteIGV), 8);
                }
                CalcularTotalesArticulo();
            }
            catch (Exception) {}
        }

        #endregion        
    }

    class DocumentoReferencia 
    {
        public string Value { get; set; }
        public string Item { get; set; }

        public static List<DocumentoReferencia> GetFactura
        {
            get 
            {
                return new List<DocumentoReferencia>() 
                {
                    new DocumentoReferencia()
                    {
                        Item = "Ninguno",
                        Value = "NGN"
                    },
                    new DocumentoReferencia()
                    {
                        Item = "Recepción Guía",
                        Value = "GRC"
                    },
                    new DocumentoReferencia()
                    {
                        Item = "Orden Compra",
                        Value = "ODC"
                    },
                    new DocumentoReferencia()
                    {
                        Item = "Orden Servicio",
                        Value = "ODS"
                    }
                };
            }
        }

        public static List<DocumentoReferencia> GetProvisionActivo
        {
            get
            {
                return new List<DocumentoReferencia>() 
                {
                    new DocumentoReferencia()
                    {
                        Item = "Ninguno",
                        Value = "NGN"
                    }
                    //,
                    //new DocumentoReferencia()
                    //{
                    //    Item = "Anticipo",
                    //    Value = "ATP"
                    //}
                };
            }
        }

        public static List<DocumentoReferencia> GetProvisionInsumo
        {
            get
            {
                return new List<DocumentoReferencia>() 
                {
                    new DocumentoReferencia()
                    {
                        Item = "Recepción Guía",
                        Value = "GRC"
                    },
                    new DocumentoReferencia()
                    {
                        Item = "Orden Compra",
                        Value = "ODC"
                    },
                };
            }
        }

        public static List<DocumentoReferencia> GetDirecta
        {
            get
            {
                return new List<DocumentoReferencia>() 
                {
                    new DocumentoReferencia()
                    {
                        Item = "Ninguno",
                        Value = "NGN"
                    },
                };
            }
        }

        public static List<DocumentoReferencia> GetHonorario
        {
            get
            {
                return new List<DocumentoReferencia>() 
                {
                    new DocumentoReferencia()
                    {
                        Item = "Orden Servicio",
                        Value = "ODS"
                    }
                };
            }
        }

        public static List<DocumentoReferencia> GetRID
        {
            get
            {
                return new List<DocumentoReferencia>() 
                {
                    new DocumentoReferencia()
                    {
                        Item = "Ninguno",
                        Value = "NGN"
                    }
                };
            }
        }
    }
}
