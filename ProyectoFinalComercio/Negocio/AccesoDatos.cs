using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        private SqlTransaction transaccion;

        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public AccesoDatos()
        {
            conexion = new SqlConnection("server=localhost; database=ComercioDB; Persist Security Info=True; User ID= sa; Password=Contra993!");

            comando = new SqlCommand();
        }

        public void SetearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
            comando.Parameters.Clear();
        }

        public void EjecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public object EjecutarScalar()
        {
            comando.Connection = conexion;

            if (transaccion != null)
                comando.Transaction = transaccion;

            bool abrirConexion = conexion.State != System.Data.ConnectionState.Open;

            try
            {
                if (abrirConexion)
                    conexion.Open();

                return comando.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar scalar: " + ex.Message, ex);
            }
            finally
            {
                if (abrirConexion)
                    conexion.Close();
            }

        }

        public void AbrirConexion()
        {
            if (conexion.State != System.Data.ConnectionState.Open)
                conexion.Open();
        }

        public void ComenzarTransaccion()
        {
            transaccion = conexion.BeginTransaction();
        }

        public void ConfirmarTransaccion()
        {
            if (transaccion != null)
                transaccion.Commit();
        }

        public void RollbackTransaccion()
        {
            if (transaccion != null)
                transaccion.Rollback();
        }


        public void SetearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        public void EjecutarAccion()
        {
            comando.Connection = conexion;

            if (transaccion != null)
                comando.Transaction = transaccion;

            bool abrirConexion = conexion.State != System.Data.ConnectionState.Open;

            try
            {
                if (abrirConexion)
                    conexion.Open();

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar acción: " + ex.Message, ex);
            }
            finally
            {
                if (abrirConexion)
                    conexion.Close();
            }

        }


        public void CerrarConexion()
        {
            if (lector != null)
                lector.Close();
                conexion.Close();
        }
    }
}
