namespace CMP.Presentation.OrdenServicio
{
    using ALM.Business;
    using CMP.Business;
    using CMP.Entity;
    using CMP.Reports;
    using CMP.Useful.Metodo;
    using CMP.Useful.Modulo;
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.Buttons;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using MNF.Business;
    using SGC.Empresarial.Entity;
    using SGC.Empresarial.Method;
    using SGC.Empresarial.Useful;
    using SGC.Empresarial.Useful.Modulo;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Controls;

    public partial class PCMP_AprobacionOrdenServicio : UserControl, CmpINavigation
    {
        #region INSTANCIAS, VARIABLES, EVENTOS AGREGADOS

        public bool imprimir { get; set; }
        private string strNetoText = string.Empty;
        private NumLetra NumLetras = new NumLetra(); 
        #endregion

        #region EVENTOS DE LA CLASE

        public PCMP_AprobacionOrdenServicio()
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

                this.KeyDownCmpButtonTitleTecla(ActionF12: btnGuardarIsClicked,
                                                ActionF9: btnImprimirIsClicked,
                                                ActionESC: btnSalirIsClicked);

				MyHeader.DataContext = ObjECMP_OrdenServicio;

				lblTitleOrdenServicio03.Text = "IGV " + decimal.Round(ObjECMP_OrdenServicio.IGV * 100, 2) + "%";
				lblTitleOrdenServicio04.Text = "Total " + ObjECMP_OrdenServicio.ObjESGC_Moneda.Simbolo;

				if (ObjECMP_OrdenServicio.Exonerado == 11)
				{
					dgDetalleServicio.Columns[5].Header = (ObjECMP_OrdenServicio.Retencion) ? "Retención" : "IGV";
                    rbExonerado.IsChecked = false;
                    chkAplicarRetencion.IsChecked = ObjECMP_OrdenServicio.Retencion;
					rbIncluidoIGV.IsChecked = false;
				}
				else if (ObjECMP_OrdenServicio.Exonerado == 12)
				{
					dgDetalleServicio.Columns[5].Header = "IGV";
					rbExonerado.IsChecked = false;
					rbIncluidoIGV.IsChecked = true;
				}
				else if (ObjECMP_OrdenServicio.Exonerado == 21)
				{
					dgDetalleServicio.Columns[5].Header = "Retención";
					rbExonerado.IsChecked = true;
					chkAplicarRetencion.IsChecked = ObjECMP_OrdenServicio.Retencion;
					rbIncluidoIGV.IsChecked = false;
				}
				else if (ObjECMP_OrdenServicio.Exonerado == 22)
				{
					dgDetalleServicio.Columns[5].Header = "Retención";
					rbIncluidoIGV.IsChecked = false;
					rbExonerado.IsChecked = false;
				}

                cbxTipoDestino.ItemsSource = new BMNF_TipoDestino().CollectionTipoDestino();

				LoadDetail();

                MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminCompra, CMPMensajes.GetAccesoRestringidoNull("Orden Servicio"), new Action<ESGC_PermisoPerfil>((P) =>
                {
                    if (P == null) { return; }
                    btnGuardar.IsEnabled = (P.Nuevo || P.Editar);
                }));
            }
        }

        private void cbxTipoDestino_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            LoadTipoDestino();
        }

        public void btnGuardarIsClicked()
        {
            if (!btnGuardar.IsEnabled)
                return;

            MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAprobacionOrdenServicio, CMPMensajes.GetAccesoRestringidoNull("Orden Servicio"), new Action<ESGC_PermisoPerfil>((P) =>
            {
                if (P.Nuevo)
                {
                    var varObjECMP_OrdenServicio = (ECMP_OrdenServicio)MyHeader.DataContext;

                    string strOutMessageError = string.Empty;
                    CmpMessageBox.Proccess(CMPMensajes.TitleAprobacionOrdenServicio, CMPMensajes.ProcesandoDatos, () =>
                    {
                        try
                        {
                            varObjECMP_OrdenServicio.Opcion = "A";
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
                            CmpMessageBox.Show(CMPMensajes.TitleAprobacionOrdenServicio, strOutMessageError, CmpButton.Aceptar);
                        }
                        else
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleAdminOrdenServicio, CMPMensajes.DatoProcesados + "\n¿Desea Imprimir el documento de Orden de Servicio?", CmpButton.AceptarCancelar, () =>
                            {
                                ImprimirOrdenServicio("APROBADO");

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
                    CmpMessageBox.Show(CMPMensajes.TitleAprobacionOrdenServicio, CMPMensajes.GetAccesoRestringidoNuevo("Orden Servicio"), CmpButton.Aceptar);
                }
            }));
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

        private void LoadTipoDestino()
        {
            try
            {
                var varObjECMP_OrdenServicio = (ECMP_OrdenServicio)MyHeader.DataContext;
                var vrSelectTipoDestino = (ECMP_ValueComboBox)cbxTipoDestino.SelectedItem;
                if (vrSelectTipoDestino == null) return;

                if (vrSelectTipoDestino.Value == "CTDIS") //Cuenta por Distribuir General
                {
                    dtgColumnaTipoDestino.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    dtgColumnaTipoDestino.Header = vrSelectTipoDestino.Item;
                    dtgColumnaTipoDestino.Visibility = System.Windows.Visibility.Visible;

                    var vrListECMP_ValueComboBox = new List<ECMP_ValueComboBox>();

                    string strOutMessageError = string.Empty;
                    CmpTask.Process(
                    () =>
                    {
                        try
                        {
                            if (vrSelectTipoDestino.Value == "CCOST") //Categoría Centro Costo (Listado Centro de Costo)
                            {
                                var vrListCentroCosto = new BMNF_CentroCosto().ListFiltrarCentroCosto(varObjECMP_OrdenServicio.ObjESGC_EmpresaSucursal);
                                foreach (var item in vrListCentroCosto)
                                {
                                    vrListECMP_ValueComboBox.Add(new ECMP_ValueComboBox() { Item = item.Descripcion, Value = item.IdCentroCosto.ToString() });
                                }
                            }
                            else if (vrSelectTipoDestino.Value == "SCOST") //Centro Costo (Listado Sub Centro de Costo)
                            {
                                var vrListCentroCosto = new BMNF_SubCentroCosto().ListGetSubCentroCosto(varObjECMP_OrdenServicio.ObjESGC_EmpresaSucursal);
                                foreach (var item in vrListCentroCosto)
                                {
                                    vrListECMP_ValueComboBox.Add(new ECMP_ValueComboBox() { Item = item.Descripcion, Value = item.IdSubCenCosto.ToString() });
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

                            var vrObjListECMP_OrdenServicioDetalle = (dgDetalleServicio.Items.OfType<ECMP_OrdenServicioDetalle>()).ToList();
                            foreach (var item in vrObjListECMP_OrdenServicioDetalle)
                            {
                                item.ListCentroCosto = vrListECMP_ValueComboBox;
                            }

                            dgDetalleServicio.Items.Refresh();
                        }
                    });
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Carga datos del orden de compra
        /// </summary>
        private void LoadDetail()
        {
            dgDetalleServicio.Items.Clear();
            var vrListECMP_OrdenServicioDetalle = new List<ECMP_OrdenServicioDetalle>();
            var vrListPeriodoCapania = new List<ECMP_ValueComboBox>();
            var varObjECMP_OrdenServicio = (ECMP_OrdenServicio)MyHeader.DataContext;

            string strOutMessageError = string.Empty;
            CmpTask.Process(
            () =>
            {
                try
                {
                    vrListECMP_OrdenServicioDetalle = new BCMP_OrdenServicioDetalle().ListAdministrarOrdenServicioDetalle(varObjECMP_OrdenServicio);

                    int Anio = Convert.ToInt32(varObjECMP_OrdenServicio.Periodo.Substring(0, varObjECMP_OrdenServicio.Periodo.Length - 2));
                    for (int i = Anio - 1; i <= Anio + 1; i++)
                    {
                        vrListPeriodoCapania.Add(new ECMP_ValueComboBox() { Item = i.ToString(), Value = i.ToString() });
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
                    CmpMessageBox.Show(CMPMensajes.TitleAprobacionOrdenServicio, strOutMessageError, CmpButton.Aceptar);
                }
                else
                {
                    var ObjECMP_OrdenServicio = (ECMP_OrdenServicio)MyHeader.DataContext;
                    foreach (var item in vrListECMP_OrdenServicioDetalle)
                    {
                        item.ListPeriodoCampania = vrListPeriodoCapania;
                        if (varObjECMP_OrdenServicio.Exonerado == 12) //INCLUYE IGV
                        {
                            decimal dmlCalculoIGV = (item.PrecioUnitario * varObjECMP_OrdenServicio.IGV);
                            item.PrecioUnitario = decimal.Round(dmlCalculoIGV + item.PrecioUnitario, 8);
                        } 
                        LoadTipoDestino();
                        dgDetalleServicio.Items.Add(item);
                        CalcularTotalesItems(item);
                    }

                    txtGravada.Text = ObjECMP_OrdenServicio.Gravada.ToString("###,###,##0.#0");
                    txtImporteIGV.Text = ObjECMP_OrdenServicio.ImporteIGV.ToString("###,###,##0.#0");                    

                    //factura
                    if (rbIncluidoIGV.IsChecked.Value)
                    {
                        //Calculo con incluir IGV
                        txtTotal.Text = decimal.Round((ObjECMP_OrdenServicio.Gravada + ObjECMP_OrdenServicio.ImporteIGV), 2).ToString("###,###,##0.#0");
                    }
                    else if (rbExonerado.IsChecked.Value)
                    {
                        //Calculo cuando es Honorario
                        txtTotal.Text = decimal.Round((ObjECMP_OrdenServicio.Gravada - ObjECMP_OrdenServicio.ImporteIGV), 2).ToString("###,###,##0.#0");
                    }
                    else if(chkAplicarRetencion.IsChecked.Value)
                    {
                        //Calculo cuando es Renta de segunda categoria
                        txtTotal.Text = decimal.Round((ObjECMP_OrdenServicio.Gravada - ObjECMP_OrdenServicio.ImporteIGV), 2).ToString("###,###,##0.#0");
                    }
                    else
                    {
                        //Calculo sin incluir IGV
                        txtTotal.Text = decimal.Round((ObjECMP_OrdenServicio.Gravada + ObjECMP_OrdenServicio.ImporteIGV), 2).ToString("###,###,##0.#0");
                    }
                }
            });
        }

        /// <summary>
        /// Calculta los totales
        /// </summary>
        private void CalcularTotales()
        {
            try
            {
                var varObjECMP_OrdenServicio = (ECMP_OrdenServicio)MyHeader.DataContext;
                var ListECMP_OrdenServicioDetalle = (dgDetalleServicio.Items.OfType<ECMP_OrdenServicioDetalle>()).ToList();
                if (ListECMP_OrdenServicioDetalle != null && ListECMP_OrdenServicioDetalle.Count > 0)
                {
                    //Recibo por Honorario
                    if (rbExonerado.IsChecked.Value)
                    {
                        lblTitleOrdenServicio02.Text = "Total Honorario";
                        lblTitleOrdenServicio03.Text = "Retención " + decimal.Round(varObjECMP_OrdenServicio.IGV * 100, 2) + "%";
                        lblTitleOrdenServicio04.Text = "Total Neto " + varObjECMP_OrdenServicio.ObjESGC_Moneda.Simbolo;
                        strNetoText = "Total Neto";
                    }
                    else if (chkAplicarRetencion.IsChecked.Value)
                    {

                        lblTitleOrdenServicio02.Text = "Renta Bruta";
                        lblTitleOrdenServicio03.Text = "Retención " + decimal.Round(varObjECMP_OrdenServicio.IGV * 100, 2) + "%";
                        lblTitleOrdenServicio04.Text = "Renta Neta " + varObjECMP_OrdenServicio.ObjESGC_Moneda.Simbolo;
                    }
                    else
                    {
                        lblTitleOrdenServicio02.Text = "Gravada";
                        lblTitleOrdenServicio03.Text = "IGV " + decimal.Round(varObjECMP_OrdenServicio.IGV * 100, 2) + "%";
                        lblTitleOrdenServicio04.Text = "Importe Total " + varObjECMP_OrdenServicio.ObjESGC_Moneda.Simbolo;
                        strNetoText = "Importe Total";
                    }

                    var First = ListECMP_OrdenServicioDetalle.FirstOrDefault();
                    if (First.ObjEMNF_Servicio.ObjEMNF_OperacionIGV.CodOperacionIGV == "EX" && !rbExonerado.IsChecked.Value)
                    {
                        lblTitleOrdenServicio02.Text = "Exonerado";
                        lblTitleOrdenServicio03.Text = "IGV " + decimal.Round(varObjECMP_OrdenServicio.IGV * 100, 2) + "%";
                        lblTitleOrdenServicio04.Text = "Importe Total " + varObjECMP_OrdenServicio.ObjESGC_Moneda.Simbolo;
                        strNetoText = "Importe Total";
                    }
                    txtLineas.Text = ListECMP_OrdenServicioDetalle.Count.ToString();                    
                }
                else
                {
                    txtLineas.Text = ("0");
                    txtGravada.Text = ("0.00");
                    txtImporteIGV.Text = ("0.00");
                    txtTotal.Text = ("0.00");
                } 
                dgDetalleServicio.Items.Refresh();
            }
            catch(Exception){}
        }

        /// <summary>
        /// Calcula los valores del registro seleccionado
        /// </summary>
        private void CalcularTotalesItems(ECMP_OrdenServicioDetalle ObjECMP_OrdenServicioDetalle)
        {
            try
            {
                var varObjECMP_OrdenServicio = (ECMP_OrdenServicio)MyHeader.DataContext;
                //factura
                if (rbIncluidoIGV.IsChecked.Value)
                {
                    //Calculo con incluir IGV
                    decimal dmlPrecioUnitario = decimal.Round((ObjECMP_OrdenServicioDetalle.PrecioUnitarioTemp / ((decimal.Round(varObjECMP_OrdenServicio.IGV * 100, 2) + 100) / 100)), 8);
                    decimal dmlImporte = decimal.Round((dmlPrecioUnitario * ObjECMP_OrdenServicioDetalle.Cantidad), 8);
                    decimal dmlImporteIGV = decimal.Round(dmlImporte * varObjECMP_OrdenServicio.IGV, 8);

                    ObjECMP_OrdenServicioDetalle.PrecioUnitario = dmlPrecioUnitario;
                    ObjECMP_OrdenServicioDetalle.Importe = dmlImporte;
                    ObjECMP_OrdenServicioDetalle.ImporteIGV = dmlImporteIGV;
                }
                else if (rbExonerado.IsChecked.Value)
                {
                    //Calculo cuando es Honorario
                    decimal dmlTasa = 0;
                    decimal dmlImporte = ObjECMP_OrdenServicioDetalle.PrecioUnitarioTemp * ObjECMP_OrdenServicioDetalle.Cantidad;
                    if (chkAplicarRetencion.IsChecked.Value)
                        dmlTasa = (SGCMethod.GetTasaHonorario(dmlImporte) / 100);
                    decimal dmlImporteIGV = (dmlImporte * dmlTasa);

                    ObjECMP_OrdenServicioDetalle.PrecioUnitario = decimal.Round(ObjECMP_OrdenServicioDetalle.PrecioUnitarioTemp, 8);
                    ObjECMP_OrdenServicioDetalle.Importe = decimal.Round(dmlImporte, 8);
                    ObjECMP_OrdenServicioDetalle.ImporteIGV = decimal.Round(dmlImporteIGV, 8);
                }
                else
                {
                    //Calculo sin incluir IGV
                    decimal dmlImporte = decimal.Round((ObjECMP_OrdenServicioDetalle.PrecioUnitarioTemp * ObjECMP_OrdenServicioDetalle.Cantidad), 8);
                    decimal dmlImporteIGV = decimal.Round(dmlImporte * varObjECMP_OrdenServicio.IGV, 8);

                    ObjECMP_OrdenServicioDetalle.PrecioUnitario = ObjECMP_OrdenServicioDetalle.PrecioUnitarioTemp;
                    ObjECMP_OrdenServicioDetalle.Importe = dmlImporte;
                    ObjECMP_OrdenServicioDetalle.ImporteIGV = dmlImporteIGV;
                }

                CalcularTotales();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Imprime un reporte orden de Servicio
        /// </summary>
        private void ImprimirOrdenServicio(string Estado = "%")
        {
            try
            {
                var varObjECMP_OrdenServicio = (ECMP_OrdenServicio)MyHeader.DataContext;
                var vrObjListECMP_OrdenServicioDetalle = new BCMP_OrdenServicioDetalle().ListAdministrarOrdenServicioDetalle(varObjECMP_OrdenServicio);
                vrObjListECMP_OrdenServicioDetalle.ForEach((x) =>
                {
                    x.Importe = x.Cantidad * x.PrecioUnitario;
                });
                string[] parametro;
                string direccion = new BALM_Almacen().ListFiltrarAlmacen(varObjECMP_OrdenServicio.ObjESGC_EmpresaSucursal).FirstOrDefault().Direccion;
                string Monto = Convert.ToDecimal(txtTotal.Text).ToString();
                parametro = new string[]
                {
                    "prmRazonSocial|"          + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.RazonSocial,
                    "prmRuc|"               + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.Ruc,
                    "prmDireccionEmpresa|" + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.DireccionFiscal,
                    "prmTelefonoEmpresa|"  + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.Telefono,
                    "prmFechaDocumento|"    + varObjECMP_OrdenServicio.Fecha.ToShortDateString(),
                    "prmFechaLetra|"        + DateTime.Now.ToLongDateString().Split(',').ElementAt(1),
                    "prmNumOrden|"          + varObjECMP_OrdenServicio.Serie + " - " + varObjECMP_OrdenServicio.Numero,
                    "prmProveedor|"         + varObjECMP_OrdenServicio.ObjEMNF_ClienteProveedor.RazonSocial,
                    "prmRucProveedor|"      + varObjECMP_OrdenServicio.ObjEMNF_ClienteProveedor.NroDocIdentidad,
                    "prmDireccionProv|"     + varObjECMP_OrdenServicio.ObjEMNF_ClienteProveedor.Direccion,
                    "prmCondicionPago|"     + varObjECMP_OrdenServicio.ObjESGC_FormaPago.FormaPago,
                    "prmNetoLetra|"         + NumLetras.Convertir(Monto,true,varObjECMP_OrdenServicio.ObjESGC_Moneda.Descripcion),
                    "prmTotal|"             + varObjECMP_OrdenServicio.ObjESGC_Moneda.Simbolo + " "+ txtGravada.Text,
                    "prmIgv|"               + varObjECMP_OrdenServicio.ObjESGC_Moneda.Simbolo + " "+ txtImporteIGV.Text,
                    "prmGravadaText|"       + lblTitleOrdenServicio02.Text,
                    "prmIgvText|"           + lblTitleOrdenServicio03.Text,
                    "prmNetoText|"          + strNetoText,
                    "prmNeto|"              + varObjECMP_OrdenServicio.ObjESGC_Moneda.Simbolo + " "+ txtTotal.Text,
                    "prmSucursal|"          + varObjECMP_OrdenServicio.ObjESGC_EmpresaSucursal.Sucursal + " "+ txtTotal.Text,
                    "prmDireccionSucursal|" + direccion,
                    "prmFechaFin|"          + varObjECMP_OrdenServicio.FechaFin.ToShortDateString(),
                    "prmMoneda|"            + varObjECMP_OrdenServicio.ObjESGC_Moneda.Descripcion,
                    "prmCreadopor|"         + varObjECMP_OrdenServicio.Creacion ,
                    "prmAprobadopor|"       + SGCVariables.ObjESGC_Usuario.Nombres + " " +  SGCVariables.ObjESGC_Usuario.Apellidos ,

                };

                //vrObjListECMP_OrdenServicioDetalle.AgregarEnReportViewer(new MainReports(), "CMP.Reports.Files.RptOrdenServicio.rdlc", "dtsOrdenServicioDetalle", parametro);

                MainRerport ObjMainRerport = new MainRerport();
                ObjMainRerport.InitializeMainRerport("CMP.Reports.Files.RptOrdenServicio.rdlc", "dtsOrdenServicioDetalle", vrObjListECMP_OrdenServicioDetalle, parametro);
                ObjMainRerport.ShowDialog();
            }
            catch (Exception ex)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAprobacionOrdenServicio, ex.Message, CmpButton.Aceptar);
            }
        }

        #endregion
    }
}
