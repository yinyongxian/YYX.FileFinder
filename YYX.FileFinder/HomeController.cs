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

            ContentLog.WriteLine(Request.ToString());

            var httpResponseMessage = Request.Response(path);

            ContentLog.WriteLine(httpResponseMessage.ToString());

            return httpResponseMessage;
        }
    }
}
