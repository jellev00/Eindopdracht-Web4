   M S B A M L   `   `   `  ����H  DRestaurant.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Restaurant.UIRestaurant.UI  5   	       Restaurant.UI.Registartie   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Restaurant.UI     5   	     � Title$  Registartie��5   	   $	��450��6   $	��800��6*   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $
��auto �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��2* �6   �� 5      $��* �6   �� 5      $��2* �6   .+�5   
   �� $��0��6   $��0��6   $��AantalPlaatsen��6-   $��5q�6F   �� 5   
   $��0��6   $��2��6   $��Datum��6-   $��5q�6=   �� 5   
   $��1��6   $��0��6   $	��Uur��6-   $��5q�6;   �� 5   
   $��1��6   $��2��6   $��Minuten��6-   $��5q�6?   �� 5   
   -   5        $��AantalPlaatsen��5   /   $��0��6   $��1��6   $��5q�6E   (  "System.Windows.Controls.DatePicker  5   
   -   5        $��Datum��5   2   $��0��6   $��3��6"   $��5q�6?   6L   �� 5   
   -   5        $	��Uur��5   0   $��1��6   $��1��6    $��5q�6;   .@�5      �� .��6   126   �� 5      .��6   136   �� 5       .��6   146   �� 5!      .��6   156   �� 5"      .��6   166   �� 5#      .��6   176   �� 5$      .��6   186   �� 5%      .��6   196   �� 5&      .��6   206   �� 5'      .��6   216   5(      �� 5)   
   -   5        $��Minuten��5)   0   $��1��6   $��3��6    $��5q�6?   .@�5*      �� .��6   006   �� 5+      .��6   306   5,      &   System.Windows.Controls.DataGrid  5.   
   -   5        $��OrganiserDataGrid��5.   O   $��2��6   $��0��6    $��4��60   $��5q�6D   6O   �� 5/   
   -   5        $��CancelButton��5/   M   $��3��6   $��0��6   $��2��6.   $��5q�6B   $��Cancel��6a   6r   �� 50   
   -   5        $��	AddButton��50   M   $��3��6   $��2��6   $��2��6.   $��5q�6B   $	��Add��6^   6l   51      52      