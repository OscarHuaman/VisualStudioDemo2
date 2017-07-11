namespace CMP.Presentation.OrdenServicio
{
    using CMP.Entity;

    using System;

    public partial class PCMP_ShowViewOrdenServicio
    {
        #region INSTANCIAS, VARIABLES, EVENTOS AGREGADOS
        private ECMP_OrdenServicio ObjECMP_OrdenServicio;

        #endregion

        #region EVENTOS DE LA CLASE

        public PCMP_ShowViewOrdenServicio(ECMP_OrdenServicio ObjECMP_OrdenServicio)
        {
            InitializeComponent();
            this.ObjECMP_OrdenServicio = ObjECMP_OrdenServicio;
        }

        private void MetroWindow_ContentRendered_1(object sender, EventArgs e)
        {
            MyPCMP_ViewOrdenServicio.InitializePCMP_ViewOrdenServicio(ObjECMP_OrdenServicio);
        }

        #endregion

    }
}
