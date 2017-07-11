namespace CMP.Presentation.OrdenCompra
{
    using CMP.Business;
    using CMP.Entity;
    using CMP.Reports;
    using CMP.Useful.Metodo;
    using CMP.Useful.Modulo;
    using ComputerSystems;
    using ComputerSystems.MethodList;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.Buttons;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using SGC.Empresarial.Entity;
    using SGC.Empresarial.Method;
    using SGC.Empresarial.Useful.Modulo;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    public partial class PCMP_ListadoOrdenCompra : UserControl, CmpINavigation
    {
        #region INSTANCIAS, VARIABLES, EVENTOS AGREGADOS

        private ECMP_OrdenCompra ObjECMP_OrdenCompra;
        private List<ECMP_OrdenCompraDetalle> ObjListECMP_OrdenCompraDetalle = new List<ECMP_OrdenCompraDetalle>();
        private NumLetra NumLetras = new NumLetra();
        #endregion

        #region EVENTOS DE LA CLASE

        public PCMP_ListadoOrdenCompra()
        {
            DataContext = this;
            InitializeComponent();
        }

        private Frame _MyFrame;
        public Frame MyFrame
        {
            set 
            {
                _MyFrame = value;
            }
        }

        public object Parameter
        {
            set
            {
                int delta = DayOfWeek.Monday - DateTime.Now.DayOfWeek;

                ObjECMP_OrdenCompra = new ECMP_OrdenCompra()
                {
                    ObjEMNF_ClienteProveedor = new MNF.Entity.EMNF_ClienteProveedor() { IdCliProveedor = 0 },
                    ObjESGC_Estado = new ESGC_Estado() { CodEstado = "%" },
                    ObjESGC_FormaPago = new ESGC_FormaPago() { IdFormaPago = -1 },
                    ObjESGC_Moneda = new ESGC_Moneda() { CodMoneda = string.Empty },
                };
                this.KeyDownCmpButtonTitleTecla(ActionF6: btnNuevoIsClicked,
                                                ActionF2: btnEditarIsClicked,
                                                ActionF5: btnAprobarIsClicked,
                                                ActionF1: btnAnularIsClicked,
                                                ActionESC: btnSalirIsClicked,
                                                ActionCtrlV: btnVisualizarIsClicked);

                if (dtpFechaDesde.SelectedDate == null)
                    dtpFechaDesde.SelectedDate = DateTime.Now;
                if (dtpFechaHasta.SelectedDate == null)
                    dtpFechaHasta.SelectedDate = DateTime.Now;

                dtpFechaDesde.SelectedDateChanged += _SelectedDateChanged;
                dtpFechaHasta.SelectedDateChanged += _SelectedDateChanged;

                cbxOpcion.SelectionChanged += cbxOpcion_SelectionChanged;
                txtFiltrar.KeyUp += txtFiltrar_KeyUp_1;

                cbxOpcion.SelectedIndex = 0;

                MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminCompra, CMPMensajes.GetAccesoRestringidoNull("Compra"), new Action<ESGC_PermisoPerfil>((P) =>
                {
                    if (P == null) { return; }
                    btnAprobar.IsEnabled = P.Nuevo;
                    btnAnular.IsEnabled = P.Eliminar;
                    btnNuevo.IsEnabled = P.Nuevo;
                    btnEditar.IsEnabled = P.Editar;
                }));
                LoadDetail();
            }
        }

        private void _SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbxOpcion.SelectedIndex == 0)
            {                
                LoadDetail();
            }
        }

        private void cbxOpcion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dtpFechaDesde.IsEnabled = false;
            dtpFechaHasta.IsEnabled = false;

            int intIndex = cbxOpcion.SelectedIndex;
            if (intIndex == 0)
            {
                GridFiltrarFecha.Visibility = Visibility.Visible;
                GridFiltrarDescripcion.Visibility = System.Windows.Visibility.Collapsed;

                ObjECMP_OrdenCompra.Opcion = "F";
                dtpFechaDesde.IsEnabled = true;
                dtpFechaHasta.IsEnabled = true;

                string strToolTip = string.Empty;
                txtFiltrar.ToolTip = strToolTip;
                lblFiltrar.Text = string.Empty;
            }
            else if (intIndex == 1)
            {
                GridFiltrarFecha.Visibility = Visibility.Collapsed;
                GridFiltrarDescripcion.Visibility = System.Windows.Visibility.Visible;

                ObjECMP_OrdenCompra.Opcion = "P";
                txtFiltrar.Focus();

                string strWatermarkProperty = "Filtrar por Razón Social";
                string strToolTip = "Filtrar por Razón Social";
                txtFiltrar.ToolTip = strToolTip;
                lblFiltrar.Text = strWatermarkProperty;
                txtFiltrar.Text = string.Empty;
            }
            else if (intIndex == 2)
            {
                GridFiltrarFecha.Visibility = Visibility.Collapsed;
                GridFiltrarDescripcion.Visibility = System.Windows.Visibility.Visible;
                ObjECMP_OrdenCompra.Opcion = "M";
                txtFiltrar.Focus();

                string strWatermarkProperty = "Filtrar por Moneda";
                string strToolTip = "Filtrar por Moneda";
                txtFiltrar.ToolTip = strToolTip;
                lblFiltrar.Text = strWatermarkProperty;
                txtFiltrar.Text = string.Empty;
            }
            else if (intIndex == 3)
            {
                GridFiltrarFecha.Visibility = Visibility.Collapsed;
                GridFiltrarDescripcion.Visibility = System.Windows.Visibility.Visible;
                ObjECMP_OrdenCompra.Opcion = "D";
                txtFiltrar.Focus();

                string strWatermarkProperty = "Filtrar por Número documento";
                string strToolTip = "Filtrar por Número documento";
                txtFiltrar.ToolTip = strToolTip;
                lblFiltrar.Text = strWatermarkProperty;
                txtFiltrar.Text = string.Empty;
            }
            LoadDetail("%");
        }

        private void txtFiltrar_KeyUp_1(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string strFiltro = (txtFiltrar.Text == "") ? "%" : txtFiltrar.Text;
            LoadDetail(strFiltro);
        }

        public void btnExportarIsClicked()
        {
            if (dtgOrdenCompra.Items.Count == 0)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, "No hay Datos que Exportar", CmpButton.Aceptar);
                return;
            }
            var vrObjListECMP_OrdenCompraDetalle = (dtgOrdenCompra.Items.OfType<ECMP_OrdenCompra>()).ToList();
            try
            {
                var ListarExcel = vrObjListECMP_OrdenCompraDetalle.Select(x
                       => new
                       {
                           IdOrdenCompra = x.IdOrdenCompra,
                           Sucursal = x.ObjESGC_EmpresaSucursal.Sucursal,
                           AlmacenDestino = x.ObjEALM_Almacen.Almacen,
                           Moneda = x.ObjESGC_Moneda.Descripcion,
                           NumeroDocumento = x.DocumenSerie,
                           RazonSocial = x.ObjEMNF_ClienteProveedor.RazonSocial,
                           Fecha = x.Fecha.ToShortDateString(),
                           Estado = x.ObjESGC_Estado.Estado,
                           FechaEntrega = x.FechaEntrega.ToShortDateString(),
                           DiasRetraso = x.DiasRetraso,
                           Provisionado = x.ProvicionadoText,
                           Gravada = x.Gravada,
                           Exonerado = x.Exonerada,
                           ImporteIgv = x.ImporteIGV,
                           Total = (x.Gravada + x.Exonerada + x.ImporteIGV)
                       }).ToList();
                ListarExcel.Export("ListaOrdenCompra", ExportType.Excel, (value) =>
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, value, CmpButton.Aceptar);
                }); 
            }
            catch (Exception ex)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, ex.Message, CmpButton.Aceptar);
            }
        }

        public void btnNuevoIsClicked()
        {
            if (!btnNuevo.IsEnabled)
                return;

            MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.GetAccesoRestringidoNull("Orden Compra"), new Action<ESGC_PermisoPerfil>((P) =>
            {
                try
                {
                    if (P.Nuevo)
                    {
                        new CmpNavigationService().Ir(new PCMP_OrdenCompra(), _MyFrame, new ECMP_OrdenCompra(null, TipoConstructor.Insert));
                    }
                    else
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.GetAccesoRestringidoNuevo("Orden Compra"), CmpButton.Aceptar);
                    }
                }
                catch(Exception ex)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, ex.Message, CmpButton.Aceptar);
                }
            }));
        }

        public void btnEditarIsClicked()
        {
            if (!btnEditar.IsEnabled)
                return;

            MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.GetAccesoRestringidoNull("Orden Compra"), new Action<ESGC_PermisoPerfil>((P) =>
            {
                try
                {
                    if (P.Editar)
                    {
                        if (dtgOrdenCompra.SelectedItems.Count == 0)
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.IsNullSelectItem, CmpButton.Aceptar);
                            return;
                        }

                        var vrObjECMP_OrdenCompra = new ECMP_OrdenCompra(dtgOrdenCompra.SelectedItem, TipoConstructor.Update);

                        //if (vrObjECMP_OrdenCompra.ObjESGC_Estado.CodEstado != "PECOC")
                        //{
                        //    new CmpNavigationService().Ir(new PCMP_ViewOrdenCompra(), _MyFrame, vrObjECMP_OrdenCompra);
                        //}
                        //else
                        //{
                            new CmpNavigationService().Ir(new PCMP_OrdenCompra(), _MyFrame, vrObjECMP_OrdenCompra);
                        //}
                    }
                    else
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.GetAccesoRestringidoEditar("Orden Compra"), CmpButton.Aceptar);
                    }
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, ex.Message, CmpButton.Aceptar);
                }
            }));
        }

        public void btnVisualizarIsClicked()
        {
            if (!btnVisualizar.IsEnabled)
                return;

            MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.GetAccesoRestringidoNull("Orden Compra"), new Action<ESGC_PermisoPerfil>((P) =>
            {
                try
                {
                    if (P.Consulta)
                    {
                        if (dtgOrdenCompra.SelectedItems.Count == 0)
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.IsNullSelectItem, CmpButton.Aceptar);
                            return;
                        }

                        ObjECMP_OrdenCompra = (ECMP_OrdenCompra)dtgOrdenCompra.SelectedItem;
                        ObjListECMP_OrdenCompraDetalle = new BCMP_OrdenCompraDetalle().ListAdministrarOrdenCompraDetalle(ObjECMP_OrdenCompra);

                        foreach (var item in ObjListECMP_OrdenCompraDetalle)
                        {
                            if (item.ObjEMNF_Articulo.ObjEMNF_OperacionIGV.CodOperacionIGV == "GB")
                            {
                                if (ObjECMP_OrdenCompra.IncluyeIGV)
                                {
                                    //pRECIO INCLUIDO IGV
                                    decimal dmlCalculoIGV = item.PrecioUnitario * ObjECMP_OrdenCompra.IGV;
                                    item.PrecioUnitario = decimal.Round(dmlCalculoIGV + item.PrecioUnitario, 8);
                                }
                            }

                            //factura
                            if (item.ObjEMNF_Articulo.ObjEMNF_OperacionIGV.CodOperacionIGV == "GB")
                            {
                                if (ObjECMP_OrdenCompra.IncluyeIGV)
                                {
                                    //Calculo con incluir IGV
                                    decimal dmlImporte = item.PrecioUnitario * item.Cantidad;
                                    decimal dmlImporteIGV = (dmlImporte / ((decimal.Round(ObjECMP_OrdenCompra.IGV * 100, 2) + 100) / 100)) * ObjECMP_OrdenCompra.IGV;

                                    item.Importe = dmlImporte;
                                    item.ImporteIGV = dmlImporteIGV;
                                }

                                else
                                {
                                    //Calculo sin incluir IGV
                                    decimal dmlImporte = item.PrecioUnitario * item.Cantidad;
                                    decimal dmlImporteIGV = dmlImporte * ObjECMP_OrdenCompra.IGV;

                                    item.Importe = dmlImporte;
                                    item.ImporteIGV = dmlImporteIGV;
                                }
                            }
                            else
                            {
                                item.ImporteIGV = 0;
                                item.Importe = (item.PrecioUnitario * item.Cantidad) + item.ImporteIGV;
                            }
                        }
                        ImprimirOrdenCompra(ObjECMP_OrdenCompra, ObjListECMP_OrdenCompraDetalle);
                    }
                    else
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.GetAccesoRestringidoEditar("Orden Compra"), CmpButton.Aceptar);
                    }
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, ex.Message, CmpButton.Aceptar);
                }
            }));
        }

        public void btnAprobarIsClicked()
        {
            if (!btnAprobar.IsEnabled)
                return;

            MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.GetAccesoRestringidoNull("Aprobación Orden Compra"), new Action<ESGC_PermisoPerfil>((P) =>
            {
                try
                {
                    if (dtgOrdenCompra.SelectedItems.Count == 0)
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.IsNullSelectItem, CmpButton.Aceptar);
                        return;
                    }

                    var vrObjECMP_OrdenCompra = new ECMP_OrdenCompra(dtgOrdenCompra.SelectedItem, TipoConstructor.Update);
                    if (vrObjECMP_OrdenCompra.ObjESGC_Estado.CodEstado == "PECOC")
                    {
                        new CmpNavigationService().Ir(new PCMP_AprobacionOrdenCompra(), _MyFrame, vrObjECMP_OrdenCompra);
                    }
                    else
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, "Solo se puede dar Aprobado a las Ordenes de Compras Pendientes", CmpButton.Aceptar);
                    }
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, ex.Message, CmpButton.Aceptar);
                }
            }), "PCMP_AprobacionOrdenCompra");
        }

        public void btnAnularIsClicked()
        {
            if (!btnAnular.IsEnabled)
                return;

            MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.GetAccesoRestringidoNull("Orden Compra"), new Action<ESGC_PermisoPerfil>((P) =>
            {
                try
                {
                    if (P.Eliminar)
                    {
                        if (dtgOrdenCompra.SelectedItems.Count == 0)
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.IsNullSelectItem, CmpButton.Aceptar);
                            return;
                        }

                        var vrObjECMP_OrdenCompra = new ECMP_OrdenCompra(dtgOrdenCompra.SelectedItem, TipoConstructor.Update);

                        CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.PreguntaContinuarProceso, CmpButton.AceptarCancelar, () =>
                        {
                            string strOutMessageError = string.Empty;
                            CmpMessageBox.Proccess(CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.ProcesandoDatos, () =>
                            {
                                try
                                {
                                    vrObjECMP_OrdenCompra.Opcion = "N";
                                    new BCMP_OrdenCompra().TransOrdenCompra(vrObjECMP_OrdenCompra);
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
                                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.DatoProcesados, CmpButton.Aceptar);
                                    LoadDetail();
                                }
                            });
                        });
                    }
                    else
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.GetAccesoRestringidoEliminar("Orden Compra"), CmpButton.Aceptar);
                    }
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, ex.Message, CmpButton.Aceptar);
                }
            }));
        }

        public void btnSalirIsClicked()
        {
            if (!btnSalir.IsEnabled)
                return;

            cbxOpcion.SelectionChanged -= cbxOpcion_SelectionChanged;
            txtFiltrar.KeyUp -= txtFiltrar_KeyUp_1;
            dtpFechaDesde.SelectedDateChanged -= _SelectedDateChanged;
            dtpFechaHasta.SelectedDateChanged -= _SelectedDateChanged;

            this.Close(TipoModulo.ManuFactura);
        }
        #endregion

        #region MÉTODOS DE LA CLASE

        /// <summary>
        /// Carga datos Orden Compra
        /// </summary>
        /// <param name="Filtro">Datos para filtrar</param>
        public void LoadDetail(string Filtro = "%")
        {
            var vrListECMP_OrdenCompra = new List<ECMP_OrdenCompra>();

            string strOutMessageError = string.Empty;

            //Actualiza parametros de filtro
            if (cbxOpcion.SelectedIndex == 0)
            {
                ObjECMP_OrdenCompra.Fecha = (dtpFechaDesde.SelectedDate != null) ? dtpFechaDesde.SelectedDate.Value : DateTime.Now;
                ObjECMP_OrdenCompra.FechaEntrega = (dtpFechaHasta.SelectedDate != null) ? dtpFechaHasta.SelectedDate.Value : DateTime.Now;
            }

            CmpTask.Process(
            () =>
            {
                try
                {
                    vrListECMP_OrdenCompra = new BCMP_OrdenCompra().ListOrdenCompra(ObjECMP_OrdenCompra, Filtro);
                    foreach (var item in vrListECMP_OrdenCompra)
                    {
                        if (item.ObjESGC_Estado.CodEstado == "APCOC")
                        {
                            var fechahoy = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                            var FechaEntrega = Convert.ToDateTime(item.FechaEntrega.ToShortDateString());
                            TimeSpan ts = fechahoy - FechaEntrega;
                            item.DiasRetraso =Convert.ToInt32( ts.TotalDays);
                        }
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
                    dtgOrdenCompra.ItemsSource = vrListECMP_OrdenCompra;
                    lblCountItems.Text = vrListECMP_OrdenCompra.Count + " Registros";
                }
            });

        }

        private void ImprimirOrdenCompra(ECMP_OrdenCompra objECMP_OrdenCompra, List<ECMP_OrdenCompraDetalle> ListECMP_OrdenCompraDetalle)
        {
            try
            {
                string[] parametro;
                ListECMP_OrdenCompraDetalle.ForEach((f) =>
                {
                    if (f.ObjEMNF_Articulo.IdArticulo != 0)
                    {
                        decimal dmlPrecioUnitario = 0;

                        if ((f.ObjEMNF_Articulo.ObjEMNF_OperacionIGV.CodOperacionIGV == "GB"))
                        {
                            if (ObjECMP_OrdenCompra.IncluyeIGV)
                            {
                                dmlPrecioUnitario = (f.PrecioUnitario) / ((decimal.Round(ObjECMP_OrdenCompra.IGV * 100, 2) + 100) / 100);
                            }
                            else
                            {
                                dmlPrecioUnitario = f.PrecioUnitario;
                            }
                        }
                        else
                        {
                            dmlPrecioUnitario = f.PrecioUnitario;
                        }
                        f.PrecioUnitario = dmlPrecioUnitario;
                        f.Importe = f.PrecioUnitario * f.Cantidad;
                    }
                });

                string strIgvText = "IGV " + string.Format("{0:N2}", decimal.Round((ObjECMP_OrdenCompra.IGV * 100), 2)) + "%";
                string Monto = Decimal.Round((objECMP_OrdenCompra.Gravada + objECMP_OrdenCompra.Exonerada + objECMP_OrdenCompra.ImporteIGV), 2).ToString();
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
                    "prmLugarEntrega|"      + ObjECMP_OrdenCompra.LugarEntrega,
                    "prmIgv|"               + ObjECMP_OrdenCompra.ObjESGC_Moneda.Simbolo + " "+string.Format( "{0:N2}", ObjECMP_OrdenCompra.ImporteIGV),
                    "prmNetoLetra|"         + NumLetras.Convertir(Monto,true,ObjECMP_OrdenCompra.ObjESGC_Moneda.Descripcion),
                    "prmNeto|"              + ObjECMP_OrdenCompra.ObjESGC_Moneda.Simbolo + " "+string.Format( "{0:N2}", Convert.ToDecimal(Monto)),
                    "prmExonerado|"         + ObjECMP_OrdenCompra.ObjESGC_Moneda.Simbolo + " "+string.Format( "{0:N2}",decimal.Round(ObjECMP_OrdenCompra.Exonerada)),  
                    "prmIgvText|"           + strIgvText  ,
                    "prmGravada|"           + ObjECMP_OrdenCompra.ObjESGC_Moneda.Simbolo + " "+string.Format( "{0:N2}",decimal.Round(ObjECMP_OrdenCompra.Gravada,2)),
                    "prmSucursal|"          + ObjECMP_OrdenCompra.ObjESGC_EmpresaSucursal.Sucursal,
                    "prmDireccionSucursal|" + ObjECMP_OrdenCompra.LugarEntrega,
                    "prmFechaEntrega|"      + ObjECMP_OrdenCompra.FechaEntrega.ToShortDateString(),
                    "prmCreadopor|"         + ObjECMP_OrdenCompra.Creacion ,
                    "prmMoneda|"            + ObjECMP_OrdenCompra.ObjESGC_Moneda.Descripcion,
                    "prmAprobadorpor|"      + ObjECMP_OrdenCompra.Aprobacion
                };

                MainRerport ObjMainRerport = new MainRerport();
                ObjMainRerport.InitializeMainRerport("CMP.Reports.Files.RptOrdenCompra.rdlc", "dtsOrdenCompra", ListECMP_OrdenCompraDetalle, parametro);
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
