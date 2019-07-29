﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;
using Microsoft.Reporting.WebForms;

namespace CyM
{
    /// <summary>
    /// Página general para mostrar reportes
    /// </summary>
    public partial class ReporteGeneral : System.Web.UI.Page
    {
      
        
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if(!Page.IsPostBack)
            {
                var reporteGral = (ReporteGeneral)Session["ReporteGeneral"];
                if (reporteGral != null)
                {
                    ImprimirReporteGeneral(reporteGral); 
                }
               
            }
            //var mostrarSiguientePagina = Request.QueryString["NextPage"];
            //if (mostrarSiguientePagina == "1")
            //{
            //    Response.Redirect("MisTramites.aspx?Exito=1");
            //}
            
        }
        
        #region CONSTRUCTORES
         
        public ReporteGeneral(string nombreReporte,  TipoArchivoEnum tipo)
        {
            _nombreReporte = nombreReporte;
            _listaParametros = new List<ReportParameter>();
            _listaDataSources = new List<ReportDataSource>();
            _tipoArchivo = tipo;
        }

        public ReporteGeneral()
        {
            _listaParametros = new List<ReportParameter>();
        
        }

        #endregion

        #region PROPIEDADES

        private TipoArchivoEnum _tipoArchivo;
        
        

        private string _nombreReporte;
        public  virtual string NombreReporte
        {
            get { return _nombreReporte; }
            set { _nombreReporte = value; }
        }

        private List<ReportParameter> _listaParametros;
        public virtual List<ReportParameter> ListaParametros
        {
            get { return _listaParametros; }
            set { _listaParametros = value; }
        }


        private List<ReportDataSource> _listaDataSources;
        public virtual List<ReportDataSource> ListaDataSources
        {
            get { return _listaDataSources; }
            set { _listaDataSources = value; }
        }




        #endregion
        
        #region METODOS
        
        /// <summary>
        /// Agrega un parámetro nuevo al reporte
        /// </summary>
        /// <param name="nombreParametro"></param>
        /// <param name="value"></param>
        public void AddParameter(string nombreParametro,string value)
        {
            _listaParametros.Add(new ReportParameter(nombreParametro,value));
        }
        
        public void AddDataSource(string nombreDataSource,object dataSource)
        {
            _listaDataSources.Add(new ReportDataSource(nombreDataSource,dataSource));
        }
        


        private void ImprimirReporteGeneral(ReporteGeneral reporteGral)
        {
            //cargo el diseño del ReporteGeneral del assembly mvp, esta ahi porque no puedo agregar un ReporteGeneral en el proyecto web
            Assembly assembly = Assembly.Load("CyM");
            Stream stream = assembly.GetManifestResourceStream(reporteGral._nombreReporte);

            //variablse para la impresion 
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;



            //creo un reporviewer nuevo para visualizar el ReporteGeneral
            ReportViewer ReportViewer1 = new ReportViewer();
            ReportViewer1.Visible = true;
            //le digo que se renderize en la misma pagina
            ReportViewer1.Page = (System.Web.UI.Page)HttpContext.Current.Handler;
            ReportViewer1.ID = "Test";
            //se setea esto para las imagenes que se le agrega al ReporteGeneral
            ReportViewer1.LocalReport.EnableExternalImages = true;
            ReportViewer1.ProcessingMode = ProcessingMode.Local;

            //se carga la definicion del ReporteGeneral al repoviewer
            ReportViewer1.LocalReport.LoadReportDefinition(stream);

            //se setean los parametros al diseño
            if (reporteGral._listaParametros != null)
                ReportViewer1.LocalReport.SetParameters(reporteGral._listaParametros);
            
            ReportViewer1.LocalReport.DataSources.Clear();
            if (reporteGral.ListaDataSources != null)
            {
                foreach (ReportDataSource report  in reporteGral.ListaDataSources)
                { 
                    //se setea el datasourc 
                    ReportViewer1.LocalReport.DataSources.Add(report);
                }
              
                ReportViewer1.DataBind();
            }


            //esta linea es importante porque es la que renderiza el ReporteGeneral con los parametros asignados
            ReportViewer1.LocalReport.Refresh();

            //var archivoFormulario = new byte[] {};
            //string tipoDeContenedor = "Application/pdf";

            string tipoDescarga = "PDF";

            var nombreCompletoArchivo = "Comprobante_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year ;

            switch (reporteGral._tipoArchivo)
            {
                case TipoArchivoEnum.Excel:
                    tipoDescarga = "Excel";
                    Response.AddHeader("content-disposition", "attachment; filename=" +nombreCompletoArchivo +".xls");
                    break;
            }


            Response.AddHeader("content-disposition", "attachment; filename=" + nombreCompletoArchivo + ".pdf");
            var archivoFormulario = ReportViewer1.LocalReport.Render(
                        tipoDescarga, null, out mimeType, out encoding, 
                        out extension,
                        out streamids, out warnings);
            //tipoDeContenedor = mimeType;



            Response.ContentType = mimeType;
            Response.BinaryWrite(archivoFormulario);
            Response.Buffer = true; 
            Response.End();
 
        }
        
        #endregion
    }
}