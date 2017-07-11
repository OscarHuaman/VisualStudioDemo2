namespace CMP.Presentation.OrdenServicio
{
    using SGC.Empresarial.Entity;
    using CMP.Entity;
    using ALM.Entity;
    using MNF.Entity;
    using System.Collections.Generic;
    using ComputerSystems.WPF.Acciones.Controles.Buttons;

    public partial class PCMP_ShowOrdenServicio
    {
        private ESGC_PermisoPerfil ObjEPermisoPerfil;
        private ECMP_OrdenServicio ObjECMP_OrdenServicio;
        

        public PCMP_ShowOrdenServicio(ESGC_PermisoPerfil ObjEPermisoPerfil, ECMP_OrdenServicio ObjECMP_OrdenServicio)
        {
            InitializeComponent();
            this.ObjEPermisoPerfil = ObjEPermisoPerfil;
            this.ObjECMP_OrdenServicio = ObjECMP_OrdenServicio;
        }
        private void MetroWindow_ContentRendered_1(object sender, System.EventArgs e)
        {
            MyAdministrarOrdenCompra.InitializeAdministrarOrdenServicio(ObjEPermisoPerfil, ObjECMP_OrdenServicio);
        }
    }
}
