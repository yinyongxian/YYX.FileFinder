using System.Runtime.InteropServices;

namespace YYX.FileFinder
{
    static class HtmlHelper
    {
        public static string CreatHtmlTrOfFolder(string folderName, string path)
        {
            const string operation = "打开";
            var link = GetOpenFolderLink(path);
            return CreatHtmlTr(folderName, link, operation);
        }

        public static string GetOpenFolderLink(string path)
        {
            return string.Format("/Folder/Open?path={0}", path);
        }

        public static string CreatHtmlTrOfFile(string fileName, string fileFullName)
        {
            const string operation = "下载";
            var link = string.Format("/File/Download?filePath={0}", fileFullName);
            return CreatHtmlTr(fileName, link, operation);
        }

        private static string CreatHtmlTr(string name, string link, string operation)
        {
            return string.Format("<tr><td>{0}<a href = \"{1}\">{2}</a></td></tr>", name, link, operation);
        }

        public static string GetLink(string link, string text)
        {
            return string.Format("<a href = \"{0}\" style=\"float:none\">{1}</a>", link, text);
        }
    }
}
