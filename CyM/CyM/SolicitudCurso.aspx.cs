using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using AppComunicacion;
using AppComunicacion.ApiModels;
using BlCyM;
using CyM.Models;
using DataAccessCyM;
using DataAccessCyM.Dtos;
using DataAccessCyM.Entidades; 


namespace CyM
{
    public partial class SolictudCurso : System.Web.UI.Page
    {
        public ReglaDeNegocios Bl = new ReglaDeNegocios();
        //public Principal master;


        protected void Page_Load(object sender, EventArgs e)
        {
            //master = (Principal) Page.Master;

            if (!Page.IsPostBack)
            {
                //if (Request.Cookies["CiDi"] != null)
                //{
                //    ObtenerUsuario();

                    divMensajeError.Visible = false;
                    divMensajeExito.Visible = false;
                    divMensajeErrorAgregarSolicitante.Visible = false;

                    CambiarEstado(EstadoAbmcEnum.REGISTRANDO);
                    CargarDepartamentos();
                    //RefrescarGrillaAsistentes();
                    txtNombreCooperativa.Focus();

                //}
                //else
                //{
                //    Response.Redirect(ConfigurationManager.AppSettings["CiDiUrl"] + "?url=" +
                //                      ConfigurationManager.AppSettings["Url_Retorno"] + "&app=" +
                //                      ConfigurationManager.AppSettings["CiDiIdAplicacion"]);
                //}
               
            }
        }

        private EstadoAbmcEnum EstadoVista
        {
            get { return (EstadoAbmcEnum) Session["EstadoVista"]; }
            set { Session["EstadoVista"] = value; }
        }

        private int EntidadSeleccionada
        {
            get { return (int) Session["EntidadSeleccionada"]; }
            set { Session["EntidadSeleccionada"] = value; }
        }

        private IList<AsistenteCursoDto> ListaAsistentesCurso
        {
            get
            {
                return Session["ListaAsistentesCurso"] == null
                    ? new List<AsistenteCursoDto>()
                    : (List<AsistenteCursoDto>) Session["ListaAsistentesCurso"];
            }
            set { Session["ListaAsistentesCurso"] = value; }
        }

        private void CargarDepartamentos()
        {
            ddlDepartamento.DataSource = Bl.GetDepartamentos("X");
            ddlDepartamento.DataTextField = "NombreDepartamento";
            ddlDepartamento.DataValueField = "IdDepartamento";
            ddlDepartamento.DataBind();
        }

        private void CargarLocalidades()
        {
            ddlLocalidad.DataSource = Bl.GetLocalidades(ddlDepartamento.SelectedValue, "X");
            ddlLocalidad.DataTextField = "NombreLocalidad";
            ddlLocalidad.DataValueField = "IdLocalidad";
            ddlLocalidad.DataBind();
        }
        public Usuario UsuarioCidiLogueado
        {
            get
            {
                return Session["UsuarioCiDiLogueado"] == null ? new Usuario() : (Usuario)Session["UsuarioCiDiLogueado"];

            }
            set
            {
                Session["UsuarioCiDiLogueado"] = value;
            }
        }
      
        protected void btnNuevo_OnClick(object sender, EventArgs e)
        {
            CambiarEstado(EstadoAbmcEnum.REGISTRANDO);
        }


        protected void btnCancelar_OnClick(object sender, EventArgs e)
        {
            //CambiarEstado(EstadoAbmcEnum.CONSULTANDO);
            Response.Redirect("~/SolicitudCurso.aspx");
        }

        protected void btnConsultar_OnClick(object sender, EventArgs e)
        {
            AccionConsultar();
        }

        private void AccionConsultar()
        {
            //var lista = ;
            //Session["ListaSession"] = lista;
            //RefrescarGrilla();
        }

        private void RefrescarGrilla()
        {
            var lista = (List<CooperativaDto>) Session["ListaSession"];
            gvResultado.PagerSettings.Mode = PagerButtons.Numeric;

            if (lista.Count > 0)
            {
                gvResultado.PagerSettings.PageButtonCount =
                    int.Parse(Math.Ceiling((double) (lista.Count / (double) gvResultado.PageSize)).ToString());
                gvResultado.PagerSettings.Visible = false;
                gvResultado.DataSource = lista;
                gvResultado.DataBind();

                lblTotalRegistrosGrilla.Text = lista.Count.ToString();
                var cantBotones = gvResultado.PagerSettings.PageButtonCount;
                var listaNumeros = new ArrayList();

                for (int i = 0; i < cantBotones; i++)
                {
                    var datos = new
                    {
                        nroPagina = i + 1
                    };
                    listaNumeros.Add(datos);
                }

                rptBotonesPaginacion.DataSource = listaNumeros;
                rptBotonesPaginacion.DataBind();
            }
        }

