   M S B A M L   `   `   `  ����N  JExchange Office WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Exchange_Office_WPFExchange_Office_WPF  5   	   !    Exchange_Office_WPF.Window3   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:Exchange_Office_WPF     5   	   -   5        $	��261��5   	   $	��529��6     � WindowStyle$
  None=�5	   	    � 
ResizeMode$ NoResize=�5
   	   
������  � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=�5   	   .��5      �� ���#DCDCDC �� CornerRadius$ 30��6"   $	��262��64   $��Center=�6A   $
��Left=�6\   $	��528��6w   ._�5   
   � $	��262��6   $��Center=�6   $��9,0,10,0q�67    � RowDefinitions 5      �� 5      .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ 	dataGrid1��5   �      DataGridStyle1#���   6     DataGridCellStyle1   	CellStyle# �  6A     DataGridColumnHeaderStyle1   ColumnHeaderStyle# �  6r     DataGridRowStyle1	   RowStyle#	 �  6�   
   AutoGenerateColumns
 .     
IsReadOnly .    CanUserResizeRows .     CanUserResizeColumns .     CanUserReorderColumns .  $��
10,15,0,23q�6z  $
��Left=�6�  ��6�  Z�    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Exchange office��6)      
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$	 120 6      CanUserSort .  1  +System.Windows.Controls.DataGridBoundColumn   Binding 6C   ��  �� Path$ nameOfTheOffice�6�     5      $ Number of sold��6)    . $	 120 6y    .   6A   �� $ numberOfSold�6�     5      $ Total sales amount��6)    . $	 150 6�    .   6E   �� $ totalSalesAmount�6�   5      �� 5      -   5        $��
Выйти��5      
������ 
�������$
��Left=�6Q   $��
423,15,0,0q�6l   $	��Top=�6�   $��22��6�   $��76��6�    J� RenderTransformOrigin$ 0.857,0.561&�6�    � Cursor$
 Hand��6�   
���   �6�   5      5      5      