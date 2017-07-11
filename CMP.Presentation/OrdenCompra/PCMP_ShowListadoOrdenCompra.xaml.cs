namespace CMP.Presentation.OrdenCompra
{
    using CMP.Entity;
    using CMP.Useful.Modulo;
    using ComputerSystems.WPF;

    using SGC.Empresarial.Entity;
    using SGC.Empresarial.Useful.Modulo;

    public partial class PCMP_ShowListadoOrdenCompra
    {
        private ESGC_PermisoPerfil ObjEPermisoPerfil;
        

        public PCMP_ShowListadoOrdenCompra(ESGC_PermisoPerfil ObjEPermisoPerfil)
        {
            InitializeComponent();
            this.ObjEPermisoPerfil = ObjEPermisoPerfil;
            
        }

        private void MetroWindow_ContentRendered_1(object sender, System.EventArgs e)
        {
            MyAdministrarListadoOrdenCompra.InitializeAdministrarListadoOrdenCompra(ObjEPermisoPerfil);
            MyAdministrarListadoOrdenCompra.LoadDetail();
        }
    }
}