        protected void gvResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvResultado.PageIndex = e.NewPageIndex;
            RefrescarGrilla();
        }

        protected void gvResultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void rptBotonesPaginacion_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int nroPagina = Convert.ToInt32(e.CommandArgument.ToString());
            gvResultado.PageIndex = nroPagina - 1;
            
            RefrescarGrilla();
        }


        protected void btnNroPagina_OnClick(object sender, EventArgs e)
        {
            banderaPrimeraCargaPagina = true;

            var btn = (LinkButton) sender;
            //guardo el comando del boton de pagina seleccinoado
            commandoBotonPaginaSeleccionado = btn.CommandArgument;
        }

        private string commandoBotonPaginaSeleccionado = "";
        private bool banderaPrimeraCargaPagina = false;

        protected void rptBotonesPaginacion_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var btn = (LinkButton) e.Item.FindControl("btnNroPagina");
                if (btn.CommandArgument == "1" && banderaPrimeraCargaPagina == false)
                {
                    btn.BackColor = Color.Gainsboro; //pinto el boton.
                }

                if (btn.CommandArgument == commandoBotonPaginaSeleccionado)
                {
                    //por cada boton pregunto y encuentro el comando seleccionado q corresponde al boton selecionado.
                    btn.BackColor = Color.Gainsboro; //pinto el boton.
                }

                //los demas botones se cargan con el color de fondo blanco por defecto.
            }
        }

        protected void ddlProvincia_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDepartamentos();
        }


        protected void ddlDepartamento_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CargarLocalidades();
        }

        protected void ddlLocalidad_OnSelectedIndexChanged(object sender, EventArgs e)
        {
        }


        protected void gvResultado_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            //var acciones = new List<string> {"Editar", "Eliminar", "Ver"};

            //if (!acciones.Contains(e.CommandName))
            //    return;

            //gvResultado.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            //EntidadSeleccionada = 0;

            //if (gvResultado.SelectedValue != null)
            //    EntidadSeleccionada = int.Parse(gvResultado.SelectedValue.ToString());

            //switch (e.CommandName)
            //{
            //    //case "Ver":
            //    //    VerEntidad(EstadoAbmcEnum.VIENDO);
            //    //    break;

            //    //case "Editar":
            //    //    EditarEntidad(EstadoAbmcEnum.EDITANDO);
            //    //    break;

            //    //case "Eliminar":
            //    //    EliminarEntidad(EstadoAbmcEnum.ELIMINANDO);
            //    //    break;
            //}
        }

        //private void EditarEntidad(EstadoAbmcEnum estado)
        //{
        //    CambiarEstado(estado);
        //    var entidad = Bl.GetSolicitudCursoDtoById(EntidadSeleccionada.ToString());
        //    CargarCamposFormulario(entidad);
        //    HabilitarDehabilitarCampos(true);
        //}

        //private void EliminarEntidad(EstadoAbmcEnum estado)
        //{
        //    CambiarEstado(estado);
        //    var entidad = Bl.GetSolicitudCursoDtoById(EntidadSeleccionada.ToString());
        //    CargarCamposFormulario(entidad);
        //    HabilitarDehabilitarCampos(false);
        //}

        //private void VerEntidad(EstadoAbmcEnum estado)
        //{
        //    CambiarEstado(estado);
        //    var entidad = Bl.GetSolicitudCursoDtoById(EntidadSeleccionada.ToString());
        //    CargarCamposFormulario(entidad);
        //    HabilitarDehabilitarCampos(false);
        //}

        private void HabilitarDehabilitarCampos(bool valor)
        {
            txtNombreCooperativa.Enabled = valor;


            //CONTACTO
            txtEmail.Enabled = valor;
            txtCelular.Enabled = valor;
            txtTelefonoFijo.Enabled = valor;


            //DOMICILIO

            ddlDepartamento.Enabled = valor;
            ddlLocalidad.Enabled = valor;
        }

        private void CargarCamposFormulario(SolicitudCursoDto entidad)
        {
            //txtNombreCooperativa.Text = entidad.cuit;


            //var domicilio = _domicilioRules.ConsultarDomicilio(entidad.id_dom_legal);

            //CargarDepartamentos();
            //ddlDepartamento.SelectedValue = domicilio.ID_DEPARTAMENTO;
            //CargarLocalidades();
            //ddlLocalidad.SelectedValue = domicilio.ID_LOCALIDAD;
        }

        private void CambiarEstado(EstadoAbmcEnum estado)
        {
            switch (estado)
            {
                case EstadoAbmcEnum.CONSULTANDO:
                    divPantallaConsulta.Visible = true;
                    divPantallaABM.Visible = false;
                    EstadoVista = EstadoAbmcEnum.CONSULTANDO;
                    break;
                case EstadoAbmcEnum.REGISTRANDO:
                    divPantallaConsulta.Visible = false;
                    divPantallaABM.Visible = true;
                    lblTituloPantallaABM.Text = "Registrar Nueva Solicitud de Curso";
                    EstadoVista = EstadoAbmcEnum.REGISTRANDO;
                    HabilitarDehabilitarCampos(true);
                    LimpiarCamporFormuario();
                    divMensajeError.Visible = false;
                    divMensajeExito.Visible = false;
                    break;
                case EstadoAbmcEnum.EDITANDO:
                    divPantallaConsulta.Visible = false;
                    divPantallaABM.Visible = true;
                    lblTituloPantallaABM.Text = "Editar Solicitud de Curso";
                    EstadoVista = EstadoAbmcEnum.EDITANDO;
                    divMensajeError.Visible = false;
                    divMensajeExito.Visible = false;
                    break;
                case EstadoAbmcEnum.VIENDO:
                    divPantallaConsulta.Visible = false;
                    divPantallaABM.Visible = true;
                    EstadoVista = EstadoAbmcEnum.VIENDO;
                    lblTituloPantallaABM.Text = "Datos de la Solicitud de Curso";
                    divMensajeError.Visible = false;
                    divMensajeExito.Visible = false;
                    break;
                case EstadoAbmcEnum.ELIMINANDO:
                    divPantallaConsulta.Visible = false;
                    divPantallaABM.Visible = true;
                    EstadoVista = EstadoAbmcEnum.ELIMINANDO;
                    lblTituloPantallaABM.Text = "Eliminar Solicitud de Curso";
                    divMensajeError.Visible = false;
                    divMensajeExito.Visible = false;
                    break;
            }
        }

        private void LimpiarCamporFormuario()
        {
            txtNombreCooperativa.Text = "";


            //CargarDepartamentos();
            ddlDepartamento.SelectedValue = "0";
            //CargarLocalidades();
            ddlLocalidad.SelectedValue = "0";
        }

        protected void btnAceptar_OnClick(object sender, EventArgs e)
        {
            AccionGuardar();
        }
  
        private void AccionGuardar()
        {
            var solicitud = CargarDatosSolicitud();
            ResultadoRule result = Bl.RegistrarSolicitudCurso(solicitud);

            if (result.OcurrioError)
            {
                lblMensajeError.Text = result.MensajeError;
                divMensajeError.Visible = true;
                divMensajeExito.Visible = false;
                return;
            }
            else
            {
                lblMensajeExito.Text = result.MensajeExito;
                divMensajeExito.Visible = true;
                divMensajeError.Visible = false;
            }

            SolicitudAImprimir = solicitud;
            
            divPantallaABM.Visible = false;

        }

        public SolicitudCurso SolicitudAImprimir
        {
            get { return (SolicitudCurso) Session["SolicitudAImprimir"]; }
            set { Session["SolicitudAImprimir"] = value; }
        }

        private SolicitudCurso CargarDatosSolicitud()
        {
            var solicitud = new SolicitudCurso
            {
                IdDepartamento = ddlDepartamento.SelectedValue.ToString(),
                IdLocalidad = ddlLocalidad.SelectedValue,
                NombreCooperativa = txtNombreCooperativa.Text,
                SolicitantesCurso = ListaAsistentesCurso.ToList(),
                CuilUsuarioLogueado = UsuarioCidiLogueado.CUIL
            };

            /*Seteo datos en memoria*/
            NombreLocalidadSeleccionada = ddlLocalidad.SelectedItem== null ? "": ddlLocalidad.SelectedItem.Text;
            NombreDepartamentoSeleccionado = ddlDepartamento.SelectedItem == null ? "" : ddlDepartamento.SelectedItem.Text;
            return solicitud;

        }

        public string NombreLocalidadSeleccionada 
        {
            get
            {
                return (string) Session["NombreLocalidadSeleccionada"];  
            }
            set
            {
                Session["NombreLocalidadSeleccionada"]= value;
            }
        }

        public string NombreDepartamentoSeleccionado
        {
            get
            {
                return (string)Session["NombreDepartamentoSeleccionado"];
            }
            set
            {
                Session["NombreDepartamentoSeleccionado"] = value;
            }
        }


        protected void btnBuscarAsistente_OnClick(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtEmail.Text))
            //{
            //    lblMensajeErrorAgregarSolicitante.Text =
            //        "Debe indicar la dirección mail.";
            //    divMensajeErrorAgregarSolicitante.Visible = true;
            //    return;
            //}

            //if (string.IsNullOrEmpty(txtTelefonoFijoCodArea.Text) || string.IsNullOrEmpty(txtTelefonoFijo.Text))
            //{
            //    lblMensajeErrorAgregarSolicitante.Text =
            //        "Debe completar el Telefono Fijo.";
            //    divMensajeErrorAgregarSolicitante.Visible = true;
            //    return;
            //}

            //if (string.IsNullOrEmpty(txtCelularCodArea.Text) || string.IsNullOrEmpty(txtCelular.Text))
            //{
            //    lblMensajeErrorAgregarSolicitante.Text =
            //        "Debe completar el Telefono Celular.";
            //    divMensajeErrorAgregarSolicitante.Visible = true;
            //    return;
            //}

            var idSexo = ddlSexoAsistente.SelectedValue;
            var nroDoc = txtNroDocAsistente.Text.Trim();

            var solicitante = getPersonaUnica(nroDoc, idSexo, RolesAPIPersonas.CONSULTAR_DATOS_BASICOS);
            if (solicitante != null)
            {
                var lista = ListaAsistentesCurso;
                var asistente = new AsistenteCursoDto
                {
                    Dni = solicitante.NroDocumento,
                    Sexo = solicitante.Sexo.Nombre,
                    NombreCompletoPersona = solicitante.NombreCompleto,
                    idNumero = solicitante.Id_Numero.ToString(),
                    idSexo = solicitante.Sexo.IdSexo,
                    PaiCodPais = solicitante.PaisTD.IdPais,
                    Contacto = new Comunicacion
                    {
                        EMail = txtEmail.Text.Trim() ,
                        CodAreaTelFijo = txtTelefonoFijoCodArea.Text.Trim(),
                        NroTelfijo = txtTelefonoFijo.Text.Trim(),
                        CodAreaCel = txtCelularCodArea.Text.Trim(),
                        NroCel = txtCelular.Text.Trim()
                    }
                };
                lista.Add(asistente);
                ListaAsistentesCurso = lista;
                RefrescarGrillaAsistentes();
                LimpiarCamposAsistentes();
                txtNroDocAsistente.Focus();
            }
            else
            {
                lblMensajeErrorAgregarSolicitante.Text =
                    "No se encontraron datos en Reg. Civil para el Nro de Documento del Solicitante ingresado. Verifique y vuelva a intentarlo.";
                divMensajeErrorAgregarSolicitante.Visible = true;
                txtNroDocAsistente.Focus();
            }
        }

        private void LimpiarCamposAsistentes()
        {
            txtNroDocAsistente.Text = string.Empty;
            txtTelefonoFijo.Text = string.Empty;
            txtTelefonoFijoCodArea.Text = string.Empty;
            txtCelular.Text = string.Empty;
            txtCelularCodArea.Text = string.Empty;
            txtEmail.Text = string.Empty;
        }

        private void RefrescarGrillaAsistentes()
        {
            gvAsistentes.DataSource = ListaAsistentesCurso;
            gvAsistentes.DataBind();
        }


        private PersonaUnica getPersonaUnica(string nroDoc, string idSexo, RolesAPIPersonas rolesApiPersonas)
        {
            //ServicioComunicacion servicioComAppComunes = new ServicioComunicacion(new Configuracion() { AppId = Config.CiDiIdAplicacion.ToString(), AppKey = Config.CiDiKeyAplicacion, AppPass = Config.CiDiPassAplicacion, Entorno = master.EntornoDeEjecucion });
            //var hash = Request.Cookies["CiDi"].Value.ToString();
            //try
            //{
            //    PersonaFiltro personaFiltro = new PersonaFiltro();
            //    personaFiltro.NroDocumento = nroDoc;
            //    personaFiltro.Sexo = idSexo;
            //    personaFiltro.PaisTD = "ARG";
            //    var idEntidad = personaFiltro.ObtenerIdEntidad();

            //    return servicioComAppComunes.ApiPersonas(hash, personaFiltro, rolesApiPersonas);
            //}
            //catch (Exception)
            //{
            //    return null;
            //}
            var p = new PersonaUnica();

            var dt = Bl.BlGetPersonaUnica(nroDoc, idSexo);

            if (dt.Rows.Count > 0)
            {
                p.Nombre = dt.Rows[0]["Nombre"].ToString();
                p.Apellido = dt.Rows[0]["Apellido"].ToString();
                p.NroDocumento = dt.Rows[0]["Nro_Documento"].ToString();
                p.Id_Numero = int.Parse(dt.Rows[0]["ID_NUMERO"].ToString());
                p.CUIL = dt.Rows[0]["CUIL"].ToString();
                p.Sexo = new Sexo
                {
                    IdSexo = dt.Rows[0]["ID_SEXO"].ToString(),
                    Nombre = dt.Rows[0]["ID_SEXO"].ToString() == "01" ? "MASCULINO" : "FEMENINO"
                };
                p.FechaNacimiento = string.IsNullOrEmpty(dt.Rows[0]["FECHA_NACIMIENTO"].ToString())
                    ? (DateTime?) null
                    : DateTime.Parse(dt.Rows[0]["FECHA_NACIMIENTO"].ToString());
                p.TipoDocumento = new TipoDocumento
                {
                    Id = "1",
                    Nombre = "DNI"
                };
                p.PaisTD = new Pais
                {
                    IdPais = dt.Rows[0]["PAI_COD_PAIS"].ToString(),
                    Nombre = dt.Rows[0]["PAI_COD_PAIS"].ToString()
                };

                return p;
            }

            return null;
        }


        protected void gvAsistentes_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var asistenteCursoDto = (AsistenteCursoDto)e.Row.DataItem;

                var lblNroOrden = (Label) e.Row.FindControl("lblNroOrden");
                asistenteCursoDto.NroOrden = (e.Row.RowIndex + 1);
                lblNroOrden.Text = (e.Row.RowIndex + 1).ToString();
            }
        }

        protected void gvAsistentes_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var acciones = new List<string> {"QuitarAsistente"};
            if (!acciones.Contains(e.CommandName))
                return;

            gvAsistentes.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            EntidadSeleccionada = 0;

            if (gvAsistentes.SelectedValue != null)
                EntidadSeleccionada = int.Parse(gvAsistentes.SelectedValue.ToString());

            switch (e.CommandName)
            {
                case "QuitarAsistente":
                    QuitarAsistente(EntidadSeleccionada);
                    break;
            }
        }

        private void QuitarAsistente(int dni)
        {
            foreach (var asistenteCursoDto in ListaAsistentesCurso)
            {
                if (asistenteCursoDto.Dni == dni.ToString())
                {
                    var lista = ListaAsistentesCurso;
                    lista.Remove(asistenteCursoDto);
                    ListaAsistentesCurso = lista;
                    RefrescarGrillaAsistentes();
                    break;
                }
            }
        }




        private void ImprimirReporte(SolicitudCurso solicitudCurso)
        {
            var nombreReporteRdlc = "CyM.rptSolicitudCurso.rdlc";
              
            /*Creo y Cargo el Reporte*/
            var reporte = new ReporteGeneral(nombreReporteRdlc, TipoArchivoEnum.Pdf);

            /*Seteo los DataSources*/
            reporte.AddDataSource("dsAsistentesCurso", solicitudCurso.SolicitantesCurso);
             
            /*Agrego los parámetros al reporte*/
            reporte.AddParameter("pNroSolicitud", solicitudCurso.NroSolicitudCurso); 
            reporte.AddParameter("pNombreCoop", solicitudCurso.NombreCooperativa); 
            reporte.AddParameter("pNombreDpto",NombreDepartamentoSeleccionado );
            reporte.AddParameter("pNombreLocalidad",NombreLocalidadSeleccionada );
            

            Session["ReporteGeneral"] = reporte;
            Response.Redirect("ReporteGeneral.aspx");
        }

        protected void btnImprimirComprobante_OnClick(object sender, EventArgs e)
        {
            ImprimirReporte(SolicitudAImprimir);
        }
    }
}