   M S B A M L   `   `   `  ����H  DGestordeStock, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:GestordeStock.ViewGestordeStock.View  5      )    #GestordeStock.View.Menu_Proveedores   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:GestordeStock.View     5      .��5	      �    Background-grid#��[  6     � ColumnDefinitions  5
   
   �� 5      $��1* �6   6,    � RowDefinitions 5   
   �� 5      $��1* �6   6+   �� 5      $��4* �6   6*   �� 5      $��5* �6   6*   .+�5   
   �� -   5        $��Titulo��5      $��Menu Proveedores��6     
Label:Menu#��[ 69   6_   � 5   
   $��1��6   .+�5      �� -   5         ��Name$ Rut_texblock��5      $	��Rut��6.   $��90,10,630,150q�69     Text#��[ 6P   6p   �� 5      -   5         ��Name$	 Rut��5      $��145,5,0,145q�6#   $
��Left=�6�   6�   �� 5      -   5        $ Nombre_texblock��5      $��Nombre��61   $��90,55,630,105q�6?   #��[ 6V   6w   �� 5      -   5        $ Nombre��5      $��145,50,0,100q�6&   $
��Left=�6<   6Z   �� 5      -   5        $ Apellido_texblock��5      $��Apellido��63   $��90,100,630,60q�6C   #��[ 6Z   6{   �� 5      -   5        $ Apellido��5      $��145,95,0,55q�6(   $
��Left=�6=   6[   �� 5       -   5        $ Contacto_texblock��5       $��Telefono��63   $��90,145,630,15q�6C   #��[ 6Z   6z   �� 5!      -	   5        $ Contacto��5!      $��145,140,0,10q�6(   $
��Left=�6t   6�   �� 5"      -
   5        $ Mail_texblock��5"      $
��Mail��6/   $��385,10,335,150q�6;   #��[ 6S   6t   �� 5#      -   5        $
 Mail��5#      $��460,5,0,145q�6$   $
��Left=�69   6W   �� 5$      -   5        $ Descripcion_texblock��5$      $��Descripcion��66   $��385,55,335,105q�6I   #��[ 6a   6�   �� 5%      -   5         ��Name$ Descripcion��5%      $��460,55,90,45q�6/   $	��250��6E   $��80��6Q   6_   �� 5'      -   5         ��Name$ Cancelar��5'      $��Cancelar��6'   $��0,140,-220,0q�6:   6i   �� 5(      -   5        $ Accion��5(      $��Agregar��6%   $��520,140,0,0q�67   6c   5)      � 5,   
   $��2��6   .+�5-      �� -   5        $  Buscar_proveedor_textblock��5-      $��Buscar Proveedor��6<   $��0,0,300,190q�6T   #��[ 6i   6�   �� 5.      -   5        $ Buscar_proveedor��5.      $��310,0,0,190q�61   6F   �� 50      -   5        $ watermark_text_search��50      $"��Ingrese nombre del proveedor��67   $��	5,0,0,185q�6[     watermark_search_Proveedor#��[ 6n   6�   �� 51      -   5        $ Buscar��51      $��260,0,0,190q�6%     ButtonSearch#��[ 6:   ��52      �� 53       �� Orientation$ 
Horizontal=�6!   .+�55      �� $��/IMG/Search.png��6      icon_Search#��[ 69   �� 57      $��Buscar��6$     Text_search#��[ 62   58      5:      &   System.Windows.Controls.DataGrid  5;      -   5          Name$ DGProveedor��5;      ��6>   ��    Columns 5<      0  *System.Windows.Controls.DataGridTextColumn  5=      -   5        colum_Nombre��,  &System.Windows.Controls.DataGridColumn	   Header$	 Nombre��5=   ?   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6O   �� *NOMBRE_PROVEEDOR+  5>      -   5        	colum_Rut��$		 RUT��5>   <   
 6I   �� *RUT+  5?      -   5        colum_Telefono��$	 Telefono��5?   A   
 6S   �� *
TELEFONO+  5@      -   5        
colum_Mail��$
	 Mail��5@   =   
 6K   �� *MAIL+  5A      -   5        colum_Descripcion��$	 Descripcion��5A   D   
 6Y   �� *DESCRIPCION+4  .System.Windows.Controls.DataGridTemplateColumn  5C         CellTemplate 5D      �� 5E      .R�5F   "   �� $ 
Horizontal=�6-   .+�5G   &   �� $ 
Actualizar��6-   -   5          ButtonDataGrid#��[ 5G   A   .��5H   *   ��   icon_datagrid#��[ 60   $��/Img/Upadte.png��6W   5I   '   �� 5J   &   $ Eliminadatos��6-   -   5        #��[ 5J   C   .��5K   *   �� #��[ 60   $��/Img/Delete.png��6W   5L   '   5M   #   5N      5P      5R      5S      5U      5V      