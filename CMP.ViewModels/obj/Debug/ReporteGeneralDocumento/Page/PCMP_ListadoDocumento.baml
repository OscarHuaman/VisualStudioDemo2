   M S B A M L   `   `   `  ����N  JComputerSystems.WPF, Version=3.0.0.0, Culture=neutral, PublicKeyToken=null�Yclr-namespace:ComputerSystems.WPF.Acciones.Controles.Buttons;assembly=ComputerSystems.WPF.ComputerSystems.WPF.Acciones.Controles.Buttons  5      �Sclr-namespace:ComputerSystems.WPF.Acciones.TitleModule;assembly=ComputerSystems.WPF(ComputerSystems.WPF.Acciones.TitleModule  5      �Zclr-namespace:ComputerSystems.WPF.Acciones.Controles.TextBoxs;assembly=ComputerSystems.WPF/ComputerSystems.WPF.Acciones.Controles.TextBoxs  5      �Xclr-namespace:ComputerSystems.WPF.Acciones.Controles.Period;assembly=ComputerSystems.WPF-ComputerSystems.WPF.Acciones.Controles.Period  5	       CMP.ViewModelsG   ACMP.ViewModels.ReporteGeneralDocumento.Page.PCMP_ListadoDocumento   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      o	CmpButtonYclr-namespace:ComputerSystems.WPF.Acciones.Controles.Buttons;assembly=ComputerSystems.WPF     5      kTitleModuleSclr-namespace:ComputerSystems.WPF.Acciones.TitleModule;assembly=ComputerSystems.WPF     5      q
CmpTextBoxZclr-namespace:ComputerSystems.WPF.Acciones.Controles.TextBoxs;assembly=ComputerSystems.WPF     5      o
CmpPeriodoXclr-namespace:ComputerSystems.WPF.Acciones.Controles.Period;assembly=ComputerSystems.WPF     5	      H DMahApps.Metro, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null;Controls,http://metro.mahapps.com/winfx/xaml/controls  5
        N� Title$   Reporte General Docuemntos��5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      �� 5      $
��Auto �6   .+�5   
   <   6ComputerSystems.WPF.Acciones.TitleModule.MyTitleModule  $��0��6$   $��0��61   $��2��6A   $��5q�6U      
NameModule$ Compra��6a      
NameWindow$# Reporte General de Documentos��6u      Logo$. (/CMP.ViewModels;component/Source/ima.png��6�   � 5   
   $��1��6   $��0��6    � ColumnDefinitions 5      �� 5      $	��100 �6#   �� 5      $	��200 �6#   �� 5      �� 5      $	��150 �6#   �� 5      $	��200 �6#   �� 5      $
��Auto �6#   .+�5       �� $��0��6   .+�5!      �� $��Periodo��6   $��10(�6+   $��5,0,5,0q�69   =   7ComputerSystems.WPF.Acciones.Controles.Period.CmpPeriod  5)      $��5,0,5,0q�6'      ItemsSource 68   �� *#!DataContext.ListPeriodoCmpPeriodo+ �� RelativeSource �� *FindAncestor+ �� AncestorType$
 PageL�	   SelectedItem	 5*   '   �� *%#DataContext.SelectPeriodoCmpPeriodo+ �� *FindAncestor+$
 PageL�5+      �� 5,      $��1��6   .+�5-      �� $��Opción��6   $��5,0,5,0q�6*   $��10(�6;   �� 5.      -   5        
 ��Name$
 	cbxOpcion��5.      $��5,0,5,5q�6/    �� 
IsEditable .  �� MaxDropDownHeight$	 125��5/   -   F   @ComputerSystems.WPF.Acciones.Controles.TextBoxs.FocusAdvancement   AdvancesByEnterKey .    AccentColorBrush+  %MahApps.Metro.Controls.ControlsHelper   FocusBorderBrush# �   51       �� SelectedValuePath$ Value��54       �� DisplayMemberPath$
 Item��65   ��52      �� *DataContext.ListOpciones+ �� *FindAncestor+$
 PageL� �� SelectedItem 53      �� *DataContext.SelectOpcion+ �� *FindAncestor+$
 PageL�54   5   55      �� 56      $��2��6   .+�57      �� $��Proveedor [Control + B]��6   $��5,0,5,0q�6;   $��10(�6L   �� 58      -   5         ��Name$ txtProveedor��58      $��5,0,5,5q�60   # �   59       . ��5;      �� *RazonSocial+ �� UpdateSourceTrigger$ PropertyChanged=�*  $MahApps.Metro.Controls.TextBoxHelper   ButtonCommand 5<      �� *DataContext.IBuscarProveedor+ �� *FindAncestor+$
 PageL�   ButtonCommandParameter 5=      ��  �� ElementName$ txtProveedor�� �� Path$
 Text�.��6y    J� InputBindings 5?      �� 5@      
 �� Key$ B��6�    �� 	Modifiers$ Controlf�6�    �� Command 6%   �� *DataContext.IBuscarProveedor+ �� *FindAncestor+$
 PageL� �� CommandParameter 6�   �� $ txtProveedor��$
 Text�6�   .��5A   -   5B      5C      �� 5D      $��3��6   .+�5E      �� $��Moneda��6   $��5,0,5,0q�6*   $��10(�6;   �� 5F      -   5        $
 	cbxMoneda��5F      $��5,0,5,5q�6.    . $	 125��5G   -    . # �   5I      $ 	CodMoneda��5L      $ Descripcion��69   ��5J      �� *DataContext.ListESGC_Moneda+ �� *FindAncestor+" N� 5K      �� *DataContext.SelectMoneda+ �� *FindAncestor+" N�5L   9   5N      �� 5O      $��4��6   .+�5P      �� $��	Documento��6   $��5,0,5,0q�6-   $��10(�6>   �� 5Q      -   5        $
 cbxDocumento��5Q      $��5,0,5,5q�61    . $	 125��5R   -    . # �   5T      $ CodDocumento��5W      $ Descripcion��6<   ��5U      �� * DataContext.ListESGC_Documento+ �� *FindAncestor+$
 PageL� 5V      �� *DataContext.SelectDocumento+ �� *FindAncestor+$
 PageL�5W   <   5X      5Y      &   System.Windows.Controls.DataGrid  5Z   
   -   5          Name$ dtgListDocumentos��5Z      $��2��6.   $��0��6;   $��5q�6K     CmpStandarDataGrid#��[ 6V   ��6�   �� *ListECMP_ReporteGrlDocumento+   Columns 5[      0  *System.Windows.Controls.DataGridTextColumn  5\      ,  &System.Windows.Controls.DataGridColumn   Header$ Fecha Emisión��6%       MinWidth$	  100Q�6v   !   Width5	  /System.Windows.Controls.DataGridLengthConverter$	! 100	 6�   "   
IsReadOnly" . 1
  +System.Windows.Controls.DataGridBoundColumn# 
  Binding# 6<   �� *FechaEmision+$ �� StringFormat$$ 
dd/MM/yyyy��6�     5]      $ Fecha Contable��6%   $	  100Q�6x   $	! 100	 6�   " . # 6=   �� *FechaContable+$$ 
dd/MM/yyyy��6�     5^      $ 	Documento��6%   $	  100Q�6j   $
! Auto	 6y   " . # 68   �� *ObjESGC_Documento.Descripcion+6�     5_      $ Serie - Número��6%   $	  100Q�6]   $
! Auto	 6l   " . # 6=   �� *SerieNumero+6y     5`      $ 	Proveedor��6%   $	  180Q�6q   $
! Auto	 6�   " . # 68   �� *&$ObjEMNF_ClienteProveedor.RazonSocial+6�     5a      $ Nro. Documento��6%   $  50Q�6z   $
! Auto	 6�   " . # 6=   �� **(ObjEMNF_ClienteProveedor.NroDocIdentidad+6�     5b      $ Moneda��6%   $  50Q�6d   $
! Auto	 6r   " . # 65   �� *ObjESGC_Moneda.Descripcion+6   4  .System.Windows.Controls.DataGridTemplateColumn  5d      $ Tipo Cambio��6)   $  50Q�6>   $
! Auto	 6L   " . %   CellTemplate% 5e      �� 5f      .R�5g      �� & �� TextAlignment$& Right=�6g   ��6(   �� *
TipoCambio+$$ #,###,###,###0.#000��6g   5h      5j        5k      $ Fecha Recepción��6%   $	  100Q�6z   $
! Auto	 6�   # 6>   �� *FechaRecepcion+$$ 
dd/MM/yyyy��6�     5m      $ 	Exonerado��6)   $	  100Q�6<   $
! Auto	 6K   " . % 5n      �� 5o      .R�5p      �� $& Right=�6f   ��6(   �� *	Exonerada+$$ #,###,###,###0.#000��6f   5q      5s        5u      $ Gravada��6)   $	  100Q�6:   $
! Auto	 6I   " . % 5v      �� 5w      .R�5x      �� $& Right=�6d   ��6(   �� *	Gravada+$$ #,###,###,###0.#000��6d   5y      5{        5}      $
 %IGV��6)   $	  100Q�67   $
