using SGC.Empresarial.Business;
using SGC.Empresarial.Method;
using SGC.Empresarial.Presentation;
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
    public partial class App : Application
    {
        public App()
        {
            //this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }
        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }

        private void Application_Startup_1(object sender, StartupEventArgs e)
        {
            SGCVariables.AsirClientListModulo = new List<string>() { "ALM", "MNF", "CRM", "VTA", "CMP" };
            SGCVariables.ConectionString = @"Data Source =  192.168.0.40\SYSTEMSSQL2012R2; DataBase = ERP_SGC_LOCAL; User Id = sgc_local; Password = sgclocal2016";
            //SGCVariables.ConectionString = @"Data Source=LTP-SYSTEMS-02\SQL2012R2EXP; DataBase = ERP_SGC; User Id = sa; Password = solutions01+";

            SGCVariables.ObjESGC_Usuario = new SGC.Empresarial.Entity.ESGC_Usuario()
            {
                IdUsuario = 1,
                Nombres = "Abel",
                ObjESGC_Perfil = new SGC.Empresarial.Entity.ESGC_Perfil()
                {
                    IdPerfil = 1
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

            InicializeConfiguracion.Load();
        }
    }
}
