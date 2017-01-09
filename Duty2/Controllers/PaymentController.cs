using System.Linq;
using System.Web.DynamicData;
using System.Web.Http;
using Duty2.DTOs;
using Duty2.Helpers;
using Duty2.Models;

namespace Duty2.Controllers
{
    [Authorize]
    [ClausAuth]
    public class PaymentController : ApiController 
    {
        public Resp Get(string Group, int month, int year)
        {
            var db = new DataContext();
            dynamic result =
                from d in db.Duties
                where d.User.Group.Description == Group && d.Date.Month == month && d.Date.Year == year
                group d by d.User
                into g
                select new {user = g.Key, Count = g.Count()};
                

            return new Resp
            {
                Id = "PartOfDays",
                Responce = result
            };
        }
    }

}
