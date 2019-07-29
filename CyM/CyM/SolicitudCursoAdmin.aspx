<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="SolicitudCursoAdmin.aspx.cs" Inherits="CyM.SolictudCursoAdmin" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
    <h3 class="page-title">Solicitud de Curso
    </h3>
    <div class="page-bar">
        <ul class="page-breadcrumb">
            <li>
                <i class="fa fa-home"></i>
                <a href="#">Inicio</a>
                <i class="fa fa-angle-right"></i>
            </li>
            <li>
                <a href="#">Solicitud de Curso</a>
                <i class="fa fa-angle-right"></i>
            </li>

        </ul>

    </div>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ConenedorPrincipal">
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
                    <asp:Button runat="server" ID="btnNuevo" CssClass="btn green form-control" Style="display: none;" Text="+ Nuevo" OnClick="btnNuevo_OnClick" />
                </div>
            </div>
            <div class="portlet-body form">
                <!-- BEGIN FORM-->
                <div class="form-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Nombre Cooperativa en Formación:</label>
                                <asp:TextBox ID="txtFiltroNombre" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Fecha Desde:</label>
                                <asp:TextBox ID="txtFiltroFechaDesde" runat="server" CssClass="form-control date-picker"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Fecha Hasta:</label>
                                <asp:TextBox ID="txtFiltroFechaHasta" runat="server" CssClass="form-control date-picker"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-actions">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnConsultar" CssClass="btn default blue form-control" Text="Consultar" OnClick="btnConsultar_OnClick" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnLimpiarFiltros" CssClass="btn default form-control" Text="Limpiar Campos" OnClick="btnLimpiarFiltros_OnClick" />
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
                            <asp:GridView ID="gvResultado" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" DataKeyNames="NroSolicitudCurso"
                                AllowPaging="true" PageSize="10" OnPageIndexChanging="gvResultado_PageIndexChanging" OnRowDataBound="gvResultado_RowDataBound" OnRowCommand="gvResultado_OnRowCommand">
                                <Columns>
                                    <asp:BoundField DataField="NroSolicitudCurso" HeaderText="Nro Solicitud Curso" />
                                    <asp:BoundField DataField="NombreCooperativa" HeaderText="Nombre" />
                                    <asp:BoundField DataField="NombreDepartamento" HeaderText="Departamento" />
                                    <asp:BoundField DataField="NombreLocalidad" HeaderText="Localidad" />
                                    <asp:BoundField DataField="FechaAlta" HeaderText="FechaAlta" />
                                    <asp:BoundField DataField="Estado" HeaderText="Estado" />

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
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Nombre Cooperativa en Formación: </label>
                                <asp:TextBox ID="txtNombreCooperativa" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Departamento</label>
                                <asp:DropDownList ID="ddlDepartamento" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartamento_OnSelectedIndexChanged" CssClass="form-control select2me" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Localidad</label>
                                <asp:DropDownList ID="ddlLocalidad" CssClass="form-control select2me" AutoPostBack="True" OnSelectedIndexChanged="ddlLocalidad_OnSelectedIndexChanged" runat="server">
                                </asp:DropDownList>
                            </div>
                            
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Cantidad de Asistentes: </label>
                                <asp:TextBox ID="txtCantAsistentes" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                                <label>Seleccione Archivo para adjuntar</label>
                                <asp:FileUpload ID="FUAsistentes" runat="server" CssClass="form-control"/>
                            <br/>
                            <h5>
                            <img src="Resources/flecha_abajo.png" />   Haga click en "Adjuntar Archivo".    
                            </h5>
                            <asp:Button ID="btnAdjuntar" CssClass="form-control btn btn-default" Text="Adjuntar Archivo" runat="server" OnClick="btnAdjuntar_OnClick"/>
                            <asp:label runat="server" ID="lblResultadoAdjuntar"></asp:label>
                        </div>
                        
                    </div>
                    
                    <h3>Personas que solicitan el Curso</h3>
                    <div id="divGestionAsistentes" runat="server">
                        
                        <div id="divMensajeErrorAgregarSolicitante" runat="server" class="alert alert-danger alert-dismissable">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true"></button>
                            <strong>Error! </strong>
                            <asp:Label runat="server" ID="lblMensajeErrorAgregarSolicitante"></asp:Label>
                        </div>
                        <div class="alert alert-info alert-dismissable">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true"></button>
                            <strong>Máximo 10 personas </strong>
                        </div>

                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>DNI :</label>
                                    <asp:TextBox runat="server" ID="txtNroDocAsistente" placeholder="Ingrese nro de documento" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Sexo :</label>
                                    <asp:DropDownList ID="ddlSexoAsistente" CssClass="form-control select2me" runat="server">
                                        <asp:ListItem Value="01" Text="Masculino" />
                                        <asp:ListItem Value="02" Text="Femenino" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Email *</label>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <div class="row">

                            <div class="col-md-1">
                                <div class="form-group">
                                    <label>Tel Fijo</label>
                                    <asp:TextBox ID="txtTelefonoFijoCodArea" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label style="color: white;">.</label>
                                    <asp:TextBox ID="txtTelefonoFijo" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-1">
                                <div class="form-group">
                                    <label>Celular</label>
                                    <asp:TextBox ID="txtCelularCodArea" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Telefono Celular</label>
                                    <asp:TextBox ID="txtCelular" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label style="color: white;">.</label>
                                    <asp:Button runat="server" ID="btnBuscarAsistente" Text="Agregar Asistente" placeholder="Buscar asistente en Reg. Civil por su DNI y Sexo" OnClick="btnBuscarAsistente_OnClick" CssClass="form-control btn blue"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="overflow: scroll;">
                            <asp:GridView ID="gvAsistentes" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" DataKeyNames="Dni" OnRowDataBound="gvAsistentes_OnRowDataBound" OnRowCommand="gvAsistentes_OnRowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Orden">
                                        <ItemStyle CssClass="grilla-columna-accion" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblNroOrden"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NombreCompletoPersona" HeaderText="Nombre Completo" />
                                    <asp:BoundField DataField="Dni" HeaderText="Dni" />
                                    <asp:BoundField DataField="Sexo" HeaderText="Sexo" />
                                    <asp:BoundField DataField="Contacto.EMail" HeaderText="Email" />
                                    <asp:BoundField DataField="Contacto.CodAreaCel" HeaderText="Código Area Celular" />
                                    <asp:BoundField DataField="Contacto.NroCel" HeaderText="Nro Celular" />
                                    <asp:BoundField DataField="Contacto.CodAreaTelFijo" HeaderText="Código Area TelFijo" />
                                    <asp:BoundField DataField="Contacto.NroTelFijo" HeaderText="Tel Fijo" />


                                    <asp:TemplateField HeaderText="Quitar">
                                        <ItemStyle CssClass="grilla-columna-accion" />
                                        <ItemTemplate>
                                            <asp:Button ID="btnQuitar" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                CommandName="QuitarAsistente" CssClass="botonEliminar" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>

                            </asp:GridView>

                            <br />

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
        
    </script>
</asp:Content>
