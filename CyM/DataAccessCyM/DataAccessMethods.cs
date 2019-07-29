using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Remoting.Messaging;
using System.Text;
using DataAccessCyM.Dtos;
using DataAccessCyM.Entidades;
using Oracle.ManagedDataAccess.Client;


namespace DataAccessCyM
{
    public class DataAccessMethods : DataAccessCyM.daBase
    {
        public DataAccessMethods()
            : base()
        {
        }
         
        public IList<Departamento> GetDepartamentos(string idProvincia)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexion());

            var lista = new List<Departamento>();


            try
            {
                OracleCommand com = new OracleCommand("CYM.PCK_CYM_CONSULTA.pr_getDepartamentos", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdProvincia", OracleDbType.Varchar2);

                if (idProvincia != "")
                    com.Parameters["pIdProvincia"].Value = idProvincia;
                else
                    com.Parameters["pIdProvincia"].Value = DBNull.Value;



                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                var resultado = com.ExecuteReader();

                while (resultado.Read())
                {
                    var obj = new Departamento
                    {
                        IdDepartamento = resultado["ID_DEPARTAMENTO"].ToString(),
                        NombreDepartamento = resultado["N_DEPARTAMENTO"].ToString(),
                        IdProvincia = resultado["ID_PROVINCIA"].ToString()
                    };

                    lista.Add(obj);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return lista;
            return lista;
        }
        public IList<Localidad> GetLocalidades(string idDepartamento, string idProvincia)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexion());

            var lista = new List<Localidad>();


            try
            {
                OracleCommand com = new OracleCommand("CYM.PCK_CYM_CONSULTA.pr_getLocalidades", conn);
                com.CommandType = CommandType.StoredProcedure;


                com.Parameters.Add("pIdDepartamento", OracleDbType.Int32);

                if (idDepartamento != "")
                    com.Parameters["pIdDepartamento"].Value = int.Parse(idDepartamento);
                else
                    com.Parameters["pIdDepartamento"].Value = DBNull.Value;


                com.Parameters.Add("pIdProvincia", OracleDbType.Varchar2);

                if (idProvincia != "")
                    com.Parameters["pIdProvincia"].Value = idProvincia;
                else
                    com.Parameters["pIdProvincia"].Value = DBNull.Value;



                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                var resultado = com.ExecuteReader();

                while (resultado.Read())
                {
                    var obj = new Localidad
                    {
                        IdLocalidad = resultado["ID_LOCALIDAD"].ToString(),
                        NombreLocalidad = resultado["N_LOCALIDAD"].ToString(),
                        IdDepartamento = resultado["ID_DEPARTAMENTO"].ToString(),
                        IdProvincia = resultado["ID_PROVINCIA"].ToString()
                    };

                    lista.Add(obj);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return lista;

        }

        public IList<Barrio> GetBarrios(string idLocalidad)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            var lista = new List<Barrio>();


            try
            {
                OracleCommand com = new OracleCommand("CYM.PCK_CYM_CONSULTA.pr_getBarrios", conn);
                com.CommandType = CommandType.StoredProcedure;


                com.Parameters.Add("pIdLocalidad", OracleDbType.Int32);

                if (idLocalidad != "")
                    com.Parameters["pIdLocalidad"].Value = int.Parse(idLocalidad);
                else
                    com.Parameters["pIdLocalidad"].Value = DBNull.Value;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                var resultado = com.ExecuteReader();

                while (resultado.Read())
                {
                    var obj = new Barrio
                    {
                        IdBarrio = resultado["ID_BARRIO"].ToString(),
                        NombreBarrio = resultado["N_BARRIO"].ToString(),
                        IdLocalidad = resultado["ID_LOCALIDAD"].ToString()
                    };

                    lista.Add(obj);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return lista;
        }


        public IList<Provincia> GetProvincias()
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexion());

            var lista = new List<Provincia>();

            try
            {

                OracleCommand com = new OracleCommand("CYM.PCK_CYM_CONSULTA.pr_getProvincias", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                var resultado = com.ExecuteReader();

                while (resultado.Read())
                {
                    var obj = new Provincia
                    {
                        IdProvincia = resultado["ID_PROVINCIA"].ToString(),
                        NombreProvincia = resultado["N_PROVINCIA"].ToString(),
                    };

                    lista.Add(obj);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return lista;

        }

        public IList<Calle> GetCalles(string idProvincia, string idDepartamento, string idLocalidad)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexion());

            var lista = new List<Calle>();

            try
            {
                OracleCommand com = new OracleCommand("CYM.PCK_CYM_CONSULTA.pr_getCalles", conn);
                com.CommandType = CommandType.StoredProcedure;



                com.Parameters.Add("pIdLocalidad", OracleDbType.Int32);

                if (idLocalidad != "")
                    com.Parameters["pIdLocalidad"].Value = int.Parse(idLocalidad);
                else
                    com.Parameters["pIdLocalidad"].Value = DBNull.Value;


                com.Parameters.Add("pIdDepartamento", OracleDbType.Int32);

                if (idDepartamento != "")
                    com.Parameters["pIdDepartamento"].Value = int.Parse(idDepartamento);
                else
                    com.Parameters["pIdDepartamento"].Value = DBNull.Value;


                com.Parameters.Add("pIdProvincia", OracleDbType.Varchar2);

                if (idProvincia != "")
                    com.Parameters["pIdProvincia"].Value = idProvincia;
                else
                    com.Parameters["pIdProvincia"].Value = DBNull.Value;



                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                var resultado = com.ExecuteReader();

                while (resultado.Read())
                {
                    var obj = new Calle
                    {
                        IdCalle = resultado["ID_CALLE"].ToString(),
                        NombreCalle = resultado["N_CALLE"].ToString(),
                    };

                    lista.Add(obj);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return lista;
        }

        public LocalidadDto GetLocalidadDto(string idLocalidad)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexion());

            var lista = new List<LocalidadDto>();


            try
            {
                OracleCommand com = new OracleCommand("CYM.PCK_CYM_CONSULTA.pr_getLocalidadCompleta", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdLocalidad", OracleDbType.Int32);

                if (idLocalidad != "")
                    com.Parameters["pIdLocalidad"].Value = idLocalidad;
                else
                    com.Parameters["pIdLocalidad"].Value = DBNull.Value;



                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                var resultado = com.ExecuteReader();

                while (resultado.Read())
                {
                    var obj = new LocalidadDto
                    {
                        IdLocalidad = resultado["ID_LOCALIDAD"].ToString(),
                        NombreLocalidad = resultado["N_LOCALIDAD"].ToString(),
                        IdDepartamento = resultado["ID_DEPARTAMENTO"].ToString(),
                        NombreDepartamento = resultado["N_DEPARTAMENTO"].ToString(),
                        IdProvincia = resultado["ID_PROVINCIA"].ToString(),
                        NombreProvincia = resultado["N_PROVINCIA"].ToString()
                    };

                    lista.Add(obj);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return lista[0];
        }

        public DataTable DaGetPersonaUnica(String pDNI, String pIdSexo)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            try
            {

                OracleCommand com = new OracleCommand("CYM.PCK_CYM_CONSULTA.pr_getPersonaUnica", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pDNI", OracleDbType.Varchar2, 20);

                if (pDNI != "")
                    com.Parameters["pDNI"].Value = pDNI;
                else
                    com.Parameters["pDNI"].Value = DBNull.Value;

                com.Parameters.Add("pIdSexo", OracleDbType.Varchar2, 2);

                if (pIdSexo != "")
                    com.Parameters["pIdSexo"].Value = pIdSexo;
                else
                    com.Parameters["pIdSexo"].Value = DBNull.Value;


                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                conn.Close();
                if (ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        public Comunicacion GetContacto(string idEntidad,string tablaOrigen)
        {
            var contacto = new Comunicacion();
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            try
            {

                OracleCommand com = new OracleCommand("CYM.PCK_CYM_CONSULTA.pr_getcontacto", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdEntidad", OracleDbType.Varchar2, 20);
                com.Parameters["pIdEntidad"].Value = idEntidad;

                com.Parameters.Add("pTablaOrigen", OracleDbType.Varchar2, 50);
                com.Parameters["pTablaOrigen"].Value = tablaOrigen;

                
                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                OracleDataReader dr = com.ExecuteReader();

                while (dr.Read())
                {
                    switch (dr["ID_TIPO_COMUNICACION"].ToString())
                    {
                        case "01": //TELEFONO FIJO
                            contacto.CodAreaTelFijo = dr["COD_AREA"].ToString();
                            contacto.NroTelfijo = dr["NRO_MAIL"].ToString();
                            break;
                        case "07": //CELULAR
                            contacto.CodAreaCel = dr["COD_AREA"].ToString();
                            contacto.NroCel = dr["NRO_MAIL"].ToString();
                            break;
                        case "11": //EMAIL
                            contacto.EMail = !string.IsNullOrEmpty(dr["NRO_MAIL"].ToString()) ? dr["NRO_MAIL"].ToString() : " - ";
                            break;
                        case "12": //PAGINA WEB
                            
                            break;
                        case "17": //FACEBOOK
                            
                            break;
                    }
                }

                return contacto;
            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Devuelve el nro de solicitud (int) una vez registrada.
        /// </summary>
        /// <param name="solicitud"></param>
        /// <returns></returns>
        public int RegistrarSolicitudCurso(SolicitudCurso solicitud,out ResultadoRule result)
        {
            result = new ResultadoRule();
            var nroSolicitud = 0;
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            try
            {

                OracleCommand com = new OracleCommand("CYM.PCK_CYM_INSERCIONES.pr_Insert_Solicitud_Curso", conn);
                com.CommandType = CommandType.StoredProcedure;

               
                com.Parameters.Add("pDescripcion", OracleDbType.Varchar2, 500);
                com.Parameters.Add("pCuilUsuarioCidi", OracleDbType.Varchar2, 11);
                com.Parameters.Add("pIdLocalidad", OracleDbType.Int32);
                com.Parameters.Add("pLinkArchivoAsistente", OracleDbType.Varchar2, 500);
                com.Parameters.Add("pCantEstimada", OracleDbType.Int32);
                

                if (string.IsNullOrEmpty(solicitud.NombreCooperativa))
                    com.Parameters["pDescripcion"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pDescripcion"].Value = solicitud.NombreCooperativa;
                }



                if (string.IsNullOrEmpty(solicitud.CuilUsuarioLogueado))
                    com.Parameters["pCuilUsuarioCidi"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pCuilUsuarioCidi"].Value = solicitud.CuilUsuarioLogueado;
                }


                com.Parameters["pIdLocalidad"].Value = solicitud.IdLocalidad;

                if (string.IsNullOrEmpty(solicitud.LinkArchivoAsistentes))
                    com.Parameters["pLinkArchivoAsistente"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pLinkArchivoAsistente"].Value = solicitud.LinkArchivoAsistentes;
                }

                if (string.IsNullOrEmpty(solicitud.cantAsistentes))
                    com.Parameters["pCantEstimada"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pCantEstimada"].Value = solicitud.cantAsistentes;
                }

                com.Parameters.Add("pNroSolicitud", OracleDbType.Int32);
                com.Parameters["pNroSolicitud"].Direction = ParameterDirection.Output;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 20);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                string pResultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                if (pResultado == "OK")
                {
                    result.OcurrioError = false;
                    result.MensajeExito = "Se registró con éxito la Solicitud.";
                }
                else
                {
                    result.OcurrioError = true;
                    result.MensajeError = pResultado;
                }

                nroSolicitud = Convert.ToInt32(com.Parameters["pNroSolicitud"].Value.ToString());
                /** El detalle de la solicitud se carga en la regla de negocio.*/
                return nroSolicitud;
            }
            catch (Exception ex)
            {
                result.OcurrioError = true;
                result.MensajeError = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return nroSolicitud;

        }

        public void RegistrarContacto(Comunicacion comunicacion, out ResultadoRule result)
        {
            result = new ResultadoRule(); 
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            try
            {
                OracleCommand com = new OracleCommand("CYM.PCK_CYM_INSERCIONES.pr_Insert_Comunicacion", conn);
                com.CommandType = CommandType.StoredProcedure;


                com.Parameters.Add("p_entidad", OracleDbType.Varchar2, 40);
                com.Parameters.Add("p_email", OracleDbType.Varchar2, 100);
                com.Parameters.Add("p_nroCel", OracleDbType.Varchar2, 20);
                com.Parameters.Add("p_CodAreaCel", OracleDbType.Varchar2, 10);
                com.Parameters.Add("p_nroTelFijo", OracleDbType.Varchar2, 20);
                com.Parameters.Add("p_CodAreaTelFijo", OracleDbType.Varchar2, 10);
                com.Parameters.Add("p_Origen_tabla", OracleDbType.Varchar2, 100);

                if (string.IsNullOrEmpty(comunicacion.IdEntidad))
                    com.Parameters["p_entidad"].Value = DBNull.Value;
                else
                {
                    com.Parameters["p_entidad"].Value = comunicacion.IdEntidad;
                }

                if (string.IsNullOrEmpty(comunicacion.EMail))
                    com.Parameters["p_email"].Value = DBNull.Value;
                else
                {
                    com.Parameters["p_email"].Value = comunicacion.EMail;
                }

                if (string.IsNullOrEmpty(comunicacion.NroCel))
                    com.Parameters["p_nroCel"].Value = DBNull.Value;
                else
                {
                    com.Parameters["p_nroCel"].Value = comunicacion.NroCel;
                }

                if (string.IsNullOrEmpty(comunicacion.CodAreaCel))
                    com.Parameters["p_CodAreaCel"].Value = DBNull.Value;
                else
                {
                    com.Parameters["p_CodAreaCel"].Value = comunicacion.CodAreaCel;
                }

                if (string.IsNullOrEmpty(comunicacion.NroTelfijo))
                    com.Parameters["p_nroTelFijo"].Value = DBNull.Value;
                else
                {
                    com.Parameters["p_nroTelFijo"].Value = comunicacion.NroTelfijo;
                }

                if (string.IsNullOrEmpty(comunicacion.CodAreaTelFijo))
                    com.Parameters["p_CodAreaTelFijo"].Value = DBNull.Value;
                else
                {
                    com.Parameters["p_CodAreaTelFijo"].Value = comunicacion.CodAreaTelFijo;
                }


                com.Parameters["p_Origen_tabla"].Value = comunicacion.Tabla_Origen;  /*"CYM.T_ENTIDADES"  Ó "CYM.T_DETALLE_SOLICITUDES"*/

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 20);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                string pResultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                if (pResultado == "OK")
                {
                    result.OcurrioError = false;
                    result.MensajeExito = "Se registró con éxito la Comunicación.";
                }
                else
                {
                    result.OcurrioError = true;
                    result.MensajeError = pResultado;
                }


            }
            catch (Exception ex)
            {
                result.OcurrioError = true;
                result.MensajeError = ex.Message;
            }
            finally
            {
                conn.Close();
            } 
        }
        

        /// <summary>
        /// Carga un domicilio dom_manager y devuelve el id_vinculo del nuevo domicilio
        /// </summary>
        /// <param name="idEntidad">Está compuesto por el CUIT + ID_SEDE.</param>
        /// <param name="idVin">parametro de salida que se usa para cargar el id_vinculo del domicilio generado</param>
        /// <returns></returns>
        public string CargarDomicilio(string idEntidad, string idProvincia, string idDepartamento, string nombreLocalidad, string idTipoCalle,
            string nombreTipoCalle, string idCalle, string nombreCalle, string idBarrio, string nombreBarrio, string idPrecinto, string altura,
            string piso, string dpto, string torre, string idLocalidad, string codPostal, string manzana, string lote, out int? idVin)
        {

            var sql = new StringBuilder();
            sql.Append("CYM.PCK_CYM_INSERCIONES.pr_Insert_Domicilio"); 

            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            conn.Open();
            try
            {
                var com = new OracleCommand(sql.ToString(), conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("p_id_aplicacion", OracleDbType.Int32, 11);//
                com.Parameters.Add("p_id_tipodom", OracleDbType.Int32, 8);
                com.Parameters.Add("p_id_entidad", OracleDbType.Varchar2, 11);
                com.Parameters.Add("p_id_provincia", OracleDbType.Varchar2, 11);
                com.Parameters.Add("p_id_departamento", OracleDbType.Int32, 11);
                com.Parameters.Add("p_id_localidad", OracleDbType.Int32, 11);
                com.Parameters.Add("p_localidad", OracleDbType.Varchar2, 30);
                com.Parameters.Add("p_id_tipocalle", OracleDbType.Varchar2, 11);
                com.Parameters.Add("p_tipo_calle", OracleDbType.Varchar2, 11);
                com.Parameters.Add("p_id_calle", OracleDbType.Int32, 11);
                com.Parameters.Add("p_calle", OracleDbType.Varchar2, 50);
                com.Parameters.Add("p_id_barrio", OracleDbType.Int32, 11);//
                com.Parameters.Add("p_barrio", OracleDbType.Varchar2, 50);//
                com.Parameters.Add("p_id_precinto", OracleDbType.Int32, 11);
                com.Parameters.Add("p_cpa", OracleDbType.Varchar2, 11);
                com.Parameters.Add("p_altura", OracleDbType.Int32, 11);
                com.Parameters.Add("p_piso", OracleDbType.Varchar2, 11);
                com.Parameters.Add("p_depto", OracleDbType.Varchar2, 11);
                com.Parameters.Add("p_torre", OracleDbType.Varchar2, 11);
                com.Parameters.Add("p_mzna", OracleDbType.Varchar2, 11);
                com.Parameters.Add("p_lote", OracleDbType.Varchar2, 11);

                com.Parameters["p_id_aplicacion"].Value = 129;  //id_app :> sifcos en BD de gobierno. el 152 es de CiDi.
                com.Parameters["p_id_tipodom"].Value = Convert.ToInt32(3);//real   
                com.Parameters["p_id_entidad"].Value = idEntidad; //CUIT + SEDE.
                com.Parameters["p_id_provincia"].Value = idProvincia;
                com.Parameters["p_id_departamento"].Value = Convert.ToInt32(idDepartamento);
                com.Parameters["p_id_localidad"].Value = Convert.ToInt32(idLocalidad);
                com.Parameters["p_localidad"].Value = nombreLocalidad;
                com.Parameters["p_id_tipocalle"].Value = idTipoCalle;
                com.Parameters["p_tipo_calle"].Value = "CALLE";

                if (!string.IsNullOrEmpty(idCalle))
                {
                    com.Parameters["p_id_calle"].Value = Convert.ToInt32(idCalle);
                }
                else
                {
                    com.Parameters["p_id_calle"].Value = DBNull.Value;
                }
                com.Parameters["p_calle"].Value = nombreCalle;

                if (!string.IsNullOrEmpty(idBarrio))
                {
                    com.Parameters["p_id_barrio"].Value = Convert.ToInt32(idBarrio);
                }
                else
                {
                    com.Parameters["p_id_barrio"].Value = DBNull.Value;
                }

                com.Parameters["p_barrio"].Value = nombreBarrio;
                com.Parameters["p_id_precinto"].Value = 0;
                com.Parameters["p_cpa"].Value = codPostal;
                com.Parameters["p_altura"].Value = Convert.ToInt32(altura);
                com.Parameters["p_piso"].Value = piso;
                com.Parameters["p_depto"].Value = dpto;
                com.Parameters["p_torre"].Value = torre;
                com.Parameters["p_mzna"].Value = manzana;
                com.Parameters["p_lote"].Value = lote;


                //parametros de salida
                com.Parameters.Add("o_id_vin", OracleDbType.Int32, 11);
                com.Parameters.Add("o_tipo_dom", OracleDbType.Int32, 11);
                com.Parameters.Add("o_mensaje", OracleDbType.Varchar2, 20);

                com.Parameters["o_id_vin"].Direction = ParameterDirection.Output;
                com.Parameters["o_tipo_dom"].Direction = ParameterDirection.Output;
                com.Parameters["o_mensaje"].Direction = ParameterDirection.Output;


                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                var value = com.Parameters["o_id_vin"].Value;
                var mensaje = com.Parameters["o_mensaje"].Value;
                idVin = value != null ? (int?)int.Parse(value.ToString()) : null;
                conn.Close();
                if (mensaje.ToString() == "OK")
                {
                    return mensaje.ToString();
                }
                else
                {
                    return "ERROR DE BASE DE DATOS: " + mensaje.ToString();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            idVin = null;
            return null;
        }

        public Domicilio GetDomicilioByIdVin(string pIdVin)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            try
            {


                OracleCommand com = new OracleCommand("CYM.PCK_CYM_CONSULTA.pr_get_Domicilio_by_idvin", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("pidvin", OracleDbType.Int64, 20);
                if (Int64.Parse(pIdVin) != 0)
                    com.Parameters["pidvin"].Value = Int64.Parse(pIdVin);
                else
                    com.Parameters["pidvin"].Value = DBNull.Value;
                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                OracleDataReader dr= com.ExecuteReader();


                while (dr.Read())
                {
                    var domicilio = new Domicilio
                    {
                        IdVin = dr["id_vin"].ToString(),
                        IdDepartamento = dr["id_departamento"].ToString(),
                        Departamento = dr["n_departamento"].ToString(),
                        IdLocalidad = dr["id_localidad"].ToString(),
                        Localidad = dr["n_localidad"].ToString(),
                        IdProvincia = dr["id_provincia"].ToString(),
                        Provincia = dr["n_provincia"].ToString(),
                        IdBarrio = dr["id_barrio"].ToString(),
                        Barrio = dr["n_barrio"].ToString(),
                        Calle = dr["n_calle"].ToString(),
                        Altura = dr["altura"].ToString(),
                        Piso = dr["piso"].ToString(),
                        Depto = dr["depto"].ToString(),
                        CodigoPostal = dr["cpa"].ToString()
                    };
                    return domicilio;

                }
                return null;

            }
            catch (Exception ex)
            {
                throw new daException(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void RegistrarDetalleSolicitudCurso(AsistenteCursoDto Asistente,string  NrosolicitudCurso,out ResultadoRule result)
        {
            result = new ResultadoRule();
            var nroDetalle = 0;
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            try
            {

                OracleCommand com = new OracleCommand("CYM.PCK_CYM_INSERCIONES.pr_Insert_Detalle_Solicitud", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pNroSolicitud", OracleDbType.Int64, 15);
                com.Parameters.Add("pDni", OracleDbType.Varchar2, 15);
                com.Parameters.Add("pIdSexo", OracleDbType.Varchar2, 2);
                com.Parameters.Add("pPaiCodPais", OracleDbType.Varchar2, 10);
                com.Parameters.Add("pIdNumero", OracleDbType.Varchar2, 2);

                com.Parameters.Add("pNroDetalle", OracleDbType.Int32);
                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 20);

                if (string.IsNullOrEmpty(NrosolicitudCurso))
                    com.Parameters["pNroSolicitud"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pNroSolicitud"].Value = NrosolicitudCurso;
                }
                
                if (string.IsNullOrEmpty(Asistente.Dni))
                    com.Parameters["pDni"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pDni"].Value = Asistente.Dni;
                }

                if (string.IsNullOrEmpty(Asistente.idSexo))
                    com.Parameters["pIdSexo"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pIdSexo"].Value = Asistente.idSexo;
                }

                if (string.IsNullOrEmpty(Asistente.PaiCodPais))
                    com.Parameters["pPaiCodPais"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pPaiCodPais"].Value = Asistente.PaiCodPais;
                }

                if (string.IsNullOrEmpty(Asistente.idNumero))
                    com.Parameters["pIdNumero"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pIdNumero"].Value = Asistente.idNumero;
                }

                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                com.Parameters["pNroDetalle"].Direction = ParameterDirection.Output;

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                string pResultado = com.Parameters["pResultado"].Value.ToString();
                if (pResultado == "OK")
                {
                    result.OcurrioError = false;
                    
                    
                }
                else
                {
                    result.OcurrioError = true;
                    result.MensajeError = pResultado;
                }
                


            }
            catch (Exception ex)
            {
                result.OcurrioError = true;
                result.MensajeError = ex.Message;
                
            }
            finally
            {
                conn.Close();
            }

            
        }

        public List<SolicitudCursoDto> getSolicitudesCurso(string nombreCoop, string fechaDesde, string fechaHasta)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            conn.Open();
            var lista = new List<SolicitudCursoDto>();
           

            try
            {
                OracleCommand com = new OracleCommand("CYM.PCK_CYM_CONSULTA.pr_getSolicitudCurso", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pNombreEntidad", OracleDbType.Varchar2, 50);
                if (nombreCoop != "")
                    com.Parameters["pNombreEntidad"].Value = nombreCoop;
                else
                    com.Parameters["pNombreEntidad"].Value = DBNull.Value;


                com.Parameters.Add("pFechaDesde", OracleDbType.Varchar2, 2);
                if (fechaDesde != "")
                    com.Parameters["pFechaDesde"].Value = fechaDesde;
                else
                    com.Parameters["pFechaDesde"].Value = DBNull.Value;

                
                com.Parameters.Add("pFechaHasta", OracleDbType.Varchar2, 2);
                if (fechaHasta != "")
                    com.Parameters["pFechaHasta"].Value = fechaHasta;
                else
                    com.Parameters["pFechaHasta"].Value = DBNull.Value;


                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                 
                var resultado = com.ExecuteReader();

                while (resultado.Read())
                {
                    var obj = new SolicitudCursoDto
                    {
                        NroSolicitudCurso = resultado["NRO_SOLICITUD_CURSO"].ToString(),
                        NombreCooperativa = resultado["descripcion"].ToString(),
                        NombreLocalidad = resultado["LOCALIDAD"].ToString(),
                        idLocalidad = resultado["id_localidad"].ToString(),
                        NombreDepartamento = resultado["DEPARTAMENTO"].ToString(),
                        FechaAlta = resultado["fec_solicitud"].ToString(),
                        Estado = string.IsNullOrEmpty(resultado["ID_CURSO"].ToString()) ? "SIN_CURSO" : "CON_CURSO"  ,
                        CuilUsuarioCidi = resultado["usuario_cidi"].ToString(),
                        CantidadEstimada = string.IsNullOrEmpty(resultado["cant_estimada"].ToString()) ? 0 : Convert.ToInt32(resultado["cant_estimada"].ToString()),
                        
                        asignado = string.IsNullOrEmpty(resultado["ID_CURSO"].ToString()) ? "N" : "S"
                    };

                    lista.Add(obj);
                }

                return lista;
            }
            catch (Exception ex)
            {
                return lista;
            }
            finally
            {
                conn.Close();
            }
            return lista;
        }

        public int RegistrarCurso(Curso curso, out ResultadoRule result)
        {
            result = new ResultadoRule();
            var idCurso = 0;
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            try
            {

                OracleCommand com = new OracleCommand("CYM.PCK_CYM_INSERCIONES.pr_Insert_Curso", conn);
                com.CommandType = CommandType.StoredProcedure;


                com.Parameters.Add("pNombreCurso", OracleDbType.Varchar2, 40);
                com.Parameters.Add("pFecCurso", OracleDbType.Date);
                com.Parameters.Add("pCantAsist", OracleDbType.Int32);
                com.Parameters.Add("pIdLocalidad", OracleDbType.Int32); 

                if (string.IsNullOrEmpty(curso.n_curso))
                    com.Parameters["pNombreCurso"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pNombreCurso"].Value = curso.n_curso;
                }



                if (curso.FechaDictado.HasValue == false)
                    com.Parameters["pFecCurso"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pFecCurso"].Value = curso.FechaDictado.Value;
                }


                com.Parameters["pCantAsist"].Value = curso.CantidadAsistentes;
                com.Parameters["pIdLocalidad"].Value = curso.Localidad.IdLocalidad;
                 



                com.Parameters.Add("pIdCurso", OracleDbType.Int32);
                com.Parameters["pIdCurso"].Direction = ParameterDirection.Output;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 20);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                
                conn.Open();

                var res = com.ExecuteNonQuery();

                string pResultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                if (pResultado == "OK")
                {
                    result.OcurrioError = false;
                    result.MensajeExito = "Se registró con éxito la Solicitud.";
                }
                else
                {
                    result.OcurrioError = true;
                    result.MensajeError = pResultado;
                }

                idCurso = Convert.ToInt32(com.Parameters["pIdCurso"].Value.ToString());

                //Una vez guardado el curso , actualizo las solicitudes con el curso generado
                    
                foreach (SolicitudCursoDto solicitudCursoDto in curso.Soliciudes)
                {
                    var r = DaActualizarSolicitudCurso(Convert.ToInt64(solicitudCursoDto.NroSolicitudCurso), idCurso);
                    if (r != "OK")
                    {
                       result.MensajeError = r;
                        result.OcurrioError = true;
                        return idCurso;
                    }
                }
                return idCurso;
            }
            catch (Exception ex)
            {
                result.OcurrioError = true;
                result.MensajeError = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return idCurso;
        }


        public int RegistrarAsamblea(int idEntidad, int idTipoAsamblea, Asamblea asamblea, out ResultadoRule result)
        {
            result = new ResultadoRule();
            var idAsamblea = 0;
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            try
            {

                OracleCommand com = new OracleCommand("CYM.PCK_CYM_INSERCIONES.pr_Insert_Asamblea", conn);
                com.CommandType = CommandType.StoredProcedure;


                com.Parameters.Add("pIdEntidad", OracleDbType.Int32);
                com.Parameters.Add("pIdTipoAsamblea", OracleDbType.Int32);
                com.Parameters.Add("pEjercicio", OracleDbType.Int32);
                com.Parameters.Add("pFecConvocatoria", OracleDbType.Date);
                com.Parameters.Add("pHora", OracleDbType.Varchar2,10);
                com.Parameters.Add("pFecPreAsamblea", OracleDbType.Date);
                com.Parameters.Add("pFecAsamblea", OracleDbType.Date);
                com.Parameters.Add("pFecPosAsamblea", OracleDbType.Date);
                com.Parameters.Add("pFecTerceraPresentacion", OracleDbType.Date);
                com.Parameters.Add("pVeedor", OracleDbType.Varchar2,1);
                com.Parameters.Add("pObservacion", OracleDbType.Varchar2,20); 

              
                com.Parameters["pIdEntidad"].Value =idEntidad;
                com.Parameters["pIdTipoAsamblea"].Value = idTipoAsamblea;

                 
                if (string.IsNullOrEmpty(asamblea.Ejercicio))
                    com.Parameters["pEjercicio"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pEjercicio"].Value = asamblea.Ejercicio;
                }

                if (!asamblea.FechaConvocatoria.HasValue)
                    com.Parameters["pFecConvocatoria"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pFecConvocatoria"].Value = asamblea.FechaConvocatoria.Value;
                }

                if (string.IsNullOrEmpty(asamblea.Hora))
                    com.Parameters["pHora"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pHora"].Value = asamblea.Hora;
                }

                if (!asamblea.FechaPreAsamblea.HasValue)
                    com.Parameters["pFecPreAsamblea"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pFecPreAsamblea"].Value = asamblea.FechaPreAsamblea.Value;
                }

                if (!asamblea.FechaAsamblea.HasValue)
                    com.Parameters["pFecAsamblea"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pFecAsamblea"].Value = asamblea.FechaAsamblea.Value;
                }


                if (!asamblea.FechaPosAsamblea.HasValue)
                    com.Parameters["pFecPosAsamblea"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pFecPosAsamblea"].Value = asamblea.FechaPosAsamblea.Value;
                }

                if (!asamblea.FechaTerceraPresentacion.HasValue)
                    com.Parameters["pFecTerceraPresentacion"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pFecTerceraPresentacion"].Value = asamblea.FechaTerceraPresentacion.Value;
                }

                
                com.Parameters["pVeedor"].Value = asamblea.SolicitoVeedor ? "S" : "N";
               
                if (string.IsNullOrEmpty(asamblea.Observacion))
                    com.Parameters["pObservacion"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pObservacion"].Value = asamblea.Observacion;
                }


                com.Parameters.Add("pIdAsamblea", OracleDbType.Int32);
                com.Parameters["pIdAsamblea"].Direction = ParameterDirection.Output;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 20);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                var res = com.ExecuteNonQuery();

                string pResultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                if (pResultado == "OK")
                {
                    result.OcurrioError = false;
                    result.MensajeExito = "Se registró con éxito la asamblea.";
                }
                else
                {
                    result.OcurrioError = true;
                    result.MensajeError = pResultado;
                }

                idAsamblea = Convert.ToInt32(com.Parameters["pIdAsamblea"].Value.ToString());

                 
                return idAsamblea;
            }
            catch (Exception ex)
            {
                result.OcurrioError = true;
                result.MensajeError = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return idAsamblea;
        }


        public void ActualizarCurso(Curso curso, out ResultadoRule result)
        {
            result = new ResultadoRule();
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            try
            {

                OracleCommand com = new OracleCommand("CYM.PCK_CYM_ACTUALIZACIONES.pr_update_Curso", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdCurso", OracleDbType.Int64, 15);
                com.Parameters.Add("pNombreCurso", OracleDbType.Varchar2, 40);
                com.Parameters.Add("pFecCurso", OracleDbType.Date);
                com.Parameters.Add("pCantAsist", OracleDbType.Int32);
                com.Parameters.Add("pIdLocalidad", OracleDbType.Int32);

                com.Parameters["pIdCurso"].Value = curso.IdCurso;

                if (string.IsNullOrEmpty(curso.n_curso))
                    com.Parameters["pNombreCurso"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pNombreCurso"].Value = curso.n_curso;
                }



                if (curso.FechaDictado.HasValue == false)
                    com.Parameters["pFecCurso"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pFecCurso"].Value = curso.FechaDictado.Value;
                }


                com.Parameters["pCantAsist"].Value = curso.CantidadAsistentes;
                com.Parameters["pIdLocalidad"].Value = curso.Localidad.IdLocalidad;

                
                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 20);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                string pResultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                if (pResultado == "OK")
                {
                    result.OcurrioError = false;
                    result.MensajeExito = "Se actualió con éxito la Solicitud.";
                }
                else
                {
                    result.OcurrioError = true;
                    result.MensajeError = pResultado;
                }

                //Una vez actualizado el curso , actualizo las solicitudes con el curso generado

                foreach (SolicitudCursoDto solicitudCursoDto in curso.Soliciudes)
                {
                    var r = DaActualizarSolicitudCurso(Convert.ToInt64(solicitudCursoDto.NroSolicitudCurso), curso.IdCurso);
                    if (r != "OK")
                    {
                        result.MensajeError = r;
                        result.OcurrioError = true; 
                    }
                }
            }
            catch (Exception ex)
            {
                result.OcurrioError = true;
                result.MensajeError = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            
        }

        public void ActualizarSolicitud(SolicitudCurso solicitud, out ResultadoRule result)
        {
            result = new ResultadoRule();
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            try
            {

                OracleCommand com = new OracleCommand("CYM.PCK_CYM_ACTUALIZACIONES.pr_update_solicitud_curso2", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pNro_solicitud", OracleDbType.Int32);
                com.Parameters.Add("pDescripcion", OracleDbType.Varchar2, 500);
                com.Parameters.Add("pUsuarioCidi", OracleDbType.Varchar2,11);
                com.Parameters.Add("pId_localidad_solicitante", OracleDbType.Int32);
                com.Parameters.Add("pLink_archivos_asistentes", OracleDbType.Varchar2, 500);
                com.Parameters.Add("pCant_Estimada", OracleDbType.Int32);

                if (string.IsNullOrEmpty(solicitud.NroSolicitudCurso))
                    com.Parameters["pNro_solicitud"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pNro_solicitud"].Value = solicitud.NroSolicitudCurso;
                }

                if (string.IsNullOrEmpty(solicitud.NombreCooperativa))
                    com.Parameters["pDescripcion"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pDescripcion"].Value = solicitud.NombreCooperativa;
                }

                if (string.IsNullOrEmpty(solicitud.CuilUsuarioLogueado))
                    com.Parameters["pUsuarioCidi"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pUsuarioCidi"].Value = solicitud.CuilUsuarioLogueado;
                }

                if (string.IsNullOrEmpty(solicitud.IdLocalidad))
                    com.Parameters["pId_localidad_solicitante"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pId_localidad_solicitante"].Value = solicitud.IdLocalidad;
                }

                if (string.IsNullOrEmpty(solicitud.LinkArchivoAsistentes))
                    com.Parameters["pLink_archivos_asistentes"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pLink_archivos_asistentes"].Value = solicitud.LinkArchivoAsistentes;
                }

               
                if (string.IsNullOrEmpty(solicitud.cantAsistentes))
                    com.Parameters["pCant_Estimada"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pCant_Estimada"].Value = solicitud.cantAsistentes;
                }

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 20);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                string pResultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                if (pResultado != "OK")
                {
                    result.OcurrioError = false;
                    
                }
                else
                {
                    result.OcurrioError = true;
                    result.MensajeError = pResultado;
                }

                //Una vez actualizada la solicitud , actualizo los asistentes

                foreach (AsistenteCursoDto Asistente in solicitud.SolicitantesCurso)
                {
                    RegistrarDetalleSolicitudCurso(Asistente,solicitud.NroSolicitudCurso,out result);
                    
                    if (result.OcurrioError)
                    {
                        result.MensajeError = result.MensajeError;
                        result.OcurrioError = true;
                        return;
                    }
                   
                    Asistente.Contacto.IdEntidad = Asistente.idSexo + Asistente.Dni + Asistente.PaiCodPais +
                                                   Asistente.idNumero;
                    RegistrarContacto(Asistente.Contacto,out result);
                    if (result.OcurrioError)
                    {
                        result.MensajeError = result.MensajeError;
                        result.OcurrioError = true;
                    }
                    else
                    {
                        result.MensajeExito = "OK";

                    }

                }
            }
            catch (Exception ex)
            {
                result.OcurrioError = true;
                result.MensajeError = ex.Message;
            }
            finally
            {
                conn.Close();
            }

        }

        //private string DaActualizarDetalleSolicitud(AsistenteCursoDto Asistente,string NroSolicitudCurso, out ResultadoRule result)
        //{
        //    result = new ResultadoRule();
        //    OracleConnection conn = new OracleConnection(CadenaDeConexion());
        //    try
        //    {

        //        OracleCommand com = new OracleCommand("CYM.PCK_CYM_ACTUALIZACIONES.pr_update_detalle_Solicitud", conn);
        //        com.CommandType = CommandType.StoredProcedure;

        //        com.Parameters.Add("pNroSolicitudCurso", OracleDbType.Int64, 15);
        //        com.Parameters.Add("pDNI", OracleDbType.Varchar2, 15);
        //        com.Parameters.Add("pPaiCodPais", OracleDbType.Varchar2,10);
        //        com.Parameters.Add("pIdSexo", OracleDbType.Varchar2,2);
        //        com.Parameters.Add("pIdNumero", OracleDbType.Varchar2,2);

        //        if (string.IsNullOrEmpty(NroSolicitudCurso))
        //            com.Parameters["pNroSolicitudCurso"].Value = DBNull.Value;
        //        else
        //        {
        //            com.Parameters["pNroSolicitudCurso"].Value = NroSolicitudCurso;
        //        }



        //        if (string.IsNullOrEmpty(Asistente.Dni))
        //            com.Parameters["pDNI"].Value = DBNull.Value;
        //        else
        //        {
        //            com.Parameters["pDNI"].Value = Asistente.Dni;
        //        }

        //        if (string.IsNullOrEmpty(Asistente.PaiCodPais))
        //            com.Parameters["pPaiCodPais"].Value = DBNull.Value;
        //        else
        //        {
        //            com.Parameters["pPaiCodPais"].Value = Asistente.PaiCodPais;
        //        }

        //        if (string.IsNullOrEmpty(Asistente.idSexo))
        //            com.Parameters["pIdSexo"].Value = DBNull.Value;
        //        else
        //        {
        //            com.Parameters["pIdSexo"].Value = Asistente.idSexo;
        //        }

        //        if (string.IsNullOrEmpty(Asistente.idNumero))
        //            com.Parameters["pIdNumero"].Value = DBNull.Value;
        //        else
        //        {
        //            com.Parameters["pIdNumero"].Value = Asistente.idNumero;
        //        }


        //        com.Parameters.Add("pResultado", OracleDbType.Varchar2, 20);
        //        com.Parameters["pResultado"].Direction = ParameterDirection.Output;

        //        OracleDataAdapter da = new OracleDataAdapter(com);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);

        //        string pResultado = com.Parameters["pResultado"].Value.ToString();
        //        conn.Close();
        //        if (pResultado == "OK")
        //        {
        //            result.OcurrioError = false;
        //            result.MensajeExito = pResultado;
        //        }
        //        else
        //        {
        //            result.OcurrioError = true;
        //            result.MensajeError = pResultado;
        //        }

                
        //    }
        //    catch (Exception ex)
        //    {
        //        result.OcurrioError = true;
        //        result.MensajeError = ex.Message;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    return result.ToString();
        //}

        public string DaActualizarSolicitudCurso(Int64 nro_solicitud,Int64 id_curso)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            try
            {

                OracleCommand com = new OracleCommand("CYM.PCK_CYM_ACTUALIZACIONES.pr_update_solicitud_curso", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pNro_solicitud", OracleDbType.Int64, 10);
                com.Parameters.Add("pId_curso", OracleDbType.Int64, 5);

                com.Parameters["pNro_solicitud"].Value = nro_solicitud;
                com.Parameters["pId_curso"].Value = id_curso;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                String Resultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                return Resultado;

            }
            catch (Exception ex)
            {
                return  ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }

        public ResultadoRule DaEliminarAsistentesByNroSolicitud(string nro_solicitud)
        {
            ResultadoRule result = new ResultadoRule();
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            try
            {

                OracleCommand com = new OracleCommand("cym.pck_cym_actualizaciones.pr_elim_asist_by_nrosolicitud", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pNro_solicitud", OracleDbType.Varchar2, 10);

                com.Parameters["pNro_solicitud"].Value = nro_solicitud;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                String Resultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                if (Resultado != "OK")
                {
                    result.OcurrioError = true;
                    result.MensajeError = Resultado;
                    return result;
                }
                result.MensajeExito = Resultado;
                result.OcurrioError = false;
                return result;

            }
            catch (Exception ex)
            {
                result.OcurrioError = true;
                return result;
            }
            finally
            {
                conn.Close();
            }
        }

        public string DaEliminarCurso(Int64 id_curso)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            try
            {

                OracleCommand com = new OracleCommand("cym.pck_cym_actualizaciones.pr_eliminar_curso", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdcurso", OracleDbType.Int32);

                com.Parameters["pIdcurso"].Value = id_curso;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                String Resultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                return Resultado;
                

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }


        /// <summary>
        /// Elimina FÍSICA de la solicitud y su detalle (asistentes al curso)
        /// </summary>
        /// <param name="Nro_solicitud"></param>
        /// <returns></returns>
        public string DaEliminarSolicitudCurso(Int64 Nro_solicitud)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            try
            {

                OracleCommand com = new OracleCommand("cym.pck_cym_actualizaciones.pr_Eliminar_Solicitud_curso", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("p_nroSolicitud", OracleDbType.Int32);

                com.Parameters["p_nroSolicitud"].Value = Nro_solicitud;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                com.ExecuteReader();

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                String Resultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                return Resultado;


            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Curso> GetCursos()
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            conn.Open();
            var lista = new List<Curso>();
            
            try
            {
                OracleCommand com = new OracleCommand("CYM.PCK_CYM_CONSULTA.pr_getcursos", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                var resultado = com.ExecuteReader();
                
                
                
                while (resultado.Read())
                {
                    var localidad = new LocalidadDto()
                    {
                        IdDepartamento = resultado["id_departamento"].ToString(),
                        IdLocalidad = resultado["id_localidad"].ToString(),
                        NombreLocalidad = resultado["n_localidad"].ToString()
                    };
                    var obj = new Curso
                    {
                        IdCurso=int.Parse(resultado["id_curso"].ToString()),
                        Descripcion = resultado["n_curso"].ToString(),
                        CantidadAsistentes = resultado["cant_asist"].ToString(),
                        FechaDictado = DateTime.Parse(resultado["fecha_curso"].ToString()),
                        Localidad = localidad
                    };

                    lista.Add(obj);
                }
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                conn.Close();
            }
            return lista;
        }

        public List<Tema> GetTemas()
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            conn.Open();
            var lista = new List<Tema>();
            
            try
            {
                OracleCommand com = new OracleCommand("CYM.PCK_CYM_CONSULTA.pr_getTemas", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                var resultado = com.ExecuteReader();
               
                
                while (resultado.Read())
                {
                    var tema = new Tema()
                    {
                        IdTema = Convert.ToInt32(resultado["id_temario"].ToString()),
                        NombreTema = resultado["n_temario"].ToString(), 
                    };
                    lista.Add(tema);
                }
                return lista;
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                conn.Close();
            }
            return lista;
        }

        public List<EstadoEntidad> GetEstadosEntidad()
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            conn.Open();
            var lista = new List<EstadoEntidad>();
            
            try
            {
                OracleCommand com = new OracleCommand("CYM.PCK_CYM_CONSULTA.pr_getEstados", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                var resultado = com.ExecuteReader();
               
                
                while (resultado.Read())
                {
                    var estado = new EstadoEntidad()
                    {
                        IdEstadoEntidad = Convert.ToInt32(resultado["id_estado"].ToString()),
                        NombreEstadoEntidad = resultado["n_estado"].ToString(), 
                    };
                    lista.Add(estado);
                }
                return lista;
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                conn.Close();
            }
            return lista;
        }

        public Curso GetCursoById(int curso)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            conn.Open(); 
            try
            {
                OracleCommand com = new OracleCommand("CYM.PCK_CYM_CONSULTA.pr_getCursoById", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdCurso", OracleDbType.Varchar2, 30);
                com.Parameters["pIdCurso"].Value = curso;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                var resultado = com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);


                while (resultado.Read())
                {
                    var localidad = new LocalidadDto()
                    {
                        IdDepartamento = resultado["id_departamento"].ToString(),
                        IdLocalidad = resultado["id_localidad"].ToString(),
                        NombreLocalidad = resultado["n_localidad"].ToString()
                    };
                    var c = new Curso
                    {
                        IdCurso = int.Parse(resultado["id_curso"].ToString()),
                        n_curso = resultado["n_curso"].ToString(),
                        CantidadAsistentes = resultado["cant_asist"].ToString(),
                        FechaDictado = DateTime.Parse(resultado["fecha_curso"].ToString()),
                        Soliciudes = GetSolicitCursosByIdCurso(resultado["id_curso"].ToString()),
                        Localidad = localidad
                    };
                    return c;
                    
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
            return null;
        }

        public List<SolicitudCursoDto> GetSolicitCursosByIdCurso(string idCurso)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            conn.Open();
            var lista = new List<SolicitudCursoDto>();

            try
            {
                OracleCommand com = new OracleCommand("CYM.PCK_CYM_CONSULTA.pr_getSolicCursoByIdCurso", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdCurso", OracleDbType.Varchar2, 30);
                com.Parameters["pIdCurso"].Value = idCurso;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                var resultado = com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);


                while (resultado.Read())
                {
                    var obj = new SolicitudCursoDto()
                    {

                        NombreCooperativa = resultado["nombre_entidad"].ToString(),
                        NombreDepartamento = resultado["departamento"].ToString(),
                        NombreLocalidad = resultado["localidad"].ToString(),
                        idLocalidad = resultado["id_localidad"].ToString(),
                        NroSolicitudCurso = resultado["nro_solicitud_curso"].ToString(),
                        FechaAlta = resultado["fec_solicitud"].ToString(), 
                        Estado = string.IsNullOrEmpty(resultado["ID_CURSO"].ToString()) ? "SIN_CURSO" : "CON_CURSO",
                        CuilUsuarioCidi = resultado["usuario_cidi"].ToString(),
                        CantidadEstimada = int.Parse(resultado["cant_estimada"].ToString())
                    };

                    lista.Add(obj);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return lista;
        }

        public List<AsistenteCursoDto> GetAsistSolicitCursosByNroSolicitud(string nroSolicitud)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            conn.Open();
            var lista = new List<AsistenteCursoDto>();

            try
            {
                OracleCommand com = new OracleCommand("CYM.PCK_CYM_CONSULTA.pr_getAsistSolicByNroSolicitud", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pNroSolicitud", OracleDbType.Varchar2, 30);
                com.Parameters["pNroSolicitud"].Value = nroSolicitud;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                var resultado = com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);


                while (resultado.Read())
                {
                    var obj = new AsistenteCursoDto()
                    {
                        NombreCompletoPersona = resultado["NOMBRE_COMPLETO"].ToString(),
                        Sexo = resultado["SEXO"].ToString(),
                        Dni = resultado["NRO_DOCUMENTO"].ToString(),
                        idSexo = resultado["ID_SEXO"].ToString(),
                        PaiCodPais = resultado["PAI_COD_PAIS"].ToString(),
                        idNumero = resultado["ID_NUMERO"].ToString(),
                        Contacto = new Comunicacion
                        {
                            EMail = resultado["EMAIL"].ToString() ,
                            CodAreaTelFijo = resultado["COD_AREA_TELFIJO"].ToString() ,
                            NroTelfijo = resultado["TEL_FIJO"].ToString() ,
                            CodAreaCel = resultado["COD_AREA_CELULAR"].ToString() ,
                            NroCel = resultado["CELULAR"].ToString(),
                            Tabla_Origen = "CYM.T_DETALLE_SOLICITUDES",
                            IdEntidad = resultado["ID_ENTIDAD"].ToString(),

                        }
                        
                    };

                    lista.Add(obj);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return lista;
        }

        public SolicitudCursoDto GetSolicitCursoByNroSolicitud(string nroSolicitud)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            conn.Open();

            SolicitudCursoDto solicitud = null;
            try
            {
                OracleCommand com = new OracleCommand("CYM.PCK_CYM_CONSULTA.pr_getSolicCursoByNroSolicitud", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pNroSolicitud", OracleDbType.Varchar2, 30);
                com.Parameters["pNroSolicitud"].Value = nroSolicitud;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                var resultado = com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);


                while (resultado.Read())
                {
                    
                    solicitud = new SolicitudCursoDto
                    {
                        NroSolicitudCurso = resultado["nro_solicitud_curso"].ToString(),
                        NombreCooperativa = resultado["descripcion"].ToString(),
                        NombreLocalidad = resultado["LOCALIDAD"].ToString(),
                        idLocalidad = resultado["IDLOCALIDAD"].ToString(),
                        NombreDepartamento = resultado["DEPARTAMENTO"].ToString(),
                        //FechaAlta = resultado["fec_solicitud"].ToString(),
                        //CuilUsuarioCidi = resultado["usuario_cidi"].ToString(),
                        //LinkArchivosAsistentes = resultado["link_archivo_asistentes"].ToString(),
                        //CantidadEstimada = int.Parse(resultado["cant_estimada"].ToString()),
                        Estado = string.IsNullOrEmpty(resultado["ID_CURSO"].ToString()) ? "SIN_CURSO" : "CON_CURSO"  ,
                    };

                    return solicitud;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return solicitud;
        }

        public string DaGetCantAsistentesTotales(string idCurso)
        {
            string Resultado="0";
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            conn.Open();
            
            try
            {
                OracleCommand com = new OracleCommand("CYM.PCK_CYM_CONSULTA.pr_getCantidadTotalAsist", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdCurso", OracleDbType.Varchar2, 30);
                com.Parameters["pIdCurso"].Value = idCurso;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2,10);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                com.ExecuteReader();

                Resultado = com.Parameters["pResultado"].Value.ToString();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }   
            
            return Resultado;
            
        }


        public IList<TramiteSUACDto> getTramiteSUAC(string NroTramite)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            
            var lista = new List<TramiteSUACDto>();
            var sql = new StringBuilder();
            try
            {
                conn.Open();
                OracleCommand com = new OracleCommand("CYM.PCK_CYM_CONSULTA.pr_getTramites_SUAC", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pNroTramite", OracleDbType.Varchar2, 30);
                com.Parameters["pNroTramite"].Value = NroTramite;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                var resultado = com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);

                while (resultado.Read())
                {
                    var obj = new TramiteSUACDto()
                    {
                        ID_TRAMITE = resultado["ID_TRAMITE"].ToString(),
                        NRO_TRAMITE = resultado["NRO_TRAMITE"].ToString(),
                        NRO_STICKER = resultado["NRO_STICKER"].ToString(),
                        ASUNTO = resultado["ASUNTO"].ToString(),
                        NOMBRE_INICIADOR = resultado["NOMBRE_INICIADOR"].ToString(),
                        NRO_DOCUMENTO = resultado["NRO_DOCUMENTO"].ToString(),
                        FECHA_CREACION = resultado["FECHA_CREACION"].ToString(),
                        TIPO = resultado["TIPO"].ToString(),
                        SUBTIPO = resultado["SUBTIPO"].ToString(),
                        UNIDAD_ACTUAL = resultado["UNIDAD_ACTUAL"].ToString(),
                        UNIDAD_PROXIMA = resultado["UNIDAD_PROXIMA"].ToString(),
                        FECHA_UNIDAD = resultado["FECHA_UNIDAD"].ToString(),
                        N_ESTADO = resultado["N_ESTADO"].ToString(),
                        ID_TRA_RELACIONADOS = resultado["ID_TRA_RELACIONADOS"].ToString()


                    };

                    lista.Add(obj);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return lista;

        }

       


        public string RegistrarEntidadEnFormacion(Entidad entidad, out ResultadoRule result)
        {
            result = new ResultadoRule(); 
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            try
            {

                OracleCommand com = new OracleCommand("CYM.PCK_CYM_INSERCIONES.pr_Insert_Entidad_EnFormacion", conn);
                com.CommandType = CommandType.StoredProcedure;


                com.Parameters.Add("pIdTipoEntidad", OracleDbType.Varchar2,2);
                com.Parameters.Add("pDescripcion", OracleDbType.Varchar2,500);
                com.Parameters.Add("pIdEstado", OracleDbType.Int32);
                com.Parameters.Add("pObservacion", OracleDbType.Varchar2,500);
                com.Parameters.Add("pNroSolicitud", OracleDbType.Int32); 

                if (string.IsNullOrEmpty(entidad.id_tipo_entidad))
                    com.Parameters["pIdTipoEntidad"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pIdTipoEntidad"].Value = entidad.id_tipo_entidad;
                }

                if (string.IsNullOrEmpty(entidad.descripcion))
                    com.Parameters["pDescripcion"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pDescripcion"].Value = entidad.descripcion;
                }

                
                com.Parameters["pIdEstado"].Value = 5; //EN FORMACIÓN

                if (string.IsNullOrEmpty(entidad.observacion))
                    com.Parameters["pObservacion"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pObservacion"].Value = entidad.observacion;
                }


                com.Parameters["pNroSolicitud"].Value = string.IsNullOrEmpty(entidad.NRO_SOLICITUD_CURSO) ? 0 : int.Parse(entidad.NRO_SOLICITUD_CURSO);

              

                com.Parameters.Add("pIdEntidad", OracleDbType.Varchar2, 20);
                com.Parameters["pIdEntidad"].Direction = ParameterDirection.Output;




                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 20);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                string pResultado = com.Parameters["pResultado"].Value.ToString();
                
                string idEntidadEnFormacion = com.Parameters["pIdEntidad"].Value.ToString();

                conn.Close();
                if (pResultado != "OK")
                {
                    result.OcurrioError = true;
                    result.MensajeError = pResultado;
                    return "";
                }

                
                result.OcurrioError = false;
                result.MensajeExito = "Se registró con éxito la Entidad.";

                return idEntidadEnFormacion;
            }
            catch (Exception ex)
            {
                result.OcurrioError = true;
                result.MensajeError = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return string.Empty;
        }


        public void RegistrarEntidadTramineSuac(Entidad entidad, out ResultadoRule result)
        {
            result = new ResultadoRule();
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            try
            {

                OracleCommand com = new OracleCommand("CYM.PCK_CYM_INSERCIONES.pr_Insert_Tramite_Suac", conn);
                com.CommandType = CommandType.StoredProcedure;


                com.Parameters.Add("pIdEntidad", OracleDbType.Varchar2);
                com.Parameters.Add("pNroExpediente", OracleDbType.Varchar2);
                com.Parameters.Add("pIdTramiteSuac", OracleDbType.Varchar2); 
                
                com.Parameters["pIdEntidad"].Value = entidad.id_entidad.ToString();

                com.Parameters["pNroExpediente"].Value = entidad.ExpedientesSuac[0].NRO_TRAMITE;

                com.Parameters["pIdTramiteSuac"].Value = entidad.ExpedientesSuac[0].ID_TRAMITE; 

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 20);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                string pResultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                if (pResultado == "OK")
                {
                    result.OcurrioError = false;
                    result.MensajeExito = "Se registró con éxito la Entidad Tramite Suac.";
                }
                else
                {
                    result.OcurrioError = true;
                    result.MensajeError = pResultado;
                }

            }
            catch (Exception ex)
            {
                result.OcurrioError = true;
                result.MensajeError = ex.Message;
            }
            finally
            {
                conn.Close();
            }

        }


        public List<CooperativaDto> GetCooperativasDtoByFilters(string descripcion, string matricula, string nroRegistro, string idEstado)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexion());

            var lista = new List<CooperativaDto>();

            try
            {
                conn.Open();
                OracleCommand com = new OracleCommand("CYM.PCK_CYM_CONSULTA.pr_getCooperativas", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pDescripcion", OracleDbType.Varchar2, 50);
                if (string.IsNullOrEmpty(descripcion))
                {
                    com.Parameters["pDescripcion"].Value = DBNull.Value;
                }
                else
                {
                    com.Parameters["pDescripcion"].Value = descripcion.ToUpper();
                }

                com.Parameters.Add("pMatricula", OracleDbType.Varchar2, 20);
                if (string.IsNullOrEmpty(matricula))
                {
                    com.Parameters["pMatricula"].Value = DBNull.Value;
                }
                else
                {
                    com.Parameters["pMatricula"].Value = matricula;
                }



                com.Parameters.Add("pNroRegistro", OracleDbType.Varchar2, 20);
                if (string.IsNullOrEmpty(nroRegistro))
                {
                    com.Parameters["pNroRegistro"].Value = DBNull.Value;
                }
                else
                {
                    com.Parameters["pNroRegistro"].Value = nroRegistro;
                }


                com.Parameters.Add("pIdEstado", OracleDbType.Varchar2, 10);
                if (string.IsNullOrEmpty(idEstado) || idEstado == "0")
                {
                    com.Parameters["pIdEstado"].Value = DBNull.Value;
                }
                else
                {
                    com.Parameters["pIdEstado"].Value = idEstado;
                }




                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                var resultado = com.ExecuteReader();
                //var da = new OracleDataAdapter(com);

                //var ds = new DataSet();
                //da.Fill(ds);

                while (resultado.Read())
                {
                    var obj = new CooperativaDto()
                    {
                        id_entidad = Convert.ToInt32(resultado["id_entidad"].ToString()),
                        nro_rp = resultado["nro_rp"].ToString(),
                        nro_matricula = resultado["nro_matricula"].ToString(),
                        descripcion = resultado["descripcion"].ToString(),
                        cuit = resultado["cuit"].ToString(),
                        EstadoEntidad = new EstadoEntidad { IdEstadoEntidad = int.Parse(resultado["id_estado_entidad"].ToString()) , NombreEstadoEntidad = resultado["estado"].ToString() },
                        NRO_SOLICITUD_CURSO = resultado["nro_solicitud_curso"].ToString(),
                        FECHA_APROBACION = string.IsNullOrEmpty(resultado["fecha_aprobacion"].ToString()) ? new DateTime?() : DateTime.Parse(resultado["fecha_aprobacion"].ToString()),
                        fecha_elevacion = string.IsNullOrEmpty(resultado["fecha_elevacion"].ToString()) ? new DateTime?() : DateTime.Parse(resultado["fecha_elevacion"].ToString()),
                        Fecha_Ult_Estado = string.IsNullOrEmpty(resultado["Fecha_Ult_Estado"].ToString()) ? new DateTime?() : DateTime.Parse(resultado["Fecha_Ult_Estado"].ToString())
                    };

                    lista.Add(obj);
                }
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                conn.Close();
            }
            return lista;
        }

        public void ActualizarEntidadEnFormacion(Curso curso, out ResultadoRule result)
        {
            result = new ResultadoRule();
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            try
            {

                OracleCommand com = new OracleCommand("CYM.PCK_CYM_ACTUALIZACIONES.pr_update_Entidad", conn);
                com.CommandType = CommandType.StoredProcedure;


                com.Parameters.Add("pIdTipoEntidad", OracleDbType.Varchar2, 2);
                com.Parameters.Add("pDescripcion", OracleDbType.Varchar2, 500);
                com.Parameters.Add("pIdEstado", OracleDbType.Int32);
                com.Parameters.Add("pObservacion", OracleDbType.Varchar2, 500);

                //if (string.IsNullOrEmpty(entidad.id_tipo_entidad))
                //    com.Parameters["pIdTipoEntidad"].Value = DBNull.Value;
                //else
                //{
                //    com.Parameters["pIdTipoEntidad"].Value = entidad.id_tipo_entidad;
                //}

                //if (string.IsNullOrEmpty(entidad.descripcion))
                //    com.Parameters["pDescripcion"].Value = DBNull.Value;
                //else
                //{
                //    com.Parameters["pDescripcion"].Value = entidad.descripcion;
                //}


                //com.Parameters["pIdEstado"].Value = 5; //EN FORMACIÓN

                //if (string.IsNullOrEmpty(entidad.observacion))
                //    com.Parameters["pObservacion"].Value = DBNull.Value;
                //else
                //{
                //    com.Parameters["pObservacion"].Value = entidad.observacion;
                //}


                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 20);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                string pResultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                if (pResultado == "OK")
                {
                    result.OcurrioError = false;
                    result.MensajeExito = "Se registró con éxito la Entidad.";
                }
                else
                {
                    result.OcurrioError = true;
                    result.MensajeError = pResultado;
                }

            }
            catch (Exception ex)
            {
                result.OcurrioError = true;
                result.MensajeError = ex.Message;
            }
            finally
            {
                conn.Close();
            }

        }

        public CooperativaDto GetCooperativaDtoById(int idEntidad)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
               
            try
            {
                conn.Open();
                OracleCommand com = new OracleCommand("CYM.PCK_CYM_CONSULTA.pr_getCooperativaById", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdEntidad", OracleDbType.Int32);
                com.Parameters["pIdEntidad"].Value = idEntidad;
                

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                var resultado = com.ExecuteReader();
                //OracleDataAdapter da = new OracleDataAdapter(com);

                //DataSet ds = new DataSet();
                //da.Fill(ds);

                while (resultado.Read())
                {
                    var obj = new CooperativaDto 
                    {
                        id_entidad = Convert.ToInt32(resultado["id_entidad"].ToString()),
                        id_dom_legal = string.IsNullOrEmpty(resultado["id_vin_dom_legal"].ToString()) ? new int?() : Convert.ToInt32(resultado["id_vin_dom_legal"].ToString()),
                        nro_rp = resultado["nro_rp"].ToString(),
                        nro_matricula = resultado["nro_matricula"].ToString(),
                        descripcion = resultado["descripcion"].ToString(),
                        cuit = resultado["cuit"].ToString(),
                        EstadoEntidad = new EstadoEntidad { IdEstadoEntidad = int.Parse(resultado["id_estado_entidad"].ToString()), NombreEstadoEntidad = resultado["estado"].ToString() },
                        NRO_SOLICITUD_CURSO = resultado["nro_solicitud_curso"].ToString(),
                        FECHA_APROBACION = string.IsNullOrEmpty(resultado["fecha_aprobacion"].ToString()) ? new DateTime?() : DateTime.Parse(resultado["fecha_aprobacion"].ToString()),
                        fecha_elevacion = string.IsNullOrEmpty(resultado["fecha_elevacion"].ToString()) ? new DateTime?() : DateTime.Parse(resultado["fecha_elevacion"].ToString())

                    };

                    return obj;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
            return null;
        }

        public List<HistorialEstadoEntidad> GetHistorialEstado(int idEntidad)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexion());

            var lista = new List<HistorialEstadoEntidad>();

            try
            {
                conn.Open();
                OracleCommand com = new OracleCommand("CYM.PCK_CYM_CONSULTA.pr_getHistEstadoEntidad", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdEntidad", OracleDbType.Int32);
                com.Parameters["pIdEntidad"].Value = idEntidad;

                 
                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                var resultado = com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);

                while (resultado.Read())
                {
                    var obj = new HistorialEstadoEntidad  
                    {
                        IdHistorialEstado = Convert.ToInt32(resultado["id_hist_estado_ent"].ToString()),
                        IdEstado = resultado["id_estado_entidad"].ToString(),
                        NombreEstado = resultado["n_estado"].ToString(),
                        FechaDesde = string.IsNullOrEmpty(resultado["fecha_desde"].ToString()) ? new DateTime?() : DateTime.Parse(resultado["fecha_desde"].ToString()),
                        FechaHasta = string.IsNullOrEmpty(resultado["fecha_hasta"].ToString()) ? new DateTime?() : DateTime.Parse(resultado["fecha_hasta"].ToString()),
                        Descripcion = resultado["descripcion"].ToString(),
                        CuilUsuarioCidi = resultado["usr_cidi"].ToString(),
                    };

                    lista.Add(obj);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return lista;
        }

        public void RegistrarHistorialEstadoEntidad(HistorialEstadoEntidad historialEstadoEntidad, out ResultadoRule result)
        {
            result = new ResultadoRule();
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            try
            {

                OracleCommand com = new OracleCommand("CYM.PCK_CYM_INSERCIONES.pr_Insert_Hist_Estado_Entidad", conn);
                com.CommandType = CommandType.StoredProcedure;


                com.Parameters.Add("pIdEntidad", OracleDbType.Varchar2);
                com.Parameters.Add("pIdEstado", OracleDbType.Varchar2);
                com.Parameters.Add("pFechaDesde", OracleDbType.Date);
                com.Parameters.Add("pFechaHasta", OracleDbType.Varchar2);
                com.Parameters.Add("pDescripcion", OracleDbType.Varchar2);
                com.Parameters.Add("pUsuarioCidi", OracleDbType.Varchar2);

                if (string.IsNullOrEmpty(historialEstadoEntidad.IdEntidad))
                    com.Parameters["pIdEntidad"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pIdEntidad"].Value = historialEstadoEntidad.IdEntidad;
                }

                if (string.IsNullOrEmpty(historialEstadoEntidad.IdEstado))
                    com.Parameters["pIdEstado"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pIdEstado"].Value = historialEstadoEntidad.IdEstado;
                }


                if (historialEstadoEntidad.FechaDesde.HasValue == false)
                    com.Parameters["pFechaDesde"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pFechaDesde"].Value = historialEstadoEntidad.FechaDesde.Value;
                }
                
                if (historialEstadoEntidad.FechaHasta.HasValue == false)
                    com.Parameters["pFechaHasta"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pFechaHasta"].Value = historialEstadoEntidad.FechaHasta.Value;
                }

                
                if (string.IsNullOrEmpty(historialEstadoEntidad.Descripcion))
                    com.Parameters["pDescripcion"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pDescripcion"].Value = historialEstadoEntidad.Descripcion;
                }

                if (string.IsNullOrEmpty(historialEstadoEntidad.CuilUsuarioCidi))
                    com.Parameters["pUsuarioCidi"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pUsuarioCidi"].Value = historialEstadoEntidad.CuilUsuarioCidi;
                }
                  

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 20);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                string pResultado = com.Parameters["pResultado"].Value.ToString();
                 

                conn.Close();
                if (pResultado == "OK")
                {
                    result.OcurrioError = false;
                    result.MensajeExito = "Se registró con éxito el Historial.";
                }
                else
                {
                    result.OcurrioError = true;
                    result.MensajeError = pResultado;
                }
                 
            }
            catch (Exception ex)
            {
                result.OcurrioError = true;
                result.MensajeError = ex.Message;
            }
            finally
            {
                conn.Close();
            }
             
        }


        /// <summary>
        ///Actualiza la Cooperativa(Entidad) los datos y el estado.
        /// Actualizo el último estado de la entidad en el historial de estado cargando la fecha_hasta.*/
        ///Por otro lado, se va a insertar un nuevo registro en T_HIST_ESTADO_ENTIDAD con fecha_hasta en null,
        ///indicando que es el ultimo estado.*/
        /// </summary>
        /// <param name="cooperativa"></param>
        /// <param name="result"></param>
        public void ActivarCooperativa(CooperativaDto cooperativa, out ResultadoRule result)
        {
            result = new ResultadoRule();
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            try
            {

                OracleCommand com = new OracleCommand("CYM.PCK_CYM_ACTUALIZACIONES.pr_Activar_Entidad", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdEntidad", OracleDbType.Int32);
                com.Parameters.Add("pNroRp", OracleDbType.Int32);
                com.Parameters.Add("pNroMatricula", OracleDbType.Int32);
                com.Parameters.Add("pDescripcion", OracleDbType.Varchar2,500);
                com.Parameters.Add("pCuit", OracleDbType.Int32);
                com.Parameters.Add("pIdEstado", OracleDbType.Int32);
                com.Parameters.Add("pIdVinLegal", OracleDbType.Int32);
                com.Parameters.Add("pFechaAprobacion", OracleDbType.Date);

                com.Parameters["pIdEntidad"].Value = cooperativa.id_entidad;

                if (string.IsNullOrEmpty(cooperativa.nro_rp))
                    com.Parameters["pNroRp"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pNroRp"].Value = int.Parse(cooperativa.nro_rp);
                }



                if (string.IsNullOrEmpty(cooperativa.nro_matricula))
                    com.Parameters["pNroMatricula"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pNroMatricula"].Value = int.Parse(cooperativa.nro_matricula);
                }


                if (string.IsNullOrEmpty(cooperativa.descripcion))
                    com.Parameters["pDescripcion"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pDescripcion"].Value = cooperativa.descripcion;
                }

                if (string.IsNullOrEmpty(cooperativa.cuit))
                    com.Parameters["pCuit"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pCuit"].Value = cooperativa.cuit;
                }

                if (cooperativa.id_estado.HasValue)
                    com.Parameters["pIdEstado"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pIdEstado"].Value = cooperativa.id_estado.Value;
                }

                if (cooperativa.id_dom_legal.HasValue)
                    com.Parameters["pIdVinLegal"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pIdVinLegal"].Value = cooperativa.id_dom_legal.Value;
                }

                if (cooperativa.FECHA_APROBACION.HasValue)
                    com.Parameters["pFechaAprobacion"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pFechaAprobacion"].Value = cooperativa.FECHA_APROBACION.Value;
                }

                
                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 20);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                string pResultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                if (pResultado == "OK")
                {
                    result.OcurrioError = false;
                    result.MensajeExito = "Se actualió con éxito la Entidad.";
                }
                else
                {
                    result.OcurrioError = true;
                    result.MensajeError = pResultado;
                }

                 
            }
            catch (Exception ex)
            {
                result.OcurrioError = true;
                result.MensajeError = ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }
        public List<Seccion> GetSecciones(string idTipoEntidad)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            conn.Open();
            var lista = new List<Seccion>();

            try
            {
                OracleCommand com = new OracleCommand("CYM.PCK_CYM_CONSULTA.pr_getSecciones", conn);
                com.CommandType = CommandType.StoredProcedure;



                com.Parameters.Add("pIdTipoEntidad", OracleDbType.Varchar2);
                com.Parameters["pIdTipoEntidad"].Value = idTipoEntidad;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                var resultado = com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);


                while (resultado.Read())
                {

                    var obj = new Seccion
                    {
                        IdSeccion = resultado["ID_SECCION"].ToString(),
                        NombreSeccion = resultado["N_SECCION"].ToString(),
                        IdTipoEntidad = resultado["ID_TIPO_ENTIDAD"].ToString(),
                        NombreTipoEntidad = resultado["TIPO_ENTIDAD"].ToString()
                    };

                    lista.Add(obj);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return lista;
        }

        public List<Cargo> GetCargos()
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            conn.Open();
            var lista = new List<Cargo>();

            try
            {
                OracleCommand com = new OracleCommand("CYM.PCK_CYM_CONSULTA.pr_getCargos", conn);
                com.CommandType = CommandType.StoredProcedure;


                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                var resultado = com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);


                while (resultado.Read())
                {

                    var obj = new Cargo
                    {
                        IdCargo = resultado["id_cargo"].ToString(),
                        NombreCargo = resultado["n_cargo"].ToString()
                    };

                    lista.Add(obj);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return lista;
        }

        public void RegistrarSeccionEntidad(Seccion seccion, int idEntidad, out ResultadoRule result)
        {
            result = new ResultadoRule();

            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            try
            {

                OracleCommand com = new OracleCommand("CYM.PCK_CYM_INSERCIONES.pr_Insert_Seccion_x_Entidad", conn);
                com.CommandType = CommandType.StoredProcedure;


                com.Parameters.Add("pIdEntidad", OracleDbType.Int32);
                com.Parameters.Add("pIdSeccion", OracleDbType.Varchar2);
                com.Parameters.Add("pMotivo", OracleDbType.Varchar2);

                com.Parameters["pIdEntidad"].Value = idEntidad;
                com.Parameters["pIdSeccion"].Value = seccion.IdSeccion;

                if (string.IsNullOrEmpty(seccion.Motivo))
                    com.Parameters["pMotivo"].Value = DBNull.Value;
                else
                {
                    com.Parameters["pMotivo"].Value = seccion.Motivo;
                }

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 20);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                string pResultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                if (pResultado == "OK")
                {
                    result.OcurrioError = false;
                    result.MensajeExito = "Se registró con éxito la Seccion_Entidad.";
                }
                else
                {
                    result.OcurrioError = true;
                    result.MensajeError = pResultado;
                }

            }
            catch (Exception ex)
            {
                result.OcurrioError = true;
                result.MensajeError = ex.Message;
            }
            finally
            {
                conn.Close();
            }

        }

        public void RegistrarAutoridadEntidad(Autoridad autoridad, int idEntidad, out ResultadoRule result)
        {
            result = new ResultadoRule();

            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            try
            {

                OracleCommand com = new OracleCommand("CYM.PCK_CYM_INSERCIONES.pr_Insert_Autoridad_x_Entidad", conn);
                com.CommandType = CommandType.StoredProcedure;


                com.Parameters.Add("pIdEntidad", OracleDbType.Int32);
                com.Parameters.Add("pIdSexo", OracleDbType.Varchar2);
                com.Parameters.Add("pDNI", OracleDbType.Varchar2);
                com.Parameters.Add("pPaiCodPais", OracleDbType.Varchar2);
                com.Parameters.Add("pIdNumero", OracleDbType.Varchar2);
                com.Parameters.Add("pCargo", OracleDbType.Varchar2);

                com.Parameters["pIdEntidad"].Value = idEntidad;
                com.Parameters["pIdSexo"].Value = autoridad.IdSexo;
                com.Parameters["pDNI"].Value = autoridad.DNI;
                com.Parameters["pPaiCodPais"].Value = autoridad.PaiCodPais;
                com.Parameters["pIdNumero"].Value = autoridad.IdNumero;
                com.Parameters["pCargo"].Value = autoridad.Cargo.IdCargo;

                
                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 20);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                OracleDataAdapter da = new OracleDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                string pResultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                if (pResultado == "OK")
                {
                    result.OcurrioError = false;
                    result.MensajeExito = "Se registró con éxito la Autoridad_Entidad.";
                }
                else
                {
                    result.OcurrioError = true;
                    result.MensajeError = pResultado;
                }

            }
            catch (Exception ex)
            {
                result.OcurrioError = true;
                result.MensajeError = ex.Message;
            }
            finally
            {
                conn.Close();
            }

        }

        public List<TramiteSUACDto> getTramiteSuacByEntidad(int idEntidad)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexion());

            var lista = new List<TramiteSUACDto>();
            var sql = new StringBuilder();
            try
            {
                conn.Open();
                OracleCommand com = new OracleCommand("CYM.PCK_CYM_CONSULTA.pr_getEntidad_x_TramiteSUAC", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdEntidad", OracleDbType.Int32);
                com.Parameters["pIdEntidad"].Value = idEntidad;

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                var resultado = com.ExecuteReader();
                OracleDataAdapter da = new OracleDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);

                while (resultado.Read())
                {
                    var obj = new TramiteSUACDto()
                    {
                        ID_TRAMITE = resultado["ID_TRAMITE"].ToString(),
                        NRO_TRAMITE = resultado["NRO_TRAMITE"].ToString(),
                        NRO_STICKER = resultado["NRO_STICKER"].ToString(),
                        ASUNTO = resultado["ASUNTO"].ToString(),
                        NOMBRE_INICIADOR = resultado["NOMBRE_INICIADOR"].ToString(),
                        NRO_DOCUMENTO = resultado["NRO_DOCUMENTO"].ToString(),
                        FECHA_CREACION = resultado["FECHA_CREACION"].ToString(),
                        TIPO = resultado["TIPO"].ToString(),
                        SUBTIPO = resultado["SUBTIPO"].ToString(),
                        UNIDAD_ACTUAL = resultado["UNIDAD_ACTUAL"].ToString(),
                        UNIDAD_PROXIMA = resultado["UNIDAD_PROXIMA"].ToString(),
                        FECHA_UNIDAD = resultado["FECHA_UNIDAD"].ToString(),
                        N_ESTADO = resultado["N_ESTADO"].ToString(),
                        ID_TRA_RELACIONADOS = resultado["ID_TRA_RELACIONADOS"].ToString()


                    };

                    lista.Add(obj);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return lista;

        }

        public List<TipoAsamblea> GetTipoAsambleas()
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexion());

            var lista = new List<TipoAsamblea>();


            try
            {
                OracleCommand com = new OracleCommand("CYM.PCK_CYM_CONSULTA.pr_getTipoAsamblea", conn);
                com.CommandType = CommandType.StoredProcedure;
                 

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                var resultado = com.ExecuteReader();

                while (resultado.Read())
                {
                    var obj = new TipoAsamblea
                    {
                        IdTipoAsamblea = Convert.ToInt32(resultado["id_tipo_asamblea"].ToString()),
                        Nombre = resultado["n_tipo_asamblea"].ToString() 
                    };

                    lista.Add(obj);
                }
               
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }

            return lista;
        }

        public List<DocumentoDto> GetDocumentos(string idTipoAsamblea, string idTipoEntidad)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexion());

            var lista = new List<DocumentoDto>();


            try
            {
                OracleCommand com = new OracleCommand("CYM.PCK_CYM_CONSULTA.pr_getDocumentos", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pIdTipoEntidad", OracleDbType.Varchar2);
                com.Parameters["pIdTipoEntidad"].Value = idTipoEntidad;

                com.Parameters.Add("pIdTipoAsamblea", OracleDbType.Int32);
                com.Parameters["pIdTipoAsamblea"].Value = int.Parse(idTipoAsamblea);

                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                conn.Open();

                var resultado = com.ExecuteReader();

                while (resultado.Read())
                {
                    var obj = new DocumentoDto
                    {
                        ID_DOCUMENTO = Convert.ToInt32(resultado["ID_DOCUMENTO"].ToString()),
                        N_DOCUMENTO = resultado["N_DOCUMENTO"].ToString(),
                        OBLIGATORIO = resultado["OBLIGATORIO"].ToString(),
                        ID_TIPO_ENTIDAD = resultado["ID_TIPO_ENTIDAD"].ToString(),
                        N_FORMA_JURIDICA = resultado["N_FORMA_JURIDICA"].ToString(),
                        ID_TIPO_ASAMBLEA = resultado["ID_TIPO_ASAMBLEA"].ToString(),
                        N_TIPO_ASAMBLEA = resultado["N_TIPO_ASAMBLEA"].ToString(),
                        N_PRESENTACION = resultado["N_PRESENTACION"].ToString(),
                        CANT_DIAS = resultado["CANT_DIAS"].ToString(),
                        INSTANCIA = resultado["INSTANCIA"].ToString()
                    };

                    lista.Add(obj);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }

            return lista;
        }

        public void RegistrarDocumentacionAsamblea(int idAsamblea , DocumentoDto documentoDto, out ResultadoRule result)
        {
            result = new ResultadoRule();
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            try
            {

                OracleCommand com = new OracleCommand("CYM.PCK_CYM_INSERCIONES.pr_Insert_Asamblea_x_Docum", conn);
                com.CommandType = CommandType.StoredProcedure;


                com.Parameters.Add("pIdAsamblea", OracleDbType.Int32);
                com.Parameters.Add("pIdDocumento", OracleDbType.Int32);
                com.Parameters.Add("pPresentada", OracleDbType.Varchar2,1);
                com.Parameters.Add("pFecPresentacion", OracleDbType.Date);

                com.Parameters["pIdAsamblea"].Value = idAsamblea;
                com.Parameters["pIdDocumento"].Value = documentoDto.ID_DOCUMENTO;
                com.Parameters["pPresentada"].Value = documentoDto.Presentada ? "S" : "N";
                com.Parameters["pFecPresentacion"].Value = documentoDto.Presentada ? DateTime.Now : (new DateTime?()).GetValueOrDefault();

                
                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 20);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                var res = com.ExecuteNonQuery();
                
                string pResultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                if (pResultado == "OK")
                {
                    result.OcurrioError = false;
                    result.MensajeExito = "Se registró con éxito la Asamblea_x_documentación.";
                }
                else
                {
                    result.OcurrioError = true;
                    result.MensajeError = pResultado;
                }
                 
            }
            catch (Exception ex)
            {
                result.OcurrioError = true;
                result.MensajeError = ex.Message;
            }
            finally
            {
                conn.Close();
            }
             
        }

        public void RegistrarTemario(int idAsamblea, Tema tema, out ResultadoRule result)
        {
            result = new ResultadoRule();
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            try
            {

                OracleCommand com = new OracleCommand("CYM.PCK_CYM_INSERCIONES.pr_Insert_Temario", conn);
                com.CommandType = CommandType.StoredProcedure;


                com.Parameters.Add("pIdTema", OracleDbType.Int32);
                com.Parameters.Add("pIdAsamblea", OracleDbType.Int32);
                com.Parameters.Add("pObservacion", OracleDbType.Varchar2, 500);


                com.Parameters["pIdTema"].Value = tema.IdTema;
                com.Parameters["pIdAsamblea"].Value = idAsamblea;
                com.Parameters["pObservacion"].Value = tema.Observacion;


                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 20);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                var res = com.ExecuteNonQuery();

                string pResultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                if (pResultado == "OK")
                {
                    result.OcurrioError = false;
                    result.MensajeExito = "Se registró con éxito la Tema por Asamblea.";
                }
                else
                {
                    result.OcurrioError = true;
                    result.MensajeError = pResultado;
                }

            }
            catch (Exception ex)
            {
                result.OcurrioError = true;
                result.MensajeError = ex.Message;
            }
            finally
            {
                conn.Close();
            }
             
        }

        public void RegistrarVeedor(int idAsamblea, Veedor veedor, out ResultadoRule result)
        {
            result = new ResultadoRule();
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            try
            {

                OracleCommand com = new OracleCommand("CYM.PCK_CYM_INSERCIONES.pr_Insert_Veedor", conn);
                com.CommandType = CommandType.StoredProcedure;


                com.Parameters.Add("pUsrCidi", OracleDbType.Int32);
                com.Parameters.Add("pIdAsamblea", OracleDbType.Int32);


                com.Parameters["pUsrCidi"].Value = veedor.cuil;
                com.Parameters["pIdAsamblea"].Value = idAsamblea;


                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 20);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;
                conn.Open();
                var res = com.ExecuteNonQuery();

                string pResultado = com.Parameters["pResultado"].Value.ToString();
                conn.Close();
                if (pResultado == "OK")
                {
                    result.OcurrioError = false;
                    result.MensajeExito = "Se registró con éxito el Veedor.";
                }
                else
                {
                    result.OcurrioError = true;
                    result.MensajeError = pResultado;
                }

            }
            catch (Exception ex)
            {
                result.OcurrioError = true;
                result.MensajeError = ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<AsambleaDto> GetAsambleasByFilter(string idTipoEntidad, string idEstado, string matricula, string nroReg, string fechaDesde, string fechaHasta)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexion());

            var lista = new List<AsambleaDto>();

            try
            {
                conn.Open();
                OracleCommand com = new OracleCommand("CYM.PCK_CYM_CONSULTA.pr_getAsambleas", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("idTipoEntidad", OracleDbType.Varchar2, 2);
                com.Parameters["idTipoEntidad"].Value = idTipoEntidad;
                

                com.Parameters.Add("pMatricula", OracleDbType.Varchar2, 20);
                if (string.IsNullOrEmpty(matricula))
                {
                    com.Parameters["pMatricula"].Value = DBNull.Value;
                }
                else
                {
                    com.Parameters["pMatricula"].Value = matricula;
                }



                com.Parameters.Add("pNroRegistro", OracleDbType.Varchar2, 20);
                if (string.IsNullOrEmpty(nroReg))
                {
                    com.Parameters["pNroRegistro"].Value = DBNull.Value;
                }
                else
                {
                    com.Parameters["pNroRegistro"].Value = nroReg;
                }


                com.Parameters.Add("pIdEstado", OracleDbType.Varchar2, 10);
                if (string.IsNullOrEmpty(idEstado) || idEstado == "0")
                {
                    com.Parameters["pIdEstado"].Value = DBNull.Value;
                }
                else
                {
                    com.Parameters["pIdEstado"].Value = idEstado;
                }


                com.Parameters.Add("pFechaDesde", OracleDbType.Varchar2,20);
                if (string.IsNullOrEmpty(fechaDesde))
                {
                    com.Parameters["pFechaDesde"].Value = DBNull.Value;
                }
                else
                {
                    com.Parameters["pFechaDesde"].Value = fechaDesde;
                }
                
                com.Parameters.Add("pFechaHasta", OracleDbType.Varchar2,20);
                if (string.IsNullOrEmpty(fechaHasta))
                {
                    com.Parameters["pFechaHasta"].Value = DBNull.Value;
                }
                else
                {
                    com.Parameters["pFechaHasta"].Value = fechaHasta;
                }



                com.Parameters.Add("pResultado", OracleDbType.RefCursor);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                var resultado = com.ExecuteReader();
                //OracleDataAdapter da = new OracleDataAdapter(com);

                //DataSet ds = new DataSet();
                //da.Fill(ds);

                while (resultado.Read())
                {
                    var asambleaDto = new AsambleaDto()
                    {
                        IdAsamblea = Convert.ToInt32(resultado["id_asamblea"].ToString()),
                        Ejercicio = resultado["ejercicio"].ToString(),
                        Lugar = "-",
                        Observacion = resultado["observacion"].ToString(),
                        NombreEstadoActualEntidad = resultado["n_estado"].ToString(),
                        FechaAsamblea = DateTime.Parse(resultado["fec_asamblea"].ToString()),
                        FechaPreAsamblea = DateTime.Parse(resultado["fec_preasamblea"].ToString()),
                        FechaPosAsamblea = DateTime.Parse(resultado["fec_postasamblea"].ToString()),
                    };

                    var entidad = new Entidad
                    {
                        id_entidad = Convert.ToInt32(resultado["id_entidad"].ToString()),
                        id_tipo_entidad = resultado["id_tipo_entidad"].ToString(),
                        nro_rp = resultado["nro_rp"].ToString(),
                        nro_matricula = resultado["nro_matricula"].ToString(),
                        descripcion = resultado["descripcion"].ToString()
                    };

                    asambleaDto.Entidad = entidad;

                    var tipoAsamblea = new TipoAsamblea
                    {
                        IdTipoAsamblea = Convert.ToInt32(resultado["id_tipo_asamblea"].ToString()),
                        Nombre = resultado["n_tipo_asamblea"].ToString(),
                    };
                    asambleaDto.TipoAsamblea = tipoAsamblea;

                    
                    lista.Add(asambleaDto);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return lista;
        }

        public bool ValidarEntidadEnFormacionByNroSolicitud(int nroSolicitudCurso, out ResultadoRule result)
        {
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            result = new ResultadoRule();
            try
            {
                conn.Open();
                OracleCommand com = new OracleCommand("CYM.PCK_CYM_CONSULTA.pr_getAsambleas", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pNroSolicitud", OracleDbType.Int32);
                com.Parameters["pNroSolicitud"].Value = nroSolicitudCurso;
                 


                com.Parameters.Add("pResultado", OracleDbType.Varchar2);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                var resultado = com.ExecuteReader();
                //OracleDataAdapter da = new OracleDataAdapter(com);

                //DataSet ds = new DataSet();
                //da.Fill(ds);

                var res = resultado["pResultado"].ToString();
                result.OcurrioError = false;

                return res == "SI";

            }
            catch (Exception ex)
            {
                result.OcurrioError = true;
                result.MensajeError = ex.Message;
                return false;
            }
            finally
            {
                conn.Close();
            }
            return false;
        }

        public string DaCheckExpedienteSolicitud(string pExpediente)
        {
            string Resultado = string.Empty;
            OracleConnection conn = new OracleConnection(CadenaDeConexion());
            conn.Open();

            try
            {
                OracleCommand com = new OracleCommand("CYM.PCK_CYM_CONSULTA.PR_CHECK_EXPEDIENTE_SOLICITUD", conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("pExpediente", OracleDbType.Varchar2, 30);
                com.Parameters["pExpediente"].Value = pExpediente;

                com.Parameters.Add("pResultado", OracleDbType.Varchar2, 100);
                com.Parameters["pResultado"].Direction = ParameterDirection.Output;

                com.ExecuteReader();

                Resultado = com.Parameters["pResultado"].Value.ToString();
            }
            catch (Exception ex)
            {
                Resultado = "ERROR: " + ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return Resultado;

        }
    }
}
