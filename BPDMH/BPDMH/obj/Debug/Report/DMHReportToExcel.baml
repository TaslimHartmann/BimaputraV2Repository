   M S B A M L   `   `   `  ����@  <BPDMH, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:BPDMH.ToolsBPDMH.Tools  5   	   I EExtendedButton, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG4clr-namespace:ExtendedButton;assembly=ExtendedButtonExtendedButton 5   	   #    BPDMH.Report.DmhReportToExcel   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   +toolsclr-namespace:BPDMH.Tools     5   	   OextendedButton4clr-namespace:ExtendedButton;assembly=ExtendedButton     5   	   -   5        $	��580��5   	   $	��819��6     � WindowStyle$  
ToolWindow=�6"    � SizeToContent$ WidthAndHeight=�6;    � Title$ DAFTAR MUAT HARIAN��5   	    � WindowStartupLocation$ CenterScreen=�5   	   $��Arial)�6.    � 	Resources 5
         
MasterView  
DetailView �� Source �� Source �� Path  KendaraanLookup  KaryawanLookup  KaryawanLookup1  KaryawanLookup2  CabangLookup  PenerimaLookup:  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType	 �� Property
 �� Value ��Name �� StringFormat �� RelativeSource �� AncestorType(  "System.Windows.Controls.DatePicker��%�  &	        &	      7   &	 Q     &	 d     &	 w     &	 �     &	 �     &	 �     '  �     �� 5   
   6   �� 5   
    6   �� 8   $ TdByPengIdResults�6o   �� 5   
   6   �� 5   
   6   �� 5   
   6   �� 5   
   6   �� 5   
   6   �� 5   
   6   �� 5   
   "  6   .�5      �� 	 � ��
 5      �� 5      .R�5      �� $ PART_TextBox��6"   ��5      �� $ SelectedDate�$ 
dd/MM/yyyy�� �� "  5      5      5      5      .��5!      � -   5        $��MainGrid��5!      $
��Left=�6   $	��814��66   $��0,0,-3,0q�5"   F    +� FocusedElement 5!   B   ��  �� ElementName$ TbNoSp�� � DataContext 5"      �� # [   � RowDefinitions 5#   
   �� 5$      $��42 �6   �� 5%      $	��158 �6   �� 5&      $��45 �6   �� 5'      $	��157 �6   �� 5(      $��98 �6   �� 5)      $��50 �6   .+�5.   
   � -   5         �Name$ Grid1��5.      $��1��6   $��	0,0,-1,10q�6+    � ColumnDefinitions 5/      �� 50      $��95 �6#   �� 51      $	��290 �6#   �� 52      $	��100 �6#   �� 53      $	��310 �6#   �� 54      .+�56      �� -   5         ��Name$ StackPanelFirstColumn��56      $��0��68    �� Orientation$ Vertical=�6H   .+�57      �� $��Tujuan��6   $	��Top=�6)   $��Right=�6H   $��Center=�6k   $��95��6�   $��Arial)�6�   $��83��6�   $��Right=�6�   $��0,5,0,0q�6�   �� 58      $��	Kendaraan��6   $��Center=�6,   $��Right=�6N   $��Center=�6q   $	��115��6�   $��Arial)�6�   $��25��6�   $��Right=�6�   $��	-20,5,0,0q�6�   59      �� 5:      -   5        $ StackPanelSecondColumn��5:      $��1��69   $ Vertical=�6I   .+�5<      �� $ 
Horizontal=�6   $��0,5,0,0q�66   .+�5=         BPDMH.Tools.LivePreviewCb  -   5          Name$ 	CboCabang��5=   6   $��23��6*   $��70��6I    �� SelectedValuePath$ CabangId��5>      $��Center=�6$    �� DisplayMemberPath$ CabangId��5?      $��5,0,0,0q�6$    �� 
IsEditable . $y�	Collapsed=�6G   ��5=   T   �� # [ 5?   G   �� 5@      -   5        $ TbTujuan��5@      $��Center=�60   $��5,0,0,0q�6R   $	��145��6c   $��Arial)�6o   $��25��6�   $	��Top=�6�   {�.  $y�	Collapsed=�5A   �   ��6   �� *SelectedItem.NmCabang+$ 	CboCabang�� �� Mode$ OneWay=� �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5C      -   5        $��ListBoxCabang��5C      $��83��63    �� SelectionMode$ Multiple=�5D      $	��280��63   $��5,0,0,0q�6?   ��5C   ?   �� *Cabang+ �� SelectedItem 5E      �� $ 	IsChecked�$ TwoWay=���5F      �� 5G      .R�5H   "   A�  A� Columns$ 3��6.   5I      ��5K      �� 5L      .R�5N   "   �� $��CheckBoxCabang��6+   $��5,5,0,0q�5P   7   ��5N   A   �� *
CabangId+
  � Tag  6^   �� *
CabangId+! d� 	IsChecked! 5O   %   ��  �� " ��$ 
IsSelected�5P   7   5Q      5S      5U      �� 5V      $ 
Horizontal=�6   $��25��66   $��0,5,0,0q�6B   .+�5W        -	   5        $ CboKendaraan��5W   6   $��23��6*   $	��105��6L   $ KendaraanId��5X      $��Center=�6'   $ KendaraanId��5Y      $��5,1,0,1q�6'    . ��5W   X   �� # [ 5Y   8   �� 5Z      -
   5        $ TbKendaraan��5Z      $��Center=�63   $��5,0,0,0q�6U   $	��170��6f   $��Arial)�6r   {�.  ��5[      �� *SelectedItem.NoPolisi+$ CboKendaraan��$ OneWay=�$ PropertyChanged=�6z   5\      5^      �� 5`      -   5        $ StackPanelThirdColumn��5`      $��2��68   $ Vertical=�6H   .+�5a      �� $��
Tgl. Kirim��6   $��Center=�6-   $��Right=�6O   $��Center=�6r   $	��110��6�   $��Arial)�6�   $��25��6�   $��Right=�6�   $��0,5,0,0q�6�   �� 5b      $��Sopir/Pengawal��6   $��Center=�61   $��Right=�6S   $��Center=�6v   $	��110��6�   $��Arial)�6�   $��25��6�   $��Right=�6�   $��0,5,0,0q�6�   �� 5c      $��Penyusun��6   $��Center=�6+   $��Right=�6M   $��Center=�6p   $	��115��6�   $��Arial)�6�   $��25��6�   $��Right=�6�   $��0,5,0,0q�6�   �� 5d      $��Peneliti��6   $��Center=�6+   $��Right=�6M   $��Center=�6p   $	��110��6�   $��Arial)�6�   $��25��6�   $��Right=�6�   $��0,5,0,0q�6�   5f      �� 5h      -   5        $ StackPanelForthColumn��5h      $��3��68   $ Vertical=�6H   $
��Left=�6_   $	��285��6z   .+�5i        -   5        "  Name$" 	TglPicker��5i      $��5,5,0,0q�60   $	��150��6A   $��Center=�6M   $
��Left=�5j      $��25��6!   #   SelectedDateFormat$# Short=�6-   �� 5k      $ 
Horizontal=�6   $��25��66   $��0,5,0,0q�6B   .+�5l        -   5        $ CboSupir��5l   6   $��23��6*   $	��105��6H   $ SupirId��5m      $��Center=�6#   $ SupirId��5n      $��5,1,0,1q�6"   $
��Left=�63    . 6N   �� 5o      -   5        $ 	TbChecker��5o      $��Center=�61   $��5,0,0,0q�6S   $	��170��6d   $��Arial)�6p   {�.  ��5p      �� *SelectedItem.NamaSupir+$ CboSupir��$ OneWay=�$ PropertyChanged=�6w   5q      �� 5s      $ 
Horizontal=�6   $��25��66   $��0,5,0,0q�6B   .+�5t        -   5        $ CboPenyusun��5t   6   $��23��6*   $	��105��6K   $ 
KaryawanId��5u      $��Center=�6&   $ 
KaryawanId��5v      $��5,1,0,1q�6&    . ��5t   W   �� # [ 5v   7   �� 5w      -   5        $ 
TbPenyusun��5w      $��Center=�62   $��5,0,0,0q�6T   $	��170��6e   $��Arial)�6q   {�.  ��5x      �� *SelectedItem.Nama+$ CboPenyusun��$ OneWay=�$ PropertyChanged=�6u   5y      �� 5{      $ 
Horizontal=�6   $��25��66   $��0,5,0,0q�6B   .+�5|        -   5        $ CboPeneliti��5|   6   $��23��6*   $	��105��6K   $ 
KaryawanId��5}      $��Center=�6&   $ 
KaryawanId��5~      $��5,1,0,1q�6&    . 67   �� 5      -   5        $ 
TbPeneliti��5      $��Center=�62   $��5,0,0,0q�6T   $	��170��6e   $��Arial)�6q   {�.  ��5�      �� *SelectedItem.Nama+$ CboPeneliti��$ OneWay=�$ PropertyChanged=�6u   5�      5�      5�      � 5�   
   -   5        $ Grid2��5�      $��2��6   $��54��6+   $	��Top=�67   $��2��6O   .+�5�      �� $ 
Horizontal=�6   $��	0,0,-1,14q�62   $	��815��6E   $��Center=�6Q   .+�5�         ExtendedButton.ImageButton  -   5        $  Name$$ BtnGenerate��5�   -   %   Image$$% ../Images/SaveIconNoCircle.png��6B   &   
ImageWidth$& 10Q�6i   '   ImageHeight$' 12Q�6y   $	��122��6�   $��30��6�   $��Generate Data��6�   $	��Top=�6�   $��	215,0,0,0q�6�     5�      -   5        $$ BtnCetak��5�   -   $$% ../Images/SaveIconNoCircle.png��6?   $& 10Q�6f   $' 12Q�6v   $	��122��6�   $��30��6�   $��Cetak ke Excel��6�   $	��Top=�6�   $��10,0,0,0q�6�   {�.  6�     5�      -   5        $$ BtnClose��5�   -   $&%  ../Images/DeleteIconNoCircle.png��6?   $& 10Q�6h   $' 12Q�6x   $��122.5��6�   $��30��6�   $��10, 0, 0, 0q�6�   $��Tutup��6�   $	��Top=�6�   6�   5�      5�      &   System.Windows.Controls.DataGrid  5�   
   -   5        $��	MainGrid1��5�   !   $��3��6   (   AutoGenerateColumns( .  )   CanUserAddRows) .  *   RowDetailsVisibilityMode$* VisibleWhenSelected=�5�      +   SelectionMode$+ Single=�6B   $
��Left=�6Y   $	��786��6t   $��	14,0,0,15q�6�   $��3��6�   ��5�   2   �� # [  ,   Columns, 5�      4  .System.Windows.Controls.DataGridTemplateColumn  5�      -   CellTemplate- 5�      �� 5�      .R�5�        -   5        $��+��5�   9   6j   5�      5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn.   Header$. PenerimaanId��6F   /   
Visibility$/ Hidden=�6\   1	  +System.Windows.Controls.DataGridBoundColumn0 	  Binding0 6%   �� *PengirimanId+1   HeaderStyle1 5�      �� 5�      =
  7System.Windows.Controls.Primitives.DataGridColumnHeader" 
 6    .�5�      �� 	 � ��$
 Center=�6K   5�      5�        5�      $. No Seri��6@   2   Width5  /System.Windows.Controls.DataGridLengthConverter$	2 100 6Q   0 6%   �� *NoSeri+1 5�      �� 5�      " 
 6    .�5�      �� 	 � ��$
 Center=�6K   5�      5�        5�      $. 
PengirimId��6D   $/ 	Collapsed=�6X   0 6%   �� *
PengirimId+6X     5�      $. Pengirim��6F   $	2 217 6X   0 6%   �� *NamaPengirim+1 5�      �� 5�      " 
 6    .�5�      �� 	 � ��$
 Center=�6K   5�      5�        5�      $. Penerima��6F   $	2 220 6X   0 6%   �� *NamaPenerima+1 5�      �� 5�      " 
 6    .�5�      �� 	 � ��$
 Center=�6K   5�      5�        5�      $. Tujuan��6D   $	2 220 6T   0 6%   �� *
NamaCabang+1 5�      �� 5�      " 
 6    .�5�      �� 	 � ��$
 Center=�6K   5�      5�      3   RowDetailsTemplate3 5�      �� 5�      .R�5�        4  Name$4 
DetailGrid��5�   ;   ( .  5   
IsReadOnly5 .  ) . ��5�      �� # [ , 5�        5�      $. PengirimanDId��6X   $/ Hidden=�6o   0 61   �� $ PengirimanDId�6o     5�      $. PenerimaanId��6W   $/ Hidden=�6m   0 61   �� $ PengirimanId�6m     5�      $. Barang��6U   $	2 290 6e   0 61   �� $ 
NamaBarang�1 5�   "   �� 5�   &   " 
 6,   .�5�   *   �� 	 � ��$
 Center=�6W   5�   '   5�        5�      $. 	Jml Colie��6S   $	2 100 6f   0 61   �� $ JmlColie�1 5�   "   �� 5�   &   " 
 6,   .�5�   *   �� 	 � ��$
 Center=�6W   5�   '   5�        5�      $. PembungkusId��6W   $/ Hidden=�6m   0 61   �� $ PembungkusId�6m     5�      $. 
Pembungkus��6U   $	2 250 6i   0 61   �� $ 
KetBungkus�1 5�   "   �� 5�   &   " 
 6,   .�5�   *   �� 	 � ��$
 Center=�6W   5�   '   5�        5�      $. Berat��6P   $	2 130 6_   0 61   �� $ Berat�1 5�   "   �� 5�   &   " 
 6,   .�5�   *   �� 	 � ��$
 Center=�6W   5�   '   5�      5�      5�      5�      5�      5�      