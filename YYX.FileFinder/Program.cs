﻿using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Windows.Forms;

namespace YYX.FileFinder
{
    static class Program
    {
        static void Main()
        {
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

            var thread = new Thread(() =>
                {
                    var config = new HttpSelfHostConfiguration(DomainName.Vlaue);
                    config.Routes.MapHttpRoute(
                        "Default",
                        "{controller}/{action}",
                        new { controller = "Home", action = "Index" }
                    );
                    //config.Routes.MapHttpRoute(
                    //    "Folder",
                    //    "api/{controller}/{action}",
                    //    new { controller = "Folder", action = "Open" }
                    //);
                    //config.Routes.MapHttpRoute(
                    //    "File",
                    //    "api/{controller}/{action}",
                    //    new { controller = "File", action = "Download" }
                    //);

                    using (var httpSelfHostServer = new HttpSelfHostServer(config))
                    {
                        try
                        {
                            httpSelfHostServer.OpenAsync().Wait();
                            Console.WriteLine(@"Started");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            Console.WriteLine(ex.Message);
                        }
                        Console.ReadLine();
                    }
                })
            { IsBackground = true };
            thread.Start();
            Console.ReadLine();
        }
    }
}
