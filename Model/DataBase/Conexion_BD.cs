using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestordeStock.Model.DataBase
{
    internal class Conexion_BD
    {//Conexion base de datos gestion Pedidos
        private string miConexion;
        protected const string Datosconexion= "GestordeStock.Properties.Settings.InventarioConnectionString";//datos de conexion
        private SqlConnection conn;

        private string MiConexion { get => miConexion; set => miConexion = value; }
        public SqlConnection Conn { get => conn; set => conn = value; }
        public Conexion_BD()//contructor de conexion de bdd
        {
            // Inicializa la cadena de conexión
            MiConexion = ConfigurationManager.ConnectionStrings[Datosconexion].ConnectionString;

            // Inicializa la conexión SQL
            Conn = new SqlConnection(MiConexion);
        }
    }
}
