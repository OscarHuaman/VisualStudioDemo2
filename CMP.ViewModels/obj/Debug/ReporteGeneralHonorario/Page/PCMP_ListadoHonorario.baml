   M S B A M L   `   `   `  ����N  JComputerSystems.WPF, Version=3.0.0.0, Culture=neutral, PublicKeyToken=null�Yclr-namespace:ComputerSystems.WPF.Acciones.Controles.Buttons;assembly=ComputerSystems.WPF.ComputerSystems.WPF.Acciones.Controles.Buttons  5      �Sclr-namespace:ComputerSystems.WPF.Acciones.TitleModule;assembly=ComputerSystems.WPF(ComputerSystems.WPF.Acciones.TitleModule  5      �Zclr-namespace:ComputerSystems.WPF.Acciones.Controles.TextBoxs;assembly=ComputerSystems.WPF/ComputerSystems.WPF.Acciones.Controles.TextBoxs  5      �Xclr-namespace:ComputerSystems.WPF.Acciones.Controles.Period;assembly=ComputerSystems.WPF-ComputerSystems.WPF.Acciones.Controles.Period  5	       CMP.ViewModelsG   ACMP.ViewModels.ReporteGeneralHonorario.Page.PCMP_ListadoHonorario   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      o	CmpButtonYclr-namespace:ComputerSystems.WPF.Acciones.Controles.Buttons;assembly=ComputerSystems.WPF     5      kTitleModuleSclr-namespace:ComputerSystems.WPF.Acciones.TitleModule;assembly=ComputerSystems.WPF     5      q
CmpTextBoxZclr-namespace:ComputerSystems.WPF.Acciones.Controles.TextBoxs;assembly=ComputerSystems.WPF     5      o
CmpPeriodoXclr-namespace:ComputerSystems.WPF.Acciones.Controles.Period;assembly=ComputerSystems.WPF     5	      H DMahApps.Metro, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null;Controls,http://metro.mahapps.com/winfx/xaml/controls  5
        N� Title$   Reporte General Docuemntos��5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      �� 5      $
��Auto �6   .+�5   
   <   6ComputerSystems.WPF.Acciones.TitleModule.MyTitleModule  $��0��6$   $��0��61   $��2��6A   $��5q�6U      
NameModule$ Compra��6a      
NameWindow$# Reporte General de Honorarios��6u      Logo$. (/CMP.ViewModels;component/Source/ima.png��6�   � 5   
   $��1��6   $��0��6    � ColumnDefinitions 5      �� 5      $	��100 �6#   �� 5      $	��200 �6#   �� 5      �� 5      $	��300 �6#   �� 5      $
��Auto �6#   .+�5      �� $��0��6   .+�5       �� $��Periodo��6   $��10(�6+   $��5,0,5,0q�69   =   7ComputerSystems.WPF.Acciones.Controles.Period.CmpPeriod  5(      $��5,0,5,5q�6'      ItemsSource 68   �� *#!DataContext.ListPeriodoCmpPeriodo+ �� RelativeSource �� *FindAncestor+ �� AncestorType$
 PageL�	 �� Mode$	 TwoWay=�
 �� UpdateSourceTrigger$
 PropertyChanged=�   SelectedItem 5)   '   �� *%#DataContext.SelectPeriodoCmpPeriodo+ �� *FindAncestor+$
 PageL�5*      �� 5+      $��1��6   .+�5,      �� $��Opción��6   $��5,0,5,0q�6*   $��10(�6;   �� 5-      -   5         ��Name$ 	cbxOpcion��5-      $��5,0,5,5q�6/    �� 
IsEditable .  �� MaxDropDownHeight$	 125��5.   -   F   @ComputerSystems.WPF.Acciones.Controles.TextBoxs.FocusAdvancement   AdvancesByEnterKey .    AccentColorBrush+  %MahApps.Metro.Controls.ControlsHelper   FocusBorderBrush# �   50       �� SelectedValuePath$ Value��53       �� DisplayMemberPath$
 Item��65   ��51      �� *DataContext.ListOpciones+ �� *FindAncestor+$
 PageL� �� SelectedItem 52      �� *DataContext.SelectOpcion+ �� *FindAncestor+$
 PageL�53   5   54      �� 55      $��2��6   .+�56      �� $��Proveedor [Control + B]��6   $��5,0,5,0q�6;   $��10(�6L   �� 57      -   5         ��Name$ txtProvetedor��57      $��5,0,5,5q�61   # �   58       . ��5:      �� *RazonSocial+$
 PropertyChanged=�*  $MahApps.Metro.Controls.TextBoxHelper   ButtonCommand 5;      �� *DataContext.IBuscarProveedor+ �� *FindAncestor+$
 PageL�   ButtonCommandParameter 5<      ��  �� ElementName$ txtProvetedor�� �� Path$
 Text�.��6y    J� InputBindings 5>      �� 5?      
 �� Key$ B��6�    �� 	Modifiers$ Controlf�6�    �� Command 6%   �� *DataContext.IBuscarProveedor+ �� *FindAncestor+$
 PageL� �� CommandParameter 6�   �� $ txtProvetedor��$
 Text�6�   .��5@   -   5A      5B      �� 5C      $��3��6   .+�5D      �� $��Moneda��6   $��5,0,5,0q�6*   $��10(�6;   �� 5E      -   5        $ 	cbxMoneda��5E      $��5,0,5,5q�6.    . $	 125��5F   -   # �   5G       . $ 	CodMoneda��5K      $ Descripcion��69   ��5I      �� *DataContext.ListESGC_Moneda+ �� *FindAncestor+$
 PageL� 5J      �� *DataContext.SelectMoneda+ �� *FindAncestor+$
 PageL�5K   9   5L      5M      &   System.Windows.Controls.DataGrid  5N   
   -   5          Name$ dtgListDocumentos��5N      $��0��6.   $��2��6?   $��5q�6L     CmpStandarDataGrid#��[ 6W   ��6�   �� *ListECMP_ReporteGrlHonorario+   Columns 5O      0  *System.Windows.Controls.DataGridTextColumn  5P      ,  &System.Windows.Controls.DataGridColumn    Header$  Fecha Emisión��6%   !   MinWidth$	! 100Q�6v   "   Width5	  /System.Windows.Controls.DataGridLengthConverter$	" 100	 6�   #   
IsReadOnly# . 1
  +System.Windows.Controls.DataGridBoundColumn$ 
  Binding$ 6<   �� *FechaEmision+% �� StringFormat$% 
dd/MM/yyyy��6�     5Q      $  Fecha Contable��6%   $	! 100Q�6x   $	" 100	 6�   # . $ 6=   �� *FechaContable+$% 
dd/MM/yyyy��6�     5R      $  	Documento��6%   $	! 100Q�6j   $
" Auto	 6y   # . $ 68   �� *ObjESGC_Documento.Descripcion+6�     5S      $  Serie - Número��6%   $	! 100Q�6]   $
" Auto	 6l   # . $ 6=   �� *SerieNumero+6y     5T      $  	Proveedor��6%   $	! 150Q�6q   $
" Auto	 6�   # . $ 68   �� *&$ObjEMNF_ClienteProveedor.RazonSocial+6�     5U      $  Nro. Documento��6%   $	! 100Q�6z   $
" Auto	 6�   # . $ 6=   �� **(ObjEMNF_ClienteProveedor.NroDocIdentidad+6�     5V      $  Moneda��6%   $! 50Q�6d   $
" Auto	 6r   # . $ 65   �� *ObjESGC_Moneda.Descripcion+6   4  .System.Windows.Controls.DataGridTemplateColumn  5W      $  Tipo Cambio��6)   $! 50Q�6>   $
" Auto	 6L   # . &   CellTemplate& 5X      �� 5Y      .R�5Z      �� ' �� TextAlignment$' Right=�6g   ��6(   �� *
TipoCambio+$% #,###,###,###0.#000��6g   5[      5]        5^      $  Fecha Recepción��6%   $	! 120Q�6z   $	" 120	 6�   # . $ 6>   �� *FechaRecepcion+$% 
dd/MM/yyyy��6�     5_      $  Total Honorario��6)   $	! 120Q�6B   $
" Auto	 6Q   # . & 5`      �� 5a      .R�5b      �� $' Right=�6g   ��6(   �� *
THonorario+$% #,###,###,###0.#000��6g   5c      5e        5f      $  Porcentaje Retenido��6)   $	! 120Q�6F   $
" Auto	 6U   # . & 5g      �� 5h      .R�5i      �� $' Right=�6D   ��6(   �� *
PRetencion+6D   5j      5l        5m      $  Monto Retenido��6)   $	! 120Q�6A   $
" Auto	 6P   # . & 5n      �� 5o      .R�5p      �� $' Right=�6g   ��6(   �� *
MRetencion+$% #,###,###,###0.#000��6g   5q      5s        5t      $  
Total Neto��6)   $	! 120Q�6=   $
" Auto	 6L   # . & 5u      �� 5v      .R�5w      �� $' Right=�6f   ��6(   �� *	TotalNeto+$% #,###,###,###0.#000��6f   5x      5z        5{      $  Glosa��6%   $	! 100Q�6N   $	" 100	 6]   # . $ 64   �� *Glosa+6i   5}      � 5~   
   $��3��6   $��0��6   $��Right=�6,    5      �� 5�      �� 5�      �� 5�      .+�5�      P   JComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaImprimir  $��0��64   $��0��6A   $��5q�6Q   (   	IsCommand( 6]   �� *	IImprimir+U   OComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaExportarExcel  5�      $��0��69   $��1��6F   $��5q�6V   )   	IsCommand) 6b   �� *	IExportar+M   GComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaSalir  5�      $��0��61   $��2��6>   $��5q�6N   *   	IsCommand* 6Y   �� *IClose+5�      � 5�   
   $��3��6    5�      �� 5�      $
��Auto �6#   �� 5�      .+�5�      �� $	��5,0q�6   $��14(�6%   ��63   �� *Items.Count+$ dtgListDocumentos���� 5�      $��1��6   $��14(�6(   $��	Registros��66   $��	0,0,248,0q�6G   5�      5�      5�      