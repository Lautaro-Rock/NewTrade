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
        public SqlConnection conexion;
        public SqlCommand conexionCommand;
        public SqlDataReader conexionDataReader;
        public SqlDataReader ConexionDataReader
        {
            get { return conexionDataReader; }
        }

        public AccesoDatos()
        {
            conexion = new SqlConnection("server=localhost; database=ComercioDB; Persist Security Info=True; User ID= sa; Password=Contra993!");
            conexionCommand = new SqlCommand();
        }
        public void SetearConsulta(string consulta)
        {
            conexionCommand.CommandType = System.Data.CommandType.Text;
            conexionCommand.CommandText = consulta;
        }
        public void EjecutarLectura()
        {
            conexionCommand.Connection = conexion;
            try
            {
                conexion.Open();
                conexionDataReader = conexionCommand.ExecuteReader();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void SetearParametro(string nombre, object valor)
        {
            conexionCommand.Parameters.AddWithValue(nombre, valor);
        }
        public void EjecutarAccion()
        {
            conexionCommand.Connection = conexion;
            try
            {
                conexion.Open();
                conexionCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void CerrarConexion()
        {
            if (conexionDataReader != null)
            {
                conexionDataReader.Close();
            }
            conexion.Close();
        }
    }
}
