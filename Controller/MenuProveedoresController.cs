using GestordeStock.Model;
using GestordeStock.Model.DataBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml.Linq;

namespace GestordeStock.Controller
{
    internal class MenuProveedoresController
    {   //Show providers in Menu Product's   
        public void List_Combobox(ComboBox combo)
        {
            string dateshow = "NOMBRE_PROVEEDOR";
            string idselected = "ID_PROVEEDOR";
            Proveedor proveedor = new Proveedor();
            DataTable pedidostabla=proveedor.Listar_Combobox();
            if (pedidostabla==null)
            {
                MessageBox.Show("Ingrese Proveedores para ingresar productos", "Error");
                return;
            }

            DataRow newRow = pedidostabla.NewRow();
            newRow[dateshow] = "Seleccione un Proveedor";// Text for combobox
            newRow[idselected] = DBNull.Value;         // Default value item
            pedidostabla.Rows.InsertAt(newRow, 0);// Add row to datatable
            combo.DisplayMemberPath = dateshow; // value show
            combo.SelectedValuePath = idselected; // value selected
            combo.ItemsSource = pedidostabla.DefaultView;
            combo.SelectedIndex = 0;// load firt message default
        } 
        //TODO implementar listar categoria en combobox
        public void List_Combobox_categoria(ComboBox combo)
        {
            string dateshow = "NOMBRE_PROVEEDOR";
            string idselected = "ID_PROVEEDOR";
            Proveedor proveedor = new Proveedor();
            DataTable pedidostabla=proveedor.Listar_Combobox();
            if (pedidostabla==null)
            {
                MessageBox.Show("Ingrese Proveedores para ingresar productos", "Error");
                return;
            }

            DataRow newRow = pedidostabla.NewRow();
            newRow[dateshow] = "Seleccione un Proveedor";// Text for combobox
            newRow[idselected] = DBNull.Value;         // Default value item
            pedidostabla.Rows.InsertAt(newRow, 0);// Add row to datatable
            combo.DisplayMemberPath = dateshow; // value show
            combo.SelectedValuePath = idselected; // value selected
            combo.ItemsSource = pedidostabla.DefaultView;
            combo.SelectedIndex = 0;// load firt message default
        }
        public Boolean Check_providers()
        {
            string query = "SELECT IIF( EXISTS ( SELECT 1 FROM PROVEEDOR WITH (NOLOCK)), 1 , 0 );";//cheking if existe 
            Proveedor proveedor= new Proveedor();
            Helper helper = new Helper();
            if (helper.Check_if_exist(query) == 0)
            {
                MessageBox.Show("No se a encontrado el Proveedor");
                return false;
            }
            return true;
        }
        public void Delete_Providers(string id)
        {
            string query = "";
            int ID = 0;
            Validation validation = new Validation();
            Helper helper = new Helper();
            Dictionary<string, string> dic = new Dictionary<string, string>() { { "Id", id } };
            if (validation.Validation_strings(dic, "proveedor") == false)
            {
                return;
            }
            //validation number
            if (validation.Validation_numbers(dic) == false)
            {
                return;
            }
            ID = validation.transformint(id);
            //prepare the query whit ID
            query = $"SELECT IIF( EXISTS ( SELECT 1 FROM PROVEEDOR where ID_PROVEEDOR={ID}), 1 , 0 );";
            Proveedor proveedor = new Proveedor(ID);
            if (helper.Check_if_exist(query) == 0)
            {
                MessageBox.Show("No se a encontrado el Proveedor");
                return;
            }
            proveedor.Eliminar();
        }
        public bool Add_Provider(string rut, string name, string last_name, string contact, string mail, string description)
        {

            Helper helper = new Helper();
            //no contar rut, email,descripcion 
            Validation validation = new Validation();
            Rut rut_Validation = new Rut();
            //generate message and query to check's for validation
            string messageisexist = "El RUT del proveedor ya existe";
            string messageerror = "Ingrese un Rut del Proveedor";
            string query= $"SELECT IIF( EXISTS ( SELECT 1 FROM PROVEEDOR where RUT='{rut}'), 1 , 0 );";
            string errorcountnumberphone = "El número telefónico debe tener exactamente 9 dígitos.\nPor favor, ingrese un número válido.";
            //remove separation
            rut= rut.Trim();
            name = name.Trim();
            last_name = last_name.Trim();
            contact = contact.Trim();
            mail = mail.Trim();
            //this is special because for the content to generate more space
            description = description.Trim('\n', '\r', ' ');
            Dictionary<string, string> dic = new Dictionary<string, string>(){
                {"Rut",rut },
                {"Nombre",name },
                {"Apellido",last_name },
                {"Telefono",contact },
                {"Mail",mail }
            };
            int phonnumber = 0;
            //Validation if string's are empty
            if (validation.Validation_strings(dic, "Proveedor") == false)
            {
                return false;
            }
            if (rut_Validation.ValidaRut(rut) == false)
            {
                return false;
            }
            //Validation if existe same rut
            if (helper.existEntity(query,messageisexist,messageerror)==false)
            {
                return false;
            }
            //Validation characters especial (cut the dictionary)
            if (validation.Validation_characters_especials(dic, 2) == false)
            {
                return false;
            }

            //validation number
            if (validation.Validation_numbers(dic, 3) == false)
            {
                return false;
            }
            if (!validation.countnumberphone(contact, 9, errorcountnumberphone))
            {
                return false;
            }
            //validation mail
            if (!IsValidEmail(mail))
            {
                return false;
            }
            //tranform to int 
            phonnumber = validation.transformint(contact);
            Proveedor proveedor = new Proveedor(rut, name, last_name, phonnumber, mail, description);
            proveedor.Agregar();
            return true;
        }
        public bool Update_Providers(int id, string rut, string name, string last_name, string contact, string mail, string description)
        {
            //no contar rut, email,descripcion 
            Validation validation = new Validation();
            Helper helper = new Helper();
            Rut rut_Validation = new Rut();
            string errorcountnumberphone = "El número telefónico debe tener exactamente 9 dígitos.\nPor favor, ingrese un número válido.";
            string query = $"SELECT IIF( EXISTS ( select ID_PROVEEDOR from PROVEEDOR where ID_PROVEEDOR=(select ID_PROVEEDOR from PROVEEDOR where RUT='{rut}') and ID_PROVEEDOR={id}), 1 , 0 );";
            //remove separation
            rut = rut.Trim();
            name = name.Trim();
            last_name = last_name.Trim();
            contact = contact.Trim();
            mail = mail.Trim();
            //this is special because for the content to generate more space
            description = description.Trim('\n', '\r', ' ');
            Dictionary<string, string> dic = new Dictionary<string, string>(){
                {"Rut",rut },
                {"Nombre",name },
                {"Apellido",last_name },
                {"Telefono",contact },
                {"Mail",mail }
            };
            int phonnumber = 0;
            //Validation if string's are empty
            if (validation.Validation_strings(dic, "Proveedor") == false)
            {
                return false;
            }
            if (rut_Validation.ValidaRut(rut) == false)
            {
                return false;
            }
            /*
             obtener el id por medio de una consulta y validar que sea el mismo
             */
            //Validation if existe same rut
            if (helper.Check_if_exist(query)==0)
            {
                MessageBox.Show("El RUT del proveedor ya existe", "Error");
                return false;
            }
            if (validation.Validation_characters_especials(dic, 2) == false)
            {
                return false;
            }

            //validation number
            if (validation.Validation_numbers(dic, 3) == false)
            {
                return false;
            }
            if (!validation.countnumberphone(contact, 9, errorcountnumberphone))
            {
                return false;
            }
            //validation mail
            if (!IsValidEmail(mail))
            {
                return false;
            }
            //tranform to int 
            phonnumber = validation.transformint(contact);
            Proveedor proveedor = new Proveedor(id, rut, name, last_name, phonnumber, mail, description);
            proveedor.Actualizar();
            return true;
        }
        public void List_providers(DataGrid dataGrid)
        {
            Proveedor proveedor = new Proveedor();
            dataGrid.ItemsSource = proveedor.Listar().DefaultView;
        }
        public void List_uniq_provider(DataGrid dataGrid,string buscador)
        {
            Proveedor proveedor = new Proveedor();
            if (ValidationSearch(buscador)==false)
            {
                List_providers(dataGrid);
            }
            dataGrid.ItemsSource = proveedor.Listar_unico(buscador).DefaultView;
        }
        #region Validation
        public string StringFromRichTextBox(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(
                // TextPointer to the start of content in the RichTextBox.
                rtb.Document.ContentStart,
                // TextPointer to the end of content in the RichTextBox.
                rtb.Document.ContentEnd
            );

            // The Text property on a TextRange object returns a string
            // representing the plain text content of the TextRange.
            return textRange.Text;
        }


