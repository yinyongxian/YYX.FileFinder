namespace YYX.FileFinder
{
    static class HtmlHelper
    {
        public static string CreatHtmlTrOfFolder(string folderName, string link)
        {
            const string operation = "打开";
            return CreatHtmlTr(folderName, link, operation);
        }
        public static string CreatHtmlTrOfFile(string fileName, string fileFullName)
        {
            const string operation = "下载";
            var link = string.Format("Download/DownloadFile?filePath={0}", fileFullName);
            return CreatHtmlTr(fileName, link, operation);
        }

        private static string CreatHtmlTr(string name, string link, string operation)
        {
            return string.Format("<tr><td>{0}<a href = \"{1}\" >{2}</a></td></tr>", name, link, operation);
        }
    }
}
