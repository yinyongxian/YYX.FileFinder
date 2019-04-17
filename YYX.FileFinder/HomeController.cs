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

            Log4Log.Info(Request.ToString());
            ContentLog.WriteLine(Request.ToString());

            var httpResponseMessage = Request.Response(path);

            Log4Log.Info(httpResponseMessage.ToString());
            ContentLog.WriteLine(httpResponseMessage.ToString());

            return httpResponseMessage;
        }
    }
}
