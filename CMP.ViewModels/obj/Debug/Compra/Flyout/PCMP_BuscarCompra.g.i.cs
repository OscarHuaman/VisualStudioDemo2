﻿#pragma checksum "..\..\..\..\Compra\Flyout\PCMP_BuscarCompra.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "16AE0DEAF61667F1DDCA3963BCAD2BC0"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using CMP.ViewModels.Converts;
using ComputerSystems.WPF.Acciones.Controles;
using ComputerSystems.WPF.Acciones.Controles.Buttons;
using ComputerSystems.WPF.Acciones.Controles.TextBoxs;
using MahApps.Metro.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace CMP.ViewModels.Compra.Flyouts {
    
    
    /// <summary>
    /// PCMP_BuscarCompra
    /// </summary>
    public partial class PCMP_BuscarCompra : MahApps.Metro.Controls.Flyout, System.Windows.Markup.IComponentConnector {
        
        
        #line 36 "..\..\..\..\Compra\Flyout\PCMP_BuscarCompra.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ComputerSystems.WPF.Acciones.Controles.DataGrid dtgCompra;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\..\..\Compra\Flyout\PCMP_BuscarCompra.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtFiltrar;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\..\..\Compra\Flyout\PCMP_BuscarCompra.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtMonto;
        
        #line default
        #line hidden
        
        
        #line 124 "..\..\..\..\Compra\Flyout\PCMP_BuscarCompra.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ComputerSystems.WPF.Acciones.Controles.DataGrid dtgCompraSeleccion;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CMP.ViewModels;component/compra/flyout/pcmp_buscarcompra.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Compra\Flyout\PCMP_BuscarCompra.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.dtgCompra = ((ComputerSystems.WPF.Acciones.Controles.DataGrid)(target));
            return;
            case 2:
            this.txtFiltrar = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtMonto = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.dtgCompraSeleccion = ((ComputerSystems.WPF.Acciones.Controles.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

