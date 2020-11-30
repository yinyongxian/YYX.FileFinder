using System.Collections.Generic;

namespace YYX.FileFinder.Tools
{
    static class HtmlHelper
    {
        public static string CreatHtmlTrOfFolder(string folderName, string path)
        {
            var link = $"<a class=\"a-operation\" href = \"/Folder/Open?path={path}\">打开</a>";
            return CreatHtmlTr(folderName, link);
        }

        public static string CreatHtmlTrOfFile(string fileName, string fileFullName)
        {
            var playLink = GetPlayLink(fileFullName);
            var downloadLink = GetDownloadLink(fileFullName);


            var links = string.Join(" ", new []
            {
                playLink,
                downloadLink
            });

            return CreatHtmlTr(fileName, links);
        }

        private static string CreatHtmlTr(string name, string links)
        {
            return $"<tr><td>{links}{name}</td></tr>";
        }

        public static string GetLink(string link, string text)
        {
            return string.Format("<a href = \"{0}\" style=\"float:none\">{1}</a>", link, text);
        }

        public static string GetOpenFolderLink(string path)
        {
            return $"/Folder/Open?path={path}";
        }

        public static string GetPlayLink(string filePath)
        {
            var link = $"<a class=\"a-operation\" href = \"/Video/Play?filePath={filePath}\">播放</a>";
            return link;
        }

        public static string GetDownloadLink(string filePath)
        {
            var link = $"<a class=\"a-operation\" href = \"/File/Download?filePath={filePath}\">下载</a>";
            return link;
        }
    }
}
