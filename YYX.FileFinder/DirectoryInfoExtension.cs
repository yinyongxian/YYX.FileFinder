using System.IO;

namespace YYX.FileFinder
{
    public static class DirectoryInfoExtension
    {
        public static string GetFullHtmlPath(this DirectoryInfo directoryInfo)
        {
            var fullHtmlPath = string.Empty;
            while (true)
            {
                var htmlPath = directoryInfo.CreatHtmlPath();
                fullHtmlPath = htmlPath + fullHtmlPath;
                directoryInfo = directoryInfo.Parent;
                if (directoryInfo == null)
                {
                    break;
                }
                else if(directoryInfo.Parent != null)
                {
                    fullHtmlPath = "\\" + fullHtmlPath;
                }
            }
            return string.Format("当前路径&nbsp{0}", fullHtmlPath);
        }

        private static string CreatHtmlPath(this FileSystemInfo directoryInfo)
        {
            return directoryInfo == null
                ? "文件夹信息错误"
                : HtmlHelper.GetLink(HtmlHelper.GetOpenFolderLink(directoryInfo.FullName), directoryInfo.Name);
        }
    }
}
