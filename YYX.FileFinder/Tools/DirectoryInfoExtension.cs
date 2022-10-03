using System.IO;

namespace YYX.FileFinder.Tools
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
            return $"当前路径&nbsp&nbsp&nbsp{fullHtmlPath}";
        }

        private static string CreatHtmlPath(this FileSystemInfo directoryInfo)
        {
            return directoryInfo == null
                ? "文件夹信息错误"
                : HtmlHelper.GetLink(HtmlHelper.GetOpenFolderLink(directoryInfo.FullName), directoryInfo.Name);
        }
    }
}
