   M S B A M L   `   `   `  ����H  DRestaurant.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Restaurant.UIRestaurant.UI  5   	       Restaurant.UI.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Restaurant.UI     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $
��auto �6    � ColumnDefinitions 5   
   �� 5      $��2* �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6"    �� Orientation$ 
Horizontal=�62   $��5q�6K   .+�5      �� $ 
Horizontal=�6   .+�5      �� $��Datum:��6   (  "System.Windows.Controls.DatePicker  5      -   5        $��Datum��5      $	��100��6*   $��0,0,5,0q�66   6I   5      �� 5      $ 
Horizontal=�6   .+�5      �� $��Aantalplaatsen:��6   �� 5      -   5        $��Aantalplaatsen��5      $	��100��60   $��0,0,5,0q�6<   5      5      �� 5   
   -   5        $��SearchRestaurantButton��5   .   $��0��6   $��1��6   $��Search Restaurants��6L   $��5q�6�   &   System.Windows.Controls.DataGrid  5   
   -   5        $��RestaurantsDataGrid��5      $��1��6.   $��0��6;   $��2��6K   $��5q�6_      
IsReadOnly$
 True��6j    � ContextMenu 5      �� 5       .@�5!      m� -   5        $��Registration��5!      6U   5"      5$      5%      5&      