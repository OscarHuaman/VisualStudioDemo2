   M S B A M L   `   `   `  ����N  JComputerSystems.WPF, Version=3.0.0.0, Culture=neutral, PublicKeyToken=null�Yclr-namespace:ComputerSystems.WPF.Acciones.Controles.Buttons;assembly=ComputerSystems.WPF.ComputerSystems.WPF.Acciones.Controles.Buttons  5      �Sclr-namespace:ComputerSystems.WPF.Acciones.TitleModule;assembly=ComputerSystems.WPF(ComputerSystems.WPF.Acciones.TitleModule  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5       CMP.PresentationA   ;CMP.Presentation.OrdenServicio.PCMP_AprobacionOrdenServicio   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      o	CmpButtonYclr-namespace:ComputerSystems.WPF.Acciones.Controles.Buttons;assembly=ComputerSystems.WPF     5      kTitleModuleSclr-namespace:ComputerSystems.WPF.Acciones.TitleModule;assembly=ComputerSystems.WPF     5      6sys&clr-namespace:System;assembly=mscorlib     5      H DMahApps.Metro, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null;Controls,http://metro.mahapps.com/winfx/xaml/controls  5	      $	��600��5      $	��850��6   .��5      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   <   6ComputerSystems.WPF.Acciones.TitleModule.MyTitleModule  $��0��6$   $��0��61   $��5q�6A   $��2��6L      
NameModule$ Compra��6a      
NameWindow$# Aprobación de Orden Servicio��6u      Logo$0 */CMP.Presentation;component/Source/ima.png��6�   � 5   
   -   5         �Name$ MyHeader��5      $��0��6!   $��1��61   $��3��6>    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $	��180 �6#   $	��180��6/   �� 5      $	��180 �6#   $	��180��6/   �� 5      $	��175 �6#   $	��175��6/   .+�5       � $��0��6   $��0��6     5!      �� 5"      .+�5$      �� $��0��6   $��0��6*   .+�5%      �� $��	Surcursal��6    $��5,0,5,0q�61   $��10(�6B   �� 5&      -   5         ��Name$ txtUsuarioEmpresaSucursal��5&      $��5,0,5,5q�6A    � ToolTip$ Sucursal��5'       �� CharacterCasing$ Upper=�63   ��5(      �� *" ObjESGC_EmpresaSucursal.Sucursal+	 �� UpdateSourceTrigger$	 PropertyChanged=��� 5)      $��	Proveedor��6    $��5,0,5,0q�61   $��10(�6B   �� 5*      -   5        $ txtProveedorRazonSocial��5*      $ Upper=�6A   $��5,0,5,5q�5,      $ Proveedor Razón Social��5-      ��5+      �� *&$ObjEMNF_ClienteProveedor.RazonSocial+$	 PropertyChanged=�5-      �� 5.      $��Contacto��6    $��5,0,5,0q�60   $��10(�6A   �� 5/      -   5        $ txtContacto��5/      $��5,0,5,5q�63   $ Upper=�6E   $ Persona Contacto��51      
 �� 
IsReadOnly
 . ��50      �� *
Contacto+$	 PropertyChanged=�51   5   52      53      �� 55      $��0��6   $��1��6&   .+�56      �� $��Periodo��6   $��5,0,5,0q�6+   $��10(�6<   �� 57      -   5        $ 
txtPeriodo��57      $��5,0,5,5q�6.   $ Upper=�6@   $ Periodo��58   "   {�.  ��57   X   �� *	Periodo+58   4   �� 59      $��Moneda��6   $��5,0,5,0q�6*   $��10(�6;   �� 5:      -   5        $ 	txtMoneda��5:      $ Upper=�6.   $��1��5;      $��5,0,5,5q�6*   $ Moneda��5<      
 . ��5=      �� *ObjESGC_Moneda.Descripcion+$	 PropertyChanged=��� 5>      $��Estado��6   $��5,0,5,0q�6*   $��10(�6;   �� 5?      -   5        $ 	txtEstado��5?      $ Upper=�6.   $��2��5@      $��1��6*   $��2��6:   $��5,0,5,5q�6N   $ Estado��5B      
 . ��5A      �� *ObjESGC_Estado.Estado+$	 PropertyChanged=�5B   .   �� 5C      $��Tipo Destino��6   $��5,0,5,0q�60   $��10(�6A   �� 5D      -   5         ��Name$ cbxTipoDestino��5D      $��2��5E      $��1��6*   $��2��6:   $��5,0,5,5q�6N    �� 
IsEditable .   �� MaxDropDownHeight$	 125��5F   0   {�.     AccentColorBrush+  %MahApps.Metro.Controls.ControlsHelper   FocusBorderBrush# �   5G      $ Seleccione Tipo Destino��5I       �� DisplayMemberPath$ TipoDestino��5J       �� SelectedValuePath$ CodTipoDestino��6=    �� SelectedValue 5H      �� *$"ObjEMNF_TipoDestino.CodTipoDestino+$	 PropertyChanged=�5K      5L      �� 5N      $��0��6   $��2��6&   .+�5O      �� $��Área��6   $��5,0,5,0q�6(   $��10(�69   �� 5P      -	   5        $ txtArea��5P      $��5,0,5,5q�6+   $ Upper=�6=   $ Seleccione Área��5Q      
 . ��5R      �� *ObjESGC_Area.Area+$	 PropertyChanged=��� 5S      $��Tipo cambio��6   $��5,0,5,0q�6/   $��10(�6@   *  $MahApps.Metro.Controls.NumericUpDown  5T      -
   5          Name$ txtSelRateTipoCambio��5T   )   $��5,0,5,5q�6G      
IsReadOnly .    Interval$ 1Q�5V   )      Minimum$ 0Q�66      StringFormat$ ###,###,##0.#00��6B   $! Ingrese SelRate Tipo Cambio��6a      Value 5U   )   �� *
TipoCambio+$	 PropertyChanged=�5V   a   �� 5W      $��Fecha orden servicio��6   $��5,0,5,0q�68   $��10(�6I   (  "System.Windows.Controls.DatePicker  5X      -   5          Name$ dtpFechaServicio��5X      $��5,0,5,5q�67   
 �� Now   DisplayDateEnd# Z 6J   {�.  $% Seleccione Fecha Orden Servicio��5Z         SelectedDate 5Y      �� *Fecha+$	 PropertyChanged=�5Z      �� 5[      -   5         ��Name$ rbExonerado��5[      $��5,18,5,5q�60   {�.   d� 	IsChecked$ FalseY�6T   $��	Honorario��6f   5\      � 5^      $��0��6   $��3��6     5_      �� 5`      $
