   M S B A M L   `   `   `  ����I  EExtendedButton, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG4clr-namespace:ExtendedButton;assembly=ExtendedButtonExtendedButton  5   	   	 BPDMH!   BPDMH.MasterData.MasterUser   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   CEB4clr-namespace:ExtendedButton;assembly=ExtendedButton     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6F   -   5          � Title$  Master User��5   	   
����iA� � WindowStyle$ 
ToolWindow=�65    � WindowStartupLocation$ CenterScreen=�6e   $��Stretch=�5      $��Stretch=�6   $��Arial)�6=   $	��769��6P   .��5	      � $��0,0,58,0q�6   $��Stretch=�6    � ColumnDefinitions 5
   
   �� 5      $
��115* �6   �� 5      $
��220* �6   �� 5      $	��90* �6   �� 5      $
��184* �6   �� 5      $	��30* �6   �� 5      $	��63* �6   �� 5      $��0* �6   .+�5   
   �� 
������ $
��Left=�6'   $	��146��6B   
����iA�$	��Top=�6b   $	��693��6z   $��20,20,-10,0q�6�    �� RadiusY$	 2.5��5       �� RadiusX$	 2.5��6   $��7��6!   5      �� 5   
   $
��Left=�6   $��29��6/   $��33.5,48,0,0q�6;   $
��Wrap=�6P   $
��Kode��6d   $	��Top=�6p   $��49��6�   $��13.333(�6�   �� 5   
   $
��Left=�6   $��29��6/   $��33.5,82,0,0q�6;   $
��Wrap=�6P   $
��Nama��6d   $	��Top=�6p   $��74��6�   $��13.333(�6�   �� 5   
   $
��Left=�6   $��29��6/   $��33.5,116,0,0q�6;   $
��Wrap=�6Q   $��Jabatan��6e   $	��Top=�6t   $��74��6�   $��13.333(�6�   �� 5   
   -   5         ��Name$ TbKdUser��5      $��1��6$   $
��Left=�64   $��29��6O   $��
5.5,48,0,0q�6[    �� TextWrapping$
 Wrap=�6o   $	��Top=�6�   $��72��6�   $��13.333(�6�    �� SelectionBrush
 ��iA�	 �� 	MaxLength$	 10��5      �� 5   
   -   5        $ 
TbNamaUser��5      $��1��6&   $
��Left=�66   $��29��6Q   $��
5.5,82,0,0q�6]   $
 Wrap=�6q   $	��Top=�6�   $	��198��6�   $��13.333(�6�   $	 50��6�   �� 5   
   -   5        $ 	TbJabatan��5      $��1��6%   $
��Left=�65   $��29��6P   $��5.5,116,0,0q�6\   $
 Wrap=�6q   $	��Top=�6�   $	��198��6�   $��13.333(�6�   $	 50��6�   �� 5   
   $��5��6   
������ $
��Left=�67   $��30��6R   $��1.019,82,-20.929,0q�6^   
����iA�$	��Top=�6�   $��83��6�   $ 3��6�   $ 3��6�   $��2��6�       ExtendedButton.ImageButton  5   
   -   5        
  Name$
 BtnUserSimpan��5      $��Simpan��60   $��5��6A   $��Right=�6Q   $��
0,50,-16,0q�6m   $	��Top=�6�   $!��75                         ��6�   $��24��6�   
������ 
������    Image$$ ../Images/SaveIconNoCircle.png��5         
ImageWidth$ 10Q�6+      ImageHeight$ 10Q�6;   $��13.333(�6L   $��2��6^   �� 5    
   $��5��6   
������ $
��Left=�67   $��30��6R   $��1.019,116,-20.929,0q�6^   
����iA�$	��Top=�6�   $��83��6�   $ 3��6�   $ 3��6�   $��2��6�     5!   
   -   5        $
 BtnUserHapus��5!      $��Hapus��6/   $��5��6?   $
��Left=�6O   $��4.019,119,-16.929,0q�6j   $	��Top=�6�   $��76��6�   $��24��6�   
������ 
������ $&  ../Images/DeleteIconNoCircle.png��5"   9   $ 8Q�6b   $ 8Q�6q   $��13.333(�6�   $��2��6�     5#   
   -   5        $
 BtnClose��5#      $��Tutup��6+   $��5��6;   $
��Left=�6K   $��
5,85,-17,0q�6f   $	��Top=�6z   $��76��6�   $��24��6�   
������ 
������ $&  ../Images/DeleteIconNoCircle.png��5$      $ 8Q�6G   $ 8Q�6o   $��13.333(�6   $��2��6�   �� 5<   
   -   5         ��Name$ 	ListView1��5<   *   $��20,190,-10,37q�6   $��7��6=    �� SelectionMode$ Single=�6Q    J� RenderTransformOrigin$ 1,0.5&�5=   2   $��13.333(�6P   z�5>      V� 5?      .~�5@      �� �� 5A      �� 5B      T� 5C      5D       �� View 5F      �� 5G       �� AllowsColumnReorder .  �� ColumnHeaderToolTip$ Authors��66    �� ColumnHeaderContextMenu 5J      �� 5K      $��Arial)�6&   .@�5L      m� $��	Ascending��6'   m� 5M      $��
Descending��6'   5N      .P�5Q      �� $
��Kode��6%    �� Width$ 50��63    �� DisplayMemberBinding 6>   �� *
KaryawanId+�� 5R      $
��Nama��6%   $	 100��63    6?   �� *Nama+�� 5S      $��Jabatan��6%   $	 150��66    6B   �� *	Jabatan+�� 5T      $��Telpon��6%   $	 100��65    6A   �� *Telpon+�� 5U      $��Alamat��6%   $	 200��65    6A   �� *Alamat+�� 5V      $��Password��6%   $	 100��67    6C   �� *
Password+5m      5o      �� 5p   
   $
��Left=�6   $��29��6/   $��0.481,48,0,0q�6;   $
��Wrap=�6Q   $��Telpon��6e   $	��Top=�6s   $��49��6�   $��13.333(�6�   $��2��6�   �� 5q   
   $
��Left=�6   $��29��6/   $��0.481,82,0,0q�6;   $
��Wrap=�6Q   $��Alamat��6e   $	��Top=�6s   $��74��6�   $��13.333(�6�   $��2��6�   �� 5r   
   $
��Left=�6   $��29��6/   $��0.481,116,0,0q�6;   $
��Wrap=�6R   $��Password��6f   $	��Top=�6v   $��74��6�   $��13.333(�6�   $��2��6�   �� 5s   
   -	   5        $ TbTelpon��5s      $��3��6$   $
��Left=�64   $��29��6O   $��5.481,48,0,0q�6[   $
 Wrap=�6q   $	��Top=�6�   $��72��6�   $��13.333(�6�   
 ��iA�$	 10��5t   
   �� 5u   
   -
   5        $ TbAlamat��5u      $��3��6$   $
��Left=�64   $��29��6O   $��5.481,82,0,0q�6[   $
 Wrap=�6q   $	��Top=�6�   $	��198��6�   $��13.333(�6�   $	 50��6�   $��2��6�   �� 5v   
   -   5        $ 
TbPassword��5v      $��3��6&   $
��Left=�66   $��29��6Q   $��5.481,116,0,0q�6]   $
 Wrap=�6t   $	��Top=�6�   $	��198��6�   $��13.333(�6�   $	 50��6�   $��2��6�   5w      5x      