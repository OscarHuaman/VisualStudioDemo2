namespace CMP.Reports
{
    using CMP.Useful.Modulo;
    using ComputerSystems;
    using ComputerSystems.WPF;
    using System;
    using System.IO;

    public partial class SendEmail
    {
        private string strFile = string.Empty;
        public string SetFile 
        {
            set { strFile = value; }
        }

        public SendEmail()
        {
            InitializeComponent();
        }

        private void MetroWindow_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (File.Exists(strFile))
            {
                File.Delete(strFile);
            }
        }

        private void btnCancelar_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnEnviar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ValidaDatos())
            {
                return;
            }

            CmpMail objMail = new CmpMail("mail.computersystems.com.pe", "notificaciones@computersystems.com.pe", "?du0@v31@LbE", 25);
            objMail.AgregarArchivos(strFile);
            objMail.ValorDelReceptor(txtCorreo.Text, txtAsunto.Text, txtMensaje.Text);

            string strOutMessage = string.Empty;
            CmpMessageBox.Proccess("Enviando archivo", "por favor espere..",
            () =>
            {
                try
                {
                    objMail.EnviarEmail();
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
                    CmpMessageBox.Show(CMPMensajes.TitleMessage, strOutMessage, CmpButton.Aceptar);
                }
                else
                {
                    CmpMessageBox.Show(CMPMensajes.TitleMessage, "Correo enviado exitosamente.", CmpButton.Aceptar, () =>
                    {
                        this.Close();
                    });
                }
            });
        }

        private bool ValidaDatos() 
        {
            bool blEstado = false;
            string strOutMessageError = string.Empty;

            if (CmpRegex.validateCorreo(txtCorreo.Text, out strOutMessageError))
            {
                CmpMessageBox.Show(CMPMensajes.TitleMessage, strOutMessageError, CmpButton.Aceptar);
            }
            else if (txtAsunto.Text.Trim().Length == 0)
            {
                CmpMessageBox.Show(CMPMensajes.TitleMessage, "Ingrece Un Asunto", CmpButton.Aceptar);
            }
            else if (txtMensaje.Text.Trim().Length == 0)
            {
                CmpMessageBox.Show(CMPMensajes.TitleMessage, "Ingrece Un Mensaje", CmpButton.Aceptar);
            }

            return blEstado;
        }
    }
}
