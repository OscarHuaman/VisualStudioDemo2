namespace CMP.Presentation.OrdenServicio
{
    using CMP.Entity;
    using SGC.Empresarial.Entity;
    using SGC.Empresarial.Useful.Modulo;

    public partial class PCMP_ShowListadoOrdenServicio
    {
        private ESGC_PermisoPerfil ObjEPermisoPerfil;

        public PCMP_ShowListadoOrdenServicio(ESGC_PermisoPerfil ObjEPermisoPerfil)
        {
            InitializeComponent();
            this.ObjEPermisoPerfil = ObjEPermisoPerfil;
            
        }

        private void MetroWindow_ContentRendered_1(object sender, System.EventArgs e)
        {
            MyAdministrarListadoOrdenServicio.InitializeAdministrarListadoOrdenCompra(ObjEPermisoPerfil);
            MyAdministrarListadoOrdenServicio.LoadDetail();
        }

    }
}
