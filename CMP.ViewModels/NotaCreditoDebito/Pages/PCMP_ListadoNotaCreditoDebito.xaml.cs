using CMP.ViewModels.NotaCreditoDebito.VM;
using System.Windows.Controls;

namespace CMP.ViewModels.NotaCreditoDebito.Pages
{
    /// <summary>
    /// Lógica de interacción para PCMP_ListarNotaCreditoDebito.xaml
    /// </summary>
    public partial class PCMP_ListadoNotaCreditoDebito : Page
    {
        public PCMP_ListadoNotaCreditoDebito()
        {
            InitializeComponent();
            DataContext = new VCMP_ListarNotaCreditoDebito();
        }
    }
}
