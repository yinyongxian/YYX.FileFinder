using System;
using System.Diagnostics;
using System.Threading;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Windows.Forms;

namespace YYX.FileFinder
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            ContentLog.Initialize(richTextBoxContent);

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
                            ContentLog.WriteLine(@"Started");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            ContentLog.WriteLine(ex.Message);
                        }

                        while (true)
                        {
                            Thread.Sleep(10);
                        }
                    }
                })
            { IsBackground = true };
            thread.Start();
        }

        private void richTextBoxContent_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }
    }
}
