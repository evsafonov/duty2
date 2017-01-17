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
            var claimsIdentity = ((HttpContextWrapper)Request.Properties["MS_HttpContext"]).User.Identity as ClaimsIdentity;
            var userGroupClaims = claimsIdentity?.Claims.Where(c => c.Type == "http://2gis.local/Duty/DutyGroups");
            if (userGroupClaims != null)
            {
                var userGroups = from c in userGroupClaims select new {usergroup = c.Value};
                return new Resp()
                {
                    Id = "IsUserAuthorized",
                    Responce = userGroups
                };
            }
            return new Resp()
            {
                Id = "IsUserAuthorized",
                Responce = null
            };
        }
    }
}