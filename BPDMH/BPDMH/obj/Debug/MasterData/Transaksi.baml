   M S B A M L   `   `   `  ����I  EExtendedButton, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG4clr-namespace:ExtendedButton;assembly=ExtendedButtonExtendedButton  5   	   @ <BPDMH, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:BPDMH.MasterDataBPDMH.MasterData 5   	   clr-namespace:BPDMHBPDMH 64   -clr-namespace:BPDMH.DataSetBPDMH.DataSet 5   	   )clr-namespace:BPDMH.ToolsBPDMH.Tools 5   	       BPDMH.MasterData.Transaksi   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   OextendedButton4clr-namespace:ExtendedButton;assembly=ExtendedButton     5   	   .srcclr-namespace:BPDMH.MasterData     5   	   %bpdmhclr-namespace:BPDMH     64   -localclr-namespace:BPDMH.DataSet     5   	   +toolsclr-namespace:BPDMH.Tools     5   	   -   5          � Title$  TRANSAKSI PENGIRIMAN��5   	   $	��577��6&   $	��769��63    � WindowStyle$ 
ToolWindow=�6?    � SizeToContent$ WidthAndHeight=�6X    � WindowStartupLocation$ CenterScreen=�6w   $��Arial)�6�    � 	Resources 5
         FindCriteria S� 
ObjectType  BPDMH.Tools.CategoryEnum-  'BPDMH.Tools.RadioButtonCheckedConverter  RadioButtonCheckedConverter  myHeaderStyle �� 
TargetType �� Property �� Value# BPDMH.DataSet.TransactionList  TransactionList��%�   &	        &	      &	 +     &	 z     S� 5   
   "  62     5   
   6,   �� 5   
   " ��6&   .�5      ��  � y�$ 	Collapsed=�6+   5        5   
   6    5      .��5      � .+�5   
   �� $��Center=�6   $	��290��62   $��
20,10,26,0q�6?   $	��Top=�6S   $	��715��6k   .+�5      �� $��25��6   $��0,3,0,0q�6%   	 �� Orientation$	 
Horizontal=�66   $
��Left=�6O   $	��715��6j   .+�5      �� $��No. SP��6   $��Center=�6)   $��Right=�6K   $��Center=�6n   $	��115��6�   $��Arial)�6�   $��25��6�   $��0,-1,0,0q�6�   �� 5      -   5        
 ��Name$
 TbNoSp��5      $��Center=�6*   $��5,0,0,0q�6L   $��80��6]   $��Arial)�6h   $��25��6{   $	��Top=�6�   ��5      �� *SelectedItem.NoSeri+ �� ElementName$ ListBoxNoSp�� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� 5      -   5        $	��...��5      $��40��6'   ���	#FFD61D1D6I   �� 5      $��
Tgl. Kirim��6   $��Center=�6-   $��Right=�6O   $��Center=�6r   $	��110��6�   $��Arial)�6�   $��25��6�   $��	170,0,0,0q�6�   (  "System.Windows.Controls.DatePicker  5      $��5,0,0,0q�6   $	��150��6.      Text 5      �� *SelectedItem.TglInput+$ ListBoxNoSp��$ TwoWay=�$ PropertyChanged=�5      �� 5      $	 
Horizontal=�6   $
��Left=�62   $	��715��6M   $��25��6Y   $��0,5,0,0q�6e   .+�5       �� $��Nama Pengirim��6   $��Center=�60   $��Right=�6R   $��Center=�6u   $	��115��6�   $��Arial)�6�   $��25��6�   $��0,0,0,0q�6�   �� 5!      -   5        $
 TbPengirimId��5!      $��Center=�60   $��5,0,0,0q�6R   $��50��6c   $��Arial)�6n   ��5"      �� *SelectedItem.PengirimId+$ ListBoxNoSp��$ TwoWay=�$ PropertyChanged=��� 5#      -   5        $
 TbNamaPengirim��5#      $��Center=�62   $��5,0,0,0q�6T   $	��140��6e   $��Arial)�6q   ��5$      �� $ TbNoSp�� �� Path$ NamaPengirim�$ TwoWay=�$ PropertyChanged=��� 5%      $��Nama Penerima��6   $��Center=�60   $��Right=�6R   $��Center=�6u   $	��110��6�   $��Arial)�6�   $��25��6�   $��50,0,0,0q�6�   �� 5&      -   5        $
 TbPenerimaId��5&      $��Center=�60   $��5,0,0,0q�6R   $��50��6c   $��Arial)�6n   ��5'      �� *SelectedItem.PenerimaId+$ ListBoxNoSp��$ TwoWay=�$ PropertyChanged=��� 5(      -   5        $
 TbNamaPenerima��5(      $��Center=�62   $��5,0,0,0q�6T   $	��150��6e   $��Arial)�6q   ��5)      �� *SelectedItem.NamaPenerima+$ ListBoxNoSp��$ TwoWay=�$ PropertyChanged=�5*      �� 5+      $	 
Horizontal=�6   $
��Left=�62   $	��715��6M   $��25��6Y   $��0,5,0,0q�6e   .+�5,      �� $��Alamat Pengirim��6   $��Center=�62   $��Right=�6T   $��Center=�6w   $	��115��6�   $��Arial)�6�   $��25��6�   $��0,-1,0,0q�6�   �� 5-      -   5        $
 TbAlamatPengirim��5-      $��Center=�64   $��5,0,0,0q�6V   $	��185��6g   $��Arial)�6s   ��5.      �� *SelectedItem.AlamatPengirim+$ ListBoxNoSp��$ TwoWay=�$ PropertyChanged=��� 5/      $��Alamat Penerima��6   $��Center=�62   $��Right=�6T   $��Center=�6w   $	��110��6�   $��Arial)�6�   $��25��6�   $��65,0,0,0q�6�   �� 50      -	   5        $
 TbAlamatPenerima��50      $��Center=�64   $��5,0,0,0q�6V   $	��184��6g   $��Arial)�6s   ��51      �� *SelectedItem.AlamatPenerima+$ ListBoxNoSp��$ TwoWay=�$ PropertyChanged=�52      �� 53      $��25��6   $��0,5,0,0q�6%   $	 
Horizontal=�66   $
��Left=�6O   $	��715��6j   .+�54      �� $��Tujuan��6   $��Center=�6)   $��Right=�6K   $��Center=�6n   $	��115��6�   $��Arial)�6�   $��25��6�   $��0,0,0,0q�6�   �� 55      -
   5        $
 
TbTujuanId��55      $��Center=�6.   $��5,0,0,0q�6P   $��50��6a   $��Arial)�6l   $��25��6   $	��Top=�6�   ��56      �� *SelectedItem.CabangId+$ ListBoxNoSp��$ TwoWay=�$ PropertyChanged=��� 57      -   5        $
 TbTujuan��57      $��Center=�6,   $��5,0,0,0q�6N   $	��185��6_   $��Arial)�6k   $��25��6~   $	��Top=�6�   ��58      �� *SelectedItem.NmCabang+$ ListBoxNoSp��$ TwoWay=�$ PropertyChanged=��� 59      $��Supir��6   $��Center=�6(   $��Right=�6J   $��Bottom=�6m   $	��110��6�   $��Arial)�6�   $��25��6�   $��65,0,0,0q�6�   �� 5:      -   5        $
 TbSupir��5:      $��Center=�6+   $��5,0,0,0q�6M   $	��175��6^   $��Arial)�6j   ��5;      �� *SelectedItem.NamaPelanggan+$ ListBoxNoSp��$ TwoWay=�$ PropertyChanged=�5<      �� 5=      $	 
Horizontal=�6   $
��Left=�62   $	��715��6M   $��25��6Y   $��0,5,0,0q�6e   .+�5>      �� $��	Kendaraan��6   $��Center=�6,   $��Right=�6N   $��Center=�6q   $	��115��6�   $��Arial)�6�   $��25��6�   $��0,-0.5,0,0.5q�6�   �� 5?      -   5        $
 TbKendaraanId��5?      $��Center=�61   $��5,0,0,0q�6S   $��50��6d   $��Arial)�6o   $��25��6�   $	��Top=�6�   ��5@      �� *SelectedItem.KendaraanId+$ ListBoxNoSp��$ TwoWay=�$ PropertyChanged=��� 5A      -   5        $
 TbKendaraan��5A      $��Center=�6/   $��5,0,0,0q�6Q   $	��185��6b   $��Arial)�6n   $��25��6�   $	��Top=�6�   ��5B      �� *SelectedItem.Jenis+$ ListBoxNoSp��$ TwoWay=�$ PropertyChanged=��� 5C      $��Checker��6   $��Center=�6*   $��Right=�6L   $��Center=�6o   $	��110��6�   $��Arial)�6�   $��25��6�   $��65,0,0,0q�6�   �� 5D      -   5        $
 	TbChecker��5D      $��Center=�6-   $��5,0,0,0q�6O   $	��175��6`   $��Arial)�6l   ��5E      �� *SelectedItem.Checker+$ ListBoxNoSp��$ TwoWay=�$ PropertyChanged=�5F      �� 5G      $��30��6   $��0,30,0,0q�6%   $	 
