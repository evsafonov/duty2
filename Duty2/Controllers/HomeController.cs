using System.Web.Mvc;

namespace Duty2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View("View");
        }
    }
}