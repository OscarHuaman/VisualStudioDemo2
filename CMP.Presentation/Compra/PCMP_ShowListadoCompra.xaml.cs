namespace CMP.Presentation.Compra
{
    using CMP.Entity;

    using SGC.Empresarial.Entity;
    using SGC.Empresarial.Useful.Modulo;

    public partial class PCMP_ShowListadoCompra
    {
        private ESGC_PermisoPerfil ObjEPermisoPerfil;

        public PCMP_ShowListadoCompra(ESGC_PermisoPerfil ObjEPermisoPerfil)
        {
            InitializeComponent();
            this.ObjEPermisoPerfil = ObjEPermisoPerfil;
            
        }

        private void MetroWindow_ContentRendered_1(object sender, System.EventArgs e)
        {
            MyAdministrarListadoCompra.InitializePCMP_ListadoCompra(ObjEPermisoPerfil);
            MyAdministrarListadoCompra.LoadDetail();
        }
    }
}
