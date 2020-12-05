using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Http;
using YYX.FileFinder.Tools;

namespace YYX.FileFinder.Controllers
{
    public class VideoController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage Play(HttpRequestMessage request, string filePath)
        {
            Log4Log.Info(Request.ToString());
            ContentLog.WriteLine(Request.ToString());

            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceStream = assembly.GetManifestResourceStream("YYX.FileFinder.video.html");
                if (resourceStream == null)
                {
                    throw new Exception("页面异常");
                }
                var response = request.CreateResponse(HttpStatusCode.OK);
                using (var stream = new StreamReader(resourceStream))
                {
                    var html = stream.ReadToEnd();
                    var urlEncodeFilePath = HttpUtility.UrlEncode(filePath);
                    var videoLink = $"/File/Download?filePath={urlEncodeFilePath}";
                    html = html.Replace("{videoLink}", videoLink);
                 
                    response.Content = new StringContent(html, Encoding.UTF8);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                }

                Log4Log.Info(response.ToString());
                ContentLog.WriteLine(response.ToString());

                return response;
            }
            catch (Exception exception)
            {
                var httpResponseMessage = Request.CreateErrorResponse(HttpStatusCode.NotFound, exception.Message);

                Log4Log.Info(httpResponseMessage.ToString());
                ContentLog.WriteLine(httpResponseMessage.ToString());

                return httpResponseMessage;
            }
        }
    }
}
