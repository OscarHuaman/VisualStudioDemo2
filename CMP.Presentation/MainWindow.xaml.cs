using CMP.Entity;
using CMP.Presentation.Compra;
using CMP.Presentation.OrdenCompra;
using CMP.Presentation.OrdenServicio;
using CMP.Presentation.TempArticuloListaPrecio;
using SGC.Empresarial.Entity;
using System.Windows;
using ComputerSystems.WPF;
using CMP.ViewModels.NotaCreditoDebito.Pages;
using CMP.ViewModels.ReporteGeneralHonorario.Page;
using CMP.ViewModels.ReporteGeneralDocumento.Page;
using CMP.ViewModels.CuentasPorPagar.Pages;
using CMP.ViewModels.ReporteStockMinimo.Page;
using ComputerSystems.WPF.Notificaciones;

namespace CMP.Presentation
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //new PCMP_ShowListadoOrdenCompra(new ESGC_PermisoPerfil() { Nuevo = true, Editar = true, Eliminar = true }).Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //new PCMP_ShowListadoCompra(new ESGC_PermisoPerfil() { Nuevo = true, Editar = true, Eliminar = true }).Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //new PCMP_ShowListadoOrdenServicio(new ESGC_PermisoPerfil() { Nuevo = true, Editar = true, Eliminar = true }).Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //new PCMP_ShowListadoOrdenCompra(new ESGC_PermisoPerfil() { Nuevo = true, Editar = true, Eliminar = true }).Show();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            //new PCMP_ShowCompra(new ESGC_PermisoPerfil() { Nuevo = true, Editar = true, Eliminar = true }, new ECMP_Compra(new ECMP_Compra(null, TipoConstructor.Insert), TipoConstructor.Insert)).Show();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            //new PCMP_ShowTempArticuloListaPrecio(new ESGC_PermisoPerfil() { Nuevo = true, Editar = true, Eliminar = true }).Show();
            //CmpViewNavigation.Show(new PCMP_NotaCreditoDebito(), new ECMP_NotaCreditoDebito());
            new CmpNavigationService().Show(new PCMP_ListadoNotaCreditoDebito(), new ECMP_NotaCreditoDebito());
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            new CmpNavigationService().Show(new PCMP_ListadoStockMinimo(), new ECMP_ReporteStockMinimo());
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            new CmpNavigationService().Show(new PCMP_ListadoDocumento(), new ECMP_ReporteGrlDocumento());
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            new CmpNavigationService().Show(new PCMP_ListadoHonorario(), new ECMP_ReporteGrlHonorario());
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            new CmpNavigationService().Show(new PCMP_ListadoReciboGastosInternos(), new ECMP_ReporteGastosInternos());
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            new CmpNavigationService().Show(new PCMP_CuentasPorPagar(), new ECMP_CuentasPorPagar());
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            //new PCMP_ShowTempArticuloListaPrecio(new ESGC_PermisoPerfil() { Nuevo = true, Editar = true, Eliminar = true }).Show();
        }
    }
}