Horizontal=�67   $��Right=�6P   $	��715��6l   .+�5H          ExtendedButton.ImageButton  -   5          Name$ 	BtnSimpan��5H   -      Image$$ ../Images/SaveIconNoCircle.png��6@      
ImageWidth$ 10Q�6g      ImageHeight$ 12Q�6w   $��80��6�   $��	250,0,5,0q�6�   $��30��6�   $��Simpan��6�   $	��Top=�6�     5I      -   5        $ BtnHapus��5I   -   $&  ../Images/DeleteIconNoCircle.png��6?   $��5,0,40,0q�6h   $ 10Q�6z   $ 12Q�6�   $��Hapus��6�   $��80��6�   $��30��6�   $��Center=�6�   5L      �� 5M      $��42��6   $��0,15,0,0q�6%   $	 
Horizontal=�67   $��Right=�6P   $	��715��6l   .+�5N      �� $��Jumlah Colie��6   $��Center=�6/   $��Center=�6Q   $	��105��6u   $��8,0,0,0q�6�   �� 5O      $��
Pembungkus��6   $��Center=�6-   $��0,0,0,0q�6O   $��Center=�6`   $	��190��6�   �� 5P      $��Nama Barang��6   $��Center=�6.   $��Center=�6P   $��0,0,0,0q�6t   $	��305��6�   $��Center=�6�   �� 5Q      $��Berat Kg. Bruto��6   $��Center=�62   $��Center=�6T   $��0,0,0,0q�6x   $	��110��6�   $��42��6�   $��Bottom=�6�   5R      �� 5S      $��25��6   $��0q�6%   $	 
Horizontal=�60   $��Right=�6I   $	��715��6e   .+�5T      �� -   5        $
 
TbJmlColie��5T      $��Center=�6.   $��3,0,0,0q�6P   $	��105��6a   $��Arial)�6m   $��25��6�   $	��Top=�6�   �� 5U      -   5        $
 TbPembungkusId��5U      $��Center=�62   $��1,0,0,0q�6T   $��50��6e   $��Arial)�6p   $��25��6�   $	��Top=�6�   �� 5V      -   5        $
 TbPembungkus��5V      $��Center=�60   $��1,0,0,0q�6R   $	��140��6c   $��Arial)�6o   $��25��6�   $	��Top=�6�   �� 5W      -   5        $
 TbNamaBarang��5W      $��Center=�60   $��1,0,0,0q�6R   $	��303��6c   $��Arial)�6o   $��25��6�   $	��Top=�6�   �� 5X      -   5        $
 TbBerat��5X      $��Center=�6+   $��1,0,0,0q�6M   $	��110��6^   $��Arial)�6j   $��25��6}   $	��Top=�6�   5Y      5Z      �� 5^   
   -   5         ��Name$ ListViewTransaksi��5^   *   $��30,302,30,102q�6    �� SelectionMode$ Single=�6E    J� RenderTransformOrigin$ 1,0.5&�5_      $��13.333(�6"    �� SelectedIndex$ 0��5a      ��5`      �� *SelectedItem.TrnDetails+$ ListBoxNoSp��z�5b      V� 5c      .~�5d      �� �� 5e      �� 5f      T� 5g      5h       �� View 5j      �� 5k       �� AllowsColumnReorder .  �� ColumnHeaderToolTip$ MstPelanggan��66   ! �� ColumnHeaderContainerStyle# [ 6Y    �� ColumnHeaderContextMenu 5l      �� 5m      $��Arial)�6&   .@�5n      m� $��	Ascending��6'   m� 5o      $��
