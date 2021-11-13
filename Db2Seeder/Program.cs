using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Db2Seeder
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Analytics.SetEnabledAsync(true);
            AppCenter.Start("18969fc7-c640-4544-8fe7-09542a100eae",
                   typeof(Analytics), typeof(Crashes));


            Application.Run(new Form1());
        }
        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Crashes.TrackError(e.Exception);
           

        }
    }
}
