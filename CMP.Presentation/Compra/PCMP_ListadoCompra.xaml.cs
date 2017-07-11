namespace CMP.Presentation.Compra
{
    using CMP.Business;
    using CMP.Entity;
    using CMP.Useful.Modulo;
    using ComputerSystems;
    using ComputerSystems.WPF;
    using MahApps.Metro.Controls;
    using SGC.Empresarial.Entity;
    using SGC.Empresarial.Method;
    using SGC.Empresarial.Useful.Modulo;
    using System;
    using System.Linq;
    using ComputerSystems.WPF.Acciones.Controles.Buttons;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using MNF.Presentation;
    using ComputerSystems.MethodList;
    using ComputerSystems.WPF.Notificaciones;
    using ComputerSystems.WPF.Interfaces;

    public partial class PCMP_ListadoCompra : UserControl, CmpINavigation
    {
        #region INSTANCIAS, VARIABLES, EVENTOS AGREGADOS

        private string strOpcion = string.Empty;

        #endregion

        #region EVENTOS DE LA CLASE

        public PCMP_ListadoCompra()
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
                this.KeyDownCmpButtonTitleTecla(ActionShiftE: btnExportarIsClicked,
                                                ActionF6: btnNuevoIsClicked,
                                                ActionF1: btnAnularIsClick,
                                                ActionF2: btnEditarIsClicked,
                                                ActionESC: btnSalirIsClicked);

                cbxOpcion.SelectionChanged += cbxOpcion_SelectionChanged;
                txtFiltrar.KeyUp += txtFiltrar_KeyUp_1;
                dtpFechaDesde.SelectedDateChanged += _SelectedDateChanged;
                dtpFechaHasta.SelectedDateChanged += _SelectedDateChanged;
                cbxOpcion.SelectedIndex = 0;

                if (dtpFechaDesde.SelectedDate == null)
                    dtpFechaDesde.SelectedDate = DateTime.Now;
                if (dtpFechaHasta.SelectedDate == null)
                    dtpFechaHasta.SelectedDate = DateTime.Now;

                LoadDetail();

                MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminCompra, CMPMensajes.GetAccesoRestringidoNull("Compra"), new Action<ESGC_PermisoPerfil>((P) =>
                {
                    if (P == null) { return; }
                    btnNuevo.IsEnabled = P.Nuevo;
                    btnEditar.IsEnabled = P.Editar;
                }));
            }
        }

        private void _SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbxOpcion.SelectedIndex == 0)
                LoadDetail();
        }

        private void cbxOpcion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dtpFechaDesde.IsEnabled = false;
            dtpFechaHasta.IsEnabled = false;

            int intIndex = cbxOpcion.SelectedIndex;
            if (intIndex == 0)
            {
                if (dtpFechaDesde.SelectedDate == null)
                    dtpFechaDesde.SelectedDate = DateTime.Now;
                if (dtpFechaHasta.SelectedDate == null)
                    dtpFechaHasta.SelectedDate = DateTime.Now;
                GridFiltrarFecha.Visibility = Visibility.Visible;
                GridFiltrarDescripcion.Visibility = System.Windows.Visibility.Collapsed;

                strOpcion = "F";
                dtpFechaDesde.IsEnabled = true;
                dtpFechaHasta.IsEnabled = true;

                string strWatermarkProperty = string.Empty;
                string strToolTip = string.Empty;
                txtFiltrar.SetValue(TextBoxHelper.WatermarkProperty, strWatermarkProperty);
                txtFiltrar.ToolTip = strToolTip;
                txtFiltrar.Focus();
                lblFiltrar.Text = strWatermarkProperty;
            }
            else if (intIndex == 1)
            {
                GridFiltrarFecha.Visibility = Visibility.Collapsed;
                GridFiltrarDescripcion.Visibility = System.Windows.Visibility.Visible;

                strOpcion = "P";

                string strWatermarkProperty = "Filtrar por Razón Social";
                string strToolTip = "Filtrar por Razón Social";
                txtFiltrar.SetValue(TextBoxHelper.WatermarkProperty, strWatermarkProperty);
                txtFiltrar.ToolTip = strToolTip;
                txtFiltrar.Focus();
                lblFiltrar.Text = strWatermarkProperty;
            }
            else if (intIndex == 2)
            {
                GridFiltrarFecha.Visibility = Visibility.Collapsed;
                GridFiltrarDescripcion.Visibility = System.Windows.Visibility.Visible;

                strOpcion = "M";
                txtFiltrar.Visibility = System.Windows.Visibility.Visible;

                string strWatermarkProperty = "Filtrar por Moneda";
                string strToolTip = "Filtrar por Moneda";
                txtFiltrar.SetValue(TextBoxHelper.WatermarkProperty, strWatermarkProperty);
                txtFiltrar.ToolTip = strToolTip;
                txtFiltrar.Focus();
                lblFiltrar.Text = strWatermarkProperty;
            }
            else if (intIndex == 3)
            {
                GridFiltrarFecha.Visibility = Visibility.Collapsed;
                GridFiltrarDescripcion.Visibility = System.Windows.Visibility.Visible;

                strOpcion = "D";

                string strWatermarkProperty = "Filtrar por Número documento";
                string strToolTip = "Filtrar por Número documento";
                txtFiltrar.SetValue(TextBoxHelper.WatermarkProperty, strWatermarkProperty);
                txtFiltrar.ToolTip = strToolTip;
                txtFiltrar.Focus();
                lblFiltrar.Text = strWatermarkProperty;
            }
            LoadDetail("%");
        }

        private void txtFiltrar_KeyUp_1(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string strFiltro = (txtFiltrar.Text.Trim().Length == 0) ? "%" : txtFiltrar.Text;
            LoadDetail(strFiltro);
        }

        private void btnNuevoIsClicked()
        {
            if (!btnNuevo.IsEnabled)
                return;

            MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminCompra, CMPMensajes.GetAccesoRestringidoNull("Compra"), new Action<ESGC_PermisoPerfil>((P) =>
            {
                try
                {
                    if (P.Nuevo)
                    {
                        new CmpNavigationService().Ir(new PCMP_Compra(), _MyFrame, new ECMP_Compra(null, TipoConstructor.Insert));
                    }
                    else
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, CMPMensajes.GetAccesoRestringidoNuevo("Compra"), CmpButton.Aceptar);
                    }
                }
                catch(Exception ex)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, ex.Message, CmpButton.Aceptar);
                }
            }));
        }

        private void btnAnularIsClick()
        {
            if (!btnAnular.IsEnabled)
                return;

            MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminCompra, CMPMensajes.GetAccesoRestringidoNull("Compra"), new Action<ESGC_PermisoPerfil>((P) =>
            {
                try
                {
                    if (P.Eliminar)
                    {
                        if (dtgCompra.SelectedItems.Count == 0)
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, CMPMensajes.IsNullSelectItem, CmpButton.Aceptar);
                            return;
                        }
                        var ObjECMP_Compra = (ECMP_Compra)dtgCompra.SelectedItem;
                        if (ObjECMP_Compra != null )
                        {
                            if (ObjECMP_Compra.ObjESGC_Estado.CodEstado == "PECMP")
                            {
                                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, CMPMensajes.PreguntaContinuarProceso, CmpButton.AceptarCancelar, () =>
                                {
                                    string strOutMessageError = string.Empty;
                                    CmpMessageBox.Proccess(CMPMensajes.TitleAdminCompra, CMPMensajes.PreguntaContinuarProceso, () =>
                                    {
                                        try
                                        {
                                            ObjECMP_Compra.Opcion = "N";
                                            new BCMP_Compra().TransCompra(ObjECMP_Compra);
                                        }
                                        catch (Exception ex)
                                        {
                                            strOutMessageError = ex.Message;
                                        }
                                    }, () =>
                                    {
                                        if (strOutMessageError.Length > 0)
                                        {
                                            CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, strOutMessageError, CmpButton.Aceptar);
                                        }
                                        else
                                        {
                                            CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, CMPMensajes.DatoProcesados, CmpButton.Aceptar);
                                            LoadDetail();
                                        }
                                    });
                                });
                            }
                            else 
                            {
                                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra,"Solo se pueden anular Compras Pendientes", CmpButton.Aceptar);
                            }
                        }
                    }
                    else
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, CMPMensajes.GetAccesoRestringidoEliminar("Compra"), CmpButton.Aceptar);
                    }
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, ex.Message, CmpButton.Aceptar);
                }
            }));
        }
        
        private void btnEditarIsClicked()
        {
            if (!btnEditar.IsEnabled)
                return;

            MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminCompra, CMPMensajes.GetAccesoRestringidoNull("Compra"), new Action<ESGC_PermisoPerfil>((P) =>
            {
                try
                {
                    if (P.Editar)
                    {
                        if (dtgCompra.SelectedItems.Count == 0)
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, CMPMensajes.IsNullSelectItem, CmpButton.Aceptar);
                            return;
                        }

                        new CmpNavigationService().Ir(new PCMP_Compra(), _MyFrame, new ECMP_Compra(dtgCompra.SelectedItem, TipoConstructor.Update));
                    }
                    else
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, CMPMensajes.GetAccesoRestringidoEditar("Compra"), CmpButton.Aceptar);
                    }
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, ex.Message, CmpButton.Aceptar);
                }
            }));
        }

        private void btnExportarIsClicked()
        {
            if (dtgCompra.Items.Count == 0)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "No hay Datos que Exportar", CmpButton.Aceptar);
                return;
            }
            else
            {
                var vrObjListECMP_Compra = (dtgCompra.Items.OfType<ECMP_Compra>()).ToList();
                try
                {
                    var ListarExcel = vrObjListECMP_Compra.Select(x => new
                        {
                            IdCompra = x.IdCompra,
                            Periodo = int.Parse(x.Periodo),
                            FechaEmision = x.Fecha.ToShortDateString(),
                            FechaContable = x.FechaContable.ToShortDateString(),
                            MotivoMovimiento = x.ObjEMNF_MotivoMovimiento.MotMovimiento,
                            RazonSocial = x.ObjEMNF_ClienteProveedor.RazonSocial,
                            NumeroDocIdentidad = x.ObjEMNF_ClienteProveedor.NroDocIdentidad,
                            Direccion = x.ObjEMNF_ClienteProveedor.Direccion,
                            Moneda = x.ObjESGC_Moneda.Descripcion,
                            TipoCambio = x.TipoCambio,
                            CodDocumento = x.CodDocumento,
                            Serie = x.Serie,
                            NroDocumento = x.Numero,
                            FormaPago = x.ObjESGC_FormaPago.FormaPago,
                            Gravada = ((x.CodDocumento) == "HNR" | (x.Gravada == 0)) ? 0 : x.Gravada,
                            Exonerada = ((x.Exonerada) == 0) ? 0 : x.Exonerada,
                            PctjeIGV = ((x.CodDocumento) == "HNR") ? 0 : x.IGV * 100,
                            ImporteIGV = ((x.CodDocumento) == "HNR") ? 0 : x.ImporteIGV,
                            TotalHonorarios = ((x.CodDocumento) == "HNR") ? x.Gravada : 0,
                            PcteRetencion = ((x.CodDocumento) == "HNR") ? x.IGV * 100 : 0,
                            ImporteRetencion = ((x.CodDocumento) == "HNR") ? x.ImporteIGV : 0,
                            Total = x.Gravada + x.Exonerada + x.ImporteIGV,
                            PctjePercepcion = (x.AfectoPercepcion) ? x.Percepcion * 100 : 0,
                            ImportePercepcion = (x.AfectoPercepcion) ? ((x.Gravada + x.Exonerada + x.ImporteIGV) * x.Percepcion) : 0,
                            ImporteTotal = ((x.Gravada + x.Exonerada + x.ImporteIGV) + ((x.Gravada + x.Exonerada + x.ImporteIGV) * x.Percepcion)),
                            PctajeDetraccion = (x.AfectoDetraccion) ? x.Detraccion : 0,
                            ImporteDetraccion = (x.AfectoDetraccion) ? ((x.Gravada + x.Exonerada + x.ImporteIGV) * x.Detraccion / 100) : 0,
                            Estado = x.ObjESGC_Estado.Estado,
                            Observacion = x.Observacion
                        }).ToList();
                    ListarExcel.Export("ListaCompra", ExportType.Excel, (value) =>
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, value, CmpButton.Aceptar);
                    });
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenCompra, ex.Message, CmpButton.Aceptar);
                }
            }
        }

        private void btnSalirIsClicked()
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
            var vrListECMP_Compra = new List<ECMP_Compra>();
            DateTime FechaDesde = (dtpFechaDesde.SelectedDate != null) ? dtpFechaDesde.SelectedDate.Value : DateTime.Now;
            DateTime FechaHasta = (dtpFechaHasta.SelectedDate != null) ? dtpFechaHasta.SelectedDate.Value : DateTime.Now;

            string strOutMessageError = string.Empty;
            CmpTask.Process(
            () =>
            {
                try
                {
                    vrListECMP_Compra = new BCMP_Compra().ListCompra(((strOpcion == string.Empty) ? "F" : strOpcion), FechaDesde, FechaHasta, "%", Filtro);
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
                    dtgCompra.ItemsSource = vrListECMP_Compra;
                    lblCountItems.Text = vrListECMP_Compra.Count + " Registros";
                }
            });

        }

        #endregion
    }
}
