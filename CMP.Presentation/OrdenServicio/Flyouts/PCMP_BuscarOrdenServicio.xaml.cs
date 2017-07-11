namespace CMP.Presentation.OrdenServicio.Flyouts
{
    using CMP.Business;
    using CMP.Entity;
    using CMP.Useful.Modulo;
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.Buttons;
    using MahApps.Metro.Controls;
    using MNF.Entity;
    using SGC.Empresarial.Entity;
    using SGC.Empresarial.Method;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    public partial class PCMP_BuscarOrdenServicio
    {
        #region INSTANCIAS, VARIABLES, EVENTOS AGREGADOS

        private ECMP_OrdenServicio ObjECMP_OrdenServicio;

        private bool IsFiltrado = true;
     
        private TypeFilterServicio MyTypeFilter;
        
        public delegate void isSelected(ECMP_OrdenServicio ObjECMP_OrdenServicio);

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
        public List<ECMP_OrdenServicio> SetListECMP_OrdenServicio
        {
            set
            {
                if (value.Count > 0)
                {
                    IsFiltrado = false;
                }
                dtgOrdenServicio.ItemsSource = value;
            }
        }

        #endregion

        #region EVENTOS DE LA CLASE

        public PCMP_BuscarOrdenServicio()
        {
        }

        public void InitializePCMP_BuscarOrdenServicio(TypeFilterServicio MyTypeFilter, ECMP_OrdenServicio ObjECMP_OrdenServicio)
        {
            InitializeComponent();


            int delta = DayOfWeek.Monday - DateTime.Now.DayOfWeek;

            dtpFechaDesde.SelectedDate = DateTime.Now.AddDays(delta);
            dtpFechaHasta.SelectedDate = DateTime.Now;

            this.KeyDownCmpButtonTitleTecla(ActionF7: btnAceptarIsClicked,
                                            ActionCtrlD: btnVolverIsClicked);
            MyIsOpenChanged();

            this.ObjECMP_OrdenServicio = ObjECMP_OrdenServicio;
            this.MyTypeFilter = MyTypeFilter;
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
                ObjECMP_OrdenServicio.Opcion = "D";
                cbxOpcion.SelectedIndex = 2;
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

        private void cbxOpcion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int intIndex = cbxOpcion.SelectedIndex;
                if (intIndex == 0)
                {
                    GridFriltrarFecha.Visibility = System.Windows.Visibility.Visible;
                    txtFiltrar.Visibility = System.Windows.Visibility.Collapsed;
                    ObjECMP_OrdenServicio.Opcion = "F";

                    string strWatermarkProperty = string.Empty;
                    string strToolTip = string.Empty;
                    txtFiltrar.SetValue(TextBoxHelper.WatermarkProperty, strWatermarkProperty);
                    txtFiltrar.ToolTip = strToolTip;
                }
                else if (intIndex == 1)
                {
                    GridFriltrarFecha.Visibility = System.Windows.Visibility.Collapsed;
                    txtFiltrar.Visibility = System.Windows.Visibility.Visible;
                    ObjECMP_OrdenServicio.Opcion = "M";

                    string strWatermarkProperty = "Filtrar por Moneda";
                    string strToolTip = "Filtrar por Moneda";
                    txtFiltrar.SetValue(TextBoxHelper.WatermarkProperty, strWatermarkProperty);
                    txtFiltrar.ToolTip = strToolTip;
                }
                else if (intIndex == 2)
                {
                    GridFriltrarFecha.Visibility = System.Windows.Visibility.Collapsed;
                    txtFiltrar.Visibility = System.Windows.Visibility.Visible;
                    ObjECMP_OrdenServicio.Opcion = "D";

                    string strWatermarkProperty = "Filtrar por Número documento";
                    string strToolTip = "Filtrar por Número documento";
                    txtFiltrar.SetValue(TextBoxHelper.WatermarkProperty, strWatermarkProperty);
                    txtFiltrar.ToolTip = strToolTip;
                }

                LoadDetail("%");
            }
            catch (NullReferenceException) { }
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
        }

        private void btnAceptarIsClicked()
        {
            IsSelectedItems();
        }

        #endregion

        #region MÉTODOS

        /// <summary>
        /// Carga datos de orden compra
        /// </summary>
        /// <param name="Filtro">Datos a filtrar</param>
        /// <summary>
        /// Carga datos Orden Compra
        /// </summary>
        /// <param name="Filtro">Datos para filtrar</param>
        public async void LoadDetail(string Filtro = "%")
        {
            var vrListECMP_OrdenServicio = new List<ECMP_OrdenServicio>();

            string strOutMessageError = string.Empty;
            await System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                try
                {
                    if (!IsFiltrado)
                        IsFiltrado = true;
                    else
                        vrListECMP_OrdenServicio = new BCMP_OrdenServicio().ListFiltrarOrdenServicio(ObjECMP_OrdenServicio, Filtro);

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (MyTypeFilter == TypeFilterServicio.Factura)
                            dtgOrdenServicio.ItemsSource = vrListECMP_OrdenServicio.Where(x => x.Exonerado == 11 || x.Exonerado == 12 || x.Exonerado == 22);
                        else if (MyTypeFilter == TypeFilterServicio.FacturaRetencion)
                            dtgOrdenServicio.ItemsSource = vrListECMP_OrdenServicio.Where(x => x.Exonerado == 11 && x.Retencion == true);
                        else
                            dtgOrdenServicio.ItemsSource = vrListECMP_OrdenServicio.Where(x => x.Exonerado == 21);
                        
                        lblCountItems.Text = dtgOrdenServicio.Items.Count + " Registros";
                    });
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(CMPMensajes.TitleMessage, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        /// <summary>
        /// Selecciona el items de los datos filtrado
        /// </summary>
        private void IsSelectedItems()
        {
            if (dtgOrdenServicio.Items.Count == 0) { return; }
            if (_isSelected != null)
            {
                cbxOpcion.SelectionChanged -= cbxOpcion_SelectionChanged;
                var varObjECMP_Servicio = (ECMP_OrdenServicio)dtgOrdenServicio.SelectedItem;
                if (varObjECMP_Servicio == null) { return; }
                _isSelected.Invoke(varObjECMP_Servicio);
            }
            IsOpen = false;
        }

        #endregion

    }

    public enum TypeFilterServicio 
    {
        FacturaRetencion,
        Factura,
        Honorario
    }
}
