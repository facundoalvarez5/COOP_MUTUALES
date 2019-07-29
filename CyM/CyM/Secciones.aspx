<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Secciones.aspx.cs" Inherits="CyM.Secciones" %>
<%@ Register TagPrefix="Ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.1.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
    <h3 class="page-title">Básico de Cooperativas
    </h3>
    <div class="page-bar">
        <ul class="page-breadcrumb">
            <li>
                <i class="fa fa-home"></i>
                <a href="#">Cooperativas</a>
                <i class="fa fa-angle-right"></i>
            </li>
            <li>
                <a href="#">Básico de Cooperativas</a>
                <i class="fa fa-angle-right"></i>
            </li>

        </ul>

    </div>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ConenedorPrincipal">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnableScriptGlobalization="True"
        EnableScriptLocalization="True">
    </asp:ScriptManager>
    <div id="divMensajeError" runat="server" class="alert alert-danger alert-dismissable">
        <button type="button" class="close" data-dismiss="alert" aria-hidden="true"></button>
        <strong>Error! </strong>
        <asp:Label runat="server" ID="lblMensajeError"></asp:Label>
    </div>
    <div id="divMensajeExito" runat="server" class="alert alert-success alert-dismissable">
        <button type="button" class="close" data-dismiss="alert" aria-hidden="true"></button>
        <strong>Exito! </strong>
        <asp:Label runat="server" ID="lblMensajeExito"></asp:Label>
    </div>
    <%--PANTALLA CONSULTA--%>
    <div id="divPantallaConsulta" runat="server">
        <div class="portlet light bordered">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-equalizer font-red-sunglo"></i>
                    <span class="caption-subject font-red-sunglo bold uppercase">Filtro de Consulta</span>
                    <span class="caption-helper">Múltiples filtros</span>
                </div>
                <div class="actions">
                    <asp:Button runat="server" ID="btnNuevo" CssClass="btn green form-control" Text="+ Crear Coooperativa En Formación" OnClick="btnNuevo_OnClick" />
                </div>
            </div>
            <div class="portlet-body form">
                <!-- BEGIN FORM-->
                <div class="form-body">
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Registro</label>
                                <asp:TextBox runat="server" ID="txtFiltroRegistro" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Matrícula</label>
                                <asp:TextBox ID="txtFiltroMatricula" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Estado</label>
                                <asp:DropDownList ID="ddlFiltroEstado" CssClass="form-control select2me" runat="server">
                                    <asp:ListItem Value="0" Enabled="true" Selected="True" Text="Seleccione..."></asp:ListItem>
                                    <asp:ListItem Text="ACTIVA" Value="1" />
                                    <asp:ListItem Text="INACTIVA" Value="2" />
                                    <asp:ListItem Text="DADA DE BAJA" Value="3" />
                                    <asp:ListItem Text="EN TRÁMITE" Value="4" />
                                    <asp:ListItem Text="EN FORMACIÓN" Value="5" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Descripción</label>
                                <asp:TextBox ID="txtFiltroDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="form-actions">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnConsultar" CssClass="btn default form-control" Text="Consultar" OnClick="btnConsultar_OnClick" />
                        </div>
                        <div class="col-md-2">
                            <button id="btnLimpiarFiltros" class="btn default form-control">Limpiar Campos</button>
                        </div>
                    </div>
                </div>
                <!-- END FORM-->
            </div>
        </div>

        <%--RESULTADO DE CONSULTA--%>
        <div class="portlet light bordered">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-equalizer font-red-sunglo"></i>
                    <span class="caption-subject font-red-sunglo bold uppercase">Resultado de Consulta</span>

                </div>

            </div>
            <div class="portlet-body form">
                <!-- BEGIN FORM-->
                <div class="form-body">
                    <div class="row">
                        <div class="col-md-12" style="overflow: scroll;">
                            <asp:GridView ID="gvResultado" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" DataKeyNames="id_entidad"
                                AllowPaging="true" PageSize="10" OnPageIndexChanging="gvResultado_PageIndexChanging" OnRowDataBound="gvResultado_RowDataBound" OnRowCommand="gvResultado_OnRowCommand">
                                <Columns>
                                    <asp:BoundField DataField="id_entidad" HeaderText="Codigo" />
                                    <%--<asp:BoundField DataField="Estado" HeaderText="Estado" />--%>
                                    <asp:TemplateField HeaderText="Estado">
                                        <ItemStyle CssClass="grilla-columna-accion" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblEstado" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="descripcion" HeaderText="Cooperativa" />
                                    <asp:BoundField DataField="cuit" HeaderText="CUIT" />
                                    <asp:BoundField DataField="nro_rp" HeaderText="Nro RP" />
                                    <asp:BoundField DataField="nro_matricula" HeaderText="Matrícula" />
                                    <asp:BoundField DataField="NRO_SOLICITUD_CURSO" HeaderText="Solicitud Curso" />
                                    <asp:BoundField DataField="Fecha_Ult_Estado" HeaderText="Fecha Ultimo Estado" />

                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemStyle CssClass="grilla-columna-accion" />
                                        <ItemTemplate>
                                            <asp:Button ID="btnActivarCooperativa" runat="server" CssClass="btnActivarCooperativa" CommandArgument="<%# Container.DataItemIndex %>"
                                                CommandName="ActivarCooperativa" Text="Activar Cooperativa" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ver">
                                        <ItemStyle CssClass="grilla-columna-accion" />
                                        <ItemTemplate>
                                            <asp:Button ID="btnVer" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                CommandName="Ver" CssClass="botonVer" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Editar">
                                        <ItemStyle CssClass="grilla-columna-accion" />
                                        <ItemTemplate>
                                            <asp:Button ID="btnEditar" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                CommandName="Editar" CssClass="botonEditar"></asp:Button>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Eliminar">
                                        <ItemStyle CssClass="grilla-columna-accion" />
                                        <ItemTemplate>
                                            <asp:Button ID="btnEliminar" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                CommandName="Eliminar" CssClass="botonEliminar" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>

                            </asp:GridView>

                            <div class="row">
                                <div class="col-md-12">
                                    <h6>Total de registros encontrados :
                                        <asp:Label runat="server" ID="lblTotalRegistrosGrilla" Text="0"></asp:Label></h6>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">

                                    <ul class="pagination pull-right">
                                        <li>
                                            <h6>Páginas :</h6>
                                        </li>
                                        <asp:Repeater ID="rptBotonesPaginacion" OnItemDataBound="rptBotonesPaginacion_OnItemDataBound" OnItemCommand="rptBotonesPaginacion_OnItemCommand" runat="server">
                                            <ItemTemplate>
                                                <li class="paginate_button">
                                                    <asp:LinkButton ID="btnNroPagina" OnClick="btnNroPagina_OnClick" CommandArgument='<%# Bind("nroPagina") %>' runat="server" class="btn btn-default "><%# Eval("nroPagina")%></asp:LinkButton>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>
                            </div>
                            <br />

                        </div>
                    </div>


                </div>
                <div class="form-actions">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnImprimir" CssClass="btn green form-control" Text="Imprimir" />
                        </div>
                    </div>
                </div>
                <!-- END FORM-->
            </div>
        </div>
    </div>

    <div id="divPantallaABM" runat="server">
        <div class="portlet light bordered">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-equalizer font-red-sunglo"></i>
                    <span class="caption-subject font-red-sunglo bold uppercase">
                        <asp:Label runat="server" ID="lblTituloPantallaABM" Text=""></asp:Label></span>
                </div>
            </div>
            <div class="portlet-body form">
                <!-- BEGIN FORM-->
                <div class="form-body">
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Nro Solicitud Curso : </label>
                                <asp:TextBox ID="txtNroSolicitud" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label style="color: white;">.</label>
                                <asp:Button Text="Buscar Solicitud" ID="btnBuscarSolicutd" OnClick="btnBuscarSolicutd_OnClick" CssClass="form-control btn blue" runat="server" />
                            </div>
                        </div>
                    </div>
                    <h4 class="subtituloForm">Datos de la Solicitud</h4>
                    <div class="row">
                        <div class="col-md-8">
                            <div class="form-group">
                                <label>Nombre <small>Cooperativa en Formación </small></label>
                                <asp:TextBox ID="txtNombreCooperativa" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Estado</label>
                                <asp:DropDownList Enabled="False" ID="ddlEstado" CssClass="form-control" runat="server">
                                    <asp:ListItem Selected="True" Text="EN FORMACIÓN" Value="5" />
                                    <asp:ListItem Text="ACTIVA" Value="1" />
                                    <asp:ListItem Text="INACTIVA" Value="2" />
                                    <asp:ListItem Text="DADA DE BAJA" Value="3" />
                                    <asp:ListItem Text="EN TRÁMITE" Value="4" />
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Departamento</label>
                                <asp:DropDownList ID="ddlDepartamento" Enabled="False" CssClass="form-control  " runat="server" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Localidad</label>
                                <asp:DropDownList ID="ddlLocalidad" CssClass="form-control  " Enabled="False" runat="server">
                                </asp:DropDownList>
                            </div>
                            <span class="help-block">Ubicación de la Cooperativa</span>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Nro Expediente SUAC : </label>
                                <asp:TextBox ID="txtNroExpediente" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label style="color: white;">.</label>
                                <asp:Button Text="Buscar Expediente" ID="btnBuscarExpedienteSuac" OnClick="btnBuscarExpedienteSuac_OnClick" CssClass="form-control btn blue" runat="server" />
                            </div>
                        </div>
                    </div>
                    <h4 class="subtituloForm">Datos del Expediente</h4>
                    <div class="row">
                        <div class="col-md-12" style="overflow: scroll;">
                            <asp:GridView ID="gvTramitesSuac" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" DataKeyNames="ID_TRAMITE" OnRowDataBound="gvTramitesSuac_OnRowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="ID_TRAMITE" HeaderText="ID_TRAMITE" />
                                    <asp:BoundField DataField="NRO_TRAMITE" HeaderText="NRO_TRAMITE" />
                                    <asp:BoundField DataField="NRO_STICKER" HeaderText="NRO_STICKER" />
                                    <asp:BoundField DataField="ASUNTO" HeaderText="ASUNTO" HeaderStyle-Width="100px" ItemStyle-Width="100px" FooterStyle-Width="100px" />
                                    <asp:BoundField DataField="NOMBRE_INICIADOR" HeaderText="NOMBRE_INICIADOR" />
                                    <asp:BoundField DataField="NRO_DOCUMENTO" HeaderText="NRO_DOCUMENTO" />
                                    <asp:BoundField DataField="FECHA_CREACION" HeaderText="FECHA_CREACION" />
                                    <asp:BoundField DataField="TIPO" HeaderText="TIPO" />
                                    <asp:BoundField DataField="SUBTIPO" HeaderText="SUBTIPO" />
                                    <asp:BoundField DataField="UNIDAD_ACTUAL" HeaderText="UNIDAD_ACTUAL" />
                                    <asp:BoundField DataField="UNIDAD_PROXIMA" HeaderText="UNIDAD_PROXIMA" />
                                    <asp:BoundField DataField="FECHA_UNIDAD" HeaderText="FECHA_UNIDAD" />
                                    <asp:BoundField DataField="N_ESTADO" HeaderText="N_ESTADO" />


                                </Columns>

                            </asp:GridView>

                            <div class="row">
                                <div class="col-md-12">
                                    <h6>Total de registros encontrados :
                                        <asp:Label runat="server" ID="Label1" Text="0"></asp:Label></h6>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">

                                    <ul class="pagination pull-right">
                                        <li>
                                            <h6>Páginas :</h6>
                                        </li>
                                        <asp:Repeater ID="Repeater1" OnItemDataBound="rptBotonesPaginacion_OnItemDataBound" OnItemCommand="rptBotonesPaginacion_OnItemCommand" runat="server">
                                            <ItemTemplate>
                                                <li class="paginate_button">
                                                    <asp:LinkButton ID="btnNroPagina" OnClick="btnNroPagina_OnClick" CommandArgument='<%# Bind("nroPagina") %>' runat="server" class="btn btn-default "><%# Eval("nroPagina")%></asp:LinkButton>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>
                            </div>
                            <br />

                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Fecha Elevación :<small> (De envío al INAES)</small> </label>
                                <asp:TextBox ID="txtFechaElevacion" runat="server" CssClass="form-control date-picker"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Observación (Opcional): </label>
                                <asp:TextBox ID="txtObservacion" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-actions">
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Button runat="server" ID="btnAceptar2" Text="Aceptar" CssClass="btn green form-control" OnClick="btnAceptar_OnClick" />
                            </div>
                            <div class="col-md-2">
                                <asp:Button runat="server" ID="btnCancelar2" Text="Cancelar" CssClass="btn default form-control" OnClick="btnCancelar_OnClick" />
                            </div>
                        </div>
                    </div>
                    <!-- END FORM-->
                </div>
            </div>
        </div>
    </div>


    <div id="divPantallaActivarCooperativa" runat="server">
        <div class="portlet light bordered">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-equalizer font-red-sunglo"></i>
                    <span class="caption-subject font-red-sunglo bold uppercase">
                        <asp:Label runat="server" ID="lblTituloPantallaActivarCooperativa" Text=""></asp:Label></span>
                </div>
            </div>
            <div class="portlet-body form">
                <!-- BEGIN FORM-->
                <div class="form-body">

                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>N° de Matrícula (*) :</label>
                                <asp:TextBox ID="txtMatricula" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>N° Registro Provincial (*) :</label>
                                <asp:TextBox ID="txtRegistroProv" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Fecha Aprobación : </label>
                                    <asp:TextBox ID="txtFechaAprobacion" runat="server" CssClass="form-control date-picker"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Nombre de la Cooperativa :</label>
                                <asp:TextBox ID="txtNombreCooperativaActiva" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>CUIT :</label>
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                    </div>

                    <div class="tabbable-custom">
                        <ul class="nav nav-tabs ">
                            <li class="active">
                                <a href="#tab_15_1" data-toggle="tab">Domicilio Legal y Contacto </a>
                            </li>
                            <li>
                                <a href="#tab_15_2" data-toggle="tab">Expedientes SUAC</a>
                            </li>
                            <li>
                                <a href="#tab_15_3" data-toggle="tab">Secciones </a>
                            </li>
                            <li>
                                <a href="#tab_15_4" data-toggle="tab">Autoridades </a>
                            </li>
                        </ul>
                        <div class="tab-content">
                            <div class="tab-pane active" id="tab_15_1">

                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Departamento</label>
                                            <asp:DropDownList ID="ddlDepartamentoLegal" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartamentoLegal_OnSelectedIndexChanged" CssClass="form-control select2me" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Localidad</label>
                                            <asp:DropDownList ID="ddlLocalidadLegal" CssClass="form-control select2me" AutoPostBack="True" OnSelectedIndexChanged="ddlLocalidadLegal_OnSelectedIndexChanged" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div id="divComboBarrio" runat="server" class="col-md-3">
                                        <div class="form-group">
                                            <label>Barrio</label>
                                            <asp:DropDownList ID="ddlBarrioLegal" CssClass="form-control select2me" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div id="divBarrioNuevo" runat="server" class="col-md-3">
                                        <div class="form-group">
                                            <label>Barrio</label>
                                            <asp:TextBox runat="server" ID="txtBarrioNuevo" placeholder="Ingrese nombre del Barrio" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Si mi Barrio no aparece :</label>
                                            <asp:CheckBox runat="server" ID="chkMostrarBarrio" AutoPostBack="True" OnCheckedChanged="chkMostrarBarrio_OnCheckedChanged" CssClass="form-control" BorderStyle="None" Text="Cargar nuevo Barrio." />
                                        </div>
                                    </div>
                                    <div id="divComboCalle" runat="server" class="col-md-3">
                                        <div class="form-group">
                                            <label>Calle</label>
                                            <asp:DropDownList ID="ddlCalleLegal" CssClass="form-control select2me" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div id="divCalleNueva" runat="server" class="col-md-3">
                                        <div class="form-group">
                                            <label>Calle</label>
                                            <asp:TextBox runat="server" ID="txtCalleNueva" placeholder="Ingrese nombre de la Calle" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Si mi Calle no aparece :</label>
                                            <asp:CheckBox runat="server" ID="chkMostrarCalle" AutoPostBack="True" OnCheckedChanged="chkMostrarCalle_OnCheckedChanged" CssClass="form-control" BorderStyle="None" Text="Cargar nueva Calle." />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Cód Postal</label>
                                            <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Altura</label>
                                            <asp:TextBox ID="txtAltura" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Mza.</label>
                                            <asp:TextBox ID="txtManzana" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Lote</label>
                                            <asp:TextBox ID="txtLote" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Piso</label>
                                            <asp:TextBox ID="txtPiso" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <label>Dpto</label>
                                            <asp:TextBox ID="txtDpto" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <label>Torre</label>
                                            <asp:TextBox ID="txtTorre" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">

                                        <div class="row">
                                            <div class="col-md-2">
                                                <label>Celular (*) :</label>
                                                <asp:TextBox ID="txtCelularCodArea" CssClass="form-control " placeholder="cod Área"
                                                    runat="server" MaxLength="6"></asp:TextBox>
                                                <Ajax:FilteredTextBoxExtender ID="txtCelularCodArea_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" FilterType="Numbers" TargetControlID="txtCelularCodArea">
                                                </Ajax:FilteredTextBoxExtender>
                                            </div>
                                            <div class="col-md-4">
                                                <label style="color: wheat">.</label>
                                                <asp:TextBox ID="txtCelular" CssClass="form-control " placeholder="número de celular"
                                                    runat="server" MaxLength="9"></asp:TextBox>
                                                <span class="help-block">Ingrese Teléfono Celular:</span>
                                                <Ajax:FilteredTextBoxExtender ID="txtCelular_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" FilterType="Numbers" TargetControlID="txtCelular">
                                                </Ajax:FilteredTextBoxExtender>
                                            </div>
                                            <div class="col-md-2">
                                                <label>Tel. Fijo (*):</label>
                                                <asp:TextBox ID="txtTelFijoCodArea" CssClass="form-control" placeholder="cod area"
                                                    runat="server" MaxLength="5"></asp:TextBox>
                                                <Ajax:FilteredTextBoxExtender ID="txtTelFijoCodArea_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" FilterType="Numbers" TargetControlID="txtTelFijoCodArea">
                                                </Ajax:FilteredTextBoxExtender>
                                            </div>
                                            <div class="col-md-4">
                                                <label style="color: wheat">.</label>
                                                <asp:TextBox ID="txtTelFijo" CssClass="form-control " placeholder="número de teléfono"
                                                    runat="server" MaxLength="9"></asp:TextBox>
                                                <span class="help-block">Ingrese Teléfono Fijo:</span>
                                                <Ajax:FilteredTextBoxExtender ID="txtTelFijo_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" FilterType="Numbers" TargetControlID="txtTelFijo">
                                                </Ajax:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Email *</label>
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>


                            </div>
                            <div class="tab-pane" id="tab_15_2">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Listado de Expediente en SUAC</label>
                                            <div class="input-group">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                            <div class="col-md-12" style="overflow: scroll;">
                                                <asp:GridView ID="gvExpedientes" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" DataKeyNames="ID_TRAMITE"
                                                    OnRowDataBound="gvExpedientes_RowDataBound" OnRowCommand="gvExpedientes_OnRowCommand">
                                                    <Columns>
                                                        <asp:BoundField DataField="ID_TRAMITE" HeaderText="ID_TRAMITE" />
                                                        <asp:BoundField DataField="NRO_TRAMITE" HeaderText="NRO_TRAMITE" />
                                                        <asp:BoundField DataField="NRO_STICKER" HeaderText="NRO_STICKER" />
                                                        <asp:BoundField DataField="ASUNTO" HeaderText="ASUNTO" HeaderStyle-Width="100px" ItemStyle-Width="100px" FooterStyle-Width="100px" />
                                                        <asp:BoundField DataField="NOMBRE_INICIADOR" HeaderText="NOMBRE_INICIADOR" />
                                                        <asp:BoundField DataField="NRO_DOCUMENTO" HeaderText="NRO_DOCUMENTO" />
                                                        <asp:BoundField DataField="FECHA_CREACION" HeaderText="FECHA_CREACION" />
                                                        <asp:BoundField DataField="TIPO" HeaderText="TIPO" />
                                                        <asp:BoundField DataField="SUBTIPO" HeaderText="SUBTIPO" />
                                                        <asp:BoundField DataField="UNIDAD_ACTUAL" HeaderText="UNIDAD_ACTUAL" />
                                                        <asp:BoundField DataField="UNIDAD_PROXIMA" HeaderText="UNIDAD_PROXIMA" />
                                                        <asp:BoundField DataField="FECHA_UNIDAD" HeaderText="FECHA_UNIDAD" />
                                                        <asp:BoundField DataField="N_ESTADO" HeaderText="N_ESTADO" />
                                                        <asp:TemplateField HeaderText="Quitar">
                                                            <ItemStyle CssClass="grilla-columna-accion" />
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnEliminar" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                    CommandName="QuitarExpediente" CssClass="botonEliminar" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>

                                                </asp:GridView>

                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <h6>Total de registros encontrados :
                                                            <asp:Label runat="server" ID="Label2" Text="0"></asp:Label></h6>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">

                                                        <ul class="pagination pull-right">
                                                            <li>
                                                                <h6>Páginas :</h6>
                                                            </li>
                                                            <asp:Repeater ID="rptBotonesPaginacion_Expedientes" OnItemDataBound="rptBotonesPaginacion_Expedientes_OnItemDataBound" OnItemCommand="rptBotonesPaginacion_Expedientes_OnItemCommand" runat="server">
                                                                <ItemTemplate>
                                                                    <li class="paginate_button">
                                                                        <asp:LinkButton ID="btnNroPagina" OnClick="btnNroPagina_Expedientes_OnClick" CommandArgument='<%# Bind("nroPagina") %>' runat="server" class="btn btn-default "><%# Eval("nroPagina")%></asp:LinkButton>
                                                                    </li>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </ul>
                                                    </div>
                                                </div>
                                                <br />

                                            </div>
                                        </div>
                            </div>
                            <div class="tab-pane" id="tab_15_3">

                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Sección</label>
                                            <asp:DropDownList ID="ddlSeccion" CssClass="form-control select2me" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>

                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Motivo</label>
                                                    <asp:TextBox ID="txtMotivo" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <label style="color: white;">.</label>
                                                <asp:Button runat="server" ID="btnAgregarSeccion" CssClass="btn blue form-control" Text="+ Agregar Sección" OnClick="btnAgregarSeccion_OnClick" />
                                            </div>
                                        </div>

                                        <div id="divMensajeErrorSecciones" runat="server" class="alert alert-danger alert-dismissable">
                                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                            </button>
                                            <strong>Error! </strong>
                                            <asp:Label runat="server" ID="lblMensajeErrorSecciones"></asp:Label>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12" style="overflow: scroll;">
                                                <asp:GridView ID="gvSecciones" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" DataKeyNames="IdSeccion"
                                                    OnRowDataBound="gvSecciones_RowDataBound" OnRowCommand="gvSecciones_OnRowCommand">
                                                    <Columns>
                                                        <asp:BoundField DataField="IdSeccion" HeaderText="Codigo" />

                                                        <asp:BoundField DataField="NombreSeccion" HeaderText="Nombre Sección" />
                                                        <asp:BoundField DataField="Motivo" HeaderText="Motivo" />

                                                        <asp:TemplateField HeaderText="Quitar">
                                                            <ItemStyle CssClass="grilla-columna-accion" />
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnEliminar" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                    CommandName="QuitarSeccion" CssClass="botonEliminar" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>

                                                </asp:GridView>

                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <h6>Total de registros encontrados :
                                                            <asp:Label runat="server" ID="lblTotalRegistras_Secciones" Text="0"></asp:Label></h6>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">

                                                        <ul class="pagination pull-right">
                                                            <li>
                                                                <h6>Páginas :</h6>
                                                            </li>
                                                            <asp:Repeater ID="rptBotonesPaginacion_Secciones" OnItemDataBound="rptBotonesPaginacion_Secciones_OnItemDataBound" OnItemCommand="rptBotonesPaginacion_Secciones_OnItemCommand" runat="server">
                                                                <ItemTemplate>
                                                                    <li class="paginate_button">
                                                                        <asp:LinkButton ID="btnNroPagina" OnClick="btnNroPagina_Secciones_OnClick" CommandArgument='<%# Bind("nroPagina") %>' runat="server" class="btn btn-default "><%# Eval("nroPagina")%></asp:LinkButton>
                                                                    </li>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </ul>
                                                    </div>
                                                </div>
                                                <br />

                                            </div>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                            <div class="tab-pane" id="tab_15_4">
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Cargo</label>
                                            <asp:DropDownList ID="ddlCargo" CssClass="form-control select2me" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>DNI :</label>
                                                    <asp:TextBox runat="server" ID="txtNroDocAutoridad" placeholder="Ingrese nro de documento" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Sexo :</label>
                                                    <asp:DropDownList ID="ddlSexoAutoridad" CssClass="form-control select2me dllControl" runat="server">
                                                        <asp:ListItem Value="01" Text="Masculino" />
                                                        <asp:ListItem Value="02" Text="Femenino" />
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label style="color: white;">.</label>
                                                    <asp:Button runat="server" ID="btnBuscarAutoridad" Text="+ Agregar Asistente" placeholder="Buscar asistente en Reg. Civil por su DNI y Sexo" OnClick="btnBuscarAutoridad_OnClick" CssClass="form-control btn blue"></asp:Button>
                                                </div>
                                            </div>
                                            
                                        </div>
                                        <div id="divMensajeErrorAutoridad" runat="server" class="alert alert-danger alert-dismissable">
                                                <button type="button" class="close" data-dismiss="alert" aria-hidden="true"></button>
                                                <strong>Error! </strong>
                                                <asp:Label runat="server" ID="lblMensajeErrorAutoridad"></asp:Label>
                                            </div>
                                        <div class="row">
                                            <div class="col-md-12" style="overflow: scroll;">
                                                <asp:GridView ID="gvAutoridades" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" DataKeyNames="Dni" OnRowDataBound="gvAutoridades_OnRowDataBound" OnRowCommand="gvAutoridades_OnRowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Orden">
                                                            <ItemStyle CssClass="grilla-columna-accion" />
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblNroOrden"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                                        <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                                                        <asp:BoundField DataField="Dni" HeaderText="Dni" />
                                                        <asp:BoundField DataField="cargo.NombreCargo" HeaderText="Cargo" />
                                                        <asp:TemplateField HeaderText="Quitar">
                                                            <ItemStyle CssClass="grilla-columna-accion" />
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnQuitar" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                    CommandName="QuitarAutoridad" CssClass="botonEliminar" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>

                                                </asp:GridView>

                                                <br />

                                            </div>
                                        </div>
                                        
                                    </ContentTemplate>
                                </asp:UpdatePanel>


                            </div>
                        </div>
                    </div>
                </div>

                <div class="form-actions">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" CssClass="btn green form-control" OnClick="btnAceptar_OnClick" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" CssClass="btn default form-control" OnClick="btnCancelar_OnClick" />
                        </div>
                    </div>
                </div>
                <!-- END FORM-->
            </div>
        </div>
    </div>
    



</asp:Content>

<asp:Content ID="ScriptContenedor" runat="server" ContentPlaceHolderID="ContentScript">
    <script type="text/javascript">
        //$("#")
    </script>
</asp:Content>

