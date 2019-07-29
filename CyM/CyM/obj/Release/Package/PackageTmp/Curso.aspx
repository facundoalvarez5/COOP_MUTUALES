<%@ Page  Language="C#"  MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Curso.aspx.cs" Inherits="CyM.Curso" %>
 
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
     <h3 class="page-title">
         Gestión de Cursos
            </h3>
            <div class="page-bar">
                <ul class="page-breadcrumb">
                    <li>
                        <i class="fa fa-home"></i>
                        <a href="#">Inicio</a>
                        <i class="fa fa-angle-right"></i>
                    </li>
                    <li>
                        <a href="#">Gestión de Cursos</a>
                        <i class="fa fa-angle-right"></i>
                    </li>
                    
                </ul>
               
        </div>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ConenedorPrincipal">
    <div id="divMensajeError" runat="server" class="alert alert-danger alert-dismissable">
        <button type="button" class="close" data-dismiss="alert" aria-hidden="true"></button>
	    <strong>Error! </strong> <asp:Label runat="server" ID="lblMensajeError"></asp:Label>
    </div>
     <div id="divMensajeExito" runat="server" class="alert alert-success alert-dismissable">
        <button type="button" class="close" data-dismiss="alert" aria-hidden="true"></button>
	    <strong>Exito! </strong> <asp:Label runat="server" ID="lblMensajeExito"></asp:Label>
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
			    <asp:Button runat="server" ID="btnNuevo" CssClass="btn green form-control"   Text="+ Nuevo" OnClick="btnNuevo_OnClick"/>
			</div>
		</div>
		<div class="portlet-body form">
			<!-- BEGIN FORM-->
				<div class="form-body">
				    <div class="row">
                       
				        <div class="col-md-4">
				            <div class="form-group">
				                <label>Fecha Desde:</label>
				                <asp:TextBox ID="txtFiltroFechaDesde" runat="server"  CssClass="form-control date-picker"  ></asp:TextBox>
				            </div>            
				        </div>
				        <div class="col-md-4">
				            <div class="form-group">
				                <label>Fecha Hasta:</label>
				                <asp:TextBox ID="txtFiltroFechaHasta" runat="server"  CssClass="form-control date-picker"  ></asp:TextBox>
				            </div>            
				        </div>
                    </div>
				</div>
				<div class="form-actions">
					<div class="row">
						<div class="col-md-2">
							<asp:Button runat="server" ID="btnConsultar" CssClass="btn default blue form-control"  Text="Consultar" OnClick="btnConsultar_OnClick"/>
						</div>
                        <div class="col-md-2"> 
                            <asp:Button runat="server" ID="btnLimpiarFiltros" CssClass="btn default form-control"  Text="Limpiar Campos" OnClick="btnLimpiarFiltros_OnClick"/>
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
                        <div class="col-md-12" style="overflow:scroll;">
                            <asp:GridView ID="gvResultado" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" DataKeyNames="IdCurso"
                            AllowPaging="true" PageSize="10"  OnPageIndexChanging="gvResultado_PageIndexChanging" OnRowDataBound="gvResultado_RowDataBound" OnRowCommand="gvResultado_OnRowCommand" >
                            <Columns>
                                <asp:BoundField DataField="FechaDictado" HeaderText="Fecha Dictado" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Nombre Curso" />
                                <asp:BoundField DataField="CantidadAsistentes" HeaderText="Cantidad Asistentes" />
                                <asp:BoundField DataField="Localidad.nombrelocalidad" HeaderText="Localidad" />
                                
                                <asp:TemplateField HeaderText="Ver">              
                                <ItemStyle CssClass="grilla-columna-accion"/>   
                                    <ItemTemplate>
                                        <asp:Button ID="btnVer" runat="server"  CommandArgument="<%# Container.DataItemIndex %>"
                                            CommandName="Ver" CssClass="botonVer"  />
                                    </ItemTemplate>
                                </asp:TemplateField>
            
                                <asp:TemplateField HeaderText="Editar">
                                    <ItemStyle CssClass="grilla-columna-accion"/>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditar" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                            CommandName="Editar" CssClass="botonEditar" ></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Eliminar">
                                    <ItemStyle CssClass="grilla-columna-accion"/>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEliminar" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                            CommandName="Eliminar" CssClass="botonEliminar"   />
                                    </ItemTemplate>
                                </asp:TemplateField>
                               

                            </Columns>
        
                </asp:GridView>

                <div class="row">
                      <div class="col-md-12"  >
                     <h6>Total de registros encontrados : <asp:Label runat="server" ID="lblTotalRegistrosGrilla" Text="0"></asp:Label></h6>           
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12"  >
                        
                        <ul class="pagination pull-right">
                            <li ><h6>Páginas :</h6></li>    
                            <asp:Repeater ID="rptBotonesPaginacion" OnItemDataBound="rptBotonesPaginacion_OnItemDataBound" OnItemCommand="rptBotonesPaginacion_OnItemCommand" runat="server"   >
                                <ItemTemplate>
                                    <li class="paginate_button">
                                    <asp:LinkButton ID="btnNroPagina" OnClick="btnNroPagina_OnClick" CommandArgument='<%# Bind("nroPagina") %>' runat="server" class="btn btn-default " ><%# Eval("nroPagina")%></asp:LinkButton>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
			            </ul>
	                </div>
                </div>
                <br/>
						 
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
				<span class="caption-subject font-red-sunglo bold uppercase"><asp:Label runat="server" ID="lblTituloPantallaABM" Text=""></asp:Label></span>
			</div>
		</div>
		<div class="portlet-body form">
			<!-- BEGIN FORM-->
			<div class="form-body">
			        <div class="row">
			            <div class="col-md-6">
			                <div class="form-group">
			                    <label>Nombre del Curso : </label>
			                    <asp:TextBox ID="txtNombreCurso" runat="server"  CssClass="form-control"  ></asp:TextBox>
			                </div>
			            </div>
			        </div>
                    <div class="row">
			            <div class="col-md-6">
			                <div class="form-group">
			                    <label>Cantidad de Asistentes : </label>
			                    <asp:TextBox ID="txtCantAsistentes" runat="server"  CssClass="form-control" MaxLength="5"  ></asp:TextBox>
                                
			                </div>
			            </div>
			        </div>
				    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                            <label>Fecha Dictado: </label>
                            <asp:TextBox ID="txtFechaDictado" runat="server"  CssClass="form-control date-picker"  ></asp:TextBox>
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
                                <asp:DropDownList ID="ddlLocalidad" CssClass="form-control select2me" AutoPostBack="True" OnSelectedIndexChanged="ddlLocalidad_OnSelectedIndexChanged" runat="server" >
                            </asp:DropDownList>
                                </div>
                                <span class="help-block">Ubicación del dictado del Curso</span>
                        </div>  
                    </div> 
                    
                    <h3>Solicitudes de Curso Cargadas y/o sin asignación en otro curso</h3>
                    <div id="divMensajeErrorSolicitudes" runat="server" class="alert alert-danger alert-dismissable">
                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true"></button>
	                    <strong>Error! </strong> <asp:Label runat="server" ID="lblMensajeErrorSolicitudes"></asp:Label>
                    </div>
                    <div   class="alert alert-info alert-dismissable">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true"></button>
	                                    <strong>Información</strong> 
                                         <asp:Label ID="lblseleccion" runat="server"></asp:Label>   
                                    </div>
     
                 <div class="row">
                        <div class="col-md-12" style="overflow:scroll;">
                            <asp:GridView ID="gvSolicitudes" runat="server" CssClass="table table-condensed table-hover" AutoGenerateColumns="false" DataKeyNames="NroSolicitudCurso" OnRowDataBound="gvSolicitudes_OnRowDataBound" OnRowCommand="gvSolicitudes_OnRowCommand">
                            <Columns> 
                                <asp:TemplateField HeaderText="Seleccionar">
                                    <ItemStyle CssClass="grilla-columna-accion"/>
                                    <ItemTemplate> 
                                        <asp:CheckBox ID="chkSeleccionar"  runat="server"  CommandArgument="<%# Container.DataItemIndex %>"
                                                      CommandName="Seleccionar" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NroSolicitudCurso" HeaderText="Nro Solicitud" />
                                <asp:BoundField DataField="NombreCooperativa" HeaderText="Nombre Entidad" />
                                <asp:BoundField DataField="NombreDepartamento" HeaderText="Departamento" />
                                <asp:BoundField DataField="NombreLocalidad" HeaderText="Localidad" />
                                <asp:BoundField DataField="FechaAlta" HeaderText="Fecha Carga Solicitud" />
                                 

                            </Columns>
        
                </asp:GridView>
                      
                        <br/>
						 
                        </div>
                    </div> 

              </div>
                      
			<div class="form-actions">
					<div class="row">
						<div class="col-md-2">
                            <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" CssClass="btn green form-control" OnClick="btnAceptar_OnClick"/>
						</div>
                        <div class="col-md-2">
                             <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" CssClass="btn default form-control" OnClick="btnCancelar_OnClick"/>
                        </div>
                        <div class="col-md-2" id="DivBtnImprimir" runat="server" Visible="False">
                            <input type='button' onclick='window.print();' value='Imprimir' class="btn green form-control" />
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