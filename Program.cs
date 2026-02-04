using System;
using System.Net;
using System.Windows.Forms;

namespace AYGEST.Updater
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // GitHub precisa TLS 1.2 na maior parte dos Windows
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UpdaterForm(args));
        }
    }
}
