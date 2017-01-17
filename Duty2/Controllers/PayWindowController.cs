using System.Web.Mvc;
using Duty2.Helpers;

namespace Duty2.Controllers
{
    public class PayWindowController : Controller
    {
        // GET: PayWindow
        [Authorize]
        [ClausAuth]
        public ActionResult Index()
        {
            return View();
        }
    }
}

