namespace CMP.Presentation.OrdenServicio
{
    using CMP.Entity;
    using SGC.Empresarial.Entity;
    using System;

    public partial class PCMP_ShowAprobacionOrdenServicio
    {
        #region INSTANCIAS, VARIABLES, EVENTOS AGREGADOS
        private ESGC_PermisoPerfil ObjEPermisoPerfil;
        private ECMP_OrdenServicio ObjECMP_OrdenServicio;
        #endregion

        #region EVENTOS DE LA CLASE

        public PCMP_ShowAprobacionOrdenServicio(ESGC_PermisoPerfil ObjEPermisoPerfil, ECMP_OrdenServicio ObjECMP_OrdenServicio)
        {
            InitializeComponent();
            this.ObjEPermisoPerfil = ObjEPermisoPerfil;
            this.ObjECMP_OrdenServicio = ObjECMP_OrdenServicio;
        }

        private void MetroWindow_ContentRendered_1(object sender, EventArgs e)
        {
            MyPCMP_AprobarOrdenServicio.InitializePCMP_AprobarOrdenServicio(ObjEPermisoPerfil, ObjECMP_OrdenServicio);
        }

        #endregion

        
    }
}
