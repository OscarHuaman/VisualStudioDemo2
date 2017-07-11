namespace CMP.Presentation.Compra
{
    using CMP.Entity;
    using SGC.Empresarial.Entity;

    public partial class PCMP_ShowCompra
    {
        private ESGC_PermisoPerfil ObjEPermisoPerfil;
        private ECMP_Compra ObjECMP_Compra;
        

        public PCMP_ShowCompra(ESGC_PermisoPerfil ObjEPermisoPerfil, ECMP_Compra ObjECMP_Compra)
        {
            InitializeComponent();
            this.ObjEPermisoPerfil = ObjEPermisoPerfil;
            this.ObjECMP_Compra = ObjECMP_Compra;
        }

        private void MetroWindow_ContentRendered_1(object sender, System.EventArgs e)
        {
            //MyAdministrarCompra.InitializeAdministrarCompra(ObjEPermisoPerfil, ObjECMP_Compra);
        }
    }
}
