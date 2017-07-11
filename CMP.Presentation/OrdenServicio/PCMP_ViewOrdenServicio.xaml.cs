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
    using MNF.Entity;
    using SGC.Empresarial.Method;
    using SGC.Empresarial.Useful;
    using SGC.Empresarial.Useful.Modulo;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Controls;

    public partial class PCMP_ViewOrdenServicio : CmpINavigation
    {
        #region INSTANCIAS, VARIABLES, EVENTOS AGREGADOS

        public bool imprimir { get; set; }
        private NumLetra NumLetras = new NumLetra();
        private string strNetoText = string.Empty;
        private ECMP_OrdenServicio ObjECMP_OrdenServicio;
        #endregion

        #region EVENTOS DE LA CLASE

        public PCMP_ViewOrdenServicio()
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

				this.ObjECMP_OrdenServicio = (ECMP_OrdenServicio)value;
				this.KeyDownCmpButtonTitleTecla(
														ActionF9:  btnImprimirIsClicked,
														ActionESC: btnSalirIsClicked);


				MyHeader.DataContext = ObjECMP_OrdenServicio;

				lblTitleOrdenServicio03.Text = "IGV " + decimal.Round(ObjECMP_OrdenServicio.IGV * 100, 2) + "%";
				lblTitleOrdenServicio04.Text = "Total " + ObjECMP_OrdenServicio.ObjESGC_Moneda.Simbolo;

				dgDetalleServicio.Columns[4].Header = "IGV";
				if (ObjECMP_OrdenServicio.Exonerado == 11)
				{
					rbExonerado.IsChecked = false;
					rbIncluidoIGV.IsChecked = false;
					rbExoneradoIGV.IsChecked = false;
				}
				else if (ObjECMP_OrdenServicio.Exonerado == 12)
				{
					rbExonerado.IsChecked = false;
					rbIncluidoIGV.IsChecked = true;
					rbExoneradoIGV.IsChecked = false;
				}
				else if (ObjECMP_OrdenServicio.Exonerado == 21)
				{
					rbExonerado.IsChecked = true;
					chkAplicarRetencion.IsChecked = ObjECMP_OrdenServicio.Retencion;
					dgDetalleServicio.Columns[4].Header = "Retención";
					rbIncluidoIGV.IsChecked = false;
					rbExoneradoIGV.IsChecked = false;
				}
				else if (ObjECMP_OrdenServicio.Exonerado == 22)
				{
					rbExoneradoIGV.IsChecked = true;
					rbIncluidoIGV.IsChecked = false;
					rbExonerado.IsChecked = false;
				}

				rbExoneradoIGV.IsEnabled = false;
				rbIncluidoIGV.IsEnabled = false;
				rbExonerado.IsEnabled = false;
                cbxTipoDestino.ItemsSource = new BMNF_TipoDestino().CollectionTipoDestino();
				LoadDetail();
            }
        }

        private void cbxTipoDestino_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            LoadTipoDestino();
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
                new CmpNavigationService().Volver();
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
                var vrSelectTipoDestino = (EMNF_TipoDestino)cbxTipoDestino.SelectedItem;
                if (vrSelectTipoDestino == null) return;

                if (vrSelectTipoDestino.CodTipoDestino == "CTDIS") //Cuenta por Distribuir General
                {
                    dtgColumnaTipoDestino.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    dtgColumnaTipoDestino.Header = vrSelectTipoDestino.TipoDestino;
                    dtgColumnaTipoDestino.Visibility = System.Windows.Visibility.Visible;

                    var vrListECMP_ValueComboBox = new List<ECMP_ValueComboBox>();

                    string strOutMessageError = string.Empty;
                    CmpTask.Process(
                    () =>
                    {
                        try
                        {
                            if (vrSelectTipoDestino.CodTipoDestino == "CCOST") //Categoría Centro Costo (Listado Centro de Costo)
                            {
                                var vrListCentroCosto = new BMNF_CentroCosto().ListFiltrarCentroCosto(varObjECMP_OrdenServicio.ObjESGC_EmpresaSucursal);
                                foreach (var item in vrListCentroCosto)
                                {
                                    vrListECMP_ValueComboBox.Add(new ECMP_ValueComboBox() { Item = item.Descripcion, Value = item.IdCentroCosto.ToString() });
                                }
                            }
                            else if (vrSelectTipoDestino.CodTipoDestino == "SCOST") //Centro Costo (Listado Sub Centro de Costo)
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
                    
                    foreach (var item in vrListECMP_OrdenServicioDetalle)
                    {
                        item.ListPeriodoCampania = vrListPeriodoCapania;
                        if (ObjECMP_OrdenServicio.Exonerado == 12)
                        {
                            decimal dmlCalculoIGV = (item.PrecioUnitario * ObjECMP_OrdenServicio.IGV);
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
                        txtTotal.Text = (ObjECMP_OrdenServicio.Gravada + ObjECMP_OrdenServicio.ImporteIGV).ToString("###,###,##0.#0");
                    }
                    else if (rbExonerado.IsChecked.Value)
                    {
                        //Calculo cuando es Honorario
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
                    decimal dmlTotal = 0;
                    decimal dmlGravada = 0;
                    decimal dmlImporteRetencionIGV = 0;

                    //factura
                    if (rbIncluidoIGV.IsChecked.Value)
                    {
                        //Calculo con incluir IGV
                        dmlGravada = ListECMP_OrdenServicioDetalle.Sum(x => (x.ObjEMNF_Servicio.ObjEMNF_OperacionIGV.CodOperacionIGV == "GB" ? x.Importe : 0));
                        dmlImporteRetencionIGV = ListECMP_OrdenServicioDetalle.Sum(x => (x.ObjEMNF_Servicio.ObjEMNF_OperacionIGV.CodOperacionIGV == "GB" ? x.ImporteIGV : 0));
                        dmlTotal = dmlGravada + dmlImporteRetencionIGV;

                        lblTitleOrdenServicio02.Text = "Gravada";
                        lblTitleOrdenServicio03.Text = "IGV " + decimal.Round(varObjECMP_OrdenServicio.IGV * 100, 2 ) + "%";
                        lblTitleOrdenServicio04.Text = "Importe Total " + ObjECMP_OrdenServicio.ObjESGC_Moneda.Simbolo;
                        strNetoText = "Importe Total";
                    }
                    else if (rbExonerado.IsChecked.Value)
                    {
                        //Calculo cuando es Honorario
                        dmlGravada = ListECMP_OrdenServicioDetalle.Sum(o => o.Importe);
                        decimal dmlTasa = (SGCMethod.GetTasaHonorario(dmlGravada) / 100);
                        dmlImporteRetencionIGV = dmlGravada * dmlTasa;
                        dmlTotal = dmlGravada - dmlImporteRetencionIGV;

                        lblTitleOrdenServicio02.Text = "Total Honorario";
                        lblTitleOrdenServicio03.Text = "Retención " + decimal.Round(dmlTasa * 100, 2) + "%";
                        lblTitleOrdenServicio04.Text = "Total Neto " + ObjECMP_OrdenServicio.ObjESGC_Moneda.Simbolo;
                        strNetoText = "Total Neto";
                    }
                    else
                    {
                        //Calculo sin incluir IGV
                        ListECMP_OrdenServicioDetalle.ForEach(x => { if (x.ObjEMNF_Servicio.ObjEMNF_OperacionIGV.CodOperacionIGV == "GB") dmlGravada += x.PrecioUnitario * x.Cantidad; });
                        dmlImporteRetencionIGV = dmlGravada * varObjECMP_OrdenServicio.IGV;
                        dmlTotal = dmlGravada + dmlImporteRetencionIGV;

                        lblTitleOrdenServicio02.Text = "Gravada";
                        lblTitleOrdenServicio03.Text = "IGV " + decimal.Round(varObjECMP_OrdenServicio.IGV * 100, 2) + "%";
                        lblTitleOrdenServicio04.Text = "Importe Total " + ObjECMP_OrdenServicio.ObjESGC_Moneda.Simbolo;
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
                    decimal dmlImporte = ObjECMP_OrdenServicioDetalle.PrecioUnitario * ObjECMP_OrdenServicioDetalle.Cantidad;
                    if (chkAplicarRetencion.IsChecked.Value)
                        dmlTasa = (SGCMethod.GetTasaHonorario(dmlImporte) / 100);
                    decimal dmlImporteIGV = (dmlImporte * dmlTasa);

                    ObjECMP_OrdenServicioDetalle.Importe = dmlImporte - dmlImporteIGV;
                    ObjECMP_OrdenServicioDetalle.ImporteIGV = dmlImporteIGV;
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
        private void ImprimirOrdenServicio()
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
                string Monto = Convert.ToDecimal(txtTotal.Text).ToString();
                string direccion = new BALM_Almacen().ListFiltrarAlmacen(ObjECMP_OrdenServicio.ObjESGC_EmpresaSucursal).FirstOrDefault().Direccion;
                parametro = new string[]
                {
                    "prmRazonSocial|"       + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.RazonSocial,
                    "prmRuc|"               + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.Ruc,
                    "prmDireccionEmpresa|"  + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.DireccionFiscal,
                    "prmTelefonoEmpresa|"   + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.Telefono,
                    "prmFechaDocumento|"    + varObjECMP_OrdenServicio.Fecha.ToShortDateString(),
                    "prmFechaLetra|"        + DateTime.Now.ToLongDateString().Split(',').ElementAt(1),
                    "prmNumOrden|"          + varObjECMP_OrdenServicio.Serie + " - " + varObjECMP_OrdenServicio.Numero,
                    "prmProveedor|"         + varObjECMP_OrdenServicio.ObjEMNF_ClienteProveedor.RazonSocial,
                    "prmRucProveedor|"      + varObjECMP_OrdenServicio.ObjEMNF_ClienteProveedor.NroDocIdentidad,
                    "prmDireccionProv|"     + varObjECMP_OrdenServicio.ObjEMNF_ClienteProveedor.Direccion,
                    "prmCondicionPago|"     + varObjECMP_OrdenServicio.ObjESGC_FormaPago.FormaPago,
                    "prmNetoLetra|"         + NumLetras.Convertir(Monto,true,varObjECMP_OrdenServicio.ObjESGC_Moneda.Descripcion ),
                    "prmTotal|"             + varObjECMP_OrdenServicio.ObjESGC_Moneda.Simbolo + " "+txtGravada.Text,
                    "prmIgv|"               + varObjECMP_OrdenServicio.ObjESGC_Moneda.Simbolo + " "+txtImporteIGV.Text,
                    "prmGravadaText|"       + lblTitleOrdenServicio02.Text,
                    "prmIgvText|"           + lblTitleOrdenServicio03.Text,
                    "prmNetoText|"          + strNetoText,
                    "prmNeto|"              + varObjECMP_OrdenServicio.ObjESGC_Moneda.Simbolo + " "+txtTotal.Text,
                    "prmSucursal|"          + ObjECMP_OrdenServicio.ObjESGC_EmpresaSucursal.Sucursal,
                    "prmDireccionSucursal|" + direccion,
                    "prmFechaFin|"          + ObjECMP_OrdenServicio.FechaFin.ToShortDateString(),
                    "prmMoneda|"            + ObjECMP_OrdenServicio.ObjESGC_Moneda.Descripcion,
                    "prmCreadopor|"         + ObjECMP_OrdenServicio.Creacion ,
                    "prmAprobadopor|"       + ObjECMP_OrdenServicio.Aprobacion

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
