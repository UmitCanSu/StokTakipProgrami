   M S B A M L   `   `   `  ����N  JStokTakipUygulamasi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:StokTakipUygulamasi.UserController"StokTakipUygulamasi.UserController  5      3    -StokTakipUygulamasi.UserController.ucAnasayfa   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Blocal0clr-namespace:StokTakipUygulamasi.UserController     5      -   5        .��5	      �   � RowDefinitions  5
   
   �� 5      �� 5      $��50 �6   .+�5   
   �� $��ÇALIŞANLAR��6   $��110,40,605,333q�6%   
������� K� ZIndex$ 3��6P   $
��Bold"�6a   �� 5   
    �� CornerRadius$ 30��6   ���	#FF03257F$	��250��6:   $	��200��6F   $	��Top=�6S   $
��Left=�6k   $��	30,30,0,0q�6�   ._�5      &   System.Windows.Controls.DataGrid  -   5          Name$ dtg_calisanlar��5      ���	#FF042263   
IsReadOnly .    	RowHeight$ 30Q�5   @      CanUserAddRows .     CanUserSortColumns .    CanUserReorderColumns .  	   CanUserResizeRows	 .  
   HorizontalGridLinesBrush
 �	#FF989B9B   VerticalGridLinesBrush �	#FFBBBBBB   HeadersVisibility$ Column=�5   _      AutoGenerateColumns .  $	��200��5   3   $	��250��6@   $��0,40,0,0q�6L   $	��Top=�6^   $
��Left=�6v   ��5   /   �� ��6G   Z� ��6]   Z�    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   
IsReadOnly .    Header$ ID��6W      MaxWidth$ 2Q�6c   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   ��  �� Path$ ID�   ElementStyle 5      �� 5       �� 
TargetType" ��6$   .�5   "   ��  �� Property � �� �� Value$
 Left=�6H   �� 5   "    � ��$ Center=�6F   �� 5   "    �@��Padding$ 5q�6<   �� 5    "    � ��$	 0.1(�6=   5!         	CellStyle 5$      �� 5%      *  $System.Windows.Controls.DataGridCell"  6$   .�5&   "   ��  � �� �#2e61af6?   �� 5'   "    � �� �	#FF1341876@   �� 5(   "    � ��$ 0,0,0,0q�6D   �� 5)   "    � ��
 �����6?   5+      5-        5/       . $
 Adı��6W      MinWidth$	 110Q�6d    �� TextAlignment$ Center=�6s   $��Expanded&�6�    6)   �� $ Ad� 50      �� 51      " ��6$   .�52   "   ��  � ��$
 Left=�6H   �� 53   "    � ��$ Center=�6F   �� 54   "    �@��Padding$ 5,0,5,0q�6<   55      57        59       . $ Soyadı��6Z   $	 135Q�6j   $ Center=�6y   $��Expanded&�6�    6)   �� $ Soyad� 5:      �� 5;      " ��6$   .�5<   "   ��  � ��$
 Left=�6H   �� 5=   "    � ��$ Center=�6F   �� 5>   "    �@��Padding$ 5,0,5,0q�6<   5?      5A      5E      5F      �� 5H   
   $	��120��6   $��30��6   $	��Top=�6)   $��Right=�6A   ���	#FF213874$��	0,40,40,0q�6t   $ 10��6�   ._�5I      �� �� 5J      $��Dolar: ��6   $��50��6*   
�������$
��Bold"�6H    �� 5K      -   5         ��Name$ lblDolar��5K      $�� ��6+    �� Left$	 724��66   
 �� Top$ 19��6H   $��48��6X   
�������$��Right=�6v   5L      5N      �� 5Q   
   
�������$��1��6$   ._�5R      ��  �� Orientation$ 
Horizontal=�6   $��Right=�62   .+�5S      d�    menubutonlar#���   6   $��	Buton - 1��6F   $	��150��6Z   $��40��6f   d� 5T      #���   6   $��	Buton - 2��6F   $	��150��6Z   $��40��6f   d� 5U      #���   6   $��	Buton - 3��6F   $	��150��6Z   $��40��6f   5V      5W      5Y      5Z      