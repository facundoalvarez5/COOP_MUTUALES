using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using BlCyM;
using DataAccessCyM;
using DataAccessCyM.Dtos;


namespace CyM
{
    public partial class Curso : System.Web.UI.Page
    {
        public ReglaDeNegocios Bl = new ReglaDeNegocios();
        public Principal master;


        protected void Page_Load(object sender, EventArgs e)
        {
            master = (Principal)Page.Master;

            if (!Page.IsPostBack)
            {
                divMensajeError.Visible = false;
                divMensajeExito.Visible = false;
                divMensajeErrorSolicitudes.Visible = false;
                CambiarEstado(EstadoAbmcEnum.CONSULTANDO);
                CargarDepartamentos();
                lblseleccion.Text = "Seleccionar las solicitudes pendientes para realizar el curso.";
            }
        }

        private EstadoAbmcEnum EstadoVista
        {
            get { return (EstadoAbmcEnum)Session["EstadoVista"]; }
            set { Session["EstadoVista"] = value; }
        }

        private int EntidadSeleccionada
        {
            get { return (int)Session["EntidadSeleccionada"]; }
            set { Session["EntidadSeleccionada"] = value; }
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


        protected void btnNuevo_OnClick(object sender, EventArgs e)
        {
            CambiarEstado(EstadoAbmcEnum.REGISTRANDO); 
            var lista = Bl.GetSolicitudesCurso("", "", "").Where(x => x.Estado == "SIN_CURSO").ToList();
            ListaSolicitudes = lista;

            RefrescarGrillaSolicitudes();
        }

        private void RefrescarGrillaSolicitudes()
        {
            gvSolicitudes.DataSource = ListaSolicitudes;
            gvSolicitudes.DataBind();

        }

        public List<SolicitudCursoDto> ListaSolicitudes
        {
            get
            {
                return Session["ListaSolicitudes"] == null
                    ? new List<SolicitudCursoDto>()
                    : (List<SolicitudCursoDto>)Session["ListaSolicitudes"];
            }
            set
            {
                Session["ListaSolicitudes"] = value;
            }
        }

        


        protected void btnCancelar_OnClick(object sender, EventArgs e)
        {
            CambiarEstado(EstadoAbmcEnum.CONSULTANDO);

        }

        protected void btnConsultar_OnClick(object sender, EventArgs e)
        {
            AccionConsultar();
        }

        private void AccionConsultar()
        {
            ListaCursos = Bl.GetCursos();

            RefrescarGrilla();
        }

        public IList<DataAccessCyM.Entidades.Curso> ListaCursos
        {
            get
            {
                return Session["ListaCursos"] == null
                    ? new List<DataAccessCyM.Entidades.Curso>()
                    : (List<DataAccessCyM.Entidades.Curso>)Session["ListaCursos"];
            }
            set { Session["ListaCursos"] = value; }
        }

        private void RefrescarGrilla()
        {
            var lista = ListaCursos;
            gvResultado.PagerSettings.Mode = PagerButtons.Numeric;

            if (lista.Count > 0)
            {
                gvResultado.PagerSettings.PageButtonCount =
                    int.Parse(Math.Ceiling((double)(lista.Count / (double)gvResultado.PageSize)).ToString());
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

            var btn = (LinkButton)sender;
            //guardo el comando del boton de pagina seleccinoado
            commandoBotonPaginaSeleccionado = btn.CommandArgument;
        }

        private string commandoBotonPaginaSeleccionado = "";
        private bool banderaPrimeraCargaPagina = false;

        protected void rptBotonesPaginacion_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var btn = (LinkButton)e.Item.FindControl("btnNroPagina");
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
            var acciones = new List<string> { "Ver", "Editar", "Eliminar"};

            if (!acciones.Contains(e.CommandName))
                return;

            gvResultado.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            EntidadSeleccionada = 0;

            if (gvResultado.SelectedValue != null)
                EntidadSeleccionada = int.Parse(gvResultado.SelectedValue.ToString());

            switch (e.CommandName)
            {
                case "Ver":
                    VerEntidad(EstadoAbmcEnum.VIENDO);
                    break;
                case "Editar":
                    EditarEntidad(EstadoAbmcEnum.EDITANDO);
                    break;

                case "Eliminar":
                    EliminarEntidad(EstadoAbmcEnum.ELIMINANDO);
                    break;
                
            }
        }

        private void EditarEntidad(EstadoAbmcEnum estado)
        {
            CambiarEstado(estado);
            var curso = Bl.GetCursoById(EntidadSeleccionada);
            curso.IdCurso = EntidadSeleccionada;

            CargarCamposFormulario(curso);
            HabilitarDehabilitarCampos(true);

               
             
        }

        private void EliminarEntidad(EstadoAbmcEnum estado)
        {
            CambiarEstado(estado);
            var entidad = Bl.GetCursoById(EntidadSeleccionada);
            CargarCamposFormulario(entidad);
            HabilitarDehabilitarCampos(false);

        }

        private void VerEntidad(EstadoAbmcEnum estado)
        {
            CambiarEstado(estado);
            var entidad = Bl.GetCursoById(EntidadSeleccionada);
            if (entidad.Localidad == null)
            {
                divMensajeError.Visible = true;
                lblMensajeError.Text = "La solicitud no posee cursos asignados";
                CambiarEstado(EstadoAbmcEnum.CONSULTANDO);
                return;
            }
            CargarCamposFormulario(entidad);
            HabilitarDehabilitarCampos(false);
        }

        private void HabilitarDehabilitarCampos(bool valor)
        {
            txtFechaDictado.Enabled = valor;
            txtNombreCurso.Enabled = valor;
            ddlDepartamento.Enabled = valor;
            ddlLocalidad.Enabled = valor;
        }

        private void CargarCamposFormulario(DataAccessCyM.Entidades.Curso entidad)
        {
            txtNombreCurso.Text = entidad.n_curso;
            txtFechaDictado.Text = entidad.FechaDictado.ToString();
           

            CargarDepartamentos(); 
            ddlDepartamento.SelectedValue = entidad.Localidad.IdDepartamento;
            CargarLocalidades();
            ddlLocalidad.SelectedValue = entidad.Localidad.IdLocalidad;

            var lista = entidad.Soliciudes;

            var listaSinCurso = Bl.GetSolicitudesCurso("", "", "").Where(x => x.Estado == "SIN_CURSO").ToList();

            lista.AddRange(listaSinCurso);

            ListaSolicitudes = lista;

            RefrescarGrillaSolicitudes();
        }

        private void CambiarEstado(EstadoAbmcEnum estado)
        {
            switch (estado)
            {
                case EstadoAbmcEnum.CONSULTANDO:
                    divPantallaConsulta.Visible = true;
                    divPantallaABM.Visible = false;
                    EstadoVista = EstadoAbmcEnum.CONSULTANDO;
                    DivBtnImprimir.Visible = true;
                    break;
                case EstadoAbmcEnum.REGISTRANDO:
                    divPantallaConsulta.Visible = false;
                    divPantallaABM.Visible = true;
                    lblTituloPantallaABM.Text = "Registrar un Nuevo Curso";
                    EstadoVista = EstadoAbmcEnum.REGISTRANDO;
                    HabilitarDehabilitarCampos(true);
                    LimpiarCamporFormuario();
                    divMensajeError.Visible = false;
                    divMensajeExito.Visible = false;
                    btnAceptar.Visible = true;
                    btnAceptar.Text = "ACEPTAR";
                    btnCancelar.Text = "CANCELAR";
                    DivBtnImprimir.Visible = false;
                    break;
                case EstadoAbmcEnum.EDITANDO:
                    divPantallaConsulta.Visible = false;
                    divPantallaABM.Visible = true;
                    lblTituloPantallaABM.Text = "Editar Curso";
                    EstadoVista = EstadoAbmcEnum.EDITANDO;
                    divMensajeError.Visible = false;
                    divMensajeExito.Visible = false;
                    btnAceptar.Visible = true;
                    btnAceptar.Text = "ACEPTAR";
                    btnCancelar.Text = "CANCELAR";
                    DivBtnImprimir.Visible = false;
                    break;
                case EstadoAbmcEnum.VIENDO:
                    divPantallaConsulta.Visible = false;
                    divPantallaABM.Visible = true;
                    EstadoVista = EstadoAbmcEnum.VIENDO;
                    lblTituloPantallaABM.Text = "Datos del Curso";
                    divMensajeError.Visible = false;
                    divMensajeExito.Visible = false;
                    btnAceptar.Visible = false;
                    btnCancelar.Text = "VOLVER";
                    DivBtnImprimir.Visible = true;
                    break;
                case EstadoAbmcEnum.ELIMINANDO:
                    divPantallaConsulta.Visible = false;
                    divPantallaABM.Visible = true;
                    EstadoVista = EstadoAbmcEnum.ELIMINANDO;
                    lblTituloPantallaABM.Text = "Eliminar Curso";
                    divMensajeError.Visible = false;
                    divMensajeExito.Visible = false;
                    btnAceptar.Visible = true;
                    btnAceptar.Text = "ELIMINAR";
                    btnCancelar.Text = "CANCELAR";
                    DivBtnImprimir.Visible = false;
                    break;
            }
        }

        private void LimpiarCamporFormuario()
        {
            txtNombreCurso.Text = "";
            txtFechaDictado.Text = "";


            //CargarDepartamentos();
            ddlDepartamento.SelectedValue = "0";
            //CargarLocalidades();
            ddlLocalidad.SelectedValue = "0";
        }

        protected void btnAceptar_OnClick(object sender, EventArgs e)
        {
            switch (EstadoVista)
            {
                //case EstadoAbmcEnum.CONSULTANDO://nunca debe pasar por acá
                //    break;
                case EstadoAbmcEnum.VIENDO:
                    CambiarEstado(EstadoAbmcEnum.CONSULTANDO);
                    break;
                case EstadoAbmcEnum.REGISTRANDO:
                    AccionGuardar();
                    AccionConsultar();
                    break;
                case EstadoAbmcEnum.ELIMINANDO:
                    AccionEliminar();
                    AccionConsultar();
                    break;
                case EstadoAbmcEnum.EDITANDO:
                    AccionEditar();
                    AccionConsultar();
                    break;
            }
        }

        private void AccionEditar()
        {
            var curso = CargarDatosCurso();
            curso.IdCurso = EntidadSeleccionada;

            ResultadoRule result = Bl.ActualizarCurso(curso);
             
            if (result.OcurrioError)
            {
                lblMensajeError.Text = result.MensajeError;
                divMensajeError.Visible = true;
                divMensajeExito.Visible = false;
            }
            else
            {
                lblMensajeExito.Text = result.MensajeExito;
                CambiarEstado(EstadoAbmcEnum.CONSULTANDO);
                divMensajeExito.Visible = true;
                divMensajeError.Visible = false;
            }
        }

        private void AccionEliminar()
        {
            var curso = CargarDatosCurso();
            curso.IdCurso = EntidadSeleccionada;
            string result = Bl.EliminarCurso(curso.IdCurso);

            if (result!="OK")
            {
                lblMensajeError.Text = "El curso no fue eliminado porque no existe o porque tiene solicitudes asignadas";
                divMensajeError.Visible = true;
                divMensajeExito.Visible = false;
            }
            else
            {
                lblMensajeExito.Text = "Se Elimino el curso con éxito";
                CambiarEstado(EstadoAbmcEnum.CONSULTANDO);
                divMensajeExito.Visible = true;
                divMensajeError.Visible = false;

                ListaCursos = Bl.GetCursos();
                RefrescarGrilla();
            }
        }

        private void AccionGuardar()
        {
            var curso = CargarDatosCurso();
            ResultadoRule result = Bl.RegistrarCurso(curso);

            if (result.OcurrioError)
            {
                lblMensajeError.Text = result.MensajeError;
                divMensajeError.Visible = true;
                divMensajeExito.Visible = false;
            }
            else
            {
                lblMensajeExito.Text = result.MensajeExito;
                CambiarEstado(EstadoAbmcEnum.CONSULTANDO);
                divMensajeExito.Visible = true;
                divMensajeError.Visible = false;
            }

        }

        private DataAccessCyM.Entidades.Curso CargarDatosCurso()
        {

            //Obtengo las Solicitudes Seleccionadas
            var solicitudesSeleccionadas = new List<SolicitudCursoDto>();

            for (int i = 0; i < gvSolicitudes.Rows.Count; i++)
            {
                GridViewRow row = gvSolicitudes.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkSeleccionar")).Checked;

                if (isChecked)
                {
                    string nroSolicitud = gvSolicitudes.Rows[i].Cells[1].Text; //primer columna : NRO SOLICIUTUD
                    var solicitudAux = ListaSolicitudes.Where(x => x.NroSolicitudCurso == nroSolicitud).ToList()[0];
                    solicitudesSeleccionadas.Add(solicitudAux);
                }
            }

            var localidad = new LocalidadDto()
            {
                IdDepartamento = ddlDepartamento.SelectedValue,
                IdLocalidad = ddlLocalidad.SelectedValue,
                NombreLocalidad = ddlLocalidad.SelectedItem.Text
            };

            var curso = new DataAccessCyM.Entidades.Curso
            {
                Soliciudes = solicitudesSeleccionadas,
                //CantidadAsistentes = solicitudesSeleccionadas.Count,
                FechaDictado = string.IsNullOrEmpty(txtFechaDictado.Text) ? new DateTime?() : Convert.ToDateTime(txtFechaDictado.Text),
                Localidad = localidad,
                n_curso = txtNombreCurso.Text


            };

            return curso;
        }


        protected void gvSolicitudes_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (EstadoVista == EstadoAbmcEnum.VIENDO)
                    gvSolicitudes.Columns[0].Visible = false;

                
                if (EstadoVista == EstadoAbmcEnum.ELIMINANDO)
                    gvSolicitudes.Columns[0].Visible = false;

                

                if (EstadoVista == EstadoAbmcEnum.EDITANDO)
                {
                    gvSolicitudes.Columns[0].Visible = true;
                    var solitudDto = (SolicitudCursoDto)e.Row.DataItem;
                    var chkSeleccionar = (CheckBox)e.Row.FindControl("chkSeleccionar");
                    if (solitudDto != null)
                    {
                        
                        if (solitudDto.Estado == "CON_CURSO")
                        {
                            e.Row.BackColor =  Color.FromArgb(1,217,237,247)   ;
                            //R: 217 , G: 237 , B: 247 ,  A: 1
                            chkSeleccionar.Checked = true;
                        }
                        else
                        {
                            e.Row.BackColor = Color.White;
                            chkSeleccionar.Checked = false;
                        }
                    }
                    
                }
            }
        }

