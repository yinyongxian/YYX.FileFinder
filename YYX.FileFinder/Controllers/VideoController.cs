using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using YYX.FileFinder.Tools;

namespace YYX.FileFinder.Controllers
{
    public class VideoController : ApiController
    {
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Play(HttpRequestMessage request, string filePath)
        {
            Log4Log.Info(Request.ToString());
            ContentLog.WriteLine(Request.ToString());

            if (File.Exists(filePath))
            {
                var fileInfo = new FileInfo(filePath);
                if (fileInfo.Length > int.MaxValue)
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
            }

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

        [System.Web.Http.HttpGet]
        public async Task<HttpResponseMessage> GetVideo(HttpRequestMessage request, string filePath)
        {
            Log4Log.Info(Request.ToString());
            ContentLog.WriteLine(Request.ToString());

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                var videoStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                videoStream.Seek(0, SeekOrigin.Begin);

                long from = 0, to = videoStream.Length, length = videoStream.Length;
                if (request.Headers != null &&
                    request.Headers.Range != null &&
                    request.Headers.Range.Ranges.Count > 0 &&
                    request.Headers.Range.Ranges.FirstOrDefault()?.From > 0)
                {
                    var range = request.Headers.Range.Ranges.ToArray()[0];
                    from = range.From ?? 0;
                    to = (range.To.HasValue && range.To > 0) ? range.To.Value : videoStream.Length - 1;
                    videoStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite,
                        bufferSize: 4096, options: FileOptions.SequentialScan | FileOptions.Asynchronous)
                    {
                        //Position = from
                    };
                    response.StatusCode = HttpStatusCode.PartialContent;
                }

                var memoryStream = new MemoryStream();
                using (videoStream)
                {
                    await videoStream.CopyToAsync(memoryStream);
                }

                memoryStream.Position = from;

                response.Content = new StreamContent(memoryStream);

                response.Content.Headers.ContentRange = new ContentRangeHeaderValue(from, to, memoryStream.Length);
                response.Headers.AcceptRanges.Add("bytes");

                response.Content.Headers.ContentLength = memoryStream.Length;

                var extension = Path.GetExtension(filePath).TrimStart('.');
                var urlEncodeFilePath = HttpUtility.UrlEncode(filePath, Encoding.UTF8);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue($"video/{extension}");

                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = urlEncodeFilePath
                };

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
