namespace CMP.Presentation.OrdenServicio
{
    using ALM.Business;
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
    using MahApps.Metro.Controls;
    using SGC.Empresarial.Entity;
    using SGC.Empresarial.Method;
    using SGC.Empresarial.Useful.Modulo;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    public partial class PCMP_ListadoOrdenServicio : UserControl, CmpINavigation
    {
        #region INSTANCIAS, VARIABLES, EVENTOS AGREGADOS

        private ECMP_OrdenServicio ObjECMP_OrdenServicio;
        private List<ECMP_OrdenServicioDetalle> ObjListECMP_OrdenServicioDetalle = new List<ECMP_OrdenServicioDetalle>();
        private NumLetra NumLetras = new NumLetra();


        #endregion

        #region EVENTOS DE LA CLASE

        public PCMP_ListadoOrdenServicio()
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
                int delta = DayOfWeek.Monday - DateTime.Now.DayOfWeek;
                ObjECMP_OrdenServicio = new ECMP_OrdenServicio()
                {
                    ObjEMNF_ClienteProveedor = new MNF.Entity.EMNF_ClienteProveedor() { IdCliProveedor = -1 },
                    ObjESGC_Estado = new ESGC_Estado() { CodEstado = "%" },
                    ObjESGC_Moneda = new ESGC_Moneda() { CodMoneda = string.Empty },
                    FechaInicio = DateTime.Now.AddDays(delta)
                };
                this.KeyDownCmpButtonTitleTecla(
                                                ActionF6: btnNuevoIsClicked,
                                                ActionF2: btnEditarIsClicked,
                                                ActionF5: btnAprobarIsClicked,
                                                ActionF1: btnAnularIsClicked,
                                                ActionESC: btnSalirIsClicked,
                                                ActionCtrlV: btnVisualizarIsClicked);

                if (dtpFechaDesde.SelectedDate == null)
                    dtpFechaDesde.SelectedDate = DateTime.Now;
                if (dtpFechaHasta.SelectedDate == null)
                    dtpFechaHasta.SelectedDate = DateTime.Now;

                cbxOpcion.SelectionChanged += cbxOpcion_SelectionChanged;
                txtFiltrar.KeyUp += txtFiltrar_KeyUp_1;
                dtpFechaDesde.SelectedDateChanged += _SelectedDateChanged;
                dtpFechaHasta.SelectedDateChanged += _SelectedDateChanged;

                cbxOpcion.SelectedIndex = -1;
                cbxOpcion.SelectedIndex = 0;

                ObjECMP_OrdenServicio.FechaInicio = (dtpFechaDesde.SelectedDate != null) ? dtpFechaDesde.SelectedDate.Value : DateTime.Now;
                ObjECMP_OrdenServicio.FechaFin = (dtpFechaHasta.SelectedDate != null) ? dtpFechaHasta.SelectedDate.Value : DateTime.Now;
                
                LoadDetail();

                MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminCompra, CMPMensajes.GetAccesoRestringidoNull("Compra"), new Action<ESGC_PermisoPerfil>((P) =>
                {
                    if (P == null) { return; }
                    btnAprobar.IsEnabled = P.Nuevo;
                    btnAnular.IsEnabled = P.Eliminar;
                    btnNuevo.IsEnabled = P.Nuevo;
                    btnEditar.IsEnabled = P.Editar;
                }));
            }
        }

        private void _SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxOpcion.SelectedIndex == 0)
            {
                ObjECMP_OrdenServicio.FechaInicio = (dtpFechaDesde.SelectedDate != null) ? dtpFechaDesde.SelectedDate.Value : DateTime.Now;
                ObjECMP_OrdenServicio.FechaFin = (dtpFechaHasta.SelectedDate != null) ? dtpFechaHasta.SelectedDate.Value : DateTime.Now; 
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

                ObjECMP_OrdenServicio.Opcion = "F";
                dtpFechaDesde.IsEnabled = true;
                dtpFechaHasta.IsEnabled = true;

                string strWatermarkProperty = string.Empty;
                string strToolTip = string.Empty;
                txtFiltrar.Text = string.Empty;
                txtFiltrar.SetValue(TextBoxHelper.WatermarkProperty, strWatermarkProperty);
                txtFiltrar.ToolTip = strToolTip;
                LoadDetail("%");
            }
            else if (intIndex == 1)
            {
                GridFiltrarFecha.Visibility = Visibility.Collapsed;
                GridFiltrarDescripcion.Visibility = System.Windows.Visibility.Visible;

                ObjECMP_OrdenServicio.Opcion = "P";

                string strWatermarkProperty = "Filtrar por Razón Social";
                string strToolTip = "Filtrar por Razón Social";
                txtFiltrar.SetValue(TextBoxHelper.WatermarkProperty, strWatermarkProperty);
                txtFiltrar.ToolTip = strToolTip;
                lblFiltrar.Text = strWatermarkProperty;
                txtFiltrar.Text = string.Empty;
                txtFiltrar.Focus();
                LoadDetail("%");
            }
            else if (intIndex == 2)
            {
                GridFiltrarFecha.Visibility = Visibility.Collapsed;
                GridFiltrarDescripcion.Visibility = System.Windows.Visibility.Visible;

                ObjECMP_OrdenServicio.Opcion = "M";

                string strWatermarkProperty = "Filtrar por Moneda";
                string strToolTip = "Filtrar por Moneda";
                txtFiltrar.SetValue(TextBoxHelper.WatermarkProperty, strWatermarkProperty);
                txtFiltrar.ToolTip = strToolTip;
                txtFiltrar.Text = string.Empty;
                lblFiltrar.Text = strWatermarkProperty;
                lblFiltrar.Focus();
                LoadDetail("%");
            }
            else if (intIndex == 3)
            {
                GridFiltrarFecha.Visibility = Visibility.Collapsed;
                GridFiltrarDescripcion.Visibility = System.Windows.Visibility.Visible;

                ObjECMP_OrdenServicio.Opcion = "D";

                string strWatermarkProperty = "Filtrar por Número documento";
                string strToolTip = "Filtrar por Número documento";
                txtFiltrar.SetValue(TextBoxHelper.WatermarkProperty, strWatermarkProperty);
                txtFiltrar.ToolTip = strToolTip;
                txtFiltrar.Text = string.Empty;
                lblFiltrar.Text = strWatermarkProperty;
                lblFiltrar.Focus();
                LoadDetail("%");
            }
        }

        private void txtFiltrar_KeyUp_1(object sender, System.Windows.Input.KeyEventArgs e)
        {
            LoadDetail(txtFiltrar.Text);
        }

        public void btnNuevoIsClicked()
        {
            if (!btnNuevo.IsEnabled)
                return;

            MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminOrdenServicio, CMPMensajes.GetAccesoRestringidoNull("Orden Servicio"), new Action<ESGC_PermisoPerfil>((P) =>
            {
                try
                {
                    if (P.Nuevo)
                    {
                        new CmpNavigationService().Ir(new PCMP_OrdenServicio(), _MyFrame, new ECMP_OrdenServicio(null, TipoConstructor.Insert));
                    }
                    else
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, CMPMensajes.GetAccesoRestringidoNuevo("Orden Servicio"), CmpButton.Aceptar);
                    }
                }
                catch(Exception ex)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, ex.Message, CmpButton.Aceptar);
                }
            }));
        }

        public void btnEditarIsClicked()
        {
            if (!btnEditar.IsEnabled)
                return;

            MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminOrdenServicio, CMPMensajes.GetAccesoRestringidoNull("Orden Servicio"), new Action<ESGC_PermisoPerfil>((P) =>
            {
                try
                {
                    if (P.Editar)
                    {
                        if (dtgOrdenServicio.SelectedItems.Count == 0)
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, CMPMensajes.IsNullSelectItem, CmpButton.Aceptar);
                            return;
                        }

                        var vrObjECMP_OrdenServicio = new ECMP_OrdenServicio(dtgOrdenServicio.SelectedItem, TipoConstructor.Update);

                        //if (vrObjECMP_OrdenServicio.ObjESGC_Estado.CodEstado != "PECOS")
                        //{
                        //    new CmpNavigationService().Ir(new PCMP_ViewOrdenServicio(), _MyFrame, vrObjECMP_OrdenServicio);
                        //}
                        //else
                        //{
                            new CmpNavigationService().Ir(new PCMP_OrdenServicio(), _MyFrame, vrObjECMP_OrdenServicio);
                        //}
                    }
                    else
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, CMPMensajes.GetAccesoRestringidoEditar("Orden Servicio"), CmpButton.Aceptar);
                    }
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, ex.Message, CmpButton.Aceptar);
                }
            }));
        }

        public void btnAprobarIsClicked()
        {
            if (!btnAprobar.IsEnabled)
                return;

            MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAprobacionOrdenServicio, CMPMensajes.GetAccesoRestringidoNull("Aprobación de Orden de Servicio"), new Action<ESGC_PermisoPerfil>((P) =>
            {
                try
                {
                    if (dtgOrdenServicio.SelectedItems.Count == 0)
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, CMPMensajes.IsNullSelectItem, CmpButton.Aceptar);
                        return;
                    }

                    var vrObjECMP_OrdenServicio = new ECMP_OrdenServicio(dtgOrdenServicio.SelectedItem, TipoConstructor.Update);
                    if (vrObjECMP_OrdenServicio.ObjESGC_Estado.CodEstado == "PECOS")
                    {
                        new CmpNavigationService().Ir(new PCMP_AprobacionOrdenServicio(), _MyFrame, vrObjECMP_OrdenServicio);
                    }
                    else
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, "Solo se puede dar Aprobado a las Orden de Servicio Pendientes", CmpButton.Aceptar);
                    }
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAprobacionOrdenServicio, ex.Message, CmpButton.Aceptar);
                }
            }), "PCMP_AprobacionOrdenServicio");
        }

        private void btnVisualizarIsClicked()
        {
            if (!btnVisualizar.IsEnabled)
                return;

            MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.GetAccesoRestringidoNull("Orden Compra"), new Action<ESGC_PermisoPerfil>((P) =>
            {
                try
                {
                    if (P.Consulta)
                    {
                        if (dtgOrdenServicio.SelectedItems.Count == 0)
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.IsNullSelectItem, CmpButton.Aceptar);
                            return;
                        }

                        ObjECMP_OrdenServicio = (ECMP_OrdenServicio)dtgOrdenServicio.SelectedItem;
                        ObjListECMP_OrdenServicioDetalle = new BCMP_OrdenServicioDetalle().ListAdministrarOrdenServicioDetalle(ObjECMP_OrdenServicio);
                        foreach (var item in ObjListECMP_OrdenServicioDetalle)
                        {
                            if (ObjECMP_OrdenServicio.Exonerado == 12)
                            {
                                decimal dmlCalculoIGV = (item.PrecioUnitario * ObjECMP_OrdenServicio.IGV);
                                item.PrecioUnitario = decimal.Round(dmlCalculoIGV + item.PrecioUnitario, 2);
                            }
                            
                            //factura
                            if (ObjECMP_OrdenServicio.Exonerado == 12)
                            {
                                //Calculo con incluir IGV [12]
                                decimal dmlImporte = item.PrecioUnitario * item.Cantidad;
                                decimal dmlImporteIGV = (dmlImporte / (((ObjECMP_OrdenServicio.IGV * 100) + 100) / 100)) * ObjECMP_OrdenServicio.IGV;

                                item.Importe = dmlImporte;
                                item.ImporteIGV = dmlImporteIGV;
                            }
                            else if (ObjECMP_OrdenServicio.Exonerado == 21)
                            {
                                //Calculo cuando es Honorario [21]
                                decimal dmlImporte = item.PrecioUnitario * item.Cantidad;
                                decimal dmlImporteIGV = 0;

                                item.Importe = dmlImporte;
                                item.ImporteIGV = dmlImporteIGV;
                            }
                            else
                            {
                                //Calculo sin incluir IGV [11]
                                decimal dmlImporte = item.PrecioUnitario * item.Cantidad;
                                decimal dmlImporteIGV = dmlImporte * ObjECMP_OrdenServicio.IGV;

                                item.Importe = dmlImporte;
                                item.ImporteIGV = dmlImporteIGV;
                            } 
                        }
                        ImprimirOrdenServicio(ObjECMP_OrdenServicio, ObjListECMP_OrdenServicioDetalle);
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

        public void btnExportarIsClicked()
        {
            if (dtgOrdenServicio.Items.Count == 0)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, "No hay Datos que Exportar", CmpButton.Aceptar);
                return;
            }
            //Processing.Visibility = System.Windows.Visibility.Visible;
            var vrObjListECMP_OrdenServicio = (dtgOrdenServicio.Items.OfType<ECMP_OrdenServicio>()).ToList();
            try
            {
                var ListarExcel = vrObjListECMP_OrdenServicio.Select(x
                       => new
                       {
                           IdOrdenServicio = x.IdOrdenServicio,
                           Sucursal = x.ObjESGC_EmpresaSucursal.Sucursal,
                           NumeroDocumento = x.DocumenSerie,
                           Moneda = x.ObjESGC_Moneda.Descripcion,
                           RazonSocial = x.ObjEMNF_ClienteProveedor.RazonSocial,
                           Fecha = x.Fecha.ToShortDateString(),
                           Estado = x.ObjESGC_Estado.Estado,
                           FechaInicio = x.FechaInicio.ToShortDateString(),
                           FechaFin = x.FechaFin.ToShortDateString(),
                           DiasRetraso = x.DiasRetraso,
                           Gravada = x.Gravada,
                           ImporteIGV_Retención = x.ImporteIGV,
                           Total = (x.Gravada + x.Exonerado + x.ImporteIGV),
                           Código = x.Exonerado,
                       }).ToList();
                ListarExcel.Export("ListaOrdenServicio", ExportType.Excel, (value) =>
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, value, CmpButton.Aceptar);
                    //Processing.Visibility = System.Windows.Visibility.Hidden;
                }); 
            }
            catch (Exception ex)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, ex.Message, CmpButton.Aceptar);
                //Processing.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        public void btnAnularIsClicked()
        {
            if (!btnAnular.IsEnabled)
                return;

            MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.GetAccesoRestringidoNull("Orden Servicio"), new Action<ESGC_PermisoPerfil>((P) =>
            {
                try
                {
                    if (P.Eliminar)
                    {
                        if (dtgOrdenServicio.SelectedItems.Count == 0)
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, CMPMensajes.IsNullSelectItem, CmpButton.Aceptar);
                            return;
                        }

                        var vrObjECMP_OrdenServicio = new ECMP_OrdenServicio(dtgOrdenServicio.SelectedItem, TipoConstructor.Update);

                        CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.PreguntaContinuarProceso, CmpButton.AceptarCancelar, () =>
                        {
                            string strOutMessageError = string.Empty;
                            CmpMessageBox.Proccess(CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.ProcesandoDatos, () =>
                            {
                                try
                                {
                                    vrObjECMP_OrdenServicio.Opcion = "N";
                                    new BCMP_OrdenServicio().TransOrdenServicio(vrObjECMP_OrdenServicio);
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
                        CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, CMPMensajes.GetAccesoRestringidoEliminar("Orden Servicio"), CmpButton.Aceptar);
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

        #region MÉTODOS

        /// <summary>
        /// Carga datos Orden Compra
        /// </summary>
        /// <param name="Filtro">Datos para filtrar</param>
        public void LoadDetail(string Filtro = "%")
        {
            var vrListECMP_OrdenServicio = new List<ECMP_OrdenServicio>();

            string strOutMessageError = string.Empty;
            CmpTask.Process(
            () =>
            {
                try
                {
                    vrListECMP_OrdenServicio = new BCMP_OrdenServicio().ListFiltrarOrdenServicio(ObjECMP_OrdenServicio, Filtro);
                    foreach (var item in vrListECMP_OrdenServicio)
                    {
                        if (item.ObjESGC_Estado.CodEstado == "APCOS")
                        {
                            var fechahoy = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                            var FechaFin = Convert.ToDateTime(item.FechaFin.ToShortDateString());
                            TimeSpan ts = fechahoy - FechaFin;
                            item.DiasRetraso = Convert.ToInt32(ts.TotalDays);
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
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, strOutMessageError, CmpButton.Aceptar);
                }
                else
                {
                    dtgOrdenServicio.ItemsSource = vrListECMP_OrdenServicio;
                    lblCountItems.Text = vrListECMP_OrdenServicio.Count + " Registros";
                }
            });

        }

        private void ImprimirOrdenServicio(ECMP_OrdenServicio ObjECMP_OrdenServicio, List<ECMP_OrdenServicioDetalle> ListECMP_OrdenServicioDetalle)
        {
            try
            {
                ListECMP_OrdenServicioDetalle.ForEach((f) =>
                {
                    if (f.ObjEMNF_Servicio.IdServicio != 0)
                    {
                        decimal dmlPrecioUnitario = 0;

                        if ((f.ObjEMNF_Servicio.ObjEMNF_OperacionIGV.CodOperacionIGV == "GB"))
                        {
                            if (ObjECMP_OrdenServicio.Exonerado == 12)
                            {
                                dmlPrecioUnitario = (f.PrecioUnitario) / ((decimal.Round(ObjECMP_OrdenServicio.IGV * 100, 2) + 100) / 100);
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

                string[] parametro;

                string direccion = new BALM_Almacen().ListFiltrarAlmacen(ObjECMP_OrdenServicio.ObjESGC_EmpresaSucursal).FirstOrDefault().Direccion;
                var textMostrar = ObjECMP_OrdenServicio.Exonerado;
                var text = ListECMP_OrdenServicioDetalle.FirstOrDefault().ObjEMNF_Servicio.ObjEMNF_OperacionIGV.CodOperacionIGV;
                string strIgvText = ((ObjECMP_OrdenServicio.Retencion) ? "Retención " : "IGV ") + String.Format("{0:N2}", decimal.Round((ObjECMP_OrdenServicio.IGV * 100), 2)) + "%";
                string Monto = string.Empty;
                if(ObjECMP_OrdenServicio.Retencion || ObjECMP_OrdenServicio.Exonerado == 21)
                    Monto = decimal.Round(Convert.ToDecimal(ObjECMP_OrdenServicio.Gravada - ObjECMP_OrdenServicio.ImporteIGV), 2).ToString();
                else
                    Monto = decimal.Round(Convert.ToDecimal(ObjECMP_OrdenServicio.Gravada +  ObjECMP_OrdenServicio.ImporteIGV), 2).ToString();

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
                    "prmTotal|"             + ObjECMP_OrdenServicio.ObjESGC_Moneda.Simbolo + " "+string.Format( "{0:N2}",decimal.Round( ObjECMP_OrdenServicio.Gravada,2)),
                    "prmIgv|"               + ObjECMP_OrdenServicio.ObjESGC_Moneda.Simbolo + " "+string.Format( "{0:N2}",decimal.Round( ObjECMP_OrdenServicio.ImporteIGV,2)),
                    "prmGravadaText|"       + ((textMostrar == 21 ) ? "Total Honorario": (text== "GB") ? ((ObjECMP_OrdenServicio.Retencion) ? "Renta Bruta" : "Gravada") : "Exonerado"),
                    "prmIgvText|"           + ((textMostrar == 21) ? ("Retención " + decimal.Round(ObjECMP_OrdenServicio.IGV * 100, 2) + "%"): strIgvText),
                    "prmNetoText|"          + ((textMostrar == 21) ? "Total Neto " : ((textMostrar == 11 && ObjECMP_OrdenServicio.Retencion) ? "Renta Neta" : "Importe Total ")),
                    "prmNeto|"              + ObjECMP_OrdenServicio.ObjESGC_Moneda.Simbolo + " "+string.Format( "{0:N2}",Convert.ToDecimal( Monto)),
                    "prmSucursal|"          + ObjECMP_OrdenServicio.ObjESGC_EmpresaSucursal.Sucursal ,
                    "prmDireccionSucursal|" + direccion,
                    "prmFechaFin|"          + ObjECMP_OrdenServicio.FechaFin.ToShortDateString(),
                    "prmMoneda|"            + ObjECMP_OrdenServicio.ObjESGC_Moneda.Descripcion,
                    "prmCreadopor|"         + ObjECMP_OrdenServicio.Creacion ,
                    "prmAprobadopor|"       + ObjECMP_OrdenServicio.Aprobacion,

                };
                MainRerport ObjMainRerport = new MainRerport();
                ObjMainRerport.InitializeMainRerport("CMP.Reports.Files.RptOrdenServicio.rdlc", "dtsOrdenServicioDetalle", ListECMP_OrdenServicioDetalle.ToList(), parametro);
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
