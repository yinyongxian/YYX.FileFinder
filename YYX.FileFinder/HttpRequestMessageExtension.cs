using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using YYX.FileFinder.Tools;

namespace YYX.FileFinder
{
    public static class HttpRequestMessageExtension
    {
        public static HttpResponseMessage Response(this HttpRequestMessage request, string path)
        {
            try
            {
                var directoryInfo = new DirectoryInfo(path);
                var directoryInfos = directoryInfo.GetDirectories();
                var fileInfos = directoryInfo.GetFiles();
                var assembly = Assembly.GetExecutingAssembly();
                var resourceStream = assembly.GetManifestResourceStream("YYX.FileFinder.index.html");
                if (resourceStream == null)
                {
                    throw new Exception("页面异常");
                }
                var response = request.CreateResponse(HttpStatusCode.OK);
                using (var stream = new StreamReader(resourceStream))
                {
                    var html = stream.ReadToEnd();
                    var htmlTrOfFolders = directoryInfos.Select(item => HtmlHelper.CreateHtmlTrOfFolder(item.Name, item.FullName));
                    var htmlTrOfFiles = fileInfos.Select(item => HtmlHelper.CreateHtmlTrOfFile(item.Name, item.FullName));

                    var logicalDrives = Environment.GetLogicalDrives();
                    var openFolderLinks = string.Join("&nbsp&nbsp&nbsp", logicalDrives.Select(item => HtmlHelper.GetLink(HtmlHelper.GetOpenFolderLink(item), item)));
                    var selectLogicalDrive = $"{openFolderLinks}<br><br>";
                    var fullHtmlPath = directoryInfo.GetFullHtmlPath();

                    var totalHtmlTr = string.Join(Environment.NewLine, htmlTrOfFolders.Concat(htmlTrOfFiles).Concat(new[] { selectLogicalDrive, fullHtmlPath }));
                    html = html.Replace("content", totalHtmlTr);

                    response.Content = new StringContent(html, Encoding.UTF8);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                }
                return response;
            }
            catch (Exception exception)
            {
                return request.CreateErrorResponse(HttpStatusCode.NotFound, exception.Message);
            }
        }
    }
}
