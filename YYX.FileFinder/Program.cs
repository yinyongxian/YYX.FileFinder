using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Windows.Forms;

namespace YYX.FileFinder
{
    /// <summary>
    /// 文件查找器
    /// 尹永贤
    /// yinyongxian@qq.com
    /// 2018-6-15 11:26:20
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppDomain.CurrentDomain.AssemblyResolve += (sender, a) =>
            {
                var nameSpace = typeof(Program).Namespace;
                var resourceName = string.Format("{0}.Resources.{1}.dll", nameSpace, new AssemblyName(a.Name).Name);
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        var assemblyData = new byte[stream.Length];
                        stream.Read(assemblyData, 0, assemblyData.Length);
                        return Assembly.Load(assemblyData);
                    }
                    else
                    {
                        throw new FileLoadException(resourceName);
                    }
                }
            };

            AutoRun.RunWithWindowsStarted();

            var thread = new Thread(() =>
                {
                    var config = new HttpSelfHostConfiguration(DomainName.Vlaue);
                    config.Routes.MapHttpRoute(
                        "Default",
                        "{controller}/{action}",
                        new { controller = "Home", action = "Index" }
                    );

                    using (var httpSelfHostServer = new HttpSelfHostServer(config))
                    {
                        try
                        {
                            httpSelfHostServer.OpenAsync().Wait();
                            IpaddressHelper.DisplayIpaddress();
                            Console.WriteLine(@"Started");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            Console.WriteLine(ex.Message);
                        }
                        Console.ReadLine();

                        while (true)
                        {
                            Thread.Sleep(10);
                        }
                    }
                })
            { IsBackground = true };
            thread.Start();

            Application.Run(new MainForm());
        }
    }
}
