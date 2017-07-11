using SGC.Empresarial.Business;
using SGC.Empresarial.Method;
using SGC.Empresarial.Useful.Modulo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace CMP.Presentation
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App4 : Application
    {
        public App4()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }
        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }

        private void Application_Startup_1(object sender, StartupEventArgs e)
        {
            SGCVariables.AsirClientListModulo = new List<string>() { "ALM", "MNF", "CRM", "VTA", "CMP" };
            SGCVariables.ConectionString = @"Data Source =  DSK-SERVER-01\SYSTEMSSQL2008; DataBase =  ERP_SGC; User Id = demo; Password = demo";

            SGCVariables.ObjESGC_Usuario = new SGC.Empresarial.Entity.ESGC_Usuario()
            {
                IdUsuario = 1,
                Nombres = "Abel",
                ObjESGC_Perfil = new SGC.Empresarial.Entity.ESGC_Perfil()
                {
                    IdPerfil = 0
                },
                ObjESGC_Empresa = new SGC.Empresarial.Entity.ESGC_Empresa()
                {
                    IdEmpresa = 1,
                    RazonSocial = "FUNDO SAN LUIS",
                    Ruc = "20494738423",
                    DireccionFiscal = "Carretera Panamericana Sur KM 213 - El Carmen - Chincha - Ica",
                    Telefono = "998392342"
                }
            };

            var objListConfiguracion = new BSGC_Configuracion().ListGetConfiguracion();
            //InicializeObject.SetListaConfiguracion = new BSGC_Configuracion().ListGetConfiguracion(); 
            //InicializeObject.GetRetencion();
            //Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/Orange.xaml") });
        }
    }
}
