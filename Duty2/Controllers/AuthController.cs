using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using Duty2.DTOs;

namespace Duty2.Controllers
{
    public class AuthController : ApiController
    {
        public Resp Get()
        {
            string userGroup = null;
            var claimsIdentity = ((HttpContextWrapper)Request.Properties["MS_HttpContext"]).User.Identity as ClaimsIdentity;
            var userGroupClaim = claimsIdentity?.Claims.FirstOrDefault(c => c.Type == "http://2gis.local/Duty/DutyGroups");
            if (userGroupClaim != null)
            {
                userGroup = userGroupClaim.Value;
            }

            return new Resp()
            {
                Id = "IsUserAuthorized",
                Responce = userGroup
            };
        }
    }
}