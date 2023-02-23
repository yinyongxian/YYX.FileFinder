using System;
using System.Diagnostics;
using System.Threading;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Windows.Forms;
using YYX.FileFinder.Tools;

namespace YYX.FileFinder
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Disposed += (sender, args) =>
            {
                INetFwManger.NetFwDelApps(Application.ExecutablePath);
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            ContentLog.Initialize(richTextBoxContent);

            var autoRunEnable = AutoRun.GetRunEnable();
            AutoRunToolStripMenuItem.Checked = autoRunEnable;

            var thread = new Thread(() =>
                {
                    var config = new HttpSelfHostConfiguration(Domain.Value);
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

                            Log4Log.Info(@"Started");
                            ContentLog.WriteLine(@"Started");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());

                            Log4Log.Info(ex.ToString());
                            ContentLog.WriteLine(ex.ToString());
                        }

                        while (true)
                        {
                            Thread.Sleep(10);
                        }
                    }
                    // ReSharper disable once FunctionNeverReturns
                })
            { IsBackground = true };
            thread.Start();
        }

        private void richTextBoxContent_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void AutoRunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var autoRun = AutoRunToolStripMenuItem.Checked;
            var newAutoRun = !autoRun;
            AutoRun.SetRunEnable(newAutoRun);
            var runEnable = AutoRun.GetRunEnable();
            AutoRunToolStripMenuItem.Checked = runEnable;
        }

        
    }
}
