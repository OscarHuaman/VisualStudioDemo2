   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      N JComputerSystems.WPF, Version=3.0.0.0, Culture=neutral, PublicKeyToken=null�Sclr-namespace:ComputerSystems.WPF.Acciones.TitleModule;assembly=ComputerSystems.WPF(ComputerSystems.WPF.Acciones.TitleModule 5      �Yclr-namespace:ComputerSystems.WPF.Acciones.Controles.Buttons;assembly=ComputerSystems.WPF.ComputerSystems.WPF.Acciones.Controles.Buttons 5      �Zclr-namespace:ComputerSystems.WPF.Acciones.Controles.TextBoxs;assembly=ComputerSystems.WPF/ComputerSystems.WPF.Acciones.Controles.TextBoxs 5	      �[clr-namespace:ComputerSystems.WPF.Acciones.Controles.DataGrids;assembly=ComputerSystems.WPF0ComputerSystems.WPF.Acciones.Controles.DataGrids 5
       CMP.Presentation)   #CMP.Presentation.Compra.PCMP_Compra   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6sys&clr-namespace:System;assembly=mscorlib     5      kTitleModuleSclr-namespace:ComputerSystems.WPF.Acciones.TitleModule;assembly=ComputerSystems.WPF     5      o	CmpButtonYclr-namespace:ComputerSystems.WPF.Acciones.Controles.Buttons;assembly=ComputerSystems.WPF     5      q
CmpTextBoxZclr-namespace:ComputerSystems.WPF.Acciones.Controles.TextBoxs;assembly=ComputerSystems.WPF     5	      sCmpDataGrid[clr-namespace:ComputerSystems.WPF.Acciones.Controles.DataGrids;assembly=ComputerSystems.WPF     5
      H DMahApps.Metro, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null;Controls,http://metro.mahapps.com/winfx/xaml/controls  5      .��5      �   � 	Resources  5   
   P  JComputerSystems.WPF.Acciones.Controles.TextBoxs.ConvertTextBoxTrimSpaceInt   ConvertTextBoxTrimSpaceInt��%   &	          5      64   5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   <  6ComputerSystems.WPF.Acciones.TitleModule.MyTitleModule  $��0��6$   $��0��61   $��5q�6A   $��2��6L      
NameModule$ Cuentas Por Pagar��6`      
NameWindow$ Provisión de Documentos��6      Logo$0 */CMP.Presentation;component/Source/ima.png��6�   � 5   
   -   5         �Name$ MyHeader��5   A   $��0��6   $��1��6    $��3��6-    � ColumnDefinitions 5      �� 5      $
��1.5* �6#   �� 5      $
��0.8* �6#   �� 5      $
��0.8* �6#   �� 5      $
��1.5* �6#   �� 5       $
��0.8* �6#    5"      �� 5#      $
��Auto �6    �� 5$      $��* �6    .+�5'      �� $��0��6   $��2��6&   $��0��67   .+�5(      �� $��
Sub-diario��6   $��5,0,5,0q�6.   $��10(�6?   �� 5)      -   5         ��Name$ cbxSubDiario��5)      {�.  $��2��5*      $��1��6(   $��2��68   $��5,0,5,5q�6L    �� 
IsEditable . 	 �� MaxDropDownHeight$		 125��5+   -     AccentColorBrush+  %MahApps.Metro.Controls.ControlsHelper
   FocusBorderBrush#
 �  5,       � ToolTip$ Seleccione Sub Diario��5.      F  @ComputerSystems.WPF.Acciones.Controles.TextBoxs.FocusAdvancement   AdvancesByEnterKey .  �� DisplayMemberPath$ 	SubDiario��50       �� SelectedValuePath$ IdSubDiario��69    �� SelectedValue 5-      �� *ObjEMNF_SubDiario.IdSubDiario+ �� UpdateSourceTrigger$ PropertyChanged=�51      �� 52      $��Operación movimiento��6   $��5,0,5,0q�68   $��10(�6I   �� 53      -   5        $ cbxOperacionMovimiento��53      $��0��54      $��0��6)   $��5,0,5,5q�69    . $		 125��55   .   #
 �  56      $&  Seleccione Operación Movimiento��58       . $ OpMovimiento��5:      $ CodOpeMovimiento��6=    57      �� *.,ObjEMNF_OperacionMovimiento.CodOpeMovimiento+$ PropertyChanged=�5;      �� 5<      $��Motivo movimiento��6   $��5,0,5,0q�65   $��10(�6F   �� 5=      -   5        $ cbxMotivoMovimiento��5=      $��2��5>      $��1��6(   $��2��68   $��5,0,5,5q�6L    .  $		 125��5?   .   #
 �  5@      $" Seleccione Motivo Movimiento��5B      {�.   . $ MotMovimiento��5D      $ CodMotMovimiento��6=    5A      �� *+)ObjEMNF_MotivoMovimiento.CodMotMovimiento+$ PropertyChanged=�5E      �� 5F      $��Proveedor [Control + B]��6   $��5,0,5,0q�6;   $��10(�6L   �� 5G      -   5         ��Name$ txtProveedorRazonSocial��5G      $��5,0,5,5q�6;    �� CharacterCasing$ Upper=�6M   #
 �  5H       . $r lRazón Social del Proveedor | Filtrar por [ Nro Documento | Razón Social | Contacto | Representante Legal ]��5L      ��5J      �� *&$ObjEMNF_ClienteProveedor.RazonSocial+$ PropertyChanged=�5L      �� 5M      $��
Forma pago��6   $��5,0,5,0q�6.   $��10(�6?   �� 5N      -   5        $ cbxFormaPago��5N      $��5,0,5,5q�61    . $		 125��5O   -   #
 �  5P      $ Seleccione F. Pago��5Q       . $ 	FormaPago��5T      $ IdFormaPago��69    5S      �� *ObjESGC_FormaPago.IdFormaPago+$ PropertyChanged=�5U      5V      �� 5X      $��0��6   $��1��6'   .+�5Y      �� $��Periodo��6   $��5,0,5,0q�6+   $��10(�6<   �� 5Z      -   5        $ 
cbxPeriodo��5Z      $��5,0,5,5q�6/    . $ Periodo��5]      $ Periodo��65    5[      �� *	Periodo+ �� Mode$ TwoWay=�$ PropertyChanged=�5]   Q   �� 5_      $��Fecha emisión��6   $��5,0,5,0q�61   $��10(�6B   (  "System.Windows.Controls.DatePicker  5`      -   5          Name$ dtpFechaOrden��5`      $��5,0,5,5q�64   #
 �  5a       . $ Seleccione Fecha Emisión��5e         SelectedDate 5b      �� *Fecha+$ PropertyChanged=�5e      �� 5g      $��Moneda��6   $��5,0,5,0q�6*   $��10(�6;   �� 5h      -	   5        $ 	cbxMoneda��5h      $��1��5i      $��1��6(   $��2��68   $��5,0,5,5q�6L    . $		 125��5j   -   #
 �  5k       . $ Seleccione Moneda��5m      $ Descripcion��5n      $ 	CodMoneda��6<   5o      5p      �� 5r      $��0��6   $��2��6&   .+�5s      �� $��Estado��6   $��5,0,5,0q�6*   $��10(�6;   �� 5t      -
   5        $ 	cbxEstado��5t      $��2��5u      $��1��6(   $��2��68   $��5,0,5,5q�6L    . $		 125��5v   -   #
 �  5w       . $ Seleccione Estado��5z      {�.  $ Estado��5{      $ 	CodEstado��66    5x      �� *ObjESGC_Estado.CodEstado+$ PropertyChanged=�5|      �� 5}      $��Fecha contable��6   $��5,0,5,0q�62   $��10(�6C     5~      -   5        $ dtpFechaContable��5~      $��5,0,5,5q�68   #
 �  5       . $ Seleccione Fecha Contable��5�       5�      �� *FechaContable+$ PropertyChanged=�5�   A   �� 5�      $��Tipo cambio��6   $��5,0,5,0q�6/   $��10(�6@   *  $MahApps.Metro.Controls.NumericUpDown  5�      -   5          Name$ txtSelRateTipoCambio��5�   )   $��5,0,5,5q�6G   {�.  #
 �  5�   )    .    Interval$ 1Q�5�   )      Minimum$ 0Q�66      StringFormat$ ###,###,##0.#00��6C   $ Ingrese Tipo Cambio��6b      Value 5�   )   �� *
TipoCambio+$ PropertyChanged=�5�   b   5�      �� 5�      $��0��6   $��2��6&   $��3��67   .+�5�      �� -   5         ��Name$ lblDocReferencia��5�      $��[Automático] ��67   $
��Wrap=�6L   $��5,0,5,0q�6`   $��10(�6q   �� 5�      -   5        $ cbxDocReferencia��5�      $��5,0,5,5q�65   {�.   . $		 125��5�   -    . #
 �  5�      $
 Item��5�      $ Value��64    5�      �� *CodDocumentoRef+$ PropertyChanged=�5�      �� 5�      -   5        $ cbxEmpresaSucursal��5�      $��5,0,5,5q�67   $y�	Collapsed=�6H    . $		 125��5�   1   #
 �  5�      $ Seleccione Sucursal��5�       . $&  ObjESGC_EmpresaSucursal.Sucursal��5�      $+ %ObjESGC_EmpresaSucursal.IdEmpSucursal��6T    5�      �� *'%ObjESGC_EmpresaSucursal.IdEmpSucursal+$ PropertyChanged=�5�      � 5�      -   5        $ GridGuiaRemision��5�      $y�Visible=�61    5�      �� 5�      �� 5�      .+�5�      �� $��0��6    $��0��6-   $��Guía Remisión��6=   $��5,0,5,0q�6R   $��10(�6c   �� 5�      -   5        $ txtGuiaRemision��5�      $��1��67   $��0��6D   $��5,0,5,5q�6T   $ Upper=�6f   {�.   . $ Guía Remisión��5�      ��5�      �� *GuiaRemision+$ PropertyChanged=�5�      5�      � 5�      -   5        $ GridDocReferencias��5�       5�      �� 5�      �� 5�       5�      �� 5�      $��* �6+   �� 5�      $
��Auto �6+   .+�5�      �� $��0��6!   $��0��6.   .+�5�      �� $,��&Nro Documento Referencia [Control + B]��6    $
��Wrap=�6N   $��5,0,5,0q�6b   $��10(�6s   �� 5�      -   5        $ txtDocumentoRef��5�      $��5,0,5,5q�68   #
 �  5�       . $ Upper=�5�   B   $ Nro Documento Referencia��5�      ��5�      �� *	NumeroRef+$ PropertyChanged=�5�      5�      �� 5�      -   5         ��Name$ stpAnticipo��5�   !   $��0��66   $��1��6D   .+�5�      �� $��Anticipo��6$   $
��Wrap=�64   $��5,0,5,0q�6H   $��10(�6Y   �� 5�      -   5         ��Name$ btnAnticipo��5�   !      CmpDataViewButtonObservado01#��[ 66   $ Observación del Servicio��6l   $��5,0,5,0q�6�   5�      &   System.Windows.Controls.DataGrid  5�      -   5          Name$ dgReferencias��5�      $��1��66   $��2��6C   $��5,13,5,5q�6X   $��11(�6j   $��70��6x     CmpStandarDataGrid#���  6�   $��Bottom=�6�      Columns 5�      4  .System.Windows.Controls.DataGridTemplateColumn  5�      -   5        dtgDetalleColumnEliminar��,	  &System.Windows.Controls.DataGridColumn  	  MinWidth$  20Q�5�   W   ! 	  Width5
  /System.Windows.Controls.DataGridLengthConverter$! 20
 6e   " 	  Header$"  ��6p   #   CellTemplate# 5�   "   �� 5�   &   .R�5�   *   �� $ dtgDetalleColumnEliminar��61   -   5          CmpDataViewButtonEliminar03#��[ 5�   S   $��15��6�   $��15��6�   $ Eliminar��6�   6�   5�   '   5�      0  *System.Windows.Controls.DataGridTextColumn  5�      $" Serie - Número��61   $	  100Q�6i   $! *
 6x   $ 	  
IsReadOnly$ . 1  +System.Windows.Controls.DataGridBoundColumn%   Binding% 6I   �� *SerieNumero+6�     5�      -   5         dtgDetalleColumnGuiaRemision��$" Guía Remision��5�   W   $	  105Q�6�   $ . % 6n   �� *GuiaRemision+6�   5�      5�      �� 5�      -   5        & ��Name$& chkPlanilla��5�      $6��0Planilla, Ingreso de Montos Totales a Distribuir��60   $��5q�6k   $y�	Collapsed=�6v   6�   5�      � 5�      $��0��6   $��2��6    $��4��61    5�      �� 5�      $	��70* �6'   �� 5�      $
��105* �6'    5�      �� 5�      $
��Auto �6$   �� 5�      $
��Auto �6$   $��44��62   �� 5�      $
��Auto �6$   $	��131��62   .+�5�      �� $��0��6   $��0��6*   $��2��6:   .+�5�      �� $��	Documento��6    $��5,0,5,0q�61   $��10(�6B   �� 5�      -   5        $ cbxDocumento��5�       $��2��5�       $��1��6-   $��2��6=   $��5,0,5,5q�6Q    . $		 125��5�   2    . #
 �  5�       $ Seleccione Documento��5�       {�.   5�       �� *CodDocumento+$ PropertyChanged=�5�   Q   5�      �� 5�      $��0��6   $��1��6-   .+�5�      �� $��Serie��6    $��5,0,5,0q�6-   $��10(�6>   �� 5�      -   5        $ txtSerie��5�      $��5,0,5,5q�60   E  ?ComputerSystems.WPF.Acciones.Controles.TextBoxs.CmpMaskBehavior'   Mask$	' Any=�5�      ( �� 	MaxLength$( 4��6D   #
 �  5�      $ Serie��5�      ��5�      �� *Serie+$ PropertyChanged=�) �� 	Converter#) [  5�      5�      �� 5�      $��1��6   $��1��6.   .+�5�      �� $��Número��6    $��5,0,5,0q�6.   $��10(�6?   �� 5�      -   5        $ txtCorrelativo��5�      $��5,0,5,5q�66   $	' Any=�5�      $( 10��6D   #
 �  5�      $ Nro Documento��5�      ��5�      �� *Numero+$ PropertyChanged=�#) [  5�      5�      �� 5�      $��2��6   $��0��6*   $��2��6:   .+�5�      �� -   5        $& chkAfectoDetraccion��5�      #
 �  5�      $��Afecto a detracción��5�      $��5q�6=   * d� 	IsChecked* 5�   <   �� *AfectoDetraccion+$ TwoWay=�$ PropertyChanged=�5�   H   � 5�      {�6   �� + �� ElementName$+ chkAfectoDetraccion��, �� Path$, 	IsChecked� 5�      �� 5      $
��Auto �6/   �� 5     $��* �6/   .+�5     �� $��Tasa [%]��6$   $��0��64   $��0��6A   $��5q�6Q   �� 5     -   5        $��txtDetraccion��5  "   $��0��67   $��1��6D   $��5,0,5,5q�6T   I  CComputerSystems.WPF.Acciones.Controles.TextBoxs.CmpMaskBehaviorMVVM-   Mask$- Decimal=�5  "   ��5  f   �� *
Detraccion+$ PropertyChanged=�. �� StringFormat$. ##0.#00��5  "   5     � 5	      5
     �� 5     $��* �6/   �� 5     $��* �6/   .+�5     �� -    5        $& chkAfectoAlmacen��5  #   $��0��6=   #
 �  5     $��Almacén��5     $��5q�61   * 5  M   �� *AfectoAlmacen+$ TwoWay=�$ PropertyChanged=�5  <   �� 5     -!   5        $& chkDistribucion��5  #   $��1��6<   #
 �  5     $��
Distribuir��5     $	��0,5q�64   * 5  L   �� *Distribucion+$ TwoWay=�$ PropertyChanged=�5  A   5     �� 5     -"   5        $ lblTipoDestino��5      $��Tipo Destino��69   $��5,0,0,0q�6M   $��10(�6^   �� 5     -#   5        $ cbxTipoDestino��5     $��2��5     $��1��6*   $	��5,0q�6:    . $		 125��5  /    . #
 �  5     $ Seleccione Tipo Destino��5     $ TipoDestino��5     $ CodTipoDestino��6=    5     �� *ObjTipoDestino.Value+$ PropertyChanged=�5     5      5!     �� 5#     $��1��6   $��1��6&   $��2��66   .+�5$     �� $��Glosa��6   $��0��6)   $��0��66   $��5,0,5,0q�6F   $��10(�6W   �� 5%     -$   5        $ txtObservacion��5%     $��1��62   $��0��6?   $��70��6O   / �� TextWrapping$
/ Wrap=�6[   0 �� AcceptsReturn0 .  . #
 �  5'     $��5,0,5,5q�5)     $ Upper=�6,   $ Ingrese Glosa��5*     ��5(     �� *Glosa+$ PropertyChanged=�5*     5+     5,     � 5.  
   -%   5        $ DetailArticulo��5.     $��0��6'   $��2��67   $y�Visible=�6D    5/     �� 50     $
��Auto �6#   �� 51     $��* �6#   .+�54     � $��0��6   $��2��6    $��0��61   $��0q�6A    55     �� 56     �� 57     .+�5:     �� $��1��6   $��0��6*   .+�5;     O  IComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaAddItem  -&   5        1  Name$1 btnAgregarArtServ��5;  ;   $��5q�6V   {�.  2   ContentTitle$2 Agregar Artículo��6s   6�   P  JComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaImprimir  5<     -'   5        3  Name$3 btnImprimirArtServ��5<  <   $��5q�6X   {�.  6u   M  GComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaSalir  5=     -(   5        4  Name$4 btnSalirArtServ��5=  9   $��5q�6R   6]   5>     �� 5?     $��1��6   $��1��6*   .+�5@     R  LComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaDeleteItem  -)   5        5  Name$5 btnQuitarArtServ��5@  >   $��5q�6X   6   ContentTitle$6 Quitar Artículo��6c   {�.  6�   O  IComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaGuardar  5A     -*   5        7  Name$7 btnGuardarArtServ��5A  ;   $��5q�6V   {�.  6s   5B     5C     � 5F     $��0��6   $��1��6     5G     �� 5H     $��* �6$   �� 5I     $
��Auto �6$   .+�5K       -+   5        $ dgCompraDetalle��5K     $��1��64   $��0��6E   $��5q�6R   #���  6]   8   SelectionUnit$
8 Cell=�5L     G  AComputerSystems.WPF.Acciones.Controles.DataGrids.CmpGridViewFocus9   IsMoveFocus9 .  5N       5O     $
" Item��6-   $  50Q�6T   $! 50
 6b   $ . % 6;   �� *Item+6m     5P     -,   5        dgColumnSerieNum��$" Serie - Número��5P  G   $	  120Q�6   $	! 120
 6�   $ . % 6_   �� *SerieNumero+6�     5Q     $" Código��6-   $	  100Q�6X   $	! 100
 6g   $ . % 6=   �� *Codigo+6s     5R     $" Artículo / Servicio��6-   $	  250Q�6o   $	! 250
 6~   $ . % 6J   �� *ArticuloServicio+6�     5S     $" Unidad��6-   $	  100Q�6^   $	! 100
 6m   $ . % 6=   �� *CodUndMedida+6y     5T     --   5        dgColumnAlmacen��$" Almacén��5T  J   $	  250Q�6[   $	! 250
 6j   # 5U     �� 5V  "   .R�5W  &   �� $ 
cbxAlmacen��6/   $ 	IdAlmacen��5Z  /   $ Almacen��6M   *  $MahApps.Metro.Controls.TextBoxHelper:   	Watermark$: Seleccione Almacén��5[  /   $ Seleccione Almacén��5\  /    5W  C   �� *ObjEALM_Almacen.IdAlmacen+$ PropertyChanged=�{�5X  /   �� *IsEnabledColumnAlmcen+; �� SelectedItem; 5Y  /   �� *ObjEALM_Almacen+$ PropertyChanged=���5Z  i   �� *ListEALM_Almacen+5\  /   5]  #   5_       5`     -.   5        "dgDetalleArtServColumnCantidad��$" Cantidad��5`  Y   $	  160Q�6k   $	! 160
 6z   # 5a     �� 5b  "   .R�5c  &   �� $ npdCantidadArtServ��6.   -/   5        $' Decimal=�5e  .    . < �� TextAlignment$< Right=�5g  .   ��5d  .   �� *
Cantidad+$ PropertyChanged=�$. ###,###,##0.#0000000��{�5h  .   �� *IsEnabledColumnPrecioUnitario+$ PropertyChanged=�5i  .   5j  #   5l       5m     -0   5        dgPrecioUnitario��$" Precio Unitario��5m  K   $	  160Q�6d   $	! 160
 6s   # 5n     �� 5o  "   .R�5p  &   �� $ npdCantidadArtServ��6.   -1   5         . $' Decimal=�5s  .   $< Right=�5t  .   ��5q  .   �� *PrecioUnitario+$ PropertyChanged=�$. ###,###,##0.#0000000��{�5u  .   �� *IsEnabledColumnPrecioUnitario+$ PropertyChanged=�5v  .   5w  #   5y       5z     -2   5        	dgImporte��$" Importe��5z  2   $	  150Q�6C   $
! Auto
 6R   $ . # 5{     �� 5|  "   .R�5}  &   �� = �� TextAlignment$= Right=�6n   ��60   �� *	Importe+$. ###,###,##0.#0000000��6n   5~  #   5�       5�     -3   5        	dgIGV��$	" IGV��5�  @   $	  150Q�6M   $
! Auto
 6\   $ . # 5�     �� 5�  "   .R�5�  &   �� $= Right=�6q   ��60   �� *
ImporteIGV+$. ###,###,##0.#0000000��6q   5�  #   5�       5�     -4   5        dtgColumnaTipoDestino��$  40Q�5�  P   $"  ��6^   > 	  
Visibility$> 	Collapsed=�6h   # 5�     �� 5�  "   .R�5�  &   �� $ Seleccione Tipo Destino��5�  -   $
 Item��5�  -   $ Value��6F   ��5�  /   �� *ListCentroCosto+ 5�  -   �� *	IdDestino+$ PropertyChanged=�5�  F   5�  #   5�       5�     -5   5        dtgColumnaPeriodoCompania��$  40Q�5�  T   $" Periodo Campaña��6b   # 5�     �� 5�  "   .R�5�  &   �� $ cbxPeriodoCampaña��6/   $: Selec. Periodo Campaña��5�  /   $! Seleccione Periodo Campaña��5�  /   $
 Item��5�  /   $ Value��6H   ��5�  J   �� *ListPeriodoCampania+ 5�  /   �� *PeriodoCampania+$ PropertyChanged=�5�  H   5�  #   5�     5�     � 5�     $��1��6   $��1��6$   $��5q�64   $
��Left=�6?   $��60��6Z   .+�5�     �� -6   5        $& chkCajaBanco��5�     $	��0,5q�65   $��0��6B   $��Caja y Banco��6R   $	��Top=�6i   5�     � 5�     $��1��6   $��1��6$   $��5q�64   $��Right=�6?   $��60��6[     5�     ? �� 
TargetType@ �� PropertyA �� ValueB �� 
TargetTypeC J� SnapsToDevicePixelsD �� TriggersE P� PropertyF P� Value��%b  '��       �� 5�     "? ��6    .�5�     �� @ � ��
A �����6;   �� 5�     @ � ��
A �   �6<   �� 5�     @ � ��A 5�  "   �� 5�  &   "B ��66   .R�5�  *   �� $��Bd��61   #��z��6;   #��z��5�  -   #��z��5�  -   C . ._�5�  .   �� $��PART_ContentHost��6;   #��z��6S   #C zC 5�  /   5�  +   D 5�  *   P� 5�  .   E � {�F .  . �5�  2   �� @ � ��
A �   �6G   �� 5�  2   @ � ��
A �����6G   5�  /   P� 5�  .   E � {�F . . �5�  2   �� @ � ��
A �   �6G   �� 5�  2   @ � ��
A �����6G   5�  /   5�  '   5�     5�     5�      5�     �� 5�     $
��Auto �6+   �� 5�     $
��Auto �6+   �� 5�     $
��Auto �6+   �� 5�     $
��Auto �6+   �� 5�     $
��Auto �6+   �� 5�     $
��Auto �6+   �� 5�     $
��Auto �6+   .+�5�     �� $��0��6!   .+�5�     �� -7   5        $& chkAnticipo��5�  #   $��0,5,10,5q�68   $��0��6J   $��Anticipo��6Z   $��Center=�6m   $
��Left=�6�   6�   5�     �� 5�     $��1��6!   .+�5�     �� -8   5        $& chkIncluyeIGV��5�  #   $��5q�6:   $��0��6E   $��Precio Incluye IGV��6U   $��Center=�6r   $��Center=�6�   {�.  * 5�     �� *
IncluyeIGV+$ PropertyChanged=�6e   �� 5�     -9   5        $& chkAfectoPercepcion��5�  #   $��5q�6@   $��0��6K   $��Afecto percepción��6[   $��Center=�6w   $��Center=�6�   6�   �� 5�     -:   5        $& chkRetencion��5�  #   $��5q�69   $��0��6D   $��
Retención��6U   $��Center=�6i   $
��Left=�6�   6�   5�     �� 5�     
���   �$��5q�64   $��2��6?   .+�5�     �� -;   5        $ lblTitleCompra01��5�  $   $��	# Líneas��6>   $��3q�6N   
�������$��Center=�6m   $��Center=�6�   �� 5�     -<   5        $ txtLineasArtServ��5�  +   $��0��6"   
�������$��0q�6X   $
��Bold"�6c   $��15(�6u   $< Center=�6�   $��0,1,0,0q�6�   
���   �G �� 
IsReadOnlyG . 6�   5�     �� 5�     -=   5        $ ColumnCal01��5�  !   $y�Visible=�66   
���   �$��5q�6^   $��3��6i   $	��110��6y   .+�5�     �� ->   5        $ lblTitleCompra02��5�  $   $��	Exonerado��6>   $��3q�6O   
�������$��Center=�6n   $
��Left=�6�   �� 5�     -?   5        $ txtExoneradoArtServ��5�  "   $
��0.00��6?   
�������$��0q�6^   $
��Bold"�6i   $��15(�6{   
���   �$��0,1,0,0q�6�   $' Decimal=�5�  
   H   MaximumDecimalNumber$H 2��64   $< Right=�5�  F   5�     �� 5�     
���   �$��5q�64   $��4��6?   $	��110��6O   .+�5�     �� -@   5        $ lblTitleCompra03��5�  $   $��Gravada��6>   $��3q�6M   
�������$��Center=�6l   $
��Left=�6�   �� 5�     -A   5        $ txtGravadaArtServ��5�  "   $
��0.00��6=   
�������$��0q�6\   $
��Bold"�6g   $��15(�6y   $��Center=�6�   $��Right=�5�     
���   �$��0q�6N   $< Right=�6b   G . 5�     5�     �� 5�     
���   �$��5q�64   $��5��6?   $	��110��6O   .+�5�     �� -B   5        $ lblTitleCompra04��5�  $   $��IGV 18%��6>   $��3q�6M   
�������$��Center=�6l   $
��Left=�6�   �� 5�     -C   5        $ txtTotalIgvArtServ��5�  "   $
��0.00��6>   
�������$��0q�6]   $
��Bold"�6h   $��15(�6z   $��Center=�6�   $��Right=�5�     
���   �$��0q�6N   $< Right=�6b   G . 5�     5�     �� 5�     
���   �$��5q�64   $��6��6?   $	��110��6O   .+�5�     �� -D   5        $ lblTitleCompra05��5�  $   $��Total ��6>   $��3q�6L   
�������$��Center=�6k   $
��Left=�6�   �� 5�     -E   5        $ txtTotalNetoArtServ��5�  "   $
��0.00��6?   
�������$��0q�6_   $
��Bold"�6j   $��15(�6|   
���   �$��0,1,0,0q�6�   $< Right=�6�   G . 6�   5�     5�     5�     5�     5�     5�     