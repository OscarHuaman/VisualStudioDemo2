
using CMP.Useful.Modulo;
using ComputerSystems;
using ComputerSystems.WPF;
using Microsoft.Reporting.WinForms;
using System;
using System.Linq;
using System.IO;
using System.Reflection;
namespace CMP.Reports
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainRerport 
    {
        public MainRerport()
        {
            InitializeComponent();
        }

        public void InitializeMainRerport(string NameReport, string NameDataSources, object Mylist, string[] Parametro)
        {
            _reportViewer.Reset();
            _reportViewer.LocalReport.DataSources.Clear();
            _reportViewer.ProcessingMode = ProcessingMode.Local;
            _reportViewer.LocalReport.ReportEmbeddedResource = NameReport;
            _reportViewer.LocalReport.DataSources.Add(new ReportDataSource(NameDataSources, Mylist));
            _reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            _reportViewer.ZoomMode = ZoomMode.Percent;
            var vrEstado = Parametro.ToArray();
            bool imprimir = true;
            vrEstado.ToList().ForEach((x) =>
            {
                if (x.ToString().Split('|').ElementAt(1) == "PENDIENTE")
                {
                    imprimir = false;
                }
            });
            btnEnviar.IsEnabled = imprimir;

            if (Parametro != null)
            {
                ReportParameter[] parameters = new ReportParameter[Parametro.Length];

                int Items = 0;
                foreach (var Parameters in Parametro)
                {
                    string[] ParametersList = Parameters.Split("|".ToCharArray());

                    parameters[Items] = new ReportParameter(ParametersList[0], ParametersList[1]);
                    Items++;
                }
                _reportViewer.LocalReport.SetParameters(parameters);
            }
        }

        private void MetroWindow_Loaded_1(object sender, System.Windows.RoutedEventArgs e)
        {
            _reportViewer.RefreshReport();
        }

        private void btnEnviar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string LocationFile = string.Empty;

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

                SendEmail ObjSendEmail = new SendEmail();
                ObjSendEmail.SetFile = LocationFile;
                ObjSendEmail.Show();
            }
            catch (Exception ex)
            {
                CmpMessageBox.Show(CMPMensajes.TitleMessage, ex.Message, CmpButton.Aceptar);
            }


        }
    }
}
