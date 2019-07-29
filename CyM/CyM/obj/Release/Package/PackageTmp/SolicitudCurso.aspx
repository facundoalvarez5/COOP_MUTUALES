<%@ Page  Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SolicitudCurso.aspx.cs" Inherits="CyM.SolictudCurso" %>
 
   
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="ContenedorEncabezado"> 
    <header id="header">
        <div class="container">

            <div id="logo" class="pull-left logoCoopMut">
                <a href="#body" class="scrollto">
                    <img src="Resources/Logo.png" style="width: 33px;">
                    Cooperativa y Mutuales
                </a>
            </div>
            <nav id="nav-menu-container">
                <ul class="nav-menu">
                    <li>
                        <a href="Inicio.aspx">Inicio</a>
                    </li>
                    <li><a href="MiSolicitudCurso.aspx">Consultar Mi Solicitud</a></li>
                    <li class=" ">
                        <a href="#" class="" data-toggle="dropdown" data-hover="dropdown" data-close-others="true">
                            <img alt="" class="img-circle" src="Resources/ico_ciudig28.png"> 
                            <span class="username username-hide-on-mobile">
                                <asp:Label runat="server" ID="lblUsuarioCIDI"  ></asp:Label> </span>
                           
                        </a>
                        <ul class="dropdown-menu">
                            <li>
                                <a href="https://cidi.cba.gov.ar/">
                                    <i class="icon-user"></i> Mi Perfil </a>
                            </li>
                            <li>
                                <a href="https://cidi.cba.gov.ar/Usuario/Historial">
                                    <i class="icon-calendar"></i> Actividades CiDi </a>
                            </li> 
                            <li>
                                <a href="https://cidi.cba.gov.ar/Cuenta/cerrarsesion">
                                    <i class="icon-lock"></i> Cerrar Sesión </a>
                            </li>
                            <li>
                                <a href="Cooperativas.aspx">
                                    <i class="icon-key"></i> Salir </a>
                            </li>
                        </ul>
                    </li>
                   <%-- <li><a href="#">
                        <img alt="" class="img-circle" src="Resources/ico_ciudig28.png"> 
                        <asp:Label runat="server" ID="lblUsuarioCIDI"  ></asp:Label>
                    </a></li>--%>
                  
                </ul>
            </nav>
            <!-- #nav-menu-container -->
        </div>
    </header>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContenedorPrincipal">
  
