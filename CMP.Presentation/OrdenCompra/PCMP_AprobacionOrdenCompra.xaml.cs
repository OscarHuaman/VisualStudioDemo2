namespace CMP.Presentation.OrdenCompra
{
    using CMP.Business;
    using CMP.Entity;
    using CMP.Reports;
    using CMP.Useful.Metodo;
    using CMP.Useful.Modulo;
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using SGC.Empresarial.Entity;
    using SGC.Empresarial.Method;
    using SGC.Empresarial.Useful.Modulo;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Controls;

    public partial class PCMP_AprobacionOrdenCompra : UserControl, CmpINavigation
    {
        #region INSTANCIAS, VARIABLES, EVENTOS AGREGADOS
        private NumLetra NumLetras = new NumLetra();
        public bool imprimir { get; set; }

        #endregion

        #region EVENTOS DE LA CLASE

        public PCMP_AprobacionOrdenCompra()
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

                MyHeader.DataContext = ObjECMP_OrdenCompra;
                //chkPrecioIncluidoIGV.IsChecked = ObjECMP_OrdenCompra.IncluyeIGV;
                chkPrecioIncluidoIGV.IsChecked = (ObjECMP_OrdenCompra.IncluyeIGV == true) ? true : false;
                lblTitleOrdenCompra04.Text = "IGV " + decimal.Round(ObjECMP_OrdenCompra.IGV * 100, 2) + "%";
                lblTitleServicio05.Text = "Total " + ObjECMP_OrdenCompra.ObjESGC_Moneda.Simbolo;

                LoadDetail();

                MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAdminCompra, CMPMensajes.GetAccesoRestringidoNull("Orden Compra"), new Action<ESGC_PermisoPerfil>((P) =>
                {
                    if (P == null) { return; }
                    btnGuardar.IsEnabled = (P.Nuevo || P.Editar);
                }));
            }
        }

        public void btnGuardarIsClicked()
        {
            if (!btnGuardar.IsEnabled)
                return;

            MSGC_UpdatePrivilege.Process(this, "CMP", CMPMensajes.TitleAprobacionOrdenCompra, CMPMensajes.GetAccesoRestringidoNull("Orden Compra"), new Action<ESGC_PermisoPerfil>((P) =>
            {
                if (P.Nuevo)
                {
                    var varObjECMP_OrdenCompra = (ECMP_OrdenCompra)MyHeader.DataContext;
                    varObjECMP_OrdenCompra.IncluyeIGV = chkPrecioIncluidoIGV.IsChecked.Value;

                    string strOutMessageError = string.Empty;
                    CmpMessageBox.Proccess(CMPMensajes.TitleAprobacionOrdenCompra, CMPMensajes.ProcesandoDatos, () =>
                    {
                        try
                        {
                            varObjECMP_OrdenCompra.Opcion = "A";
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
                            CmpMessageBox.Show(CMPMensajes.TitleAprobacionOrdenCompra, strOutMessageError, CmpButton.Aceptar);
                        }
                        else
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleAprobacionOrdenCompra, CMPMensajes.DatoProcesados + "\n ¿Desea Imprimir el documento de Orden de Compra?", CmpButton.AceptarCancelar, () =>
                            {
                                ImprimirOrdenCompra("APROBADO");
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
                    CmpMessageBox.Show(CMPMensajes.TitleAprobacionOrdenCompra, CMPMensajes.GetAccesoRestringidoNuevo("Orden Compra"), CmpButton.Aceptar);
                }
            }));
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
                new CmpNavigationService().Volver(_MyFrame,new ECMP_OrdenCompra());
            }),
            MyNameFomulario: "PCMP_ListadoOrdenCompra",
            MyActionAbort: () =>
            {
                this.Close(TipoModulo.ManuFactura);
            });
        }

        #endregion

        #region MÉTODOS DE LA CLASE

        /// <summary>
        /// Carga datos del orden de compra
        /// </summary>
        private void LoadDetail()
        {
            dgDetalleArticulo.Items.Clear();
            var vrListECompraDetalle = new List<ECMP_OrdenCompraDetalle>();
            var varObjECMP_OrdenCompra = (ECMP_OrdenCompra)MyHeader.DataContext;

            string strOutMessageError = string.Empty;
            CmpTask.Process(
            () =>
            {
                try
                {
                    vrListECompraDetalle = new BCMP_OrdenCompraDetalle().ListAdministrarOrdenCompraDetalle(varObjECMP_OrdenCompra);
                    vrListECompraDetalle.ForEach(x => x.IsEnableEstado = true);
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
                    CmpMessageBox.Show(CMPMensajes.TitleAprobacionOrdenCompra, strOutMessageError, CmpButton.Aceptar);
                }
                else
                {
                    var ObjECMP_OrdenCompra = (ECMP_OrdenCompra)MyHeader.DataContext;

                    foreach (var item in vrListECompraDetalle)
                    {
                        if (item.ObjEMNF_Articulo.ObjEMNF_OperacionIGV.CodOperacionIGV == "GB")
                        {
                            if (chkPrecioIncluidoIGV.IsChecked.Value || ObjECMP_OrdenCompra.IncluyeIGV)
                            {
                                //pRECIO INCLUIDO IGV
                                decimal dmlCalculoIGV = item.PrecioUnitario * ObjECMP_OrdenCompra.IGV;
                                item.PrecioUnitario = decimal.Round(dmlCalculoIGV + item.PrecioUnitario, 8);
                            }
                        }

                        dgDetalleArticulo.Items.Add(item);
                        CalcularTotalesItems(item);
                    }

                    lblExonerado.Text = decimal.Round(ObjECMP_OrdenCompra.Exonerada, 2).ToString("###,###,##0.#0");
                    lblGravada.Text = decimal.Round(ObjECMP_OrdenCompra.Gravada, 2).ToString("###,###,##0.#0");
                    lblTotalIgv.Text = decimal.Round(ObjECMP_OrdenCompra.ImporteIGV, 2).ToString("###,###,##0.#0");
                    lblTotalNeto.Text = decimal.Round((ObjECMP_OrdenCompra.Exonerada + ObjECMP_OrdenCompra.Gravada + ObjECMP_OrdenCompra.ImporteIGV), 2).ToString("###,###,##0.#0");
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
                var varObjECMP_OrdenCompra = (ECMP_OrdenCompra)MyHeader.DataContext;
                var ListECMP_OrdenCompraDetalle = (dgDetalleArticulo.Items.OfType<ECMP_OrdenCompraDetalle>()).ToList();
                if (ListECMP_OrdenCompraDetalle != null && ListECMP_OrdenCompraDetalle.Count > 0)
                {
                    decimal dmlTotal = 0;
                    decimal dmlGravada = 0;
                    decimal dmlImporteIGV = 0;
                    
                    //factura
                    if (chkPrecioIncluidoIGV.IsChecked.Value)
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
                        dmlImporteIGV = dmlGravada * varObjECMP_OrdenCompra.IGV;
                        dmlTotal = dmlGravada + dmlImporteIGV;
                    }

                    lblExonerado.Text = ListECMP_OrdenCompraDetalle.Sum(o => (o.ObjEMNF_Articulo.ObjEMNF_OperacionIGV.CodOperacionIGV == "EX" ? o.Importe : 0)).ToString("###,###,##0.#0");

                    lblLineas.Text = ListECMP_OrdenCompraDetalle.Count.ToString();
                    lblGravada.Text = decimal.Round(dmlGravada, 2).ToString("###,###,##0.#0");
                    lblTotalIgv.Text = decimal.Round(dmlImporteIGV, 2).ToString("###,###,##0.#0");
                    lblTotalNeto.Text = Convert.ToDouble(dmlTotal + Convert.ToDecimal(lblExonerado.Text)).ToString("N2");
                }
                else
                {
                    lblLineas.Text = ("0");
                    lblGravada.Text = ("0.00");
                    lblTotalIgv.Text = ("0.00");
                    lblExonerado.Text = ("0.00");
                    lblTotalNeto.Text = ("0.00");
                }
                dgDetalleArticulo.Items.Refresh();
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
                        decimal dmlPrecioUnitario = decimal.Round((ObjECMP_OrdenCompraDetalle.PrecioUnitarioTemp / ((decimal.Round(varObjECMP_OrdenCompra.IGV * 100, 2) + 100) / 100)), 8);
                        decimal dmlImporte = decimal.Round((dmlPrecioUnitario * ObjECMP_OrdenCompraDetalle.Cantidad), 8);
                        decimal dmlImporteIGV = decimal.Round(dmlImporte * varObjECMP_OrdenCompra.IGV, 8);

                        ObjECMP_OrdenCompraDetalle.PrecioUnitario = dmlPrecioUnitario;
                        ObjECMP_OrdenCompraDetalle.Importe = dmlImporte;
                        ObjECMP_OrdenCompraDetalle.ImporteIGV = dmlImporteIGV;
                    }

                    else
                    {
                        //Calculo sin incluir IGV
                        decimal dmlImporte = decimal.Round((ObjECMP_OrdenCompraDetalle.PrecioUnitarioTemp * ObjECMP_OrdenCompraDetalle.Cantidad), 8);
                        decimal dmlImporteIGV = decimal.Round(dmlImporte * varObjECMP_OrdenCompra.IGV, 8);

                        ObjECMP_OrdenCompraDetalle.PrecioUnitario = ObjECMP_OrdenCompraDetalle.PrecioUnitarioTemp;
                        ObjECMP_OrdenCompraDetalle.Importe = dmlImporte;
                        ObjECMP_OrdenCompraDetalle.ImporteIGV = dmlImporteIGV;
                    }
                }
                else
                {
                    ObjECMP_OrdenCompraDetalle.ImporteIGV = 0;
                    ObjECMP_OrdenCompraDetalle.Importe = (ObjECMP_OrdenCompraDetalle.PrecioUnitario * ObjECMP_OrdenCompraDetalle.Cantidad) + ObjECMP_OrdenCompraDetalle.ImporteIGV;
                }
                CalcularTotales();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Imprime un reporte de la venta
        /// </summary>
        private void ImprimirOrdenCompra(string Estado = "%")
        {
            try
            {
                var varObjECMP_OrdenCompra = (ECMP_OrdenCompra)MyHeader.DataContext;
                var vrObjListECMP_OrdenCompraDetalle = new BCMP_OrdenCompraDetalle().ListAdministrarOrdenCompraDetalle(varObjECMP_OrdenCompra);
                vrObjListECMP_OrdenCompraDetalle.ForEach((x) =>
                {
                    x.Importe = x.Cantidad * x.PrecioUnitario;
                });
                string[] parametro;
                string Monto = Convert.ToDecimal(lblTotalNeto.Text).ToString();
                parametro = new string[]
                {
                    "prmRazonSocial|"          + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.RazonSocial,
                    "prmRuc|"               + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.Ruc,
                    "prmDireccionEmpresa|" + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.DireccionFiscal,
                    "prmTelefonoEmpresa|"  + SGCVariables.ObjESGC_Usuario.ObjESGC_Empresa.Telefono,
                    "prmFechaDocumento|"    + varObjECMP_OrdenCompra.Fecha.ToShortDateString(),
                    "prmFechaLetra|"        + DateTime.Now.ToLongDateString().Split(',').ElementAt(1),
                    "prmNumOrden|"          + varObjECMP_OrdenCompra.Serie + " - " + varObjECMP_OrdenCompra.Numero,
                    "prmProveedor|"         + varObjECMP_OrdenCompra.ObjEMNF_ClienteProveedor.RazonSocial,
                    "prmRucProveedor|"      + varObjECMP_OrdenCompra.ObjEMNF_ClienteProveedor.NroDocIdentidad,
                    "prmDireccionProv|"     + varObjECMP_OrdenCompra.ObjEMNF_ClienteProveedor.Direccion,
                    "prmCondicionPago|"     + varObjECMP_OrdenCompra.ObjESGC_FormaPago.FormaPago,
                    "prmEstadoDocumento|"   + ((varObjECMP_OrdenCompra.ObjESGC_Estado.CodEstado=="PECOC") ? ((Estado == "%") ? "PENDIENTE" : "APROBADO"):varObjECMP_OrdenCompra.ObjESGC_Estado.Estado),
                    "prmLugarEntrega|"      + txtLugarEntrega.Text,
                    "prmIgv|"               + varObjECMP_OrdenCompra.ObjESGC_Moneda.Simbolo + " "+lblTotalIgv.Text,
                    "prmNeto|"              + varObjECMP_OrdenCompra.ObjESGC_Moneda.Simbolo + " "+lblTotalNeto.Text,
                    "prmExonerado|"         + varObjECMP_OrdenCompra.ObjESGC_Moneda.Simbolo + " "+lblExonerado.Text,  
                    "prmGravada|"           + varObjECMP_OrdenCompra.ObjESGC_Moneda.Simbolo + " "+lblGravada.Text,
                    "prmIgvText|"           + lblTitleOrdenCompra04.Text,
                    "prmNetoLetra|"         + NumLetras.Convertir(Monto,true,varObjECMP_OrdenCompra.ObjESGC_Moneda.Descripcion),
                    "prmSucursal|"          + varObjECMP_OrdenCompra.ObjESGC_EmpresaSucursal.Sucursal,
                    "prmDireccionSucursal|" + varObjECMP_OrdenCompra.LugarEntrega,
                    "prmFechaEntrega|"      + varObjECMP_OrdenCompra.FechaEntrega.ToShortDateString(),
                    "prmCreadopor|"         + varObjECMP_OrdenCompra.Creacion,
                    "prmMoneda|"            + varObjECMP_OrdenCompra.ObjESGC_Moneda.Descripcion,
                    "prmAprobadorpor|"      + varObjECMP_OrdenCompra.Aprobacion 
                };

                MainRerport ObjMainRerport = new MainRerport();
                ObjMainRerport.InitializeMainRerport("CMP.Reports.Files.RptOrdenCompra.rdlc", "dtsOrdenCompra", vrObjListECMP_OrdenCompraDetalle,  parametro);
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
