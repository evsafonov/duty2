using System.Collections.Generic;
using System.Linq;
using System.Web.DynamicData;
using System.Web.Http;
using System.Web.Http.Cors;
using Duty2.DTOs;
using Duty2.Helpers;
using Duty2.Models;

namespace Duty2.Controllers
{
    [Authorize]
    [ClausAuth]
    public class PaymentController : ApiController 
    {
        public Resp Get(string group, int month, int year)
        {
            var db = new DataContext();
            IEnumerable<UserPayment> result =
                (from d in db.Duties
                where d.User.Group.Description == @group && d.Date.Month == month && d.Date.Year == year
                group d by d.User
                into g
                select new UserPayment(){ User = g.Key, Count = g.Count()}).OrderBy(u => u.User.Name);
                

            return new Resp
            {
                Id = "PartOfDays",
                Responce = result
            };
        }
    }

}
