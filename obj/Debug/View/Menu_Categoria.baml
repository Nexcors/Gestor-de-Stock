   M S B A M L   `   `   `  ����H  DGestordeStock, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:GestordeStock.ViewGestordeStock.View  5      '    !GestordeStock.View.Menu_Categoria   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:GestordeStock.View     5      .��5	      �    Background-grid#��[  6     � ColumnDefinitions  5
   
   �� 5      $��1* �6   6,   �� 5      $��1* �6   6,    � RowDefinitions 5   
   �� 5      $��1* �6   6+   �� 5      $��5* �6   6*   .+�5   
   �� -   5        $��Titulo��5      $��Menu Categoria��6     
Label:Menu#��[ 67   6]   � 5   
   $��1��6   $��0��6   .+�5      �� -   5         ��Name$ Nombre_texblock��5      $��Nombre��61   $��
0,0,200,80q�6?     Text#��[ 6S   6s   �� 5      -   5         ��Name$ Nombre��5      $��
160,0,0,80q�6&   $
��Left=�6:   6W   �� 5      -   5         ��Name$ Accion��5      $��Agregar��6%   $��
130,50,0,0q�67   6b   �� 5      -   5        $ Cancelar��5      $��Cancelar��6'   $��
0,50,180,0q�6:   6g   5      � 5    
   $��1��6   $��1��6   .+�5!      �� -   5        $  Buscar_proveedor_textblock��5!      $��Buscar Categoria��6<   $��0,0,250,300q�6T   #��[ 6i   6�   �� 5"      -   5        $ Buscar_categoria��5"      $��130,0,0,300q�61   6F   �� 5$      -   5        $ watermark_text_search��5$      $$��Ingrese nombre de la categoria��67   $��
45,0,0,295q�6]     watermark_search_Categoria#��[ 6q   6�   �� 5%      -	   5        $ Buscar��5%      $��300,0,0,300q�6%     ButtonSearch#��[ 6:   ��5&      �� 5'       �� Orientation$ 
Horizontal=�6!   .+�5)      �� $��/IMG/Search.png��6      icon_Search#��[ 69   �� 5+      $��Buscar��6$     Text_search#��[ 62   5,      5.      &   System.Windows.Controls.DataGrid  50      -
   5          Name$ 
DGCategory��50        Datagrid:Categorias#��[ 6U   ��6=   ��    Columns 51      0  *System.Windows.Controls.DataGridTextColumn  52      -   5        colum_Nombre��,  &System.Windows.Controls.DataGridColumn   Header$ Nombre��52   ?   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6O   �� *NOMBRE_CATEGORIA+4  .System.Windows.Controls.DataGridTemplateColumn  55      
   CellTemplate
 56      �� 57      .R�58   "   �� $ 
Horizontal=�6-   .+�5:   &   �� $ 
Actualizar��6-   -   5          ButtonDataGrid#��[ 5:   A   .��5;   *   ��  	 icon_datagrid#��[	 60   $��/Img/Upadte.png��6W   5<   '   �� 5=   &   $ Eliminadatos��6-   -   5        #��[ 5=   C   .��5>   *   �� #��[	 60   $��/Img/Delete.png��6W   5?   '   5@   #   5A      5C      5E      5F      5I      5J      