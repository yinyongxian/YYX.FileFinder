using System;
using System.IO;
using System.Reflection;
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

                    throw new FileLoadException(resourceName);
                }
            };

            Application.Run(new MainForm());
        }
    }
}
