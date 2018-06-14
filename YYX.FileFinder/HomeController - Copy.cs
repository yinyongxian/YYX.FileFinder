using System.Net.Http;
using System.Web.Http;
using System.Windows.Forms;

namespace YYX.FileFinder
{
    public class HomeController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Index()
        {
            var path = Application.StartupPath;
            return Request.Response(path);
        }
    }
}
