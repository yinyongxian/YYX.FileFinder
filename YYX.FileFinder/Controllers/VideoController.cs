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
                    var videoLink = $"src=/Video/GetVideo?filePath={urlEncodeFilePath}  type=\"video/{extension}\"";
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

            if (File.Exists(filePath) == false)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "文件未找到");
            }

            var range = request.Headers.Range.Ranges.ToArray()[0];
            var rangeFrom = range.From ?? 0;


            var response = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                {
                    response.Content = new StreamContent(fileStream);

                }

                //response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("video/mp4");
                var urlEncodeFilePath = HttpUtility.UrlEncode(filePath);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = urlEncodeFilePath
                };

                response.Content.Headers.ContentLength = fileStream.Length;
                response.Content.Headers.ContentRange = new ContentRangeHeaderValue(rangeFrom, fileStream.Length);
                response.Headers.AcceptRanges.Add("bytes");
                response.Headers.ETag = new EntityTagHeaderValue("\"tag\"");

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

            //String rangeString = request.Headers.Range...getHeader("Range");//如果是video标签发起的请求就不会为null

            //long range = Long.valueOf(rangeString.substring(rangeString.indexOf("=") + 1, rangeString.indexOf("-")));

            //response.Content.Headers.ContentType = .he("Content-Type", "video/mp4");

            //response.setHeader("content-disposition", "attachment;filename=" + URLEncoder.encode("视频文件名称.mp4", "UTF-8"));

            //response.setContentLength(10000);//10000是视频文件的大小，上传文件时都会有这些参数的

            //response.setHeader("Content-Range", String.valueOf(range + (10000 - 1)));//拖动进度条时的断点，其中10000是上面的视频文件大小，改成你的就好

            //response.setHeader("Accept-Ranges", "bytes");

            //response.setHeader("Etag", "W/"9767057 - 1323779115364"");//上传文件时都会有这些参数的

        }
    }
}
