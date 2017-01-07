using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Duty2.Helpers
{
    public class ClausAuthAttribute : AuthorizeAttribute
    {

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var userGroup = actionContext.Request.GetQueryNameValuePairs().ToDictionary(x => x.Key, x => x.Value).FirstOrDefault(g => g.Key == "group").Value;

            var claimsIdentity = actionContext.RequestContext.Principal.Identity as ClaimsIdentity;
            var userGroupClaim = claimsIdentity?.Claims.FirstOrDefault(c => c.Type == "http://2gis.local/Duty/DutyGroups");

            if (userGroupClaim == null || userGroup != userGroupClaim.Value)
            {
                return false;
            }
            return true;
        }
    }
}
