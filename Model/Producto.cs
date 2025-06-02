using GestordeStock.Controller;
using GestordeStock.Model.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GestordeStock.Model
{
    internal class Producto:BaseProductSupplier,IProductSupplier
    {
        private Conexion_BD conexion = new Conexion_BD();

        private int cantidad;
        private int valor;

        public int Cantidad { get => cantidad; set => cantidad = value; }
        public int Valor { get => valor; set => valor = value; }

        //builder for enlist
        public Producto(int id,string nombre,int cantidad, int valor):base(id,nombre)
        {
            Cantidad = cantidad;
            Valor = valor;
        }

        public Producto(string nombre,int cantidad, int valor) : base(nombre)
        {
            Cantidad = cantidad;
            Valor = valor;
        }

        public Producto()
        {
        }
        public void Actualizar()
        {
            throw new NotImplementedException();
        }
        public void Agregar()
        {
            string consulta = "INSERT INTO [dbo].[Producto] (NOMBRE_PRODUCTO,VALOR,CANTIDAD) VALUES (@NOMBRE,@VALOR,@CANTIDAD) ";
            SqlCommand sqlComando = new SqlCommand(consulta, conexion.Conn);//permite ejeectura con parametro
            conexion.Conn.Open();
            try
            {
                //agregar Parametros
                sqlComando.Parameters.AddWithValue("@NOMBRE", Nombre);//parametro nombre se toma del textBox
                sqlComando.Parameters.AddWithValue("@VALOR", Valor);
                sqlComando.Parameters.AddWithValue("@CANTIDAD", Cantidad);
                sqlComando.ExecuteNonQuery();
                conexion.Conn.Close();

                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conexion.Conn.Close();
            }
        }

        public void Eliminar()
        {
            throw new NotImplementedException();
        }

        public DataTable Listar()
        {
            throw new NotImplementedException();
        }
        public DataTable Listar_unico(string NOMBRE)
        {
            throw new NotImplementedException();
        }
    }
}
