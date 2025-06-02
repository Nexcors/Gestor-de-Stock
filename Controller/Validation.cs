using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Xml.Linq;

namespace GestordeStock.Controller
{
    internal class Validation
    {
        #region Validation parameters
        public bool Validation_strings(Dictionary<string, string> dic, string entidad)
        {

            //pick the dictionary and check the null of string's (he gont to agrup the null's whit the string.join
            string parameters = string.Join(", ", dic
                .Where(v => string.IsNullOrEmpty(v.Value))
                .Select(k => k.Key));
            string mensaje = "";

            if (!string.IsNullOrEmpty(parameters))
            {
                mensaje = $"Ingrese {parameters} del "+entidad;
                MessageBox.Show(mensaje);
                return false;
            }
            return true;
        }
        public bool Validation_characters_especials(Dictionary<string, string> dic, int operation = 0)
        {
            string mensaje = "";
            Regex regex = new Regex("[^A-Za-z0-9]", RegexOptions.IgnoreCase);
            //check if need cut diccionary
            dic = CutDiccionary(dic, operation);
            string parameters = string.Join(", ", dic
               .Where(v => regex.IsMatch(v.Value))
               .Select(k => k.Key));
            if (!string.IsNullOrEmpty(parameters))
            {
                mensaje = $"No ingresar caracteres especiales en el campo: {parameters}";
                MessageBox.Show(mensaje, "Error");
                return false;
            }
            return true;
        }
        public static Dictionary<string, string> CutDiccionary(Dictionary<string, string> dic, int operation = 0)
        {
            if (operation == 3)//remove all expet numberphone.
            {
                //find the key=> telefone and save the value in variable value
                if (dic.TryGetValue("Telefono", out string value))
                {
                    dic=new Dictionary<string, string>() { { "Telefono",value } };
                }
                return dic;
            }
            if (operation == 2)//remove the last 1 digits and rut 
            {
                string primeraClave = dic.Keys.First();
                dic.Remove(primeraClave);
                Dictionary<string,string> diccionarioFiltrado = dic.Take(dic.Count - 1).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                return diccionarioFiltrado;
            }
            if (operation == 1)//remove the first element from dictionary in product
            {
                string primeraClave = dic.Keys.First();
                dic.Remove(primeraClave);
                return dic;
            }
            return dic;
        }
        public bool Validation_numbers(Dictionary<string, string> dic, int operation=0)//validation
        {
            string mensaje = "";
            dic = CutDiccionary(dic, operation);
            try
            {
                string parameters = string.Join(", ", dic
                    .Where(v => !int.TryParse(v.Value, out _))
                    .Select(k => k.Key));

                if (!string.IsNullOrEmpty(parameters))
                {
                    mensaje = $"Ingrese numero valido en el campo {parameters}";
                    MessageBox.Show(mensaje, "Error");
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int transformint(string number)
        {
            try
            {
                int val = 0;
                //tranform
                val = int.Parse(number);
                return val;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public bool countnumberphone(string phonnumber, int length, string message)
        {
            if (phonnumber.Length != length)
            {
                MessageBox.Show(message);
                return false;
            }
            return true;
        }
        #endregion
        /// <summary>
        /// In base to parameter id wen having form datagrid we can validate if empty and
        /// if null show message whit the name of "instance" from get.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="instancia"></param>
        /// <returns></returns>
        public bool Check_delete_Datagrid(string ID,string instancia)
        {
            if (!string.IsNullOrEmpty(ID))
            {
                MessageBox.Show($"No se pudo eliminar el objto de : {instancia}");
                return false;
            }
            return true;
        }
    }
}
