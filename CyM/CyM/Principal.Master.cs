using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using CyM.Models;
using Microsoft.Ajax.Utilities;


namespace CyM
{
    public partial class Principal : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblEntorno.Text = EntornoDeEjecucion;
            lblVersion.Text = VersionProducto;
        }

        public String UltimoAcceso;
        public String RolUsuario { get; set; }

        /*Nota (Facundo Álvarez) : se inicializa el evento Init porque surge la necesidad de ejecutar primero el evento Init de la master y luego cualquier load de las Paginas que utilizan la master.*/

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.Cookies["CiDi"] != null)
            {
                ObtenerUsuario();
               

            }
            else
            {
                Response.Redirect(ConfigurationManager.AppSettings["CiDiUrl"] + "?url=" +
                                  ConfigurationManager.AppSettings["Url_Retorno"] + "&app=" +
                                  ConfigurationManager.AppSettings["CiDiIdAplicacion"]);
            }
        }

        public string EntornoDeEjecucion
        {
            get
            {
                return ConfigurationManager.AppSettings["Entorno"].ToString();
            }
        }

        public string VersionProducto
        {
            get
            {
                return ConfigurationManager.AppSettings["VersionProducto"].ToString();
            }
        }


        public Usuario UsuarioCidiLogueado 
        {
            get
            {
                return Session["UsuarioCiDiLogueado"] == null ? new Usuario() : (Usuario) Session["UsuarioCiDiLogueado"];

            }
            set
            {
                Session["UsuarioCiDiLogueado"] = value;
            }
        }

        
        
        protected void ObtenerUsuario()
        {
            //if (UsuarioCidiLogueado.Respuesta.Resultado != null)
            //{
            //    return;
            //}
            string urlapi = WebConfigurationManager.AppSettings["CiDiUrl"].ToString();
            Entrada entrada = new Entrada();
            entrada.IdAplicacion = Config.CiDiIdAplicacion;
            entrada.Contrasenia = Config.CiDiPassAplicacion;
            entrada.HashCookie = Request.Cookies["CiDi"].Value.ToString();
            entrada.TimeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            entrada.TokenValue = Config.ObtenerToken_SHA1(entrada.TimeStamp);

            UsuarioCidiLogueado = Config.LlamarWebAPI<Entrada, Usuario>(APICuenta.Usuario.Obtener_Usuario_Aplicacion,
                entrada);

            if (UsuarioCidiLogueado.Respuesta.Resultado == Config.CiDi_OK)
            {
                //var getRolUsuario = Bl.BlGetRolUsuario(UsuarioCidiLogueado.CUIL);
                //switch (getRolUsuario)
                //{
                //    case "0":
                //        //Bl.BlActualizarRolUsuario(UsuarioCidiLogueado.CUIL, 0);
                //        Bl.BlAgregarUsuario(UsuarioCidiLogueado.CUIL, 2);
                //        break;
                //    case "1":
                //        Bl.BlActualizarRolUsuario(UsuarioCidiLogueado.CUIL, 1);
                //        break;
                //}

                if (UsuarioCidiLogueado.Id_Estado != null)
                {
                    int nivelCidi = (int) UsuarioCidiLogueado.Id_Estado;
                        //nivel de cidi '0' no verificado nivel 1 si confirmacion de mail,id=1 nivel 1, id=2 nivel 2 confirmado
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

                    Label varUsuarioCIDI = lblUsuarioCIDI;
                    varUsuarioCIDI.Text = UsuarioCidiLogueado.NombreFormateado + ", Nivel " + nivelCidi + " " +
                                          estadoCidi;

                    //    //var rolUsuario = "";
                    //    //Bl.BlActualizarAccesoUsuario(UsuarioCidiLogueado.CUIL, out rolUsuario, out UltimoAcceso);

                    //    ////si el USUARIO NO TIENE ASIGNADO UN ROL.
                    //    //if (string.IsNullOrEmpty(rolUsuario) || rolUsuario == "SIN ASIGNAR")
                    //    //    Response.Redirect("Inicio.aspx");

                    //    //RolUsuario = rolUsuario;

                    //    //if (UltimoAcceso != "ERROR")
                    //    //{
                    //    //    lblUltimoAcceso.Text = UltimoAcceso;
                    //    //    lblRolUsuario.Text = RolUsuario;
                    //    //}

                    //    //switch (RolUsuario)
                    //    //{
                    //    //    case "ADMIN":
                    //    //        liAdministracion.Visible = true;
                    //    //        break;
                    //    //    case "SOLICITANTE":
                    //    //        liAdministracion.Visible = false;
                    //    //        break;

                    //    //}
                    //}



                }
               
            }
            else
            {
                Response.Redirect(ConfigurationManager.AppSettings["CiDiUrl"] + "?url=" +
                                    ConfigurationManager.AppSettings["Url_Retorno"] + "&app=" +
                                    ConfigurationManager.AppSettings["CiDiIdAplicacion"]);
            }


        }
    }
}