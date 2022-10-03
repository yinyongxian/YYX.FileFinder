using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows.Forms;

namespace YYX.FileFinder.Tools
{
    public static class AdministratorHelper
    {
        public static bool IsAdministrator()
        {

            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            var inRole = principal.IsInRole(WindowsBuiltInRole.Administrator);
            return inRole;
        }

        public static void RunAsAdministrator()
        {
           var startInfo = new ProcessStartInfo
           {
               UseShellExecute = true,
               WorkingDirectory = Environment.CurrentDirectory,
               FileName = Application.ExecutablePath,
               Verb = "runas"
           };
           Process.Start(startInfo);
        }

        public static void Run( )
        {
            if (IsAdministrator())
            {
                Application.Run(new MainForm());
            }
            else
            {
                try
                {
                    RunAsAdministrator();
                }
                catch
                {
                    return;
                }

                Application.Exit();
            }
        }
    }
}
