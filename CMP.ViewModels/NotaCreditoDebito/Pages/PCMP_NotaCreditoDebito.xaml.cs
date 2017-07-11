namespace CMP.ViewModels.NotaCreditoDebito.Pages
{
    using CMP.ViewModels.NotaCreditoDebito.VM;
    using System.Windows.Controls;

    /// <summary>
    /// Lógica de interacción para ViewNotaCreditoDebito.xaml
    /// </summary>
    public partial class PCMP_NotaCreditoDebito : Page
    {
        public PCMP_NotaCreditoDebito()
        {
            InitializeComponent();
            DataContext = new VCMP_NotaCreditoDebito();
        }
    }
}
