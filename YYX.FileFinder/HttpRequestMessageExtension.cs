using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

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
                    var htmlTrOfFolders = directoryInfos.Select(item => HtmlHelper.CreatHtmlTrOfFolder(item.Name, "#"));
                    var htmlTrOfFiles = fileInfos.Select(item => HtmlHelper.CreatHtmlTrOfFile(item.Name, item.FullName));
                    var totalHtmlTr = string.Join(Environment.NewLine, htmlTrOfFolders.Concat(htmlTrOfFiles));
                    html = html.Replace("trList", totalHtmlTr);
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