! Auto	 6F   " . % 5~      �� 5      .R�5�      �� $& Right=�6a   ��6(   �� *PIGV+$$ #,###,###,###0.#000��6a   5�      5�        5�      $	 IGV��6)   $	  100Q�66   $
! Auto	 6E   " . % 5�      �� 5�      .R�5�      �� $& Right=�6`   ��6(   �� *IGV+$$ #,###,###,###0.#000��6`   5�      5�        5�      $ Total��6)   $	  100Q�68   $
! Auto	 6G   " . % 5�      �� 5�      .R�5�      �� $& Right=�6b   ��6(   �� *Total+$$ #,###,###,###0.#000��6b   5�      5�        5�      $ %Percepción��6)   $	  100Q�6>   $
! Auto	 6M   " . % 5�      �� 5�      .R�5�      �� $& Right=�6h   ��6(   �� *PPercepcion+$$ #,###,###,###0.#000��6h   5�      5�        5�      $ Percepción��6)   $	  100Q�6=   $
! Auto	 6L   " . % 5�      �� 5�      .R�5�      �� $& Right=�6g   ��6(   �� *
Percepcion+$$ #,###,###,###0.#000��6g   5�      5�        5�      $ Otros Cargos��6)   $	  100Q�6?   $
! Auto	 6N   " . % 5�      �� 5�      .R�5�      �� $& Right=�6d   ��6(   �� *	OCargos+$$ #,###,###,###0.#000��6d   5�      5�        5�      $ Importe Total��6)   $	  100Q�6@   $
! Auto	 6O   " . % 5�      �� 5�      .R�5�      �� $& Right=�6e   ��6(   �� *
ImpTotal+$$ #,###,###,###0.#000��6e   5�      5�        5�      $ %Detracción ��6)   $	  100Q�6?   $
! Auto	 6N   " . % 5�      �� 5�      .R�5�      �� $& Right=�6D   ��6(   �� *
Detraccion+6D   5�      5�        5�      $ Detracción ��6)   $	  100Q�6>   $
! Auto	 6M   " . % 5�      �� 5�      .R�5�      �� $& Right=�6h   ��6(   �� *PDetraccion+$$ #,###,###,###0.#000��6h   5�      5�        5�      $ Glosa ��6)   $	  100Q�69   $
! Auto	 6H   " . % 5�      �� 5�      .R�5�      �� $
& Left=�6?   ��6(   �� *Glosa+6?   5�      5�      5�      � 5�   
   $��3��6   $��0��6   $��Right=�6,    5�      �� 5�      �� 5�      �� 5�      .+�5�      P   JComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaImprimir  $��0��64   $��0��6A   $��5q�6Q   '   	IsCommand' 6]   �� *	IImprimir+U   OComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaExportarExcel  5�      $��0��69   $��1��6F   $��5q�6V   (   	IsCommand( 6b   �� *	IExportar+M   GComputerSystems.WPF.Acciones.Controles.Buttons.CmpButtonTitleTeclaSalir  5�      $��0��61   $��2��6>   $��5q�6N   )   	IsCommand) 6Y   �� *IClose+5�      � 5�   
   $��3��6    5�      �� 5�      $
��Auto �6#   �� 5�      .+�5�      �� $	��5,0q�6   $��14(�6%   ��63   �� *Items.Count+$ dtgListDocumentos���� 5�      $��1��6   $��14(�6(   $��	Registros��66   $��	0,0,248,0q�6G   5�      5�      5�      