        protected void gvSolicitudes_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            
        }


        protected void btnLimpiarFiltros_OnClick(object sender, EventArgs e)
        {
            txtFiltroFechaDesde.Text = string.Empty;
            txtFiltroFechaHasta.Text = string.Empty;
        }

        private void ImprimirReporte(SolicitudCursoDto solicitudCursoDto)
        {
            var nombreReporteRdlc = "CyM.rptSolicitudCurso.rdlc";

            /*Creo y Cargo el Reporte*/
            var reporte = new ReporteGeneral(nombreReporteRdlc, TipoArchivoEnum.Pdf);

            /*Seteo los DataSources*/
            reporte.AddDataSource("dsAsistentesCurso", solicitudCursoDto.SolicitantesCurso);

            /*Agrego los parámetros al reporte*/
            reporte.AddParameter("pNroSolicitud", solicitudCursoDto.NroSolicitudCurso);
            reporte.AddParameter("pNombreCoop", solicitudCursoDto.NombreCooperativa);
            reporte.AddParameter("pNombreDpto", solicitudCursoDto.NombreDepartamento);
            reporte.AddParameter("pNombreLocalidad", solicitudCursoDto.NombreLocalidad);

            Session["ReporteGeneral"] = reporte;
            Response.Redirect("ReporteGeneral.aspx");
        }
    }
}