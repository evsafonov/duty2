using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Duty2.Models;
using Duty2.ViewModels;

namespace Duty2.Controllers
{
    public class AuthController : ApiController
    {
        public Resp Get()
        {
            var httpContextWrapper = Request.Properties["MS_HttpContext"] as HttpContextWrapper;
            var isAusorized = httpContextWrapper != null && httpContextWrapper.User.Identity.IsAuthenticated;

            return new Resp()
            {
                Id = "IsUserAuthorized",
                Responce = isAusorized
            };
        }
    }
}