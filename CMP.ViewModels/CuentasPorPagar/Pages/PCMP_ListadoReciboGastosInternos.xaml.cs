namespace CMP.ViewModels.CuentasPorPagar.Pages
{

    using CMP.ViewModels.CuentasPorPagar.VM;
    using System.Windows.Controls;

    public partial class PCMP_ListadoReciboGastosInternos : Page
    {
        public PCMP_ListadoReciboGastosInternos()
        {
            InitializeComponent();
            DataContext = new VCMP_ListadoReciboGastosInternos();
        }
    }
}
