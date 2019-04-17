using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace YYX.FileFinder
{
    public class FileController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Download(string filePath)
        {
            Log4Log.Info(Request.ToString());
            ContentLog.WriteLine(Request.ToString());

            var fileName = Path.GetFileName(filePath);
            if (File.Exists(filePath) == false)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "文件未找到");
            }
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                {
                    response.Content = new StreamContent(fileStream);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = fileName
                    };
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