��Auto �6'   �� 5a      $
��Auto �6'     5c      �� 5d      $
��Auto �6$   �� 5e      $
��Auto �6$   .+�5h      �� $��0��6   $��0��6-   .+�5i      �� $��Serie��6    $��5,0,5,0q�6-   $��10(�6>   �� 5j      -   5        $ txtSerie��5j      $��5,0,5,5q�60   
 . $ Serie��5l      $ Upper=�6+   ��5k      �� *Serie+$	 PropertyChanged=�5l   +   5m      �� 5o      $��1��6   $��0��6-   .+�5p      �� $��	Documento��6    $��5,0,5,0q�61   $��10(�6B   �� 5q      -   5        $ txtCorrelativo��5q      $��5,0,5,5q�66   
 . $ Nro Documento��5s      ��5r      �� *Numero+$	 PropertyChanged=�5s      5t      �� 5v      $��1��6   $��0��6*   $��2��6:   .+�5w      �� $��Fecha inicio��6    $��5,0,5,0q�64   $��10(�6E     5x      -   5        $ dtpFechaInicio��5x   !   $��5,0,5,5q�69   {�.  $ Seleccione Fecha Inicio��5z       5y      �� *FechaInicio+$	 PropertyChanged=�5z      �� 5{      $��	Fecha fin��6    $��5,0,5,0q�61   $��10(�6B     5|      -   5        $ dtpFechaFin��5|   !   $��5,0,5,5q�66   {�.  $ Seleccione Fecha Fin��5         DisplayDateStart 5}   !   ��  �� ElementName$ dtpFechaInicio��  �� Path$  SelectedDate�! �� Mode$! TwoWay=� 5~      �� *
FechaFin+$	 PropertyChanged=�5      �� 5�      $��
Forma pago��6    $��5,0,5,0q�62   $��10(�6C   �� 5�      -   5        $ cbxFormaPago��5�      
 . $��5,0,5,5q�5�      $	��163��6-   $ 
Forma Pago��5�      $ Upper=�61   ��5�      �� *ObjESGC_FormaPago.FormaPago+$	 PropertyChanged=�5�      5�      5�      � 5�   
   $��0��6   $��2��6    5�      �� 5�      $
��Auto �6#   �� 5�      $��* �6#     5�      �� 5�      $��* �6    �� 5�      $
��Auto �6    .+�5�      � $��0��6   $��0��6    $��2��60   $��0q�6A    5�      �� 5�      �� 5�      .+�5�      �� $��1��6   $��0��6*   .+�5�      O   IComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaGuardar  -   5        "  Name$" 
btnGuardar��5�   ;   $��5q�6O   {�.  6l   M   GComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaSalir  5�      -   5        #  Name$# btnSalir��5�   9   $��5q�6K   6V   5�      �� 5�      $��1��6   $��1��6*   .+�5�      P   JComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaImprimir  -   5        $  Name$$ btnImprimir��5�   <   $��5q�6Q   {�. 6m   5�      5�      &   System.Windows.Controls.DataGrid  5�      -   5        %  Name$% dgDetalleServicio��5�      $��1��62   $��0��6C   $��5q�6P     CmpStandarDataGrid#���  6[   &   
IsReadOnly& . '   Columns' 5�      0	  *System.Windows.Controls.DataGridTextColumn	  5�      ,
  &System.Windows.Controls.DataGridColumn( 
  Header$
( Item��6)   ) 
  MinWidth$) 50Q�6P   * 
  Width5  /System.Windows.Controls.DataGridLengthConverter$* 50 6^   + 
  
IsReadOnly+ . 1  +System.Windows.Controls.DataGridBoundColumn,   Binding, 67   �� *Item+6i   	  5�      $( Servicio��6)   $	) 350Q�6i   $	* 350 6x   + . , 6;   �� *ObjEMNF_Servicio.Servicio+6�   4  .System.Windows.Controls.DataGridTemplateColumn  5�      $( Cantidad��6-   $	) 150Q�6@   $	* 150 6O   + . -   CellTemplate- 5�      �� 5�      .R�5�   "   �� . �� TextAlignment$. Right=�6l   ��6,   �� *
Cantidad+/ �� StringFormat$/ ###,###,##0.#0000000��6l   5�      5�        5�      $( Precio Unitario��6-   $	) 150Q�6G   $
* Auto 6V   + . - 5�      �� 5�      .R�5�   "   �� $. Right=�6r   ��6,   �� *PrecioUnitario+$/ ###,###,##0.#0000000��6r   5�      5�        5�      $( Importe��6-   $	) 100Q�6?   $
* Auto 6N   + . - 5�      �� 5�      .R�5�   "   �� $. Right=�6k   ��6,   �� *	Importe+$/ ###,###,##0.#0000000��6k   5�      5�        5�      $	( IGV��6-   $	) 100Q�6;   $
* Auto 6J   + . - 5�      �� 5�      .R�5�   "   �� $. Right=�6n   ��6,   �� *
ImporteIGV+$/ ###,###,##0.#0000000��6n   5�      5�        5�      -   5        dtgColumnaTipoDestino��$) 40Q�5�   L   $(  ��6Z   0 
  
Visibility$0 	Collapsed=�6d   - 5�      �� 5�      .R�5�   "   �� {�.  $ Seleccione Tipo Destino��5�   -   $
 Item��5�   -   $ Value��6F   ��5�   ,   �� *ListCentroCosto+ 5�   -   �� *	IdDestino+$	 PropertyChanged=�5�   F   5�      5�        5�      -   5        dtgColumnaPeriodoCompania��$) 40Q�5�   P   $( Periodo Compañía��6^   - 5�      �� 5�      .R�5�   "   �� $# Seleccione Periodo Compañía��5�   ,    . $
 Item��5�   ,   $ Value��6E   {�.  ��5�   ,   �� *ListPeriodoCampania+ 6X   �� *PeriodoCampania+$	 PropertyChanged=�5�   _   5�      5�      5�      � 5�      $��1��6   $��1��6!   $��5,5,5,5q�61   $��Right=�6B    5�      �� 5�      $
��Auto �6'   �� 5�      $
��Auto �6'   �� 5�      $
��Auto �6'   �� 5�      $
��Auto �6'   �� 5�      $
��Auto �6'   �� 5�      $
��Auto �6'   .+�5�      �� -   5        $ chkAplicarRetencion��5�      $��0��68   $��0��6E   $��Center=�6U   $��Center=�6p   $��5q�6�   {�.  $ FalseY�5�   +   $��Aplicar Retención��6=   �� 5�      -   5        $ rbIncluidoIGV��5�      $��0��62   $��1��6?   $��Center=�6O   $��Center=�6j   $��5q�6�   {�.  $ FalseY�5�   +   $��Precio Incluido IGV��6=   �� 5�      
���   �$��5q�60   $��2��6;   .+�5�      �� -   5        1 ��Name$1 lblTitleOrdenServicio01��5�       $��	# Líneas��6A   $��3q�6Q   
�������$��Center=�6p   $��Center=�6�   �� 5�      -   5        $1 	txtLineas��5�   )   $��0��6    
�������$��0q�6O   $
��Bold"�6Z   $��20(�6l   $��Center=�6z   $��Center=�6�   5�      �� 5�      
���   �$��5q�60   $��3��6;   $	��122��6K   .+�5�      �� -   5        $1 lblTitleOrdenServicio02��5�       $��Gravada��6A   $��3q�6P   
�������$��Center=�6o   $
��Left=�6�   �� 5�      -   5        $ 
txtGravada��5�      $
��0.00��62   
�������$��0q�6Q   $
��Bold"�6\   $��15(�6n   $��Center=�6|   $��Right=�6�   
���   �$��0q�6�   5�      �� 5�      
���   �$��5q�60   $��4��6;   $	��122��6K   .+�5�      �� -   5        $1 lblTitleOrdenServicio03��5�       $��IGV 18%��6B   $��3q�6Q   
�������$��Center=�6p   $
��Left=�6�   �� 5�      -   5        $ txtImporteIGV��5�      $
��0.00��65   
�������$��0q�6T   $
��Bold"�6_   $��15(�6q   $��Center=�6   $��Right=�6�   
���   �$��0q�6�   5�      �� 5�      
���   �$��5q�60   $��5��6;   $	��122��6K   .+�5�      �� -    5        $1 lblTitleOrdenServicio04��5�       $��Total ��6A   $��3q�6O   
�������$��Center=�6n   $
��Left=�6�   �� 5�      -!   5        $ txtTotal��5�      $
��0.00��60   
�������$��0q�6O   $
��Bold"�6Z   $��15(�6l   $��Center=�6z   $��Right=�6�   
���   �$��0q�6�   5�      5�      5�      5�      5�      