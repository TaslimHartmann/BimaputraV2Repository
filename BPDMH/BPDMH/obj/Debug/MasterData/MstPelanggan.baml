   M S B A M L   `   `   `  ����I  EExtendedButton, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG4clr-namespace:ExtendedButton;assembly=ExtendedButtonExtendedButton  5   	   @ <BPDMH, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:BPDMH.MasterDataBPDMH.MasterData 5   	   )clr-namespace:BPDMH.ToolsBPDMH.Tools 5   	   #   BPDMH.MasterData.MstPelanggan   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   OextendedButton4clr-namespace:ExtendedButton;assembly=ExtendedButton     5   	   .srcclr-namespace:BPDMH.MasterData     5   	   +toolsclr-namespace:BPDMH.Tools     5   	   -   5          � Title$  MASTER PELANGGAN��5   	   $	��580��6"   $	��860��6/    � WindowStyle$ 
ToolWindow=�6;    � WindowStartupLocation$ CenterScreen=�6T   $��Arial)�6y    � 	Resources 5	      -  'BPDMH.Tools.RadioButtonCheckedConverter   RadioButtonCheckedConverter. (BPDMH.Tools.FilesystemSuggestionProvider  fsp  
fsTemplate �� Padding �� Orientation �� Path �� StringFormat " ListBoxSubCategoryListTemplate �� DataType	 ��Name
 �� Triggers P� Property P� Value �� 
TargetName �� Property �� Value��%Q  &	        &	      &	 &     &	 &      5
   
   6,     5   
   6-   �� 5   
   .R�5      �� $ 5q�6   ._�5      �� $ Vertical=�6   .+�5      �� $
��Bold"�5      ��5       �� $
 Name�5      �� 5      ��6    �� $ LastWriteTime�$ Last Modified: {0}��5      5      5      �� 5   
   " ��6>   .R�5      �� $	 btnSubCategoryList��6   
������ $	��250��6^   ��6M   �� 6^   
 5      P� 5       �@J�IsMouseOver . . �5      �� $ btnSubCategoryList��6    � ��
 ��iA�6S   5      5      5      .��5       �  � RowDefinitions 5!   
   �� 5"      $	��11* �6   �� 5#      $	��46* �6   �� 5$      $	��53* �6    � ColumnDefinitions 5&   
   �� 5'      $
��320* �6   �� 5(      $
��185* �6   �� 5)      $
��347* �6   .+�5+   
   �� $��Center=�6   $	��258��62   $��
15,22,22,0q�6?   $	��Top=�6S   $	��815��6k   $��3��6w   $��2��6�   .+�5,      �� $��1��6   $��25��6&   $��0,3,0,0q�62   $ 
Horizontal=�6C   $
��Left=�6\   $	��815��6w   .+�5-      �� $��Id��6   $��Center=�6%   $��Right=�6G   $��Center=�6j   $	��300��5.      $��Arial)�6$   $��24��67   $��0,0,0,1q�6C   �� 5/      -   5         ��Name$
 TbId��5/      $��Center=�6(   $��5,0,0,0q�6J   $��79��6[   $��Arial)�6f   $��25��6y   $	��Top=�50      $��0��62   51      �� 52      $��2��6   $ 
Horizontal=�6&   $
��Left=�6?   $	��815��6Z   $
��Auto��6f   $��0,5,0,0q�6u   .+�53      �� $
��Nama��6   $��Center=�6'   $��Right=�6I   $	��Top=�6l   $	��300��54      $��Arial)�6$   $��25��67   $��0,0,0,0q�6C   �� 5<      $ 
Horizontal=�6   .+�5=      �� -   5        $ TbNama��5=      $��Center=�6.   $��5,0,0,0q�6P   $	��184��6a   $��25��6m   $��Arial)�6y   $	��Top=�5>      $��1��66   5A      5B      �� 5C      $ 
Horizontal=�6   $
��Left=�62   $	��815��6M   $��25��6Y   $��0,5,0,0q�6e   .+�5D      �� $��Telpon��6   $��Center=�6)   $��Right=�6K   $��Center=�6n   $	��300��5E      $��Arial)�6$   $��25��67   $��0,-1,0,0q�6C   �� 5F      -   5        $ TbTelpon��5F      $��Center=�6,   $��5,0,0,0q�6N   $	��184��6_   $��Arial)�6k   5G      �� 5H      $��25��6   $��0,5,0,0q�6%   $ 
Horizontal=�66   $
��Left=�6O   $	��815��6j   .+�5I      �� $��Kontak Person��6   $��Center=�60   $��Right=�6R   $��Center=�6u   $	��300��5J      $��Arial)�6$   $��25��67   $��0,0,0,0q�6C   �� 5K      -   5        $ 
TbKtPerson��5K      $��Center=�6.   $��5,0,0,0q�6P   $	��184��6a   $��Arial)�6m   $��25��6�   $	��Top=�6�   5L      �� 5M      $ 
Horizontal=�6   $
��Left=�62   $	��815��6M   $��25��6Y   $��0,5,0,0q�6e   .+�5N      �� $��CategoryEnum��6   $��Center=�6/   $��Right=�6Q   $��Center=�6t   $	��300��5O      $��Arial)�6$   $��25��67   $��0,-0.5,0,0.5q�6C   	� 5P      -   5         	�Name$ 
RbPengirim��5P       	� 	GroupName$ 
RbPengirim��62   $	��0,5q�6I    d� 	IsChecked 5Q      �� *BooleanProperty+ �� 	Converter# [  #  BPDMH.MasterData.MstPelanggan   BooleanTrue �� ConverterParameter# Z .��5S      �� $
��Wrap=�6    $��Pengirim��64   $��Arial)�6D   5T      	� 5U      -   5        $ 
RbPenerima��5U      $ 
RbPenerima��62   $��10,5,0,5q�6I    5V      �� *BooleanProperty+# [     BooleanFalse# Z .��5X      �� $
��Wrap=�6    $��Penerima��64   $��Arial)�6D   5Y      5Z      �� 5[      $ 
Horizontal=�6   $
��Left=�62   $	��815��6M   $��25��6Y   $��0,5,0,0q�6e   .+�5\      �� $��Alamat��6   $��Center=�6)   $��Right=�6K   $��Center=�6n   $	��300��5]      $��Arial)�6$   $��25��67   $��0,-0.5,0,0.5q�6C   �� 5^      -   5        $ TbAlamat��5^      $��Center=�6,   $��5,0,0,0q�6N   $	��265��6_   $��Arial)�6k   $��25��6~   $	��Top=�6�   5_      �� 5`      $��30��6   $��0,30,0,0q�6%   $ 
Horizontal=�67   $��Center=�6P   $	��816��6m   .+�5b          ExtendedButton.ImageButton  -	   5          Name$ BtnBaru��5b   -      Image$$ ../Images/SaveIconNoCircle.png��6>      
ImageWidth$ 10Q�6e      ImageHeight$ 12Q�6u   $��80��6�   $��	305,0,0,0q�6�   $��30��6�   $
��Baru��6�   6�     5c      -
   5        $ 	BtnSimpan��5c   -   $$ ../Images/SaveIconNoCircle.png��6@   $ 10Q�6g   $ 12Q�6w   $��80��6�   $��10,0,0,0q�6�   $��30��6�   $��Simpan��6�   6�     5d      -   5        $ BtnHapus��5d   -   $&  ../Images/DeleteIconNoCircle.png��6?   $��10,0,0,0q�6h   $ 10Q�6z   $ 12Q�6�   $��Hapus��6�   $��80��6�   $��30��6�   6�     5e      -   5        $ BtnClose��5e   -   $&  ../Images/DeleteIconNoCircle.png��6?   $��10,0,0,0q�6h   $ 10Q�6z   $ 12Q�6�   $��Tutup��6�   $��80��6�   $��30��6�   6�   5g      5h      �� 5j   
   -   5         ��Name$ ListViewPelanggan��5j   '   $��
