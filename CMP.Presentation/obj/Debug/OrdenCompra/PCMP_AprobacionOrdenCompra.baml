   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      N JComputerSystems.WPF, Version=3.0.0.0, Culture=neutral, PublicKeyToken=null�Yclr-namespace:ComputerSystems.WPF.Acciones.Controles.Buttons;assembly=ComputerSystems.WPF.ComputerSystems.WPF.Acciones.Controles.Buttons 5      �Sclr-namespace:ComputerSystems.WPF.Acciones.TitleModule;assembly=ComputerSystems.WPF(ComputerSystems.WPF.Acciones.TitleModule 5       CMP.Presentation=   7CMP.Presentation.OrdenCompra.PCMP_AprobacionOrdenCompra   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6sys&clr-namespace:System;assembly=mscorlib     5      o	CmpButtonYclr-namespace:ComputerSystems.WPF.Acciones.Controles.Buttons;assembly=ComputerSystems.WPF     5      kTitleModuleSclr-namespace:ComputerSystems.WPF.Acciones.TitleModule;assembly=ComputerSystems.WPF     5      H DMahApps.Metro, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null;Controls,http://metro.mahapps.com/winfx/xaml/controls  5	      $	��600��5      $	��900��6   .��5      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   <  6ComputerSystems.WPF.Acciones.TitleModule.MyTitleModule  $��0��6$   $��0��61   $��5q�6A   $��2��6L      
NameModule$ Compra��6a      
NameWindow$! Aprobación de Orden Compra��6u      Logo$0 */CMP.Presentation;component/Source/ima.png��6�   � 5   
   -   5         �Name$ MyHeader��5      $��0��6!   $��1��61   $��3��6>    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $	��180 �6#   $	��180��6/   �� 5      $	��175 �6#   $	��175��6/   .+�5      � $��0��6   $��0��6     5       �� 5!      .+�5#      �� $��0��6   $��0��6*   .+�5$      �� $��Sucursal��6    $��5,0,5,0q�60   $��10(�6A   �� 5%      -   5         ��Name$ txtUsuarioEmpresaSucursal��5%      $��5,0,5,5q�5'       �� 
IsReadOnly .  � ToolTip$ Sucursal��6A   ��5&      �� *" ObjESGC_EmpresaSucursal.Sucursal+	 �� UpdateSourceTrigger$	 PropertyChanged=�5'   A   �� 5(      $��Almacén destino��6    $��5,0,5,0q�67   $��10(�6H   �� 5)      -   5        $ txtAlmacenDestino��5)      $��5,0,5,5q�5+       . $ Almacén Destino��6A   ��5*      �� *ObjEALM_Almacen.Almacen+$	 PropertyChanged=�5+   A   �� 5,      $��	Proveedor��6    $��5,0,5,0q�61   $��10(�6B   �� 5-      -   5        $ txtProveedorRazonSocial��5-      $��5,0,5,5q�5/       . $ Proveedor Razón Social��6A   ��5.      �� *&$ObjEMNF_ClienteProveedor.RazonSocial+$	 PropertyChanged=�5/   A   �� 50      $��Lugar entrega��6    $��5,0,5,0q�65   $��10(�6F   �� 51      -   5        $ txtLugarEntrega��51      $��5,0,5,5q�67    . $ Lugar Entrega��53      ��52      �� *LugarEntrega+$	 PropertyChanged=�53      54      55      �� 57      $��0��6   $��1��6&   .+�58      �� $��Periodo��6   $��5,0,5,0q�6+   $��10(�6<   �� 59      -   5        $ 
txtPeriodo��59      $��5,0,5,5q�6.   
 �� CharacterCasing$
 Upper=�6@   $ Periodo��5:   "   {�.  ��59   X   �� *	Periodo+5:   4   �� 5;      $��Estado��6   $��5,0,5,0q�6*   $��10(�6;   �� 5<      -   5        $ 	txtEstado��5<      $��5,0,5,5q�6-    . $ Estado��5>      ��5=      �� *ObjESGC_Estado.Estado+$	 PropertyChanged=�5>      �� 5?      $��Moneda��6   $��5,0,5,0q�6*   $��10(�6;   �� 5@      -   5        $ 	txtMoneda��5@      $��5,0,5,5q�6-    . $ Moneda��5B      ��5A      �� *ObjESGC_Moneda.Descripcion+$	 PropertyChanged=�5B      �� 5C      $��Fecha orden compra��6   $��5,0,5,0q�66   $��10(�6G   (  "System.Windows.Controls.DatePicker  5D      -	   5          Name$ dtpFechaOrden��5D      $��0��64   $��0��6D   $��5,0,5,5q�6Q   
 �� Now   DisplayDateEnd# Z 6c   {�.  $# Seleccione Fecha Orden Compra��5F         SelectedDate 5E      �� *Fecha+$	 PropertyChanged=�5F      5G      � 5I      $��0��6   $��2��6     5J      �� 5K      $
��Auto �6'   �� 5L      $
��Auto �6'     5N      �� 5O      $
��Auto �6$   �� 5P      $
��Auto �6$   .+�5S      �� $��0��6   $��0��6-   .+�5T      �� $��Serie��6    $��5,0,5,0q�6-   $��10(�6>   �� 5U      -
   5        $ txtSerie��5U      $��5,0,5,5q�60    . $ Serie��5W      ��5V      �� *Serie+$	 PropertyChanged=�5W      5X      �� 5Z      $��1��6   $��0��6.   .+�5[      �� $��	Documento��6    $��5,0,5,0q�61   $��10(�6B   �� 5\      -   5        $ txtCorrelativo��5\      $��5,0,5,5q�66    . $ Nro Documento��5^      ��5]      �� *Numero+$	 PropertyChanged=�5^      5_      �� 5a      $��0��6   $��2��6-   $��1��6A   .+�5b      �� $��
Forma pago��6    $��5,0,5,0q�62   $��10(�6C   �� 5c      -   5        $ txtFormaPago��5c      $��5,0,5,5q�64    . $
 Pago��5e      ��5d      �� *ObjESGC_FormaPago.FormaPago+$	 PropertyChanged=�5e      �� 5f      $��Tipo cambio��6    $��5,0,5,0q�63   $��10(�6D   *  $MahApps.Metro.Controls.NumericUpDown  5g      -   5          Name$ txtSelRateTipoCambio��5g   -   $��5,0,5,5q�6K      
IsReadOnly . $	��165��5h   k      Interval$ 1Q�5i   )      Minimum$ 0Q�66      StringFormat$ ###,###,##0.#00��6C   $ Ingrese Tipo Cambio��6b      Value 5h   )   �� *
TipoCambio+$	 PropertyChanged=�5i   b   �� 5j      $��Fecha entrega��6    $��5,0,5,0q�65   $��10(�6F     5k      -   5        $ dtpFechaEntrega��5k   !   $��5,0,5,5q�6:   {�.  $ Seleccione Fecha Entrega��5n         DisplayDateStart 5l      ��  �� ElementName$ dtpFechaOrden�� �� Path$ SelectedDate� �� Mode$ TwoWay=� 5m      �� *FechaEntrega+$	 PropertyChanged=�5n      5o      5p      5q      � 5s   
   $��0��6   $��2��6    5t      �� 5u      $
��Auto �6#   �� 5v      $��* �6#     5x      �� 5y      $��* �6    �� 5z      $
��Auto �6    .+�5}      � $��0��6   $��2��6    $��0��61   $��0q�6A    5~      �� 5      �� 5�      .+�5�      �� $��1��6   $��0��6*   .+�5�      O  IComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaGuardar  -   5          Name$ 
btnGuardar��5�   ;   $��5q�6O   {�.  6l   M  GComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaSalir  5�      -   5          Name$ btnSalir��5�   9   $��5q�6K   6V   5�      �� 5�      $��1��6   $��1��6*   .+�5�      P  JComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaImprimir  -   5          Name$ btnImprimir��5�   <   $��5q�6Q   {�. 6m   5�      5�      &   System.Windows.Controls.DataGrid  5�      -   5          Name$ dgDetalleArticulo��5�      $��1��62   $��0��6C   $��5q�6P      CmpStandarDataGrid#���   6[      Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,	  &System.Windows.Controls.DataGridColumn 	  Header$
 Item��6)    	  MinWidth$ 50Q�6P     	  Width5
  /System.Windows.Controls.DataGridLengthConverter$  50
 6^   ! 	  
IsReadOnly! . 1  +System.Windows.Controls.DataGridBoundColumn"   Binding" 67   �� *Item+6i     5�      $ 	Artículo��6)   $	 400Q�6i   $	  400
 6x   ! . " 6;   �� *ObjEMNF_Articulo.Articulo+6�     5�      $ Unidad��6)   $	 100Q�6�   $	  100
 6�   ! . " 69   �� *42ObjEMNF_Articulo.ObjEMNF_UnidadMedida.CodUndMedida+6�   4  .System.Windows.Controls.DataGridTemplateColumn  5�      $ Cantidad��6-   $ 70Q�6@   $
  Auto
 6N   ! . #   CellTemplate# 5�      �� 5�      .R�5�   "   �� $ �� TextAlignment$$ Right=�6k   ��6,   �� *
Cantidad+% �� StringFormat$% ###,###,##0.#0000000��6k   5�      5�        5�      $ Precio Unitario��6-   $	 100Q�6G   $
  Auto
 6V   ! . # 5�      �� 5�      .R�5�   "   �� $$ Right=�6q   ��6,   �� *PrecioUnitario+$% ###,###,##0.#0000000��6q   5�      5�        5�      $ Importe��6-   $	 100Q�6?   $
  Auto
 6N   ! . # 5�      �� 5�      .R�5�   "   �� $$ Right=�6j   ��6,   �� *	Importe+$% ###,###,##0.#0000000��6j   5�      5�        5�      $	 IGV��6-   $	 100Q�6;   $
  Auto
 6J   ! . # 5�      �� 5�      .R�5�   "   �� $$ Right=�6m   ��6,   �� *
ImporteIGV+$% ###,###,##0.#0000000��6m   5�      5�        5�      $ Estado��6)   $	 100Q�6c   $	  100
 6r   ! . " 69   �� *ObjESGC_Estado.Estado+6~   5�      � 5�      $��1��6   $��1��6    $��5,5,1,5q�60   $��Right=�6A    5�      �� 5�      $
��Auto �6'   �� 5�      $
��Auto �6'   �� 5�      $
��Auto �6'   �� 5�      $
��Auto �6'   �� 5�      $
��Auto �6'   �� 5�      $
��Auto �6'   .+�5�      �� $��5q�6   $��0��6(   .+�5�      �� -   5        & ��Name$& chkPrecioIncluidoIGV��5�      $��Precio Incluido IGV��6>   $��5,15,5,5q�6\   $��Center=�6n   {�.  6�   5�      �� 5�      
���   �$��5q�60   $��1��6;   .+�5�      �� $��	# Líneas��6    $��3q�60   
�������$��Center=�6O   $��Center=�6j   �� 5�      -   5        ' ��Name$' 	lblLineas��5�   )   $��0��6    
�������$��0q�6O   $
��Bold"�6Z   $��20(�6l   $��Center=�6z   $��Center=�6�   5�      �� 5�      
���   �$��5q�60   $��2��6;   $	��122��6K   .+�5�      �� $��	Exonerado��6    $��3q�61   
�������$��Center=�6P   $
��Left=�6k   �� 5�      -   5        $ lblExonerado��5�      $
��0.00��64   
������� . $��0q�6e   $
��Bold"�6p   $��15(�6�   $��Center=�6�   $��Right=�6�   
���   �$��0q�6�   5�      �� 5�      
���   �$��5q�60   $��3��6;   $	��122��6K   .+�5�      �� $��Gravada��6    $��3q�6/   
�������$��Center=�6N   $
��Left=�6i   �� 5�      -   5        $ 
lblGravada��5�      $
��0.00��62   
������� . $��0q�6c   $
��Bold"�6n   $��15(�6�   $��Center=�6�   $��Right=�6�   
���   �$��0q�6�   5�      �� 5�      
���   �$��5q�60   $��4��6;   $	��122��6K   .+�5�      �� -   5        $' lblTitleOrdenCompra04��5�       $��IGV 18%��6?   $��3q�6N   
�������$��Center=�6m   $
��Left=�6�   �� 5�      -   5        $ lblTotalIgv��5�      $
��0.00��63   
������� . $��0q�6d   $
��Bold"�6o   $��15(�6�   $��Center=�6�   $��Right=�6�   
���   �$��0q�6�   5�      �� 5�      
���   �$��5q�60   $��5��6;   $	��122��6K   .+�5�      �� -   5        $' lblTitleServicio05��5�       $��Total ��6<   $��3q�6J   
�������$��Center=�6i   $
��Left=�6�   �� 5�      -   5        $ lblTotalNeto��5�      $
��0.00��64   
�������$��0q�6T   $
��Bold"�6_   $��15(�6q   $��Center=�6   $��Right=�6�   
���   �$��0q�6�    . 6�   5�      5�      5�      5�      5�      