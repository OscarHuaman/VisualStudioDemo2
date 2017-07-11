namespace CMP.Presentation.OrdenServicio.Flyouts
{
    using CMP.Entity;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.Buttons;

    public partial class Obervaciones
    {
        #region EVENTOS 

        public Obervaciones()
        {
        }

        public void InitializeObervaciones(ECMP_OrdenServicioDetalle ObjECMP_OrdenServicioDetalle)
        {
            InitializeComponent();
            this.DataContext = ObjECMP_OrdenServicioDetalle;
            this.KeyDownCmpButtonTitleTecla(
                                                                ActionCtrlD: btnVolverIsClicked);
            MyIsOpenChanged();
        }

        public void MyIsOpenChanged()
        {
            this.IsOpenChanged += CmpFlyoutEvents.IsOpenChanged(() =>
            {
                this.IsFocus(false);
            },
            () =>
            {
                this.IsFocus(true);
                this.IsOpenChanged -= CmpFlyoutEvents.IsOpenChanged;
            });
        }

        private void btnVolverIsClicked()
        {
            IsOpen = false;
        }
        #endregion

    }
}
