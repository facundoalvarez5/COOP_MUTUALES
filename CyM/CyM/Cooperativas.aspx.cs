using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppComunicacion;
using AppComunicacion.ApiModels;
using BlCyM;
using CyM;
using DataAccessCyM;
using DataAccessCyM.Dtos;
using DataAccessCyM.Entidades;


namespace CyM
{
    public partial class Cooperativas : System.Web.UI.Page
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
                divMensajeErrorSecciones.Visible = false;
                divMensajeErrorAutoridad.Visible = false;

                CargarComboEstado();

                CambiarEstado(EstadoAbmcEnum.CONSULTANDO);
                
            }
           
        }

        private void CargarComboEstado()
        {
            var lis =Bl.GetEstadosEntidad();
            ddlEstado.DataSource = lis;
            ddlEstado.DataTextField = "NombreEstadoEntidad";
            ddlEstado.DataValueField = "IdEstadoEntidad";
            ddlEstado.DataBind();
            ddlEstado.Items.Add(new ListItem("Seleccionar...", "0"));
            ddlEstado.SelectedValue = "0";

            ddlFiltroEstado.DataSource = lis;
            ddlFiltroEstado.DataTextField = "NombreEstadoEntidad";
            ddlFiltroEstado.DataValueField = "IdEstadoEntidad";
            ddlFiltroEstado.DataBind();
            ddlFiltroEstado.Items.Add(new ListItem("Seleccionar...", "0"));
            ddlFiltroEstado.SelectedValue = "0";
        }

        private EstadoAbmcEnum EstadoVista
        {
            get
            {
                return (EstadoAbmcEnum)Session["EstadoVista"];
            }
            set
            {
                Session["EstadoVista"] = value;
            }
        }

        private int EntidadSeleccionada
        {
            get
            {
                return (int) Session["EntidadSeleccionada"];
            }
            set
            {
                Session["EntidadSeleccionada"] = value;
            }
        }

        private int AutoridadSeleccionada
        {
            get
            {
                return (int)Session["AutoridadSeleccionada"];
            }
            set
            {
                Session["AutoridadSeleccionada"] = value;
            }
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
            ddlLocalidad.DataSource = Bl.GetLocalidades(ddlDepartamento.SelectedValue,"X");
            ddlLocalidad.DataTextField = "NombreLocalidad";
            ddlLocalidad.DataValueField = "IdLocalidad";
            ddlLocalidad.DataBind();
        }

        private void CargarDepartamentosLegal()
        {
            ddlDepartamentoLegal.DataSource = Bl.GetDepartamentos("X");
            ddlDepartamentoLegal.DataTextField = "NombreDepartamento";
            ddlDepartamentoLegal.DataValueField = "IdDepartamento";
            ddlDepartamentoLegal.DataBind();
        }

        private void CargarLocalidadesLegal()
        {
            ddlLocalidadLegal.DataSource = Bl.GetLocalidades(ddlDepartamentoLegal.SelectedValue,"X");
            ddlLocalidadLegal.DataTextField = "NombreLocalidad";
            ddlLocalidadLegal.DataValueField = "IdLocalidad";
            ddlLocalidadLegal.DataBind();
        }

        private void CargarBarrios()
        {
            ddlBarrioLegal.DataSource = Bl.GetBarrios(ddlLocalidadLegal.SelectedValue);
            ddlBarrioLegal.DataTextField = "NombreBarrio";
            ddlBarrioLegal.DataValueField = "IdBarrio";
            ddlBarrioLegal.DataBind();
        }
        private void CargarCalle()
        {
            ddlCalleLegal.DataSource = Bl.GetCalles("X", ddlDepartamentoLegal.SelectedValue, ddlLocalidadLegal.SelectedValue);
            ddlCalleLegal.DataTextField = "NombreCalle";
            ddlCalleLegal.DataValueField = "IdCalle";
            ddlCalleLegal.DataBind();
        }

        protected void btnNuevo_OnClick(object sender, EventArgs e)
        {
            CambiarEstado(EstadoAbmcEnum.REGISTRANDO);
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
            var lista = Bl.GetCooperativasDtoByFilters(txtFiltroDescripcion.Text, txtFiltroRegistro.Text, txtFiltroMatricula.Text, ddlFiltroEstado.SelectedValue);
            ListaCooperativas = lista;
            RefrescarGrilla();
        }

        public List<CooperativaDto> ListaCooperativas
        {
            get
            {
                return Session["ListaCooperativas"]==null ? new List<CooperativaDto>() : (List<CooperativaDto>)Session["ListaCooperativas"];
            }
            set
            {
                Session["ListaCooperativas"] = value;
            }
        }

        private void RefrescarGrilla()
        {
            var lista = ListaCooperativas;
            gvResultado.PagerSettings.Mode = PagerButtons.Numeric;

            if (lista.Count > 0)
            {
                gvResultado.PagerSettings.PageButtonCount = int.Parse(Math.Ceiling((double)(lista.Count / (double)gvResultado.PageSize)).ToString());
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                var cooperativaDto = (CooperativaDto)e.Row.DataItem;

                var lblEstado = (Label)e.Row.FindControl("lblEstado");
                var btnActivarCooperativa = (Button)e.Row.FindControl("btnActivarCooperativa");


                if (cooperativaDto != null)
                {
                    lblEstado.Text = cooperativaDto.EstadoEntidad.NombreEstadoEntidad;
                   
                    switch (cooperativaDto.EstadoEntidad.IdEstadoEntidad)
                    {
                        case 1://ACTIVA
                            lblEstado.CssClass = "label label-success";
                            btnActivarCooperativa.Visible = false;
                            break;
                        case 2://SUSPENDIDA
                            lblEstado.CssClass = "label label-warning";
                            btnActivarCooperativa.Visible = false;
                            break;
                        case 3://NO AUTORIZADO FUNC.
                             lblEstado.CssClass = "label label-danger";
                            btnActivarCooperativa.Visible = false;
                            break;
                        case 4://CANCELADA
                            lblEstado.CssClass = "label label-danger";
                            btnActivarCooperativa.Visible = false;
                            break;
                        case 5://EN FORMACION
                           lblEstado.CssClass = "label label-info";
                            btnActivarCooperativa.Visible = true;
                            break;
                    }
                }


            }
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
        private string commandoBotonPaginaSeleccionado_Secciones= "";
        private string commandoBotonPaginaSeleccionado_Expedientes= "";

        private bool banderaPrimeraCargaPagina = false;
        private bool banderaPrimeraCargaPagina_Secciones = false;
        private bool banderaPrimeraCargaPagina_Expedientes = false;
        protected void rptBotonesPaginacion_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                
                var btn = (LinkButton)e.Item.FindControl("btnNroPagina");
                if (btn.CommandArgument == "1" && banderaPrimeraCargaPagina == false)
                {
                    btn.BackColor = Color.Gainsboro;//pinto el boton.
                }
                if (btn.CommandArgument == commandoBotonPaginaSeleccionado)
                {
                    //por cada boton pregunto y encuentro el comando seleccionado q corresponde al boton selecionado.
                    btn.BackColor = Color.Gainsboro;//pinto el boton.
                }
                //los demas botones se cargan con el color de fondo blanco por defecto.
            }
        }
 

       
        
        protected void gvResultado_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var acciones = new List<string> { "Editar", "Eliminar", "Ver", "ActivarCooperativa" };

            if (!acciones.Contains(e.CommandName))
                return;

            gvResultado.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            EntidadSeleccionada = 0;

            if (gvResultado.SelectedValue != null)
                EntidadSeleccionada = int.Parse(gvResultado.SelectedValue.ToString());

            switch (e.CommandName)
            {
                case "ActivarCooperativa":
                    ActivarCooperativa();
                    break;
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

        private void ActivarCooperativa()
        {
            

            //Cambio el estado de la pantallas.
            divPantallaConsulta.Visible = false;
            divPantallaABM.Visible = false;
            divPantallaActivarCooperativa.Visible = true;

            lblTituloPantallaActivarCooperativa.Text = "Activar Cooperativa";
            EstadoVista = EstadoAbmcEnum.ACTIVANDO_COOPERATIVA;
            HabilitarDehabilitarCampos(true);
            LimpiarCamporFormuario();
            divMensajeError.Visible = false;
            divMensajeExito.Visible = false;
            btnAceptar.Visible = true;
            btnAceptar.Text = "Activar";
            btnCancelar.Text = "Cancelar";

            txtFechaAprobacion.Text = DateTime.Now.ToShortDateString();
            divBarrioNuevo.Visible = false;
            divCalleNueva.Visible = false;

            CargarDepartamentosLegal();

            CargarComboSecciones();
            CargarComboCargos();

            /*consulta la cooperativa en formación que se va a ACTIVAR (dar de alta)*/
            var cooperativa = Bl.GetCooperativaDtoById(EntidadSeleccionada);
            txtNombreCooperativaActiva.Text = cooperativa.descripcion;

            /*Limpio la Sessiones en Memoria utilizadas en la Activación.*/
            ListaSecciones = null;
            RefrescarGrillaSecciones();

            ListaExpedientesSuac = Bl.GetTramitesSuacByEntidad(cooperativa.id_entidad); ;
            RefrescarGrillaExpedientes();

            /*Inicializo los expedientes cargados para la Entidad seleccinoada*/


        }

        private void RefrescarGrillaExpedientes()
        {
            gvExpedientes.DataSource = ListaExpedientesSuac;
            gvExpedientes.DataBind();
        }

        private void CargarComboCargos()
        {
            var cargos = Bl.GetCargos();
            ddlCargo.DataSource = cargos;
            ddlCargo.DataTextField = "NombreCargo";
            ddlCargo.DataValueField = "IdCargo";
            ddlCargo.DataBind();
        }

        private void CargarComboSecciones()
        {
            var secciones = Bl.GetSecciones(Entidad.TipoEntidadCooperativa);
            ddlSeccion.DataSource =secciones;
            ddlSeccion.DataTextField = "NombreSeccion";
            ddlSeccion.DataValueField = "IdSeccion";
            ddlSeccion.DataBind();
        }

        private void EditarEntidad(EstadoAbmcEnum estado)
        {
            CambiarEstado(estado);
            var entidad = Bl.GetCooperativaDtoById(EntidadSeleccionada);
            CargarCamposFormulario(entidad);
            HabilitarDehabilitarCampos(true); 
        }

        private void EliminarEntidad(EstadoAbmcEnum estado)
        {
            CambiarEstado(estado);
            var entidad = Bl.GetCooperativaDtoById(EntidadSeleccionada);
            CargarCamposFormulario(entidad);
            HabilitarDehabilitarCampos(false);
        }

        private void VerEntidad(EstadoAbmcEnum estado)
        {
            CambiarEstado(estado);
            CooperativaDto entidad = Bl.GetCooperativaDtoById(EntidadSeleccionada);
            CargarCamposFormulario(entidad);
            HabilitarDehabilitarCampos(false);
            //busco el estado actual de la coperativa 
            var idEstadoActual = entidad.HistorialEstado.Where(x => x.FechaHasta.HasValue == false).ToList()[0].IdEstado;
            entidad.id_estado = int.Parse(idEstadoActual);
            switch (entidad.id_estado.Value)
            {
                case 1://ACTIVA
                    divPantallaActivarCooperativa.Visible = true;
                    HabilitarDehabilitarCamposActivarCooperativa(false);
                    
                    lblTituloPantallaActivarCooperativa.Text = "Detalle de la Cooperativa Activa";
                    divFooterPantallaABM.Visible = false;
                    divFooterPantallaActivarCooperativa.Visible = true;
                    divExpedienteEnFormacion.Visible = false;
                    break;
                 case 2://INACTIVA
                    break;
                 case 3://DADA DE BAJA
                    break;
                 case 4://EN TRAMITE
                    break;
                 case 5://EN FORMACION
                    divPantallaActivarCooperativa.Visible = false;
                    
                    divFooterPantallaABM.Visible = true;
                    divFooterPantallaActivarCooperativa.Visible = false;
                    divExpedienteEnFormacion.Visible = true;
                    break;
                 
            }
        }

        private void HabilitarDehabilitarCamposActivarCooperativa(bool valor)
        {
            txtMatricula.Enabled = valor;
            txtRegistroProv.Enabled = valor;
            txtFechaAprobacion.Enabled = valor;
            txtNombreCooperativaActiva.Enabled = valor;
            txtCuit.Enabled = valor;
            ddlDepartamentoLegal.Enabled = valor;
            ddlLocalidadLegal.Enabled = valor;
            ddlBarrioLegal.Enabled = valor;
            txtBarrioNuevo.Enabled = valor;
            ddlCalleLegal.Enabled = valor;
            txtCalleNueva.Enabled = valor;
            txtCodigoPostal.Enabled = valor;
            txtAltura.Enabled = valor;
            txtManzana.Enabled = valor;
            txtLote.Enabled = valor;
            txtPiso.Enabled = valor;
            txtDpto.Enabled = valor;
            txtTorre.Enabled = valor;
            txtCelularCodArea.Enabled = valor;
            txtCelular.Enabled = valor;
            txtTelFijoCodArea.Enabled = valor;
            txtTelFijo.Enabled = valor;
            txtEmail.Enabled = valor;
            
            ddlSeccion.Enabled = valor;
            txtMotivo.Enabled = valor;
            btnAgregarSeccion.Enabled = valor;
            if (!valor)
            {
                foreach (DataControlField column in gvSecciones.Columns)
                {
                    if (column.HeaderText == "Quitar")
                    {
                        column.Visible = false;
                        break;
                    }
                }
            }

            ddlCargo.Enabled = valor;
            txtNroDocAutoridad.Enabled = valor;
            ddlSexoAutoridad.Enabled = valor;
            btnBuscarAutoridad.Enabled = valor;

            if (!valor)
            {
                foreach (DataControlField column in gvAutoridades.Columns)
                {
                    if (column.HeaderText == "Quitar")
                    {
                        column.Visible = false;
                        break;
                    }
                }
            }
        }

        private void HabilitarDehabilitarCampos(bool valor)
        {
            ddlEstado.Enabled = valor;
            txtNroSolicitud.Enabled = valor;
            txtNroExpediente.Enabled = valor;
            ddlDepartamento.Enabled = valor;
            ddlLocalidad.Enabled = valor;
        }

        private void CargarCamposFormulario(Entidad entidad)
        {
            
            ddlEstado.SelectedValue = entidad.HistorialEstado.Where(x => x.FechaHasta.HasValue == false).ToList()[0].IdEstado;
            txtNombreCooperativa.Text = entidad.descripcion;
            txtNroSolicitud.Text = entidad.NRO_SOLICITUD_CURSO;

            if (ddlEstado.SelectedValue == "5") //EN_FORMACIÓN
            {
                SolicitudCursoDto solicitudDto = Bl.GetSolicitudCursoDtoById(txtNroSolicitud.Text);
                  /*Si la entidad en Fromación , no se creó todavía el domicilio legal.*/
                CargarDepartamentos();
                ddlDepartamento.SelectedValue = solicitudDto.LocalidadDto.IdDepartamento;
                CargarLocalidades();
                ddlLocalidad.SelectedValue = solicitudDto.idLocalidad;
            }

            //var domicilio = Bl.ConsultarDomicilio(entidad.id_dom_legal);

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
                    divPantallaActivarCooperativa.Visible = false;
                    EstadoVista = EstadoAbmcEnum.CONSULTANDO;
                    break;
                case EstadoAbmcEnum.REGISTRANDO:
                    divPantallaConsulta.Visible = false;
                    divPantallaABM.Visible = true;
                    divPantallaActivarCooperativa.Visible = false;
                    lblTituloPantallaABM.Text = "Registrar Nueva Cooperativa en Formación";
                    EstadoVista = EstadoAbmcEnum.REGISTRANDO;
                    HabilitarDehabilitarCampos(true);
                    LimpiarCamporFormuario();
                    divMensajeError.Visible = false;
                    divMensajeExito.Visible = false;
                    btnAceptar.Visible = true;
                    btnAceptar.Text = "Aceptar";
                    btnCancelar.Text = "Cancelar";
                    break;
                case EstadoAbmcEnum.EDITANDO:
                    divPantallaConsulta.Visible = false;
                    divPantallaABM.Visible = true;
                    divPantallaActivarCooperativa.Visible = false;
                    lblTituloPantallaABM.Text = "Editar Cooperativa";
                    EstadoVista = EstadoAbmcEnum.EDITANDO;
                    divMensajeError.Visible = false;
                    divMensajeExito.Visible = false;
                    btnAceptar.Visible = true;
                    btnAceptar.Text = "Guardar";
                    btnCancelar.Text = "Cancelar";
                    break;
                case EstadoAbmcEnum.VIENDO:
                    divPantallaConsulta.Visible = false;
                    divPantallaABM.Visible = true;
                    divPantallaActivarCooperativa.Visible = true;
                    EstadoVista = EstadoAbmcEnum.VIENDO;
                    lblTituloPantallaABM.Text = "Datos de la Cooperativa";
                    divMensajeError.Visible = false;
                    divMensajeExito.Visible = false;
                    btnAceptar.Visible = false;
                    btnCancelar.Text = "Volver";

                    btnAceptar2.Visible = false;
                    btnCancelar2.Text = "Volver";

                    break;
                case EstadoAbmcEnum.ELIMINANDO:
                    divPantallaConsulta.Visible = false;
                    divPantallaABM.Visible = true;
                    divPantallaActivarCooperativa.Visible = true;
                    EstadoVista = EstadoAbmcEnum.ELIMINANDO;
                    lblTituloPantallaABM.Text = "Eliminar Cooperativa";
                    divMensajeError.Visible = false;
                    divMensajeExito.Visible = false;
                    btnAceptar.Visible = true;
                    btnAceptar.Text = "Eliminar";
                    btnCancelar.Text = "Cancelar";
                    break;
            }
        }

        private void LimpiarCamporFormuario()
        {
            if (EstadoVista == EstadoAbmcEnum.ACTIVANDO_COOPERATIVA)
            {
                txtMatricula.Text = string.Empty;
                txtRegistroProv.Text = string.Empty;
                /*Domicilio*/

                ddlDepartamentoLegal.SelectedValue = "0";
                ddlLocalidadLegal.SelectedValue = "0";
                txtBarrioNuevo.Text = string.Empty;
                ddlBarrioLegal.SelectedValue = "0";
                ddlCalleLegal.SelectedValue = "0";
                txtCalleNueva.Text = string.Empty;

                chkMostrarBarrio.Checked = false;
                chkMostrarCalle.Checked = false;

                txtAltura.Text = string.Empty;
                txtCodigoPostal.Text = string.Empty;
                txtPiso.Text = string.Empty;
                txtDpto.Text = string.Empty;

                /*Contacto*/
                txtEmail.Text = string.Empty;
                txtTelFijo.Text = string.Empty;
                txtTelFijoCodArea.Text = string.Empty;
                txtCelularCodArea.Text = string.Empty;
                txtCelular.Text = string.Empty;

                return;
            }
            ddlEstado.SelectedValue = "5";

            CargarDepartamentos();
            ddlDepartamento.SelectedValue = "0";
            CargarLocalidades();
            ddlLocalidad.SelectedValue = "0";

            txtNroSolicitud.Text = "";
            txtNroExpediente.Text = "";
            txtFechaElevacion.Text = "";

            gvTramitesSuac.DataSource = new List<TramiteSUACDto>();
            gvTramitesSuac.DataBind();
        }

        private ResultadoRule GuardarContacto()
        {
             
            var entidad = EntidadSeleccionada; //idEntidad de la tabla T_ENTIDADES

            ResultadoRule resultado =
                Bl.BlRegistrarContacto(new Comunicacion()
                {
                    IdEntidad = entidad.ToString(),
                    CodAreaCel = txtCelularCodArea.Text,
                    CodAreaTelFijo = txtTelFijoCodArea.Text,
                    EMail = txtEmail.Text,
                    NroCel = txtCelular.Text,
                    NroTelfijo = txtTelFijo.Text,
                    Tabla_Origen = "CYM.T_ENTIDADES"
                });
            return resultado;
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
                case EstadoAbmcEnum.ACTIVANDO_COOPERATIVA:
                    AccionActivarCooperativa();
                    AccionConsultar();
                    break;
            }
        }

        private void AccionActivarCooperativa()
        {
            ResultadoRule result;
            /*actualizo las secciones*/
            foreach (Seccion seccion in ListaSecciones)
            {
                result = Bl.RegistrarSeccionEntidad(seccion,EntidadSeleccionada);
                if (result.OcurrioError)
                {
                    lblMensajeError.Text = "ERROR AL CARGAR SECCIÓN_ENTIDAD. " + result;
                    divMensajeError.Visible = true;
                    return;
                }
            } 

            /*actualizo las autoridades*/
            foreach (Autoridad autoridad in ListaAutoridades)
            {
                result = Bl.RegistrarAutoridadEntidad(autoridad, EntidadSeleccionada);
                if (result.OcurrioError)
                {
                    lblMensajeError.Text = "ERROR AL CARGAR AUTORIDAD_ENTIDAD. " + result;
                    divMensajeError.Visible = true;
                    return;
                }
            }

            result = GuardarContacto();
            if (result.OcurrioError)
            {
                lblMensajeError.Text = "ERROR AL CARGAR CONTACTO. " + result;
                divMensajeError.Visible = true;
                return;
            }

            int? idVin;
            var resultDomicilio = GuardarDomicilio(out idVin);
            if (resultDomicilio != "OK")
            {
                lblMensajeError.Text = "ERROR AL CARGAR DOMICILIO. " + resultDomicilio;
                divMensajeError.Visible = true;
                return;
            }

            /*consulta la cooperativa en formación que se va a ACTIVAR (dar de alta)*/
            var cooperativa = Bl.GetCooperativaDtoById(EntidadSeleccionada);
            cooperativa.nro_matricula = txtMatricula.Text;
            cooperativa.nro_rp = txtRegistroProv.Text;
            cooperativa.descripcion = txtNombreCooperativaActiva.Text;
            cooperativa.id_dom_legal = idVin;
            cooperativa.FECHA_APROBACION = DateTime.Now;
            cooperativa.id_estado = 1; //ACTIVA
            cooperativa.CUIL_USR_CIDI = master.UsuarioCidiLogueado.CUIL;

            Bl.ActivarCooperativa(cooperativa, out result);
            CambiarEstado(EstadoAbmcEnum.CONSULTANDO);
        }

        private string GuardarDomicilio(out int? idVin)
        {
            var barrio = "";
            
            if (chkMostrarBarrio.Checked)
            {
                barrio = txtBarrioNuevo.Text;
            }
            else
            {
                if (ddlBarrioLegal.SelectedItem != null)
                {
                    barrio = ddlBarrioLegal.SelectedItem.Text;
                }
            }
            
            var idBarrio = !chkMostrarBarrio.Checked
                ? ddlBarrioLegal.SelectedValue
                : null;
             
            var resultado = Bl.CargarDomicilio(EntidadSeleccionada.ToString(), "X", ddlDepartamentoLegal.SelectedValue,
                ddlLocalidadLegal.SelectedItem.Text, "", "calle", ddlCalleLegal.SelectedValue, txtCalleNueva.Text, idBarrio, barrio, "",
                txtAltura.Text, txtPiso.Text, txtDpto.Text, "", ddlLocalidadLegal.SelectedValue, txtCodigoPostal.Text, "", "", out idVin);
            return resultado;
        }


        private void AccionEditar()
        {
            
        }

        private void AccionEliminar()
        {
            var resultado = Bl.EliminarEntidad(EntidadSeleccionada);
            if (resultado)
            {
                lblMensajeExito.Text = "La cooperativa se eliminó correctamente.";
                divMensajeError.Visible = false;
                divMensajeExito.Visible = true;
                CambiarEstado(EstadoAbmcEnum.CONSULTANDO);
            }
            else
            {
                lblMensajeError.Text = "Ocurrió un error en base de datos al cargar el nuevo registro.";
                divMensajeError.Visible = true;
                divMensajeExito.Visible = false;
            }
        }

        private void AccionGuardar()
        {
            
            var cooperativaEnFormacion = CargarDatosCooperativaEnFormacion();
            ResultadoRule result = Bl.RegistrarCooperativaEnFormacion(cooperativaEnFormacion);

            if (result.OcurrioError)
            {
                lblMensajeError.Text = result.MensajeError;
                divMensajeError.Visible = true;
                divMensajeExito.Visible = false;

            }
            else
            {
                lblMensajeExito.Text = result.MensajeExito;
                divMensajeExito.Visible = true;
                divMensajeError.Visible = false;

                CambiarEstado(EstadoAbmcEnum.CONSULTANDO);
            }

            
        }

        private Entidad CargarDatosCooperativaEnFormacion()
        {
            var coop = new Entidad
            {
                descripcion = txtNombreCooperativa.Text,
                id_estado = 5 ,//EN FORMACION
                id_tipo_entidad = Entidad.TipoEntidadCooperativa,
                observacion = txtObservacion.Text,
                NRO_SOLICITUD_CURSO = txtNroSolicitud.Text,
                NRO_EXPEDIENTE_SAUC = txtNroExpediente.Text,
                CUIL_USR_CIDI = master.UsuarioCidiLogueado.CUIL ,
                fecha_elevacion = string.IsNullOrEmpty(txtFechaElevacion.Text) ? new DateTime?() : DateTime.Parse(txtFechaElevacion.Text)

            };

            List<TramiteSUACDto> expedientes = Bl.GetTramitesSuac(txtNroExpediente.Text);
            if (expedientes.Count > 0)
            {
                coop.ExpedientesSuac = expedientes; //debeería haber solo 1 expediente para la cooperativa EN FORMACIÓN. 
            }
            return coop;
        }

        protected void btnBuscarExpedienteSuac_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNroExpediente.Text))
            {
                divMensajeError.Visible = true;
                lblMensajeError.Text = "Debe ingresar un nro de expediente.";
                return;
            }
            string check = Bl.checkExpedienteSolicitud(txtNroExpediente.Text);
            if (check== "OK")
            {
                divMensajeError.Visible = false;
                ListaExpedientesSuacEnFormacion = Bl.GetTramitesSuac(txtNroExpediente.Text);
                gvTramitesSuac.DataSource = ListaExpedientesSuacEnFormacion;
                gvTramitesSuac.DataBind();
            }
            else
            {
                divMensajeError.Visible = true;
                lblMensajeError.Text = check;
                return;
            }

        }

        protected void btnBuscarSolicutd_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNroSolicitud.Text))
            {
                divMensajeError.Visible = true;
                lblMensajeError.Text = "Debe ingresar un nro de solicitud a buscar.";
                return;
            }

            SolicitudCursoDto solicitudDto = Bl.GetSolicitudCursoDtoById(txtNroSolicitud.Text);

            divMensajeError.Visible = false;

            if (solicitudDto == null)
            {
                divMensajeError.Visible = true;
                lblMensajeError.Text = "No se encontraron datos para el Nro de Solicitud solicitado.";
                ddlDepartamento.SelectedValue = "0";
                ddlLocalidad.SelectedValue = "0";
                txtNombreCooperativa.Text = "";
                txtNroSolicitud.Text = "";
                txtNroSolicitud.Focus();
                return;
            }
            txtNombreCooperativa.Text = solicitudDto.NombreCooperativa;
            CargarDepartamentos();
            ddlDepartamento.SelectedValue = solicitudDto.LocalidadDto.IdDepartamento;
            CargarLocalidades();
            ddlLocalidad.SelectedValue = solicitudDto.idLocalidad;


        }

        protected void ddlDepartamentoLegal_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CargarLocalidadesLegal();
        }

        protected void ddlLocalidadLegal_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CargarBarrios();
            CargarCalle();
        }

        protected void chkMostrarBarrio_OnCheckedChanged(object sender, EventArgs e)
        {
            if (!chkMostrarBarrio.Checked)
            {
                divBarrioNuevo.Visible = false;
                divComboBarrio.Visible = true;
            }
            else
            {
                divBarrioNuevo.Visible = true;
                divComboBarrio.Visible = false;
            }
        }

        protected void chkMostrarCalle_OnCheckedChanged(object sender, EventArgs e)
        {
            if (!chkMostrarCalle.Checked)
            {
                divCalleNueva.Visible = false;
                divComboCalle.Visible = true;
            }
            else
            {
                divCalleNueva.Visible = true;
                divComboCalle.Visible = false;
            } 
        }

        protected void gvTramitesSuac_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

              

            }
        }

        /// <summary>
        /// Lista de expedientes que solo se utilizan para mostrar los expedientes al momento de dar de alta una cooperativa En Formación.
        /// </summary>
        public List<TramiteSUACDto> ListaExpedientesSuacEnFormacion
        {
            get
            {
                return Session["ListaExpedientesSuac_EnFormacion"] == null
                    ? new List<TramiteSUACDto>()
                    : (List<TramiteSUACDto>) Session["ListaExpedientesSuac_EnFormacion"];
            }
            set
            {
                Session["ListaExpedientesSuac_EnFormacion"]= value;
            }
        }


        public List<TramiteSUACDto> ListaExpedientesSuac
        {
            get
            {
                return Session["ListaExpedientesSuac"] == null
                    ? new List<TramiteSUACDto>()
                    : (List<TramiteSUACDto>)Session["ListaExpedientesSuac"];
            }
            set
            {
                Session["ListaExpedientesSuac"] = value;
            }
        }


        protected void btnAgregarSeccion_OnClick(object sender, EventArgs e)
        {
            divMensajeErrorSecciones.Visible = false;

            foreach (Seccion seccion in ListaSecciones)
            {
                if (seccion.IdSeccion == ddlSeccion.SelectedValue)
                {
                    lblMensajeErrorSecciones.Text = "Ya está cargada la sección seleccionada.";
                    divMensajeErrorSecciones.Visible = true;
                    return;
                }
            }

            List<Seccion> secciones = ListaSecciones;

            var nuevaSeccion = new Seccion
            {
                IdSeccion = ddlSeccion.SelectedValue,
                NombreSeccion = ddlSeccion.SelectedItem.Text,
                IdTipoEntidad = Entidad.TipoEntidadCooperativa,
                Motivo = txtMotivo.Text
            };
            secciones.Add(nuevaSeccion);
            ListaSecciones = secciones;

            RefrescarGrillaSecciones();

            txtMotivo.Text = "";
            txtMotivo.Focus();
        }

        private void RefrescarGrillaSecciones()
        {
            gvSecciones.DataSource = ListaSecciones;
            gvSecciones.DataBind();
            lblTotalRegistras_Secciones.Text = ListaSecciones.ToList().Count.ToString();

        }

        public List<Seccion> ListaSecciones
        {
            get
            {
                return (List<Seccion>) (Session["ListaSecciones"] ?? new List<Seccion>()) ;
            }
            set { Session["ListaSecciones"] = value; }
        }

        public List<TramiteSUACDto> ListaExpedientes
        {
            get
            {
                return (List<TramiteSUACDto>)(Session["ListaExpedientes"] ?? new List<TramiteSUACDto>());
            }
            set { Session["ListaExpedientes"] = value; }
        }

        protected void gvSecciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
             
        }

        protected void gvSecciones_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var acciones = new List<string> { "QuitarSeccion" };

            if (!acciones.Contains(e.CommandName))
                return;

            gvSecciones.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            var idSeccion = "";

            if (gvSecciones.SelectedValue != null)
                idSeccion = gvSecciones.SelectedValue.ToString();

            switch (e.CommandName)
            {

                case "QuitarSeccion":

                    foreach (var seccion in ListaSecciones)
                    {
                        if (seccion.IdSeccion == idSeccion)
                        {
                            ListaSecciones.Remove(seccion);
                            break;
                        }
                    }
                    RefrescarGrillaSecciones();
                    break;
            }
        }

        private IList<Autoridad> ListaAutoridades
        {
            get
            {
                return Session["ListaAutoridades"] == null
                    ? new List<Autoridad>()
                    : (List<Autoridad>)Session["ListaAutoridades"];
            }
            set { Session["ListaAutoridades"] = value; }
        }

        protected void gvAutoridades_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var autoridad = (Autoridad)e.Row.DataItem;

                var lblNroOrden = (Label)e.Row.FindControl("lblNroOrden");
                autoridad.NroOrden = (e.Row.RowIndex + 1);
                lblNroOrden.Text = (e.Row.RowIndex + 1).ToString();
            }
        }

        protected void gvAutoridades_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var acciones = new List<string> { "QuitarAutoridad" };
            if (!acciones.Contains(e.CommandName))
                return;

            gvAutoridades.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            AutoridadSeleccionada = 0;

            if (gvAutoridades.SelectedValue != null)
                AutoridadSeleccionada = int.Parse(gvAutoridades.SelectedValue.ToString());

            switch (e.CommandName)
            {
                case "QuitarAutoridad":
                    QuitarAutoridad(AutoridadSeleccionada);
                    break;
            }
        }

        private void QuitarAutoridad(int dni)
        {
            foreach (var Autoridad in ListaAutoridades)
            {
                if (Autoridad.DNI == dni.ToString())
                {
                    var lista = ListaAutoridades;
                    lista.Remove(Autoridad);
                    ListaAutoridades = lista;
                    RefrescarGrillaAutoridades();
                    break;
                }
            }
        }

        private void RefrescarGrillaAutoridades()
        {
            gvAutoridades.DataSource = ListaAutoridades;
            gvAutoridades.DataBind();
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
                    ? (DateTime?)null
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

        protected void btnBuscarAutoridad_OnClick(object sender, EventArgs e)
        {
            foreach (Autoridad autoridad in ListaAutoridades)
            {
                if (autoridad.DNI == txtNroDocAutoridad.Text && autoridad.IdSexo==ddlSexoAutoridad.SelectedValue)
                {
                    lblMensajeErrorAutoridad.Text = "Ya está cargado el DNI de la autoridad ingresada.";
                    divMensajeErrorAutoridad.Visible = true;
                    return;
                }
            }
            var idSexo = ddlSexoAutoridad.SelectedValue;
            var nroDoc = txtNroDocAutoridad.Text.Trim();

            var Autoridad = getPersonaUnica(nroDoc, idSexo, RolesAPIPersonas.CONSULTAR_DATOS_BASICOS);
            if (Autoridad != null)
            {
                var lista = ListaAutoridades;
                var autoridad = new Autoridad
                {
                    DNI = Autoridad.NroDocumento,
                    IdSexo = Autoridad.Sexo.IdSexo,
                    Nombre = Autoridad.Nombre,
                    Apellido = Autoridad.Apellido,
                    IdNumero = Autoridad.Id_Numero.ToString(),
                    PaiCodPais = Autoridad.PaisTD.IdPais,
                    Cargo = new Cargo {IdCargo = ddlCargo.SelectedValue , NombreCargo =  ddlCargo.SelectedItem.Text}
                    
                };
                lista.Add(autoridad);
                ListaAutoridades = lista;
                RefrescarGrillaAutoridades();
                LimpiarCamposAutoridades();
                txtNroDocAutoridad.Focus();
            }
            else
            {
                lblMensajeErrorAutoridad.Text =
                    "No se encontraron datos cargados para el Nro de Documento de la Autoridad ingresada. Verifique y vuelva a intentarlo.";
                divMensajeErrorAutoridad.Visible = true;
                txtNroDocAutoridad.Focus();
            }
        }
        private void LimpiarCamposAutoridades()
        {
            txtNroDocAutoridad.Text = string.Empty;
            lblMensajeErrorAutoridad.Text = "";
            divMensajeErrorAutoridad.Visible = false;

        }

        protected void rptBotonesPaginacion_Secciones_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int nroPagina = Convert.ToInt32(e.CommandArgument.ToString());
            gvSecciones.PageIndex = nroPagina - 1;

            RefrescarGrillaSecciones();
        }

        protected void rptBotonesPaginacion_Secciones_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                var btn = (LinkButton)e.Item.FindControl("btnNroPagina");
                if (btn.CommandArgument == "1" && banderaPrimeraCargaPagina_Secciones == false)
                {
                    btn.BackColor = Color.Gainsboro;//pinto el boton.
                }
                if (btn.CommandArgument == commandoBotonPaginaSeleccionado_Secciones)
                {
                    //por cada boton pregunto y encuentro el comando seleccionado q corresponde al boton selecionado.
                    btn.BackColor = Color.Gainsboro;//pinto el boton.
                }
                //los demas botones se cargan con el color de fondo blanco por defecto.
            }
        }

        protected void btnNroPagina_Secciones_OnClick(object sender, EventArgs e)
        {
            banderaPrimeraCargaPagina_Secciones = true;

            var btn = (LinkButton)sender;
            //guardo el comando del boton de pagina seleccinoado
            commandoBotonPaginaSeleccionado_Secciones = btn.CommandArgument;
        }

        
         

        protected void btnNroPagina_Expedientes_OnClick(object sender, EventArgs e)
        {
            banderaPrimeraCargaPagina_Expedientes = true;

            var btn = (LinkButton)sender;
            //guardo el comando del boton de pagina seleccinoado
            commandoBotonPaginaSeleccionado_Expedientes = btn.CommandArgument;
            
        }

        protected void gvExpedientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

        protected void gvExpedientes_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var acciones = new List<string> { "QuitarVer" };

            if (!acciones.Contains(e.CommandName))
                return;

            gvSecciones.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            var idSeccion = "";

            if (gvSecciones.SelectedValue != null)
                idSeccion = gvSecciones.SelectedValue.ToString();

            switch (e.CommandName)
            {

                case "QuitarVer":

                    //foreach (var seccion in ListaSecciones)
                    //{
                    //    if (seccion.IdSeccion == idSeccion)
                    //    {
                    //        ListaSecciones.Remove(seccion);
                    //        break;
                    //    }
                    //}
                    //RefrescarGrillaSecciones();
                    break;
            }
        }

        protected void rptBotonesPaginacion_Expedientes_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int nroPagina = Convert.ToInt32(e.CommandArgument.ToString());
            gvExpedientes.PageIndex = nroPagina - 1;

            RefrescarGrillaExpedientes();
        }

        protected void rptBotonesPaginacion_Expedientes_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                var btn = (LinkButton)e.Item.FindControl("btnNroPagina");
                if (btn.CommandArgument == "1" && banderaPrimeraCargaPagina_Expedientes == false)
                {
                    btn.BackColor = Color.Gainsboro;//pinto el boton.
                }
                if (btn.CommandArgument == commandoBotonPaginaSeleccionado_Expedientes)
                {
                    //por cada boton pregunto y encuentro el comando seleccionado q corresponde al boton selecionado.
                    btn.BackColor = Color.Gainsboro;//pinto el boton.
                }
                //los demas botones se cargan con el color de fondo blanco por defecto.
            }
        }
    }
}