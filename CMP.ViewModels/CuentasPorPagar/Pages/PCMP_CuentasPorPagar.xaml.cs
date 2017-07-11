using CMP.ViewModels.CuentasPorPagar.VM;
using System.Windows.Controls;

namespace CMP.ViewModels.CuentasPorPagar.Pages
{
    /// <summary>
    /// Lógica de interacción para ViewAccountsByToPay.xaml
    /// </summary>
    public partial class PCMP_CuentasPorPagar : Page
    {
        public PCMP_CuentasPorPagar()
        {
            InitializeComponent();
            DataContext = new VCMP_CuentasPorPagar();
        }
    }
}
