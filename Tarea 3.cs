using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Runtime.CompilerServices;

namespace Registrador.Clases
{
    public class ClsCliente
    {
        public int id { get; set; }
        public string Consultar { get; set; }
        public string Borrar { get; set; }
        public string Modificar { get; set; }
        private static int tipoOperacion =0;

       public static List<ClsCliente> clientes = new List<ClsCliente>();    
        public ClsCliente(int id, string Consultar, string Borrar, string Modificar)
        {
            this.id = id;
            this.Consultar = Consultar;
            this.Borrar = Borrar;
            this.Modificar = Modificar;
        }
        public ClsCliente()
        {

        }

        public static int AgregarClientes( string Consultar, string Borrar, string Modificar)
        {
            int retorno = 0;
            tipoOperacion = 1;
            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = Clases.DBconn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("Sp_GestionarCliente", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@Operacion", tipoOperacion));
                    cmd.Parameters.Add(new SqlParameter("@ID", 0));
                    cmd.Parameters.Add(new SqlParameter("@Consultar", Consultar));
                    cmd.Parameters.Add(new SqlParameter("@Borrar", Borrar));
                    cmd.Parameters.Add(new SqlParameter("@Modificar", Modificar));

                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                retorno = -1;
            }
            finally
            {
                Conn.Close();
            }

            return retorno;
        }


        public static int BorrarClientes(string codigo)
        {
            int retorno = 0;
            tipoOperacion = 2;
            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = Clases.DBconn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("Sp_GestionarCliente", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@Operacion", tipoOperacion));
                    cmd.Parameters.Add(new SqlParameter("@ID", codigo));


                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                retorno = -1;
            }
            finally
            {
                Conn.Close();
            }

            return retorno;
        }


        public static List<ClsCliente> Consultar()
        {
            int retorno = 0;
            tipoOperacion = 4;
            SqlConnection Conn = new SqlConnection();

            try
            {

                using (Conn =Clases.DBconn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("Sp_ConsultaCliente ", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@Operacion", tipoOperacion));
                    retorno = cmd.ExecuteNonQuery();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ClsCliente cliente = new ClsCliente();
                            cliente.id = reader.GetInt32(0);
                            cliente.Consultar = reader.GetString(1);
                            cliente.Borrar = reader.GetString(2);
                            cliente.Modificar = reader.GetString(3);
                            clientes.Add(cliente);
                        }

                    }
                }
            }

             public static List<ClsCliente> Modificar()
            {
                int retorno = 0;
                tipoOperacion = 2;
                SqlConnection Conn = new SqlConnection();

                try
                {

                    using (Conn = Clases.DBconn.obtenerConexion())
                    {
                        SqlCommand cmd = new SqlCommand("Sp_ModificarCliente ", Conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.Add(new SqlParameter("@Operacion", tipoOperacion));
                        retorno = cmd.ExecuteNonQuery();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClsCliente cliente = new ClsCliente();
                                cliente.id = reader.GetInt32(0);
                                cliente.Consultar = reader.GetString(1);
                                cliente.Borrar = reader.GetString(2);
                                cliente.Modificar = reader.GetString(3);
                                clientes.Add(cliente);
                            }

                        }
                    }
                }



                catch (System.Data.SqlClient.SqlException ex)
            {
                return clientes;
            }
            finally
            {
                Conn.Close();
                Conn.Dispose();
            }

            return clientes;
        }


    }
}