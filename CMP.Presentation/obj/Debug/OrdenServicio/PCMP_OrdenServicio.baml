   M S B A M L   `   `   `  ����N  JComputerSystems.WPF, Version=3.0.0.0, Culture=neutral, PublicKeyToken=null�Yclr-namespace:ComputerSystems.WPF.Acciones.Controles.Buttons;assembly=ComputerSystems.WPF.ComputerSystems.WPF.Acciones.Controles.Buttons  5      �Sclr-namespace:ComputerSystems.WPF.Acciones.TitleModule;assembly=ComputerSystems.WPF(ComputerSystems.WPF.Acciones.TitleModule  5      �Zclr-namespace:ComputerSystems.WPF.Acciones.Controles.TextBoxs;assembly=ComputerSystems.WPF/ComputerSystems.WPF.Acciones.Controles.TextBoxs  5      �[clr-namespace:ComputerSystems.WPF.Acciones.Controles.DataGrids;assembly=ComputerSystems.WPF0ComputerSystems.WPF.Acciones.Controles.DataGrids  5	      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5
       CMP.Presentation7   1CMP.Presentation.OrdenServicio.PCMP_OrdenServicio   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      o	CmpButtonYclr-namespace:ComputerSystems.WPF.Acciones.Controles.Buttons;assembly=ComputerSystems.WPF     5      kTitleModuleSclr-namespace:ComputerSystems.WPF.Acciones.TitleModule;assembly=ComputerSystems.WPF     5      q
CmpTextBoxZclr-namespace:ComputerSystems.WPF.Acciones.Controles.TextBoxs;assembly=ComputerSystems.WPF     5      sCmpDataGrid[clr-namespace:ComputerSystems.WPF.Acciones.Controles.DataGrids;assembly=ComputerSystems.WPF     5	      6sys&clr-namespace:System;assembly=mscorlib     5
      H DMahApps.Metro, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null;Controls,http://metro.mahapps.com/winfx/xaml/controls  5      .��5      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   <   6ComputerSystems.WPF.Acciones.TitleModule.MyTitleModule  $��0��6$   $��0��61   $��5q�6A   $��2��6L      
NameModule$ Compra��6a      
NameWindow$  Administrar Orden Servicio��6u      Logo$0 */CMP.Presentation;component/Source/ima.png��6�   � 5   
   -   5         �Name$ MyHeader��5      $��0��6!   $��1��61   $��3��6>    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $	��180 �6#   $	��180��6/   �� 5      $	��180 �6#   $	��180��6/   �� 5      $	��175 �6#   $	��175��6/   .+�5!      � $��0��6   $��0��6     5"      �� 5#      .+�5%      �� $��0��6   $��0��6*   .+�5&      �� $��Sucursal��6    $��5,0,5,0q�60   $��10(�6A   �� 5'      -   5         ��Name$ cbxUsuarioEmpresaSucursal��5'      $��5,0,5,5q�6B    �� 
IsEditable .  �� MaxDropDownHeight$	 125��5(   1      AccentColorBrush+  %MahApps.Metro.Controls.ControlsHelper	   FocusBorderBrush#	 �   5)      
 � ToolTip$
 Seleccione Sucursal��5*      F   @ComputerSystems.WPF.Acciones.Controles.TextBoxs.FocusAdvancement   AdvancesByEnterKey .  �� DisplayMemberPath$&  ObjESGC_EmpresaSucursal.Sucursal��5-       �� SelectedValuePath$+ %ObjESGC_EmpresaSucursal.IdEmpSucursal��6T    �� SelectedValue 5,      �� *'%ObjESGC_EmpresaSucursal.IdEmpSucursal+ �� UpdateSourceTrigger$ PropertyChanged=�5.      �� 5/      $��Proveedor [Control + B]��6    $��5,0,5,0q�6?   $��10(�6P   �� 50      -   5         ��Name$ txtProveedorRazonSocial��50       �� CharacterCasing$ Upper=�6A   #	 �   51       . $��5,0,5,5q�54      $
 Proveedor Razón Social��55      ��53      �� *&$ObjEMNF_ClienteProveedor.RazonSocial+$ PropertyChanged=�55      �� 56      $��Contacto��6    $��5,0,5,0q�60   $��10(�6A   �� 57      -   5        $ txtContacto��57      $��5,0,5,5q�63   #	 �   58       . $
 Persona Contacto��5;      $ Upper=�6:   *  $MahApps.Metro.Controls.TextBoxHelper   ClearTextButton . ��59      �� *
Contacto+$ PropertyChanged=�5<      5=      5>      �� 5@      $��0��6   $��1��6&   .+�5A      �� $��Periodo��6   $��5,0,5,0q�6+   $��10(�6<   �� 5B      -   5        $ 
cbxPeriodo��5B      $��5,0,5,5q�6/    . $ Periodo��5D      $ Periodo��65   6Q   �� 5E      $��Moneda��6   $��5,0,5,0q�6*   $��10(�6;   �� 5F      -   5        $ 	cbxMoneda��5F       . $	 125��5G   -   #	 �   5H      $��1��5I      $��1��6*   $��2��6:   $��5,0,5,5q�6N   $
 Seleccione Moneda��5J       . $ Descripcion��5M      $ 	CodMoneda��6>    5L      �� *ObjESGC_Moneda.CodMoneda+$ PropertyChanged=�5N      �� 5O      $��Estado��6   $��5,0,5,0q�6*   $��10(�6;   �� 5P      -   5        $ 	cbxEstado��5P       . $	 125��5Q   -   #	 �   5R      $��2��5S      $��1��6*   $��2��6:   $��5,0,5,5q�6N   $
 Seleccione Estado��5U      {�.   . $ Estado��5W      $ 	CodEstado��68    5T      �� *ObjESGC_Estado.CodEstado+$ PropertyChanged=�5X      �� 5Y      $��Tipo Destino��6   $��5,0,5,0q�60   $��10(�6A   �� 5Z      -   5        $ cbxTipoDestino��5Z      $��2��5[      $��1��6&   $��2��66   $��5,0,5,5q�6J    . $	 125��5\   +    . #	 �   5^      $
 Seleccione Tipo Destino��5`      $ TipoDestino��5a      $ CodTipoDestino��69    5_      �� *$"ObjEMNF_TipoDestino.CodTipoDestino+$ PropertyChanged=�5b      5c      �� 5e      $��0��6   $��2��6&   .+�5f      �� $��Área��6   $��5,0,5,0q�6(   $��10(�69   �� 5g      -	   5        $ cbxArea��5g      $��5,0,5,5q�6,    �� SelectedIndex$ 0��6=    . $	 125��5h   -   #	 �   5i      $
 Seleccione Área��5j       . $
 Area��5m      $ IdArea��68    5l      �� *ObjESGC_Area.IdArea+$ PropertyChanged=�5n      �� 5o      $��Tipo cambio��6   $��5,0,5,0q�6/   $��10(�6@   *  $MahApps.Metro.Controls.NumericUpDown  5p      -
   5          Name$ txtSelRateTipoCambio��5p   )   $��5,0,5,5q�6G    . #	 �   5r   )      Interval$ 1Q�5t   -      Minimum$ 0Q�6:      StringFormat$ ###,###,##0.#00��6F   $!
 Ingrese SelRate Tipo Cambio��6e      Value 5s   )   �� *
