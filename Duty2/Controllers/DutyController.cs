﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Web.Http.Results;
using Duty2.Models;
using Duty2.ViewModels;
using Duty = Duty2.Models.Duty;

namespace Duty2.Controllers
{
    public class DutyController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<Resp> Get(string group, int month, int year)
        {
            var db = new DataContext();
            var duties = await 
                db.Duties.AsQueryable()
                    .Include(d => d.PartOfDay.Group)
                    .Include(d => d.User.Group)
                    .Where(d => d.User.Group.Description == group && d.Date.Month == month && d.Date.Year == year).ToListAsync();

            var result = duties.Select(duty => new ViewModels.Duty()
            {
                Day = duty.Date.Day,
                PartOfDay = duty.PartOfDay,
                Id = duty.Id,
                User = duty.User
            });

            return new Resp()
            {
                Id = "Duties",
                Responce = result
            };
        }

        [Authorize]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [ResponseType(typeof (OkResult))]
        public async Task<IHttpActionResult> Post(SelectorContainer selectorContainer)
        {
            var db = new DataContext();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            foreach (var item in selectorContainer.Selector)
            {
                var dutysInDB = await db.Duties.Where(
                    d =>
                        d.User.Id == item.Id && d.Date == selectorContainer.Date && d.PartOfDay.Sortpos == selectorContainer.DayPart &&
                        d.User.Group.Id == 1).ToListAsync();

                if (!dutysInDB.Any() && item.isSelected)
                {
                    db.Duties.Add(new Duty()
                    {
                        User = await db.Users.FindAsync(item.Id),
                        Date = selectorContainer.Date.Date,
                        PartOfDay = await db.DayParts.Where(d => d.Sortpos == selectorContainer.DayPart).FirstAsync()
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
