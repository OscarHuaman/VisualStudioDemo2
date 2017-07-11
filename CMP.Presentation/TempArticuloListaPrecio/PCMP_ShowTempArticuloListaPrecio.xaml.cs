namespace CMP.Presentation.TempArticuloListaPrecio
{

    using SGC.Empresarial.Entity;
    using System;

    public partial class PCMP_ShowTempArticuloListaPrecio 
    {
        private ESGC_PermisoPerfil ObjEPermisoPerfil;
        

        public PCMP_ShowTempArticuloListaPrecio(ESGC_PermisoPerfil ObjEPermisoPerfil)
        {
            InitializeComponent();
            this.ObjEPermisoPerfil = ObjEPermisoPerfil;
        }
        
        private void MetroWindow_ContentRendered_1(object sender, EventArgs e)
        {
            MyTempListaPrecio.InitializePCMP_TempArticuloListaPrecio(ObjEPermisoPerfil);
        }

    }
}
