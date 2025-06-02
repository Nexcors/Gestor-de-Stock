using GestordeStock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml.Linq;

namespace GestordeStock.Controller
{
    internal class CategoriaController
    {
        public void List_Category(DataGrid dataGrid)
        {
            Categoria categoria = new Categoria();
            dataGrid.ItemsSource= categoria.Listar().DefaultView;
        }
        /// <summary>
        /// en base al parametro buscador valida dicha variable si es null esta 
        /// devuelve la lista en su formato predeterminado, sino esta va al procedimiento
        /// de listar unico pasando el parametro buscador para la sentencia "sql"
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="buscador"></param>
        public void List_uniq_category(DataGrid dataGrid, string buscador)
        {
            Categoria categoria = new Categoria();
            if (ValidationSearch(buscador) == false)
            {
                List_Categorys(dataGrid);
            }
            dataGrid.ItemsSource = categoria.Listar_unico(buscador).DefaultView;
        }
        public void List_Categorys(DataGrid dataGrid)
        {
            Categoria categoria = new Categoria();
            dataGrid.ItemsSource = categoria.Listar().DefaultView;
        }
        private bool ValidationSearch(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return false;
            }
            return true;
        }
        public Boolean Check_category()
        {
            string query = "SELECT IIF( EXISTS ( SELECT 1 FROM CATEGORIA WITH (NOLOCK)), 1 , 0 );";//cheking if existe 
            Helper helper = new Helper();
            if (helper.Check_if_exist(query) == 0)
            {
                return false;
            }
            return true;
        }
        public bool Add_Category(string name)
        {
            Proveedor proveedor_check = new Proveedor();
            Helper helper = new Helper();
            //no contar rut, email,descripcion 
            Validation validation = new Validation();
            //remove separation
            name = name.Trim();
            string query = $"SELECT IIF( EXISTS ( SELECT 1 FROM CATEGORIA where NOMBRE_CATEGORIA='{name}'), 1 , 0 );";
            string messageisexist = "El nombre de la categoria ya existe";
            string messageerror = "Ingrese nombre del proveedor";
            Dictionary<string, string> dic = new Dictionary<string, string>(){
                {"Nombre",name}
            };
            //Validation if string's are empty
            if (validation.Validation_strings(dic, "Categoria") == false)
            {
                return false;
            }
            //Validation if existe same category
            if (helper.existEntity(query, messageisexist, messageerror) == false)
            {
                return false;
            }
            Console.WriteLine(name);
            Categoria categoria = new Categoria(name);
            categoria.Agregar();
            return true;
        }
        public bool Update_Category(int id, string name)
        {

            Validation validation = new Validation();
            Helper helper = new Helper();
            //remove separation
            name = name.Trim();
            string query = "";
            Dictionary<string, string> dic = new Dictionary<string, string>(){
                {"Nombre",name }
            };
            if (validation.Validation_strings(dic, "Categoria") == false)
            {
                return false;
            }
            query= $"SELECT IIF( EXISTS ( select NOMBRE_CATEGORIA from CATEGORIA where NOMBRE_CATEGORIA='{name}'), 1 , 0 );";
            if (helper.Check_if_exist(query) == 1)
            {
                MessageBox.Show("La categoria ya existe", "Error");
                return false;
            }
            Categoria categoria = new Categoria(id,name);
            categoria.Actualizar();
            return true;
        }
        public void Delete_Category(string id)
        {
            string query = "";
            int ID = 0;
            Validation validation = new Validation();
            Helper Helper = new Helper();
            Dictionary<string, string> dic = new Dictionary<string, string>() { { "Id", id } };
            if (validation.Validation_strings(dic, "categoria") == false)
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
            query = $"SELECT IIF( EXISTS ( SELECT 1 FROM CATEGORIA where ID_CATEGORIA={ID}), 1 , 0 );";
            if (Helper.Check_if_exist(query) == 0)
            {
                MessageBox.Show("No se a encontrado la categoria");
                return;
            }
            Categoria categoria = new Categoria(ID);
            categoria.Eliminar();
        }

    }
}
