using System.IO;
using System.Windows.Forms;

namespace YYX.FileFinder.Tools
{
    public static class AutoRun
    {
        public static bool GetRunEnable()
        {
            var filePath = Application.ExecutablePath;
            var fileName = Path.GetFileName(filePath);

            return WindowAutoRun.GetEnable(fileName, filePath);
        }

        public static void SetRunEnable(bool runEnable)
        {
            var filePath = Application.ExecutablePath;
            var fileName = Path.GetFileName(filePath);

            WindowAutoRun.SetEnable(fileName, filePath, runEnable);
        }
    }
}
