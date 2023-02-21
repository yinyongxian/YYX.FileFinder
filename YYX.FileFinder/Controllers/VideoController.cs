using System;
using System.IO;
using System.Linq;
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

            var extension = Path.GetExtension(filePath).TrimStart('.');

            try
            {
                Stream resourceStream = GetStream();
                var response = request.CreateResponse(HttpStatusCode.OK);
                using (var stream = new StreamReader(resourceStream))
                {
                    var html = stream.ReadToEnd();
                    var urlEncodeFilePath = HttpUtility.UrlEncode(filePath);
                    var videoLink = $"/Video/GetVideo?filePath={urlEncodeFilePath}\" type=\"video/{extension}";
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

        private static Stream GetStream()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceStream = assembly.GetManifestResourceStream("YYX.FileFinder.video.html");
            if (resourceStream == null)
            {
                throw new Exception("页面异常");
            }

            return resourceStream;
        }

        [HttpGet]
        public HttpResponseMessage GetVideo(HttpRequestMessage request, string filePath)
        {
            Log4Log.Info(Request.ToString());
            ContentLog.WriteLine(Request.ToString());

            long from = 0;
            if (request.Headers != null &&
                request.Headers.Range   != null &&
                request.Headers.Range.Ranges.Count > 0)
            {
                var range = request.Headers.Range.Ranges.ToArray()[0];
                from = range.From ?? 0;
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 300 * 1024);
                response.Content = new StreamContent(fileStream);

                var extension = Path.GetExtension(filePath).TrimStart('.');
                var urlEncodeFilePath = HttpUtility.UrlEncode(filePath, Encoding.UTF8);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue($"video/{extension}");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = urlEncodeFilePath
                };

                var end = fileStream.Length - 1;
                long byteCount = 1_000;
                long to = end - from > byteCount ? from + byteCount : end;
                var contentRangeHeaderValue = new ContentRangeHeaderValue(from, to, fileStream.Length);
                response.Content.Headers.ContentRange = contentRangeHeaderValue;
                response.Headers.AcceptRanges.Add("bytes");

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
