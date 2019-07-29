using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataAccessCyM;
using DataAccessCyM.Dtos;
using DataAccessCyM.Entidades;

namespace BlCyM
{
    public class ReglaDeNegocios 
    {
        public DataAccessMethods p_da;
        public ReglaDeNegocios() : base()
        {
            p_da = new DataAccessMethods();
        }
         

        public IList<Departamento> GetDepartamentos(string idProvincia)
        {
            return p_da.GetDepartamentos(idProvincia);
        }

        public IList<Localidad> GetLocalidades(string idDepartamento,string idProvincia)
        {
            return p_da.GetLocalidades(idDepartamento, idProvincia);
        }
 
        
        public IList<Calle> GetCalles(string idProvincia, string idDepartamento, string idLocalidad)
        {
            return p_da.GetCalles(idProvincia, idDepartamento, idLocalidad);
        }

        public IList<Barrio> GetBarrios(string idLocalidad)
        {
            return p_da.GetBarrios(idLocalidad);
        }

        public Domicilio GetDomicilioByIdVin(string idVin)
        {
            return p_da.GetDomicilioByIdVin(idVin);
        }

        public CooperativaDto GetCooperativaDtoById(int idEntidad)
        {
            var entidad =  p_da.GetCooperativaDtoById(idEntidad);

            if (entidad==null)
                return null;

            entidad.HistorialEstado = p_da.GetHistorialEstado(entidad.id_entidad);

            if (entidad.id_dom_legal.HasValue)
            {
                entidad.Domicilio = p_da.GetDomicilioByIdVin(entidad.id_dom_legal.ToString());    
            }

            var contacto = GetContacto(entidad.id_entidad.ToString(), "CYM.T_ENTIDADES");
            if (contacto != null)
            {
                entidad.Contacto = contacto;
            }

            if (entidad.id_tipo_entidad == Entidad.TipoEntidadCooperativa)
            {
                
            }
            if (entidad.id_tipo_entidad == Entidad.TipoEntidadMutual)
            {

            }

            return entidad;
        }

        

        public bool EliminarEntidad(int entidadSeleccionada)
        {
            throw new NotImplementedException();
        }

        public string EliminarCurso(Int64 id_curso)
        {
            var c = p_da.DaEliminarCurso(id_curso);
            return c;
        }

        public string EliminarSolicitudCurso(Int64 nro_solicitud)
        {
            var c = p_da.DaEliminarSolicitudCurso(nro_solicitud);
            return c;
        }
        

