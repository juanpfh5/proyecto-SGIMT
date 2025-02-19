﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SGIMTProyecto {
    public class D_OrdenCobroDiversos {
        public List<string[]> OrdenCD(string placa) {
            MySqlDataReader Resultado;
            List<string[]> listaDatos = new List<string[]>();
            MySqlConnection SqlCon = new MySqlConnection();

            try {
                SqlCon = Conexion.getInstancia().CrearConexion();
                string sql_tarea = "SELECT nombre_co, noExterior_co, CONCAT(domicilio_co, ' No. ', noExterior_co, ', ', municipio_co, ', ', estado_co) AS domicilio_co, noInterior_co, rfc_co, cp_co, colonia_co, estado_co, municipio_co FROM unidad_un INNER JOIN  concesionario_co ON unidad_un.id_co = concesionario_co.id_co WHERE placa_un = @Placa AND baja_un IS NULL";

                MySqlCommand Comando = new MySqlCommand(sql_tarea, SqlCon);
                Comando.Parameters.AddWithValue("@Placa", placa);
                Comando.CommandTimeout = 60;
                SqlCon.Open();
                Resultado = Comando.ExecuteReader();

                while (Resultado.Read()) {
                    string[] datosFila = new string[Resultado.FieldCount];

                    for (int i = 0; i < Resultado.FieldCount; i++) {
                        datosFila[i] = Resultado[i].ToString();
                    }

                    listaDatos.Add(datosFila);
                }

                return listaDatos;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
        }

        public List<string> ListadoClaveConcepto(string busqueda) {
            MySqlDataReader listaClaveConcepto;
            List<string> listaBusqueda = new List<string>();
            MySqlConnection SqlCon = new MySqlConnection();

            try {
                SqlCon = Conexion.getInstancia().CrearConexion();
                string sql_tarea = $"SELECT CONCAT(clave_cl, ' | ', concepto_cl) AS clave_concepto_cl FROM claves_cl WHERE clave_cl LIKE '%{busqueda}%' OR concepto_cl LIKE '%{busqueda}%'";

                MySqlCommand Comando = new MySqlCommand(sql_tarea, SqlCon);
                Comando.CommandTimeout = 60;
                SqlCon.Open();
                listaClaveConcepto = Comando.ExecuteReader();

                while (listaClaveConcepto.Read()) {
                    listaBusqueda.Add(listaClaveConcepto["clave_concepto_cl"].ToString());
                }

                return listaBusqueda;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
        }

        public List<string> ListadoPersonal() {
            List<string> listaNombres = new List<string>();
            MySqlConnection SqlCon = new MySqlConnection();

            try {
                SqlCon = Conexion.getInstancia().CrearConexion();
                string sql_tarea = "SELECT nombre_pe FROM personal_pe";

                using (MySqlCommand Comando = new MySqlCommand(sql_tarea, SqlCon)) {
                    Comando.CommandTimeout = 60;
                    SqlCon.Open();

                    using (MySqlDataReader Resultado = Comando.ExecuteReader()) {
                        while (Resultado.Read()) {
                            string nombre = Resultado["nombre_pe"].ToString();
                            listaNombres.Add(nombre);
                        }
                    }
                }

                return listaNombres;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
        }

        public List<string> ListadoDirector() {
            List<string> listaNombres = new List<string>();
            MySqlConnection SqlCon = new MySqlConnection();

            try {
                SqlCon = Conexion.getInstancia().CrearConexion();
                string sql_tarea = "SELECT nombre_di FROM director_di";

                using (MySqlCommand Comando = new MySqlCommand(sql_tarea, SqlCon)) {
                    Comando.CommandTimeout = 60;
                    SqlCon.Open();

                    using (MySqlDataReader Resultado = Comando.ExecuteReader()) {
                        while (Resultado.Read()) {
                            string nombre = Resultado["nombre_di"].ToString();
                            listaNombres.Add(nombre);
                        }
                    }
                }

                return listaNombres;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
        }

        public List<string[]> DatosRestantes(string placa) {
            MySqlDataReader Resultado;
            List<string[]> listaDatos = new List<string[]>();
            MySqlConnection SqlCon = new MySqlConnection();

            try {
                SqlCon = Conexion.getInstancia().CrearConexion();
                string sql_tarea = "SELECT folioRevista_un, noSerie_un, noMotor_un, modelo_un, marca_un, claveVehicular_un, tipo_un, combustible_un, pasajeros_un, rfc_co, cilindros_un, ruta_un, observaciones_un FROM unidad_un INNER JOIN concesionario_co ON unidad_un.id_co = concesionario_co.id_co WHERE placa_un = @Placa;";

                MySqlCommand Comando = new MySqlCommand(sql_tarea, SqlCon);
                Comando.Parameters.AddWithValue("@Placa", placa);
                Comando.CommandTimeout = 60;
                SqlCon.Open();
                Resultado = Comando.ExecuteReader();

                while (Resultado.Read()) {
                    string[] datosFila = new string[Resultado.FieldCount];

                    for (int i = 0; i < Resultado.FieldCount; i++) {
                        datosFila[i] = Resultado[i].ToString();
                    }

                    listaDatos.Add(datosFila);
                }

                return listaDatos;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
        }

        public bool ExistenciaVehiculo(string placa) {
            MySqlConnection SqlCon = new MySqlConnection();

            try {
                SqlCon = Conexion.getInstancia().CrearConexion();
                string sql_tarea = "SELECT EXISTS(SELECT 1 FROM unidad_un WHERE placa_un = @Placa) as existeVehiculo";

                MySqlCommand Comando = new MySqlCommand(sql_tarea, SqlCon);
                Comando.Parameters.AddWithValue("@Placa", placa);
                Comando.CommandTimeout = 60;
                SqlCon.Open();

                int resultado = Convert.ToInt32(Comando.ExecuteScalar());

                return resultado == 1;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
        }

        public (decimal, int) ObtenerDescuento(string fecha) {
            MySqlConnection SqlCon = new MySqlConnection();
            decimal des = 0.00m;
            int id = 0;

            try {
                SqlCon = Conexion.getInstancia().CrearConexion();
                string sql_tarea = "SELECT COALESCE(subquery.procentaje_de, 0.00) AS PorcentajeDescuento, COALESCE(subquery.id_de, 0) AS ID_Descuento FROM ( SELECT id_de, procentaje_de FROM descuento_de WHERE @Fecha BETWEEN fechaInicio_de AND fechaFin_de ORDER BY id_de DESC LIMIT 1 ) AS subquery UNION SELECT 0 AS id_de, 0 AS procentaje_de LIMIT 1;";

                MySqlCommand Comando = new MySqlCommand(sql_tarea, SqlCon);
                Comando.Parameters.AddWithValue("@Fecha", fecha);

                Comando.CommandTimeout = 60;
                SqlCon.Open();

                using (var reader = Comando.ExecuteReader()) {
                    if (reader.Read()) {
                        des = Convert.ToDecimal(reader["PorcentajeDescuento"]);
                        id = Convert.ToInt32(reader["ID_Descuento"]);
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (SqlCon.State == ConnectionState.Open) {
                    SqlCon.Close();
                }
            }

            return (des, id);
        }

        public int ObtenerIDUMA() {
            MySqlConnection SqlCon = new MySqlConnection();
            int id = 0;

            try {
                SqlCon = Conexion.getInstancia().CrearConexion();
                string sql_tarea = "SELECT id_um FROM uma_um ORDER BY id_um DESC LIMIT 1";

                MySqlCommand Comando = new MySqlCommand(sql_tarea, SqlCon);

                Comando.CommandTimeout = 60;
                SqlCon.Open();

                using (var reader = Comando.ExecuteReader()) {
                    if (reader.Read()) {
                        id = Convert.ToInt32(reader["id_um"]);
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (SqlCon.State == ConnectionState.Open) {
                    SqlCon.Close();
                }
            }

            return id;
        }

        public int ObtenerIDMovimiento() {
            MySqlConnection SqlCon = new MySqlConnection();
            int id = 0;

            try {
                SqlCon = Conexion.getInstancia().CrearConexion();
                string sql_tarea = "SELECT id_mo FROM movimiento_mo ORDER BY id_mo DESC LIMIT 1";

                MySqlCommand Comando = new MySqlCommand(sql_tarea, SqlCon);

                Comando.CommandTimeout = 60;
                SqlCon.Open();

                using (var reader = Comando.ExecuteReader()) {
                    if (reader.Read()) {
                        id = Convert.ToInt32(reader["id_mo"]);
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (SqlCon.State == ConnectionState.Open) {
                    SqlCon.Close();
                }
            }

            return id;
        }

        public int ObtenerIDClave(string concepto) {
            MySqlConnection SqlCon = new MySqlConnection();
            int id = 0;

            try {
                SqlCon = Conexion.getInstancia().CrearConexion();
                string sql_tarea = "SELECT id_cl FROM claves_cl WHERE concepto_cl = @Concepto";

                MySqlCommand Comando = new MySqlCommand(sql_tarea, SqlCon);
                Comando.Parameters.AddWithValue("@Concepto", concepto);

                Comando.CommandTimeout = 60;
                SqlCon.Open();

                using (var reader = Comando.ExecuteReader()) {
                    if (reader.Read()) {
                        id = Convert.ToInt32(reader["id_cl"]);
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (SqlCon.State == ConnectionState.Open) {
                    SqlCon.Close();
                }
            }

            return id;
        }

        public void InsertarMovimiento(List<object> datosMovimiento) {
            MySqlConnection SqlCon = new MySqlConnection();

            try {
                int count = 0;
                string parametro;

                SqlCon = Conexion.getInstancia().CrearConexion();
                string sql_tarea = "INSERT INTO movimiento_mo(fechaSistema_mo, total_mo, folioCaja_mo, id_pe, id_di, placa_un, id_um, id_de) VALUES(NOW(), @Total, @FolioCaja, @Id_pe, @Id_di, @Placa, @Id_um, @Id_de);";

                MySqlCommand Comando = new MySqlCommand(sql_tarea, SqlCon);

                foreach (var elemento in datosMovimiento) {
                    parametro = "";
                    switch (count) {
                        case 0:
                            parametro = "@Total";
                            break;
                        case 1:
                            parametro = "@FolioCaja";
                            break;
                        case 2:
                            parametro = "@Id_pe";
                            break;
                        case 3:
                            parametro = "@Id_di";
                            break;
                        case 4:
                            parametro = "@Placa";
                            break;
                        case 5:
                            parametro = "@Id_um";
                            break;
                        case 6:
                            parametro = "@Id_de";
                            break;
                    }
                    Comando.Parameters.AddWithValue(parametro, elemento);
                    count++;
                }
                Comando.CommandTimeout = 60;
                SqlCon.Open();
                Comando.ExecuteReader();
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
        }

        public void InsertarClavesMovimiento(List<object> datosMovimiento) {
            MySqlConnection SqlCon = new MySqlConnection();

            try {
                int count = 0;
                string parametro;

                SqlCon = Conexion.getInstancia().CrearConexion();
                string sql_tarea = "INSERT INTO movimiento_clave_mc(id_mo, id_cl) VALUES(@Id_mo, @Id_cl);";

                MySqlCommand Comando = new MySqlCommand(sql_tarea, SqlCon);

                foreach (var elemento in datosMovimiento) {
                    parametro = "";
                    switch (count) {
                        case 0:
                            parametro = "@Id_mo";
                            break;
                        case 1:
                            parametro = "@Id_cl";
                            break;
                    }
                    Comando.Parameters.AddWithValue(parametro, elemento);
                    count++;
                }
                Comando.CommandTimeout = 60;
                SqlCon.Open();
                Comando.ExecuteReader();
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
        }

        public string ObtenerDirector() {
            MySqlConnection SqlCon = new MySqlConnection();
            string nombreDirector = "";

            try {
                SqlCon = Conexion.getInstancia().CrearConexion();
                string sql_tarea = "SELECT nombre_di FROM director_di ORDER BY id_di DESC LIMIT 1;";

                MySqlCommand Comando = new MySqlCommand(sql_tarea, SqlCon);

                Comando.CommandTimeout = 60;
                SqlCon.Open();

                using (var reader = Comando.ExecuteReader()) {
                    if (reader.Read()) {
                        nombreDirector = reader["nombre_di"].ToString();
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (SqlCon.State == ConnectionState.Open) {
                    SqlCon.Close();
                }
            }
            return nombreDirector;
        }
    }
}
