namespace CMP.Presentation.OrdenCompra
{
    using SGC.Empresarial.Entity;
    using CMP.Entity;
    using ALM.Entity;
    using MNF.Entity;
    using SGC.Empresarial.Useful.Modulo;
    using System.Collections.Generic;
    using ComputerSystems.WPF.Acciones.Controles.Buttons;

    public partial class PCMP_ShowOrdenCompra
    {
        private ESGC_PermisoPerfil ObjEPermisoPerfil;
        private ECMP_OrdenCompra ObjECMP_OrdenCompra;
       
        public PCMP_ShowOrdenCompra(ESGC_PermisoPerfil ObjEPermisoPerfil, ECMP_OrdenCompra ObjECMP_OrdenCompra)
        {
            InitializeComponent();
            this.ObjEPermisoPerfil = ObjEPermisoPerfil;
            this.ObjECMP_OrdenCompra = ObjECMP_OrdenCompra;
        }

        private void MetroWindow_ContentRendered_1(object sender, System.EventArgs e)
        {

            MyAdministrarOrdenCompra.InitializeAdministrarOrdenCompra(ObjEPermisoPerfil, ObjECMP_OrdenCompra);
        }
    }
}
