   M S B A M L   `   `   `  ����N  JComputerSystems.WPF, Version=3.0.0.0, Culture=neutral, PublicKeyToken=null�Yclr-namespace:ComputerSystems.WPF.Acciones.Controles.Buttons;assembly=ComputerSystems.WPF.ComputerSystems.WPF.Acciones.Controles.Buttons  5      �Sclr-namespace:ComputerSystems.WPF.Acciones.TitleModule;assembly=ComputerSystems.WPF(ComputerSystems.WPF.Acciones.TitleModule  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5       CMP.Presentation;   5CMP.Presentation.OrdenServicio.PCMP_ViewOrdenServicio   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      o	CmpButtonYclr-namespace:ComputerSystems.WPF.Acciones.Controles.Buttons;assembly=ComputerSystems.WPF     5      kTitleModuleSclr-namespace:ComputerSystems.WPF.Acciones.TitleModule;assembly=ComputerSystems.WPF     5      6sys&clr-namespace:System;assembly=mscorlib     5      H DMahApps.Metro, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null;Controls,http://metro.mahapps.com/winfx/xaml/controls  5	      .��5      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   <   6ComputerSystems.WPF.Acciones.TitleModule.MyTitleModule  $��0��6$   $��0��61   $��5q�6A   $��2��6L      
NameModule$ Compra��6a      
NameWindow$$ Administración Orden Servicio��6u      Logo$0 */CMP.Presentation;component/Source/ima.png��6�   � 5   
   -   5         �Name$ MyHeader��5      $��0��6!   $��1��61   $��3��6>    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $	��180 �6#   $	��180��6/   �� 5      $	��180 �6#   $	��180��6/   �� 5      $	��175 �6#   $	��175��6/   .+�5      � $��0��6   $��0��6     5       �� 5!      .+�5#      �� $��0��6   $��0��6*   .+�5$      �� $��Sucursal��6    $��5,0,5,0q�60   $��10(�6A   �� 5%      -   5         ��Name$ txtUsuarioEmpresaSucursal��5%      $��5,0,5,5q�6A    � ToolTip$ Sucursal��5&       �� CharacterCasing$ Upper=�63   ��5'      �� *" ObjESGC_EmpresaSucursal.Sucursal+	 �� UpdateSourceTrigger$	 PropertyChanged=��� 5(      $��	Proveedor��6    $��5,0,5,0q�61   $��10(�6B   �� 5)      -   5        $ txtProveedorRazonSocial��5)      $��5,0,5,5q�5+      $ Upper=�61   $ Proveedor Razón Social��5,      ��5*      �� *&$ObjEMNF_ClienteProveedor.RazonSocial+$	 PropertyChanged=�5,      �� 5-      $��Contacto��6    $��5,0,5,0q�60   $��10(�6A   �� 5.      -   5        $ txtContacto��5.      $��5,0,5,5q�63   $ Upper=�6E   $ Persona Contacto��50      
 �� 
IsReadOnly
 . ��5/      �� *
Contacto+$	 PropertyChanged=�50   5   51      52      �� 54      $��0��6   $��1��6&   .+�55      �� $��Periodo��6   $��5,0,5,0q�6+   $��10(�6<   �� 56      -   5        $ 
txtPeriodo��56      $��5,0,5,5q�6.   $ Upper=�6@   $ Periodo��57   "   {�.  ��56   X   �� *	Periodo+57   4   �� 58      $��Moneda��6   $��5,0,5,0q�6*   $��10(�6;   �� 59      -   5        $ 	txtMoneda��59      $��1��5:      $��5,0,5,5q�6*   $ Upper=�6<   $ Moneda��5;      
 . ��5<      �� *ObjESGC_Moneda.Descripcion+$	 PropertyChanged=��� 5=      $��Estado��6   $��5,0,5,0q�6*   $��10(�6;   �� 5>      -   5        $ 	txtEstado��5>      $ Upper=�6.   $��2��5?      $��1��6*   $��2��6:   $��5,0,5,5q�6N   $ Estado��5A      
 . ��5@      �� *ObjESGC_Estado.Estado+$	 PropertyChanged=�5A   .   �� 5B      $��Tipo Destino��6   $��5,0,5,0q�60   $��10(�6A   �� 5C      -   5         ��Name$ cbxTipoDestino��5C      $��2��5D      $��1��6(   $��2��68   $��5,0,5,5q�6L    �� 
IsEditable .  �� MaxDropDownHeight$	 125��5E   -   {�.     AccentColorBrush+  %MahApps.Metro.Controls.ControlsHelper   FocusBorderBrush# �   5F      $ Seleccione Tipo Destino��5H       �� DisplayMemberPath$ TipoDestino��5I       �� SelectedValuePath$ CodTipoDestino��6;    �� SelectedValue 5G      �� *$"ObjEMNF_TipoDestino.CodTipoDestino+$	 PropertyChanged=�5J      5K      �� 5M      $��0��6   $��2��6&   .+�5N      �� $��Área��6   $��5,0,5,0q�6(   $��10(�69   �� 5O      -	   5        $ txtArea��5O      $��5,0,5,5q�6+   {�.  $ Seleccione Área��5P      $ Upper=�65   ��5Q      �� *ObjESGC_Area.Area+$	 PropertyChanged=��� 5R      $��Tipo cambio��6   $��5,0,5,0q�6/   $��10(�6@   *  $MahApps.Metro.Controls.NumericUpDown  5S      -
   5          Name$ txtSelRateTipoCambio��5S   )   $��5,0,5,5q�6G      
IsReadOnly .    Interval$ 1Q�5U   )      Minimum$ 0Q�66      StringFormat$ ###,###,##0.#00��6B   $! Ingrese SelRate Tipo Cambio��6a      Value 5T   )   �� *
TipoCambio+$	 PropertyChanged=�5U   a   �� 5V      $��Fecha orden servicio��6   $��5,0,5,0q�68   $��10(�6I   (  "System.Windows.Controls.DatePicker  5W      -   5          Name$ dtpFechaServicio��5W      $��5,0,5,5q�67   
 �� Now   DisplayDateEnd# Z 6J   {�.  $% Seleccione Fecha Orden Servicio��5Y         SelectedDate 5X      �� *Fecha+$	 PropertyChanged=�5Y      �� 5Z      -   5         ��Name$ rbExonerado��5Z      $��5,18,5,5q�60   {�.   d� 	IsChecked$ FalseY�6T   $��	Honorario��6f   5[      � 5]      $��0��6   $��3��6     5^      �� 5_      $
��Auto �6'   �� 5`      $
��Auto �6'     5b      �� 5c      $
��Auto �6$   �� 5d      $
��Auto �6$   .+�5g      �� $��0��6   $��0��6-   .+�5h      �� $��Serie��6    $��5,0,5,0q�6-   $��10(�6>   �� 5i      -   5        $ txtSerie��5i      $��5,0,5,5q�60   
 . $ Serie��5k      $ Upper=�6+   ��5j      �� *Serie+$	 PropertyChanged=�5k   +   5l      �� 5n      $��1��6   $��0��6-   .+�5o      �� $��	Documento��6    $��5,0,5,0q�61   $��10(�6B   �� 5p      -   5        $ txtCorrelativo��5p      $��5,0,5,5q�66   
 . $ Nro Documento��5r      $ Upper=�63   ��5q      �� *Numero+$	 PropertyChanged=�5r   3   5s      �� 5u      $��1��6   $��0��6*   $��2��6:   .+�5v      �� $��Fecha inicio��6    $��5,0,5,0q�64   $��10(�6E     5w      -   5        $ dtpFechaInicio��5w   !   $��5,0,5,5q�69   {�.  $ Seleccione Fecha Inicio��5y       5x      �� *FechaInicio+$	 PropertyChanged=�5y      �� 5z      $��	Fecha fin��6    $��5,0,5,0q�61   $��10(�6B     5{      -   5        $ dtpFechaFin��5{   !   $��5,0,5,5q�66   {�.  $ Seleccione Fecha Fin��5~         DisplayDateStart 5|   !   ��  �� ElementName$ dtpFechaInicio��  �� Path$  SelectedDate�! �� Mode$! TwoWay=� 5}      �� *
FechaFin+$	 PropertyChanged=�5~      �� 5      $��
Forma pago��6    $��5,0,5,0q�62   $��10(�6C   �� 5�      -   5        $ cbxFormaPago��5�      
 . $��5,0,5,5q�5�      $	��163��6/   $ 
Forma Pago��5�      $ Upper=�64   ��5�      �� *ObjESGC_FormaPago.FormaPago+$	 PropertyChanged=�5�      5�      5�      � 5�   
   $��0��6   $��2��6    5�      �� 5�      $
��Auto �6#   �� 5�      $��* �6#     5�      �� 5�      $��* �6    �� 5�      $
��Auto �6    .+�5�      � $��0��6   $��0��6    $��2��60   $��0q�6A    5�      �� 5�      �� 5�      .+�5�      �� $��1��6   $��0��6*   .+�5�      M   GComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaSalir  -   5        "  Name$" btnSalir��5�   9   $��5q�6K   6V   5�      �� 5�      $��1��6   $��1��6*   .+�5�      P   JComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaImprimir  -   5        #  Name$# btnImprimir��5�   <   $��5q�6Q   {�. 6m   5�      5�      &   System.Windows.Controls.DataGrid  5�      -   5        $  Name$$ dgDetalleServicio��5�      $��1��62   $��0��6C   $��5q�6P     CmpStandarDataGrid#���  6[   %   Columns% 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,	  &System.Windows.Controls.DataGridColumn& 	  Header$
& Item��6)   ' 	  MinWidth$' 50Q�6P   ( 	  Width5
  /System.Windows.Controls.DataGridLengthConverter$( 50
 6^   ) 	  
IsReadOnly) . 1  +System.Windows.Controls.DataGridBoundColumn*   Binding* 67   �� *Item+6i     5�      $& Servicio��6)   $	' 100Q�6i   $( *
 6x   ) . * 6;   �� *ObjEMNF_Servicio.Servicio+6�   4  .System.Windows.Controls.DataGridTemplateColumn  5�      $& Cantidad��6-   $	' 100Q�6@   $
( Auto
 6O   ) . +   CellTemplate+ 5�      �� 5�      .R�5�   "   �� , �� TextAlignment$, Right=�6l   ��6,   �� *
Cantidad+- �� StringFormat$- ###,###,##0.#0000000��6l   5�      5�        5�      $& Precio Unitario��6-   $	' 100Q�6G   $
( Auto
 6V   ) . + 5�      �� 5�      .R�5�   "   �� $, Right=�6r   ��6,   �� *PrecioUnitario+$- ###,###,##0.#0000000��6r   5�      5�        5�      $	& IGV��6-   $	' 100Q�6;   $
( Auto
 6J   ) . + 5�      �� 5�      .R�5�   "   �� $, Right=�6n   ��6,   �� *
ImporteIGV+$- ###,###,##0.#0000000��6n   5�      5�        5�      $& Importe��6-   $	' 100Q�6?   $
( Auto
 6N   ) . + 5�      �� 5�      .R�5�   "   �� $, Right=�6k   ��6,   �� *	Importe+$- ###,###,##0.#0000000��6k   5�      5�        5�      -   5        dtgColumnaTipoDestino��$' 40Q�5�   L   $&  ��6Z   . 	  
Visibility$. 	Collapsed=�6d   + 5�      �� 5�      .R�5�   "   ��  .  $ Seleccione Tipo Destino��5�   -   $
 Item��5�   -   $ Value��6F   ��5�   ,   �� *ListCentroCosto+ 5�   -   �� *	IdDestino+$	 PropertyChanged=�5�   F   5�      5�        5�      -   5        dtgColumnaPeriodoCompania��$' 40Q�5�   P   $& Periodo Campaña��6^   + 5�      �� 5�      .R�5�   "   �� $! Seleccione Periodo Campaña��5�   ,   {�.  $
 Item��5�   ,   $ Value��6E   ��5�   ,   �� *ListPeriodoCampania+ 6X   �� *PeriodoCampania+$	 PropertyChanged=�5�   E   5�      5�      5�      � 5�      $��1��6   $��1��6!   $��5,5,1,5q�61   $��Right=�6B    5�      �� 5�      $
��Auto �6'   �� 5�      $
��Auto �6'   �� 5�      $
��Auto �6'   �� 5�      $
��Auto �6'   �� 5�      $
��Auto �6'   �� 5�      $
��Auto �6'   .+�5�      �� -   5        $ chkAplicarRetencion��5�      $��0��68   $��0��6E   $��Center=�6U   $��Center=�6p   $��5,15,5,5q�6�   {�.  $ FalseY�5�   +   $��Aplicar Retención��6=   �� 5�      $��0��6   $��1��6+   .+�5�      �� -   5        $ rbIncluidoIGV��5�      $��5,25,5,5q�66   $ FalseY�6H   $��Center=�5�      $��Precio Incluido IGV��5�      �� 5�      -   5        $ rbExoneradoIGV��5�      $ FalseY�68   $��Center=�5�      $y�	Collapsed=�66   $��Exonerado IGV��5�      5�      �� 5�      
���   �$��5q�60   $��2��6;   .+�5�      �� -   5        / ��Name$/ lblTitleOrdenServicio01��5�       $��	# Líneas��6A   $��3q�6Q   
�������$��Center=�6p   $��Center=�6�   �� 5�      -   5        $/ 	txtLineas��5�   )   $��0��6    
�������$��0q�6O   $
��Bold"�6Z   $��20(�6l   $��Center=�6z   $��Center=�6�   5�      �� 5�      
���   �$��5q�60   $��3��6;   $	��122��6K   .+�5�      �� -   5        $/ lblTitleOrdenServicio02��5�       $��Gravada��6A   $��3q�6P   
�������$��Center=�6o   $
��Left=�6�   �� 5�      -   5        $ 
txtGravada��5�      $
��0.00��62   
�������$��0q�6Q   $
��Bold"�6\   $��15(�6n   $��Center=�6|   $��Right=�6�   
���   �$��0q�6�   5�      �� 5�      
���   �$��5q�60   $��4��6;   $	��122��6K   .+�5�      �� -   5        $/ lblTitleOrdenServicio03��5�       $��IGV 18%��6B   $��3q�6Q   
�������$��Center=�6p   $
��Left=�6�   �� 5�      -   5        $ txtImporteIGV��5�      $
��0.00��65   
�������$��0q�6T   $
��Bold"�6_   $��15(�6q   $��Center=�6   $��Right=�6�   
���   �$��0q�6�   5�      �� 5�      
���   �$��5q�60   $��5��6;   $	��122��6K   .+�5�      �� -    5        $/ lblTitleOrdenServicio04��5�       $��Total ��6A   $��3q�6O   
�������$��Center=�6n   $
��Left=�6�   �� 5�      -!   5        $ txtTotal��5�      $
��0.00��60   
�������$��0q�6O   $
��Bold"�6Z   $��15(�6l   $��Center=�6z   $��Right=�6�   
���   �$��0q�6�   5�      5�      5�      5�      5      