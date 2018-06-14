using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Web.Http;
using System.Windows.Forms;

namespace YYX.FileFinder
{
    public class HomeController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Index()
        {
            try
            {
                var directoryInfo = new DirectoryInfo(Application.StartupPath);
                var fileInfos = directoryInfo.GetFiles();
                var fileInfo = fileInfos.OrderBy(item => item.CreationTime).LastOrDefault();
                if (fileInfo == null)
                {
                    throw new Exception("未找到文件");
                }

                var fileName = fileInfo.Name;
                var assembly = Assembly.GetExecutingAssembly();
                var resourceStream = assembly.GetManifestResourceStream("YYX.FileFinder.index.html");
                if (resourceStream == null)
                {
                    throw new Exception("首页异常");
                }
                var response = Request.CreateResponse(HttpStatusCode.OK);
                using (var stream = new StreamReader(resourceStream))
                {
                    var html = stream.ReadToEnd();
                    response.Content = new StringContent(html, Encoding.UTF8);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
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
