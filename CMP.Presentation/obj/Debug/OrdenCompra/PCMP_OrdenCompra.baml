   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      N JComputerSystems.WPF, Version=3.0.0.0, Culture=neutral, PublicKeyToken=null�Yclr-namespace:ComputerSystems.WPF.Acciones.Controles.Buttons;assembly=ComputerSystems.WPF.ComputerSystems.WPF.Acciones.Controles.Buttons 5      �Sclr-namespace:ComputerSystems.WPF.Acciones.TitleModule;assembly=ComputerSystems.WPF(ComputerSystems.WPF.Acciones.TitleModule 5      �Zclr-namespace:ComputerSystems.WPF.Acciones.Controles.TextBoxs;assembly=ComputerSystems.WPF/ComputerSystems.WPF.Acciones.Controles.TextBoxs 5	      �[clr-namespace:ComputerSystems.WPF.Acciones.Controles.DataGrids;assembly=ComputerSystems.WPF0ComputerSystems.WPF.Acciones.Controles.DataGrids 5
      |Qclr-namespace:ComputerSystems.WPF.Acciones.Controles;assembly=ComputerSystems.WPF&ComputerSystems.WPF.Acciones.Controles 5       CMP.Presentation3   -CMP.Presentation.OrdenCompra.PCMP_OrdenCompra   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6sys&clr-namespace:System;assembly=mscorlib     5      o	CmpButtonYclr-namespace:ComputerSystems.WPF.Acciones.Controles.Buttons;assembly=ComputerSystems.WPF     5      kTitleModuleSclr-namespace:ComputerSystems.WPF.Acciones.TitleModule;assembly=ComputerSystems.WPF     5      q
CmpTextBoxZclr-namespace:ComputerSystems.WPF.Acciones.Controles.TextBoxs;assembly=ComputerSystems.WPF     5	      sCmpDataGrid[clr-namespace:ComputerSystems.WPF.Acciones.Controles.DataGrids;assembly=ComputerSystems.WPF     5
      iCmpControlsQclr-namespace:ComputerSystems.WPF.Acciones.Controles;assembly=ComputerSystems.WPF     5      H DMahApps.Metro, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null;Controls,http://metro.mahapps.com/winfx/xaml/controls  5      .��5      �   � 	Resources  5   
   P  JComputerSystems.WPF.Acciones.Controles.TextBoxs.ConvertTextBoxTrimSpaceInt   ConvertTextBoxTrimSpaceInt��%   &	          5      64   5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   <  6ComputerSystems.WPF.Acciones.TitleModule.MyTitleModule  $��0��6$   $��0��61   $��5q�6A   $��2��6L      
NameModule$ Compra��6a      
NameWindow$ Administrar Orden Compra��6u      Logo$0 */CMP.Presentation;component/Source/ima.png��6�   � 5   
   -   5         �Name$ MyHeader��5      $��0��6!   $��1��61   $��3��6>    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5       $	��180 �6#   $	��180��6/   �� 5!      $	��175 �6#   $	��175��6/   .+�5$      � $��0��6   $��0��6     5%      �� 5&      .+�5(      �� $��0��6   $��0��6*   .+�5)      �� $��Sucursal��6    $��5,0,5,0q�60   $��10(�6A   �� 5*      -   5         ��Name$ cbxUsuarioEmpresaSucursal��5*      $��5,0,5,5q�6B    �� 
IsEditable . 	 �� MaxDropDownHeight$		 125��5+   1     AccentColorBrush+  %MahApps.Metro.Controls.ControlsHelper
   FocusBorderBrush#
 �  5,       � ToolTip$ Seleccione Sucursal��5-      F  @ComputerSystems.WPF.Acciones.Controles.TextBoxs.FocusAdvancement   AdvancesByEnterKey .  �� DisplayMemberPath$&  ObjESGC_EmpresaSucursal.Sucursal��50       �� SelectedValuePath$+ %ObjESGC_EmpresaSucursal.IdEmpSucursal��6T    �� SelectedValue 5/      �� *'%ObjESGC_EmpresaSucursal.IdEmpSucursal+ �� UpdateSourceTrigger$ PropertyChanged=�51      �� 52      $��Almacén destino��6    $��5,0,5,0q�67   $��10(�6H   �� 53      -   5        $ cbxAlmacenDestino��53      $��5,0,5,5q�6:    . $		 125��54   1   #
 �  55      $! Seleccione Almacén Destino��56       . {�.  $ Almacen��5:      $ 	IdAlmacen��6;    59      �� *ObjEALM_Almacen.IdAlmacen+$ PropertyChanged=�5;      �� 5<      $��Proveedor [Control + B]��6    $��5,0,5,0q�6?   $��10(�6P   �� 5=      -   5         ��Name$ txtProveedorRazonSocial��5=      $��5,0,5,5q�6?   #
 �  5>       .  �� CharacterCasing$ Upper=�5A   K   $r lRazón Social del Proveedor | Filtrar por [ Nro Documento | Razón Social | Contacto | Representante Legal ]��5B      ��5@      �� *&$ObjEMNF_ClienteProveedor.RazonSocial+$ PropertyChanged=�5B      �� 5C      $��Lugar entrega��6    $��5,0,5,0q�65   $��10(�6F   �� 5D      -   5        $ txtLugarEntrega��5D      $��5,0,5,5q�67   #
 �  5E       . $ Lugar Entrega��5H      ��5F      �� *LugarEntrega+$ PropertyChanged=�5H      5I      5J      �� 5L      $��0��6   $��1��6&   .+�5M      �� $��Periodo��6   $��5,0,5,0q�6+   $��10(�6<   �� 5N      -   5        $ 
cbxPeriodo��5N      $��5,0,5,5q�6/    . $ Periodo��5Q      $ Periodo��65    5O      �� *	Periodo+ �� Mode$ TwoWay=�$ PropertyChanged=�5Q   Q   �� 5S      $��Estado��6   $��5,0,5,0q�6*   $��10(�6;   �� 5T      -   5        $ 	cbxEstado��5T      {�.   . $		 125��5U   -   #
 �  5V       . $��2��5X      $��1��6(   $��2��68   $��5,0,5,5q�6L   $ Seleccione Estado��5Z      $ Estado��5[      $ 	CodEstado��66    5Y      �� *ObjESGC_Estado.CodEstado+$ PropertyChanged=�5\      �� 5^      $��Moneda��6   $��5,0,5,0q�6*   $��10(�6;   �� 5_      -   5        $ 	cbxMoneda��5_      $��1��5`      $��1��6(   $��2��68   $��5,0,5,5q�6L    . $		 125��5a   -   #
 �  5b       . $ Seleccione Moneda��5d      $ Descripcion��5f      $ 	CodMoneda��6<    5e      �� *ObjESGC_Moneda.CodMoneda+$ PropertyChanged=�5g      �� 5i      $��Fecha orden compra��6   $��5,0,5,0q�66   $��10(�6G   (  "System.Windows.Controls.DatePicker  5j      -	   5          Name$ dtpFechaOrden��5j      $��0��64   $��0��6D   $��5,0,5,5q�6Q   #
 �  5k       . $# Seleccione Fecha Orden Compra��5o         SelectedDate 5m      �� *Fecha+$ PropertyChanged=�5o      5p      � 5r      $��0��6   $��2��6     5s      �� 5t      $
��Auto �6'   �� 5u      $
��Auto �6'    5w      �� 5x      $
��Auto �6$   �� 5y      $
��Auto �6$   .+�5|      �� $��0��6   $��0��6-   .+�5}      �� $��Serie��6    $��5,0,5,0q�6-   $��10(�6>   �� 5~      -
   5        $ txtSerie��5~      $��5,0,5,5q�60   {�.  E  ?ComputerSystems.WPF.Acciones.Controles.TextBoxs.CmpMaskBehavior   Mask$	 Any=�5       . #
 �  5�      $ Serie��5�      ��5�      �� *Serie+$ PropertyChanged=�5�      5�      �� 5�      $��1��6   $��0��6.   .+�5�      �� $��	Documento��6    $��5,0,5,0q�61   $��10(�6B   �� 5�      -   5        $ txtCorrelativo��5�      $��5,0,5,5q�66   {�.  $	 Any=�5�       . #
 �  5�      $ Nro Documento��5�      ��5�      �� *Numero+$ PropertyChanged=� �� 	Converter# [  5�      5�      �� 5�      $��1��6   $��0��6*   $��2��6:   .+�5�      �� $��
Forma pago��6    $��5,0,5,0q�62   $��10(�6C   �� 5�      -   5        $ cbxFormaPago��5�      $	��163��65   $��5,0,5,5q�6A    . $		 125��5�   -   #
 �  5�      $ Seleccione F. Pago��5�       . $ 	FormaPago��5�      $ IdFormaPago��69    5�      �� *ObjESGC_FormaPago.IdFormaPago+$ PropertyChanged=�5�      �� 5�      $��Tipo cambio��6    $��5,0,5,0q�63   $��10(�6D   *  $MahApps.Metro.Controls.NumericUpDown  5�      -   5          Name$ txtSelRateTipoCambio��5�   -   $��5,0,5,5q�6K   #
 �  5�   -   $	��165��5�   o    .    Interval$ 1Q�5�   -      Minimum$ 0Q�6:      StringFormat$ ###,###,##0.#00��6G   $ Ingrese Tipo Cambio��6f      Value 5�   -   �� *
TipoCambio+$ PropertyChanged=�5�   f   �� 5�      $��Fecha entrega��6    $��5,0,5,0q�65   $��10(�6F     5�      -   5        $ dtpFechaEntrega��5�   !   $��5,0,5,5q�6:   #
 �  5�   !    . $ Seleccione Fecha Entrega��5�   !      DisplayDateStart 5�   !   ��  �� ElementName$ dtpFechaOrden�� �� Path$ SelectedDate�$ TwoWay=� 5�   !   �� *FechaEntrega+$ PropertyChanged=�5�   !   5�      5�      5�      � 5�   
   $��0��6   $��2��6    5�      �� 5�      $
��Auto �6#   �� 5�      $��* �6#    5�      �� 5�      $��* �6    �� 5�      $
��Auto �6    .+�5�      � $��0��6   $��2��6    $��0��61   $��0q�6A    5�      �� 5�      �� 5�      .+�5�      �� $��1��6   $��0��6*   .+�5�      O  IComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaAddItem  -   5           Name$  btnAgregarArticulo��5�   ;   $��5q�6W   !   ContentTitle$! Agregar Artículo��6b   6�   P	  JComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaCancelar	  5�      -   5        " 	 Name$" btnAnularProceso��5�   <   $��5q�6V   6a   M
  GComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaSalir
  5�      -   5        # 
 Name$# btnSalir��5�   9   $��5q�6K   6V   5�      �� 5�      $��1��6   $��1��6*   .+�5�      R  LComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaDeleteItem  -   5        $  Name$$ btnQuitarArticulo��5�   >   $��5q�6Y   %   ContentTitle$% Quitar Artículo��6d   {�.  6�   O  IComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaGuardar  5�      -   5        &  Name$& 
btnGuardar��5�   ;   $��5q�6O   {�.  6l   P  JComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaImprimir  5�      -   5        '  Name$' btnImprimir��5�   <   $��5q�6Q   {�.  6n   5�      5�      &   System.Windows.Controls.DataGrid  5�      -   5        (  Name$( dgDetalleArticulo��5�      $��1��62   $��0��6C   $��5q�6P     CmpStandarDataGrid#���  6[   )   SelectionUnit$
) Cell=�5�      G  AComputerSystems.WPF.Acciones.Controles.DataGrids.CmpGridViewFocus*   IsMoveFocus* . +   Columns+ 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn,   Header$
, Item��6)   -   MinWidth$- 50Q�6P   .   Width5  /System.Windows.Controls.DataGridLengthConverter$. 50 6^   /   
IsReadOnly/ . 1  +System.Windows.Controls.DataGridBoundColumn0   Binding0 67   �� *Item+6i     5�      $, Código��6)   $- 80Q�6e   $. 80 6s   / . 0 69   �� *ObjEMNF_Articulo.Codigo+6~     5�      $, 	Artículo��6)   $	- 100Q�6i   $	. 200 6x   / . 0 6;   �� *ObjEMNF_Articulo.Articulo+6�     5�      $, Unidad��6)   $- 60Q�6�   $. 60 6�   / . 0 69   �� *42ObjEMNF_Articulo.ObjEMNF_UnidadMedida.CodUndMedida+6�   4  .System.Windows.Controls.DataGridTemplateColumn  5�      -   5        #dgDetalleArticuloColumnCantidad��$, Cantidad��5�   V   $	- 100Q�6h   $
. Auto 6w   1   CellTemplate1 5�      �� 5�      .R�5�   "   �� $ npdCantidad��6*   -   5        2 �� TextAlignment$2 Right=�5�   ?   $ Decimal=�5�   *   ��5�   U   �� *
Cantidad+$ PropertyChanged=�3 �� StringFormat$3 ###,###,##0.#0000000��5�   *   5�      5�        5�      -   5        )%dgDetalleArticuloColumnPrecioUnitario��$, Precio Unitario��5�   \   $	- 100Q�6u   $
. Auto 6�   1 5�      �� 5�      .R�5�   "   �� $ npdPrecioUnitario��6*   -   5        $ Decimal=�5�   *   $2 Right=�6U   ��5�   E   �� *PrecioUnitario+$ PropertyChanged=�$3 ###,###,##0.#0000000��5�   *   5�      5�        5�      $, Importe��6   $	- 100Q�60   $
. Auto 6?   / . 1 5�      �� 5�      .R�5�   "   �� 4 �� TextAlignment$4 Right=�6j   ��6,   �� *	Importe+$3 ###,###,##0.#0000000��6j   5�      5�        5�      $	, IGV��6-   $- 80Q�6;   $
. Auto 6I   / . 1 5�      �� 5�      .R�5�   "   �� $4 Right=�6m   ��6,   �� *
ImporteIGV+$3 ###,###,##0.#0000000��6m   5�      5�        5�      -   5        !dgDetalleArticuloColumnEstado��$, Estado��5�   T   $	- 100Q�6e   $	. 100 6t   1 5�      �� 5�      .R�5�   "   �� 5 �� SelectedIndex$5 0��6�   $ 	CodEstado��6�   $ Estado��6�    6+   �� $ ObjESGC_Estado.CodEstado�$ PropertyChanged=�{�6�   �� *IsEnableEstado+��6�   �� $ 
ListEstado�5�      5�      5�      � 5      $��1��6   $��1��6    $��5,5,5,5q�60   $��Right=�6A     5     6 �� 
TargetType7 �� Property8 �� Value9 �� 
TargetType: J� SnapsToDevicePixels; �� Triggers< P� Property= P� Value��%b  '��       �� 5     "6 ��6   .�5     �� 7 � ��
8 �����67   �� 5     7 � ��
8 �   �68   �� 5     7 � ��8 5     �� 5  "   "9 ��62   .R�5	  &   �� $��Bd��6-   #��z��67   #��z��5
  -   #��z��5  -   : . ._�5  *   �� $��PART_ContentHost��67   #��z��6O   #: z: 5  /   5  '   ; 5  &   P� 5  *   < � {�= .  . �5  .   �� 7 � ��
8 �   �6C   �� 5  .   7 � ��
8 �����6C   5  +   P� 5  *   < � {�= . . �5  .   �� 7 � ��
8 �   �6C   �� 5  .   7 � ��
8 �����6C   5  +   5  #   5     5     5      5      �� 5!     $
��Auto �6'   �� 5"     $
��Auto �6'   �� 5#     $
��Auto �6'   �� 5$     $
��Auto �6'   �� 5%     $
��Auto �6'   �� 5&     $
��Auto �6'   �� 5'     $
��Auto �6'   .+�5*     �� -   5        > ��Name$> chkPrecioIncluidoIGV��5*     $��0��69   $��1��6F   $��Center=�6V   $��Center=�6q   $��Precio Incluido IGV��5+     $��5q�69   6D   �� 5,     
���   �$��5q�60   $��2��6;   .+�5-     �� $��	# Líneas��6    $��3q�60   
�������$��Center=�6O   $
��Left=�6j   �� 5.     -   5        $ 	lblLineas��5.  '   $��0��6   
�������$��0q�6M   $
��Bold"�6X   $��15(�6j   $��0,1,0,0q�6x   
���   �? �� 
IsReadOnly? . $2 Center=�6�   5/     �� 50     
���   �$��5q�60   $��3��6;   $	��110��6K   .+�51     �� $��	Exonerado��6    $��3q�61   
�������$��Center=�6P   $
��Left=�6k   �� 52     -   5        $ lblExonerado��52     $
��0.00��64   
�������$��0q�6S   $
��Bold"�6^   $��15(�6p   $��0,1,0,0q�6~   
���   �? . $2 Right=�6�   53     �� 54     
���   �$��5q�60   $��4��6;   $	��110��6K   .+�55     �� -   5        @ ��Name$@ lblTitleOrdenCompra03��55      $��Gravada��6?   $��3q�6N   
�������$��Center=�6m   $
��Left=�6�   �� 56     -   5        $ 
txtGravada��56     $
��0.00��62   
�������$��0q�6Q   $
��Bold"�6\   $��15(�6n   $��Center=�6|   $��Right=�6�   
���   �$��0q�6�   $2 Right=�6�   ? . 57  ~   58     �� 59     
���   �$��5q�60   $��5��6;   $	��110��6K   .+�5:     �� -    5        $@ lblTitleOrdenCompra04��5:      $��IGV 18%��6?   $��3q�6N   
�������$��Center=�6m   $
��Left=�6�   �� 5;     -!   5        $ txtTotalIgv��5;     $
��0.00��63   
�������$��0q�6R   $
��Bold"�6]   $��15(�6o   $��Center=�6}   $��Right=�6�   
���   �$��0q�6�   $2 Right=�6�   ? . 5<  ~   5=     �� 5>     
���   �$��5q�60   $��6��6;   $	��110��6K   .+�5?     �� -"   5        $@ lblTitleOrdenCompra05��5?      $��Total ��6?   $��3q�6M   
�������$��Center=�6l   $
��Left=�6�   �� 5@     -#   5        $ txtTotalNeto��5@     $
��0.00��64   
�������$��0q�6T   $
��Bold"�6_   $��15(�6q   
���   �$��0,1,0,0q�6�   ? . $2 Right=�6�   5A     5B     5C     5D     5E     