Descending��6'   5p      .P�5r      ��  �� Width$	 102��6%    �� DisplayMemberBinding 61   �� *
JmlColie+�� 5s      $ 50��6%    60   �� *PembungkusId+�� 5t      $	 140��6%    61   �� *KetPembungkus+�� 5u      $	 304��6%    61   �� *
NamaBarang+�� 5v      $	 108��6%    61   �� *Berat+5x      5z      �� 5{   
   $��0,465,26,57q�6   $	 
Horizontal=�6*   $��Right=�6C   $	��715��6_   .+�5|      �� $��Biaya��6   $��Center=�6$   $��Right=�6F   $��80��6i   $��25��6t   $	��Top=�6�   $��Right=�6�   �� 5}      -   5        $
 TbBiaya��5}      $��Center=�6'   $��0q�6I   $	��300��6T   $��Arial)�6`   $��25��6s   $	��Top=�6   5~      �� 5   
   $��0,495,26,27q�6   $	 
Horizontal=�6*   $��Right=�6C   $	��715��6_   .+�5�      �� $��	Terbilang��6   $��Center=�6(   $��Right=�6J   $��80��6m   $��25��6x   $	��Top=�6�   $��Right=�6�   �� 5�      -   5        $
 TbTerbilang��5�      $��Center=�6+   $��0q�6M   $	��635��6X   $��Arial)�6d   $��25��6w   $	��Top=�6�   5�      �� 5�   
   -   5        $��CmbNoSp��5�      $��290,15,371,507q�6"   ��6:   �� *TransactionHs+ �� Source# [ ��5�      �� 5�      .R�5�      �� $	 
Horizontal=�6!   .+�5�      �� $��16��6=   $��16��6H   $��0,2,5,2q�6T   ��6$   �� *NoSeri+6T   �� 5�      ��6$   �� *NoSeri+5�      5�      5�      �� 5�   
   -   5          ��Name$  ListBoxNoSp��5�      $��1q�6'   $��402,15,286,477q�5�   V   
������ $y�Hidden=�6�   ��6   �� *TransactionHs+# [ ��5�      �� 5�      .R�5�      �� ��6    �� *
PengirimId+5�      5�      5�      5�      