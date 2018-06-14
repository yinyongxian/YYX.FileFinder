using System;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace YYX.FileFinder
{
    static class Program
    {
        static void Main(string[] args)
        {
            var config = new HttpSelfHostConfiguration("http://localhost:12321");
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
                    Console.WriteLine(@"Started");
                }
                catch (Exception ex)
                {
                    Console.WriteLine( ex.Message);
                }
                Console.ReadLine();
            }
        }
    }
}
