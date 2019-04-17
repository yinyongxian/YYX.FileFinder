using System.Net.Http;
using System.Web.Http;

namespace YYX.FileFinder
{
    public class FolderController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Open(string path)
        {
            ContentLog.WriteLine(Request.ToString());

            var httpResponseMessage = Request.Response(path);

            ContentLog.WriteLine(httpResponseMessage.ToString());

            return httpResponseMessage;
        }
    }
}
