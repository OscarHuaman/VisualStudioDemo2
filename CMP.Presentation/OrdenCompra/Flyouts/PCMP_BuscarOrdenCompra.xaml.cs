namespace CMP.Presentation.OrdenCompra.Flyouts
{
    using CMP.Business;
    using CMP.Entity;
    using CMP.Useful.Modulo;
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.Buttons;
    using MahApps.Metro.Controls;
    using SGC.Empresarial.Entity;
    using SGC.Empresarial.Method;
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls;
    using System.Linq;
    
    public partial class PCMP_BuscarOrdenCompra
    {
        #region INSTANCIAS, VARIABLES, EVENTOS AGREGADOS

        private ECMP_OrdenCompra ObjECMP_OrdenCompra;

        private bool IsFiltrado = true;        

        public delegate void isSelected(ECMP_OrdenCompra ObjECMP_OrdenCompra);
        private isSelected _isSelected;
        public event isSelected IsSelected
        {
            add
            {
                _isSelected = (isSelected)System.Delegate.Combine(_isSelected, value);
            }
            remove
            {
                _isSelected = (isSelected)System.Delegate.Remove(_isSelected, value);
            }
        }

        /// <summary>
        /// Agrega valor al textbox filtrar del buscador de Cliente Proveedores
        /// </summary>
        public string SetValueFilter 
        {
            set
            {
                txtFiltrar.Text = value;
            }
        }

        /// <summary>
        /// Agrega datos buscado a la grilla desde un buscador externo
        /// </summary>
        public List<ECMP_OrdenCompra> SetListECMP_OrdenCompra
        {
            set
            {
                if (value.Count > 0)
                {
                    IsFiltrado = false;
                }
                dtgOrdenCompra.ItemsSource = value;
            }
        }

        #endregion

        #region EVENTOS 
        public PCMP_BuscarOrdenCompra()
        {
        }

        public void InitializePCMP_BuscarOrdenCompra(ECMP_OrdenCompra ObjECMP_OrdenCompra)
        {
            InitializeComponent();

            int delta = DayOfWeek.Monday - DateTime.Now.DayOfWeek;
            dtpFechaDesde.SelectedDate = DateTime.Now.AddDays(delta);
            dtpFechaHasta.SelectedDate = DateTime.Now;

            this.KeyDownCmpButtonTitleTecla(ActionF7: btnAceptarIsClicked,
                                            ActionCtrlD: btnVolverIsClicked);

            MyIsOpenChanged();
            this.ObjECMP_OrdenCompra = ObjECMP_OrdenCompra;
            LoadDetail();
        }

        public void MyIsOpenChanged()
        {
            this.IsOpenChanged += CmpFlyoutEvents.IsOpenChanged(() =>
            {
                this.IsFocus(false);
                cbxOpcion.SelectionChanged += cbxOpcion_SelectionChanged;
                dtpFechaDesde.SelectedDateChanged += _SelectedDateChanged;
                dtpFechaHasta.SelectedDateChanged += _SelectedDateChanged;
                this.txtFiltrar.KeyUp += txtFiltrar_KeyUp_1;
                cbxOpcion.SelectedIndex = 2;
                ObjECMP_OrdenCompra.Opcion = "D";
            },
            () =>
            {
                this.IsFocus(true);
                cbxOpcion.SelectionChanged -= cbxOpcion_SelectionChanged;
                dtpFechaDesde.SelectedDateChanged -= _SelectedDateChanged;
                dtpFechaHasta.SelectedDateChanged -= _SelectedDateChanged;
                this.txtFiltrar.KeyUp -= txtFiltrar_KeyUp_1;
                this.IsOpenChanged -= CmpFlyoutEvents.IsOpenChanged;
            });
        }

        private void _SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxOpcion.SelectedIndex == 0)
            {
                ObjECMP_OrdenCompra.Fecha = dtpFechaDesde.SelectedDate.Value;
                ObjECMP_OrdenCompra.FechaEntrega = dtpFechaHasta.SelectedDate.Value;
            }
            LoadDetail();
        }

        private void dtgPreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                IsSelectedItems();
            }

        }

        private void dtgMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IsSelectedItems();
        }

        private void txtFiltrar_KeyUp_1(object sender, System.Windows.Input.KeyEventArgs e)
        {
            LoadDetail(((TextBox)sender).Text);
        }

        private void btnVolverIsClicked()
        {
            IsOpen = false;
            cbxOpcion.SelectedIndex = 0;
        }

        private void btnAceptarIsClicked()
        {
            IsSelectedItems();
        }

        private void cbxOpcion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int intIndex = cbxOpcion.SelectedIndex;
                if (intIndex == 0)
                {
                    GridFriltrarFecha.Visibility = System.Windows.Visibility.Visible;
                    txtFiltrar.Visibility = System.Windows.Visibility.Collapsed;
                    ObjECMP_OrdenCompra.Opcion = "F"; 

                    string strWatermarkProperty = string.Empty;
                    string strToolTip = string.Empty;
                    txtFiltrar.SetValue(TextBoxHelper.WatermarkProperty, strWatermarkProperty);
                    txtFiltrar.ToolTip = strToolTip;
                }
                else if (intIndex == 1)
                {
                    GridFriltrarFecha.Visibility = System.Windows.Visibility.Collapsed;
                    txtFiltrar.Visibility = System.Windows.Visibility.Visible;

                    ObjECMP_OrdenCompra.Opcion = "M";

                    string strWatermarkProperty = "Filtrar por Moneda";
                    string strToolTip = "Filtrar por Moneda";
                    txtFiltrar.SetValue(TextBoxHelper.WatermarkProperty, strWatermarkProperty);
                    txtFiltrar.ToolTip = strToolTip;
                }
                else if (intIndex == 2)
                {
                    GridFriltrarFecha.Visibility = System.Windows.Visibility.Collapsed;
                    txtFiltrar.Visibility = System.Windows.Visibility.Visible;

                    ObjECMP_OrdenCompra.Opcion = "D";

                    string strWatermarkProperty = "Filtrar por Número documento";
                    string strToolTip = "Filtrar por Número documento";
                    txtFiltrar.SetValue(TextBoxHelper.WatermarkProperty, strWatermarkProperty);
                    txtFiltrar.ToolTip = strToolTip;
                }

                LoadDetail("%");
            }
            catch (NullReferenceException) { }
        }

        #endregion

        #region MÉTODOS

        /// <summary>
        /// Carga datos de orden compra
        /// </summary>
        /// <param name="Filtro">Datos a filtrar</param>
        public void LoadDetail(string Filtro = "%")
        {
            var vrListECMP_OrdenCompra = new List<ECMP_OrdenCompra>();

            string strOutMessageError = string.Empty;
            CmpTask.Process(
            () =>
            {
                try
                {
                    if (!IsFiltrado)
                    {
                        IsFiltrado = true;
                    }
                    else
                    {
                        vrListECMP_OrdenCompra = new BCMP_OrdenCompra().ListOrdenCompra(ObjECMP_OrdenCompra, Filtro).Where(x => x.Provisionado == 0 && (x.ObjESGC_Estado.CodEstado == "APCOC" || x.ObjESGC_Estado.CodEstado == "ATCOC")).ToList();
                    }
                }
                catch (Exception ex)
                {
                    strOutMessageError = ex.Message;
                }
            },
            () =>
            {
                try
                {
                    if (strOutMessageError.Length > 0)
                    {
                        CmpMessageBox.Show(CMPMensajes.TitleConsulOrdenCompra, strOutMessageError, CmpButton.Aceptar);
                    }
                    else
                    {
                        dtgOrdenCompra.ItemsSource = vrListECMP_OrdenCompra;
                        lblCountItems.Text = vrListECMP_OrdenCompra.Count + " Registros";
                    }
                }
                catch (Exception)
                {
                }
            });
        }

        /// <summary>
        /// Selecciona el items de los datos filtrado
        /// </summary>
        private void IsSelectedItems()
        {
            if (dtgOrdenCompra.Items.Count == 0) { return; }
            if (_isSelected != null)
            {
                cbxOpcion.SelectionChanged -= cbxOpcion_SelectionChanged;
                var varObjECMP_Compra = (ECMP_OrdenCompra)dtgOrdenCompra.SelectedItem;
                if (varObjECMP_Compra == null) { return; }
                _isSelected.Invoke(varObjECMP_Compra);
            }
            IsOpen = false;
        }

        #endregion

    }
}