<section id="services">
      <div class="container">
        <div class="section-header">
          <h2>Solicitud de Curso </h2>
          <p>Solicitud de Curso para Coopertaiva/Mutual en Formación. Debe completar el siguiente formulario para solicitar el curso al Área de Promoción de la Subsecretaría de Cooperativas y Mutuales del Gobierno de la Provincia de Córdoba.</p>
        </div>

        <div class="row">
          <div class="col-lg-12">

    <div id="divMensajeError" runat="server" class="alert alert-danger alert-dismissable">
        <button type="button" class="close" data-dismiss="alert" aria-hidden="true"></button>
	    <strong>Error! </strong> <asp:Label runat="server" ID="lblMensajeError"></asp:Label>
        
    </div>
     <div id="divMensajeExito" runat="server" class="alert alert-success alert-dismissable">
         
	    <strong>Exito! </strong> <asp:Label runat="server" ID="lblMensajeExito"></asp:Label>
         <br/>
         <br/>
         <div class="row">
             <div class="col-md-4"></div>
             <div class="col-md-4"><asp:Button ID="btnImprimirComprobante" Text="Descargar Comprobante" runat="server" OnClick="btnImprimirComprobante_OnClick"/></div>
             <div class="col-md-4"></div>

         </div>
         <br/>
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
			    <asp:Button runat="server" ID="btnNuevo" CssClass="btn green form-control" Text="+ Nuevo" OnClick="btnNuevo_OnClick"/>
			</div>
		</div>
		<div class="portlet-body form">
			<!-- BEGIN FORM-->
				<div class="form-body">
				    <div class="row">
                       <div class="col-md-6">
                            <div class="form-group">
                            <label>Nombre cooperativa</label>
                            <asp:TextBox ID="txtFiltroDescripcion" runat="server"  CssClass="form-control"  ></asp:TextBox>
                            </div>            
                        </div>            
                    </div>
				</div>
				<div class="form-actions">
					<div class="row">
						<div class="col-md-2">
							<asp:Button runat="server" ID="btnConsultar" CssClass="btn default form-control"  Text="Consultar" OnClick="btnConsultar_OnClick"/>
						</div>
                        <div class="col-md-2">
                             <button id="btnLimpiarFiltros" class="btn default form-control" >Limpiar Campos</button>
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
                            <asp:GridView ID="gvResultado" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" DataKeyNames="id_entidad"
                            AllowPaging="true" PageSize="10"  OnPageIndexChanging="gvResultado_PageIndexChanging" OnRowDataBound="gvResultado_RowDataBound" OnRowCommand="gvResultado_OnRowCommand" >
                            <Columns>
                                <asp:BoundField DataField="id_entidad" HeaderText="Codigo" />
                                <asp:BoundField DataField="Estado" HeaderText="Estado" />
                                <asp:BoundField DataField="descripcion" HeaderText="Cooperativa" />
                                <asp:BoundField DataField="cuit" HeaderText="Cuit" />
                                <asp:BoundField DataField="nro_rp" HeaderText="Nro Registro" />
                                <asp:BoundField DataField="nro_matricula" HeaderText="Nro Matrícula" />
                                <asp:BoundField DataField="resolucion_fecha" HeaderText="Fecha Resolución" />
                                <asp:BoundField DataField="estatuto_fec_aprobacion" HeaderText="Estatuto Fecha Aprobacion" />
                                <asp:BoundField DataField="estatuto_fec_reforma" HeaderText="Estatuto Fecha Reforma" />
                                <asp:BoundField DataField="fec_cierre_ejercicio" HeaderText="Fecha Cierre Ejercicio" />
                                <asp:BoundField DataField="observacion" HeaderText="Observacion" />
                                
                                <asp:TemplateField HeaderText="Ver">              
                                <ItemStyle CssClass="grilla-columna-accion"/>   
                                    <ItemTemplate>
                                        <asp:Button ID="btnVer" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
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
                                            CommandName="Eliminar" CssClass="botonEliminarSite"   />
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
				<div class="form-actions">
					<div class="row">
						<div class="col-md-2">
                            <asp:Button runat="server" ID="btnImprimir" CssClass="btn green form-control" Text="Imprimir"/>
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
                        <div class="col-md-4">
                            <div class="form-group">
                            <label>Nombre Cooperativa en Formación: </label>
                            <asp:TextBox ID="txtNombreCooperativa" runat="server"  CssClass="form-control"  ></asp:TextBox>
                            </div>
                        </div>
                         <div class="col-md-4">
                            <div class="form-group">
                            <label>Departamento</label>
                            <asp:DropDownList ID="ddlDepartamento" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartamento_OnSelectedIndexChanged" CssClass="form-control select2me dllControl" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                            <label>Localidad</label>
                                <asp:DropDownList ID="ddlLocalidad" CssClass="form-control select2me dllControl" AutoPostBack="True" OnSelectedIndexChanged="ddlLocalidad_OnSelectedIndexChanged" runat="server" >
                            </asp:DropDownList>
                                </div>
                                <span class="help-block">Ubicación de la Cooperativa</span>
                        </div>  
                    </div>
                    <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                <label>Cantidad de Asistentes: </label>
                                <asp:TextBox ID="txtCantAsistentes" runat="server"  CssClass="form-control" MaxLength="10" ></asp:TextBox>
                              <%--  <ajx:FilteredTextBoxExtender ID="txtNroCalle_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" FilterType="Numbers" TargetControlID="txtCantAsistentes">
                                </ajx:FilteredTextBoxExtender>--%>
                                </div>
                            </div>
                    </div> 
                    
                    <h3>Personas que solicitan el Curso</h3>
                    <div id="divMensajeErrorAgregarSolicitante" runat="server" class="alert alert-danger alert-dismissable">
                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true"></button>
	                    <strong>Error! </strong> <asp:Label runat="server" ID="lblMensajeErrorAgregarSolicitante"></asp:Label>
                    </div>
                            <div   class="alert alert-info alert-dismissable">
                                <strong>Máximo 10 personas </strong> 
                            </div>
    
				    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>DNI :</label>
                                <asp:TextBox runat="server" ID="txtNroDocAsistente"  placeholder="Ingrese nro de documento"  CssClass="form-control" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                            <label>Sexo :</label>
                                <asp:DropDownList ID="ddlSexoAsistente" CssClass="form-control select2me dllControl"   runat="server" >
                                    <asp:ListItem Value="01" Text="Masculino" />
                                    <asp:ListItem Value="02" Text="Femenino" />
                            </asp:DropDownList>
                            </div>
                        </div>
				        <div class="col-md-4">
				            <div class="form-group">
				                <label>Email *</label>
				                <asp:TextBox ID="txtEmail" runat="server"  CssClass="form-control"  ></asp:TextBox>
				            </div>
				        </div>           
                       
                    </div>
                                
                    <div class="row">
                    
                    <div class="col-md-1">
                        <div class="form-group">
                            <label>Tel Fijo</label>
                            <asp:TextBox ID="txtTelefonoFijoCodArea" runat="server"  CssClass="form-control"  ></asp:TextBox>
                        </div>
                    </div> 
                    <div class="col-md-2">
                        <div class="form-group"> 
                            <label style="color: white;">.</label>
                            <asp:TextBox ID="txtTelefonoFijo" runat="server"  CssClass="form-control"  ></asp:TextBox>
                        </div>
                    </div> 
                    <div class="col-md-1">
                        <div class="form-group">
                            <label>Celular</label>
                            <asp:TextBox ID="txtCelularCodArea" runat="server"  CssClass="form-control"  ></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                        <label>Telefono Celular</label>
                        <asp:TextBox ID="txtCelular" runat="server"  CssClass="form-control"  ></asp:TextBox>
                        </div>
                    </div>
                     <div class="col-md-2">
                            <div class="form-group">
                                <label style="color:white;">.</label>
                                <asp:Button runat="server" ID="btnBuscarAsistente" Text="Agregar Asistente"  placeholder="Buscar asistente en Reg. Civil por su DNI y Sexo" OnClick="btnBuscarAsistente_OnClick"  CssClass="form-control btn blue" ></asp:Button>
                            </div>
                        </div>
				</div>
                 <div class="row">
                        <div class="col-md-12" style="overflow:scroll;">
                            <asp:GridView ID="gvAsistentes" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" DataKeyNames="Dni" OnRowDataBound="gvAsistentes_OnRowDataBound" OnRowCommand="gvAsistentes_OnRowCommand">
                            <Columns> 
                                <asp:TemplateField HeaderText="Orden">
                                    <ItemStyle CssClass="grilla-columna-accion"/>
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblNroOrden"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NombreCompletoPersona" HeaderText="Nombre Completo" />
                                <asp:BoundField DataField="Dni" HeaderText="Dni" />
                                <asp:BoundField DataField="Sexo" HeaderText="Sexo" />
                                <asp:BoundField DataField="Contacto.EMail" HeaderText="Email" />
                                <asp:BoundField DataField="Contacto.CodAreaCel" HeaderText="TelCelularCodArea Registro" />
                                <asp:BoundField DataField="Contacto.NroCel" HeaderText="TelCelular" />
                                <asp:BoundField DataField="Contacto.CodAreaTelFijo" HeaderText="TelFijoCodArea" />
                                <asp:BoundField DataField="Contacto.NroTelFijo" HeaderText="TelFijo" />
                                
             
                                    <asp:TemplateField HeaderText="Quitar">
                                    <ItemStyle CssClass="grilla-columna-accion"/>
                                    <ItemTemplate>
                                        <asp:Button ID="btnQuitar" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                            CommandName="QuitarAsistente" CssClass="botonEliminarSite"   />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
        
                </asp:GridView>
                      
                        <br/>
						 
                        </div>
                    </div> 

              </div>
                      
			<div class="form-actions">
					    <div class="row">
						<div class="col-md-2">
                            <asp:Button runat="server" ID="btnAceptar" Text="Enviar e Imprimir" CssClass="btn green form-control" OnClick="btnAceptar_OnClick"/>
						</div>
                        
					</div>
				    </div>
			<!-- END FORM-->
		    
	    </div>
        
    </div>
    </div>
          </div>
        </div>

    </div>
</section>


</asp:Content>
 