        public DataTable BlGetPersonaUnica(string nroDoc, string idSexo)
        {
            try
            {
                return p_da.DaGetPersonaUnica(nroDoc, idSexo);
                
            }
            catch (BlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public ResultadoRule RegistrarSolicitudCurso(SolicitudCurso solicitud)
        {
            var result = new ResultadoRule();

            if (solicitud.IdDepartamento == "0")
            {
                result.OcurrioError = true;
                result.MensajeError = "Debe seleccionar un departamento";
                return result;
            }
            if (solicitud.IdLocalidad == "0")
            {
                result.OcurrioError = true;
                result.MensajeError = "Debe seleccionar un departamento";
                return result;
            }
            if (solicitud.SolicitantesCurso.Count == 0)
            {
                result.OcurrioError = true;
                result.MensajeError = "Debe cargar al menos un Solicitante al Curso";
                return result;
            }
            if (string.IsNullOrEmpty(solicitud.NombreCooperativa))
            {
                result.OcurrioError = true;
                result.MensajeError = "Debe ingresar el Nombre de la Cooperativa en Formación";
                return result;
            }

            int nroSolicitud = 0;
            try
            {
                nroSolicitud = p_da.RegistrarSolicitudCurso(solicitud,out result);
                if (result.OcurrioError)
                {
                    return result;
                }
                solicitud.NroSolicitudCurso = nroSolicitud.ToString();

                foreach (var asistenteCursoDto in solicitud.SolicitantesCurso)
                {

                    p_da.RegistrarDetalleSolicitudCurso(asistenteCursoDto, solicitud.NroSolicitudCurso, out result);
                    if (result.OcurrioError)
                    {
                        p_da.DaEliminarSolicitudCurso(nroSolicitud);
                        result.MensajeError = "Error al cargar los asistentes al curso.";
                        return result;
                    }
                    if (asistenteCursoDto.Contacto != null)
                    {
                        var contacto = new Comunicacion()
                        {
                           EMail = asistenteCursoDto.Contacto.EMail,
                           CodAreaCel   = asistenteCursoDto.Contacto.CodAreaCel,
                           NroCel = asistenteCursoDto.Contacto.NroCel,
                           CodAreaTelFijo    = asistenteCursoDto.Contacto.CodAreaTelFijo,
                           NroTelfijo = asistenteCursoDto.Contacto.NroTelfijo,
                           IdEntidad = asistenteCursoDto.idSexo + asistenteCursoDto.Dni + asistenteCursoDto.PaiCodPais + asistenteCursoDto.idNumero, // ds.id_sexo||ds.nro_documento||ds.pai_cod_pais||ds.id_numero 
                           Tabla_Origen = "CYM.T_DETALLE_SOLICITUDES"
                        };
                        p_da.RegistrarContacto(contacto, out result); 
                    
                        if (result.OcurrioError)
                        {
                        
                            p_da.DaEliminarSolicitudCurso(nroSolicitud);
                            result.MensajeError = "Error al cargar el contacto.";
                            return result;
                        }
                    }
                    
                }
                if (result.OcurrioError)
                {
                    p_da.DaEliminarSolicitudCurso(nroSolicitud);
                    result.MensajeError = "Error al cargar los asistentes al curso.";
                    return result;
                }

                result.OcurrioError = false;
                result.MensajeExito = "Se registró una nueva solicitud con éxito. Nro Solicitud generada : " +
                                      nroSolicitud;

            }

           

            catch (Exception e)
            {
                p_da.DaEliminarSolicitudCurso(nroSolicitud);
                result.OcurrioError = true;
                result.MensajeError = e.Message;
            }
            

            return result;
        }

        /// <summary>
        /// Carga un domicilio dom_manager y devuelve el id_vinculo del nuevo domicilio
        /// </summary>
        /// <param name="idEntidad">Está compuesto por el CUIT + ID_APLICACIÓN. (id_aplicaion de SIFCOS es 152)</param>
        /// <param name="idVin">parametro de salida que se usa para cargar el id_vinculo del domicilio generado</param>
        /// <returns></returns>
        public string CargarDomicilio(string idEntidad, string idProvincia, string idDepartamento, string nombreLocalidad, string idTipoCalle,
            string nombreTipoCalle, string idCalle, string nombreCalle, string idBarrio, string nombreBarrio, string idPrecinto, string altura,
            string piso, string dpto, string torre, string idLocalidad, string codPostal, string manzana, string lote, out int? idVin)
        {
            return p_da.CargarDomicilio(idEntidad, idProvincia, idDepartamento, nombreLocalidad, idTipoCalle,
                nombreTipoCalle, idCalle, nombreCalle, idBarrio, nombreBarrio, idPrecinto, altura, piso, dpto, torre,
                idLocalidad, codPostal, manzana, lote, out idVin);
        }

        public IList<AsistenteCursoDto> GetAsistenteCursoDtos()
        {
            /*Metodo utilizado para el dataset del reporte*/
            return new List<AsistenteCursoDto>();
        }
         

        public IList<SolicitudCursoDto> GetSolicitudesCurso(string nombreCoop, string fechaDesde, string fechaHasta)
        {
            var lista = p_da.getSolicitudesCurso(nombreCoop, fechaDesde, fechaHasta);

            return lista;
        }

        public IList<SolicitudCursoDto> GetSolicitudesCurso(string cuilUsuarioLogueado)
        {
            var lista = p_da.getSolicitudesCurso("", "", "");
            
            return lista.Where(x => x.CuilUsuarioCidi == cuilUsuarioLogueado).ToList();
        }


        public Curso GetCursoById(int curso)
        {
            var c = p_da.GetCursoById(curso);
            return c;

        }

        public ResultadoRule RegistrarCurso(Curso curso)
        {
            var result = new ResultadoRule();

            if (curso.FechaDictado.HasValue == false)
            {
                result.OcurrioError = true;
                result.MensajeError = "Debe ingresar la Fecha de Dictado del Curso";
                return result;
            }
            if (curso.Localidad.IdLocalidad == "0")
            {
                result.OcurrioError = true;
                result.MensajeError = "Debe seleccionar un localidad";
                return result;
            }
            if (curso.Soliciudes.Count == 0)
            {
                result.OcurrioError = true;
                result.MensajeError = "Debe seleccionar al menos alguna Solicitud de Curso.";
                return result;
            }
            if (string.IsNullOrEmpty(curso.n_curso))
            {
                result.OcurrioError = true;
                result.MensajeError = "Debe ingresar el Nombre del Curso";
                return result;
            }

            try
            {
                int idCurso = p_da.RegistrarCurso(curso, out result);
                if (result.OcurrioError)
                {
                    return result;
                }
                curso.IdCurso = idCurso;
                result.OcurrioError = false;
                result.MensajeExito = "Se registró un nuevo curso con éxito. " ;

            }



            catch (Exception e)
            {
                result.OcurrioError = true;
                result.MensajeError = e.Message;
            }


            return result;
        }

        public ResultadoRule ActualizarCurso(Curso curso)
        {
            var result = new ResultadoRule();

            if (curso.FechaDictado.HasValue == false)
            {
                result.OcurrioError = true;
                result.MensajeError = "Debe ingresar la Fecha de Dictado del Curso";
                return result;
            }
            if (curso.Localidad.IdLocalidad == "0")
            {
                result.OcurrioError = true;
                result.MensajeError = "Debe seleccionar un localidad";
                return result;
            }
            if (curso.Soliciudes.Count == 0)
            {
                result.OcurrioError = true;
                result.MensajeError = "Debe seleccionar al menos alguna Solicitud de Curso.";
                return result;
            }
            if (string.IsNullOrEmpty(curso.n_curso))
            {
                result.OcurrioError = true;
                result.MensajeError = "Debe ingresar el Nombre del Curso";
                return result;
            }

            try
            {
                  p_da.ActualizarCurso(curso, out result);
                if (result.OcurrioError)
                {
                    return result;
                }
                result.OcurrioError = false;
                result.MensajeExito = "Se actualizó el curso con éxito. ";

            }



            catch (Exception e)
            {
                result.OcurrioError = true;
                result.MensajeError = e.Message;
            }


            return result;
        }

        public ResultadoRule ActualizarSolicitudCurso(SolicitudCurso Solicitud)
        {
            var result = new ResultadoRule();

            if (Solicitud.fec_solicitud.HasValue)
            {
                result.OcurrioError = true;
                result.MensajeError = "Debe ingresar la Fecha de Dictado del Curso";
                return result;
            }
            if (Solicitud.IdLocalidad == "0")
            {
                result.OcurrioError = true;
                result.MensajeError = "Debe seleccionar un localidad";
                return result;
            }
            
            if (string.IsNullOrEmpty(Solicitud.NombreCooperativa))
            {
                result.OcurrioError = true;
                result.MensajeError = "Debe ingresar el Nombre de la  solicitud de Curso";
                return result;
            }

            try
            {
                result=BLEliminarAsistentesByNroSolicitud(Solicitud.NroSolicitudCurso);
                if (result.OcurrioError)
                {
                    return result;
                }
                
                p_da.ActualizarSolicitud(Solicitud,out result);
                if (result.OcurrioError)
                {
                    return result;
                }
                result.OcurrioError = false;
                result.MensajeExito = result.MensajeExito;

            }



            catch (Exception e)
            {
                result.OcurrioError = true;
                result.MensajeError = e.Message;
            }


            return result;
        }

        public IList<Curso> GetCursos()
        {
            List<Curso> c = p_da.GetCursos();
            return c;
        }

        public List<TramiteSUACDto> GetTramitesSuac(string nroExpediente)
        {
            return p_da.getTramiteSUAC(nroExpediente).ToList();
        }

        public SolicitudCursoDto GetSolicitudCursoDtoById(string nroSolicitud)
        {
            var solicitud = p_da.GetSolicitCursoByNroSolicitud(nroSolicitud);
            solicitud.LocalidadDto = p_da.GetLocalidadDto(solicitud.idLocalidad);

            var asistentes = p_da.GetAsistSolicitCursosByNroSolicitud(solicitud.NroSolicitudCurso);

            solicitud.SolicitantesCurso = asistentes;

            return solicitud;
        }

        public ResultadoRule RegistrarCooperativaEnFormacion(Entidad cooperativaEnFormacion)
        {
            var result = new ResultadoRule();

            if (string.IsNullOrEmpty(cooperativaEnFormacion.NRO_EXPEDIENTE_SAUC))
            {
                result.OcurrioError = true;
                result.MensajeError = "El campo NRO DE EXPEDIENTE SUAC es obligatorio.";
                return result;
            }

            string check = this.checkExpedienteSolicitud(cooperativaEnFormacion.NRO_EXPEDIENTE_SAUC);
            if (check != "OK")
            {
                result.OcurrioError = true;
                result.MensajeError = check;
                return result;
            }
            if (string.IsNullOrEmpty(cooperativaEnFormacion.NRO_SOLICITUD_CURSO))
            {
                result.OcurrioError = true;
                result.MensajeError = "El campo NRO SOLICITUD DE CURSO es obligatorio.";
                return result;
            }
            if (cooperativaEnFormacion.fecha_elevacion.HasValue == false)
            {
                result.OcurrioError = true;
                result.MensajeError = "Debe ingresar la Fecha de Elevación.";
                return result;
            }

            if (string.IsNullOrEmpty(cooperativaEnFormacion.descripcion))
            {
                result.OcurrioError = true;
                result.MensajeError = "Debe ingresar una Solicitud, ya que el nombre de la cooperativa está vacío.";
                return result;
            }

            if (cooperativaEnFormacion.ExpedientesSuac.Count == 0)
            {
                result.OcurrioError = true;
                result.MensajeError = "No se encuentran los datos del expediente asignado. Nro Expediente : " + cooperativaEnFormacion.NRO_EXPEDIENTE_SAUC;
                return result;
            }

            /*Valido que no exista una Entidad en Formación para el  Nro_solicitud cargado.*/
            result = ValidarEntidadEnFormacionByNroSolicitud(cooperativaEnFormacion);
            if (result.OcurrioError)
                return result;

            try
            {
                /*Guardo una coopoerativa en formación*/
                 var idEntidadEnFormacion=  p_da.RegistrarEntidadEnFormacion(cooperativaEnFormacion, out result);
                if (result.OcurrioError)
                {
                    return result;
                }

                cooperativaEnFormacion.id_entidad = Convert.ToInt32(idEntidadEnFormacion);


                /*Guardo el HISTORIAL*/
                var historial = new HistorialEstadoEntidad
                {
                    IdEntidad = idEntidadEnFormacion,
                    CuilUsuarioCidi = cooperativaEnFormacion.CUIL_USR_CIDI,
                    Descripcion = "Carga de una Cooperativa En Formación llamada " + cooperativaEnFormacion.descripcion,
                    FechaDesde = DateTime.Now,
                    IdEstado = "5", //EN_FORMACION
                    NombreEstado = "EN_FORMACION"
                };

                p_da.RegistrarHistorialEstadoEntidad(historial, out result);

                if (result.OcurrioError)
                {
                    result.OcurrioError = true;
                    result.MensajeError = result.MensajeError;
                    return result;
                }


                /*Luego , guardo en  t_tramites_suac el expediente asignado a la cooperativa en formación creada*/
                p_da.RegistrarEntidadTramineSuac(cooperativaEnFormacion, out result);
                if (result.OcurrioError)
                {
                    return result;
                }
                result.OcurrioError = false;
                result.MensajeExito = "Se registró la Nueva Cooperativa en Formación con éxito. ";

            }

            catch (Exception e)
            {
                result.OcurrioError = true;
                result.MensajeError = e.Message;
            }


            return result;
        }

        private ResultadoRule ValidarEntidadEnFormacionByNroSolicitud(Entidad cooperativaEnFormacion)
        {
            ResultadoRule result;
            var existe = p_da.ValidarEntidadEnFormacionByNroSolicitud(int.Parse(cooperativaEnFormacion.NRO_SOLICITUD_CURSO),out result);
            if (result.OcurrioError)
            {
                return result;
            }
            
            if (existe)
            {
                result.MensajeError = "Ya existe una Entidad en Formación con el número de Solicitud " +
                                      cooperativaEnFormacion.NRO_SOLICITUD_CURSO + ".";
                result.OcurrioError = true;
                return result;
            }

            result.OcurrioError = false;
            return result;

        }


        public List<CooperativaDto> GetCooperativasDtoByFilters(string descripcion, string matricula, string nroRegistro,
            string idEstado)
        {
            return p_da.GetCooperativasDtoByFilters(descripcion, matricula, nroRegistro, idEstado);
        }

        public List<EstadoEntidad> GetEstadosEntidad()
        {
            return p_da.GetEstadosEntidad();
        }

        public ResultadoRule BlRegistrarContacto(Comunicacion comunicacion)
        {  
            ResultadoRule result;
            p_da.RegistrarContacto(comunicacion,out result);
            return result;
        }

        public void ActivarCooperativa(CooperativaDto cooperativa, out ResultadoRule result)
        {
            p_da.ActivarCooperativa(cooperativa, out result);
            if (result.OcurrioError)
            {
                return;
            }

            /*Guardo un nuevo  HISTORIAL de estado*/
            var historial = new HistorialEstadoEntidad
            {
                IdEntidad = cooperativa.id_entidad.ToString(),
                CuilUsuarioCidi = cooperativa.CUIL_USR_CIDI,
                Descripcion = "Se Activa a la Cooperativa " + cooperativa.descripcion,
                FechaDesde = DateTime.Now,
                IdEstado = "1", //ACTIVA
                NombreEstado = "ACTIVA"
            };

            p_da.RegistrarHistorialEstadoEntidad(historial, out result);

        }

        public ResultadoRule BLEliminarAsistentesByNroSolicitud(string nro_solicitud)
        {
            var result = p_da.DaEliminarAsistentesByNroSolicitud(nro_solicitud);
            return result;
        }

        public List<Seccion> GetSecciones(string idTipoEntidad)
        {
            return p_da.GetSecciones(idTipoEntidad);
        }

        public List<Cargo> GetCargos()
        {
            return p_da.GetCargos();
        }

        public ResultadoRule RegistrarSeccionEntidad(Seccion seccion, int idEntidad)
        {
            var result = new ResultadoRule();
            p_da.RegistrarSeccionEntidad(seccion, idEntidad, out result);

            return result;
        }
        public ResultadoRule RegistrarAutoridadEntidad(Autoridad autoridad, int idEntidad)
        {
            ResultadoRule result;
            p_da.RegistrarAutoridadEntidad(autoridad, idEntidad, out result);

            return result;
        }

        public List<TramiteSUACDto> GetTramitesSuacByEntidad(int idEntidad)
        {
            return p_da.getTramiteSuacByEntidad(idEntidad);
        }

        public ResultadoRule RegistrarAsamblea(int idEntidad, int idTipoAsamblea, Asamblea asamblea)
        {
            ResultadoRule result;
            var idAsamblea = p_da.RegistrarAsamblea(idEntidad, idTipoAsamblea, asamblea, out result);
            if (result.OcurrioError)
                return result;

            /*Guardar documentos presentados*/

            foreach (DocumentoDto documentoDto in asamblea.DocumentosPreAsamblea)
            {
                p_da.RegistrarDocumentacionAsamblea(idAsamblea,documentoDto, out result);
                if (result.OcurrioError)
                    break;
            }
            foreach (DocumentoDto documentoDto in asamblea.DocumentosPosAsamblea)
            {
                p_da.RegistrarDocumentacionAsamblea(idAsamblea, documentoDto, out result);
                if (result.OcurrioError)
                    break;
            }
            if (result.OcurrioError)
                return result;

            /*Guardar Temarios que se van a tratar. Puede ser null*/
            foreach (Tema tema in asamblea.Temario)
            {
                p_da.RegistrarTemario(idAsamblea,tema, out result);
                if (result.OcurrioError)
                    break;
            }
            if (result.OcurrioError)
                return result;
            
            

            /*Guardar Veedores*/
            
            foreach (Veedor veedor in asamblea.Veedores)
            {
                p_da.RegistrarVeedor(idAsamblea, veedor, out result);
                if (result.OcurrioError)
                    break;
            }
            
            return result;
        }

        public IList<TipoAsamblea> GetTipoAsambleas()
        {
            return p_da.GetTipoAsambleas();
        }

        public List<DocumentoDto> GetDocumentos(string idTipoAsamblea, string idTipoEntidad)
        {
            return p_da.GetDocumentos(idTipoAsamblea, idTipoEntidad);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="idEntidad"> Es el Campo ID_ENTIDAD si la tabla orgien es CYM.T_ENTIDADES. Si la tabla es CYM.T_DETALLE_SOLICITUD corresponde el idEntidad a ds.id_sexo||ds.nro_documento||ds.pai_cod_pais||ds.id_numero  del ASISTENTE.  </param>
        /// <param name="tablaOrigen">Referencia de que contacto se trae. Puede der CYM.T_ENTIDAD ó CYM.T_DETALLE_SOLICITUD </param>
        /// <returns></returns>
        public Comunicacion GetContacto(string idEntidad, string tablaOrigen)
        {
            return p_da.GetContacto(idEntidad,tablaOrigen);
        }

        public List<Tema> GetTemas()
        {
            return p_da.GetTemas();
        }


        public List<AsambleaDto> GetAsambleasByFilter(string idTipoEntidad, string idEstado, string matricula, string nroReg, string fechaDesde, string fechaHasta)
        {
            return p_da.GetAsambleasByFilter(idTipoEntidad, idEstado, matricula, nroReg, fechaDesde, fechaHasta);
        }

        public string checkExpedienteSolicitud(string nroExpediente)
        {
            return p_da.DaCheckExpedienteSolicitud(nroExpediente);
        }
    }
}
