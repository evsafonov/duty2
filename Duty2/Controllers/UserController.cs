using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Duty2.Models;
using Duty2.ViewModels;
using User = Duty2.ViewModels.User;

namespace Duty2.Controllers
{
    public class UserController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public Resp Get(string group)
        {
            var db = new DataContext();

            var result = new List<ViewModels.User>();
            var users = db.Users.AsQueryable().Where(d => d.Group.Description == group);

            foreach (var user in users)
            {
                result.Add(new ViewModels.User() {Id = user.Id, Name = user.Name});
            }

            return new Resp()
            {
                Id = "UsersForSelect",
                Responce = result
            };
        }
    }
}