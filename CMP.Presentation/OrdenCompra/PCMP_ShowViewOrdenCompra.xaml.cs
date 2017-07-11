namespace CMP.Presentation.OrdenCompra
{
    using CMP.Entity;
    using ComputerSystems.WPF.Acciones.Controles.Buttons;
    using System;

    public partial class PCMP_ShowViewOrdenCompra
    {
        #region INSTANCIAS, VARIABLES, EVENTOS AGREGADOS
        private ECMP_OrdenCompra ObjECMP_OrdenCompra;

        #endregion

        #region EVENTOS DE LA CLASE

        public PCMP_ShowViewOrdenCompra(ECMP_OrdenCompra ObjECMP_OrdenCompra)
        {
            InitializeComponent();

            this.ObjECMP_OrdenCompra = ObjECMP_OrdenCompra;
        }

        private void MetroWindow_ContentRendered_1(object sender, EventArgs e)
        {
            MyPCMP_ViewOrdenCompra.InitializePCMP_ViewOrdenCompra(ObjECMP_OrdenCompra);
            MyPCMP_ViewOrdenCompra.LoadDetail();
        }

        #endregion

    }
}
