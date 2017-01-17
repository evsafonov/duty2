using System;
using System.Linq;
using System.Web.Http;
using Duty2.DTOs;
using Duty2.Helpers;
using Duty2.Models;

namespace Duty2.Controllers
{
    public class PartOfDayController : ApiController
    {
        public Resp Get(string group)
        {
            var db = new DataContext();
            var dayParts = from d in db.DayParts
                where d.Group.Description == @group
                select new {Description = d.TimeFrom + " - " + d.TimeTo, d.Sortpos, d.Group};

            return new Resp()
            {
                Id = "PartOfDays",
                Responce = dayParts
            };
        }
    }
}