using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BlCyM;
using CyM.Models;
using DataAccessCyM;
using DataAccessCyM.Dtos;
using DataAccessCyM.Entidades;

namespace CyM
{
    public partial class Asambleas : System.Web.UI.Page
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
                divCooperativaSeleccionada.Visible = false;
                divMensajeError_BuscarCooperativa.Visible = false;
                divMensajeErrorTemas.Visible = false;
                divMensajeErrorVeedor.Visible = false;
                CambiarEstado(EstadoAbmcEnum.CONSULTANDO);
                CargarTiposAsamblea();
                CargarComboTemas();
                CargarComboEstado();
                RefrescarGrilla();
            }
           
        }

        private void RefrescarGrilla()
        {
            List<AsambleaDto> lista = Bl.GetAsambleasByFilter(Entidad.TipoEntidadCooperativa, ddlFiltroEstado.SelectedValue,
                txtFiltroMatricula.Text, txtFiltroRegistro.Text, txtFiltroFechaDesde.Text, txtFiltroFechaHasta.Text);
            gvResultado.DataSource = lista;
            gvResultado.DataBind();
        }

        private void CargarTiposAsamblea()
        {


            List<TipoAsamblea> lista = Bl.GetTipoAsambleas().Where(x => x.IdTipoAsamblea == 1 || x.IdTipoAsamblea == 2 || x.IdTipoAsamblea == 3).ToList();

        ddlTipoAsamblea.DataSource = lista;
            ddlTipoAsamblea.DataValueField = "IdTipoAsamblea";
            ddlTipoAsamblea.DataTextField = "Nombre";
            ddlTipoAsamblea.DataBind();
            ddlTipoAsamblea.Items.Add(new ListItem("Seleccione..", "0"));
            ddlTipoAsamblea.SelectedValue = "0";

        }

        protected void btnNuevo_OnClick(object sender, EventArgs e)
        {
            CambiarEstado(EstadoAbmcEnum.REGISTRANDO);
        }

        protected void btnConsultar_OnClick(object sender, EventArgs e)
        {

            AccionConsultar();
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

                var asambleaDto = (AsambleaDto)e.Row.DataItem;

                //var lblEstado = (Label)e.Row.FindControl("lblEstado");
                //var btnActivarCooperativa = (Button)e.Row.FindControl("btnActivarCooperativa");


                //if (asambleaDto != null)
                //{
                //    lblEstado.Text = asambleaDto.Estado;
                //    switch (asambleaDto.Estado)
                //    {
                //        case "EN FORMACION":
                //            lblEstado.CssClass = "label label-info";
                //            btnActivarCooperativa.Visible = true;
                //            break;
                //        case "ACTIVA":
                //            lblEstado.CssClass = "label label-success";
                //            btnActivarCooperativa.Visible = false;
                //            break;
                //        case "DADO DE BAJA":
                //            lblEstado.CssClass = "label label-danger";
                //            btnActivarCooperativa.Visible = false;
                //            break;
                //        case "INACTIVO":
                //            lblEstado.CssClass = "label label-warning";
                //            btnActivarCooperativa.Visible = false;
                //            break;
                //    }
                //}


            }
        }

        protected void gvResultado_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var acciones = new List<string> { "Editar", "Eliminar", "Ver" };

            if (!acciones.Contains(e.CommandName))
                return;

            gvResultado.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            EntidadSeleccionada = 0;

            if (gvResultado.SelectedValue != null)
                EntidadSeleccionada = int.Parse(gvResultado.SelectedValue.ToString());

            switch (e.CommandName)
            {
                case "Ver":
                    //VerEntidad(EstadoAbmcEnum.VIENDO);
                    break;

            }
        }

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

         

        protected void ddlLocalidadLegal_OnSelectedIndexChanged(object sender, EventArgs e)
        {
           
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

        private void HabilitarDehabilitarCampos(bool valor)
        {
             
        }

        private void LimpiarCamporFormuario()
        {
             
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
                    lblTituloPantallaABM.Text = "Registrar Nueva Asamblea";
                    EstadoVista = EstadoAbmcEnum.REGISTRANDO;
                    HabilitarDehabilitarCampos(true);
                    LimpiarCamporFormuario();
                    divMensajeError.Visible = false;
                    divMensajeExito.Visible = false;
                    btnAceptar.Visible = true;
                    btnAceptar.Text = "Aceptar";
                    btnCancelar.Text = "Cancelar";
                    
                    txtFechaAsamblea.Enabled = false;

                    break;
                case EstadoAbmcEnum.EDITANDO:
                    divPantallaConsulta.Visible = false;
                    divPantallaABM.Visible = true;
                    lblTituloPantallaABM.Text = "Editar Asamblea";
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
                    EstadoVista = EstadoAbmcEnum.VIENDO;
                    lblTituloPantallaABM.Text = "Datos de la Asamblea";
                    divMensajeError.Visible = false;
                    divMensajeExito.Visible = false;
                    btnAceptar.Visible = false;
                    btnCancelar.Text = "Volver";

                    break;
                case EstadoAbmcEnum.ELIMINANDO:
                    divPantallaConsulta.Visible = false;
                    divPantallaABM.Visible = true;
                    EstadoVista = EstadoAbmcEnum.ELIMINANDO;
                    lblTituloPantallaABM.Text = "Eliminar Asamblea";
                    divMensajeError.Visible = false;
                    divMensajeExito.Visible = false;
                    btnAceptar.Visible = true;
                    btnAceptar.Text = "Eliminar";
                    btnCancelar.Text = "Cancelar";
                    break;
            }
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


        private void AccionGuardar()
        {

            ActualizarDocumentacionPresentadaPreAsamblea();
            ActualizarDocumentacionPresentadaPosAsamblea();

            Asamblea asamblea = CargarDatosAsamblea();
            ResultadoRule result = Bl.RegistrarAsamblea(CooperativaSeleccionada.id_entidad,Convert.ToInt32(ddlTipoAsamblea.SelectedValue),asamblea);

            if (result.OcurrioError)
            {
                lblMensajeError.Text = result.MensajeError;
                divMensajeError.Visible = true;
                divMensajeExito.Visible = false;

            }
            else
            {
                lblMensajeExito.Text = "Se guardó con éxito la Asamblea.";
                divMensajeExito.Visible = true;
                divMensajeError.Visible = false;

                CambiarEstado(EstadoAbmcEnum.CONSULTANDO);
            }


        }

        private Asamblea CargarDatosAsamblea()
        {
            var asamblea = new Asamblea();

            asamblea.DocumentosPosAsamblea = ListaDocumentosPosAsamblea;
            asamblea.DocumentosPreAsamblea = ListaDocumentosPreAsamblea;
            asamblea.Temario = ListaTemas;
            asamblea.Veedores = ListaVeedores;

            asamblea.Entidad = CooperativaSeleccionada;
            asamblea.FechaAsamblea = string.IsNullOrEmpty(txtFechaAsamblea.Text)
                ? new DateTime?()
                : DateTime.Parse(txtFechaAsamblea.Text);
            asamblea.FechaPreAsamblea = string.IsNullOrEmpty(txtFechaPreAsamblea.Text)
                ? new DateTime?()
                : DateTime.Parse(txtFechaPreAsamblea.Text);
            asamblea.FechaPosAsamblea = string.IsNullOrEmpty(txtFechaPosAsamblea.Text)
                ? new DateTime?()
                : DateTime.Parse(txtFechaPosAsamblea.Text);
            //asamblea.FechaConvocatoria = string.IsNullOrEmpty(txtFechaAsamblea.Text)
            //    ? new DateTime?()
            //    : DateTime.Parse(txtFechaPosAsamblea.Text);
            //asamblea.FechaTerceraPresentacion = string.IsNullOrEmpty(txtFechaAsamblea.Text)
            //    ? new DateTime?()
            //    : DateTime.Parse(txtFechaPosAsamblea.Text);

            asamblea.Ejercicio = txtEjecicio.Text;
            asamblea.Lugar = txtLugar.Text;


            return asamblea;
        }

        private int EntidadSeleccionada
        {
            get
            {
                return (int)Session["EntidadSeleccionada"];
            }
            set
            {
                Session["EntidadSeleccionada"] = value;
            }
        }
        
        

        private void AccionConsultar()
        {
            RefrescarGrilla();
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


        protected void btnCancelar_OnClick(object sender, EventArgs e)
        {
            CambiarEstado(EstadoAbmcEnum.CONSULTANDO);
            
        }

        protected void btnBuscarCooperativa_OnClick(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(txtBuscarCooperativa.Text))
            {
                divMensajeError_BuscarCooperativa.Visible = true;
                lblMensajeError_BuscarCooperativa.Text = "Debe ingresar un texto para buscar una cooperativa.";
                return;
            }

            

            switch (ddlBuscarPor.SelectedValue)
            {
                case "0":
                    divMensajeError_BuscarCooperativa.Visible = true;
                    lblMensajeError_BuscarCooperativa.Text = "Debe un filtro de búsque en 'Buscar por...' ";
                    return;
                case "matricula":
                    ListaCooperativasEncontradas = Bl.GetCooperativasDtoByFilters("",txtBuscarCooperativa.Text,"" , "1");
                    break;
                case "nro_rp":
                    ListaCooperativasEncontradas = Bl.GetCooperativasDtoByFilters("", "", txtBuscarCooperativa.Text, "1");
                    break;
                case "descripcion":
                    ListaCooperativasEncontradas = Bl.GetCooperativasDtoByFilters(txtBuscarCooperativa.Text, "", "", "1");
                    break;
            }
            
            if (ListaCooperativasEncontradas.Count == 0)
            {
                divMensajeError_BuscarCooperativa.Visible = true;
                lblMensajeError_BuscarCooperativa.Text = "No se encontró ninguna cooperativa activa con ese filtro de búsqueda.";
                return;
            }

            divBuscarCooperativa.Visible = false;
            divCooperativaSeleccionada.Visible = true;

            gvBuscarCooperativa.DataSource = ListaCooperativasEncontradas;
            gvBuscarCooperativa.DataBind();

            MostrarOcultarModal(modalBuscarCooperativa,true);
        }

        public List<CooperativaDto> ListaCooperativasEncontradas
        {
            get
            {
                return (List<CooperativaDto>)(Session["ListaCooperativasEncontradas"] ?? new List<CooperativaDto>());
            }
            set { Session["ListaCooperativasEncontradas"] = value; }
        }

        protected void btnSeleccionarOtraCooperativa_OnClick(object sender, EventArgs e)
        {
            divMensajeError_BuscarCooperativa.Visible = false;

            divBuscarCooperativa.Visible = true;
            divCooperativaSeleccionada.Visible = false;
        }
        private void CargarComboEstado()
        {
            var lis = Bl.GetEstadosEntidad();

            ddlFiltroEstado.DataSource = lis;
            ddlFiltroEstado.DataTextField = "NombreEstadoEntidad";
            ddlFiltroEstado.DataValueField = "IdEstadoEntidad";
            ddlFiltroEstado.DataBind();
            ddlFiltroEstado.Items.Add(new ListItem("Seleccionar...","0"));
            ddlFiltroEstado.SelectedValue = "0";
        }

        protected void ddlTipoAsamblea_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlTipoAsamblea.SelectedValue) && ddlTipoAsamblea.SelectedValue != "0")
            {
                txtFechaAsamblea.Enabled = true;

                var documentos = Bl.GetDocumentos(ddlTipoAsamblea.SelectedValue,Entidad.TipoEntidadCooperativa);
                
                ListaDocumentosPreAsamblea = documentos.Where(x => x.N_PRESENTACION == "PRE ASAMBLEA").ToList();
                hfCantDiasPresentacionPreAsamblea.Value = ListaDocumentosPreAsamblea[0].CANT_DIAS;

                ListaDocumentosPosAsamblea = documentos.Where(x => x.N_PRESENTACION == "POS ASAMBLEA").ToList();
                hfCantDiasPresentacionPosAsamblea.Value = ListaDocumentosPosAsamblea[0].CANT_DIAS;


                RefrescarGrillaDocPosAsamblea();
                RefrescarGrillaDocPreAsamblea();
            }
            else
            {
                txtFechaAsamblea.Text = "";
                txtFechaAsamblea.Enabled = false;
            }
        }

        private void RefrescarGrillaDocPreAsamblea()
        {
            gvDocPreAsamblea.DataSource = ListaDocumentosPreAsamblea;
            gvDocPreAsamblea.DataBind();
        }

        private void RefrescarGrillaDocPosAsamblea()
        {
            gvDocPosAsamblea.DataSource = ListaDocumentosPosAsamblea;
            gvDocPosAsamblea.DataBind();
        }

        public List<DocumentoDto> ListaDocumentosPreAsamblea
        {
            get
            {
                return (List<DocumentoDto>)(Session["ListaDocumentosPreAsamblea"] ?? new List<DocumentoDto>());
            }
            set { Session["ListaDocumentosPreAsamblea"] = value; }
        }
        public List<DocumentoDto> ListaDocumentosPosAsamblea
        {
            get
            {
                return (List<DocumentoDto>)(Session["ListaDocumentosPosAsamblea"] ?? new List<DocumentoDto>());
            }
            set { Session["ListaDocumentosPosAsamblea"] = value; }
        }

        protected void gvDocPreAsamblea_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                var documentoDto = (DocumentoDto)e.Row.DataItem;

                var rbObligatorio = (RadioButton)e.Row.FindControl("rbObligatorio");
                //var chkPresentada = (CheckBox)e.Row.FindControl("chkPresentada");


                if (documentoDto != null)
                {
                    rbObligatorio.Checked = documentoDto.OBLIGATORIO == "Y";
                }


            }
        }

        protected void gvDocPreAsamblea_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
             
        }

      

        /// <summary>
        /// Se actualiza en memoria la documentación presentada.
        /// </summary>
        void ActualizarDocumentacionPresentadaPreAsamblea()
        {
            var list = ListaDocumentosPreAsamblea;

            foreach (GridViewRow row in gvDocPreAsamblea.Rows)
            {
                //var doc = (DocumentoDto) row.DataItem;
                var chk = (HtmlInputCheckBox)row.FindControl("chkPre");
                var presentada = chk.Checked;
                var idDocumento = chk.Value;
                foreach (DocumentoDto docDto in list)
                {
                    if (docDto.ID_DOCUMENTO == int.Parse(idDocumento))
                    {
                        docDto.Presentada = presentada;
                        break;
                    }
                }

            }
            ListaDocumentosPreAsamblea = list;

        }
        
        /// <summary>
        /// Se actualiza en memoria la documentación presentada.
        /// </summary>
        void ActualizarDocumentacionPresentadaPosAsamblea()
        {
            var list = ListaDocumentosPosAsamblea;

            foreach (GridViewRow row in gvDocPosAsamblea.Rows)
            {
                //var doc = (DocumentoDto) row.DataItem;
                var chk = (HtmlInputCheckBox)row.FindControl("chkPre");
                var presentada = chk.Checked;
                var idDocumento = chk.Value;
                foreach (DocumentoDto docDto in list)
                {
                    if (docDto.ID_DOCUMENTO == int.Parse(idDocumento))
                    {
                        docDto.Presentada = presentada;
                        break;
                    }
                }

            }
            ListaDocumentosPosAsamblea = list;

        }
         
        protected void gvBuscarCooperativa_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                var cooperativaDto = (CooperativaDto)e.Row.DataItem;

                var lblEstado = (Label)e.Row.FindControl("lblEstado");


                if (cooperativaDto != null)
                {
                    lblEstado.Text = cooperativaDto.EstadoEntidad.NombreEstadoEntidad;
                    switch (cooperativaDto.EstadoEntidad.IdEstadoEntidad)
                    {
                        case 1://ACTIVA
                            lblEstado.CssClass = "label label-success";
                            break;
                        case 2://SUSPENDIDA
                            lblEstado.CssClass = "label label-warning";
                            break;
                        case 3://NO AUTORIZADO FUNC.
                            lblEstado.CssClass = "label label-danger";
                            break;
                        case 4://CANCELADA
                            lblEstado.CssClass = "label label-danger";
                            break;
                        case 5://EN FORMACION
                            lblEstado.CssClass = "label label-info";
                            break;
                    }
                }


            }
        }

        protected void gvBuscarCooperativa_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
             var acciones = new List<string> { "Seleccionar"};

            if (!acciones.Contains(e.CommandName))
                return;

            gvBuscarCooperativa.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            var idEntidadCooperativa = 0;

            if (gvBuscarCooperativa.SelectedValue != null)
                idEntidadCooperativa = int.Parse(gvBuscarCooperativa.SelectedValue.ToString());

            switch (e.CommandName)
            {
                case "Seleccionar":
                    SeleccionarCooperativa(idEntidadCooperativa);
                    break;
            }
        }

        private void SeleccionarCooperativa(int idEntidadCooperativa)
        {
            divMensajeError_BuscarCooperativa.Visible = false;

            CooperativaSeleccionada =
                ListaCooperativasEncontradas.Where(x => x.id_entidad == idEntidadCooperativa).ToList()[0];

            CooperativaSeleccionada = Bl.GetCooperativaDtoById(CooperativaSeleccionada.id_entidad);

            txtCoopeativaSeleccionada.Text = CooperativaSeleccionada.descripcion + " - Matricula :" + CooperativaSeleccionada.nro_matricula + ". - Nro RP : " + CooperativaSeleccionada.nro_rp;
            //cargar datos detalle Cooperativa en modal
            CargarDatosDetalleCooperativa();

            MostrarOcultarModal(modalBuscarCooperativa, false);

        }

        private void CargarDatosDetalleCooperativa()
        {
            if (CooperativaSeleccionada == null)
                return;
            txtDetalleCooperativaNombre.Text = CooperativaSeleccionada.descripcion;
            txtDetalleCooperativaMatricula.Text = CooperativaSeleccionada.nro_matricula;
            txtDetalleCooperativaNroRp.Text = CooperativaSeleccionada.nro_rp;
            if (CooperativaSeleccionada.FECHA_APROBACION != null)
                txtDetalleCooperativaFecAprob.Text = CooperativaSeleccionada.FECHA_APROBACION.Value.ToString();
            if (CooperativaSeleccionada.fecha_elevacion != null)
                txtDetalleCooperativaFecElevacion.Text = CooperativaSeleccionada.fecha_elevacion.Value.ToString();


            gvHistorial.DataSource = CooperativaSeleccionada.HistorialEstado;
            gvHistorial.DataBind();

            var domicilio = CooperativaSeleccionada.Domicilio;
            if (domicilio != null)
            {
                CargarDepartamentosLegal();
                ddlDetalleCooperativaDepartamentoLegal.SelectedValue = domicilio.IdDepartamento;

                CargarLocalidadesLegal();
                ddlDetalleCooperativaLocalidadLegal.SelectedValue = domicilio.IdLocalidad;

                var calle = "";
                if (!string.IsNullOrEmpty(domicilio.Barrio))
                {
                    calle += " Barrio : " + domicilio.Barrio;
                }
                if (!string.IsNullOrEmpty(domicilio.Calle))
                {
                    calle += " " + domicilio.Calle;
                }
                if (!string.IsNullOrEmpty(domicilio.Altura))
                {
                    calle += " " + domicilio.Altura;
                }

                txtDetalleCooperativaCalle.Text = calle;
                txtDetalleCooperativaCodPostal.Text = domicilio.CodigoPostal;
            }

            var contacto = CooperativaSeleccionada.Contacto;
            if (contacto != null)
            {
                txtDetalleCooperativaCelular.Text = contacto.CodAreaCel + " " + contacto.NroCel;
                txtDetalleCooperativaFijo.Text = contacto.CodAreaTelFijo + " " + contacto.NroTelfijo;
                txtDetalleCooperativaEmail.Text = contacto.EMail;
            }
             
        }

        private void CargarDepartamentosLegal()
        {
            ddlDetalleCooperativaDepartamentoLegal.DataSource = Bl.GetDepartamentos("X");
            ddlDetalleCooperativaDepartamentoLegal.DataTextField = "NombreDepartamento";
            ddlDetalleCooperativaDepartamentoLegal.DataValueField = "IdDepartamento";
            ddlDetalleCooperativaDepartamentoLegal.DataBind();
        }

        private void CargarLocalidadesLegal()
        {
            ddlDetalleCooperativaLocalidadLegal.DataSource = Bl.GetLocalidades(ddlDetalleCooperativaDepartamentoLegal.SelectedValue, "X");
            ddlDetalleCooperativaLocalidadLegal.DataTextField = "NombreLocalidad";
            ddlDetalleCooperativaLocalidadLegal.DataValueField = "IdLocalidad";
            ddlDetalleCooperativaLocalidadLegal.DataBind();
        }


        public CooperativaDto CooperativaSeleccionada
        {
            get
            {
                return (CooperativaDto)(Session["CooperativaSeleccionada"] ?? new CooperativaDto());
            }
            set { Session["CooperativaSeleccionada"] = value; }
        }

        private void MostrarOcultarModal(HtmlControl divModal,bool mostrar)
        {
            //diver.Visible = false;
            if (mostrar)
            {
                var classname = "mostrarModal";
                string[] listaStrings = divModal.Attributes["class"].Split(' ');
                var listaStr = String.Join(" ", listaStrings
                    .Except(new string[] { "", classname })
                    .Concat(new string[] { classname })
                    .ToArray()
                    );
                divModal.Attributes.Add("class", listaStr);
            }
            else
            {
                //oculta la Modal 
                divModal.Attributes.Add("class", String.Join(" ", divModal
                          .Attributes["class"]
                          .Split(' ')
                          .Except(new string[] { "", "mostrarModal" })
                          .ToArray()
                  ));
            }
        }


        protected void btnSalirModalBuscarCooperativa_OnClick(object sender, EventArgs e)
        {
            divBuscarCooperativa.Visible = true;
            divCooperativaSeleccionada.Visible = false;

            MostrarOcultarModal(modalBuscarCooperativa, false);
        }

        protected void btnSalirModalDetalleCooperativa_OnClick(object sender, EventArgs e)
        {
            MostrarOcultarModal(modalDetalleCooperativa, false);
        }

        protected void btnVerCooperativaSeleccionada_OnClick(object sender, EventArgs e)
        {
            MostrarOcultarModal(modalDetalleCooperativa,true);

        }

        protected void gvDocPosAsamblea_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                var documentoDto = (DocumentoDto)e.Row.DataItem;

                var rbObligatorio = (RadioButton)e.Row.FindControl("rbObligatorio");

                if (documentoDto != null)
                {
                    rbObligatorio.Checked = documentoDto.OBLIGATORIO == "Y";
                }


            }
        }

        protected void gvDocPosAsamblea_OnRowCommand(object sender, GridViewCommandEventArgs e)
        { 

        }
        private void CargarComboTemas()
        {
            var temas = Bl.GetTemas();
            ddlTema.DataSource = temas;
            ddlTema.DataTextField = "NombreTema";
            ddlTema.DataValueField = "IdTema";
            ddlTema.DataBind();
        }

        protected void btnAgregarTema_OnClick(object sender, EventArgs e)
        {
            divMensajeErrorTemas.Visible = false;

            foreach (Tema tema in ListaTemas)
            {
                if (tema.IdTema.ToString() == ddlTema.SelectedValue)
                {
                    lblMensajeErrorTemas.Text = "Ya está cargada la tema seleccionado.";
                    divMensajeErrorTemas.Visible = true;
                    return;
                }
            }

            List<Tema> temas = ListaTemas;

            var nvo = new Tema 
            {
                IdTema = int.Parse(ddlTema.SelectedValue),
                NombreTema = ddlTema.SelectedItem.Text,
                Observacion = txtObeservacionTema.Text
            };
            temas.Add(nvo);
            ListaTemas = temas;

            RefrescarGrillaTemas();

            txtObeservacionTema.Text = "";
            txtObeservacionTema.Focus();
        }

        protected void gvTemas_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvTemas_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var acciones = new List<string> { "QuitarTema" };

            if (!acciones.Contains(e.CommandName))
                return;

            gvTemas.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            var idTema = "";

            if (gvTemas.SelectedValue != null)
                idTema = gvTemas.SelectedValue.ToString();

            switch (e.CommandName)
            {

                case "QuitarTema":

                    foreach (var tema in ListaTemas)
                    {
                        if (tema.IdTema.ToString() == idTema)
                        {
                            ListaTemas.Remove(tema);
                            break;
                        }
                    }
                    RefrescarGrillaTemas();
                    break;
            }
        }

        public List<Tema> ListaTemas
        {
            get
            {
                return (List<Tema>)(Session["ListaTemas"] ?? new List<Tema>());
            }
            set { Session["ListaTemas"] = value; }
        }

        public List<Veedor> ListaVeedores
        {
            get
            {
                return (List<Veedor>)(Session["ListaVeedores"] ?? new List<Veedor>());
            }
            set { Session["ListaVeedores"] = value; }
        }


        private void RefrescarGrillaTemas()
        {
            gvTemas.DataSource = ListaTemas;
            gvTemas.DataBind();
            lblTotalRegistras_Temas.Text = ListaTemas.ToList().Count.ToString();

        }

        private bool banderaPrimeraCargaPagina = false;
        private string commandoBotonPaginaSeleccionado = "";

        private string commandoBotonPaginaSeleccionado_Temas= "";
        private bool banderaPrimeraCargaPagina_Temas = false;
        
        protected void btnNroPagina_Temas_OnClick(object sender, EventArgs e)
        {
            banderaPrimeraCargaPagina_Temas = true;

            var btn = (LinkButton)sender;
            //guardo el comando del boton de pagina seleccinoado
            commandoBotonPaginaSeleccionado_Temas = btn.CommandArgument;
        }

        protected void rptBotonesPaginacion_Temas_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                var btn = (LinkButton)e.Item.FindControl("btnNroPagina");
                if (btn.CommandArgument == "1" && banderaPrimeraCargaPagina_Temas == false)
                {
                    btn.BackColor = Color.Gainsboro;//pinto el boton.
                }
                if (btn.CommandArgument == commandoBotonPaginaSeleccionado_Temas)
                {
                    //por cada boton pregunto y encuentro el comando seleccionado q corresponde al boton selecionado.
                    btn.BackColor = Color.Gainsboro;//pinto el boton.
                }
                //los demas botones se cargan con el color de fondo blanco por defecto.
            }
        }

        protected void rptBotonesPaginacion_Temas_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int nroPagina = Convert.ToInt32(e.CommandArgument.ToString());
            gvTemas.PageIndex = nroPagina - 1;

            RefrescarGrillaTemas();
        }

        protected void btnBuscarUsuarioCidi_OnClick(object sender, EventArgs e)
        {
            var cuil = txtCuilUsuarioCidi.Text.Trim();

            var usuario = ObtenerUsuarioCuil(cuil);
            if (usuario == null)
            {
                lblMensajeErrorVeedor.Text =
                    "No se encontró la Persona en Ciudadano Digital para el CUIL ingresado. Verifique que el CUIL sea correcto sino de de Alta al Usuario en la plataforma CIDI en cidi.cba.gov.ar . ";
                divMensajeErrorVeedor.Visible = true;
            }
            else
            {
                var veedor = new Veedor
                {
                    cuil = usuario.CUIL,
                    NombreVeedor = usuario.NombreFormateado,
                    Celular = usuario.CelFormateado,
                    Mail = usuario.Email
                };
                var list = ListaVeedores;
                list.Add(veedor);
                ListaVeedores = list;

                RefrescarGrillaVeedores();
                txtCuilUsuarioCidi.Text = "";
                txtCuilUsuarioCidi.Focus();
            }
        }

        protected Usuario ObtenerUsuarioCuil(String cuil)
        {
            Usuario usu;
            string urlapi = WebConfigurationManager.AppSettings["CiDiUrl"].ToString();
            Entrada entrada = new Entrada();
            entrada.IdAplicacion = Config.CiDiIdAplicacion;
            entrada.Contrasenia = Config.CiDiPassAplicacion;
            entrada.HashCookie = Request.Cookies["CiDi"].Value.ToString();
            entrada.TimeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            entrada.TokenValue = Config.ObtenerToken_SHA1(entrada.TimeStamp);
            entrada.CUIL = cuil;


            usu = Config.LlamarWebAPI<Entrada, Usuario>(APICuenta.Usuario.Obtener_Usuario_Basicos_CUIL, entrada);

            if (usu.Respuesta.Resultado == Config.CiDi_OK)
            {
                return usu;

            }
            return null;

        }
 

        protected void gvVeedores_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvVeedores_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var acciones = new List<string> { "QuitarVeedor" };

            if (!acciones.Contains(e.CommandName))
                return;

            gvVeedores.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            var cuilVeedor = "";

            if (gvVeedores.SelectedValue != null)
                cuilVeedor = gvVeedores.SelectedValue.ToString();

            switch (e.CommandName)
            {

                case "QuitarVeedor":

                    foreach (var veedor in ListaVeedores)
                    {
                        if (veedor.cuil == cuilVeedor)
                        {
                            ListaVeedores.Remove(veedor);
                            break;
                        }
                    }
                    RefrescarGrillaVeedores();
                    break;
            }
        }

        private void RefrescarGrillaVeedores()
        {
            gvVeedores.DataSource = ListaVeedores;
            gvVeedores.DataBind();
            lblTotalRegistras_Veedor.Text = ListaVeedores.ToList().Count.ToString();

        }
    }
}