   M S B A M L   `   `   `  ����I  EExtendedButton, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG4clr-namespace:ExtendedButton;assembly=ExtendedButtonExtendedButton  5   	   	 BPDMH#   BPDMH.MasterData.MstKendaraan   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   OextendedButton4clr-namespace:ExtendedButton;assembly=ExtendedButton     5   	   -   5          � Title$  MASTER KENDARAAN��5   	   $	��580��6"   $	��650��6/    � WindowStyle$ 
ToolWindow=�6;    � SizeToContent$ Width=�5   	    � WindowStartupLocation$ CenterScreen=�6   $��Arial)�6D   .��5	      � .+�5
   
   �� $��Center=�6   $	��258��62   $��
20,22,27,0q�6?   $	��Top=�6S   $	��595��6k   .+�5      �� $��25��6   $��0,3,0,0q�6%    �� Orientation$ 
Horizontal=�66   $
��Left=�6O   $	��595��6j   .+�5      �� $��Id��6   $��Center=�6%   $��Right=�6G   $��Center=�6j   $	��230��5      $��Arial)�6$   $��24��67   $��0,0,0,1q�6C   �� 5      -   5         ��Name$
 TbId��5      $��Center=�6(   $��5,0,0,0q�6J   $��79��6[   $��Arial)�6f   $��25��6y   $	��Top=�6�   5      �� 5      $ 
Horizontal=�6   $
��Left=�62   $	��595��6M   $��25��6Y   $��0,5,0,0q�6e   .+�5      �� $��	No Polisi��6   $��Center=�6,   $��Right=�6N   $��Center=�6q   $	��230��5      $��Arial)�6$   $��25��67   $��0,0,0,0q�6C   �� 5      -   5        $ 
TbNoPolisi��5      $��Center=�6.   $��5,0,0,0q�6P   $	��184��6a   $��Arial)�6m   5      �� 5      $ 
Horizontal=�6   $
��Left=�62   $	��595��6M   $��25��6Y   $��0,5,0,0q�6e   .+�5      �� $��Jenis��6   $��Center=�6(   $��Right=�6J   $��Center=�6m   $	��230��5      $��Arial)�6$   $��25��67   $��0,-1,0,0q�6C   �� 5      -   5        $ TbJenis��5      $��Center=�6+   $��5,0,0,0q�6M   $	��184��6^   $��Arial)�6j   5      �� 5      $ 
Horizontal=�6   $
��Left=�62   $	��595��6M   $��25��6Y   $��0,5,0,0q�6e   .+�5      �� $��
Keterangan��6   $��Center=�6-   $��Right=�6O   $��Center=�5      $	��230��63   $��Arial)�6?   $��25��6R   $��0,-1,0,0q�6^   �� 5      -   5        $ TbKet��5      $��Center=�6)   $��5,0,0,0q�6K   $	��184��6\   $��Arial)�6h   5      �� 5      $��30��6   $��0,30,0,0q�6%   $ 
Horizontal=�67   $
��Left=�6P   $	��595��6k   .+�5           ExtendedButton.ImageButton  -   5          Name$ BtnBaru��5    -      Image$$ ../Images/SaveIconNoCircle.png��6>      
ImageWidth$ 10Q�6e   	   ImageHeight$	 12Q�6u   $��80��6�   $��	235,0,0,0q�6�   $��30��6�   $
��Baru��6�   6�     5!      -   5        $ 	BtnSimpan��5!   -   $$ ../Images/SaveIconNoCircle.png��6@   $ 10Q�6g   $	 12Q�6w   $��80��6�   $��10,0,0,0q�6�   $��30��6�   $��Simpan��6�   6�     5"      -   5        $ BtnHapus��5"   -   $&  ../Images/DeleteIconNoCircle.png��6?   $��10,0,0,0q�6h   $ 10Q�6z   $	 12Q�6�   $��Hapus��6�   $��80��6�   $��30��6�   6�     5#      -	   5        $ BtnClose��5#   -   $&  ../Images/DeleteIconNoCircle.png��6?   $��10,0,0,0q�6h   $ 10Q�6z   $	 12Q�6�   $��Tutup��6�   $��80��6�   $��30��6�   6�   5$      5%      �� 5'   
   -
   5        
 ��Name$
 ListViewKendaraan��5'   '   $��
10,215,7,5q�6    �� SelectionMode$ Single=�6B    J� RenderTransformOrigin$ 1,0.5&�5(      $��13.333(�6"   z�5)      V� 5*      .~�5+      �� �� 5,      �� 5-      T� 5.      5/       �� View 51      �� 52       �� AllowsColumnReorder .  �� ColumnHeaderToolTip$ MstPelanggan��66    �� ColumnHeaderContextMenu 53      �� 54      $��Arial)�6&   .@�55      m� $��	Ascending��6'   m� 56      $��
Descending��6'   57      .P�59      �� $��Id��6%    �� Width$ 80��61    �� DisplayMemberBinding 6<   �� *KendaraanId+�� 5:      $��	No Polisi��6%   $ 85��68    6C   �� *
NoPolisi+�� 5;      $��Jenis��6%   $	 200��64    6@   �� *Jenis+�� 5<      $��
Keterangan��6%   $	 250��69    6E   �� *
Keterangan+5=      5?      5A      5B      