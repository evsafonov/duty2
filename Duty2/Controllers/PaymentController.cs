using System.Linq;
using System.Web.Http;
using Duty2.Helpers;
using Duty2.Models;
using Duty2.ViewModels;

namespace Duty2.Controllers
{
    [Authorize]
    [ClausAuth]
    public class PaymentController : ApiController 
    {
        public Resp Get(string group, int month, int year)
        {
            var db = new DataContext();
            dynamic result =
                from d in db.Duties
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
