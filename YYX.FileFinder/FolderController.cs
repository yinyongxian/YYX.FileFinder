using System.Net.Http;
using System.Web.Http;

namespace YYX.FileFinder
{
    public class FolderController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Open(string path)
        {
            Log4Log.Info(Request.ToString());
            ContentLog.WriteLine(Request.ToString());

            var httpResponseMessage = Request.Response(path);

            Log4Log.Info(httpResponseMessage.ToString());
            ContentLog.WriteLine(httpResponseMessage.ToString());

            return httpResponseMessage;
        }
    }
}
