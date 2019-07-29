<%@ Page Title="Cooperativas y Mutuales" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Inicio.aspx.cs" Inherits="CyM.Inicio" %>
    <asp:Content runat="server" ContentPlaceHolderID="ContenedorEncabezado">
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
                        <li class="menu-active">
                            <a href="#body">Home</a>
                        </li>
                        <li><a href="#Cooperativas">Cooperativas</a></li>
                        <li><a href="#Mutuales">Mutuales</a></li>
                        <li><a href="#Contactos">Contactos</a></li>
                        <li><a href="Cooperativas.aspx">Sistema</a></li>
                    </ul>
                </nav>
                <!-- #nav-menu-container -->
            </div>
        </header>
        <!-- #header -->

        <!--==========================
    Intro Section
  ============================-->
        <section id="intro">

            <div class="intro-content">
                <h2>COOPERATIVAS Y<br>
                    MUTUALES</h2>
                <div>
                    <a href="SolicitudCurso.aspx" class="btn-get-started scrollto">Solicitar Curso Inicio</a>
                    <a href="#" class="btn-projects scrollto">Padrones de mi Cooperativa</a>
                </div>
            </div>

            <div id="intro-carousel" class="owl-carousel">
                <%--<div class="item" style="background-image: url('Reveal/img/intro-carousel/1.jpg');"></div>--%>
                <div class="item" style="background-image: url('Reveal/img/intro-carousel/CBACoopYMutuales.jpg');"></div>
                <%--<div class="item" style="background-image: url('Reveal/img/intro-carousel/3.jpg');"></div>--%>
               <%-- <div class="item" style="background-image: url('Reveal/img/intro-carousel/4.jpg');"></div>--%>
                <%--<div class="item" style="background-image: url('Reveal/img/intro-carousel/5.jpg');"></div>--%>
            </div>

        </section>
        <!-- #intro -->

    </asp:Content>




    <asp:Content  runat="server" ContentPlaceHolderID="ContenedorPrincipal">
         


<div id="Cooperativas">
    <section id="about" class="">
      <div class="container">
        <div class="row">
            <div class="col-lg-6 content">
            <h2><span>¿QUÉ ES UNA COOPERATIVA?</span></h2>
            <h3>“Una Cooperativa es una Asociación autónoma de personas que se han unido voluntariamente para hacer frente a sus necesidades y aspiraciones económicas, sociales, y culturales comunes por medio de una empresa de propiedad conjunta y democráticamente controladas”.</h3>
            
          </div>
          <div class="col-lg-6 about-img">
            <img src="Reveal/img/about_coop.jpg" alt="">
          </div>

          
        </div>

      </div>
    </section><!-- #introCooperativa -->

    <!--==========================
      Seccion Cooperativas
    ============================-->
    <section id="services">
      <div class="container">
        <div class="section-header">
          <h2>Cooperativas</h2>
          <p></p>
        </div>

        <div class="row">
            
            <div class="col-lg-6">
            <div class="box wow fadeInLeft">
            <div class="icon"><i class="fa fa-tags"></i></div>
              <h4 class="title"><a href=""><br />Requisitos</a></h4>
             
            </div>
          </div>

          <div class="col-lg-6">
            <div class="box wow fadeInRight">
            <div class="icon"><i class="fa fa-legal"></i></div>
              <h4 class="title"><a href=""><br />Leyes vigentes de cooperativa</a></h4>
             
            </div>
          </div>

          <div class="col-lg-6">
            <div class="box wow fadeInRight">
              <div class="icon"><i class="fa fa-sticky-note-o"></i></div>
              <h4 class="title"><a href="/Inicio.aspx">Instructivo</a></h4>
           
            </div>
          </div>

          <div class="col-lg-6">
            <div class="box wow fadeInLeft" data-wow-delay="0.2s">
              <div class="icon"><i class="fa fa-file-o"></i></div>
              <h4 class="title"><a href="/Documentacion.aspx">Documentos</a></h4>
              
            </div>
          </div>

         
        </div>

      </div>
    </section><!-- #Cooperativas -->
</div>

<div id="Mutuales">
    <section id="about" class="wow fadeInUp">
      <div class="container">
        <div class="row">
            <div class="col-lg-6 about-img">
            <img src="Reveal/img/about_mutuales.jpg" alt="">
            </div>
            <div class="col-lg-6 content">
            <h2><span>¿QUÉ ES UNA MUTUAL?</span></h2>
            <h3>“Una Asociación constituida libremente, sin fines de lucro, por personas inspiradas en la solidaridad, con el objeto de brindarse ayuda recíproca frente a riesgos eventuales o de concurrir a su bienestar material y espiritual, mediante una contribución periódica”.</h3>
            </div>
          </div>

      </div>
    </section><!-- #introMutuales -->
         <!--==========================
      Seccion Mutuales
    ============================-->
    <section id="services">
      <div class="container">
        <div class="section-header">
          <h2>Mutuales</h2>
          <p></p>
        </div>

        <div class="row">
            
            <div class="col-lg-6">
            <div class="box wow fadeInLeft">
            <div class="icon"><i class="fa fa-tags"></i></div>
              <h4 class="title"><a href=""><br />Requisitos</a></h4>
             
            </div>
          </div>

          <div class="col-lg-6">
            <div class="box wow fadeInRight">
            <div class="icon"><i class="fa fa-legal"></i></div>
              <h4 class="title"><a href=""><br />Leyes vigentes de cooperativa</a></h4>
             
            </div>
          </div>

          <div class="col-lg-6">
            <div class="box wow fadeInRight">
              <div class="icon"><i class="fa fa-sticky-note-o"></i></div>
              <h4 class="title"><a href="/Inicio.aspx">Instructivo</a></h4>
           
            </div>
          </div>

          <div class="col-lg-6">
            <div class="box wow fadeInLeft" data-wow-delay="0.2s">
              <div class="icon"><i class="fa fa-file-o"></i></div>
              <h4 class="title"><a href="/Documentacion.aspx">Documentos</a></h4>
              
            </div>
          </div>

           

        </div>

      </div>
    </section><!-- #MUTUALES -->

</div>
<div id="Contactos">
    <!--==========================
      Seccion Contactos
    ============================-->
    <section id="contact" class="wow fadeInUp">
      <div class="container">
        <div class="section-header">
          <h2>Contactanos</h2>
          <p>Por Consultas favor de dirigirse o comunicarse con los siguientes medios detallados a continuación:</p>
        </div>

        <div class="row contact-info">

          <div class="col-md-4">
            <div class="contact-address">
              <i class="ion-ios-location-outline"></i>
              <h3>Direccion:</h3>
              <address>Av. Vélez Sarsfield 2311, CP 5000 – Córdoba</address>
            </div>
          </div>

          <div class="col-md-4">
            <div class="contact-phone">
              <i class="ion-ios-telephone-outline"></i>
              <h3>Teléfono</h3>
              <p><a href="tel:351-4688500 int. 8011">351-4688500 int. 8011</a></p>
            </div>
          </div>

          <div class="col-md-4">
            <div class="contact-email">
              <i class="ion-ios-email-outline"></i>
              <h3>Correo Electrónico</h3>
              <p><a href="mailto:subsecoopymutuales@cba.gob.ar">subsecoopymutuales@cba.gob.ar</a></p>
            </div>
          </div>

        </div>
      </div>

     </section> <!-- #Contactos -->

</div>
</asp:Content>  