using System;
using System.Windows.Forms;
using YYX.FileFinder.Tools;

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

            AssemblyHelper.Load();
            AdministratorHelper.Run();
        }
    }
}
