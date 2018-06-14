using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace YYX.FileFinder
{
    public class DownloadController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage DownloadFile(string filePath)
        {
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
                return response;
            }
            catch (Exception exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, exception.Message);
            }
        }
    }
}
