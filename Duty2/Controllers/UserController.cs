using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Duty2.Models;
using Duty2.ViewModels;

namespace Duty2.Controllers
{
    public class UserController : ApiController
    {
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