using GestordeStock.Controller;
using GestordeStock.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GestordeStock.View
{
    /// <summary>
    /// Lógica de interacción para Menu_Categoria.xaml
    /// </summary>
    public partial class Menu_Categoria : UserControl
    {
        private string id;
        public Menu_Categoria()
        {
            InitializeComponent();
            LoadCategory();
            ColumreadOnly();
        }
        #region Button action´s
        private void Accion_Click(object sender, RoutedEventArgs e)
        {
            string name = Nombre.Text;
            CategoriaController categoria = new CategoriaController();
            if (categoria.Add_Category(name) == false)
            {
                return;
            }
            LoadCategory();
            Clear_field();
        }
        #endregion
        private void Actualizar_Click(object sender, RoutedEventArgs e)
        {
            Clear_field();
            MenuProveedoresController proveedoresController = new MenuProveedoresController();
            try
            {
                DataRowView dataRowView = this.Get_valuesDatagird(sender);

                // Acceder a los valores de las columnas directamente desde el DataRowView
                id = dataRowView["ID_CATEGORIA"].ToString();

                Nombre.Text = dataRowView["NOMBRE_CATEGORIA"].ToString();
                ChangebuttonAction(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            //Reload DataGrid Providers
        }

        private void Eliminadatos_Click(object sender, RoutedEventArgs e)
        {
            CategoriaController categoriaController = new CategoriaController();
            try
            {
                DataRowView dataRowView = this.Get_valuesDatagird(sender);
                // Acceder a los valores de las columnas directamente desde el DataRowView
                id = dataRowView["ID_CATEGORIA"].ToString();
                categoriaController.Delete_Category(id);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            //Reload DataGrid Providers
            this.LoadCategory();
        }

        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            CategoriaController categoriaController = new CategoriaController();
            string busqueda = Buscar_categoria.Text;
            categoriaController.List_uniq_category(DGCategory, busqueda);
        }



        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Clear_field();
            ChangebuttonAction(2);
        }
        private void accion_Actualizar_Click(object sender, RoutedEventArgs e)
        {
            CategoriaController categoriaController = new CategoriaController();
            int idupdate = int.Parse(id);
            string name = Nombre.Text;
            if (categoriaController.Update_Category(idupdate,name)==false)
            {
                return;
            }
            this.LoadCategory();
            //Clear all fields
            this.Clear_field();
            this.ChangebuttonAction(2);

        }
        #region Action's to interface
        private void Clear_field()
        {
            Nombre.Text = string.Empty;
        }
        private void ChangebuttonAction(int op)
        {

            if (op == 1)
            {
                Accion.Content = "Actualizar";
                Accion.Click -= Accion_Click;
                Accion.Click += accion_Actualizar_Click;
            }
            else if (op == 2)
            {
                Accion.Content = "Agregar";
                Accion.Click -= accion_Actualizar_Click;
                Accion.Click += Accion_Click;
            }
        }
        private void ColumreadOnly()
        {
            //Put colum´s of datagrid in value readOnly
            this.DGCategory.Columns[0].IsReadOnly = true;
        }
        #endregion

        private void LoadCategory()
        {
            CategoriaController categoriaController = new CategoriaController();
            categoriaController.List_Category(DGCategory);
        }
        private DataRowView Get_valuesDatagird(object sender)
        {
            //Obtien the values of cell and for the use them we needd to combert to object DataRowView
            DataRowView dataRowView = (DataRowView)((FrameworkElement)sender).DataContext;
            if (dataRowView == null)
            {
                MessageBox.Show("Datos de la categoria no encontrados");
                return null;
            }
            return dataRowView;
        }
        //Reload datagrid when the field of search is empty
        private void Buscar_categoria_TextChanged(object sender, TextChangedEventArgs e)
        {
            string nombre = Buscar_categoria.Text;
            if (string.IsNullOrEmpty(nombre))
            {
                this.LoadCategory();
            }
        }

    }
}