TipoCambio+$ PropertyChanged=�5t   e   �� 5v      $��Fecha orden servicio��6   $��5,0,5,0q�68   $��10(�6I   (  "System.Windows.Controls.DatePicker  5w      -   5          Name$ dtpFechaServicio��5w      $��0��67   $��0��6G   $��5,0,5,5q�6T   #	 �   5x       . $%
 Seleccione Fecha Orden Servicio��5|         SelectedDate 5z      �� *Fecha+$ PropertyChanged=�5|      �� 5~      -   5         ��Name$ rbExonerado��5~      $��5,18,5,5q�60    d� 	IsChecked$ FalseY�6B   #	 �   5      $��	Honorario��5�      6/   5�      � 5�      $��0��6   $��3��6     5�      �� 5�      $
��Auto �6'   �� 5�      $
��Auto �6'     5�      �� 5�      $
��Auto �6$   �� 5�      $
��Auto �6$   .+�5�      �� $��0��6   $��0��6-   .+�5�      �� $��Serie��6    $��5,0,5,0q�6-   $��10(�6>   �� 5�      -   5        $ txtSerie��5�      $��5,0,5,5q�60   {�.  #	 �   5�       . $
 Serie��5�      $ Upper=�6/   ��5�      �� *Serie+$ PropertyChanged=�5�   /   5�      �� 5�      $��1��6   $��0��6-   .+�5�      �� $��	Documento��6    $��5,0,5,0q�61   $��10(�6B   �� 5�      -   5        $ txtCorrelativo��5�      $��5,0,5,5q�66   {�.  #	 �   5�       . $
 Nro Documento��5�      ��5�      �� *Numero+$ PropertyChanged=�5�      5�      �� 5�      $��1��6   $��0��6*   $��2��6:   .+�5�      �� $��Fecha inicio��6    $��5,0,5,0q�64   $��10(�6E     5�      -   5        $ dtpFechaInicio��5�   !   $��5,0,5,5q�69   #	 �   5�       . $
 Seleccione Fecha Inicio��5�       5�      �� *FechaInicio+$ PropertyChanged=�5�      �� 5�      $��	Fecha fin��6    $��5,0,5,0q�61   $��10(�6B     5�      -   5        $ dtpFechaFin��5�   !   $��5,0,5,5q�66   #	 �   5�       . $
 Seleccione Fecha Fin��5�         DisplayDateStart 5�   G   ��  �� ElementName$ dtpFechaInicio�� �� Path$ SelectedDate�  �� Mode$  TwoWay=� 5�      �� *
FechaFin+$ PropertyChanged=�5�      �� 5�      $��
Forma pago��6    $��5,0,5,0q�62   $��10(�6C   �� 5�      -   5        $ cbxFormaPago��5�       . $	 125��5�   -   #	 �   5�      $��5,0,5,5q�5�      $	��163��6-   $
 Seleccione F. Pago��5�       . $ 	FormaPago��5�      $ IdFormaPago��69    5�      �� *ObjESGC_FormaPago.IdFormaPago+$ PropertyChanged=�5�      5�      5�      5�      � 5�   
   $��0��6   $��2��6    5�      �� 5�      $
��Auto �6#   �� 5�      $��* �6#     5�      �� 5�      $��* �6    �� 5�      $
��Auto �6    .+�5�      � $��0��6   $��0��6    $��2��60   $��0q�6A    5�      �� 5�      �� 5�      .+�5�      �� $��1��6   $��0��6*   .+�5�      O   IComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaAddItem  -   5        !  Name$! btnAgregarServicio��5�   ;   $��5q�6W   "   ContentTitle$" Agregar Servicio��6b   6�   P   JComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaCancelar  5�      -   5        #  Name$# btnAnularProceso��5�   <   $��5q�6V   6a   M	   GComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaSalir	  5�      -   5        $ 	 Name$$ btnSalir��5�   9   $��5q�6K   6V   5�      �� 5�      $��1��6   $��1��6*   .+�5�      R
   LComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaDeleteItem
  -   5        % 
 Name$% btnQuitarArticulo��5�   >   $��5q�6Y   & 
  ContentTitle$& Quitar Servicio��6d   {�.  6�   O   IComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaGuardar  5�      -   5        '  Name$' 
btnGuardar��5�   ;   $��5q�6O   {�.  6l   P   JComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaImprimir  5�      -   5        (  Name$( btnImprimir��5�   <   $��5q�6Q   {�.  6n   5�      5�      &   System.Windows.Controls.DataGrid  5�      -   5        )  Name$) dgDetalleServicio��5�      $��1��62   $��0��6C   $��5q�6P     CmpStandarDataGrid#���  6[   *   SelectionUnit$
* Cell=�5�      G   AComputerSystems.WPF.Acciones.Controles.DataGrids.CmpGridViewFocus+   IsMoveFocus+ . ,   Columns, 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn-   Header$
- Item��6)   .   MinWidth$. 50Q�6P   /   Width5  /System.Windows.Controls.DataGridLengthConverter$/ 50 6^   0   
IsReadOnly0 . 1  +System.Windows.Controls.DataGridBoundColumn1   Binding1 67   �� *Item+6i     5�      $- Servicio��6)   $	. 350Q�6i   $	/ 350 6x   0 . 1 6;   �� *ObjEMNF_Servicio.Servicio+6�   4  .System.Windows.Controls.DataGridTemplateColumn  5�      $- Cantidad��6-   $	. 150Q�6?   $	/ 150 6N   2   CellTemplate2 5�      �� 5�      .R�5�   "   �� $ npdCantidad��6*   -   5        E   ?ComputerSystems.WPF.Acciones.Controles.TextBoxs.CmpMaskBehavior3   Mask$3 Decimal=�5�   *   4 �� TextAlignment$4 Right=�6T   ��5�   ?   �� *
Cantidad+$ PropertyChanged=�5 �� StringFormat$5 ###,###,##0.#0000000��5�   *   5�      5�        5�      $- Precio Unitario��6-   $	. 150Q�6F   $	/ 150 6U   2 5�      �� 5�      .R�5�   	   �� $ npdPrecioUnitario��6   -   5        $3 Decimal=�5�   *   $4 Right=�6T   ��5�   ,   �� *PrecioUnitario+$ PropertyChanged=�$5 ###,###,##0.#0000000��5�   *   5�      5�        5�      $- Importe��6-   $	. 100Q�6?   $
/ Auto 6N   0 . 2 5�      �� 5�      .R�5�   "   �� 6 �� TextAlignment$6 Right=�6i   ��6,   �� *	Importe+$5 ###,###,##0.#0000000��6i   5�      5�        5�      $	- IGV��6-   $	. 100Q�6;   $
/ Auto 6J   0 . 7   
Visibility7 6i   �� *VisibilityIGV+2 5�      �� 5�      .R�5�   "   �� $6 Right=�6l   ��6,   �� *
ImporteIGV+$5 ###,###,##0.#0000000��6l   5�      5�        5�      -   5        dtgColumnaTipoDestino��$. 40Q�5�   L   $-  ��6Z   $7 	Collapsed=�6d   2 5�      �� 5�      .R�5�   "   �� $
 Seleccione Tipo Destino��5   -   $
 Item��5  -   $ Value��6F   ��5�   ,   �� *ListCentroCosto+ 5�   -   �� *	IdDestino+$ PropertyChanged=�5  F   5     5       5     -   5        dtgColumnaPeriodoCompania��$. 40Q�5  P   $- Periodo Campaña��6^   2 5     �� 5     .R�5  "   �� $!
 Seleccione Periodo Campaña��5	  ,   $
 Item��5
  ,   $ Value��6E   ��5  ,   �� *ListPeriodoCampania+ 6X   �� *PeriodoCampania+$ PropertyChanged=�5
  E   5     5       5     $. 40Q�6-   $-  ��6;   2 5     �� 5     .R�5  "   �� 8 ��Name$$8 dtgdgDetalleServicioObervacion��6)   -   5           CmpDataViewButtonObservado01#��[ 5  Q   $
 Observación del Servicio��6�   6�   5     5     5     � 5     $��1��6   $��1��6!   $��5,5,5,5q�61   $��Right=�6B   9 � 	Resources9 5     : �� 
TargetType; �� Property< �� Value= �� 
TargetType> J� SnapsToDevicePixels? �� Triggers@ P� PropertyA P� Value��%b  '��       �� 5     ": ��6   .�5     �� ; � ��
< �����67   �� 5     ; � ��
< �   �68   �� 5     ; � ��< 5     �� 5  "   "= ��62   .R�5   &   �� $��Bd��6-   #��z��67   #��z��5!  -   #��z��5"  -   > . ._�5$  *   �� $��PART_ContentHost��67   #��z��6O   #> z> 5%  /   5&  '   ? 5'  &   P� 5(  *   @ � {�A .  . �5)  .   �� ; � ��
< �   �6C   �� 5*  .   ; � ��
< �����6C   5+  +   P� 5,  *   @ � {�A . . �5-  .   �� ; � ��
< �   �6C   �� 5.  .   ; � ��
< �����6C   5/  +   51  #   53     54     55      56     �� 57     $
��Auto �6'   �� 58     $
��Auto �6'   �� 59     $
��Auto �6'   �� 5:     $
��Auto �6'   �� 5;     $
��Auto �6'   �� 5<     $
��Auto �6'   .+�5?     �� -   5        $ chkAplicarRetencion��5?     $��0��68   $��0��6E   $��Center=�6U   $��Center=�6p   $��5q�6�   $ FalseY�5@     $��Aplicar Retención��6+   6G   �� 5B     -   5        $ rbIncluidoIGV��5B     $��0��62   $��1��6?   $��Center=�6O   $��Center=�6k   $��5q�6�   $ FalseY�5C     $��Precio Incluido IGV��6+   6J   �� 5E     
���   �$��5q�60   $��2��6;   .+�5F     �� -    5        B ��Name$B lblTitleOrdenServicio01��5F      $��	# Líneas��6A   $��3q�6Q   
�������$��Center=�6p   $��Center=�6�   �� 5G     -!   5        $ 	txtLineas��5G  '   $��0��6   
�������$��0q�6M   $
��Bold"�6X   $��15(�6j   
���   �$��0,1,0,0q�6�   C �� 
IsReadOnlyC . $4 Center=�6�   5H     �� 5I     
���   �$��5q�60   $��3��6;   $	��122��6K   .+�5J     �� -"   5        $B lblTitleOrdenServicio02��5J      $��Gravada��6A   $��3q�6P   
�������$��Center=�6o   $
��Left=�6�   �� 5K     -#   5        $ 
txtGravada��5K     $
��0.00��62   
�������$��0q�6Q   $
��Bold"�6\   $��15(�6n   
���   �$��0q�6�   $4 Right=�6�   C . 5L  Q   5N     �� 5O     
���   �$��5q�60   $��4��6;   $	��122��6K   .+�5P     �� -$   5        $B lblTitleOrdenServicio03��5P      $��IGV 18%��6B   $��3q�6Q   
�������$��Center=�6p   $
��Left=�6�   �� 5Q     -%   5        $ txtImporteIGV��5Q     $
��0.00��65   
�������$��0q�6T   $
��Bold"�6_   $��15(�6q   
���   �$��0q�6�   $4 Right=�6�   C . 5R  Q   5S     �� 5T     
���   �$��5q�60   $��5��6;   $	��122��6K   .+�5U     �� -&   5        $B lblTitleOrdenServicio04��5U      $��Total ��6A   $��3q�6O   
�������$��Center=�6n   $
��Left=�6�   �� 5V     -'   5        $ txtTotal��5V     $
��0.00��60   
�������$��0q�6O   $
��Bold"�6Z   $��15(�6l   
���   �$��0q�6�   $4 Right=�6�   C . 6�   5W     5X     5Y     5[     5\     