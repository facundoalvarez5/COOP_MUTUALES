<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Asambleas.aspx.cs" Inherits="CyM.Asambleas" %>
<%@ Register TagPrefix="Ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.1.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
    <h3 class="page-title">Básico de Asambleas
    </h3>
    <div class="page-bar">
        <ul class="page-breadcrumb">
            <li>
                <i class="fa fa-home"></i>
                <a href="#">Asambleas</a>
                <i class="fa fa-angle-right"></i>
            </li>
            <li>
                <a href="#">Básico de Asambleas</a>
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
                    <asp:Button runat="server" ID="btnNuevo" CssClass="btn green form-control" Text="+ Crear Asamblea" OnClick="btnNuevo_OnClick" />
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
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Fecha Desde :</label>
                                <asp:TextBox ID="txtFiltroFechaDesde" runat="server" CssClass="form-control date-picker"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Fecha Hasta :</label>
                                <asp:TextBox ID="txtFiltroFechaHasta" runat="server" CssClass="form-control date-picker"></asp:TextBox>
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
                            <asp:GridView ID="gvResultado" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" DataKeyNames="IdAsamblea"
                                AllowPaging="true" PageSize="10" OnPageIndexChanging="gvResultado_PageIndexChanging" OnRowDataBound="gvResultado_RowDataBound" OnRowCommand="gvResultado_OnRowCommand">
                                <Columns>
                                    
                                     <asp:BoundField DataField="Ejercicio" HeaderText="Ejercicio" />

                                    <asp:BoundField DataField="Entidad.nro_rp" HeaderText="Nro RP" />
                                    <asp:BoundField DataField="Entidad.nro_matricula" HeaderText="Observacion" />
                                    <asp:BoundField DataField="Entidad.descripcion" HeaderText="Cooperativa" />
                                    
                                    
                                    <asp:BoundField DataField="NombreEstadoActualEntidad" HeaderText="Estado" />
                                    <asp:BoundField DataField="TipoAsamblea.Nombre" HeaderText="Tipo Asamblea" />
                                    <asp:TemplateField HeaderText ="Fecha Asamblea" >
                                       <ItemTemplate >
                                       <asp:Label ID="lblFromDate" runat="server" 
                                                 
                                                  HtmlEncode="false"  
                                                  Text='<%# Eval("FechaAsamblea", "{0:dd/MM/yyyy}") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText ="Fecha Pre Asamblea" >
                                       <ItemTemplate >
                                       <asp:Label ID="lblFromDate" runat="server" 
                                                 
                                                  HtmlEncode="false"  
                                                  Text='<%# Eval("FechaPreAsamblea", "{0:dd/MM/yyyy}") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText ="Fecha Pos Asamblea" >
                                       <ItemTemplate >
                                       <asp:Label ID="lblFromDate" runat="server" 
                                                 
                                                  HtmlEncode="false"  
                                                  Text='<%# Eval("FechaPosAsamblea", "{0:dd/MM/yyyy}") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Observacion" HeaderText="Observación" />
                                     
                                    <asp:TemplateField HeaderText="Ver">
                                        <ItemStyle CssClass="grilla-columna-accion" />
                                        <ItemTemplate>
                                            <asp:Button ID="btnVer" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                CommandName="Ver" CssClass="botonVer" />
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
                        <div class="col-md-12">
                            <div class="form-group">
                               <h4 class="subtituloForm">Cooperativa </h4>
                               
                                <asp:UpdatePanel ID="upBuscarCooperativa" runat="server">
                                    <ContentTemplate>
                                        
                                         <div id="modalBuscarCooperativa" runat="server" class="w3-modal">
                                            <div class="w3-modal-content w3-animate-top w3-card-8">
                                                <header class="w3-container w3-teal">
                                                            <ul style="list-style-type: none;margin: 0;padding: 0;overflow: hidden;">
                                                                <li style="float: left;">
                                                                    <h3>Seleccionar Cooperativa</h3>
                                                                </li>
                                                                <li style="float: right;">
                                                                    <img src="Resources/logo.png" alt="logo" class="logo-default" style="height: 42px;margin: 7px;" />
                                                                </li>
                                                            </ul>
                                                            
                                                        </header>
                                                <div class="w3-container">
                                                    <div class="row">
                                                        <div class="col-md-12" style="overflow: scroll; height: 350px">
                                                            <asp:GridView ID="gvBuscarCooperativa" Style="overflow: scroll;" runat="server" CssClass="table table-striped"
                                                                AutoGenerateColumns="false" DataKeyNames="id_entidad"  OnRowDataBound="gvBuscarCooperativa_RowDataBound" OnRowCommand="gvBuscarCooperativa_OnRowCommand" >
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemStyle CssClass="grilla-columna-accion" />
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="btnSeleccionarCooperativa" Text="Seleccionar" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                                CommandName="Seleccionar" CssClass="btn btn-default" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="id_entidad" HeaderText="Codigo" />
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

                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                                <footer class="w3-container w3-teal">
                                                            <asp:Button ID="btnSalirModalBuscarCooperativa" Text="Salir" CssClass="btn btn-default" style="padding-top: 8px;margin: 10px;" OnClick="btnSalirModalBuscarCooperativa_OnClick" runat="server"/>
                                                        </footer>
                                            </div>
                                        </div>
                                        
                                         <div id="modalDetalleCooperativa" runat="server" class="w3-modal">
                                            <div class="w3-modal-content w3-animate-top w3-card-8">
                                                <header class="w3-container w3-teal">
                                                            <ul style="list-style-type: none;margin: 0;padding: 0;overflow: hidden;">
                                                                <li style="float: left;">
                                                                    <h3>Detalle de la Cooperativa Seleccionada</h3>
                                                                </li>
                                                                <li style="float: right;">
                                                                    <img src="Resources/logo.png" alt="logo" class="logo-default" style="height: 42px;margin: 7px;" />
                                                                </li>
                                                            </ul>
                                                            
                                                        </header>
                                                <div class="w3-container">
                                                    <h3 class="form-section">Datos de la Cooperativa</h3>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="control-label col-md-3">Nombre :</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtDetalleCooperativaNombre" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="control-label col-md-3">Nro RP :</label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtDetalleCooperativaNroRp" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </div>
                                                                <label class="control-label col-md-3">Matricula :</label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtDetalleCooperativaMatricula" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br/>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="control-label col-md-3">Fecha Aprobación</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtDetalleCooperativaFecAprob" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="control-label col-md-3">Fecha Elevación</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtDetalleCooperativaFecElevacion" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <h3 class="form-section">Domicilio </h3>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="control-label col-md-3">Departamento</label>
                                                                <div class="col-md-9">
                                                                    <asp:DropDownList ID="ddlDetalleCooperativaDepartamentoLegal" CssClass="form-control select2me" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="control-label col-md-3">Localidad</label>
                                                                <div class="col-md-9">
                                                                    <asp:DropDownList ID="ddlDetalleCooperativaLocalidadLegal" CssClass="form-control select2me" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br/>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="control-label col-md-3">Calle</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtDetalleCooperativaCalle" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="control-label col-md-3">Cod Postal</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtDetalleCooperativaCodPostal" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <h3 class="form-section">Contacto </h3>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="control-label col-md-3">Celular</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtDetalleCooperativaCelular" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="control-label col-md-3">Tel Fijo</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtDetalleCooperativaFijo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br/>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="control-label col-md-3">Email</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtDetalleCooperativaEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <h3 class="form-section">Historial de Estados </h3>
                                                     <div class="row">
                                                        <div class="col-md-12" style="overflow: scroll;">
                                                            <div class="form-group">
                                                                <asp:GridView ID="gvHistorial" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" DataKeyNames="IdHistorialEstado">
                                                                    <Columns>
                                                                         <asp:BoundField DataField="NombreEstado" HeaderText="Estado" />
                                                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                                                        <asp:BoundField DataField="CuilUsuarioCidi" HeaderText="Responsable" />
                                                                        <asp:BoundField DataField="FechaDesde" HeaderText="FechaDesde" />
                                                                        <asp:BoundField DataField="FechaHasta" HeaderText="FechaHasta" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                            </div>
                                                        </div>

                                                </div>
                                                <footer class="w3-container w3-teal">
                                                    <asp:Button ID="btnSalirDetalleCooperativa" Text="Salir" CssClass="btn btn-default" style="padding-top: 8px;margin: 10px;" OnClick="btnSalirModalDetalleCooperativa_OnClick" runat="server"/>
                                                </footer>
                                            </div>
                                        </div>
                                        <div id="divMensajeError_BuscarCooperativa" runat="server" class="alert alert-danger alert-dismissable">
                                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true"></button>
                                            <strong>Error! </strong>
                                            <asp:Label runat="server" ID="lblMensajeError_BuscarCooperativa"></asp:Label>
                                        </div>
                                        <div id="divBuscarCooperativa" runat="server">
                                            <div class="row">
                                                <div class="col-md-1">
                                                    <label>Buscar Por:</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddlBuscarPor" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="0" Selected="True" Text="Seleccionar..."/>
                                                        <asp:ListItem Value="matricula" Text="Matrícula"/>
                                                        <asp:ListItem Value="nro_rp" Text="Nro RP"/>
                                                        <asp:ListItem Value="descripcion" Text="Descripción"/> 
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtBuscarCooperativa" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <span class="caption-helper">Ingrese como mínimo 3 caracteres.</span>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Button ID="btnBuscarCooperativa" Text="Buscar Cooperativa" runat="server" CssClass="btn blue form-control" OnClick="btnBuscarCooperativa_OnClick"></asp:Button>
                                                </div>
                                            </div>

                                        </div>
                                        
                                        <div id="divCooperativaSeleccionada" runat="server">
                                            <div class="row">


                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtCoopeativaSeleccionada" BackColor="PaleGreen" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Button ID="btnVerCooperativaSeleccionada" Text="Detalle" OnClick="btnVerCooperativaSeleccionada_OnClick" runat="server" CssClass="btn default form-control"></asp:Button>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Button ID="btnSeleccionarOtraCooperativa" Text="Seleccionar Otra" runat="server" CssClass="btn blue form-control" OnClick="btnSeleccionarOtraCooperativa_OnClick"></asp:Button>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                              
                            </div>
                        </div>
                    </div>
                    <h4 class="subtituloForm">Datos de la Asamblea </h4>


                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Tipo de Asamblea (Caracter) (*): </label>
                                <asp:DropDownList ID="ddlTipoAsamblea" AutoPostBack="True" runat="server" CssClass="form-control select2me" OnSelectedIndexChanged="ddlTipoAsamblea_OnSelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                           <div class="form-group">
                                <label>Fecha Asamblea (*): </label>
                                <asp:TextBox ID="txtFechaAsamblea" runat="server" CssClass="form-control date-picker"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                            <label>Ejercicio : </label>
                            <asp:TextBox ID="txtEjecicio" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Lugar : </label>
                                <asp:TextBox ID="txtLugar" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    
                    
                      

                     <h4 class="subtituloForm">Detalle </h4>


                    <div class="tabbable-custom">
                        <ul class="nav nav-tabs ">
                            <li class="active">
                                <a href="#tab_15_1" data-toggle="tab" style="font-weight: bold;"><i class="fa fa-mail-forward"></i>PRE ASAMBLEA </a>
                            </li>
                            <li>
                                <a href="#tab_15_2" data-toggle="tab" style="font-weight: bold;"><i class="fa fa-mail-reply"></i>POS ASAMBLEA</a>
                            </li>
                            <li>
                                <a href="#tab_15_3" data-toggle="tab" style="font-weight: bold;"><i class="fa fa-tasks"></i> TEMARIO</a>
                            </li>
                             <li>
                                <a href="#tab_15_4" data-toggle="tab" style="font-weight: bold;"><i class="fa fa-users"></i> VEEDORES</a>
                            </li>
                        </ul>
                        <div class="tab-content">
                            <div class="tab-pane active" id="tab_15_1">
                                <asp:HiddenField runat="server" ID="hfCantDiasPresentacionPreAsamblea" />
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Fecha Pre Asamblea (*) :</label>
                                            <asp:TextBox ID="txtFechaPreAsamblea" runat="server" CssClass="form-control date-picker"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <h4 class="subtituloForm">Documentación Presentada</h4>
                                <div class="row">
                                    <div class="col-md-12" style="overflow: scroll;">
                                        <asp:GridView ID="gvDocPreAsamblea" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" DataKeyNames="ID_DOCUMENTO"
                                            OnRowDataBound="gvDocPreAsamblea_RowDataBound" OnRowCommand="gvDocPreAsamblea_OnRowCommand">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Presentada">
                                                    <ItemStyle CssClass="grilla-columna-accion" />
                                                    <ItemTemplate>
                                                        <input id="chkPre" runat="server" value='<%#(Eval("ID_DOCUMENTO"))%>' type="checkbox" class="make-switch switch-large" data-label-icon="fa fa-fullscreen" data-on-text="<i class='fa fa-check'></i>" data-off-text="<i class='fa fa-times'></i>" data-on-color="primary" data-off-color="default" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="ID_DOCUMENTO" HeaderText="Código" />
                                                <asp:BoundField DataField="N_DOCUMENTO" HeaderText="Documento" />
                                                <%--<asp:BoundField DataField="Estado" HeaderText="Estado" />--%>
                                                <asp:TemplateField HeaderText="Obligatorio">
                                                    <ItemStyle CssClass="grilla-columna-accion" />
                                                    <ItemTemplate>
                                                        <asp:RadioButton ID="rbObligatorio" Enabled="False" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>


                                        </asp:GridView>


                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" id="tab_15_2">
                                <asp:HiddenField runat="server" ID="hfCantDiasPresentacionPosAsamblea" />
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Fecha Pre Asamblea (*) :</label>
                                            <asp:TextBox ID="txtFechaPosAsamblea" runat="server" CssClass="form-control date-picker"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <h4 class="subtituloForm">Documentación Presentada</h4>
                                <div class="row">
                                    <div class="col-md-12" style="overflow: scroll;">
                                        <asp:GridView ID="gvDocPosAsamblea" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" DataKeyNames="ID_DOCUMENTO"
                                            OnRowDataBound="gvDocPosAsamblea_RowDataBound" OnRowCommand="gvDocPosAsamblea_OnRowCommand">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Presentada">
                                                    <ItemStyle CssClass="grilla-columna-accion" />
                                                    <ItemTemplate>
                                                        <input id="chkPre" runat="server" value='<%#(Eval("ID_DOCUMENTO"))%>' type="checkbox" class="make-switch switch-large" data-label-icon="fa fa-fullscreen" data-on-text="<i class='fa fa-check'></i>" data-off-text="<i class='fa fa-times'></i>"  data-on-color="danger" data-off-color="default" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="ID_DOCUMENTO" HeaderText="Código" />
                                                <asp:BoundField DataField="N_DOCUMENTO" HeaderText="Documento" />
                                                <asp:TemplateField HeaderText="Obligatorio">
                                                    <ItemStyle CssClass="grilla-columna-accion" />
                                                    <ItemTemplate>
                                                        <asp:RadioButton ID="rbObligatorio" Enabled="False" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>


                                        </asp:GridView>


                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" id="tab_15_3">

                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Tema : </label>
                                            <asp:DropDownList ID="ddlTema" CssClass="form-control select2me" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>

                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Observación:</label>
                                                    <asp:TextBox ID="txtObeservacionTema" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <label style="color: white;">.</label>
                                                <asp:Button runat="server" ID="btnAgregarTema" CssClass="btn blue form-control" Text="+ Agregar Tema" OnClick="btnAgregarTema_OnClick" />
                                            </div>
                                        </div>

                                        <div id="divMensajeErrorTemas" runat="server" class="alert alert-danger alert-dismissable">
                                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                            </button>
                                            <strong>Error! </strong>
                                            <asp:Label runat="server" ID="lblMensajeErrorTemas"></asp:Label>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12" style="overflow: scroll;">
                                                <asp:GridView ID="gvTemas" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" DataKeyNames="IdTema"
                                                    OnRowDataBound="gvTemas_RowDataBound" OnRowCommand="gvTemas_OnRowCommand">
                                                    <Columns>
                                                        <asp:BoundField DataField="IdTema" HeaderText="Codigo" />

                                                        <asp:BoundField DataField="NombreTema" HeaderText="Nombre Tema" />
                                                        <asp:BoundField DataField="Observacion" HeaderText="Observación" />

                                                        <asp:TemplateField HeaderText="Quitar">
                                                            <ItemStyle CssClass="grilla-columna-accion" />
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnEliminar" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                    CommandName="QuitarTema" CssClass="botonEliminar" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>

                                                </asp:GridView>

                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <h6>Total de registros :
                                                            <asp:Label runat="server" ID="lblTotalRegistras_Temas" Text="0"></asp:Label></h6>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">

                                                        <ul class="pagination pull-right">
                                                            <li>
                                                                <h6>Páginas :</h6>
                                                            </li>
                                                            <asp:Repeater ID="rptBotonesPaginacion_Temas" OnItemDataBound="rptBotonesPaginacion_Temas_OnItemDataBound" OnItemCommand="rptBotonesPaginacion_Temas_OnItemCommand" runat="server">
                                                                <ItemTemplate>
                                                                    <li class="paginate_button">
                                                                        <asp:LinkButton ID="btnNroPagina" OnClick="btnNroPagina_Temas_OnClick" CommandArgument='<%# Bind("nroPagina") %>' runat="server" class="btn btn-default "><%# Eval("nroPagina")%></asp:LinkButton>
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

                                
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>

                                         
                                        <div id="divMensajeErrorVeedor" runat="server" class="alert alert-danger alert-dismissable">
                                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                            </button>
                                            <strong>Error! </strong>
                                            <asp:Label runat="server" ID="lblMensajeErrorVeedor"></asp:Label>
                                        </div>
                                        <div class="row">
									    <div class="col-md-2">
									         <div class="form-group">
									        <label>CUIL Veedor :</label>
                                            <asp:TextBox ID="txtCuilUsuarioCidi" CssClass="form-control" runat="server"></asp:TextBox>
									        </div> 
									    </div> 
                                         
                                       
                                        <div class="col-md-2">
                                             <div class="form-group">
                                            <label style="color:white;">.</label>
                                            <asp:Button ID="btnBuscarUsuarioCidi" Text="Buscar Usuario CIDI" OnClick="btnBuscarUsuarioCidi_OnClick" runat="server" CssClass="form-control btn blue"/>
                                        </div> 
                                        </div> 
                                        
									</div>
                                    <div class="row">
                                        <div class="col-md-12" style="overflow: scroll;">
                                            <div class="form-group">
                                                <asp:GridView ID="gvVeedores" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" DataKeyNames="cuil"
                                                    OnRowDataBound="gvVeedores_RowDataBound" OnRowCommand="gvVeedores_OnRowCommand">
                                                    <Columns>
                                                        <asp:BoundField DataField="cuil" HeaderText="CUIL" />

                                                        <asp:BoundField DataField="NombreVeedor" HeaderText="Nombre Veedor" />
                                                        <asp:BoundField DataField="Celular" HeaderText="Celular" />
                                                        <asp:BoundField DataField="Mail" HeaderText="Email" />

                                                        <asp:TemplateField HeaderText="Quitar">
                                                            <ItemStyle CssClass="grilla-columna-accion" />
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnEliminar" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                    CommandName="QuitarVeedor" CssClass="botonEliminar" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>

                                                </asp:GridView>

                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <h6>Total de registros :
                                                        <asp:Label runat="server" ID="lblTotalRegistras_Veedor" Text="0"></asp:Label></h6>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">

                                                        <ul class="pagination pull-right">
                                                            <li>
                                                                <h6>Páginas :</h6>
                                                            </li>
                                                            <asp:Repeater ID="rptBotonesPaginacion_Veedores" OnItemDataBound="rptBotonesPaginacion_Temas_OnItemDataBound" OnItemCommand="rptBotonesPaginacion_Temas_OnItemCommand" runat="server">
                                                                <ItemTemplate>
                                                                    <li class="paginate_button">
                                                                        <asp:LinkButton ID="btnNroPagina" OnClick="btnNroPagina_Temas_OnClick" CommandArgument='<%# Bind("nroPagina") %>' runat="server" class="btn btn-default "><%# Eval("nroPagina")%></asp:LinkButton>
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

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                        </div>
                    </div>
                    <!-- END FORM-->
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
            </div>
        </div>
    </div>

     



