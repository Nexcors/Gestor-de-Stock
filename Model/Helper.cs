using GestordeStock.Model.DataBase;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestordeStock.Model
{
    internal class Helper
    {
        public bool existEntity(string query,string messageisexist,string messageerror)
        {
            try
            {
                Helper proveedor = new Helper();
                if (proveedor.Check_if_exist(query) == 1)
                {
                    MessageBox.Show(messageisexist, "Error");
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show(messageerror, "Error");
                return false;
            }
        }
        public int Check_if_exist(string query)//if is 0 ==false or 1 == true
        {
            Conexion_BD conexion = new Conexion_BD();
            SqlCommand sqlComando;
            try
            {
                sqlComando = new SqlCommand(query, conexion.Conn);
                //sqlComando.ExecuteNonQuery();
                conexion.Conn.Open();
                return Convert.ToInt32(sqlComando.ExecuteScalar());

            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
            return 0;
        }
    }
}
