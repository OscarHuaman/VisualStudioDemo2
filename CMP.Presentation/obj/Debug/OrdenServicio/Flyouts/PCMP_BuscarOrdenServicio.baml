   M S B A M L   `   `   `  ����H  DMahApps.Metro, Version=1.1.3.0, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      N JComputerSystems.WPF, Version=3.0.0.0, Culture=neutral, PublicKeyToken=null�Yclr-namespace:ComputerSystems.WPF.Acciones.Controles.Buttons;assembly=ComputerSystems.WPF.ComputerSystems.WPF.Acciones.Controles.Buttons 5       CMP.PresentationE   ?CMP.Presentation.OrdenServicio.Flyouts.PCMP_BuscarOrdenServicio   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      Pcontrols;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5      ;Controls,http://metro.mahapps.com/winfx/xaml/controls   5      0sys&clr-namespace:System;assembly=mscorlib   5      i	CmpButtonYclr-namespace:ComputerSystems.WPF.Acciones.Controles.Buttons;assembly=ComputerSystems.WPF   5      :d2http://schemas.microsoft.com/expression/blend/2008   5	      $	��600��5      #   MahApps.Metro.Controls.Flyout    IsModal  .    Theme$ Light=�5         Header$ Buscar Orden Servicio��5         Position$ Right=�5      .��5      � $��5q�6    � ColumnDefinitions 5   
   �� 5      $��* �6     � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6   $��0��6    5      �� 5       $��2* �6#   �� 5!      .+�5%      �� $��0��6   $��0��6&   .+�5&      � -   5         �Name$ GridFriltrarFecha��5&      $y�Visible=�62    5'      �� 5(      �� 5)      .+�5+      (  "System.Windows.Controls.DatePicker  -   5          Name$ dtpFechaDesde��5+   !   $��0��68   $��0��6H   $��5q�6U      AccentColorBrush+   %MahApps.Metro.Controls.ControlsHelper   FocusBorderBrush# �   5,      *   $MahApps.Metro.Controls.TextBoxHelper	   	Watermark$	 Fecha Desde��5-      
 � ToolTip$
 Seleccione Fecha Desde��5.      
 �� Now   DisplayDateEnd# Z 6:     5/      -   5        $ dtpFechaHasta��5/   !   $��1��68   $��0��6H   $��5q�6U   # �   50      $	 Fecha Hasta��51      # Z 6I   $
 Seleccione Fecha Hasta��53         DisplayDateStart 52      ��  �� Path$ SelectedDate� �� ElementName$ dtpFechaDesde��53      54      �� 56      -   5         ��Name$ 
txtFiltrar��56      $��1��6.   $��0��6;   $��3��6K   $��5q�6_   $y�Visible=�6j   # �   57        SearchMetroTextBox#���  58      $#	 Filtrar por Número documento��59       �� CharacterCasing$ Upper=�6[      ClearTextButton . $#
 Filtrar por Número documento��5;      5<      �� 5>      $��0��6   $��1��6&   .+�5?      �� -   5         ��Name$ 	cbxOpcion��5?      $��5q�6/   $��Center=�6:    �� 
IsEditable .  �� MaxDropDownHeight$	 125��5@   -   # �   5A      $$
 Seleccione Opción de busqueda��5B      .@�5C      ��  �� 
IsSelected . .��66   Fecha6=   �� 5D      .��6#   Moneda6+   �� 5E      .��6#   	Documento6.   5F      5G      5H      &   System.Windows.Controls.DataGrid  5I   
   -   5          Name$ dtgOrdenServicio��5I      $��2��6-   $	��200��6:   $��5q�6J     CmpStandarDataGrid#��[ 6V      Columns 5J      4  .System.Windows.Controls.DataGridCheckBoxColumn  5K        MetroDataGridCheckBox1  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# �  6)      EditingElementStyle# �  5L   )   ,  &System.Windows.Controls.DataGridColumn   Header$ 
Selección��5M   )      Width5	  /System.Windows.Controls.DataGridLengthConverter$ 75	 6<      MinWidth$ 75Q�6G      
Visibility$ 	Collapsed=�6U      Binding 5N   )   ��   �� RelativeSource  �� ! �� AncestorType$! DataGridRowL�$ 
IsSelected�" �� Mode$" OneWay=�0
  *System.Windows.Controls.DataGridTextColumn
  5O      $ Razón social proveedor��6%   #   
IsReadOnly# . $	 150Q�6�   $ *	 6�    6E   �� *&$ObjEMNF_ClienteProveedor.RazonSocial+6�   
  5P      $ Moneda��6%   # . $	 150Q�6v   $ *	 6�    65   �� *ObjESGC_Moneda.Descripcion+6�   
  5Q      $ N° Documento��6%   # . $	 110Q�6h   $	 110	 6x    6;   �� *Numero+6x   
  5R      $ Fecha��6%   # . $	 100Q�6y   $	 100	 6�    64   �� *Fecha+$ �� StringFormat$$ 
dd/MM/yyyy��6�   
  5S      $ Estado��6%   # . $	 100Q�6q   $	 100	 6�    65   �� *ObjESGC_Estado.Estado+6�   5V      �� 5W   
   -   5        % ��Name$% lblCountItems��5W      $��3��6+   $��1��68   $��5q�6H   $��0 Registros��6S   � 5X   
   $��4��6   $��0��6   $��5q�6,   $��Right=�67    5Y      �� 5Z      $
��Auto �6#   �� 5[      $
��Auto �6#   .+�5^      S  MComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaSeleccionar  -   5        &  Name$& 
btnAceptar��5^   7   $��0��6K   $��0��6X   $��Right=�6h   $��5q�6�   6�   N  HComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaVolver  5_      -	   5        '  Name$' 	btnVolver��5_   2   $��0��6E   $��1��6R   $��Right=�6b   $��5,5,0,5q�6~   6�   5`      5a      5b      