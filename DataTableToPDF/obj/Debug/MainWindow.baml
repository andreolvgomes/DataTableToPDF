   M S B A M L   `   `   `  ����  	DataToPDF    DataTableToPDF.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  
MainWindow��5   	   $	��800��6   $	��600��6(    � WindowStartupLocation$ CenterScreen=�65   .��5      � $��5q�6    � RowDefinitions 5	   
   �� 5
      $
��Auto �6   �� 5      $��* �6   .+�5   
   �  � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $
��Auto �6#    5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      �� $��0��6   $��ConnectionString��6%   �� 5      -   5         ��Name$ txtCnn��5      $��1��6&   $;��5server=.\sql2;database=bdsic;integrated security=sspi��63   �� 5      -   5        $��1��5      $��1��6%   $��Conectar...��62   $��18��6H   6T   5      � 5   
   -   5         �Name$ grdMain��5      $��1��6    {�.   5      �� 5      $
��Auto �6    �� 5      $��* �6    .+�5!      �� .+�5"      �� $��Select��6   �� 5#      -   5        $ txtQuery��5#       �� TextWrapping$
 Wrap=�6,   $��50��6@    �� AcceptsReturn . $I��Cselect top 100 Pro_descricao, Pro_pvenda, Pro_unidade from Produtos��6a   �� 5$       �� Orientation$ 
Horizontal=�6   .+�5%      �� -   5        $��Carrega Grid��5%      $	��150��64   $
��Left=�6@   6[   �� 5&      -   5        $��To PDF��5&      $	��150��6.   $
��Left=�6:   6U   5'      �� 5(      -   5        $ 	txtFilter��5(      $y�	Collapsed=�6Q   5)      &   System.Windows.Controls.DataGrid  5*      -   5        	  Name$		 dgv��5*   $   $��1��6   
�������   StyleDataGrid#��[  6D   
   AutoGenerateColumns
 . $��Normal"�6�   5+      5,      5-      