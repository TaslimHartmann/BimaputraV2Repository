   M S B A M L   `   `   `  ����I  EExtendedButton, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG4clr-namespace:ExtendedButton;assembly=ExtendedButtonExtendedButton  5   	   @ <BPDMH, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:BPDMH.ToolsBPDMH.Tools 5   	      BPDMH.MasterData.MstSupir   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   OextendedButton4clr-namespace:ExtendedButton;assembly=ExtendedButton     5   	   +toolsclr-namespace:BPDMH.Tools     5   	   -   5          � Title$  MASTER SUPIR��5   	   $	��580��6   $	��860��6+    � WindowStyle$ 
ToolWindow=�67    � WindowStartupLocation$ CenterScreen=�5   	   $��Arial)�6.    � 	Resources 5	       "  ListBoxSubCategoryListTemplate �� DataType ��Name �� Triggers P� Property P� Value	 �� 
TargetName
 �� Property �� Value��%  &	        �� 5   
   " ��6>   .R�5      �� $ btnSubCategoryList��6   
������ $	��250��6^   ��6M   �� 6^    5      P� 5       �@J�IsMouseOver . . �5      �� $	 btnSubCategoryList��6   
 � ��
 ��iA�6S   5      5      5      .��5      �  � RowDefinitions 5   
   �� 5      $	��11* �6   �� 5      $	��37* �6   �� 5      $	��62* �6    � ColumnDefinitions 5   
   �� 5      $
��320* �6   �� 5      $
��185* �6   �� 5      $
��347* �6   .+�5    
   �� $��Center=�6   $	��258��62   $��
15,22,22,0q�6?   $	��Top=�6S   $	��815��6k   $��3��6w   $��3��6�   .+�5!      �� $��25��6   $��0,3,0,0q�6%    �� Orientation$ 
Horizontal=�66   $
��Left=�6O   $	��815��6j   .+�5"      �� $��Id��6   $��Center=�6%   $��Right=�6G   $��Center=�6j   $	��300��5#      $��Arial)�6$   $��24��67   $��0,0,0,1q�6C   �� 5$      -   5         ��Name$
 TbId��5$      $��Center=�6(   $��5,0,0,0q�6J   $��79��6[   $��Arial)�6f   $��25��6y   $	��Top=�5%      $��0��62   5&      �� 5'      $ 
Horizontal=�6   $
��Left=�62   $	��815��6M   $
��Auto��6Y   $��0,5,0,0q�6h   .+�5(      �� $
��Nama��6   $��Center=�6'   $��Right=�6I   $	��Top=�6l   $	��300��5)      $��Arial)�6$   $��25��67   $��0,0,0,0q�6C   �� 5*      $ 
Horizontal=�6   .+�5+      �� -   5        $ TbNama��5+      $��Center=�6.   $��5,0,0,0q�6P   $	��184��6a   $��25��6m   $��Arial)�6y   $	��Top=�5,      $��1��66   5-      5.      �� 5/      $ 
Horizontal=�6   $
��Left=�62   $	��815��6M   $��25��6Y   $��0,5,0,0q�6e   .+�50      �� $��Telpon��6   $��Center=�6)   $��Right=�6K   $��Center=�6n   $	��300��51      $��Arial)�6$   $��25��67   $��0,-1,0,0q�6C   �� 52      -   5        $ TbTelpon��52      $��Center=�6,   $��5,0,0,0q�6N   $	��184��6_   $��Arial)�6k   53      �� 54      $ 
Horizontal=�6   $
��Left=�62   $	��815��6M   $��25��6Y   $��0,5,0,0q�6e   .+�55      �� $��Alamat��6   $��Center=�6)   $��Right=�6K   $��Center=�6n   $	��300��56      $��Arial)�6$   $��25��67   $��0,-0.5,0,0.5q�6C   �� 57      -   5        $ TbAlamat��57      $��Center=�6,   $��5,0,0,0q�6N   $	��265��6_   $��Arial)�6k   $��25��6~   $	��Top=�6�   58      �� 59      $��30��6   $��0,30,0,0q�6%   $ 
Horizontal=�67   $��Center=�6P   $	��816��6m   .+�5:          ExtendedButton.ImageButton  -   5          Name$ BtnBaru��5:   -      Image$$ ../Images/SaveIconNoCircle.png��6>      
ImageWidth$ 10Q�6e      ImageHeight$ 12Q�6u   $��80��6�   $��	305,0,0,0q�6�   $��30��6�   $
��Baru��6�   6�     5;      -   5        $ 	BtnSimpan��5;   -   $$ ../Images/SaveIconNoCircle.png��6@   $ 10Q�6g   $ 12Q�6w   $��80��6�   $��10,0,0,0q�6�   $��30��6�   $��Simpan��6�   6�     5<      -   5        $ BtnHapus��5<   -   $&  ../Images/DeleteIconNoCircle.png��6?   $��10,0,0,0q�6h   $ 10Q�6z   $ 12Q�6�   $��Hapus��6�   $��80��6�   $��30��6�   6�     5=      -	   5        $ BtnClose��5=   -   $&  ../Images/DeleteIconNoCircle.png��6?   $��10,0,0,0q�6h   $ 10Q�6z   $ 12Q�6�   $��Tutup��6�   $��80��6�   $��30��6�   6�   5>      5?      �� 5A   
   -
   5         ��Name$ ListViewSupir��5A   '   $��
15,0,17,15q�6    �� SelectionMode$ Single=�6>    J� RenderTransformOrigin$ 1,0.5&�5B      $��13.333(�6"   $��3��6h   $��0��5C      $��2��6#   z�5D      V� 5E      .~�5F      �� �� 5G      �� 5H      T� 5I      5J       �� View 5L      �� 5M       �� AllowsColumnReorder .  �� ColumnHeaderToolTip$ MstSupir��66    �� ColumnHeaderContextMenu 5N      �� 5O      $��Arial)�6&   .@�5P      m� $��	Ascending��6'   m� 5Q      $��
Descending��6'   5R      .P�5T      �� $��Id��6%    �� Width$ 80��61    �� DisplayMemberBinding 6<   �� *	SupirId+�� 5U      $
��Nama��6%   $	 250��63    6?   �� *	NamaSupir+�� 5V      $��Telpon��6%   $	 175��65    6A   �� *Telpon+�� 5W      $��Alamat��6%   $	 300��65    6A   �� *AlamatSupir+5Y      5[      5\      5]      