        //Validation mail
        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                MessageBox.Show("Ingrese un mail valido", "Error");
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                MessageBox.Show("Ingrese un mail valido", "Error");
                return false;
            }
        }
        #endregion
        public String[] Get_namesproviders(string nombreCompleto)
        {
            try
            {
                string[] nombreseparador = nombreCompleto.Split(' ');
                return nombreseparador;
            }
            catch (Exception)
            {
                MessageBox.Show("No se a podido encontrar el nombre del proveedor", "Error");
                return null;
            }
        } 
        public Paragraph  Get_paragrafth(string parrafo)
        {
            // Crear un objeto de tipo 'Paragraph' para agregar texto
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(parrafo);
            
            return paragraph;
        }
        /// <summary>
        /// switch the value of "if" to 0 because to use anoter query to get diferent result
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public bool CheckProviderforRUT(string query)
        {
            try
            {
                Proveedor proveedor = new Proveedor();
                Helper Helper = new Helper();
                if (Helper.Check_if_exist(query)== 0)
                {
                    MessageBox.Show("El RUT del proveedor ya existe", "Error");
                    return false;
                }
                return true;
            }
            catch (Exception)
            {

                MessageBox.Show("Ingrese un Rut del Proveedor", "Error");
                return false;
            }
        }
        private bool ValidationSearch(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return false;
            }
            return true;
        }
    }
}
