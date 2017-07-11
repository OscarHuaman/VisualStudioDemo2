namespace CMP.Presentation.OrdenCompra
{
    using CMP.Entity;
    using ComputerSystems.WPF.Acciones.Controles.Buttons;
    using SGC.Empresarial.Entity;
    using System;

    public partial class PCMP_ShowAprobacionOrdenCompra
    {
        #region INSTANCIAS, VARIABLES, EVENTOS AGREGADOS
        private ESGC_PermisoPerfil ObjEPermisoPerfil;
        private ECMP_OrdenCompra ObjECMP_OrdenCompra;

        #endregion

        #region EVENTOS DE LA CLASE

        public PCMP_ShowAprobacionOrdenCompra(ESGC_PermisoPerfil ObjEPermisoPerfil, ECMP_OrdenCompra ObjECMP_OrdenCompra)
        {
            InitializeComponent();
            this.ObjEPermisoPerfil = ObjEPermisoPerfil;
            this.ObjECMP_OrdenCompra = ObjECMP_OrdenCompra;

            this.KeyDownCmpButtonTitleTecla(
                                                                ActionF12: MyPCMP_AprobacionOrdenCompra.btnGuardarIsClicked,
                                                                ActionF9: MyPCMP_AprobacionOrdenCompra.btnImprimirIsClicked,
                                                                ActionESC: MyPCMP_AprobacionOrdenCompra.btnSalirIsClicked);
        
        }


        private void MetroWindow_ContentRendered_1(object sender, EventArgs e)
        {
            MyPCMP_AprobacionOrdenCompra.InitializePCMP_AprobacionOrdenCompra(ObjEPermisoPerfil, ObjECMP_OrdenCompra);
        }

        #endregion

    }
}
