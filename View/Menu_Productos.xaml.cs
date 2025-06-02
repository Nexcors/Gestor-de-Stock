using GestordeStock.Controller;
using GestordeStock.Model;
using GestordeStock.Model.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GestordeStock.View
{
    /// <summary>
    /// Lógica de interacción para Menu_Productos.xaml
    /// </summary>
    public partial class Menu_Productos : UserControl
    {

        public Menu_Productos()
        {
            InitializeComponent();
            MenuProveedoresController controllerProveedor = new MenuProveedoresController();
            controllerProveedor.List_Combobox(Proveedores);
        }

        private void Accion_Click(object sender, RoutedEventArgs e)
        {
            MenuProductsController productsController = new MenuProductsController();

            string name = Nombre.Text;
            string value = Precio.Text;
            string lot = Cantidad.Text;
            string providers = Proveedores.SelectedValuePath.ToString();//In base to combobox obtein id

            productsController.ADD_Product(name,value,lot);

            //TODO hacer controller producto has proveedor
            //Clean text area's
            //Nombre.Text = string.Empty;
            //Precio.Text = string.Empty;
            //Cantidad.Text = string.Empty;
        }

        private void Eliminadatos_Click(object sender, RoutedEventArgs e)
        {
            MenuProductsController productsController = new MenuProductsController();
            //Obtien the values of cell and for the use them we needd to combert to object DataRowView
            DataRowView dataRowView = (DataRowView)((FrameworkElement)sender).DataContext;
            if (dataRowView != null)
            {
                // Acceder a los valores de las columnas directamente desde el DataRowView
                int id = Convert.ToInt32(dataRowView["Id"]);
                string nombre = dataRowView["NOMBRE_PROVEEDOR"].ToString();
            }

        }
    }
}
