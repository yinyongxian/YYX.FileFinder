using System.Net.Http;
using System.Web.Http;

namespace YYX.FileFinder
{
    public class FolderController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Open(string path)
        {
           return Request.Response(path);
        }
    }
}
