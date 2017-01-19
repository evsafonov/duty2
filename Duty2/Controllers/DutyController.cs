using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Duty2.DTOs;
using Duty2.Helpers;
using Duty2.Models;
using NLog;
using Duty = Duty2.Models.Duty;

namespace Duty2.Controllers
{
    public class DutyController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public async Task<Resp> Get(string group, int month, int year)
        {
            var db = new DataContext();
            var duties = await 
                db.Duties.AsQueryable()
                    .Include(d => d.PartOfDay.Group)
                    .Include(d => d.User.Group)
                    .Where(d => d.User.Group.Description == group && d.Date.Month == month && d.Date.Year == year).ToListAsync();

            var result = duties.Select(duty => new DTOs.Duty()
            {
                Day = duty.Date.Day,
                PartOfDay = duty.PartOfDay,
                Id = duty.Id,
                User = duty.User,
                Sortpos = duty.Sortpos
            });

            return new Resp()
            {
                Id = "Duties",
                Responce = result
            };
        }

        [Authorize]
        [ClausAuth]
        public async Task<IHttpActionResult> Post(SelectorContainer selectorContainer, string group)
        {
            string userName =
                ((ClaimsIdentity) ((HttpContextWrapper) Request.Properties["MS_HttpContext"]).User.Identity)?.Claims
                    .FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;

            var selctedUsers = "";
            foreach (var selector in selectorContainer.Selector.Where(selector => selector.isSelected))
            {
                if (selctedUsers != "") selctedUsers += ", ";
                selctedUsers += selector.Name;
            }

            logger.Info("Выбранные пользователи: {0}; Дата: {1}; Дежурство: {2} Пользователь: {3}", selctedUsers,
                selectorContainer.Date, selectorContainer.DayPart, userName);


            var db = new DataContext();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var item in selectorContainer.Selector)
            {
                var dutysInDB = await db.Duties.Where(
                    d =>
                        d.User.Id == item.Id && d.Date == selectorContainer.Date &&
                        d.PartOfDay.Sortpos == selectorContainer.DayPart &&
                        d.User.Group.Description == group).ToListAsync();

                if (!dutysInDB.Any() && item.isSelected)
                {
                    db.Duties.Add(new Duty()
                    {
                        User = await db.Users.FindAsync(item.Id),
                        Date = selectorContainer.Date.Date,
                        PartOfDay = await db.DayParts.Where(d => d.Sortpos == selectorContainer.DayPart).FirstAsync(),
                        Sortpos = item.sortpos
                    });
                }

                if (dutysInDB.Any() && !item.isSelected)
                {
                    foreach (var duty in dutysInDB)
                    {
                        db.Duties.Remove(duty);
                    }
                }
            }

            await db.SaveChangesAsync();
            return Ok();
        }
    }
}

