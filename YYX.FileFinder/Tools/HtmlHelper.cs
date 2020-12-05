using System.Collections.Generic;
using System.Web;

namespace YYX.FileFinder.Tools
{
    static class HtmlHelper
    {
        public static string CreateHtmlTrOfFolder(string folderName, string path)
        {
            var link = $"<a class=\"a-operation\" href = \"/Folder/Open?path={path}\">打开</a>";
            return CreateHtmlTr(folderName, link);
        }

        public static string CreateHtmlTrOfFile(string fileName, string fileFullName)
        {
            var encodeFileFullName = HttpUtility.UrlEncode(fileFullName);

            var playLink = GetPlayLink(encodeFileFullName);
            var downloadLink = GetDownloadLink(encodeFileFullName);

            var links = string.Join(" ", new []
            {
                playLink,
                downloadLink
            });

            return CreateHtmlTr(fileName, links);
        }

        private static string CreateHtmlTr(string name, string links)
        {
            return $"<tr><td>{links}{name}</td></tr>";
        }

        public static string GetLink(string link, string text)
        {
            return $"<a href = \"{link}\" style=\"float:none\">{text}</a>";
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
