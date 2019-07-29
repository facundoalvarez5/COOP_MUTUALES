using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BlCyM;
using CyM.Models;

namespace CyM
{
    public partial class SiteMaster : MasterPage
    {

        public ReglaDeNegocios Bl = new ReglaDeNegocios();

        public String UltimoAcceso;
        public String RolUsuario { get; set; }

        /*Nota (Facundo Álvarez) : se inicializa el evento Init porque surge la necesidad de ejecutar primero el evento Init de la master y luego cualquier load de las Paginas que utilizan la master.*/
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.Cookies["CiDi"] != null)
            {
                ObtenerUsuario();
                //CargarDatosInicialesGenerales();

            }
            else
            {
                Response.Redirect(ConfigurationManager.AppSettings["CiDiUrl"] + "?url=" + ConfigurationManager.AppSettings["Url_Retorno"] + "&app=" + ConfigurationManager.AppSettings["CiDiIdAplicacion"]);
            }
        }

        //private void CargarDatosInicialesGenerales()
        //{
        //    DataTable dt = Bl.BlGetParametrosGral();

        //    var gralEsquema = "";
        //    var gralIdConceptoAlta = "";
        //    var gralIdConceptoReempadronamiento = "";
        //    var gralFecDesdeConceptoAlta = "";
        //    var gralFecDesdeConceptoReempadronamiento = "";

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        switch (row["ID_PARAMETRO_GRAL"].ToString())
        //        {
        //            case "1"://P:PRODCCIÓN, D:DESARROLLO 
        //                gralEsquema = row["VALOR"].ToString();
        //                break;
        //            case "2":
        //                gralIdConceptoAlta = row["VALOR"].ToString();
        //                break;
        //            case "3":
        //                gralIdConceptoReempadronamiento = row["VALOR"].ToString();
        //                break;

        //            case "4":
        //                gralFecDesdeConceptoAlta = row["VALOR"].ToString();
        //                break;

        //            case "5":
        //                gralFecDesdeConceptoReempadronamiento = row["VALOR"].ToString();
        //                break;

        //        }
        //    }

        //    /* Creo el objeto Singleton por unica vez y luego se utiliza en todos lados.*/
        //    //SingletonParametroGeneral.GetInstance(gralEsquema, gralIdConceptoAlta, gralIdConceptoReempadronamiento,
        //    //    gralFecDesdeConceptoAlta, gralFecDesdeConceptoReempadronamiento);
        //}

        protected void Page_Load(object sender, EventArgs e)
        {

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

        public Usuario UsuarioCidiRep
        {
            get
            {
                return Session["UsuarioCidiRep"] == null ? new Usuario() : (Usuario)Session["UsuarioCidiRep"];

            }
            set
            {
                Session["UsuarioCidiRep"] = value;
            }
        }

       protected void ObtenerUsuario()
        {
            string urlapi = WebConfigurationManager.AppSettings["CiDiUrl"].ToString();
            Entrada entrada = new Entrada();
            entrada.IdAplicacion = Config.CiDiIdAplicacion;
            entrada.Contrasenia = Config.CiDiPassAplicacion;
            entrada.HashCookie = Request.Cookies["CiDi"].Value.ToString();
            entrada.TimeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            entrada.TokenValue = Config.ObtenerToken_SHA1(entrada.TimeStamp);



            UsuarioCidiLogueado = Config.LlamarWebAPI<Entrada, Usuario>(APICuenta.Usuario.Obtener_Usuario_Aplicacion, entrada);

            if (UsuarioCidiLogueado.Respuesta.Resultado == Config.CiDi_OK)
            {


                if (UsuarioCidiLogueado.Id_Estado != null)
                {
                    int nivelCidi = (int)UsuarioCidiLogueado.Id_Estado;//nivel de cidi '0' no verificado nivel 1 si confirmacion de mail,id=1 nivel 1, id=2 nivel 2 confirmado
                    String estadoCidi = "";
                    switch (nivelCidi)
                    {
                        case 0:
                            estadoCidi = "no confirmado";
                            break;
                        case 1:
                            estadoCidi = "confirmado";
                            break;
                        case 2:
                            estadoCidi = "no verificado";
                            break;
                        case 3:
                            estadoCidi = "verificado";
                            break;
                    }

                   // Label varUsuarioCIDI = lblUsuarioCIDI;
                   // varUsuarioCIDI.Text = UsuarioCidiLogueado.NombreFormateado + ", Nivel " + nivelCidi + " " + estadoCidi;

                    var rolUsuario = "";
                    //Bl.BlActualizarAccesoUsuario(UsuarioCidiLogueado.CUIL, out rolUsuario, out UltimoAcceso);

                    //si el USUARIO NO TIENE ASIGNADO UN ROL.
                    //if (string.IsNullOrEmpty(rolUsuario))
                    //    Response.Redirect("SifcosWeb.aspx");

                    //RolUsuario = rolUsuario;

                    //if (UltimoAcceso != "ERROR")
                    //{
                    //    lblUltimoAcceso.Text = UltimoAcceso;
                    //    lblRolUsuario.Text = RolUsuario;
                    //}

                    //switch (RolUsuario)
                    //{
                    //    case "Boca de Recepcion":
                    //        liBocaMinisterio.Attributes.Add("Class", "cerrar");
                    //        liConfiguracion.Attributes.Add("Class", "cerrar");
                    //        liPanelControl.Attributes.Add("Class", "cerrar");
                    //        liLiberarTasas.Attributes.Add("Class", "cerrar");
                    //        liSistemas.Attributes.Add("Class", "cerrar");
                    //        break;
                    //    case "Boca de Ministerio":
                    //        liBocaRecepcion.Attributes.Add("Class", "cerrar");
                    //        liConfiguracion.Attributes.Add("Class", "cerrar");
                    //        liMisTramites.Attributes.Add("Class", "cerrar");
                    //        liPanelControl.Attributes.Add("Class", "cerrar");
                    //        liLiberarTasas.Attributes.Add("Class", "cerrar");
                    //        liSistemas.Attributes.Add("Class", "cerrar");
                    //        break;
                    //    case "Secretaria de comercio":
                    //        liBocaRecepcion.Attributes.Add("Class", "cerrar");
                    //        liMisTramites.Attributes.Add("Class", "cerrar");
                    //        liSistemas.Attributes.Add("Class", "cerrar");
                    //        break;
                    //    case "Gestor":
                    //        liBocaRecepcion.Attributes.Add("Class", "cerrar");
                    //        liBocaMinisterio.Attributes.Add("Class", "cerrar");
                    //        liReportes.Attributes.Add("Class", "cerrar");
                    //        liConfiguracion.Attributes.Add("Class", "cerrar");
                    //        liPanelControl.Attributes.Add("Class", "cerrar");
                    //        liLiberarTasas.Attributes.Add("Class", "cerrar");
                    //        liSistemas.Attributes.Add("Class", "cerrar");
                    //        break;
                    //    case "Administrador General":

                    //        break;
                    //}
                }



            }
            else
            {
                Response.Redirect(ConfigurationManager.AppSettings["CiDiUrl"] + "?url=" + ConfigurationManager.AppSettings["Url_Retorno"] + "&app=" + ConfigurationManager.AppSettings["CiDiIdAplicacion"]);
            }
        }

        //public void SetTituloPaginaContenido(string valor)
        //{
        //    lblTituloPaginaContenido.Text = valor;
        //}

        //public string TituloPaginaContenido
        //{
        //    get
        //    {
        //        return lblTituloPaginaContenido.Text;
        //    }
        //    set
        //    {
        //        lblTituloPaginaContenido.Text = value;
        //    }
        //}

      
    }

}