﻿using System.IO;
using System.Windows.Forms;

namespace YYX.FileFinder
{
    public static class AutoRun
    {
        private static bool GetRunEnable()
        {
            var filePath = Application.ExecutablePath;
            var fileName = Path.GetFileName(filePath);

            return WindowAutoRun.GetEnable(fileName, filePath);
        }

        private static void SetRunEnable(bool runEnable)
        {
            var filePath = Application.ExecutablePath;
            var fileName = Path.GetFileName(filePath);

            WindowAutoRun.SetEnable(fileName, filePath, runEnable);
        }

        public static void RunWithWindowsStarted()
        {
            var runEnable = GetRunEnable();
            if (!runEnable)
            {
                SetRunEnable(true);
            }
        }
    }
}
