using GestordeStock.Controller;
using GestordeStock.Model;
using GestordeStock.View;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace Gestor_de_Stock
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            firstMenulook();
            
        }
        public void firstMenulook()
        {
            //Validate if exist Proveedor
            MenuProveedoresController menuProveedoresController = new MenuProveedoresController();
            CategoriaController categoriaController = new CategoriaController();
            //Validate if exist 

            //Validate if exist 
            if (menuProveedoresController.Check_providers() == false)
            {
                Muestramenus.Children.Add(new Menu_Proveedores());
                return;
            }
            else if (categoriaController.Check_category() == false)
            {
                Muestramenus.Children.Add(new Menu_Categoria());
                return;
            }
            Muestramenus.Children.Add(new Menu_Productos());

        }
        /// <summary>
        /// Validate if existy record of provider and category.
        /// if not have return false and show message of alert of 
        /// </summary>
        /// <returns></returns>
        public Boolean checkifExist()
        {
            //Validate if exist Proveedor
            MenuProveedoresController menuProveedoresController = new MenuProveedoresController();
            CategoriaController categoriaController = new CategoriaController();
            //Validate if exist 
            if (menuProveedoresController.Check_providers() == false)
            {
                MessageBox.Show("Ingrese almenos un registro de proveedor","Alerta");
                Muestramenus.Children.Add(new Menu_Proveedores());
                return false;
            }
            else if (categoriaController.Check_category() == false)
            {
                MessageBox.Show("Ingrese almenos un registro de categoria", "Alerta");
                Muestramenus.Children.Add(new Menu_Categoria());
                return false;
            }
            return true;
        }
        #region Menu
        private void productos_Click(object sender, RoutedEventArgs e)
        {
            if (checkifExist()==false)
            {
                return;
            }
            Muestramenus.Children.Clear();
            Muestramenus.Children.Add(new Menu_Productos());
        }
        private void proveedores_Click(object sender, RoutedEventArgs e)
        {//menu proveedores
            Muestramenus.Children.Clear();
            Muestramenus.Children.Add(new Menu_Proveedores());
        }
        private void categorias_Click(object sender, RoutedEventArgs e)
        {
            Muestramenus.Children.Clear();
            Muestramenus.Children.Add(new Menu_Categoria());
        }
        private void Minimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion
        #region Move windows
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Verifica si el botón izquierdo del ratón fue presionado
            if (e.ChangedButton == MouseButton.Left)
            {
                // Permite que la ventana sea movida al arrastrar
                this.DragMove();
                if (!(sender is Button))
                {
                    e.Handled = true;
                }
            }
        }
        #endregion
    }
}
