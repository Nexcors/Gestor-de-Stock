using Gestor_de_Stock;
using GestordeStock.Controller;
using GestordeStock.View;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GestordeStock.Model.DataBase;
using System.Windows;
using System.Windows.Media;

namespace GestordeStock.Model
{
    internal class Proveedor:BaseProductSupplier,IProductSupplier
    {
        string rut;
        private string apellido;
        private int telefono;
        private string mail;
        private string descipcion;

        public string Rut { get => rut; set => rut = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public int Telefono { get => telefono; set => telefono = value; }
        public string Mail { get => mail; set => mail = value; }
        public string Descipcion { get => descipcion; set => descipcion = value; }

        public Proveedor(int id,string rut ,string nombre, string apellido, int telefono, string mail):base(id,nombre)
        {
            Rut = rut;
            Apellido = apellido;
            Telefono = telefono;
            Mail = mail;
        }
        public Proveedor(int id, string rut, string nombre, string apellido, int telefono, string mail,string descipcion) : base(id, nombre)
        {
            Rut = rut;
            Apellido = apellido;
            Telefono = telefono;
            Mail = mail;
            Descipcion = descipcion;
        }
        public Proveedor(string rut, string nombre, string apellido, int telefono, string mail, string descipcion) : base(nombre)
        {
            Rut = rut;
            Apellido = apellido;
            Telefono = telefono;
            Mail = mail;
            Descipcion = descipcion;
        }

        public Proveedor(int id) : base(id)
        {
        }
        public Proveedor(string nombre) : base(nombre)
        {
        }
        public Proveedor()
        {
        }
        public void Agregar()
        {
            Conexion_BD conexion = new Conexion_BD();
            string consulta = "INSERT INTO [dbo].[PROVEEDOR] (NOMBRE_PROVEEDOR,APELLIDO,RUT,TELEFONO,MAIL,DESCRIPCION) VALUES (@NOMBRE_PROVEEDOR,@APELLIDO,@RUT,@TELEFONO,@MAIL,@DESCRIPCION) ";
            SqlCommand sqlComando = new SqlCommand(consulta, conexion.Conn);//permite ejeectura con parametro
            conexion.Conn.Open();
            try
            {
                //agregar Parametros
                sqlComando.Parameters.AddWithValue("@NOMBRE_PROVEEDOR", Nombre);//parametro nombre se toma del textBox
                sqlComando.Parameters.AddWithValue("@APELLIDO", Apellido);
                sqlComando.Parameters.AddWithValue("@RUT", Rut);
                sqlComando.Parameters.AddWithValue("@TELEFONO", Telefono);
                sqlComando.Parameters.AddWithValue("@MAIL", Mail);
                sqlComando.Parameters.AddWithValue("@DESCRIPCION", Descipcion);
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
            Conexion_BD conexion = new Conexion_BD();
            //delete from PROVEEDOR where ID_PROVEEDOR = 
            string query = "delete from PROVEEDOR where ID_PROVEEDOR = @ID_PROVEEDOR";
            SqlCommand sqlComando = new SqlCommand(query, conexion.Conn);//permite ejeectura con parametro
            conexion.Conn.Open();
            try
            {
                sqlComando.Parameters.AddWithValue("@ID_PROVEEDOR", Id);
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

        public void Actualizar()
        {
            Conexion_BD conexion = new Conexion_BD();
            string consulta = "UPDATE [dbo].[PROVEEDOR] SET NOMBRE_PROVEEDOR=@NOMBRE_PROVEEDOR,APELLIDO=@APELLIDO,RUT=@RUT,TELEFONO=@TELEFONO,MAIL=@MAIL,DESCRIPCION=@DESCRIPCION where ID_PROVEEDOR=@ID_PROVEEDOR;";
            SqlCommand sqlComando = new SqlCommand(consulta, conexion.Conn);//permite ejeectura con parametro
            conexion.Conn.Open();
            try
            {
                //agregar Parametros
                sqlComando.Parameters.AddWithValue("@ID_PROVEEDOR", Id);
                sqlComando.Parameters.AddWithValue("@NOMBRE_PROVEEDOR", Nombre);//parametro nombre se toma del textBox
                sqlComando.Parameters.AddWithValue("@APELLIDO", Apellido);
                sqlComando.Parameters.AddWithValue("@RUT", Rut);
                sqlComando.Parameters.AddWithValue("@TELEFONO", Telefono);
                sqlComando.Parameters.AddWithValue("@MAIL", Mail);
                sqlComando.Parameters.AddWithValue("@DESCRIPCION", Descipcion);
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

        public DataTable Listar()
        {
            Conexion_BD conexion = new Conexion_BD();
            SqlCommand sqlComando;
            string query = "select ID_PROVEEDOR as ID,RUT,CONCAT_WS(' ',[NOMBRE_PROVEEDOR],[APELLIDO]) AS NOMBRE_PROVEEDOR,[TELEFONO],[MAIL],[DESCRIPCION] from PROVEEDOR";
            conexion.Conn.Open();
            try
            {
                sqlComando = new SqlCommand(query, conexion.Conn);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlComando);
                using (adapter)
                {
                    DataTable pedidostabla = new DataTable();
                    
                    //adapter.Fill(pedidostabla);
                    adapter.Fill(pedidostabla);
                    return pedidostabla;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
            finally
            {
                conexion.Conn.Close();
            }
        }

        public DataTable Listar_unico(string BUSQUEDA)
        {
            Conexion_BD conexion = new Conexion_BD();
            string consulta = $"select ID_PROVEEDOR as ID,RUT,CONCAT_WS(' ',[NOMBRE_PROVEEDOR],[APELLIDO]) AS NOMBRE_PROVEEDOR,[TELEFONO],[MAIL],[DESCRIPCION] from PROVEEDOR where CONCAT_WS(' ',[NOMBRE_PROVEEDOR],[APELLIDO]) like @BUSQUEDA";
            SqlCommand sqlComando = new SqlCommand(consulta, conexion.Conn);//permite ejeectura con parametro
            conexion.Conn.Open();
            try
            {
                //agregar Parametros
                sqlComando.Parameters.AddWithValue("@BUSQUEDA", "%"+BUSQUEDA+"%");//parametro nombre se toma del textBox
                sqlComando.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(sqlComando);
                using (adapter)
                {
                    DataTable pedidostabla = new DataTable();

                    //adapter.Fill(pedidostabla);
                    adapter.Fill(pedidostabla);
                    return pedidostabla;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
            finally
            {
                conexion.Conn.Close();
            }
        }
        public DataTable Listar_Combobox()
        {
            Conexion_BD conexion = new Conexion_BD();
            SqlCommand sqlComando;
            string query = "select ID_PROVEEDOR,NOMBRE_PROVEEDOR from PROVEEDOR";

            try
            {
                sqlComando = new SqlCommand(query, conexion.Conn);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlComando);
                using (adapter)
                {
                    DataTable pedidostabla = new DataTable();
                    adapter.Fill(pedidostabla);

                    return pedidostabla;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }  
        }
        public int Check_ID_Provider(int RUT)
        {
            Conexion_BD conexion = new Conexion_BD();
            string query = "select ID_PROVEEDOR from PROVEEDOR where RUT=@RUT";
            SqlCommand sqlComando=new SqlCommand(query, conexion.Conn);

            try
            {
                sqlComando.Parameters.AddWithValue("@RUT", Rut);
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
