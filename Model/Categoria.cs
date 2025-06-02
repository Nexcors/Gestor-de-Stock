using GestordeStock.Model.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GestordeStock.Model
{
    internal class Categoria : BaseProductSupplier, IProductSupplier
    {
        public Categoria(int id, string nombre) : base(id, nombre)
        {
        }
        public Categoria(string nombre) : base(nombre)
        {
        }
        public Categoria()
        {
        }

        public Categoria(int id) : base(id)
        {
        }

        public void Actualizar()
        {
            Conexion_BD conexion = new Conexion_BD();
            string consulta = "UPDATE [dbo].[CATEGORIA] SET NOMBRE_CATEGORIA=@NOMBRE_CATEGORIA where ID_CATEGORIA=@ID_CATEGORIA;";
            SqlCommand sqlComando = new SqlCommand(consulta, conexion.Conn);//permite ejeectura con parametro
            conexion.Conn.Open();
            try
            {
                //agregar Parametros
                sqlComando.Parameters.AddWithValue("@ID_CATEGORIA", Id);
                sqlComando.Parameters.AddWithValue("@NOMBRE_CATEGORIA", Nombre);//parametro nombre se toma del textBox
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
        public void Agregar()
        {
            Conexion_BD conexion = new Conexion_BD();
            string consulta = "INSERT INTO [dbo].[CATEGORIA] (NOMBRE_CATEGORIA) VALUES (@NOMBRE_CATEGORIA)";
            SqlCommand sqlComando = new SqlCommand(consulta, conexion.Conn);//permite ejeectura con parametro
            conexion.Conn.Open();
            try
            {
                //agregar Parametros
                sqlComando.Parameters.AddWithValue("@NOMBRE_CATEGORIA",Nombre);//parametro nombre se toma del textBox
                Console.WriteLine(sqlComando.Parameters);
                sqlComando.ExecuteNonQuery();
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
            string query = "delete from CATEGORIA where ID_CATEGORIA = @ID_CATEGORIA";
            SqlCommand sqlComando = new SqlCommand(query, conexion.Conn);//permite ejeectura con parametro
            conexion.Conn.Open();
            try
            {
                sqlComando.Parameters.AddWithValue("@ID_CATEGORIA", Id);
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
            string query = "select [ID_CATEGORIA],[NOMBRE_CATEGORIA] from CATEGORIA";
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
            string consulta = $"select ID_CATEGORIA,NOMBRE_CATEGORIA from CATEGORIA where NOMBRE_CATEGORIA like @BUSQUEDA";
            SqlCommand sqlComando = new SqlCommand(consulta, conexion.Conn);//permite ejeectura con parametro
            conexion.Conn.Open();
            try
            {
                //agregar Parametros
                sqlComando.Parameters.AddWithValue("@BUSQUEDA", "%" + BUSQUEDA + "%");//parametro nombre se toma del textBox
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
            string query = "select * from CATEGORIA";

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
        public int Check_category(string query)//if is 0 ==false or 1 == true
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
