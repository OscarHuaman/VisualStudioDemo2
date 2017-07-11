/*********************************************************
'* CREADO POR : COMPUTER SYSTEMS SOLUTION
'*				OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 10/10/2016
**********************************************************/
namespace CMP.Presentation.Compra.Flyouts
{
    using CMP.Business;
    using CMP.Entity;
    using CMP.Useful.Modulo;
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.Buttons;
    using MahApps.Metro.Controls;
    using SGC.Empresarial.Entity;
    using SGC.Empresarial.Method;
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls;
    using System.Linq;
    using CMP.ViewModels.Compra.Flyouts;
    using CMP.ViewModels.Compra.VM;
    using System.Windows.Input;
    using ALM.Business;
    using ALM.Entity;
    
    public partial class PCMP_BuscarAnticipo
    {
        #region INSTANCIAS, VARIABLES, EVENTOS AGREGADOS
        public Action<object> MySelectItem { get; set; }
        private ECMP_Compra ObjECMP_Compra;
        string OpcionAuxiliar = "I";

        /*public delegate void isSelected(ECMP_OrdenCompra ObjECMP_OrdenCompra);
        private isSelected _isSelected;
        public event isSelected IsSelected
        {
            add
            {
                _isSelected = (isSelected)System.Delegate.Combine(_isSelected, value);
            }
            remove
            {
                _isSelected = (isSelected)System.Delegate.Remove(_isSelected, value);
            }
        }*/

        #endregion

        #region EVENTOS 

        public PCMP_BuscarAnticipo()
        {
        }

        public void InitializePCMP_BuscarAnticipo(ECMP_Compra ObjECMP_Compra)
        {
            InitializeComponent();
            this.KeyDownCmpButtonTitleTecla(ActionCtrlD: btnVolverIsClicked);
            MyIsOpenChanged();
            this.ObjECMP_Compra = ObjECMP_Compra;
        }

        public void MyIsOpenChanged()
        {
            this.IsOpenChanged += CmpFlyoutEvents.IsOpenChanged(() =>
            {
                this.IsFocus(false);
                this.txtNumeroReferencia.KeyUp += txtNumeroReferencia_KeyUp_1;
            },
            () =>
            {
                IsSelectedItems();
                this.IsFocus(true);
                this.txtNumeroReferencia.KeyUp -= txtNumeroReferencia_KeyUp_1;
                this.IsOpenChanged -= CmpFlyoutEvents.IsOpenChanged;
            });
        }

        private void txtNumeroReferencia_KeyUp_1(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.B) && OpcionAuxiliar == "I")
            {
                this.FlyoutIsOpen("PCMP_BuscarCompra", new Action<Flyout>((value) =>
                {
                    if (value is PCMP_BuscarCompra)
                    {
                        var MyFlyout = (PCMP_BuscarCompra)value;
                        VCMP_BuscarCompra MyVCMP_BuscarCompra = new VCMP_BuscarCompra();
                        MyVCMP_BuscarCompra.SetKeyDownCmpButtonTitleTecla = MyFlyout;
                        MyVCMP_BuscarCompra.MySelectItem = (item) =>
                        {
                            foreach (var items in item)
                            {
                                AddItem((ECMP_Compra)items, OpcionAuxiliar);
                            }
                            MyFlyout.IsOpen = false;
                        };

                        string CodMoneda = ((ObjECMP_Compra != null) ? ObjECMP_Compra.ObjESGC_Moneda.CodMoneda : "%");
                        string CodDocumento = ((ObjECMP_Compra != null) ? ObjECMP_Compra.CodDocumento : "TODO");
                        switch (CodDocumento)
                        {
                            case "BOL":
                                MyVCMP_BuscarCompra.LoadDetail(ObjECMP_Compra.ObjEMNF_ClienteProveedor.IdCliProveedor, CodMoneda, TipoDocumento.Boleta, TipoBusqueda.Anticipo);
                                break;
                            case "FAC":
                                MyVCMP_BuscarCompra.LoadDetail(ObjECMP_Compra.ObjEMNF_ClienteProveedor.IdCliProveedor, CodMoneda, TipoDocumento.Factura, TipoBusqueda.Anticipo);
                                break;
                            case "TCK":
                                MyVCMP_BuscarCompra.LoadDetail(ObjECMP_Compra.ObjEMNF_ClienteProveedor.IdCliProveedor, CodMoneda, TipoDocumento.Ticket, TipoBusqueda.Anticipo);
                                break;
                            case "RCB":
                                MyVCMP_BuscarCompra.LoadDetail(ObjECMP_Compra.ObjEMNF_ClienteProveedor.IdCliProveedor, CodMoneda, TipoDocumento.Recibo, TipoBusqueda.Anticipo);
                                break;
                            default:
                                MyVCMP_BuscarCompra.LoadDetail(ObjECMP_Compra.ObjEMNF_ClienteProveedor.IdCliProveedor, CodMoneda, TipoDocumento.Todo, TipoBusqueda.Anticipo);
                                break;
                        }

                        MyFlyout.DataContext = MyVCMP_BuscarCompra;
                        MyFlyout.IsOpen = true;
                    }
                }));
            }
        }

        private void dtgDetalleColumnEliminar_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            if(OpcionAuxiliar == "I")
                CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, CMPMensajes.QuitarItems, CmpButton.AceptarCancelar, () =>
                {
                    var vrObjECMP_Compra = (ECMP_Compra)dtgCompra.SelectedItem;
                    if (vrObjECMP_Compra != null)
                    {
                        dtgCompra.Items.Remove(vrObjECMP_Compra);
                    }
                });
        }

        private void btnVolverIsClicked()
        {
            IsOpen = false;
        }

        private void IsSelectedItems()
        {
            if (dtgCompra.Items.Count == 0 || OpcionAuxiliar == "U") { return; }

            var vrObjListECMP_Compra = (dtgCompra.Items.OfType<ECMP_Compra>()).ToList();
            foreach (var item in vrObjListECMP_Compra)
            {
                if (MySelectItem != null)
                    MySelectItem.Invoke(item);
            }
        }

        #endregion

        #region MÉTODOS

        public void AddItem(ECMP_Compra item, string Opcion)
        {
            var vrListECMP_Compra = (dtgCompra.Items.OfType<ECMP_Compra>()).ToList();
            bool existeArticulo = vrListECMP_Compra.Exists(x => x.IdCompra == item.IdCompra);
            if (existeArticulo)
            {
                if (Opcion == "I")
                    CmpMessageBox.Show(CMPMensajes.TitleAdminCompra, "El item seleccionado ya existe", CmpButton.Aceptar);
                return;
            }
            dtgCompra.Items.Add(item);
            dtgCompra.Items.Refresh();
            lblCountItems.Text = dtgCompra.Items.Count + " Registros";
        }

        public void LoadDetail(ECMP_Compra vrobjECMP_Compra)
        {
            OpcionAuxiliar = "U";
            var objECMP_BuscarAnticipo = new BCMP_Compra().ListAdministrarCompraAnticipo(vrobjECMP_Compra).ToList();
            List<ECMP_Compra> objECMP_CompraAnticipo = new List<ECMP_Compra>();
            foreach (var item in objECMP_BuscarAnticipo)
            {
                AddItem(item, OpcionAuxiliar);
            }
        }

       #endregion
    }
}