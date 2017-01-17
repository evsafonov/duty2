using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Duty2.DTOs;
using Duty2.Models;
using User = Duty2.DTOs.User;

namespace Duty2.Controllers
{
    public class UserController : ApiController
    {
        public Resp Get(string group)
        {
            var db = new DataContext();

            var result = new List<User>();
            var users = db.Users.AsQueryable().Where(d => d.Group.Description == group);

            foreach (var user in users)
            {
                result.Add(new User() {Id = user.Id, Name = user.Name, IsHidden = user.IsHidden});
            }

            return new Resp()
            {
                Id = "UsersForSelect",
                Responce = result.OrderBy(u => u.Name)
            };
        }
    }
}