namespace CMP.ViewModels.ReporteStockMinimo.Page
{
    using CMP.ViewModels.ReporteStockMinimo.VM;
    using System;
    using System.Windows.Controls;
    public partial class PCMP_ListadoStockMinimo : Page
    {
        public PCMP_ListadoStockMinimo()
        {
            InitializeComponent();
            DataContext = new VCMP_ListadoStockMinimo();
        }
    }
}
