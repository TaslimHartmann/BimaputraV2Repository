   M S B A M L   `   `   `  ����I  EExtendedButton, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG4clr-namespace:ExtendedButton;assembly=ExtendedButtonExtendedButton  5   	   @ <BPDMH, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:BPDMH.ToolsBPDMH.Tools 5   	       BPDMH.Report.LapPenerimaan   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   OextendedButton4clr-namespace:ExtendedButton;assembly=ExtendedButton     5   	   +toolsclr-namespace:BPDMH.Tools     5   	   -   5        $	��580��5   	   $	��775��6     � WindowStyle$  
ToolWindow=�6"    � SizeToContent$ WidthAndHeight=�6;    � Title$ LAPORAN PENERIMAAN��5   	    � WindowStartupLocation$ CenterScreen=�6$   $��Arial)�5   	    � 	Resources 5
         
MasterView  
DetailView �� Source �� Source �� Path  CabangLookup:  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType	 �� Property
 �� Value ��Name �� StringFormat �� RelativeSource �� AncestorType(  "System.Windows.Controls.DatePicker��%d  &	        &	      7   &	 Q     '  d     �� 5   
   6   �� 5   
    6   �� 8   $ TdByPengIdResults�6o   �� 5   
   6   �� 5   
   "  6   .�5      �� 	 � ��
 5      �� 5      .R�5      �� $ PART_TextBox��6"   ��5      �� $ SelectedDate�$ 
dd/MM/yyyy�� �� "  5      5      5      5      .��5      � -   5        $��MainGrid��5      $
��Left=�6   $	��774��66   $��0,0,0,0q�5   F    +� FocusedElement 5   B   ��  �� ElementName$ TbNoSp�� � DataContext 5      �� # [   � RowDefinitions 5   
   �� 5      $��20 �6   �� 5       $��75 �6   �� 5!      $��45 �6   �� 5"      $	��262 �6   �� 5#      $��98 �6   �� 5$      $��50 �6   .+�5'   
   � -   5         �Name$ Grid1��5'      $��1��6   $��0,0,4,10q�6+    � ColumnDefinitions 5(      �� 5)      $��95 �6#   �� 5*      $	��225 �6#   �� 5+      $	��120 �6#   �� 5,      $	��370 �6#   .+�5.      �� -   5         ��Name$ StackPanelFirstColumn��5.      $��0��68    �� Orientation$ Vertical=�6H   .+�5/      �� $
��Dari��6   $��Center=�6'   $��Right=�6I   $��Center=�6l   $	��115��6�   $��Arial)�6�   $��25��6�   $��Right=�6�   $��0,5,0,0q�6�   50      �� 51      -   5        $ StackPanelSecondColumn��51      $��1��69   $ Vertical=�6I   .+�53      �� $ 
Horizontal=�6   $��0,5,0,0q�66   .+�54        BPDMH.Tools.LivePreviewCb  -   5          Name$ 	CboCabang��54   6   $��23��6*   $��70��6I    �� SelectedValuePath$ CabangId��55      $��Center=�6$    �� DisplayMemberPath$ CabangId��56      $��5,0,0,0q�6$    �� 
IsEditable . ��54   T   �� # [ 56   5   �� 57      -   5        $ TbTujuan��57      $��Center=�60   $��5,0,0,0q�6R   $	��145��6c   $��Arial)�6o   $��25��6�   $	��Top=�6�   {�.  ��58      �� *SelectedItem.NmCabang+$ 	CboCabang�� �� Mode$ OneWay=� �� UpdateSourceTrigger$ PropertyChanged=�6w   59      5:      �� 5<      -   5        $ StackPanelThirdColumn��5<      $��2��68   $ Vertical=�6H   .+�5=      �� $��Tgl. Terima��6   $��Center=�6.   $��Right=�6P   $��Center=�6s   $	��110��6�   $��Arial)�6�   $��25��6�   $��Right=�6�   $��0,5,0,0q�6�   5>      �� 5@      -	   5        $ StackPanelForthColumn��5@      $��3��68   $ Vertical=�6H   $
��Left=�6_   $	��325��6z   .+�5A      �� $ 
Horizontal=�6   .+�5B        -
   5          Name$ 	TglPicker��5B   !   $��5,5,0,0q�64   $	��150��6E   $��Center=�6Q   $
��Left=�5C      $��25��6!      SelectedDateFormat$ Short=�6-   �� 5D      $	��s/d��6   $��Center=�6*   $��
5, 0, 0, 0q�6E   5E        5G      -   5        $ 
TglPicker2��5G      $��5,5,0,0q�61   $	��150��6B   $��Center=�6N   $
��Left=�5H      $��25��6!   $ Short=�6-   5I      5J      � 5L   
   -   5        $ Grid2��5L      $��2��6   $��54��6+   $	��Top=�67   $��2��6O   .+�5M      �� $ 
Horizontal=�6   $��0,0,4,0q�62   $	��770��6C   $��Center=�6O   .+�5N          ExtendedButton.ImageButton  -   5          Name$ BtnGenerate��5N   -       Image$$  ../Images/SaveIconNoCircle.png��6B   !   
ImageWidth$! 10Q�6i   "   ImageHeight$" 12Q�6y   $��122.5��6�   $��30��6�   $��Generate Data��6�   $	��Top=�6�   $��	195,0,0,0q�6�     5O      -   5        $ BtnCetak��5O   -   $$  ../Images/SaveIconNoCircle.png��6?   $! 10Q�6f   $" 12Q�6v   $��122.5��6�   $��30��6�   $��Cetak ke Excel��6�   $	��Top=�6�   $��10,0,0,0q�6�   {�.  6�     5P      -   5        $ BtnClose��5P   -   $&   ../Images/DeleteIconNoCircle.png��6?   $! 10Q�6h   $" 12Q�6x   $��122.5��6�   $��30��6�   $��10, 0, 0, 0q�6�   $��Tutup��6�   $	��Top=�6�   6�   5Q      5R      &   System.Windows.Controls.DataGrid  5U   
   -   5        $��	MainGrid1��5U   !   $��3��6   #   AutoGenerateColumns# .  $   CanUserAddRows$ .  %   RowDetailsVisibilityMode$% VisibleWhenSelected=�5W      &   SelectionMode$& Single=�6B   $
��Left=�6Y   $	��745��6t   $��	10,0,0,15q�6�   $��3��6�   ��5U   2   �� # [  '   Columns' 5X      4  .System.Windows.Controls.DataGridTemplateColumn  5Y      (   CellTemplate( 5Z      �� 5[      .R�5\        -   5        $��+��5\   9   6j   5]      5_      0  *System.Windows.Controls.DataGridTextColumn  5a      ,  &System.Windows.Controls.DataGridColumn)   Header$) PenerimaanId��6F   *   
Visibility$* Hidden=�6\   1	  +System.Windows.Controls.DataGridBoundColumn+ 	  Binding+ 6%   �� *PengirimanId+,   HeaderStyle, 5b      �� 5c      =
  7System.Windows.Controls.Primitives.DataGridColumnHeader" 
 6    .�5d      �� 	 � ��$
 Center=�6K   5e      5g        5i      $) 
Tgl. Input��6c   -   Width5  /System.Windows.Controls.DataGridLengthConverter$	- 100 6w   + 6%   �� *
TglInput+$ {0:dd/MM/yyyy}��, 5j      �� 5k      " 
 6    .�5l      �� 	 � ��$
 Center=�6K   5m      5o        5q      $) No Seri��6@   $	- 100 6Q   + 6%   �� *NoSeri+, 5r      �� 5s      " 
 6    .�5t      �� 	 � ��$
 Center=�6K   5u      5w        5y      $
) Dari��6D   $	- 100 6R   + 6%   �� *
NamaCabang+, 5z      �� 5{      " 
 6    .�5|      �� 	 � ��$
 Center=�6K   5}      5        5�      $	) Bea��6B   $	- 100 6O   + 6%   �� *
KetBayar+, 5�      �� 5�      " 
 6    .�5�      �� 	 � ��$
 Center=�6K   5�      5�        5�      $) 
PengirimId��6D   $* Hidden=�6X   + 6%   �� *
PengirimId+6X     5�      $) Pengirim��6F   $	- 200 6X   + 6%   �� *NamaPengirim+, 5�      �� 5�      " 
 6    .�5�      �� 	 � ��$
 Center=�6K   5�      5�        5�      $) Penerima��6F   $	- 200 6X   + 6%   �� *NamaPenerima+, 5�      �� 5�      " 
 6    .�5�      �� 	 � ��$
 Center=�6K   5�      5�        5�      $) Penerus��6F   $	- 100 6W   + 6%   �� *BiayaPenerus+, 5�      �� 5�      " 
 6    .�5�      �� 	 � ��$
 Center=�6K   5�      5�        5�      $) Biaya��6?   $	- 100 6N   + 6%   �� *Biaya+, 5�      �� 5�      " 
 6    .�5�      �� 	 � ��$
 Center=�6K   5�      5�      .   RowDetailsTemplate. 5�      �� 5�      .R�5�        /  Name$/ 
DetailGrid��5�   ;   # .  0   
IsReadOnly0 .  $ . ��5�      �� # [ ' 5�        5�      $) PengirimanDId��6[   $* 	Collapsed=�6r   + 61   �� $ TrnPengirimanDId�6r     5�      $) PenerimaanId��6[   $* 	Collapsed=�6q   + 61   �� $ TrnPengirimanId1�6q     5�      $) 	Jml Colie��6S   $	- 100 6f   + 61   �� $ JmlColie�, 5�   "   �� 5�   &   " 
 6,   .�5�   *   �� 	 � ��$
 Center=�6W   5�   '   5�        5�      $) PembungkusId��6W   $* Hidden=�6m   + 61   �� $ PembungkusId�6m     5�      $) 
Pembungkus��6U   $	- 250 6i   + 61   �� $ 
KetBungkus�, 5�   "   �� 5�   &   " 
 6,   .�5�   *   �� 	 � ��$
 Center=�6W   5�   '   5�        5�      $) Barang��6U   $	- 250 6e   + 61   �� $ 
NamaBarang�, 5�   "   �� 5�   &   " 
 6,   .�5�   *   �� 	 � ��$
 Center=�6W   5�   '   5�        5�      $) Berat��6P   $	- 130 6_   + 61   �� $ Berat�, 5�   "   �� 5�   &   " 
 6,   .�5�   *   �� 	 � ��$
 Center=�6W   5�   '   5�      5�      5�      5�      5�      5�      