   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      N JComputerSystems.WPF, Version=3.0.0.0, Culture=neutral, PublicKeyToken=null�Yclr-namespace:ComputerSystems.WPF.Acciones.Controles.Buttons;assembly=ComputerSystems.WPF.ComputerSystems.WPF.Acciones.Controles.Buttons 5      �Sclr-namespace:ComputerSystems.WPF.Acciones.TitleModule;assembly=ComputerSystems.WPF(ComputerSystems.WPF.Acciones.TitleModule 5      �Zclr-namespace:ComputerSystems.WPF.Acciones.Controles.TextBoxs;assembly=ComputerSystems.WPF/ComputerSystems.WPF.Acciones.Controles.TextBoxs 5	      I ECMP.ViewModels, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:CMP.ViewModels.ConvertsCMP.ViewModels.Converts 5
      D   >CMP.ViewModels.ReporteStockMinimo.Page.PCMP_ListadoStockMinimo   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6sys&clr-namespace:System;assembly=mscorlib     5      o	CmpButtonYclr-namespace:ComputerSystems.WPF.Acciones.Controles.Buttons;assembly=ComputerSystems.WPF     5      kTitleModuleSclr-namespace:ComputerSystems.WPF.Acciones.TitleModule;assembly=ComputerSystems.WPF     5      q
CmpTextBoxZclr-namespace:ComputerSystems.WPF.Acciones.Controles.TextBoxs;assembly=ComputerSystems.WPF     5	      5cvr%clr-namespace:CMP.ViewModels.Converts     5
      H DMahApps.Metro, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null;Controls,http://metro.mahapps.com/winfx/xaml/controls  5        N� Title$  PCMP_ListadoStockMinimo��5      .��5      � .+�5   
   �  � 	Resources 5      8  2CMP.ViewModels.Converts.ConvertToEnablebFromLength   
FromLength��%   &	          5      61   5       � ColumnDefinitions 5      �� 5      $
��auto �6#   �� 5      $��* �6#    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $��* �6    �� 5      $
��Auto �6    .+�5!      <  6ComputerSystems.WPF.Acciones.TitleModule.MyTitleModule  $��0��6(   $��0��65   $��5q�6E   $��2��6P      
NameModule$ Compra��6e      
NameWindow$, &Listado de Stock Mínimo de Artículos��6y      Logo$. (/CMP.ViewModels;component/Source/ima.png��6�   � 5"      $��1��6   $��1��6#   $��5q�60    5#      �� 5$      $��* �6'   �� 5%      $��* �6'   �� 5&      $��* �6'   �� 5'      $��* �6'    5)      �� 5*      $
��Auto �6$   �� 5+      $
��Auto �6$   .+�5-      �� $��0��6   .+�5.      �� $��Sucursal��6    $��11(�60   $��5,0,5,0q�6>   �� 5/      -   5         ��Name$ cbxSucursal��5/      $��5,0,5,5q�64    �� 
IsEditable . 	 �� MaxDropDownHeight$		 125��50   1     AccentColorBrush+  %MahApps.Metro.Controls.ControlsHelper
   FocusBorderBrush#
 �  51      F  @ComputerSystems.WPF.Acciones.Controles.TextBoxs.FocusAdvancement   AdvancesByEnterKey .  �� DisplayMemberPath$&  ObjESGC_EmpresaSucursal.Sucursal��55       �� SelectedValuePath$+ %ObjESGC_EmpresaSucursal.IdEmpSucursal��6T   ��53      �� *!ListESGC_UsuarioEmpresaSucursal+ �� SelectedItem 54      �� *SelectUsuarioEmpresaSucursal+55   T   56      �� 57      $��1��6   .+�58      �� $��Almacén��6    $��11(�6/   $��5,0,5,0q�6=   �� 59      -   5        $ 
cbxAlmacen��59      $��5,0,5,5q�63    . $		 125��5:   1   #
 �  5;       . $ Almacen��5>      $ 	IdAlmacen��6;   ��5:   I   �� *ListEALM_Almacen+ 5=      �� *SelectAlmacen+5>   ;   5?      �� 5@      $��2��6   .+�5A      �� $��Clase��6    $��11(�6-   $��5,0,5,0q�6;   �� 5B      -   5        $ cxbArticuloClase��5B      $��5,0,5,5q�69    . $		 125��5C   1   #
 �  5D       . $ Clase��5H      $ 
IdArtClase��69   ��5F      �� *$"DataContext.ListEMNF_ArticuloClase+ �� RelativeSource �� *FindAncestor+ �� AncestorType$
 PageL� 5G      �� *SelectArticuloClase+5H   9   5I      �� 5J      $��3��6   .+�5K      �� $��Marca��6    $��11(�6-   $��5,0,5,0q�6;   �� 5L      -   5        $ cbxArticuloMarca��5L      $��5,0,5,5q�69    . $		 125��5M   1    . #
 �  5O      *  $MahApps.Metro.Controls.TextBoxHelper   	Watermark$ Selecione Marca��5Q      $ Marca��5S      $ IdMarca��69   ��5P      �� *$"DataContext.ListEMNF_ArticuloMarca+ �� *FindAncestor+$
 PageL� 5R      �� *SelectArticuloMarca+5S   9   5T      �� 5U      $��0��6   $��4��6-   $��1��6A   .+�5V      �� $��	Artículo��6    $��10(�60   $��5,0,5,0q�6>   �� 5W      -   5         ��Name$ 
txtAriculo��5W      $��5,0,5,5q�63   $	��140��6D   $��0��6T    .  �� CharacterCasing$ Upper=�5X   @   #
 �  5Y      ��5Z      �� *64ObjECMP_ReporteStockMinimo.ObjEMNF_Articulo.Articulo+ �� UpdateSourceTrigger$ PropertyChanged=�   ButtonCommand 5[      �� *DataContext.IBuscarProducto+ �� *FindAncestor+$
 PageL�   ButtonCommandParameter 5\      ��  �� ElementName$ 
txtAriculo�� �� Path$
 Text�$ PropertyChanged=� �� Mode$ TwoWay=�.��6�    J� InputBindings 5]      �� 5^      
 �� Key$ B��6�    �� 	Modifiers$ Controlf�6�    �� Command 6)   �� *DataContext.IBuscarProducto+ �� *FindAncestor+$
 PageL� �� CommandParameter 6�   �� $ 
txtAriculo��$
 Text�6�   .��5_   1   5`      5a      5b      � 5f      $��0��6   $��3��6#   $	��100��60    5g      �� 5h      .+�5j      �� $��0��6   $��0��6-   .+�5k      P  JComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaImprimir  -   5          Name$ btnImprimir��5k   <   $��5q�6Q       	IsCommand  6\   �� *	IImprimir+U  OComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaExportarExcel  5l      $��5q�6B   !   	IsCommand! 6N   �� *	IExportar+M  GComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaSalir  5m      -   5        "  Name$" btnSalir��5m   9   $��5q�6K   #   	IsCommand# 6V   �� *IClose+5n      5o      &	   System.Windows.Controls.DataGrid	  5q      -   5        $ 	 Name$$ dtgListaPrecio��5q      $��3��60   $	��200��6=   $��5q�6M   $��1��6X   ��6h   �� *ListECMP_ReporteStockMinimo+% 	  Columns% 5r      0
  *System.Windows.Controls.DataGridTextColumn
  5s      ,  &System.Windows.Controls.DataGridColumn&   Header$& Sub-Categoría��6)   '   MinWidth$	' 100Q�6b   (   Width5  /System.Windows.Controls.DataGridLengthConverter$
( Auto 6q   )   
IsReadOnly) . 1  +System.Windows.Controls.DataGridBoundColumn*   Binding* 6@   �� *SubCategoria+6~   
  5t      $& Marca��6)   $	' 100Q�6i   $
( Auto 6x   ) . * 68   �� *ObjEMNF_ArticuloMarca.Marca+6�   
  5u      $& Und. Medida��6)   $' 90Q�6�   $( 90 6�   ) . * 6>   �� *42ObjEMNF_Articulo.ObjEMNF_UnidadMedida.CodUndMedida+6�   
  5v      $& Código��6)   $' 90Q�6f   $( 90 6t   ) . * 69   �� *ObjEMNF_Articulo.Codigo+6   
  5w      $& 	Artículo��6)   $	' 150Q�6j   $
( Auto 6y   ) . * 6;   �� *ObjEMNF_Articulo.Articulo+6�   
  5x      $& Sucursal��6)   $	' 150Q�6�   $
( Auto 6�   ) . * 6;   �� *20ObjEALM_Almacen.ObjESGC_EmpresaSucursal.Sucursal+6�   
  5y      $& Almacén��6)   $	' 150Q�6g   $
( Auto 6v   ) . * 6:   �� *ObjEALM_Almacen.Almacen+6�   4  .System.Windows.Controls.DataGridTemplateColumn  5{      $& Stock Mínimo��6-   $	' 100Q�6D   $
( Auto 6S   ) . +   CellTemplate+ 5|      �� 5}      .R�5~   "   �� , �� TextAlignment$, Right=�6J   ��6-   �� *StockMinimo+6J   5      5�        5�      $& Stock Actual��6-   $	' 100Q�6D   $
( Auto 6S   ) . + 5�      �� 5�      .R�5�   "   �� $, Right=�6J   ��6-   �� *StockActual+6J   5�      5�      5�      � 5�      $��4��6   $��1��6    .+�5�      �� - �� Orientation$- 
Horizontal=�6   $��5q�66   .+�5�      �� $	��5,0q�6!   ��6.   �� *Items.Count+$ dtgListaPrecio���� 5�      $��	Registros��6    5�      5�      5�      5�      5�      