15,0,17,15q�6    �� SelectionMode$ Single=�6B     J� RenderTransformOrigin$  1,0.5&�5k      $��13.333(�6"   $��3��6�   $��0��5l      $��2��6#   z�5m      V� 5n      .~�5o      �� �� 5p      �� 5q      T� 5r      5s      ! �� View! 5u      �� 5v      " �� AllowsColumnReorder" . # �� ColumnHeaderToolTip$# MstPelanggan��66   $ �� ColumnHeaderContextMenu$ 5w      �� 5x      $��Arial)�6&   .@�5y      m� $��	Ascending��6'   m� 5z      $��
Descending��6'   5{      .P�5}      �� $��Id��6%   % �� Width$% 80��61   & �� DisplayMemberBinding& 6<   �� *PelangganId+�� 5~      $
��Nama��6%   $	% 155��63   & 6?   �� *	NamaPlg+�� 5      $��Telpon��6%   $	% 127��65   & 6A   �� *Telp+�� 5�      $��Kontak Person��6%   $	% 150��6<   & 6H   �� *
KtPerson+�� 5�      $��Kategori��6%   $% 90��67   & 6B   �� *
Kategori+�� 5�      $��Alamat��6%   $	% 200��65   & 6A   �� *Alamat+5�      5�      �� 5�   
   -   5        ' ��Name$' 	PlgLookup��5�   /   $��1��6   $��2��6   $
��Auto=�6`   #��[ 5�      $y�	Collapsed=�6T   $��0,0,87,0q�6k   $	��145��6}   $	��Top=�5�      $��2��6*   5�      5�      