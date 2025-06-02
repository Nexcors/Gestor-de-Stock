using GestordeStock.Model;
using GestordeStock.Model.DataBase;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace GestordeStock.Controller
{

    internal class MenuProductsController
    {
        #region Add Products
        public void ADD_Product(string name, string value, string lot)
        {
            //tomar productos°
            //validarlos
            //mandarlos al model -> gestion bdd
            //retorna aca
            //manda mensaje al menu
            Validation validation = new Validation();
            int values_numeric = 0;
            int lot_numeric = 0;
            //Validation black space
            name=name.Trim();
            value=value.Trim();
            lot = lot.Trim();
            //Create diccionary for agrup elements
            Dictionary<string, string> dic = new Dictionary<string, string>() {
                {"Nombre",name },
                {"Valor",value },
                {"Cantidad",lot }
            };
            //Validation All
            if (validation.Validation_strings(dic, "producto") == false)
            {
                return;
            }
            if (validation.Validation_characters_especials(dic) == false)
            {
                return;
            }

            //validation number
            if (validation.Validation_numbers(dic, 1) == false)
            {
                return;
            }
            //pass the string a int
            values_numeric = validation.transformint(value);
            lot_numeric = validation.transformint(lot);
            //check the values
            if (Validation_values(values_numeric, lot_numeric) == false)
            {
                return;
            }

            //create builder for producto
            Producto producto = new Producto(name, lot_numeric, values_numeric);
            //producto.Agregar();
        }
        public bool Validation_values(int value, int lot)//check the values
        {
            //TODO que no ingrese los numeores con =>0.0 /0,0  / redondear numeros

            try
            {
                if (value == 0 || value < 0 || lot == 0 || value < 0)
                {
                    MessageBox.Show("ingrese numeros mayores a 0", "Error");
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion
        public static void Delete_providers()
        {
            
        }
    }
}
