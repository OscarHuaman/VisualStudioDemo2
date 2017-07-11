namespace CMP.ViewModels.ReporteGeneralDocumento.Page
{
    using CMP.ViewModels.ReporteGeneralDocumento.VM;
    using System.Windows.Controls;

    public partial class PCMP_ListadoDocumento : Page
    {
        public PCMP_ListadoDocumento()
        {
            InitializeComponent();
            DataContext = new VCMP_ListadoDocumento();
        }
    }
}
