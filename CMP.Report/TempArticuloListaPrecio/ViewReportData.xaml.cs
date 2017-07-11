using CMP.Business;
using CMP.Entity;
using CMP.Useful.Modulo;
using ComputerSystems.WPF;
using Microsoft.Reporting.WinForms;
using SGC.Empresarial.Useful.Modulo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CMP.Reports.TempArticuloListaPrecio
{
    /// <summary>
    /// Lógica de interacción para ViewReportData.xaml
    /// </summary>
    public partial class ViewReportData 
    {
        public ViewReportData(string IdCategoria, string IdSubCategoria,  string IdMarca, string IdArticulo, string IdProveedor, string FechaDesde,string FechaHasta , List<string> listFilter)
        {
            InitializeComponent();
            var rds = new ReportDataSource();
            rds.Name = "Prueba";
            var Lis = new BCMP_TempArticuloListaPrecio().ListArticuloListaPrecio_Preview(IdCategoria, IdSubCategoria, IdMarca, IdArticulo, IdProveedor, FechaDesde, FechaHasta);
            rds.Value = Lis;
            var rds1 = new ReportDataSource();
            rds1.Name = "Title";
            //rds1.Value = new BCMP_TempArticuloListaPrecio().GetFirstCompany();

            var Filtering = "Filtrado por:  ";

            listFilter.ForEach(x =>
            {
                Filtering += x + (listFilter.Count > 1 ? " | " : "");
            });
            var rds2 = new ReportDataSource();
            rds2.Name = "Filter";
            var listData = new List<ECMP_FilterPropertyData>();
            listData.Add(new ECMP_FilterPropertyData { Filter = Filtering });
            rds2.Value = listData;

            _reportViewer.Reset();
            _reportViewer.LocalReport.DataSources.Clear();
            _reportViewer.LocalReport.DataSources.Add(rds);
            _reportViewer.LocalReport.DataSources.Add(rds1);
            _reportViewer.LocalReport.DataSources.Add(rds2);
            _reportViewer.ProcessingMode = ProcessingMode.Local;
            _reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            _reportViewer.LocalReport.ReportEmbeddedResource = "CMP.Reports.Files.RptListaPrecio.rdlc";
            //_reportViewer.LocalReport.ReportPath = @"Files\RptListaPrecio.rdlc";
            _reportViewer.ZoomMode = ZoomMode.PageWidth;
            _reportViewer.RefreshReport();
        }

        private void btnEnviar_Click(object sender, RoutedEventArgs e)
        {
            windowsFormsHost1.Visibility = System.Windows.Visibility.Hidden;
            string LocationFile = string.Empty;
            string strOutMessage = string.Empty;
            CmpMessageBox.Proccess("Creando archivo", "por favor espere..",
            () =>
            {
                try
                {
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string extension;
                    LocationFile = System.IO.Path.GetTempPath() + "Orden - " + DateTime.Now.ToString("dd.MM.yyyy") + ".pdf";

                    byte[] bytes = _reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                    FileStream fs = new FileStream(LocationFile, FileMode.Create);

                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();

                }
                catch (Exception ex)
                {
                    strOutMessage = ex.Message;
                }
            },
            () =>
            {
                if ((strOutMessage.Length > 0))
                {
                    CmpMessageBox.Show(CMPMensajes.TitleMessage, strOutMessage, CmpButton.Aceptar, () =>
                    {
                        windowsFormsHost1.Visibility = System.Windows.Visibility.Visible;
                    });
                }
                else
                {
                    windowsFormsHost1.Visibility = System.Windows.Visibility.Visible;
                    SendEmail ObjSendEmail = new SendEmail();
                    ObjSendEmail.SetFile = LocationFile;
                    ObjSendEmail.Show();
                }
            });
        }
    }
}
