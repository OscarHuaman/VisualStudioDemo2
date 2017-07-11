namespace CMP.Presentation.TempArticuloListaPrecio
{
    using CMP.Business;
    using CMP.Useful.Modulo;
    using ComputerSystems;
    using ComputerSystems.WPF;
    using MNF.Business;
    using MNF.Entity;
    using SGC.Empresarial.Entity;
    using SGC.Empresarial.Method;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using ComputerSystems.WPF.Acciones.Controles.Buttons;
    using System.Windows.Data;
    using ALM.Entity;
    using CMP.Reports.TempArticuloListaPrecio;
    using MNF.Presentation.ClienteProveedor.Flyouts;
    using MNF.Presentation.Articulo.Flyouts;
    using CMP.Entity;
    using ComputerSystems.MethodList;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.Loading;

    public partial class PCMP_TempArticuloListaPrecio : UserControl, CmpINavigation
    {

        #region INSTANCIAS, VARIABLES, EVENTOS AGREGADOS
		private EMNF_ClienteProveedor ObjEMNF_ClienteProveedor;
		private EMNF_Articulo ObjEMNF_Articulo;
		private EMNF_ArticuloCategoria ObjEMNF_ArticuloCategoria;
		private EMNF_ArticuloSubCategoria ObjEMNF_ArticuloSubCategoria;
		private EMNF_ArticuloMarca ObjEMNF_ArticuloMarca;
		private List<EMNF_Articulo> ListEMNF_Articulo;
		private List<EMNF_ClienteProveedor> ListEMNF_ClienteProveedor;
		string IdCategoria = string.Empty;
		string IdSubCategoria = string.Empty;
		string IdMarca = string.Empty;
		string IdArticulo = string.Empty;
		string IdProveedor = string.Empty;
		public DateTime FechaDesde;
		public DateTime FechaHasta;
		private CmpLoading CmpLoading;
        #endregion

        #region EVENTOS DE LA CLASE

        public PCMP_TempArticuloListaPrecio()
		{
            DataContext = this;
            InitializeComponent();
		}
		
        public Frame MyFrame
        {
            set { /*throw new NotImplementedException();*/ }
        }

        public object Parameter
        {
            set
            {
				CmpLoading = new CmpLoading(LoadHeader, LoadDetail);
				CmpLoading.Exceptions = ((e) => { CmpMessageBox.Show(CMPMensajes.TitleAdminListadoPrecio, e.Message, CmpButton.Aceptar); });
				this.KeyDownCmpButtonTitleTecla(ActionF9: btnImprimir_IsClicked_1
					, ActionShiftE: btnExportarIsClicked
					, ActionESC: btnSalirIsClicked);
				cbxMarca.SelectionChanged += cbxMarca_SelectionChanged;
				dtpPeriodoInicio.SelectedDateChanged += dtpPeriodoInicio_SelectedDateChanged;
				dtpPeriodoFin.SelectedDateChanged += dtpPeriodoFin_SelectedDateChanged;
				cbxCategoria.SelectionChanged += cbxCategoria_SelectionChanged;
				cbxSubCategoria.SelectionChanged += cbxSubCategoria_SelectionChanged;
				txtIdProveedor.KeyDown += txtIdProveedor_KeyDown;
				AddFlyout();
				CmpLoading.LoadHeader();
            }
        }

        private void btnImprimir_IsClicked_1()
        {
			var listFilter = new List<string>();
            listFilter.Add(IdCategoria.Length > 0 ? "Categoria" : "");
            listFilter.Add(IdSubCategoria.Length > 0 ? "Sub Categoria" : "");
            listFilter.Add(IdMarca.Length > 0 ? "Marca" : "");
            listFilter.Add(IdArticulo != "0" ? "Articulo" : "");
            listFilter.Add(IdProveedor != "0" ? "Proveedor" : "");
            listFilter.RemoveAll(x => x == "");
            if (listFilter.Count == 0)
                listFilter.Add("Todos");
            new ViewReportData(IdCategoria, IdSubCategoria, IdMarca, IdArticulo, IdProveedor, FechaDesde.ToString("yyyyMM"), FechaHasta.ToString("yyyyMM"), listFilter).ShowDialog();
        }

        private void txtAriculo_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.B))
            {
                string strFiltro = txtAriculo.Text;
                if (strFiltro.Trim().Length == 0)
                    strFiltro = "%";
                string strOutMessageError = string.Empty;
                CmpTask.ProcessAsync(
                () =>
                {
                    
                        ObjEMNF_Articulo = new EMNF_Articulo()
                        {
                            ObjEMNF_ArticuloClase = new EMNF_ArticuloClase() { IdArtClase = 0 },
                            ObjEMNF_ArticuloMarca = new EMNF_ArticuloMarca() { IdMarca = 0 },
                            ObjEMNF_ArticuloSubCategoria = new EMNF_ArticuloSubCategoria() { IdSubCategoria = 0 },
                        };
                        ObjEMNF_ArticuloCategoria = new EMNF_ArticuloCategoria();
                        ListEMNF_Articulo = new BMNF_Articulo().ListGetArticulos(ObjEMNF_Articulo, ObjEMNF_ArticuloCategoria, strFiltro);
                },
                (ex) =>
                {
					if (ex!= null)
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminListadoPrecio, ex.Message, CmpButton.Aceptar);
                        return;
                    }
                    else
                    {
                        if (ListEMNF_Articulo.Count == 1)
                        {
                            ObjEMNF_Articulo = ListEMNF_Articulo.FirstOrDefault();
                            txtAriculo.Text = ObjEMNF_Articulo.Articulo;
                            CmpLoading.LoadDetail();
                        }
                        else
                        {
                            this.FlyoutIsOpen("PMNF_BuscarArticulos", ((value) =>
                            {
                                if (value is PMNF_BuscarArticulos)
                                {
                                    var FlyoutsPMNF_BuscarArticulos = (PMNF_BuscarArticulos)value;
                                    FlyoutsPMNF_BuscarArticulos.InitializePMNF_BuscarArticulos();
                                    FlyoutsPMNF_BuscarArticulos.LoadHeader();
                                    FlyoutsPMNF_BuscarArticulos.SetValueFilter = txtAriculo.Text;
                                    FlyoutsPMNF_BuscarArticulos.IsOpen = true;
                                }
                            }));
                        }
                    }
                });
            }
        }

        private void txtIdProveedor_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.B))
            {
                string strFiltro = txtIdProveedor.Text;
                //if (strFiltro.Trim().Length == 0)
                //    strFiltro = "%";
                string strOutMessageError = string.Empty;
                CmpTask.ProcessAsync(
                () =>
                {
                    ListEMNF_ClienteProveedor = new BMNF_ClienteProveedor().ListFiltrarClienteProveedor(strFiltro);
                },
                (ex) =>
                {
                    if (ex != null)
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminListadoPrecio, ex.Message, CmpButton.Aceptar);
                        return;
                    }
                    else
                    {
                        if (ListEMNF_ClienteProveedor.Count == 1)
                        {
                            ObjEMNF_ClienteProveedor = ListEMNF_ClienteProveedor.FirstOrDefault();
                            txtIdProveedor.Text = ObjEMNF_ClienteProveedor.RazonSocial;
                            CmpLoading.LoadDetail();
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

        private void dtpPeriodoFin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            FechaHasta = (dtpPeriodoFin.SelectedDate != null) ? dtpPeriodoFin.SelectedDate.Value : DateTime.Now;
			CmpLoading.LoadDetail();
        }

        private void cbxMarca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ObjEMNF_ArticuloMarca = (EMNF_ArticuloMarca)cbxMarca.SelectedItem;
            CmpLoading.LoadDetail();
        }

        private void dtpPeriodoInicio_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            FechaDesde = (dtpPeriodoInicio.SelectedDate != null) ? dtpPeriodoInicio.SelectedDate.Value : DateTime.Now;
			CmpLoading.LoadDetail();
        }

        private void cbxCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ObjEMNF_ArticuloCategoria = (EMNF_ArticuloCategoria)cbxCategoria.SelectedItem;
            if (ObjEMNF_ArticuloCategoria == null)
            {
                dtgListaPrecio.Columns.Clear();
                return;
            }
            var vrListEMNF_SubCategoria = new List<EMNF_ArticuloSubCategoria>();
            string strOutMessageError = string.Empty;
            CmpTask.ProcessAsync(() =>
            {
                vrListEMNF_SubCategoria = new BMNF_ArticuloSubCategoria().ListAdministrarSubCategoria(ObjEMNF_ArticuloCategoria);
                vrListEMNF_SubCategoria.Add(new EMNF_ArticuloSubCategoria()
                {
                    IdSubCategoria = 0,
                    SubCategoria = "TODOS"
                });
            },
            (ex) =>
            {
                if (ex != null)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminListadoPrecio, ex.Message, CmpButton.Aceptar);
                    return;
                }
                else
                {
                    cbxSubCategoria.ItemsSource = vrListEMNF_SubCategoria;
					cbxSubCategoria.SelectedItem = vrListEMNF_SubCategoria.FirstOrDefault(x => x.IdSubCategoria == 0);
                }
            });
        }

        private void cbxSubCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ObjEMNF_ArticuloSubCategoria = (EMNF_ArticuloSubCategoria)cbxSubCategoria.SelectedItem;
            if (ObjEMNF_ArticuloSubCategoria != null) CmpLoading.LoadDetail();
        }

        private void txtAriculo_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            TextBox vrTextBox = (TextBox)sender;
			if (vrTextBox.Text.Trim().Length > 0) return;
			else
			{
				if (vrTextBox.Name == "txtAriculo")
					ObjEMNF_Articulo.IdArticulo = 0;
				else
					ObjEMNF_ClienteProveedor.IdCliProveedor = 0;
				CmpLoading.LoadDetail();
			}
        }

        public void btnExportarIsClicked()
        {
            if (dtgListaPrecio.Items.Count == 0)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "No hay Datos que Exportar", CmpButton.Aceptar);
                return;
            }
            var vrObjListaPrecio = new BCMP_TempArticuloListaPrecio().ListArticuloListaPrecio_Preview(IdCategoria, IdSubCategoria, IdMarca, IdArticulo, IdProveedor, FechaDesde.ToString("yyyyMM"), FechaHasta.ToString("yyyyMM"));
            try
            {
                var ListarExcel = vrObjListaPrecio.Select(x
					=> new
					{
						Codigo = x.Codigo,
						Articulo = x.Articulo,
						UnidadMedida = x.CodUndMedida,
						Marca = x.Marca,
						Categoria = x.Categoria,
						SubCategoria = x.SubCategoria,
						NroDocumento = x.NroDocIdentidad,
						RazonSocial = x.RazonSocial,
						CodMoneda = x.CodMoneda,
						Periodo = x.Periodo,
						Precio = x.Precio,
					}).ToList();
                ListarExcel.Export("ListaPrecio", ExportType.Excel, (value) =>
                {
                    CmpMessageBox.Show(CMPMensajes.TitleAdminListadoPrecio, value, CmpButton.Aceptar);
                });
            }
            catch (Exception ex)
            {
                CmpMessageBox.Show(CMPMensajes.TitleAdminListadoPrecio, ex.Message, CmpButton.Aceptar);
            }
        }

        public void btnSalirIsClicked()
        {
            if (!btnSalir.IsEnabled)
                return;
            cbxMarca.SelectionChanged -= cbxMarca_SelectionChanged;
            dtpPeriodoInicio.SelectedDateChanged -= dtpPeriodoInicio_SelectedDateChanged;
            dtpPeriodoFin.SelectedDateChanged -= dtpPeriodoFin_SelectedDateChanged;
            txtIdProveedor.KeyDown -= txtIdProveedor_KeyDown;
            this.Close(TipoModulo.ManuFactura);
        }

        #endregion

        #region MÉTODOS Y FUNCIONES

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

            this.FlyoutInitialize();
            this.FlyoutAdd(MyPMNF_BuscarClienteProveedor);
            this.FlyoutAdd(MyPMNF_BuscarArticulos);
        }

        public void LoadDetail()
        {
            Application.Current.Dispatcher.Invoke(() =>
                {
                    var Listado = new List<string>();
                    string strOutMessageError = string.Empty;
                    CmpTask.ProcessAsync(() =>
                    {
                        IdCategoria = (ObjEMNF_ArticuloCategoria != null) ? Convert.ToString(ObjEMNF_ArticuloCategoria.IdCategoria) : string.Empty;
                        IdSubCategoria = (ObjEMNF_ArticuloSubCategoria != null) ? Convert.ToString(ObjEMNF_ArticuloSubCategoria.IdSubCategoria) : string.Empty;
                        IdMarca = (ObjEMNF_ArticuloMarca != null) ? Convert.ToString(ObjEMNF_ArticuloMarca.IdMarca) : string.Empty;
                        IdArticulo = (ObjEMNF_Articulo != null) ? Convert.ToString(ObjEMNF_Articulo.IdArticulo) : "0";
                        IdProveedor = (ObjEMNF_ClienteProveedor != null) ? Convert.ToString(ObjEMNF_ClienteProveedor.IdCliProveedor) : "0";
                        Listado = new BCMP_TempArticuloListaPrecio().ListArticuloListaPercio(IdCategoria, IdSubCategoria, IdMarca, IdArticulo, IdProveedor, FechaDesde.ToString("yyyyMM"), FechaHasta.ToString("yyyyMM"));

                    },
                    (e) =>
                    {
                        if (e != null)
                        {
                            CmpMessageBox.Show(CMPMensajes.TitleAdminListadoPrecio, e.Message, CmpButton.Aceptar);
                            return;
                        }
                        if (Listado == null)
                        {
                            dtgListaPrecio.Columns.Clear();
                            return;
                        }

                        dtgListaPrecio.Columns.Clear();

                        var vrObjStringColumn = Listado.FirstOrDefault();
                        string[] arrayNamaColumn = vrObjStringColumn.Split("/".ToCharArray());
                        arrayNamaColumn = arrayNamaColumn.Where(x => x != "").ToArray();
                        int Index = 0;
                        foreach (var name in arrayNamaColumn)
                        {
                            FrameworkElementFactory TextFActory = new FrameworkElementFactory(typeof(TextBlock));
                            DataTemplate dataTemp = new DataTemplate();
                            DataGridTemplateColumn tempColumn = new DataGridTemplateColumn();
                            tempColumn.Header = name;
                            Binding bing = new Binding("[" + Index + "]");
                            bing.Mode = BindingMode.TwoWay;
                            TextFActory.SetBinding(TextBlock.TextProperty, bing);

                            if (Index > 8)
                            {
                                if (TextFActory.Type.Equals(typeof(TextBlock)))
                                {
                                    TextFActory.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Right);
                                }
                                tempColumn.MinWidth = 100;
                                tempColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                            }

                            dataTemp.VisualTree = TextFActory;
                            tempColumn.CellTemplate = dataTemp;
                            tempColumn.IsReadOnly = true;
                            dtgListaPrecio.Columns.Add(tempColumn);

                            Index++;
                        }
                        dtgListaPrecio.Columns[0].MinWidth = 75;
                        dtgListaPrecio.Columns[1].MinWidth = 200;
                        dtgListaPrecio.Columns[1].MinWidth = 300;
                        dtgListaPrecio.Columns[2].MinWidth = 90;
                        dtgListaPrecio.Columns[3].MinWidth = 120;
                        dtgListaPrecio.Columns[4].MinWidth = 120;
                        dtgListaPrecio.Columns[5].MinWidth = 120;
                        dtgListaPrecio.Columns[6].MinWidth = 90;
                        dtgListaPrecio.Columns[7].MinWidth = 300;
                        dtgListaPrecio.Columns[7].Header = "Proveedor";
                        dtgListaPrecio.Columns[8].MinWidth = 70;
                        //Agrega Valor

                        Listado.RemoveAt(0);
                        List<object> rows = new List<object>();
                        foreach (var value in Listado)
                        {
                            string[] arrayValueRow = value.Split(",".ToCharArray());
                            rows.Add(arrayValueRow);
                        }

                        dtgListaPrecio.ItemsSource = rows;
                        lblCountItems.Text = dtgListaPrecio.Items.Count + " Registros";
                    });
                });
        }

        public void LoadHeader()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var vrListEMNF_Categoria = new List<EMNF_ArticuloCategoria>();
                var vrListEMNF_ArticuloMarca = new List<EMNF_ArticuloMarca>();
                CmpTask.ProcessAsync(() =>
                {
                    int IdClase = 0;
                    string strOutMessageError = string.Empty;
                    vrListEMNF_Categoria = new BMNF_ArticuloCategoria().LisFiltrarCategoria(IdClase);
                    vrListEMNF_ArticuloMarca = new BMNF_ArticuloMarca().ListFiltrarMarca();
                },
                (e) =>
                {
                    if (e != null)
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleAdminListadoPrecio, e.Message, CmpButton.Aceptar);
                        return;
                    }
                    vrListEMNF_Categoria.Add(new EMNF_ArticuloCategoria()
                    {
                        IdCategoria = 0,
                        Categoria = "TODOS"
                    });
                    vrListEMNF_ArticuloMarca.Add(new EMNF_ArticuloMarca()
                    {
                        IdMarca = 0,
                        Marca = "TODOS"
                    });
                   
                    cbxCategoria.ItemsSource = vrListEMNF_Categoria;
                    cbxCategoria.SelectedItem = vrListEMNF_Categoria.First(x => x.IdCategoria == 0);
                    cbxMarca.ItemsSource = vrListEMNF_ArticuloMarca;
                    cbxMarca.SelectedItem = vrListEMNF_ArticuloMarca.First(x => x.IdMarca == 0);
                    if (dtpPeriodoFin.SelectedDate == null) dtpPeriodoFin.SelectedDate = DateTime.Now;
                    if (FechaHasta == new DateTime()) FechaHasta = DateTime.Now;
                    if (dtpPeriodoInicio.SelectedDate == null) dtpPeriodoInicio.SelectedDate = DateTime.Now;
                    if (FechaDesde == new DateTime()) FechaDesde = DateTime.Now;
                });
            });
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
                txtIdProveedor.Text = ObjEMNF_ClienteProveedor.RazonSocial;
                CmpLoading.LoadDetail();
            }
        }

        /// <summary>
        /// Instancia y pinta valor del Artículo seleccionado de la busqueda
        /// </summary>
        /// <param name="ObjEMNF_Articulo">Objeto de la clase Artículo</param>
        public void AddItemsArticulos(EMNF_Articulo ObjEMNF_Articulo)
        {
            this.ObjEMNF_Articulo = ObjEMNF_Articulo;
            txtAriculo.Text = ObjEMNF_Articulo.Articulo;
            CmpLoading.LoadDetail();
        }

        #endregion
    }
}