</asp:Content>

<asp:Content ID="ScriptContenedor" runat="server" ContentPlaceHolderID="ContentScript">
    
    <script type="text/javascript">
        $(document).ready(function () {
            $('#ConenedorPrincipal_txtFechaAsamblea').change(function () {

                var dateString = $("#ConenedorPrincipal_txtFechaAsamblea").val();//"23/10/2015"; // Oct 23

                var dateParts = dateString.split("/");

                var date = new Date(dateParts[2], dateParts[1] - 1, dateParts[0]); // month is 0-based
                var datePos = new Date(dateParts[2], dateParts[1] - 1, dateParts[0]); // month is 0-based

                if (!isNaN(date.getTime())) {
                    var days = parseInt($("#ConenedorPrincipal_hfCantDiasPresentacionPreAsamblea").val(), 10);

                    var daysPos = parseInt($("#ConenedorPrincipal_hfCantDiasPresentacionPosAsamblea").val(), 10);

                   
                    date.setDate(date.getDate() - days);
                    var yyyy = date.getFullYear().toString();
                    var mm = (date.getMonth() + 1).toString(); // getMonth() is zero-based
                    var dd = date.getDate().toString();


                    datePos.setDate(datePos.getDate() + daysPos);
                    var yyyy2 = datePos.getFullYear().toString();
                    var mm2 = (datePos.getMonth() + 1).toString(); // getMonth() is zero-based
                    var dd2 = datePos.getDate().toString();


                    $("#ConenedorPrincipal_txtFechaPreAsamblea").val(dd + "/" + mm + "/" + yyyy);
                    $("#ConenedorPrincipal_txtFechaPosAsamblea").val(dd2 + "/" + mm2 + "/" + yyyy2);
                } else {
                    // alert("Invalid Date");
                }

            });
        });
    </script>
</asp:Content>

