namespace CMP.ViewModels.ReporteGeneralHonorario.Page
{
    using CMP.ViewModels.ReporteGeneralHonorario.VM;
    using System;
    using System.Windows.Controls;
    /// <summary>
    /// Lógica de int
    /// eracción para PALM_ListadoDocumento.xaml
    /// </summary>
    public partial class PCMP_ListadoHonorario : Page
    {
        public PCMP_ListadoHonorario()
        {
            InitializeComponent();
            DataContext = new VCMP_ListadoHonorario();
        }

    }
}
