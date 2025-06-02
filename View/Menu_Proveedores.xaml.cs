using GestordeStock.Controller;
using GestordeStock.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Xml.Linq;

namespace GestordeStock.View
{
    /// <summary>
    /// Lógica de interacción para Menu_Proveedores.xaml
    /// </summary>
    public partial class Menu_Proveedores : UserControl
    {
        /// <summary>
        /// the use of this variable is for:
        /// to save the id when te user puch the button update and this use them to update this value
        /// </summary>
        private string id;
        public Menu_Proveedores()
        {
            InitializeComponent();
            LoadProviders();
            ColumreadOnly();
        }
        #region Button action´s
        private void Accion_Click(object sender, RoutedEventArgs e)
        {
            MenuProveedoresController controllerProvider = new MenuProveedoresController();
            string rut = Rut.Text;
            string name = Nombre.Text;
            string last_name = Apellido.Text;
            string contact = Contacto.Text;
            string mail = Mail.Text;
            string description = controllerProvider.StringFromRichTextBox(Descripcion);//Obtiene el texto

            if (controllerProvider.Add_Provider(rut, name, last_name, contact, mail, description) == false)
            {
                return;
            }
            //Reaload providres 
            this.LoadProviders();
            //Clear all fields
            this.Clear_field();
        }
        /// <summary>
        /// Button located in datagrid acction eliminated provider and check if exist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Eliminadatos_Click(object sender, RoutedEventArgs e)
        {

            MenuProveedoresController proveedoresController = new MenuProveedoresController();
            Controller.Validation validation = new Controller.Validation();
            try
            {
                DataRowView dataRowView = this.Get_valuesDatagird(sender);
                // Acceder a los valores de las columnas directamente desde el DataRowView
                id = dataRowView["Id"].ToString();
                if (validation.Check_delete_Datagrid(id,"Proveedor")==false)
                {
                    return;
                }

                proveedoresController.Delete_Providers(id);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            //Reload DataGrid Providers
            this.LoadProviders();
        }
        /// <summary>
        /// Button located in datagrid. Changed the action Accion to update and 
        /// put the information in the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Actualizar_Click(object sender, RoutedEventArgs e)
        {
            Clear_field();

            string[] nombreCompleto;
            MenuProveedoresController proveedoresController = new MenuProveedoresController();
            try
            {
                DataRowView dataRowView = this.Get_valuesDatagird(sender);

                // Acceder a los valores de las columnas directamente desde el DataRowView
                id = dataRowView["Id"].ToString();
                //use the metod separate for nombreCompleto => " " 
                nombreCompleto = proveedoresController.Get_namesproviders(dataRowView["NOMBRE_PROVEEDOR"].ToString());
                Nombre.Text = nombreCompleto[0];
                Apellido.Text = nombreCompleto[1];
                Rut.Text = dataRowView["RUT"].ToString();
                Contacto.Text = dataRowView["TELEFONO"].ToString();
                Mail.Text = dataRowView["MAIL"].ToString();
                //use the metod convert to paragrafth to use in richtextbox
                Descripcion.Document.Blocks.Add(proveedoresController.Get_paragrafth(dataRowView["DESCRIPCION"].ToString().Trim()));
                //poner en nuevo metodo 
                //cambiar el tipo de boton de Agregar a actualizar 
                ChangebuttonAction(1);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            //Reload DataGrid Providers
        }
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Clear_field();
            ChangebuttonAction(2);
        }
        //Button to update data from provider
        private void accion_Actualizar_Click(object sender, RoutedEventArgs e)
        {
            MenuProveedoresController controllerProvider = new MenuProveedoresController();
            int idupdate = int.Parse(id);
            string rut = Rut.Text;
            string name = Nombre.Text;
            string last_name = Apellido.Text;
            string contact = Contacto.Text;
            string mail = Mail.Text;
            string description = controllerProvider.StringFromRichTextBox(Descripcion);//Obtiene el texto

            if (controllerProvider.Update_Providers(idupdate, rut, name, last_name, contact, mail, description) == false)
            {
                return;
            }
            //Reaload providres 
            this.LoadProviders();
            //Clear all fields
            this.Clear_field();
            this.ChangebuttonAction(2);
        }
        #endregion

        #region Action's to interface
        private void LoadProviders()
        {
            //DGProveedor
            MenuProveedoresController controllerProvider = new MenuProveedoresController();
            controllerProvider.List_providers(DGProveedor);
        }
        private void ColumreadOnly()
        {
            //Put colum´s of datagrid in value readOnly
            this.DGProveedor.Columns[0].IsReadOnly = true;
            this.DGProveedor.Columns[1].IsReadOnly = true;
            this.DGProveedor.Columns[2].IsReadOnly = true;
            this.DGProveedor.Columns[3].IsReadOnly = true;
            this.DGProveedor.Columns[4].IsReadOnly = true;
        }
        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            MenuProveedoresController controllerProvider = new MenuProveedoresController();
            string busqueda = Buscar_proveedor.Text;

            controllerProvider.List_uniq_provider(DGProveedor, busqueda);
            //buscar el nombre del proveedor 
        }
        #region Validation Rut
        private void RUTTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Solo permitir caracteres numéricos y la letra 'K'
            if (!char.IsDigit(e.Text, 0) && e.Text.ToUpper() != "K")
            {
                e.Handled = true;
            }
        }
        // Evento cuando el texto cambia en el TextBox
        private void RUTTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Obtener el texto del RUT sin puntos ni guion
            string input = Rut.Text.Replace(".", "").Replace("-", "");

            // Formatear el RUT con puntos y guion solo cuando haya al menos 2 dígitos
            if (input.Length >= 2)
            {
                string rutNumerico = input.Substring(0, input.Length - 1); // Todos los números excepto el dígito verificador
                char dv = input[input.Length - 1]; // El último carácter es el dígito verificador

                rutNumerico = Regex.Replace(rutNumerico, @"(\d)(?=(\d{3})+(?!\d))", "$1.");

                // Añadir el guion solo cuando haya al menos un dígito verificador
                if (rutNumerico.Length > 0)
                {
                    Rut.Text = rutNumerico + "-" + dv;
                    Rut.SelectionStart = Rut.Text.Length;  // Coloca el cursor al final del texto
                }
            }
            else
            {
                // En caso de que el texto sea muy corto, no agregar el guion ni los puntos
                Rut.Text = input;
            }
        }

        #endregion
        #region Validation contact
        /// <summary>
        /// metothd to validate the textbloc Contacto. 
        /// Only use number's for the method regex to validate the input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Contacto_texblock_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        #endregion
        //Clear all fields of form
        private void Clear_field()
        {
            Rut.Text = string.Empty;
            Nombre.Text = string.Empty;
            Apellido.Text = string.Empty;
            Contacto.Text = string.Empty;
            Mail.Text = string.Empty;
            Descripcion.Document.Blocks.Clear();
        }
        //Method whit take all caracters form Datagrid
        private DataRowView Get_valuesDatagird(object sender)
        {
            //Obtien the values of cell and for the use them we needd to combert to object DataRowView
            DataRowView dataRowView = (DataRowView)((FrameworkElement)sender).DataContext;
            if (dataRowView == null)
            {
                MessageBox.Show("Datos del proveedor no encontrados");
                return null;
            }
            return dataRowView;
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
        //Reload datagrid when the field of search is empty
        private void Buscar_proveedor_TextChanged(object sender, TextChangedEventArgs e)
        {
            string nombre= Buscar_proveedor.Text;
            if (string.IsNullOrEmpty(nombre))
            {
                this.LoadProviders();
            }
        }
        #endregion
    }
}
