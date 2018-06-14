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
            return fullHtmlPath;
        }

        private static string CreatHtmlPath(this FileSystemInfo directoryInfo)
        {
            return directoryInfo == null
                ? "文件夹信息错误"
                : string.Format("<a href = \"{0}\" style=\"float:none\">{1}</a>", HtmlHelper.GetOpenFolderLink(directoryInfo.FullName), directoryInfo.Name);
        }
    }
}
